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
using Google.Api.Ads.Common.Logging;
using Google.Apis.Auth.OAuth2;

using Microsoft.Extensions.Configuration;

using Newtonsoft.Json;

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;

#if NET452
using System.Web.Configuration;
using System.Web.Hosting;
# endif

namespace Google.Api.Ads.Common.Lib
{
    /// <summary>
    /// This class reads the configuration keys from App.config.
    /// </summary>
    public class AppConfigBase : AppConfig, INotifyPropertyChanged
    {
        /// <summary>
        /// The registry for saving feature usage information..
        /// </summary>
        private static readonly AdsFeatureUsageRegistry featureUsageRegistry =
            AdsFeatureUsageRegistry.Instance;

        /// <summary>
        /// The short name to identify this assembly.
        /// </summary>
        private const string SHORT_NAME = "Common-Dotnet";

        /// <summary>
        /// Web proxy to be used with the services.
        /// </summary>
        private ConfigSetting<IWebProxy> proxy = new ConfigSetting<IWebProxy>("Proxy", null);

        /// <summary>
        /// True, if the credentials in the log file should be masked.
        /// </summary>
        private ConfigSetting<bool> maskCredentials =
            new ConfigSetting<bool>("MaskCredentials", true);

        /// <summary>
        /// Timeout to be used for Ads services in milliseconds.
        /// </summary>
        private ConfigSetting<int> timeout = new ConfigSetting<int>("Timeout", DEFAULT_TIMEOUT);

        /// <summary>
        /// Number of times to retry a call if an API call fails and can be retried.
        /// </summary>
        private ConfigSetting<int> retryCount = new ConfigSetting<int>("RetryCount", 0);

        /// <summary>
        /// True, if gzip compression should be turned on for SOAP requests and
        /// responses.
        /// </summary>
        private ConfigSetting<bool> enableGzipCompression =
            new ConfigSetting<bool>("EnableGzipCompression", true);

        /// <summary>
        /// OAuth2 client ID.
        /// </summary>
        private ConfigSetting<string> oAuth2ClientId =
            new ConfigSetting<string>("OAuth2ClientId", "");

        /// <summary>
        /// OAuth2 server URL.
        /// </summary>
        private ConfigSetting<string> oAuth2ServerUrl =
            new ConfigSetting<string>("OAuth2ServerUrl", DEFAULT_OAUTH2_SERVER);

        /// <summary>
        /// OAuth2 client secret.
        /// </summary>
        private ConfigSetting<string> oAuth2ClientSecret =
            new ConfigSetting<string>("OAuth2ClientSecret", "");

        /// <summary>
        /// OAuth2 access token.
        /// </summary>
        private ConfigSetting<string> oAuth2AccessToken =
            new ConfigSetting<string>("OAuth2AccessToken", "");

        /// <summary>
        /// OAuth2 refresh token.
        /// </summary>
        private ConfigSetting<string> oAuth2RefreshToken =
            new ConfigSetting<string>("OAuth2RefreshToken", "");

        /// <summary>
        /// OAuth2 prn email.
        /// </summary>
        private ConfigSetting<string> oAuth2PrnEmail =
            new ConfigSetting<string>("OAuth2PrnEmail", "");

        /// <summary>
        /// OAuth2 service account email loaded from secrets JSON file.
        /// </summary>
        private ConfigSetting<string> oAuth2ServiceAccountEmail =
            new ConfigSetting<string>("client_email", null);

        /// <summary>
        /// OAuth2 private key loaded from secrets JSON file.
        /// </summary>
        private ConfigSetting<string> oAuth2PrivateKey =
            new ConfigSetting<string>("private_key", "");

        /// <summary>
        /// OAuth2 secrets JSON file path.
        /// </summary>
        private ConfigSetting<string> oAuth2SecretsJsonPath =
            new ConfigSetting<string>("OAuth2SecretsJsonPath", "");

        /// <summary>
        /// OAuth2 scope.
        /// </summary>
        private ConfigSetting<string> oAuth2Scope = new ConfigSetting<string>("OAuth2Scope", "");

        /// <summary>
        /// Redirect uri.
        /// </summary>
        private ConfigSetting<string> oAuth2RedirectUri =
            new ConfigSetting<string>("OAuth2RedirectUri",
                GoogleAuthConsts.InstalledAppRedirectUri);

