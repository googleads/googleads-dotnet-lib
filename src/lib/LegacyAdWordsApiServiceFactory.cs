// Copyright 2009, Google Inc. All Rights Reserved.
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

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Web.Services.Protocols;

namespace com.google.api.adwords.lib {
  /// <summary>
  /// The factory class for all the legacy AdWords API services.
  /// </summary>
  class LegacyAdWordsApiServiceFactory : ServiceFactory {
    /// <summary>
    /// Default public constructor.
    /// </summary>
    public LegacyAdWordsApiServiceFactory() {
      headers = ReadHeadersFromConfig();
    }

    /// <summary>
    /// Create a service of desired type.
    /// </summary>
    /// <param name="signature">Signature of the service being created.</param>
    /// <param name="user">The user for which this service is being created.
    /// </param>
    /// <returns>The service object.</returns>
    public override object CreateService(ServiceSignature signature, AdWordsUser user) {
      if (!(signature is LegacyAdwordsApiServiceSignature)) {
        throw new ArgumentException("Expecting a LegacyAdwordsApiServiceSignature object.");
      }
      LegacyAdwordsApiServiceSignature awapiSignature =
          (LegacyAdwordsApiServiceSignature) signature;
      string serviceType = "com.google.api.adwords." + awapiSignature.version +
          "." + awapiSignature.serviceName;
      object service = Assembly.GetExecutingAssembly().CreateInstance(serviceType);

      Type type = service.GetType();
      PropertyInfo propInfo = null;

      if (this.headers != null) {
        foreach (string key in headers.Keys) {
          propInfo = type.GetProperty(key);
          if (propInfo != null) {
            propInfo.SetValue(service, headers[key], null);
          }
        }
      }

      if (ApplicationConfiguration.proxy != null) {
        propInfo = type.GetProperty("Proxy");
        if (propInfo != null) {
          propInfo.SetValue(service, ApplicationConfiguration.proxy, null);
        }
      }
      if (!string.IsNullOrEmpty(urlPrefix)) {
        string fullUrl = urlPrefix + "/api/adwords/" + awapiSignature.version + "/" +
            awapiSignature.serviceName;
        propInfo = type.GetProperty("Url");
        if (propInfo != null) {
          propInfo.SetValue(service, fullUrl, null);
        }
      }
      propInfo = type.GetProperty("Parent");
      if (propInfo != null) {
        propInfo.SetValue(service, user, null);
      }

      return service;
    }

    /// <summary>
    /// Switch AdWords API classes to sandbox mode.
    /// </summary>
    public override void UseSandbox() {
      urlPrefix = "https://sandbox.google.com";
    }

    /// <summary>
    /// Loads the AdWords API headers from App.config
    /// </summary>
    /// <returns>A map, with key=headername and value=SoapHeader object.
    /// </returns>
    private Dictionary<string, SoapHeader> ReadHeadersFromConfig() {
      headers = new Dictionary<string, SoapHeader>();
      headers["emailValue"] = MakeSoapHeader("email",
          ApplicationConfiguration.email);
      headers["passwordValue"] = MakeSoapHeader("password",
          ApplicationConfiguration.password);
      headers["useragentValue"] = MakeSoapHeader("useragent", Useragent);
      headers["developerTokenValue"] = MakeSoapHeader("developerToken",
          ApplicationConfiguration.developerToken);
      headers["applicationTokenValue"] = MakeSoapHeader("applicationToken",
          ApplicationConfiguration.applicationToken);
      headers["clientEmailValue"] = MakeSoapHeader("clientEmail",
          ApplicationConfiguration.clientEmail);
      if (!string.IsNullOrEmpty(ApplicationConfiguration.clientCustomerId)) {
        headers["clientCustomerIdValue"] = MakeSoapHeader("clientCustomerId",
            ApplicationConfiguration.clientCustomerId);
      }
      return headers;
    }

    /// <summary>
    /// Convert a dictionary of string header values to SoapHeader objects.
    /// </summary>
    /// <param name="headers">The dictionary, with key as the header field name
    /// and value as the header value.</param>
    /// <returns>A dictionary, with key as header field name and value as a
    /// SoapHeader object.</returns>
    /// <remarks>This function is used by the constructors that accept header
    /// values as string rather than SoapHeader objects.</remarks>
    public Dictionary<string, SoapHeader> MakeSoapHeaders(Dictionary<string, string> headers) {
      Dictionary<string, SoapHeader> soapHeaders = new Dictionary<string, SoapHeader>();
      foreach (string key in headers.Keys) {
        SoapHeader tempHeader = MakeSoapHeader(key, headers[key]);
        if (tempHeader != null) {
          soapHeaders[key + "Value"] = tempHeader;
        }
      }
      return soapHeaders;
    }

    /// <summary>
    /// Creates a SoapHeader for use with AdWords API.
    /// </summary>
    /// <param name="headerName">Name of the header.</param>
    /// <param name="value">String value for the header.</param>
    /// <returns>The SoapHeader object.</returns>
    private SoapHeader MakeSoapHeader(string headerName, string value) {
      string typeName = "com.google.api.adwords.v13." + headerName;
      SoapHeader header = (SoapHeader) Assembly.GetExecutingAssembly().
          CreateInstance(typeName);
      if (header != null) {
        PropertyInfo propInfo = header.GetType().GetProperty("Value");
        if (propInfo != null) {
          propInfo.SetValue(header, new string[] { value }, null);
        }
      }
      return header;
    }

    /// <summary>
    /// Gets or sets the SOAP Headers.
    /// </summary>
    public Dictionary<string, SoapHeader> Headers {
      get {
        return headers;
      }
      set {
        headers = value;
      }
    }

    /// <summary>
    /// The overridden SOAP headers to be used with AdWords API services.
    /// If the settings from App.config is not overridden, this
    /// field will be null.
    /// </summary>
    private Dictionary<string, SoapHeader> headers = null;

    /// <summary>
    /// Url prefix for Legacy AdWords API.
    /// </summary>
    private string urlPrefix = ApplicationConfiguration.legacyAdWordsApiUrl;
  }

  /// <summary>
  /// Service creation params for v13 family of services.
  /// </summary>
  class LegacyAdwordsApiServiceSignature : ServiceSignature {
    /// <summary>
    /// The service version, for instance, v13.
    /// </summary>
    public string version;
  }
}
