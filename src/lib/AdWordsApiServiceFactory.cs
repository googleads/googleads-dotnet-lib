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

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Web.Services;

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
    /// ApiServices.v200906.CampaignService.</param>
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
        propInfo.SetValue(service, requestHeader, null);

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

        propInfo = type.GetProperty("Parent");
        if (propInfo != null) {
          propInfo.SetValue(service, user, null);
        }
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

          PropertyInfo wsPropInfo = requestHeader.GetType().GetProperty("TargetNamespace");
          if (wsPropInfo != null) {
            wsPropInfo.SetValue(requestHeader, xmlns, null);
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
      requestHeader.authToken =
          new AuthToken(ApplicationConfiguration.email,
              ApplicationConfiguration.password).GetToken();
      if (!string.IsNullOrEmpty(ApplicationConfiguration.clientCustomerId)) {
        requestHeader.clientCustomerId = ApplicationConfiguration.clientCustomerId;
      }
      requestHeader.clientEmail = ApplicationConfiguration.clientEmail;
      requestHeader.developerToken = ApplicationConfiguration.developerToken;
      requestHeader.applicationToken = ApplicationConfiguration.applicationToken;
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
    /// The service version, for instance, v200906.
    /// </summary>
    public string version;

    /// <summary>
    /// The group name, for instance, cm.
    /// </summary>
    public string groupName;
  }
}
