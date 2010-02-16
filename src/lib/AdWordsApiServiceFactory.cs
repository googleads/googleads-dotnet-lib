// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

// Author: api.anash@gmail.com (Anash P. Oommen)

using com.google.api.adwords;
using com.google.api.adwords.lib;

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace com.google.api.adwords.lib {
  /// <summary>
  /// The factory class for all AdWords API services.
  /// </summary>
  class AdWordsApiServiceFactory : ServiceFactory {
    /// <summary>
    /// Default public constructor.
    /// </summary>
    public AdWordsApiServiceFactory() {
      requestHeader = ReadHeadersFromConfig();
    }

    /// <summary>
    /// Create a service of desired type.
    /// </summary>
    /// <param name="signature">The service creation signature, for instance,
    /// ApiServices.v200909.CampaignService.</param>
    /// <param name="user">The user for which this service is being created.
    /// </param>
    /// <returns>The service object.</returns>
    public override object CreateService(ServiceSignature signature, AdWordsUser user) {
      if (!(signature is AdWordsApiServiceSignature)) {
        throw new ArgumentException("Expecting a AdWordsApiServiceSignature object.");
      }
      AdWordsApiServiceSignature awapiSignature = (AdWordsApiServiceSignature) signature;

      string serviceType = "com.google.api.adwords." + awapiSignature.version +
          "." + awapiSignature.serviceName;
      object service = Assembly.GetExecutingAssembly().CreateInstance(serviceType);
      Type type = service.GetType();
      PropertyInfo propInfo = type.GetProperty("RequestHeader");
      if (propInfo != null) {
        propInfo.SetValue(service, requestHeader.Clone(), null);

        FixRequestHeaderNameSpace(awapiSignature, service);

        if (ApplicationConfiguration.proxy != null) {
          propInfo = type.GetProperty("Proxy");
          if (propInfo != null) {
            propInfo.SetValue(service, ApplicationConfiguration.proxy, null);
          }
        }

        if (!string.IsNullOrEmpty(url)) {
          string fullUrl = string.Format("{0}/api/adwords/{1}/{2}/{3}",
            url, awapiSignature.groupName, awapiSignature.version, awapiSignature.serviceName);
          propInfo = type.GetProperty("Url");
          if (propInfo != null) {
            propInfo.SetValue(service, fullUrl, null);
          }
        }
      }
      propInfo = type.GetProperty("Parent");
      if (propInfo != null) {
        propInfo.SetValue(service, user, null);
      }

      return service;
    }

    /// <summary>
    /// Fix the request header namespace in outgoing Soap Requests, so that
    /// cross namespace requests can work properly.
    /// </summary>
    /// <param name="signature">The service creation parameters.</param>
    /// <param name="service">The service object for which RequestHeader
    /// needs to be patched.</param>
    private void FixRequestHeaderNameSpace(AdWordsApiServiceSignature signature, object service) {
      // Set the header namespace prefix. For all /cm services, the members
      // shouldn't have xmlns. For all other services, the members should have
      // /cm as xmlns.
      object[] attributes = service.GetType().GetCustomAttributes(false);
      foreach (object attribute in attributes) {
        if (attribute is WebServiceBindingAttribute) {
          WebServiceBindingAttribute binding = (WebServiceBindingAttribute) attribute;
          string delimiter = "/api/adwords/";
          string xmlns = binding.Namespace.Substring(0, binding.Namespace.IndexOf(delimiter)
              + delimiter.Length);
          xmlns += "cm/" + signature.version;
          if (xmlns == binding.Namespace) {
            xmlns = "";
          }

          RequestHeader svcRequestHeader = null;
          PropertyInfo propInfo = service.GetType().GetProperty("RequestHeader");
          if (propInfo != null) {
            svcRequestHeader = (RequestHeader) propInfo.GetValue(service, null);

            if (svcRequestHeader != null) {
              PropertyInfo wsPropInfo = svcRequestHeader.GetType().GetProperty("TargetNamespace");
              if (wsPropInfo != null) {
                wsPropInfo.SetValue(svcRequestHeader, xmlns, null);
              }
            }
          }
        }
      }
    }

    /// <summary>
    /// Switch Adwords API classes to sandbox mode.
    /// </summary>
    public override void UseSandbox() {
      url = "https://adwords-sandbox.google.com";
    }

    /// <summary>
    /// Loads the AdWords API headers from App.config
    /// </summary>
    /// <returns>A map, with key=headername and value=SoapHeader object.
    /// </returns>
    private RequestHeader ReadHeadersFromConfig() {
      requestHeader = new RequestHeader();
      if (!string.IsNullOrEmpty(ApplicationConfiguration.authToken)) {
        requestHeader.authToken = ApplicationConfiguration.authToken;
      } else {
        if (!string.IsNullOrEmpty(ApplicationConfiguration.email)) {
          requestHeader.authToken =
              new AuthToken(ApplicationConfiguration.email,
                  ApplicationConfiguration.password).GetToken();
        } else {
          requestHeader.authToken = "";
        }
      }
      if (!string.IsNullOrEmpty(ApplicationConfiguration.clientCustomerId)) {
        requestHeader.clientCustomerId = ApplicationConfiguration.clientCustomerId;
      }
      requestHeader.clientEmail = ApplicationConfiguration.clientEmail;
      requestHeader.developerToken = ApplicationConfiguration.developerToken;

      // Application token is now optional.
      if (!string.IsNullOrEmpty(ApplicationConfiguration.applicationToken)) {
        requestHeader.applicationToken = ApplicationConfiguration.applicationToken;
      }
      requestHeader.userAgent = Useragent;
      return requestHeader;
    }

    /// <summary>
    /// Gets or sets the Request Header.
    /// </summary>
    public RequestHeader RequestHeader {
      get {
        return requestHeader;
      }
      set {
        requestHeader = value;
      }
    }

    /// <summary>
    /// Make a request header given a set of key-value pairs.
    /// </summary>
    /// <param name="headers">A dictionary holding key-value pairs.</param>
    /// <returns>A RequestHeader object, to be used with AdWords API services.
    /// </returns>
    internal RequestHeader MakeRequestHeaders(Dictionary<string, string> headers) {
      requestHeader = new RequestHeader();

      Type type = typeof(RequestHeader);
      if (!headers.ContainsKey("authToken")) {
        requestHeader.authToken = new AuthToken(headers["email"], headers["password"]).GetToken();
      }
      foreach (string key in headers.Keys) {
        PropertyInfo propInfo = type.GetProperty(key);
        if (propInfo != null) {
          propInfo.SetValue(requestHeader, headers[key], null);
        }
      }
      return requestHeader;
    }

    /// <summary>
    /// Url for AdWords API series.
    /// </summary>
    private string url = ApplicationConfiguration.adWordsApiUrl;

    /// <summary>
    /// The auth token to be used with AdWords API services.
    /// </summary>
    private RequestHeader requestHeader = null;
  }

  /// <summary>
  /// Service creation params for AdWords API family of services.
  /// </summary>
  class AdWordsApiServiceSignature : ServiceSignature {
    /// <summary>
    /// The service version, for instance, v200909.
    /// </summary>
    public string version;

    /// <summary>
    /// The group name, for instance, cm.
    /// </summary>
    public string groupName;
  }
}

