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

namespace Google.Api.Ads.Dfp.Lib {
  /// <summary>
  /// This class reads the configuration keys from App.config.
  /// </summary>
  public class DfpAppConfig : AppConfigBase {
    /// <summary>
    /// Key name for enableGzipCompression.
    /// </summary>
    private const string ENABLE_GZIP_COMPRESSION = "EnableGzipCompression";

    /// <summary>
    /// Key name for authToken.
    /// </summary>
    private const string AUTHTOKEN = "AuthToken";

    /// <summary>
    /// Key name for email.
    /// </summary>
    private const string EMAIL = "Email";

    /// <summary>
    /// Key name for password.
    /// </summary>
    private const string PASSWORD = "Password";

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
    /// Default value for DFPAPI_SERVER.
    /// </summary>
    private const string DEFAULT_DFPAPI_SERVER = "https://www.google.com";

    /// <summary>
    /// Authtoken to be used in making API calls.
    /// </summary>
    private string authToken;

    /// <summary>
    /// Email to be used in getting AuthToken.
    /// </summary>
    private string email;

    /// <summary>
    /// Password to be used in getting AuthToken.
    /// </summary>
    private string password;

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
    /// Gets or sets the email to be used in getting AuthToken.
    /// </summary>
    public string Email {
      get {
        return email;
      }
      set {
        email = value;
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
    /// Gets or sets networkCode to be used in SOAP headers.
    /// </summary>
    public string NetworkCode {
      get {
        return networkCode;
      }
      set {
        networkCode = value;
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
        applicationName = value;
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
        dfpApiServer = value;
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
    public DfpAppConfig() : base() {
      authToken = "";
      email = "";
      password = "";
      networkCode = "";
      applicationName = "";
      enableGzipCompression = true;
      shortNameField = "DfpApi-DotNet";

      dfpApiServer = DEFAULT_DFPAPI_SERVER;

      ReadSettings((Hashtable) ConfigurationManager.GetSection("DfpApi"));
    }

    /// <summary>
    /// Read all settings from App.config.
    /// </summary>
    /// <param name="settings">The parsed App.config settings.</param>
    protected override void ReadSettings(Hashtable settings) {
      base.ReadSettings(settings);

      authToken = ReadSetting(settings, AUTHTOKEN, authToken);
      email = ReadSetting(settings, EMAIL, email);
      password = ReadSetting(settings, PASSWORD, password);
      networkCode = ReadSetting(settings, NETWORK_CODE, networkCode);
      applicationName = ReadSetting(settings, APPLICATION_NAME, applicationName);
      enableGzipCompression = bool.Parse(ReadSetting(settings, ENABLE_GZIP_COMPRESSION,
          enableGzipCompression.ToString()));

      dfpApiServer = ReadSetting(settings, DFPAPI_SERVER, dfpApiServer);
    }
  }
}
