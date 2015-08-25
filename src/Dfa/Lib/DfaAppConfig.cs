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
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Xml;

namespace Google.Api.Ads.Dfa.Lib {
  /// <summary>
  /// This class reads the configuration keys from App.config.
  /// </summary>
  public class DfaAppConfig : AppConfigBase {
    /// <summary>
    /// The short name to identify this assembly.
    /// </summary>
    private const string SHORT_NAME = "DfaApi-Dotnet";

    /// <summary>
    /// Key name for dfaAuthToken.
    /// </summary>
    private const string DFA_AUTHTOKEN = "DfaAuthToken";

    /// <summary>
    /// Key name for dfaUserName.
    /// </summary>
    private const string DFA_USERNAME = "DfaUserName";

    /// <summary>
    /// Key name for dfaPassword.
    /// </summary>
    private const string DFA_PASSWORD = "DfaPassword";

    /// <summary>
    /// Key name for applicationName.
    /// </summary>
    private const string APPLICATION_NAME = "ApplicationName";

    /// <summary>
    /// Key name for DFA API URL.
    /// </summary>
    private const string DFAAPI_SERVER = "DfaApi.Server";

    /// <summary>
    /// Default value for DFAAPI_SERVER.
    /// </summary>
    private const string DEFAULT_DFAAPI_SERVER = "https://advertisersapi.doubleclick.net";

    /// <summary>
    /// OAUth2 scope for DFA API.
    /// </summary>
    private const string DFA_OAUTH2_SCOPE = "https://www.googleapis.com/auth/dfatrafficking";

    /// <summary>
    /// Key name for authorizationMethod.
    /// </summary>
    private const string AUTHORIZATION_METHOD = "AuthorizationMethod";

    /// <summary>
    /// Default value for authorizationMethod.
    /// </summary>
    private const DfaAuthorizationMethod DEFAULT_AUTHORIZATION_METHOD =
        DfaAuthorizationMethod.LoginService;

    /// <summary>
    /// Login authentication token to be used in making API calls.
    /// </summary>
    private string dfaAuthToken;

    /// <summary>
    /// Email to be used in getting login token.
    /// </summary>
    private string dfaUserName;

    /// <summary>
    /// Password to be used in getting login tokens.
    /// </summary>
    private string dfaPassword;

    /// <summary>
    /// Application name.
    /// </summary>
    private string applicationName;

    /// <summary>
    /// URL for DFA API.
    /// </summary>
    private string dfaApiServer;

    /// <summary>
    /// Authorization method to be used when making API calls.
    /// </summary>
    private DfaAuthorizationMethod authorizationMethod;

    /// <summary>
    /// Gets or sets the auth token to be used in SOAP headers.
    /// </summary>
    public string DfaAuthToken {
      get {
        return dfaAuthToken;
      }
      set {
        SetPropertyField("DfaAuthToken", ref dfaAuthToken, value);
      }
    }

    /// <summary>
    /// Gets or sets the username to be used in getting AuthToken.
    /// </summary>
    public string DfaUserName {
      get {
        return dfaUserName;
      }
      set {
        SetPropertyField("DfaUserName", ref dfaUserName, value);
      }
    }

    /// <summary>
    /// Gets or sets the password to be used in getting AuthToken.
    /// </summary>
    public string DfaPassword {
      get {
        return dfaPassword;
      }
      set {
        SetPropertyField("DfaPassword", ref dfaPassword, value);
      }
    }

    /// <summary>
    /// Gets or sets the application name.
    /// </summary>
    public string ApplicationName {
      get {
        return applicationName;
      }
      set {
        SetPropertyField("ApplicationName", ref applicationName, value);
      }
    }

    /// <summary>
    /// Gets or sets URL for DFA API.
    /// </summary>
    public string DfaApiServer {
      get {
        return dfaApiServer;
      }
      set {
        SetPropertyField("DfaApiServer", ref dfaApiServer, value);
      }
    }

    /// <summary>
    /// Gets or sets the authorization method to be used when making API calls.
    /// </summary>
    public DfaAuthorizationMethod AuthorizationMethod {
      get {
        return authorizationMethod;
      }
      set {
        SetPropertyField("AuthorizationMethod", ref authorizationMethod, value);
      }
    }

    /// <summary>
    /// Gets a useragent string that can be used with the library.
    /// </summary>
    public string GetUserAgent() {
      return String.Format("{0} ({1}{2})", this.applicationName, this.Signature,
          this.EnableGzipCompression ? ", gzip" : "");
    }

    /// <summary>
    /// Public constructor.
    /// </summary>
    public DfaAppConfig() : base() {
      authorizationMethod = DEFAULT_AUTHORIZATION_METHOD;
      dfaAuthToken = "";
      dfaUserName = null;
      dfaPassword = null;
      applicationName = "";
      dfaApiServer = DEFAULT_DFAAPI_SERVER;

      ReadSettings((Hashtable) ConfigurationManager.GetSection("DfaApi"));
    }

    /// <summary>
    /// Read all settings from App.config.
    /// </summary>
    /// <param name="settings">The parsed App.config settings.</param>
    protected override void ReadSettings(Hashtable settings) {
      base.ReadSettings(settings);

      try {
        authorizationMethod = (DfaAuthorizationMethod) Enum.Parse(
            typeof(DfaAuthorizationMethod),
            ReadSetting(settings, AUTHORIZATION_METHOD, authorizationMethod.ToString()));
      } catch {
        authorizationMethod = DEFAULT_AUTHORIZATION_METHOD;
      }
      dfaAuthToken = ReadSetting(settings, DFA_AUTHTOKEN, dfaAuthToken);
      dfaUserName = ReadSetting(settings, DFA_USERNAME, dfaUserName);
      dfaPassword = ReadSetting(settings, DFA_PASSWORD, dfaPassword);
      applicationName = ReadSetting(settings, APPLICATION_NAME, applicationName);

      dfaApiServer = ReadSetting(settings, DFAAPI_SERVER, dfaApiServer);

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
      return DFA_OAUTH2_SCOPE;
    }
  }
}
