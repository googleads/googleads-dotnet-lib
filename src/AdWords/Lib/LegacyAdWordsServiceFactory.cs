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
using System.Reflection;
using System.Text;
using System.Web.Services.Protocols;

namespace Google.Api.Ads.AdWords.Lib {
  /// <summary>
  /// The factory class for all the legacy AdWords API services.
  /// </summary>
  public class LegacyAdWordsServiceFactory : ServiceFactory {
    /// <summary>
    /// The overridden SOAP headers to be used with AdWords API services.
    /// If the settings from App.config is not overridden, this
    /// field will be null.
    /// </summary>
    private Dictionary<string, SoapHeader> headers;

    /// <summary>
    /// Gets a useragent string that can be used with the library.
    /// </summary>
    protected string Useragent {
      get {
        AdWordsAppConfig awConfig = (AdWordsAppConfig) AppConfig;
        return String.Join("", new string[] {awConfig.Signature, "|", awConfig.UserAgent});
      }
    }

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public LegacyAdWordsServiceFactory() {
    }

    /// <summary>
    /// Create a service of desired type.
    /// </summary>
    /// <param name="signature">Signature of the service being created.</param>
    /// <param name="user">The user for which this service is being created.
    /// </param>
    /// <param name="serverUrl">The server to which the API calls should be
    /// made.</param>
    /// <returns>The service object.</returns>
    public override AdsClient CreateService(ServiceSignature signature, AdsUser user,
        Uri serverUrl) {
      AdWordsAppConfig awConfig = (AdWordsAppConfig) AppConfig;
      if (serverUrl == null) {
        serverUrl = new Uri(awConfig.LegacyAdWordsApiServer);
      }

      if (user == null) {
        throw new ArgumentNullException("user");
      }

      if (signature == null) {
        throw new ArgumentNullException("signature");
      }

      if (!(signature is LegacyAdwordsServiceSignature)) {
        throw new ArgumentException("Expecting a LegacyAdwordsApiServiceSignature object.");
      }
      LegacyAdwordsServiceSignature awapiSignature = (LegacyAdwordsServiceSignature) signature;

      AdsClient service = (AdsClient) Activator.CreateInstance(awapiSignature.ServiceType);

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

      if (awConfig.Proxy != null) {
        service.Proxy = awConfig.Proxy;
      }
      service.Timeout = awConfig.Timeout;
      service.Url = String.Join("", new string[] {serverUrl.AbsoluteUri, "api/adwords/",
          awapiSignature.Version, "/", awapiSignature.ServiceName});

      service.User = user;
      return service;
    }

    /// <summary>
    /// Reads the headers from App.config.
    /// </summary>
    /// <param name="config">The configuration class.</param>
    protected override void ReadHeadersFromConfig(AppConfigBase config) {
      AdWordsAppConfig awConfig = (AdWordsAppConfig) config;
      this.headers = new Dictionary<string, SoapHeader>();
      this.headers["emailValue"] = MakeSoapHeader("email", awConfig.Email);
      this.headers["passwordValue"] = MakeSoapHeader("password", awConfig.Password);
      this.headers["clientEmailValue"] = MakeSoapHeader("clientEmail", awConfig.ClientEmail);
      if (string.IsNullOrEmpty(awConfig.ClientCustomerId)) {
        this.headers["clientCustomerIdValue"] = MakeSoapHeader("clientCustomerId",
            awConfig.ClientCustomerId);
      }
      this.headers["developerTokenValue"] = MakeSoapHeader("developerToken",
          awConfig.DeveloperToken);
      this.headers["applicationTokenValue"] = MakeSoapHeader("applicationToken",
          awConfig.ApplicationToken);
      this.headers["useragentValue"] = MakeSoapHeader("useragent", Useragent);
    }

    /// <summary>
    /// Creates a SoapHeader for use with AdWords API.
    /// </summary>
    /// <param name="headerName">Name of the header.</param>
    /// <param name="value">String value for the header.</param>
    /// <returns>The SoapHeader object.</returns>
    private static SoapHeader MakeSoapHeader(string headerName, string value) {
      string typeName = "Google.Api.Ads.AdWords.v13." + headerName;
      SoapHeader header = (SoapHeader) Assembly.GetExecutingAssembly().
          CreateInstance(typeName);
      if (header == null) {
        return null;
      }

      PropertyInfo propInfo = header.GetType().GetProperty("Value");
      if (propInfo != null) {
        propInfo.SetValue(header, new string[] {value}, null);
      }
      return header;
    }
  }
}
