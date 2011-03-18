// Copyright 2011, Google Inc. All Rights Reserved.
//
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

using Google.Api.Ads.Common.Lib;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Google.Api.Ads.AdWords.Lib {
  /// <summary>
  /// The factory class for all AdWords API services.
  /// </summary>
  public class AdWordsServiceFactory : ServiceFactory {
    /// <summary>
    /// The request header to be used with AdWords API services.
    /// </summary>
    private RequestHeader requestHeader;

    /// <summary>
    /// The config class to be used with this object.
    /// </summary>
    private AdWordsAppConfig config = new AdWordsAppConfig();

    /// <summary>
    /// Gets a useragent string that can be used with the library.
    /// </summary>
    protected string Useragent {
      get {
        return String.Join("", new string[] {config.Signature, "|", config.UserAgent});
      }
    }

    /// <summary>
    /// Gets an App.config reader suitable for this factory.
    /// </summary>
    public override AppConfigBase AppConfig {
      get {
        return config;
      }
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
    /// Default public constructor.
    /// </summary>
    public AdWordsServiceFactory() {
    }

    /// <summary>
    /// Create SOAP headers based on a set of key-value pairs.
    /// </summary>
    /// <param name="headers">A dictionary, with key-value pairs as headername,
    /// headervalue.</param>
    public override void SetHeaders(Dictionary<string, string> headers) {
      this.requestHeader = MakeRequestHeaders(headers);
    }

    /// <summary>
    /// Create a service object.
    /// </summary>
    /// <param name="signature">Signature of the service being created.</param>
    /// <param name="user">The user for which the service is being created.
    /// <param name="serverUrl">The server to which the API calls should be
    /// made.</param>
    /// </param>
    /// <returns>An object of the desired service type.</returns>
    public override AdsClient CreateService(ServiceSignature signature, AdsUser user,
        Uri serverUrl) {
      if (serverUrl == null) {
        serverUrl = new Uri(config.AdWordsApiServer);
      }

      if (user == null) {
        throw new ArgumentNullException("user");
      }

      if (signature == null) {
        throw new ArgumentNullException("signature");
      }

      if (!(signature is AdWordsServiceSignature)) {
        throw new InvalidCastException(string.Format(CultureInfo.InvariantCulture,
            AdWordsErrorMessages.SignatureIsOfWrongType, typeof(AdWordsServiceSignature)));
      }

      AdWordsServiceSignature awapiSignature = signature as AdWordsServiceSignature;

      AdsClient service = (AdsClient) Activator.CreateInstance(awapiSignature.ServiceType);
      PropertyInfo propInfo = awapiSignature.ServiceType.GetProperty("RequestHeader");
      if (propInfo != null) {
        propInfo.SetValue(service, requestHeader.Clone(), null);
        FixRequestHeaderNameSpace(awapiSignature, service);
      }

      if (config.Proxy != null) {
        service.Proxy = config.Proxy;
      }
      service.Timeout = config.Timeout;
      service.Url = string.Format("{0}api/adwords/{1}/{2}/{3}",
          serverUrl.AbsoluteUri, awapiSignature.GroupName, awapiSignature.Version,
          awapiSignature.ServiceName);

      service.User = user;
      return service;
    }

    /// <summary>
    /// Reads the headers from App.config.
    /// </summary>
    /// <param name="config">The configuration class.</param>
    /// <returns>A dictionary, with key-value pairs as headername, headervalue.</returns>
    public override Dictionary<string, string> ReadHeadersFromConfig(AppConfigBase config) {
      AdWordsAppConfig awConfig = (AdWordsAppConfig) config;
      Dictionary<string, string> configHeaders = new Dictionary<string, string>();
      if (!string.IsNullOrEmpty(awConfig.AuthToken)) {
        configHeaders["authToken"] = awConfig.AuthToken;
      }

      configHeaders["email"] = awConfig.Email;
      configHeaders["password"] = awConfig.Password;

      if (!string.IsNullOrEmpty(awConfig.ClientCustomerId)) {
        configHeaders["clientCustomerId"] = awConfig.ClientCustomerId;
      }
      configHeaders["clientEmail"] = awConfig.ClientEmail;
      configHeaders["developerToken"] = awConfig.DeveloperToken;
      configHeaders["applicationToken"] = awConfig.ApplicationToken;
      configHeaders["userAgent"] = Useragent;
      return configHeaders;
    }

    /// <summary>
    /// Make a request header given a set of key-value pairs.
    /// </summary>
    /// <param name="headers">A dictionary holding key-value pairs.</param>
    /// <returns>A RequestHeader object, to be used with AdWords API services.
    /// </returns>
    private RequestHeader MakeRequestHeaders(Dictionary<string, string> headers) {
      RequestHeader requestHeader = new RequestHeader();

      Type type = typeof(RequestHeader);
      if (!headers.ContainsKey("authToken")) {
        requestHeader.authToken = new AuthToken(config, "adwords", headers["email"],
            headers["password"]).GetToken();
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
    /// Fix the request header namespace in outgoing Soap Requests, so that
    /// cross namespace requests can work properly.
    /// </summary>
    /// <param name="signature">The service creation parameters.</param>
    /// <param name="service">The service object for which RequestHeader
    /// needs to be patched.</param>
    private static void FixRequestHeaderNameSpace(AdWordsServiceSignature signature,
        AdsClient service) {
      // Set the header namespace prefix. For all /cm services, the members
      // shouldn't have xmlns. For all other services, the members should have
      // /cm as xmlns.
      object[] attributes = service.GetType().GetCustomAttributes(false);
      foreach (object attribute in attributes) {
        if (attribute is WebServiceBindingAttribute) {
          WebServiceBindingAttribute binding = (WebServiceBindingAttribute) attribute;
          string delimiter = "/api/adwords/";
          string xmlns = String.Join("", new String[] {
              binding.Namespace.Substring(0, binding.Namespace.IndexOf(delimiter) +
                  delimiter.Length), "cm/", signature.Version});
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
  }
}
