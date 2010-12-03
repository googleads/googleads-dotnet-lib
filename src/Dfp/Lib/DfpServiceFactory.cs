// Copyright 2010, Google Inc. All Rights Reserved.
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

namespace Google.Api.Ads.Dfp.Lib {
  /// <summary>
  /// The factory class for all DFP API services.
  /// </summary>
  public class DfpServiceFactory : ServiceFactory {
    /// <summary>
    /// The request header to be used with DFP API services.
    /// </summary>
    private RequestHeader requestHeader;

    /// <summary>
    /// The config class to be used with this object.
    /// </summary>
    private DfpAppConfig config = new DfpAppConfig();

    /// <summary>
    /// Gets an application name that can be used with the library.
    /// </summary>
    protected string ApplicationName {
      get {
        return String.Join("", new string[] {config.Signature, "|", config.ApplicationName});
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
    public DfpServiceFactory() {
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
        serverUrl = new Uri(config.DfpApiServer);
      }

      if (user == null) {
        throw new ArgumentNullException("user");
      }

      if (signature == null) {
        throw new ArgumentNullException("signature");
      }
      if (!(signature is DfpServiceSignature)) {
        throw new InvalidCastException(string.Format(CultureInfo.InvariantCulture,
            DfpErrorMessages.SignatureIsOfWrongType, typeof(DfpServiceSignature)));
      }

      DfpServiceSignature dfpapiSignature = signature as DfpServiceSignature;

      AdsSoapClient service = (AdsSoapClient) Activator.CreateInstance(dfpapiSignature.ServiceType);
      PropertyInfo propInfo = dfpapiSignature.ServiceType.GetProperty("RequestHeader");
      if (propInfo != null) {
        propInfo.SetValue(service, requestHeader.Clone(), null);

        if (config.Proxy != null) {
          service.Proxy = config.Proxy;
        }

        service.Url = string.Format("{0}apis/ads/publisher/{1}/{2}",
            serverUrl, dfpapiSignature.Version, dfpapiSignature.ServiceName);

        service.User = user;
      }
      return service;
    }

    /// <summary>
    /// Reads the headers from App.config.
    /// </summary>
    /// <param name="config">The configuration class.</param>
    /// <returns>A dictionary, with key-value pairs as headername, headervalue.</returns>
    public override Dictionary<string, string> ReadHeadersFromConfig(AppConfigBase config) {
      DfpAppConfig dfpConfig = (DfpAppConfig) config;
      Dictionary<string, string> configHeaders = new Dictionary<string, string>();
      if (!string.IsNullOrEmpty(dfpConfig.AuthToken)) {
        configHeaders["authToken"] = dfpConfig.AuthToken;
      }

      configHeaders["email"] = dfpConfig.Email;
      configHeaders["password"] = dfpConfig.Password;

      configHeaders["networkCode"] = dfpConfig.NetworkCode;
      configHeaders["applicationName"] = ApplicationName;

      return configHeaders;
    }

    /// <summary>
    /// Make a request header given a set of key-value pairs.
    /// </summary>
    /// <param name="headers">A dictionary holding key-value pairs.</param>
    /// <returns>A RequestHeader object, to be used with DFP API services.
    /// </returns>
    private RequestHeader MakeRequestHeaders(Dictionary<string, string> headers) {
      RequestHeader requestHeader = new RequestHeader();

      Type type = typeof(RequestHeader);
      if (!headers.ContainsKey("authToken")) {
        requestHeader.authToken = new AuthToken(config, "gam", headers["email"],
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
  }
}