        /// <summary>
        /// OAuth2 mode.
        /// </summary>
        private ConfigSetting<OAuth2Flow> oAuth2Mode =
            new ConfigSetting<OAuth2Flow>("OAuth2Mode", OAuth2Flow.APPLICATION);

        /// <summary>
        /// True, if the usage of a feature should be added to the user agent,
        /// false otherwise.
        /// </summary>
        private ConfigSetting<bool> includeUtilitiesInUserAgent =
            new ConfigSetting<bool>("IncludeUtilitiesInUserAgent", false);

        /// <summary>
        /// Default value for timeout for Ads services.
        /// </summary>
        private const int DEFAULT_TIMEOUT = 1000 * 60 * 10;

        /// <summary>
        /// The default value of OAuth2 server URL.
        /// </summary>
        private const string DEFAULT_OAUTH2_SERVER = "https://accounts.google.com";

        /// <summary>
        /// Gets or sets whether the credentials in the log file should be masked.
        /// </summary>
        public bool MaskCredentials
        {
            get => maskCredentials.Value;
            set => SetPropertyAndNotify(maskCredentials, value);
        }

        /// <summary>
        /// Gets or sets the web proxy to be used with the services.
        /// </summary>
        public IWebProxy Proxy
        {
            get => proxy.Value;
            set => SetPropertyAndNotify(proxy, value);
        }

        /// <summary>
        /// Gets or sets the timeout for Ads services in milliseconds.
        /// </summary>
        public int Timeout
        {
            get => timeout.Value;
            set => SetPropertyAndNotify(timeout, value);
        }

        /// <summary>
        /// Gets or sets the number of times to retry a call if an API call fails
        /// and can be retried.
        /// </summary>
        public int RetryCount
        {
            get => retryCount.Value;
            set => SetPropertyAndNotify(retryCount, value);
        }

        /// <summary>
        /// Gets or sets whether gzip compression should be turned on for SOAP
        /// requests and responses.
        /// </summary>
        public bool EnableGzipCompression
        {
            get => enableGzipCompression.Value;
            set => SetPropertyAndNotify(enableGzipCompression, value);
        }

        /// <summary>
        /// Gets or sets the OAuth2 server URL.
        /// </summary>
        /// <remarks>This property's setter is primarily used for testing purposes.
        /// </remarks>
        public string OAuth2ServerUrl
        {
            get => oAuth2ServerUrl.Value;
            set => SetPropertyAndNotify(oAuth2ServerUrl, value);
        }

        /// <summary>
        /// Gets or sets the OAuth2 client ID.
        /// </summary>
        public string OAuth2ClientId
        {
            get => oAuth2ClientId.Value;
            set => SetPropertyAndNotify(oAuth2ClientId, value);
        }

        /// <summary>
        /// Gets or sets the OAuth2 client secret.
        /// </summary>
        public string OAuth2ClientSecret
        {
            get => oAuth2ClientSecret.Value;
            set => SetPropertyAndNotify(oAuth2ClientSecret, value);
        }

        /// <summary>
        /// Gets or sets the OAuth2 access token.
        /// </summary>
        public string OAuth2AccessToken
        {
            get => oAuth2AccessToken.Value;
            set => SetPropertyAndNotify(oAuth2AccessToken, value);
        }

        /// <summary>
        /// Gets or sets the OAuth2 refresh token.
        /// </summary>
        /// <remarks>This setting is applicable only when using OAuth2 web / application
        /// flow in offline mode.</remarks>
        public string OAuth2RefreshToken
        {
            get => oAuth2RefreshToken.Value;
            set => SetPropertyAndNotify(oAuth2RefreshToken, value);
        }

        /// <summary>
        /// Gets or sets the OAuth2 scope.
        /// </summary>
        public string OAuth2Scope
        {
            get => oAuth2Scope.Value;
            set => SetPropertyAndNotify(oAuth2Scope, value);
        }

        /// <summary>
        /// Gets or sets the OAuth2 redirect URI.
        /// </summary>
        /// <remarks>This setting is applicable only when using OAuth2 web flow.
        /// </remarks>
        public string OAuth2RedirectUri
        {
            get => oAuth2RedirectUri.Value;
            set => SetPropertyAndNotify(oAuth2RedirectUri, value);
        }

