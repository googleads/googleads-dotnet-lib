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
    /// Key name for merchantCenterId.
    /// </summary>
    private const string MERCHANT_CENTER_ID = "MerchantCenter.AccountId";

    /// <summary>
    /// Key name for placesLoginEmail.
    /// </summary>
    private const string PLACES_LOGIN_EMAIL = "GooglePlaces.LoginEmail";

    /// <summary>
    /// Key name for placesOAuth2RefreshToken.
    /// </summary>
    private const string PLACES_OAUTH2_REFRESH_TOKEN = "GooglePlaces.OAuth2RefreshToken";

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
    /// Default OAuth2 scope for AdWords API.
    /// </summary>
    private const string DEFAULT_OAUTH_SCOPE = "https://www.googleapis.com/auth/adwords";

    /// <summary>
    /// ClientCustomerId to be used in SOAP headers.
    /// </summary>
    private string clientCustomerId;

    /// <summary>
    /// DeveloperToken to be used in the SOAP header.
    /// </summary>
    private string developerToken;

    /// <summary>
    /// Login email to be used with Google Places account.
    /// </summary>
    private string placesLoginEmail;

    /// <summary>
    /// OAuth2 refresh token to be used for Google Places account.
    /// </summary>
    private string placesOAuth2RefreshToken;

    /// <summary>
    /// Merchant Center ID to be used for Shopping campaigns.
    /// </summary>
    private long merchantCenterId;

    /// <summary>
    /// Useragent to be used in the SOAP header.
    /// </summary>
    private string userAgent;

    /// <summary>
    /// Url for AdWords API.
    /// </summary>
    private string adWordsApiServer;

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
    /// Gets or sets the Merchant Center ID to be used with Shopping campaigns.
    /// </summary>
    public long MerchantCenterId {
      get {
        return merchantCenterId;
      }
      set {
        SetPropertyField("MerchantCenterId", ref merchantCenterId, value);
      }
    }

    /// <summary>
    /// Gets or sets the Google Places OAuth2 login email.
    /// </summary>
    public string PlacesLoginEmail {
      get {
        return placesLoginEmail;
      }
      set {
        SetPropertyField("PlacesLoginEmail", ref placesLoginEmail, value);
      }
    }

    /// <summary>
    /// Gets or sets the Google Places OAuth2 refresh token.
    /// </summary>
    public string PlacesOAuth2RefreshToken {
      get {
        return placesOAuth2RefreshToken;
      }
      set {
        SetPropertyField("PlacesOAuth2RefreshToken", ref placesOAuth2RefreshToken, value);
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
      return DEFAULT_OAUTH_SCOPE;
    }

    /// <summary>
    /// Public constructor.
    /// </summary>
    public AdWordsAppConfig() : base() {
      clientCustomerId = "";
      developerToken = "";
      merchantCenterId = -1;
      placesOAuth2RefreshToken = "";
      placesLoginEmail = "";
      userAgent = "";
      adWordsApiServer = DEFAULT_ADWORDSAPI_SERVER;

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

      // Configuration keys where AdWords API integrates with other products
      // that have their own settings.
      long.TryParse(ReadSetting(settings, MERCHANT_CENTER_ID, merchantCenterId.ToString()),
          out merchantCenterId);
      placesLoginEmail = ReadSetting(settings, PLACES_LOGIN_EMAIL, placesLoginEmail);
      placesOAuth2RefreshToken = ReadSetting(settings, PLACES_OAUTH2_REFRESH_TOKEN,
          placesOAuth2RefreshToken);

      userAgent = ReadSetting(settings, USER_AGENT, userAgent);
      adWordsApiServer = ReadSetting(settings, ADWORDSAPI_SERVER, adWordsApiServer);

      // If there is an OAuth2 scope mentioned in App.config, this will be
      // loaded by the above call. If there isn't one, we will initialize it
      // with a library-specific default value.
      if (string.IsNullOrEmpty(this.OAuth2Scope)) {
        this.OAuth2Scope = GetDefaultOAuth2Scope();
      }
    }
  }
}