namespace com.google.api.adwords {
  /// <summary>
  /// Soap Request header for AdWords API services.
  /// </summary>
  public class RequestHeader : SoapHeader, IXmlSerializable, ICloneable {
    /// <summary>
    /// Application token.
    /// </summary>
    private string applicationTokenField;

    /// <summary>
    /// Auth token.
    /// </summary>
    private string authTokenField;

    /// <summary>
    /// Client customer Id.
    /// </summary>
    private string clientCustomerIdField;

    /// <summary>
    /// Client email.
    /// </summary>
    private string clientEmailField;

    /// <summary>
    /// Developer token.
    /// </summary>
    private string developerTokenField;

    /// <summary>
    /// User agent.
    /// </summary>
    private string userAgentField;

    /// <summary>
    /// Validate only header - useful if you just want to check if the call is fine.
    /// </summary>
    private bool? validateOnlyField;

    /// <summary>
    /// The xml namespace under which the members should be serialized.
    /// </summary>
    private string targetNamespace;

    /// <summary>
    /// Gets or sets the application token.
    /// </summary>
    public string applicationToken {
      get {
        return this.applicationTokenField;
      }
      set {
        this.applicationTokenField = value;
      }
    }

    /// <summary>
    /// Gets or sets the auth token.
    /// </summary>
    public string authToken {
      get {
        return this.authTokenField;
      }
      set {
        this.authTokenField = value;
      }
    }

    /// <summary>
    /// Gets or sets the client customer id.
    /// </summary>
    public string clientCustomerId {
      get {
        return this.clientCustomerIdField;
      }
      set {
        this.clientCustomerIdField = value;
      }
    }

    /// <summary>
    /// Gets or sets the client email.
    /// </summary>
    public string clientEmail {
      get {
        return this.clientEmailField;
      }
      set {
        this.clientEmailField = value;
      }
    }

    /// <summary>
    /// Gets or sets the developer token.
    /// </summary>
    public string developerToken {
      get {
        return this.developerTokenField;
      }
      set {
        this.developerTokenField = value;
      }
    }

    /// <summary>
    /// Gets or sets the user agent.
    /// </summary>
    public string userAgent {
      get {
        return this.userAgentField;
      }
      set {
        this.userAgentField = value;
      }
    }

    /// <summary>
    /// Gets or sets the validateOnly header.
    /// </summary>
    public bool? validateOnly {
      get {
        return this.validateOnlyField;
      }
      set {
        this.validateOnlyField = value;
      }
    }

    /// <summary>
    /// Gets or sets the target namespace under which the child
    /// nodes should be serialized.
    /// </summary>
    public string TargetNamespace {
      get {
        return targetNamespace;
      }
      set {
        targetNamespace = value;
      }
    }