        /// <summary>
        /// Gets or sets the OAuth2 mode.
        /// </summary>
        public OAuth2Flow OAuth2Mode
        {
            get => oAuth2Mode.Value;
            set => SetPropertyAndNotify(oAuth2Mode, value);
        }

        /// <summary>
        /// Gets or sets the OAuth2 prn email.
        /// </summary>
        /// <remarks>This setting is applicable only when using OAuth2 service accounts.
        /// </remarks>
        public string OAuth2PrnEmail
        {
            get => oAuth2PrnEmail.Value;
            set => SetPropertyAndNotify(oAuth2PrnEmail, value);
        }

        /// <summary>
        /// Gets the OAuth2 service account email.
        /// </summary>
        /// <remarks>
        /// This setting is applicable only when using OAuth2 service accounts.
        /// This setting is read directly from the file referred to in
        /// <see cref="OAuth2SecretsJsonPath"/> setting.
        /// </remarks>
        public string OAuth2ServiceAccountEmail
        {
            get => oAuth2ServiceAccountEmail.Value;
            private set => SetPropertyAndNotify(oAuth2ServiceAccountEmail, value);
        }

        /// <summary>
        /// Gets the OAuth2 private key for service account flow.
        /// </summary>
        /// <remarks>
        /// This setting is applicable only when using OAuth2 service accounts.
        /// This setting is read directly from the file referred to in
        /// <see cref="OAuth2SecretsJsonPath"/> setting.
        /// </remarks>
        public string OAuth2PrivateKey
        {
            get => oAuth2PrivateKey.Value;
            private set => SetPropertyAndNotify(oAuth2PrivateKey, value);
        }

        /// <summary>
        /// Gets or sets the OAuth2 secrets JSON file path.
        /// </summary>
        /// <remarks>
        /// This setting is applicable only when using OAuth2 service accounts.
        /// </remarks>
        public string OAuth2SecretsJsonPath
        {
            get => oAuth2SecretsJsonPath.Value;
            set
            {
                SetPropertyAndNotify(oAuth2SecretsJsonPath, value);
                LoadOAuth2SecretsFromFile();
            }
        }

        /// <summary>
        /// Gets or sets whether usage of various client library features should be
        /// tracked.
        /// </summary>
        /// <remarks>The name of the property is kept different to match the setting
        /// name for other client libraries.</remarks>
        public bool IncludeUtilitiesInUserAgent
        {
            get => includeUtilitiesInUserAgent.Value;
            set => SetPropertyAndNotify(includeUtilitiesInUserAgent, value);
        }

        /// <summary>
        /// Gets the default OAuth2 scope.
        /// </summary>
        public virtual string GetDefaultOAuth2Scope()
        {
            return "";
        }

        /// <summary>
        /// Gets the user agent text.
        /// </summary>
        /// <returns>The user agent.</returns>
        public virtual string GetUserAgent()
        {
            return "";
        }

        /// <summary>
        /// Gets the signature for this assembly, given a type derived from
        /// AppConfigBase.
        /// </summary>
        /// <param name="type">Type of the class derived from AppConfigBase.</param>
        /// <returns>The assembly signature.</returns>
        /// <exception cref="ArgumentException">Thrown if type is not derived from
        /// AppConfigBase.</exception>
        private string GetAssemblySignatureFromAppConfigType(Type type)
        {
            Type appConfigBaseType = typeof(AppConfigBase);
            if (!(type.BaseType == appConfigBaseType || type == appConfigBaseType))
            {
                throw new ArgumentException(string.Format("{0} is not derived from {1}.",
                    type.FullName, appConfigBaseType.FullName));
            }

            Version version = type.Assembly.GetName().Version;
            string shortName = (string) type
                .GetField("SHORT_NAME", BindingFlags.NonPublic | BindingFlags.Static)
                .GetValue(null);
            return string.Format("{0}/{1}.{2}.{3}", shortName, version.Major, version.Minor,
                version.Revision);
        }

