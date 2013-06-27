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
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Xml;

namespace Google.Api.Ads.AdWords.Lib {
  /// <summary>
  /// This class reads the configuration keys from App.config.
  /// </summary>
  public class AdWordsAppConfig : AppConfigBase {
    /// <summary>
    /// The short name to identify this assembly.
    /// </summary>
    private const string SHORT_NAME = "AwApi-DotNet";

    /// <summary>
    /// Key name for clientCustomerId.
    /// </summary>
    private const string CLIENT_CUSTOMER_ID = "ClientCustomerId";

    /// <summary>
    /// Key name for developerToken.
    /// </summary>
    private const string DEVELOPER_TOKEN = "DeveloperToken";

    /// <summary>
    /// Key name for userAgent.
    /// </summary>
    private const string USER_AGENT = "UserAgent";

    /// <summary>
    /// Key name for AdWords API URL.
    /// </summary>
    private const string ADWORDSAPI_SERVER = "AdWordsApi.Server";

    /// <summary>
    /// Key name for authorizationMethod.
    /// </summary>
    private const string AUTHORIZATION_METHOD = "AuthorizationMethod";

    /// <summary>
    /// Default value for AdWords API URL.
    /// </summary>
    private const string DEFAULT_ADWORDSAPI_SERVER = "https://adwords.google.com";

    /// <summary>
    /// Default value for authorizationMethod.
    /// </summary>
    private const AdWordsAuthorizationMethod DEFAULT_AUTHORIZATION_METHOD =
        AdWordsAuthorizationMethod.OAuth2;

    /// <summary>
    /// ClientCustomerId to be used in SOAP headers.
    /// </summary>
    private string clientCustomerId;

    /// <summary>
    /// DeveloperToken to be used in the SOAP header.
    /// </summary>
    private string developerToken;

    /// <summary>
    /// Useragent to be used in the SOAP header.
    /// </summary>
    private string userAgent;

    /// <summary>
    /// Url for AdWords API.
    /// </summary>
    private string adWordsApiServer;

    /// <summary>
    /// Authorization method to be used when making API calls.
    /// </summary>
    private AdWordsAuthorizationMethod authorizationMethod;

    /// <summary>
    /// Gets or sets the client customerId to be used in SOAP headers.
    /// </summary>
    public string ClientCustomerId {
      get {
        return clientCustomerId;
      }
      set {
        SetPropertyField("ClientCustomerId", ref clientCustomerId, value);
      }
    }

    /// <summary>
    /// Gets or sets the developer token to be used in SOAP headers.
    /// </summary>
    public string DeveloperToken {
      get {
        return developerToken;
      }
      set {
        SetPropertyField("DeveloperToken", ref developerToken, value);
      }
    }

    /// <summary>
    /// Gets or sets the useragent to be used in SOAP headers.
    /// </summary>
    public string UserAgent {
      get {
        return userAgent;
      }
      set {
        SetPropertyField("UserAgent", ref userAgent, value);
      }
    }

    /// <summary>
    /// Gets or sets the URL for AdWords API.
    /// </summary>
    public string AdWordsApiServer {
      get {
        return adWordsApiServer;
      }
      set {
        SetPropertyField("AdWordsApiServer", ref adWordsApiServer, value);
      }
    }

    /// <summary>
    /// Gets or sets the authorization method to be used when making API calls.
    /// </summary>
    public AdWordsAuthorizationMethod AuthorizationMethod {
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
      return String.Format("{0} ({1}{2})", this.UserAgent, this.Signature,
          this.EnableGzipCompression ? ", gzip" : "");
    }

    /// <summary>
    /// Gets the default OAuth2 scope.
    /// </summary>
    public override string GetDefaultOAuth2Scope() {
      return string.Format("{0}/api/adwords/", this.AdWordsApiServer);
    }

    /// <summary>
    /// Public constructor.
    /// </summary>
    public AdWordsAppConfig() : base() {
      clientCustomerId = "";
      developerToken = "";
      userAgent = "";
      adWordsApiServer = DEFAULT_ADWORDSAPI_SERVER;
      authorizationMethod = DEFAULT_AUTHORIZATION_METHOD;

      ReadSettings((Hashtable) ConfigurationManager.GetSection("AdWordsApi"));
    }

    /// <summary>
    /// Read all settings from App.config.
    /// </summary>
    /// <param name="settings">The parsed App.config settings.</param>
    protected override void ReadSettings(Hashtable settings) {
      base.ReadSettings(settings);

      clientCustomerId = ReadSetting(settings, CLIENT_CUSTOMER_ID, clientCustomerId);
      developerToken = ReadSetting(settings, DEVELOPER_TOKEN, developerToken);
      userAgent = ReadSetting(settings, USER_AGENT, userAgent);
      adWordsApiServer = ReadSetting(settings, ADWORDSAPI_SERVER, adWordsApiServer);
      try {
        authorizationMethod = (AdWordsAuthorizationMethod) Enum.Parse(
            typeof(AdWordsAuthorizationMethod),
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
  }
}