    /// <summary>
    /// Gets the schema for this object. Can return null if the
    /// server does not expect the schema.
    /// </summary>
    /// <returns>The xml schema associated with this class, or null
    /// if not applicable.</returns>
    public XmlSchema GetSchema() {
      return null;
    }

    /// <summary>
    /// Deserialize the object from xml.
    /// </summary>
    /// <param name="reader">The xml reader for reading the
    /// serialized xml.</param>
    public void ReadXml(XmlReader reader) {
      return;
    }

    /// <summary>
    /// Serialize the object into an xml.
    /// </summary>
    /// <param name="writer">The writer to which the serialized data
    /// should be written.</param>
    public void WriteXml(XmlWriter writer) {
      string[] headersToSerialize = {"applicationToken", "authToken", "clientCustomerId",
        "clientEmail", "developerToken", "userAgent", "validateOnly"};
      foreach (string headerName in headersToSerialize) {
        PropertyInfo propInfo = this.GetType().GetProperty(headerName);
        object value = propInfo.GetValue(this, null);
        if (value != null) {
          if (value is bool) {
            // Since we do custom serialization, remember to send bool
            // in lower case.
            value = value.ToString().ToLower();
          }

          if (value is string) {
            // Should not send an empty field (e.g. clientEmail) to the server.
            if (string.IsNullOrEmpty(value.ToString())) {
              value = null;
            }
          }
        }

        if (value != null) {
          if (String.IsNullOrEmpty(targetNamespace)) {
            writer.WriteElementString(propInfo.Name, value.ToString());
          } else {
            writer.WriteElementString(propInfo.Name, targetNamespace, value.ToString());
          }
        }
      }
    }

    #region ICloneable Members

    public object Clone() {
      RequestHeader header = new RequestHeader();
      header.applicationToken = this.applicationToken;
      header.authToken = this.authToken;
      header.clientCustomerId = this.clientCustomerId;
      header.clientEmail = this.clientEmail;
      header.developerToken = this.developerToken;
      header.targetNamespace = this.targetNamespace;
      header.userAgent = this.userAgent;
      header.validateOnly = this.validateOnly;
      return header;
    }

    #endregion
  }

  /// <summary>
  /// Soap Response header for AdWords API services.
  /// </summary>
  public class ResponseHeader : SoapHeader, IXmlSerializable {
    /// <summary>
    /// Request ID for this API call.
    /// </summary>
    private string requestIdField;

    /// <summary>
    /// Number of operations for this API call.
    /// </summary>
    private long? operationsField;

    /// <summary>
    /// Response time for this API call.
    /// </summary>
    private long? responseTimeField;

    /// <summary>
    /// Units consumed for this API call.
    /// </summary>
    private long? unitsField;

    /// <summary>
    /// Gets or sets the request id for this API call.
    /// </summary>
    public string requestId {
      get {
        return this.requestIdField;
      }
      set {
        this.requestIdField = value;
      }
    }

    /// <summary>
    /// Gets or sets the number of operations for this API call.
    /// </summary>
    public long? operations {
      get {
        return this.operationsField;
      }
      set {
        this.operationsField = value;
      }
    }

    /// <summary>
    /// Gets or sets the response time for this API call.
    /// </summary>
    public long? responseTime {
      get {
        return this.responseTimeField;
      }
      set {
        this.responseTimeField = value;
      }
    }

    /// <summary>
    /// Gets or sets the number of units consumed for this API call.
    /// </summary>
    public long? units {
      get {
        return this.unitsField;
      }
      set {
        this.unitsField = value;
      }
    }

    /// <summary>
    /// Gets the schema for this object. Can return null if the
    /// server does not expect the schema.
    /// </summary>
    /// <returns>The xml schema associated with this class, or null
    /// if not applicable.</returns>
    public XmlSchema GetSchema() {
      return null;
    }

    /// <summary>
    /// Deserialize the object from xml.
    /// </summary>
    /// <param name="reader">The xml reader for reading the
    /// serialized xml.</param>
    public void ReadXml(XmlReader reader) {
      long tempVal = 0;

      string elementXml = reader.ReadOuterXml();
      XmlDocument doc = new XmlDocument();
      doc.LoadXml(elementXml);
      foreach (XmlNode node in doc.DocumentElement.ChildNodes) {
        if (node is XmlElement) {
          switch (node.Name) {
            case "requestId":
              requestId = node.InnerText;
              break;

            case "operations":
              if (long.TryParse(node.InnerText, out tempVal)) {
                operations = tempVal;
              };
              break;

            case "responseTime":
              if (long.TryParse(node.InnerText, out tempVal)) {
                responseTime = tempVal;
              };
              break;

            case "units":
              if (long.TryParse(node.InnerText, out tempVal)) {
                units = tempVal;
              };
              break;
          }
        }
      }
    }

    /// <summary>
    /// Serialize the object into an xml.
    /// </summary>
    /// <param name="writer">The writer to which the serialized data
    /// should be written.</param>
    public void WriteXml(XmlWriter writer) {
      return;
    }
  }
}
