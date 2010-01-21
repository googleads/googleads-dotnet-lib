// Copyright 2009, Google Inc. All Rights Reserved.
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

using com.google.api.adwords.lib;

using System;
using System.Configuration;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Xml;

namespace com.google.api.adwords.lib {
  /// <summary>
  /// This class reads the configuration keys from App.config.
  /// </summary>
  public sealed class ApplicationConfiguration : IConfigurationSectionHandler {
    /// <summary>
    /// Static constructor for the object.
    /// </summary>
    static ApplicationConfiguration() {
      Object obj = ConfigurationManager.GetSection("AdWordsApi");
    }

    /// <summary>
    /// Creates a configuration section handler.
    /// </summary>
    /// <param name="parent">Parent object.</param>
    /// <param name="configContext">Configuration context object.</param>
    /// <param name="section">Section XML node.</param>
    /// <returns>The created section handler object.</returns>
    public Object Create(Object parent, object configContext, XmlNode section) {
      NameValueCollection settings;
      try {
        NameValueSectionHandler baseHandler = new NameValueSectionHandler();
        settings = (NameValueCollection) baseHandler.Create(parent,
            configContext, section);
      } catch {
        settings = null;
      }

      if (settings != null) {
        // Read Application settings from configuration file.

        // Common keys.
        logPath = ReadSetting(settings, LOG_PATH, "C:\\");
        logToConsole = bool.Parse(
            ReadSetting(settings, LOG_TO_CONSOLE, "false"));
        logToFile = bool.Parse(
            ReadSetting(settings, LOG_TO_FILE, "false"));

        string proxyUrl = ReadSetting(settings, PROXY_SERVER, "");

        if(!string.IsNullOrEmpty(proxyUrl)) {
          WebProxy proxy = new WebProxy();
          proxy.Address = new Uri(proxyUrl);

          string proxyUser = ReadSetting(settings, PROXY_USER, "");
          string proxyPassword = ReadSetting(settings, PROXY_PASSWORD, "");
          string proxyDomain = ReadSetting(settings, PROXY_DOMAIN, "");

          if(!string.IsNullOrEmpty(proxyUrl)) {
            proxy.Credentials = new NetworkCredential(proxyUser,
                proxyPassword, proxyDomain);
          }
          ApplicationConfiguration.proxy = proxy;
        }
        companyName = ReadSetting(settings, COMPANY_NAME, "");
        authToken = ReadSetting(settings, AUTHTOKEN, "");
        email = ReadSetting(settings, EMAIL, "");
        password = ReadSetting(settings, PASSWORD, "");
        clientEmail = ReadSetting(settings, CLIENT_EMAIL, "");
        clientCustomerId = ReadSetting(settings, CLIENT_CUSTOMER_ID, "");
        developerToken = ReadSetting(settings, DEVELOPER_TOKEN, "");
        applicationToken = ReadSetting(settings, APPLICATION_TOKEN, "");
        maskCredentials = bool.Parse(ReadSetting(settings, MASK_CREDENTIALS, "true"));

        // Legacy AdWords API keys.
        legacyAdWordsApiUrl = ReadSetting(settings, LEGACY_ADWORDSAPI_URL, "https://adwords.google.com");

        // AdWords API keys.
        adWordsApiUrl = ReadSetting(settings, ADWORDSAPI_URL, "https://adwords.google.com");
      }
      return null;
    }

    /// <summary>
    /// Reads a setting from a given NameValueCollection, and sets
    /// default value if the key is not available in the collection.
    /// </summary>
    /// <param name="settings">The settings collection from which the keys
    /// are to be read.</param>
    /// <param name="key">Key name to be read.</param>
    /// <param name="defaultValue">Default value for the key.</param>
    /// <returns>Actual value from settings, or defaultValue if settings
    /// does not have this key.</returns>
    internal static String ReadSetting(NameValueCollection settings,
        String key, String defaultValue) {
      if (settings == null || key == null)
        throw new ArgumentNullException();
      try {
        Object setting = settings[key];
        return (setting == null) ? defaultValue : (String) setting;
      } catch {
        return defaultValue;
      }
    }

    /// <summary>
    /// Authtoken to be used in making API calls.
    /// </summary>
    public static String authToken;

