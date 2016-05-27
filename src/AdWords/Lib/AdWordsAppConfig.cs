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
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

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
    /// Key name for gmbLoginEmail.
    /// </summary>
    /// <remarks>This field is used only for testing purposes.</remarks>
    private const string GMB_LOGIN_EMAIL = "GoogleMyBusiness.LoginEmail";

    /// <summary>
    /// Key name for gmbOAuth2RefreshToken.
    /// </summary>
    /// <remarks>This field is used only for testing purposes.</remarks>
    private const string GMB_OAUTH2_REFRESH_TOKEN = "GoogleMyBusiness.OAuth2RefreshToken";

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
    /// Key name for skipReportingHeader.
    /// </summary>
    private const string SKIP_REPORT_HEADER = "SkipReportHeader";

    /// <summary>
    /// Key name for skipReportingSummary.
    /// </summary>
    private const string SKIP_REPORT_SUMMARY = "SkipReportSummary";

    /// <summary>
    /// Key name for skipColumnHeader.
    /// </summary>
    private const string SKIP_COLUMN_HEADER = "SkipColumnHeader";

    /// <summary>
    /// Key name for includeZeroImpressions.
    /// </summary>
    private const string INCLUDE_ZERO_IMPRESSIONS = "IncludeZeroImpressions";

    /// <summary>
    /// Key name for useRawEnumValues.
    /// </summary>
    private const string USE_RAW_ENUM_VALUES = "UseRawEnumValues";

    /// <summary>
    /// Default value for AdWords API URL.
    /// </summary>
    private const string DEFAULT_ADWORDSAPI_SERVER = "https://adwords.google.com";

    /// <summary>
    /// Default OAuth2 scope for AdWords API.
    /// </summary>
    private const string DEFAULT_OAUTH_SCOPE = "https://www.googleapis.com/auth/adwords";

    /// <summary>
    /// Default value for skipping reports header.
    /// </summary>
    private const bool DEFAULT_SKIP_REPORT_HEADER = false;

    /// <summary>
    /// Default value for skipping reports summary.
    /// </summary>
    private const bool DEFAULT_SKIP_REPORT_SUMMARY = false;

    /// <summary>
    /// Default value for skipping column header.
    /// </summary>
    private const bool DEFAULT_SKIP_COLUMN_HEADER = false;

    /// <summary>
    /// Default value for including zero impression rows.
    /// </summary>
    private readonly bool? DEFAULT_INCLUDE_ZERO_IMPRESSIONS = null;

    /// <summary>
    /// Default value for returning raw values for enums.
    /// </summary>
    private readonly bool? DEFAULT_USE_RAW_ENUM_VALUES = null;

    /// <summary>
    /// ClientCustomerId to be used in SOAP headers.
    /// </summary>
    private string clientCustomerId;

    /// <summary>
    /// DeveloperToken to be used in the SOAP header.
    /// </summary>
    private string developerToken;

    /// <summary>
    /// Login email to be used with Google My Business account.
    /// </summary>
    /// <remarks>This field is used only for testing purposes.</remarks>
    private string gmbLoginEmail;

    /// <summary>
    /// OAuth2 refresh token to be used for Google My Business account.
    /// </summary>
    /// <remarks>This field is used only for testing purposes.</remarks>
    private string gmbOAuth2RefreshToken;

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
    /// Flag to decide whether or not to skip report header.
    /// </summary>
    private bool skipReportHeader;

    /// <summary>
    /// Flag to decide whether or not to skip report summary.
    /// </summary>
    private bool skipReportSummary;

    /// <summary>
    /// Flag to decide whether or not to skip column header.
    /// </summary>
    private bool skipColumnHeader;

    /// <summary>
    /// Flag to decide whether or not to include zero impression rows.
    /// </summary>
    /// <remarks>This setting is a three-valued logic. If this field is set to
    /// true or false, the client library sends the value to the server, and the
    /// server responds by including or excluding the zero impression rows. If
    /// this value is set to null (either explicitly in the code, or by
    /// commenting out the key in App.config / Web.config), then this value is
    /// not sent to the server, and the server behaves as explained on
    /// https://developers.google.com/adwords/api/docs/guides/zero-impression-reports#default_behavior.
    /// </remarks>
    private bool? includeZeroImpressions;

    /// <summary>
    /// Flag to decide whether enum values should be returned as actual enum values
    /// or display values.
    /// </summary>
    /// <remarks>This setting is a three-valued logic. If this field is set to
    /// true or false, the client library sends the value to the server, and the
    /// server responds by returning the actual enum values or the display values.
    /// If this value is set to null (either explicitly in the code, or by
    /// commenting out the key in App.config / Web.config), then this value is
    /// not sent to the server, and the server behaves as explained on
    /// https://developers.google.com/adwords/api/docs/guides/reporting#request-headers.
    /// </remarks>
    private bool? useRawEnumValues;

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
    /// Gets or sets the Google My Business (GMB) OAuth2 login email.
    /// </summary>
    /// <remarks>This property is used only for testing purposes.</remarks>
    public string GMBLoginEmail {
      get {
        return gmbLoginEmail;
      }
      set {
        SetPropertyField("GMBLoginEmail", ref gmbLoginEmail, value);
      }
    }

    /// <summary>
    /// Gets or sets the Google My Business (GMB) OAuth2 refresh token.
    /// </summary>
    /// <remarks>This property is used only for testing purposes.</remarks>
    public string GMBOAuth2RefreshToken {
      get {
        return gmbOAuth2RefreshToken;
      }
      set {
        SetPropertyField("GMBOAuth2RefreshToken", ref gmbOAuth2RefreshToken, value);
      }
    }

    /// <summary>
    /// Gets or sets whether reporting headers should be skipped.
    /// </summary>
    public bool SkipReportHeader {
      get {
        return skipReportHeader;
      }
      set {
        SetPropertyField("SkipReportHeader", ref skipReportHeader, value);
      }
    }

    /// <summary>
    /// Gets or sets whether report summary should be skipped.
    /// </summary>
    public bool SkipReportSummary {
      get {
        return skipReportSummary;
      }
      set {
        SetPropertyField("SkipReportSummary", ref skipReportSummary, value);
      }
    }

    /// <summary>
    /// Gets or sets whether report column header should be skipped.
    /// </summary>
    public bool SkipColumnHeader {
      get {
        return skipColumnHeader;
      }
      set {
        SetPropertyField("SkipColumnHeader", ref skipColumnHeader, value);
      }
    }

    /// <summary>
    /// Gets or sets whether zero impression rows should be skipped.
    /// </summary>
    /// <remarks>This setting is a three-valued logic. If this field is set to
    /// true or false, the client library sends the value to the server, and the
    /// server responds by including or excluding the zero impression rows. If
    /// this value is set to null (either explicitly in the code, or by
    /// commenting out the key in App.config / Web.config), then this value is
    /// not sent to the server, and the server behaves as explained on
    /// https://developers.google.com/adwords/api/docs/guides/zero-impression-reports#default_behavior.
    /// </remarks>
    public bool? IncludeZeroImpressions {
      get {
        return includeZeroImpressions;
      }
      set {
        SetPropertyField("IncludeZeroImpressions", ref includeZeroImpressions, value);
      }
    }

    /// <summary>
    /// Gets or sets whether enum values should be returned as actual enum values
    /// or display values.
    /// </summary>
    /// <remarks>This setting is a three-valued logic. If this field is set to
    /// true or false, the client library sends the value to the server, and the
    /// server responds by returning the actual enum values or the display values.
    /// If this value is set to null (either explicitly in the code, or by
    /// commenting out the key in App.config / Web.config), then this value is
    /// not sent to the server, and the server behaves as explained on
    /// https://developers.google.com/adwords/api/docs/guides/reporting#request-headers.
    /// </remarks>
    public bool? UseRawEnumValues {
      get {
        return useRawEnumValues;
      }
      set {
        SetPropertyField("UseRawEnumValues", ref useRawEnumValues, value);
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
    public override string GetUserAgent() {
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
      gmbOAuth2RefreshToken = "";
      gmbLoginEmail = "";
      userAgent = "";
      adWordsApiServer = DEFAULT_ADWORDSAPI_SERVER;
      skipReportHeader = DEFAULT_SKIP_REPORT_HEADER;
      skipReportSummary = DEFAULT_SKIP_REPORT_SUMMARY;
      skipColumnHeader = DEFAULT_SKIP_COLUMN_HEADER;
      includeZeroImpressions = DEFAULT_INCLUDE_ZERO_IMPRESSIONS;
      useRawEnumValues = DEFAULT_USE_RAW_ENUM_VALUES;

      ReadSettings(
          ((Hashtable)ConfigurationManager.GetSection("AdWordsApi"))
          .Cast<DictionaryEntry>()
          .ToDictionary(
              setting => setting.Key.ToString(),
              setting => setting.Value.ToString()));
    }

    /// <summary>
    /// Read all settings from App.config.
    /// </summary>
    /// <param name="settings">The parsed App.config settings.</param>
    protected override void ReadSettings(Dictionary<string, string> settings) {
      base.ReadSettings(settings);

      clientCustomerId = ReadSetting(settings, CLIENT_CUSTOMER_ID, clientCustomerId);
      developerToken = ReadSetting(settings, DEVELOPER_TOKEN, developerToken);

      // Configuration keys where AdWords API integrates with other products
      // that have their own settings.
      long.TryParse(ReadSetting(settings, MERCHANT_CENTER_ID, merchantCenterId.ToString()),
          out merchantCenterId);
      gmbLoginEmail = ReadSetting(settings, GMB_LOGIN_EMAIL, gmbLoginEmail);
      gmbOAuth2RefreshToken = ReadSetting(settings, GMB_OAUTH2_REFRESH_TOKEN,
          gmbOAuth2RefreshToken);

      userAgent = ReadSetting(settings, USER_AGENT, userAgent);
      adWordsApiServer = ReadSetting(settings, ADWORDSAPI_SERVER, adWordsApiServer);

      // If there is an OAuth2 scope mentioned in App.config, this will be
      // loaded by the above call. If there isn't one, we will initialize it
      // with a library-specific default value.
      if (string.IsNullOrEmpty(this.OAuth2Scope)) {
        this.OAuth2Scope = GetDefaultOAuth2Scope();
      }

      // Configure report downloading settings.
      bool.TryParse(ReadSetting(settings, SKIP_REPORT_HEADER, skipReportHeader.ToString()),
          out skipReportHeader);
      bool.TryParse(ReadSetting(settings, SKIP_REPORT_SUMMARY, skipReportSummary.ToString()),
          out skipReportSummary);
      bool.TryParse(ReadSetting(settings, SKIP_COLUMN_HEADER, skipColumnHeader.ToString()),
          out skipColumnHeader);

      bool tempFlag = false;

      if (!bool.TryParse(ReadSetting(settings, INCLUDE_ZERO_IMPRESSIONS, ""),
          out tempFlag)) {
        includeZeroImpressions = DEFAULT_INCLUDE_ZERO_IMPRESSIONS;
      } else {
        includeZeroImpressions = tempFlag;
      }

      if (!bool.TryParse(ReadSetting(settings, USE_RAW_ENUM_VALUES, ""),
          out tempFlag)) {
        useRawEnumValues = DEFAULT_USE_RAW_ENUM_VALUES;
      } else {
        useRawEnumValues = tempFlag;
      }
    }
  }
}