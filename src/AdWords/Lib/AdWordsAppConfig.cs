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

using Google.Api.Ads.Common.Config;
using Google.Api.Ads.Common.Lib;

using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Google.Api.Ads.AdWords.Lib
{
    /// <summary>
    /// This class reads the configuration keys from App.config.
    /// </summary>
    public class AdWordsAppConfig : AppConfigBase
    {
        /// <summary>
        /// The short name to identify this assembly.
        /// </summary>
        private const string SHORT_NAME = "AwApi-DotNet";

        /// <summary>
        /// Default value for AdWords API URL.
        /// </summary>
        private const string DEFAULT_ADWORDSAPI_SERVER = "https://adwords.google.com";

        /// <summary>
        /// Default OAuth2 scope for AdWords API.
        /// </summary>
        private const string DEFAULT_OAUTH_SCOPE = "https://www.googleapis.com/auth/adwords";

        /// <summary>
        /// Default value for user agent key.
        /// </summary>
        private const string DEFAULT_USER_AGENT = "unknown";

        /// <summary>
        /// ClientCustomerId to be used in SOAP headers.
        /// </summary>
        private ConfigSetting<string> clientCustomerId =
            new ConfigSetting<string>("ClientCustomerId", "");

        /// <summary>
        /// DeveloperToken to be used in the SOAP header.
        /// </summary>
        private ConfigSetting<string> developerToken =
            new ConfigSetting<string>("DeveloperToken", "");

        /// <summary>
        /// Login email to be used with Google My Business account.
        /// </summary>
        /// <remarks>This field is used only for testing purposes.</remarks>
        private ConfigSetting<string> gmbLoginEmail =
            new ConfigSetting<string>("GoogleMyBusiness.LoginEmail", "");

        /// <summary>
        /// OAuth2 refresh token to be used for Google My Business account.
        /// </summary>
        /// <remarks>This field is used only for testing purposes.</remarks>
        private ConfigSetting<string> gmbOAuth2RefreshToken =
            new ConfigSetting<string>("GoogleMyBusiness.OAuth2RefreshToken", "");

        /// <summary>
        /// Merchant Center ID to be used for Shopping campaigns.
        /// </summary>
        private ConfigSetting<long> merchantCenterId =
            new ConfigSetting<long>("MerchantCenter.AccountId", 0);

        /// <summary>
        /// Useragent to be used in the SOAP header.
        /// </summary>
        private ConfigSetting<string> userAgent =
            new ConfigSetting<string>("UserAgent", DEFAULT_USER_AGENT);

        /// <summary>
        /// Url for AdWords API.
        /// </summary>
        private ConfigSetting<string> adWordsApiServer =
            new ConfigSetting<string>("AdWordsApi.Server", DEFAULT_ADWORDSAPI_SERVER);

        /// <summary>
        /// Flag to decide whether or not to skip report header.
        /// </summary>
        private ConfigSetting<bool> skipReportHeader =
            new ConfigSetting<bool>("SkipReportHeader", false);

        /// <summary>
        /// Flag to decide whether or not to skip report summary.
        /// </summary>
        private ConfigSetting<bool> skipReportSummary =
            new ConfigSetting<bool>("SkipReportSummary", false);

        /// <summary>
        /// Flag to decide whether or not to skip column header.
        /// </summary>
        private ConfigSetting<bool> skipColumnHeader =
            new ConfigSetting<bool>("SkipColumnHeader", false);

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
        private ConfigSetting<bool?> includeZeroImpressions =
            new ConfigSetting<bool?>("IncludeZeroImpressions", null);

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
        private ConfigSetting<bool?> useRawEnumValues =
            new ConfigSetting<bool?>("UseRawEnumValues", null);

        /// <summary>
        /// Gets or sets the client customerId to be used in SOAP headers.
        /// </summary>
        public string ClientCustomerId
        {
            get => clientCustomerId.Value;
            set => SetPropertyAndNotify(clientCustomerId, value);
        }

        /// <summary>
        /// Gets or sets the developer token to be used in SOAP headers.
        /// </summary>
        public string DeveloperToken
        {
            get => developerToken.Value;
            set => SetPropertyAndNotify(developerToken, value);
        }

        /// <summary>
        /// Gets or sets the Merchant Center ID to be used with Shopping campaigns.
        /// </summary>
        public long MerchantCenterId
        {
            get => merchantCenterId.Value;
            set => SetPropertyAndNotify(merchantCenterId, value);
        }

        /// <summary>
        /// Gets or sets the Google My Business (GMB) OAuth2 login email.
        /// </summary>
        /// <remarks>This property is used only for testing purposes.</remarks>
        public string GMBLoginEmail
        {
            get => gmbLoginEmail.Value;
            set => SetPropertyAndNotify(gmbLoginEmail, value);
        }

        /// <summary>
        /// Gets or sets the Google My Business (GMB) OAuth2 refresh token.
        /// </summary>
        /// <remarks>This property is used only for testing purposes.</remarks>
        public string GMBOAuth2RefreshToken
        {
            get => gmbOAuth2RefreshToken.Value;
            set => SetPropertyAndNotify(gmbOAuth2RefreshToken, value);
        }

        /// <summary>
        /// Gets or sets whether reporting headers should be skipped.
        /// </summary>
        public bool SkipReportHeader
        {
            get => skipReportHeader.Value;
            set => SetPropertyAndNotify(skipReportHeader, value);
        }

        /// <summary>
        /// Gets or sets whether report summary should be skipped.
        /// </summary>
        public bool SkipReportSummary
        {
            get => skipReportSummary.Value;
            set => SetPropertyAndNotify(skipReportSummary, value);
        }

        /// <summary>
        /// Gets or sets whether report column header should be skipped.
        /// </summary>
        public bool SkipColumnHeader
        {
            get => skipColumnHeader.Value;
            set => SetPropertyAndNotify(skipColumnHeader, value);
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
        public bool? IncludeZeroImpressions
        {
            get => includeZeroImpressions.Value;
            set => SetPropertyAndNotify(includeZeroImpressions, value);
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
        public bool? UseRawEnumValues
        {
            get => useRawEnumValues.Value;
            set => SetPropertyAndNotify(useRawEnumValues, value);
        }

        /// <summary>
        /// Gets or sets the useragent to be used in SOAP headers.
        /// </summary>
        public string UserAgent
        {
            get => userAgent.Value;
            set
            {
                if (value.Any(c => (int) c < 32 || (int) c > 127))
                {
                    throw new ArgumentException(AdWordsErrorMessages.UserAgentShouldbeAscii);
                }

                SetPropertyAndNotify(userAgent, value);
            }
        }

        /// <summary>
        /// Gets or sets the URL for AdWords API.
        /// </summary>
        public string AdWordsApiServer
        {
            get => adWordsApiServer.Value;
            set => SetPropertyAndNotify(adWordsApiServer, value);
        }

        /// <summary>
        /// Gets a useragent string that can be used with the library.
        /// </summary>
        public override string GetUserAgent()
        {
            return String.Format("{0} ({1}{2})", UserAgent, Signature,
                EnableGzipCompression ? ", gzip" : "");
        }

        /// <summary>
        /// Gets the default OAuth2 scope.
        /// </summary>
        public override string GetDefaultOAuth2Scope()
        {
            return DEFAULT_OAUTH_SCOPE;
        }

        /// <summary>
        /// Public constructor. Loads the configuration from the <code>AdWordsApi</code> section
        /// of the App.config / Web.config.
        /// </summary>
        public AdWordsAppConfig() : base()
        {
            LoadFromAppConfigSection("AdWordsApi");
        }

        /// <summary>
        /// Public constructor. Loads the configuration from an <see cref="IConfigurationRoot"/>.
        /// </summary>
        /// <param name="configurationRoot">The configuration root.</param>
        public AdWordsAppConfig(IConfigurationRoot configurationRoot) : base(configurationRoot)
        {
        }

        /// <summary>
        /// Public constructor. Loads the configuration from a <see cref="IConfigurationSection"/>.
        /// </summary>
        /// <param name="configurationSection">The configuration section.</param>
        public AdWordsAppConfig(IConfigurationSection configurationSection)
            : base(configurationSection)
        {
        }

        /// <summary>
        /// Read all settings from App.config.
        /// </summary>
        /// <param name="settings">The parsed App.config settings.</param>
        protected override void ReadSettings(Dictionary<string, string> settings)
        {
            base.ReadSettings(settings);

            ReadSetting(settings, clientCustomerId);
            ReadSetting(settings, developerToken);

            // Configuration keys where AdWords API integrates with other products
            // that have their own settings.
            ReadSetting(settings, merchantCenterId);
            ReadSetting(settings, gmbLoginEmail);
            ReadSetting(settings, gmbOAuth2RefreshToken);

            ReadSetting(settings, userAgent);
            ReadSetting(settings, adWordsApiServer);

            // If there is an OAuth2 scope mentioned in App.config, this will be
            // loaded by the base.ReadSettings() call above. If there isn't one, we will initialize
            // it with a library-specific default value.
            if (string.IsNullOrEmpty(this.OAuth2Scope))
            {
                this.OAuth2Scope = GetDefaultOAuth2Scope();
            }

            // Configure report downloading settings.
            ReadSetting(settings, skipReportHeader);
            ReadSetting(settings, skipReportSummary);
            ReadSetting(settings, skipColumnHeader);
            ReadSetting(settings, includeZeroImpressions);
            ReadSetting(settings, useRawEnumValues);
        }
    }
}
