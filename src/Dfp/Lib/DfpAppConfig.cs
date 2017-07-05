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

using Google.Api.Ads.Common.Lib;
using System;
using System.Collections.Generic;

namespace Google.Api.Ads.Dfp.Lib {
  /// <summary>
  /// This class reads the configuration keys from App.config.
  /// </summary>
  public class DfpAppConfig : AppConfigBase {
    /// <summary>
    /// The short name to identify this assembly.
    /// </summary>
    private const string SHORT_NAME = "DfpApi-DotNet";

    /// <summary>
    /// Key name for networkCode.
    /// </summary>
    private const string NETWORK_CODE = "NetworkCode";

    /// <summary>
    /// Key name for applicationName.
    /// </summary>
    private const string APPLICATION_NAME = "ApplicationName";

    /// <summary>
    /// Key name for Dfp API URL.
    /// </summary>
    private const string DFPAPI_SERVER = "DfpApi.Server";

    /// <summary>
    /// Key name for authorizationMethod.
    /// </summary>
    private const string AUTHORIZATION_METHOD = "AuthorizationMethod";

    /// <summary>
    /// Default value for DFPAPI_SERVER.
    /// </summary>
    private const string DEFAULT_DFPAPI_SERVER = "https://ads.google.com";

    /// <summary>
    /// The OAuth2 scope for DFP API.
    /// </summary>
    private const string DFP_OAUTH2_SCOPE = "https://www.googleapis.com/auth/dfp";

    /// <summary>
    /// Default value for authorizationMethod.
    /// </summary>
    private const DfpAuthorizationMethod DEFAULT_AUTHORIZATION_METHOD =
        DfpAuthorizationMethod.OAuth2;

    /// <summary>
    /// The default value for application name.
    /// </summary>
    public const string DEFAULT_APPLICATION_NAME = "INSERT_YOUR_APPLICATION_NAME_HERE";

    /// <summary>
    /// NetworkCode to be used in SOAP headers.
    /// </summary>
    private string networkCode;

    /// <summary>
    /// Application name to be used in SOAP headers.
    /// </summary>
    private string applicationName;

    /// <summary>
    /// URL for DFP API.
    /// </summary>
    private string dfpApiServer;

    /// <summary>
    /// Authorization method to be used when making API calls.
    /// </summary>
    private DfpAuthorizationMethod authorizationMethod;

    /// <summary>
    /// Gets or sets networkCode to be used in SOAP headers.
    /// </summary>
    public string NetworkCode {
      get {
        return networkCode;
      }
      set {
        SetPropertyField(NETWORK_CODE, ref networkCode, value);
      }
    }

    /// <summary>
    /// Gets or sets application name to be used in SOAP headers.
    /// </summary>
    public string ApplicationName {
      get {
        return applicationName;
      }
      set {
        SetPropertyField(APPLICATION_NAME, ref applicationName, value);
      }
    }

    /// <summary>
    /// Gets or sets URL for DFP API.
    /// </summary>
    public string DfpApiServer {
      get {
        return dfpApiServer;
      }
      set {
        SetPropertyField(DFPAPI_SERVER, ref dfpApiServer, value);
      }
    }

    /// <summary>
    /// Gets or sets the authorization method to be used when making API calls.
    /// </summary>
    public DfpAuthorizationMethod AuthorizationMethod {
      get {
        return authorizationMethod;
      }
      set {
        SetPropertyField(AUTHORIZATION_METHOD, ref authorizationMethod, value);
      }
    }

    /// <summary>
    /// Gets a useragent string that can be used with the library.
    /// </summary>
    public override string GetUserAgent() {
      return String.Format("{0} ({1}{2})", this.applicationName, this.Signature,
          this.EnableGzipCompression ? ", gzip" : "");
    }

    /// <summary>
    /// Public constructor.
    /// </summary>
    public DfpAppConfig() : base() {
      networkCode = "";
      applicationName = DEFAULT_APPLICATION_NAME;
      dfpApiServer = DEFAULT_DFPAPI_SERVER;
      authorizationMethod = DEFAULT_AUTHORIZATION_METHOD;

      ReadSettings(LoadConfigSection("DfpApi"));
    }

    /// <summary>
    /// Read all settings from App.config.
    /// </summary>
    /// <param name="settings">The parsed App.config settings.</param>
    protected override void ReadSettings(Dictionary<string, string> settings) {
      base.ReadSettings(settings);

      networkCode = ReadSetting(settings, NETWORK_CODE, networkCode);
      applicationName = ReadSetting(settings, APPLICATION_NAME, applicationName);
      dfpApiServer = ReadSetting(settings, DFPAPI_SERVER, dfpApiServer);

      try {
        authorizationMethod = (DfpAuthorizationMethod) Enum.Parse(
            typeof(DfpAuthorizationMethod),
            ReadSetting(settings, AUTHORIZATION_METHOD, authorizationMethod.ToString()));
      } catch {
        authorizationMethod = DEFAULT_AUTHORIZATION_METHOD;
      }

      // If there is an OAuth2 scope mentioned in App.config, this will be
      // loaded by the above call. If there isn't one, we will initialize it
      // with a library-specific default value.
      if (string.IsNullOrEmpty(this.OAuth2Scope)) {
        this.OAuth2Scope = GetDefaultOAuth2Scope();
      }
    }

    /// <summary>
    /// Gets the default OAuth2 scope.
    /// </summary>
    public override string GetDefaultOAuth2Scope() {
      return DFP_OAUTH2_SCOPE;
    }
  }
}
