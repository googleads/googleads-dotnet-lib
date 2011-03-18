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

using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Reflection;
using System.Xml;
using System.Globalization;

namespace Google.Api.Ads.Common.Lib {
  /// <summary>
  /// This class reads the configuration keys from App.config.
  /// </summary>
  public abstract class AppConfigBase {
    /// <summary>
    /// Key name for logPath.
    /// </summary>
    private const string LOG_PATH = "LogPath";

    /// <summary>
    /// Key name for logToConsole.
    /// </summary>
    private const string LOG_TO_CONSOLE = "LogToConsole";

    /// <summary>
    /// Key name for logToFile.
    /// </summary>
    private const string LOG_TO_FILE = "LogToFile";

    /// <summary>
    /// Key name for logErrorsOnly.
    /// </summary>
    private const string LOG_ERRORS_ONLY = "LogErrorsOnly";

    /// <summary>
    /// Key name for proxyServer
    /// </summary>
    private const string PROXY_SERVER = "ProxyServer";

    /// <summary>
    /// Key name for proxyUser.
    /// </summary>
    private const string PROXY_USER = "ProxyUser";

    /// <summary>
    /// Key name for proxyPassword.
    /// </summary>
    private const string PROXY_PASSWORD = "ProxyPassword";

    /// <summary>
    /// Key name for proxyDomain.
    /// </summary>
    private const string PROXY_DOMAIN = "ProxyDomain";

    /// <summary>
    /// Key name for maskCredentials.
    /// </summary>
    private const string MASK_CREDENTIALS = "MaskCredentials";

    /// <summary>
    /// Key name for timeout.
    /// </summary>
    private const string TIMEOUT = "Timeout";

    /// <summary>
    /// Path to which the SOAP logs are to be saved.
    /// </summary>
    private string logPath;

    /// <summary>
    /// True, if the SOAP logs should be written to console.
    /// </summary>
    private bool logToConsoleField;

    /// <summary>
    /// True, if the SOAP logs should be written to file.
    /// </summary>
    private bool logToFileField;

    /// <summary>
    /// True, if only the SOAP logs that correspond to an error
    /// should be logged.
    /// </summary>
    private bool logErrorsOnlyField;

    /// <summary>
    /// Web proxy to be used with the services.
    /// </summary>
    private WebProxy proxyField;

    /// <summary>
    /// True, if the credentials in the log file should be masked.
    /// </summary>
    private bool maskCredentials;

    /// <summary>
    /// The short name for this library.
    /// </summary>
    protected string shortNameField;

    /// <summary>
    /// Timeout to be used for Ads services in milliseconds.
    /// </summary>
    private int timeout;

    /// <summary>
    /// Default value for timeout for Ads services.
    /// </summary>
    private const int DEFAULT_TIMEOUT = 100000;

    /// <summary>
    /// Gets the path to which the SOAP logs are to be saved.
    /// </summary>
    public string LogPath {
      get {
        return logPath;
      }
    }

    /// <summary>
    /// Gets whether the SOAP logs should be written to console.
    /// </summary>
    public bool LogToConsole {
      get {
        return logToConsoleField;
      }
    }

    /// <summary>
    /// Gets whether the SOAP logs that correspond to an error
    /// should be logged.
    /// </summary>
    public bool LogErrorsOnly {
      get {
        return logErrorsOnlyField;
      }
    }

    /// <summary>
    /// Gets whether the SOAP logs should be written to file.
    /// </summary>
    public bool LogToFile {
      get {
        return logToFileField;
      }
    }

    /// <summary>
    /// Gets the web proxy to be used with the services.
    /// </summary>
    public WebProxy Proxy {
      get {
        return proxyField;
      }
    }

    /// <summary>
    /// Gets whether the credentials in the log file should be masked.
    /// </summary>
    public bool MaskCredentials {
      get {
        return maskCredentials;
      }
    }

    /// <summary>
    /// Gets or sets the timeout for Ads services in milliseconds.
    /// </summary>
    public int Timeout {
      get {
        return timeout;
      }
      set {
        timeout = value;
      }
    }

    /// <summary>
    /// Gets the short name for this library.
    /// </summary>
    public string ShortName {
      get {
        return shortNameField;
      }
    }

    /// <summary>
    /// Gets the signature for this library.
    /// </summary>
    public string Signature {
      get {
        return string.Format(CultureInfo.InvariantCulture, "{0}-{1}", ShortName, Version);
      }
    }

    /// <summary>
    /// Gets a version string for this assembly.
    /// </summary>
    /// <returns>The version string in format Major.Minor.Revision.</returns>
    public string Version {
      get {
        Version version = Assembly.GetExecutingAssembly().GetName().Version;
        return string.Format(CultureInfo.InvariantCulture, "{0}.{1}.{2}", version.Major,
            version.Minor, version.Revision);
      }
    }

    /// <summary>
    /// Default constructor for the object.
    /// </summary>
    protected AppConfigBase() {
      logPath = "C:\\";
      logToConsoleField = false;
      logToFileField = false;
      logErrorsOnlyField = false;
      proxyField = null;
      maskCredentials = true;
      timeout = DEFAULT_TIMEOUT;
    }

    /// <summary>
    /// Read all settings from App.config.
    /// </summary>
    /// <param name="settings">The parsed app.config settings.</param>
    protected virtual void ReadSettings(Hashtable settings) {
      // Common keys.
      logPath = ReadSetting(settings, LOG_PATH, logPath);
      logToConsoleField = bool.Parse(ReadSetting(settings, LOG_TO_CONSOLE,
          logToConsoleField.ToString()));
      logToFileField = bool.Parse(ReadSetting(settings, LOG_TO_FILE, logToFileField.ToString()));
      logErrorsOnlyField = bool.Parse(ReadSetting(settings, LOG_ERRORS_ONLY,
          logErrorsOnlyField.ToString()));

      string proxyUrl = ReadSetting(settings, PROXY_SERVER, "");

      if (!string.IsNullOrEmpty(proxyUrl)) {
        WebProxy proxy = new WebProxy();
        proxy.Address = new Uri(proxyUrl);

        string proxyUser = ReadSetting(settings, PROXY_USER, "");
        string proxyPassword = ReadSetting(settings, PROXY_PASSWORD, "");
        string proxyDomain = ReadSetting(settings, PROXY_DOMAIN, "");

        if (!string.IsNullOrEmpty(proxyUrl)) {
          proxy.Credentials = new NetworkCredential(proxyUser,
              proxyPassword, proxyDomain);
        }
        this.proxyField = proxy;
      }
      maskCredentials = bool.Parse(ReadSetting(settings, MASK_CREDENTIALS,
          maskCredentials.ToString()));
      timeout = int.Parse(ReadSetting(settings, TIMEOUT, timeout.ToString()));
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
    protected static string ReadSetting(Hashtable settings, string key, string defaultValue) {
      if (settings == null) {
        return defaultValue;
      } else {
        return settings.ContainsKey(key) ? (string) settings[key] : defaultValue;
      }
    }
  }
}