        /// <summary>
        /// Gets the signature for this library.
        /// </summary>
        public string Signature
        {
            get
            {
                string utilsAgent = (IncludeUtilitiesInUserAgent) ? featureUsageRegistry.Text : "";
                return string.Format("{0}, {1}, .NET CLR/{2}, {3}",
                    GetAssemblySignatureFromAppConfigType(GetType()),
                    GetAssemblySignatureFromAppConfigType(GetType().BaseType), Environment.Version,
                    utilsAgent);
            }
        }

        /// <summary>
        /// Gets the number of seconds after Jan 1, 1970, 00:00:00
        /// </summary>
        public virtual long UnixTimestamp
        {
            get
            {
                TimeSpan unixTime = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
                return (long) unixTime.TotalSeconds;
            }
        }

        /// <summary>
        /// The default constructor.
        /// </summary>
        public AppConfigBase()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppConfigBase"/> class.
        /// </summary>
        /// <param name="configurationRoot">The configuration root.</param>
        public AppConfigBase(IConfigurationRoot configurationRoot) : base()
        {
            LoadFromConfiguration(configurationRoot, "");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppConfigBase"/> class.
        /// </summary>
        /// <param name="configurationSection">The configuration section.</param>
        public AppConfigBase(IConfigurationSection configurationSection) : base()
        {
            LoadFromConfiguration(configurationSection, configurationSection.Key);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppConfigBase"/> class.
        /// </summary>
        /// <param name="configuration">The configuration section.</param>
        /// <param name="sectionName">The section name.</param>
        protected void LoadFromConfiguration(IConfiguration configuration, string sectionName)
        {
            ReadSettings(ToDictionary(configuration, sectionName));
        }

        /// <summary>
        /// Attempts to load the configuration section with the given name.
        /// </summary>
        /// <param name="sectionName">The name of the configuration section to load.</param>
        /// <returns>
        /// The request configuration section, or <code>null</code> if none was found.
        /// </returns>
        protected void LoadFromAppConfigSection(string sectionName)
        {
            ReadSettings(ReadAppConfigSection(sectionName));
        }

        /// <summary>
        /// Reads the application configuration section.
        /// </summary>
        /// <param name="sectionName">Name of the section.</param>
        /// <returns>A dictionary with key as configuration keyname and value as configuration
        /// value.</returns>
        private static Dictionary<string, string> ReadAppConfigSection(string sectionName)
        {
            Hashtable config = null;

#if NET452
            if (HostingEnvironment.IsHosted)
            {
                config = (Hashtable) WebConfigurationManager.GetSection(sectionName);
            }
            else 
            {
                config = (Hashtable) ConfigurationManager.GetSection(sectionName);
            }
#else
            config = (Hashtable) ConfigurationManager.GetSection(sectionName);
#endif
            return config != null ?
                config.Cast<DictionaryEntry>().ToDictionary(
                    d => d.Key.ToString(), d => d.Value?.ToString()) :
                new Dictionary<string, string>();
            ;
        }

        /// <summary>
        /// Converts a configuration section into a dictionary. Section name prefix is stripped
        /// from the key names.
        /// </summary>
        /// <param name="configuration">The configuration section.</param>
        /// <param name="sectionName">Name of the section.</param>
        /// <returns>A dictionary with key as configuration keyname and value as configuration
        /// value.</returns>
        private static Dictionary<string, string> ToDictionary(IConfiguration configuration,
            string sectionName)
        {
            string sectionPrefix = sectionName + ":";
            return configuration.AsEnumerable().ToDictionary(
                setting => setting.Key.ToString().Replace(sectionPrefix, ""),
                setting => setting.Value?.ToString());
        }

        /// <summary>
        /// Read all settings from App.config.
        /// </summary>
        /// <param name="settings">The parsed app.config settings.</param>
        protected virtual void ReadSettings(Dictionary<string, string> settings)
        {
            ReadProxySettings(settings);

            ReadSetting(settings, maskCredentials);
            ReadSetting(settings, oAuth2Mode);
            ReadSetting(settings, retryCount);

            ReadSetting(settings, oAuth2ServerUrl);
            ReadSetting(settings, oAuth2ClientId);
            ReadSetting(settings, oAuth2ClientSecret);
            ReadSetting(settings, oAuth2AccessToken);
            ReadSetting(settings, oAuth2RefreshToken);
            ReadSetting(settings, oAuth2Scope);
            ReadSetting(settings, oAuth2RedirectUri);

            // Read and parse the OAuth2 JSON secrets file if applicable.
            ReadSetting(settings, oAuth2SecretsJsonPath);

            if (!string.IsNullOrEmpty(oAuth2SecretsJsonPath.Value))
            {
                LoadOAuth2SecretsFromFile();
            }

            ReadSetting(settings, oAuth2PrnEmail);
            ReadSetting(settings, timeout);
            ReadSetting(settings, enableGzipCompression);
            ReadSetting(settings, includeUtilitiesInUserAgent);
        }

        /// <summary>
        /// Reads the proxy settings.
        /// </summary>
        /// <param name="settings">The parsed app.config settings.</param>
        private void ReadProxySettings(Dictionary<string, string> settings)
        {
            ConfigSetting<string> proxyServer = new ConfigSetting<string>("ProxyServer", null);
            ConfigSetting<string> proxyUser = new ConfigSetting<string>("ProxyUser", null);
            ConfigSetting<string> proxyPassword = new ConfigSetting<string>("ProxyPassword", null);
            ConfigSetting<string> proxyDomain = new ConfigSetting<string>("ProxyDomain", null);

            ReadSetting(settings, proxyServer);

            if (!string.IsNullOrEmpty(proxyServer.Value))
            {
                WebProxy proxy = new WebProxy()
                {
                    Address = new Uri(proxyServer.Value)
                };
                ReadSetting(settings, proxyUser);
                ReadSetting(settings, proxyPassword);
                ReadSetting(settings, proxyDomain);

                if (!string.IsNullOrEmpty(proxyUser.Value))
                {
                    proxy.Credentials = new NetworkCredential(proxyUser.Value, proxyPassword.Value,
                        proxyDomain.Value);
                }

                this.proxy.Value = proxy;
            }
            else
            {
                // System.Net.WebRequest will find a proxy if needed.
                this.proxy.Value = null;
            }
        }

        /// <summary>
        /// Loads the OAuth2 private key and service account email settings from the
        /// secrets JSON file.
        /// </summary>
        private void LoadOAuth2SecretsFromFile()
        {
            try
            {
                using (StreamReader reader = new StreamReader(OAuth2SecretsJsonPath))
                {
                    string contents = reader.ReadToEnd();
                    Dictionary<string, string> config =
                        JsonConvert.DeserializeObject<Dictionary<string, string>>(contents);

                    ReadSetting(config, oAuth2ServiceAccountEmail);
                    if (string.IsNullOrEmpty(this.OAuth2ServiceAccountEmail))
                    {
                        throw new AdsOAuthException(CommonErrorMessages
                            .ClientEmailIsMissingInJsonFile);
                    }

                    ReadSetting(config, oAuth2PrivateKey);
                    if (string.IsNullOrEmpty(this.OAuth2PrivateKey))
                    {
                        throw new AdsOAuthException(CommonErrorMessages
                            .PrivateKeyIsMissingInJsonFile);
                    }
                }
            }
            catch (AdsOAuthException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new AdsOAuthException(CommonErrorMessages.FailedToLoadJsonSecretsFile, e);
            }
        }

        /// <summary>
        /// Reads a setting from a given dictionary.
        /// </summary>
        /// <param name="settings">The settings collection from which the keys
        /// are to be read.</param>
        /// <param name="settingField">The field that holds the setting value.</param>
        protected void ReadSetting(Dictionary<string, string> settings, ConfigSetting settingField)
        {
            if (settings != null && settings.ContainsKey(settingField.Name))
            {
                settingField.TryParse(settings[settingField.Name]);
            }
        }

        /// <summary>
        /// Sets the specified property and notify any listeners.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="field">The field that store property value.</param>
        /// <param name="newValue">The new value to be set.</param>
        /// <param name="propertyName">Name of the property.</param>
        protected void SetPropertyAndNotify<T>(ConfigSetting<T> field, T newValue,
            [CallerMemberName] String propertyName = "")
        {
            if (!EqualityComparer<T>.Default.Equals(field.Value, newValue))
            {
                field.Value = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public virtual object Clone()
        {
            return MemberwiseClone();
        }
    }
}