    /// <summary>
    /// Email to be used in getting AuthToken.
    /// </summary>
    public static String email;

    /// <summary>
    /// Password to be used in getting AuthToken.
    /// </summary>
    public static String password;

    /// <summary>
    /// ClientEmail to be used in SOAP headers.
    /// </summary>
    public static String clientEmail;

    /// <summary>
    /// ClientCustomerId to be used in SOAP headers.
    /// </summary>
    public static String clientCustomerId;

    /// <summary>
    /// Path to which the SOAP logs are to be saved.
    /// </summary>
    public static String logPath;

    /// <summary>
    /// DeveloperToken to be used in the SOAP header.
    /// </summary>
    public static String developerToken;

    /// <summary>
    /// ApplicationToken to be used in the SOAP header.
    /// </summary>
    public static String applicationToken;

    /// <summary>
    /// True, if the SOAP logs should be written to console.
    /// </summary>
    public static bool logToConsole;

    /// <summary>
    /// True, if the SOAP logs should be written to file.
    /// </summary>
    public static bool logToFile;

    /// <summary>
    /// Web proxy to be used with the services.
    /// </summary>
    public static WebProxy proxy;

    /// <summary>
    /// Developer's company name, to be used in useragent string.
    /// </summary>
    public static String companyName;

    /// <summary>
    /// True, if the credentials in the log file should be masked.
    /// </summary>
    public static bool maskCredentials;

    /// <summary>
    /// Key name for authToken.
    /// </summary>
    internal const String AUTHTOKEN = "AuthToken";

    /// <summary>
    /// Key name for email.
    /// </summary>
    internal const String EMAIL = "Email";

    /// <summary>
    /// Key name for password.
    /// </summary>
    internal const String PASSWORD = "Password";

    /// <summary>
    /// Key name for clientEmail.
    /// </summary>
    internal const String CLIENT_EMAIL = "ClientEmail";

    /// <summary>
    /// Key name for clientCustomerId.
    /// </summary>
    internal const String CLIENT_CUSTOMER_ID = "ClientCustomerId";

    /// <summary>
    /// Key name for developerToken.
    /// </summary>
    internal const String DEVELOPER_TOKEN = "DeveloperToken";

    /// <summary>
    /// Key name for applicationToken.
    /// </summary>
    internal const String APPLICATION_TOKEN = "ApplicationToken";

    /// <summary>
    /// Key name for logPath.
    /// </summary>
    internal const String LOG_PATH = "LogPath";

    /// <summary>
    /// Key name for logToConsole.
    /// </summary>
    internal const String LOG_TO_CONSOLE = "LogToConsole";

    /// <summary>
    /// Key name for logToFile.
    /// </summary>
    internal const String LOG_TO_FILE = "LogToFile";

    /// <summary>
    /// Key name for proxyServer
    /// </summary>
    internal const String PROXY_SERVER = "ProxyServer";

    /// <summary>
    /// Key name for proxyUser.
    /// </summary>
    internal const String PROXY_USER = "ProxyUser";

    /// <summary>
    /// Key name for proxyPassword.
    /// </summary>
    internal const String PROXY_PASSWORD = "ProxyPassword";

    /// <summary>
    /// Key name for proxyDomain.
    /// </summary>
    internal const String PROXY_DOMAIN = "ProxyDomain";

    /// <summary>
    /// Key name for companyName.
    /// </summary>
    internal const String COMPANY_NAME = "CompanyName";

    /// <summary>
    /// Key name for maskCredentials.
    /// </summary>
    internal const String MASK_CREDENTIALS = "MaskCredentials";

    /// <summary>
    /// Url for Legacy AdWords API.
    /// </summary>
    public static String legacyAdWordsApiUrl;

    /// <summary>
    /// Key name for Legacy AdWords Api Url.
    /// </summary>
    internal const String LEGACY_ADWORDSAPI_URL = "LegacyAdWordsApi.Url";

    /// <summary>
    /// Url for AdWords API.
    /// </summary>
    public static String adWordsApiUrl;

    /// <summary>
    /// Key name for AdWords Api Url.
    /// </summary>
    internal const String ADWORDSAPI_URL = "AdWordsApi.Url";

    /// <summary>
    /// The publicly released version number.
    /// </summary>
    public const String version = "6.2";
  }
}
