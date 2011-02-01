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
    /// Key name for enableGzipCompression.
    /// </summary>
    private const string ENABLE_GZIP_COMPRESSION = "EnableGzipCompression";

    /// <summary>
    /// Key name for authToken.
    /// </summary>
    private const string AUTHTOKEN = "AuthToken";

    /// <summary>
    /// Key name for userName.
    /// </summary>
    private const string USERNAME = "UserName";

    /// <summary>
    /// Key name for password.
    /// </summary>
    private const string PASSWORD = "Password";

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
    /// Authtoken to be used in making API calls.
    /// </summary>
    private string authToken;

    /// <summary>
    /// Email to be used in getting authToken.
    /// </summary>
    private string userName;

    /// <summary>
    /// Password to be used in getting authToken.
    /// </summary>
    private string password;

    /// <summary>
    /// Application name.
    /// </summary>
    private string applicationName;

    /// <summary>
    /// URL for DFA API.
    /// </summary>
    private string dfaApiServer;

    /// <summary>
    /// True, if gzip compression should be turned on for SOAP requests and
    /// responses.
    /// </summary>
    private bool enableGzipCompression;

    /// <summary>
    /// Gets or sets the auth token to be used in SOAP headers.
    /// </summary>
    public string AuthToken {
      get {
        return authToken;
      }
      set {
        authToken = value;
      }
    }

    /// <summary>
    /// Gets or sets the username to be used in getting AuthToken.
    /// </summary>
    public string UserName {
      get {
        return userName;
      }
      set {
        userName = value;
      }
    }

    /// <summary>
    /// Gets or sets the password to be used in getting AuthToken.
    /// </summary>
    public string Password {
      get {
        return password;
      }
      set {
        password = value;
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
        applicationName = value;
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
        dfaApiServer = value;
      }
    }

    /// <summary>
    /// Gets or sets whether gzip compression should be turned on for SOAP
    /// requests and responses.
    /// </summary>
    public bool EnableGzipCompression {
      get {
        return enableGzipCompression;
      }
      set {
        enableGzipCompression = value;
      }
    }

    /// <summary>
    /// Public constructor.
    /// </summary>
    public DfaAppConfig() : base() {
      authToken = "";
      userName = "";
      password = "";
      applicationName = "";
      enableGzipCompression = true;
      shortNameField = "DfaApi-DotNet";

      dfaApiServer = DEFAULT_DFAAPI_SERVER;

      ReadSettings((Hashtable) ConfigurationManager.GetSection("DfaApi"));
    }

    /// <summary>
    /// Read all settings from App.config.
    /// </summary>
    /// <param name="settings">The parsed App.config settings.</param>
    protected override void ReadSettings(Hashtable settings) {
      base.ReadSettings(settings);

      authToken = ReadSetting(settings, AUTHTOKEN, authToken);
      userName = ReadSetting(settings, USERNAME, userName);
      password = ReadSetting(settings, PASSWORD, password);
      applicationName = ReadSetting(settings, APPLICATION_NAME, applicationName);
      enableGzipCompression = bool.Parse(ReadSetting(settings, ENABLE_GZIP_COMPRESSION,
          enableGzipCompression.ToString()));

      dfaApiServer = ReadSetting(settings, DFAAPI_SERVER, dfaApiServer);
    }
  }
}
