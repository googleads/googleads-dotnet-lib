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
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

namespace Google.Api.Ads.Common.Lib {
  /// <summary>
  /// This class reads the configuration keys from App.config.
  /// </summary>
  public abstract class AppConfigBase : INotifyPropertyChanged, AppConfig {
    /// <summary>
    /// The short name to identify this assembly.
    /// </summary>
    private const string SHORT_NAME = "Common-Dotnet";

    /// <summary>
    /// Key name for logPath.
    /// </summary>
    private const string LOG_PATH = "LogPath";

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
    /// Key name for retryCount.
    /// </summary>
    private const string RETRYCOUNT = "RetryCount";

    /// <summary>
    /// Key name for enableGzipCompression.
    /// </summary>
    private const string ENABLE_GZIP_COMPRESSION = "EnableGzipCompression";

    /// <summary>
    /// Key name for OAuth2 mode.
    /// </summary>
    private const string OAUTH2_MODE = "OAuth2Mode";

    /// <summary>
    /// Key name for OAuth2 client id.
    /// </summary>
    private const string OAUTH2_CLIENTID = "OAuth2ClientId";

    /// <summary>
    /// Key name for OAuth2 client secret.
    /// </summary>
    private const string OAUTH2_CLIENTSECRET = "OAuth2ClientSecret";

    /// <summary>
    /// Key name for OAuth2 access token.
    /// </summary>
    private const string OAUTH2_ACCESSTOKEN = "OAuth2AccessToken";

    /// <summary>
    /// Key name for OAuth2 refresh token.
    /// </summary>
    private const string OAUTH2_REFRESHTOKEN = "OAuth2RefreshToken";

    /// <summary>
    /// Key name for OAuth2 scope.
    /// </summary>
    private const string OAUTH2_SCOPE = "OAuth2Scope";

    /// <summary>
    /// Key name for redirect uri.
    /// </summary>
    private const string OAUTH2_REDIRECTURI = "OAuth2RedirectUri";

    /// <summary>
    /// Key name for service account email.
    /// </summary>
    private const string OAUTH2_SERVICEACCOUNT_EMAIL = "OAuth2ServiceAccountEmail";

    /// <summary>
    /// Key name for prn account email.
    /// </summary>
    private const string OAUTH2_PRN_EMAIL = "OAuth2PrnEmail";

    /// <summary>
    /// Key name for jwt certificate path.
    /// </summary>
    private const string OAUTH2_JWT_CERTIFICATE_PATH = "OAuth2JwtCertificatePath";

    /// <summary>
    /// Key name for jwt certificate password.
    /// </summary>
    private const string OAUTH2_JWT_CERTIFICATE_PASSWORD = "OAuth2JwtCertificatePassword";

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
    /// Path to which the SOAP logs are to be saved.
    /// </summary>
    private string logPath;

    /// <summary>
    /// True, if the SOAP logs should be written to file.
    /// </summary>
    private bool logToFile;

    /// <summary>
    /// True, if only the SOAP logs that correspond to an error
    /// should be logged.
    /// </summary>
    private bool logErrorsOnly;

    /// <summary>
    /// Web proxy to be used with the services.
    /// </summary>
    private IWebProxy proxy;

    /// <summary>
    /// True, if the credentials in the log file should be masked.
    /// </summary>
    private bool maskCredentials;

    /// <summary>
    /// Timeout to be used for Ads services in milliseconds.
    /// </summary>
    private int timeout;

    /// <summary>
    /// Number of times to retry a call if an API call fails and can be retried.
    /// </summary>
    private int retryCount;

    /// <summary>
    /// True, if gzip compression should be turned on for SOAP requests and
    /// responses.
    /// </summary>
    private bool enableGzipCompression;

    /// <summary>
    /// OAuth2 client id.
    /// </summary>
    private string oAuth2ClientId;

    /// <summary>
    /// OAuth2 client secret.
    /// </summary>
    private string oAuth2ClientSecret;

    /// <summary>
    /// OAuth2 access token.
    /// </summary>
    private string oAuth2AccessToken;

    /// <summary>
    /// OAuth2 refresh token.
    /// </summary>
    private string oAuth2RefreshToken;

    /// <summary>
    /// OAuth2 service account email.
    /// </summary>
    private string oAuth2ServiceAccountEmail;

    /// <summary>
    /// OAuth2 prn email.
    /// </summary>
    private string oAuth2PrnEmail;

    /// <summary>
    /// OAuth2 certificate path.
    /// </summary>
    private string oAuth2CertificatePath;

    /// <summary>
    /// OAuth2 certificate password.
    /// </summary>
    private string oAuth2CertificatePassword;

    /// <summary>
    /// OAuth2 scope.
    /// </summary>
    private string oAuth2Scope;

    /// <summary>
    /// Redirect uri.
    /// </summary>
    private string oAuth2RedirectUri;

    /// <summary>
    /// OAuth2 mode.
    /// </summary>
    private OAuth2Flow oAuth2Mode;

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
    /// Default value for number of times to retry a call if an API call fails
    /// and can be retried.
    /// </summary>
    private const int DEFAULT_RETRYCOUNT = 0;

    /// <summary>
    /// Default value for timeout for Ads services.
    /// </summary>
    private const int DEFAULT_TIMEOUT = -1;

    /// <summary>
    /// Gets the path to which the SOAP logs are to be saved.
    /// </summary>
    public string LogPath {
      get {
        return logPath;
      }
      protected set {
        logPath = value;
      }
    }

    /// <summary>
    /// Gets whether the SOAP logs that correspond to an error
    /// should be logged.
    /// </summary>
    public bool LogErrorsOnly {
      get {
        return logErrorsOnly;
      }
      protected set {
        logErrorsOnly = value;
      }
    }

    /// <summary>
    /// Gets whether the SOAP logs should be written to file.
    /// </summary>
    public bool LogToFile {
      get {
        return logToFile;
      }
      protected set {
        logToFile = value;
      }
    }

    /// <summary>
    /// Gets whether the credentials in the log file should be masked.
    /// </summary>
    public bool MaskCredentials {
      get {
        return maskCredentials;
      }
      protected set {
        maskCredentials = value;
      }
    }

    /// <summary>
    /// Gets the web proxy to be used with the services.
    /// </summary>
    public IWebProxy Proxy {
      get {
        return proxy;
      }
      set {
        SetPropertyField("Proxy", ref proxy, value);
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
        SetPropertyField("Timeout", ref timeout, value);
      }
    }

    /// <summary>
    /// Gets or sets the number of times to retry a call if an API call fails
    /// and can be retried.
    /// </summary>
    public int RetryCount {
      get {
        return retryCount;
      }
      set {
        SetPropertyField("RetryCount", ref retryCount, value);
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
        SetPropertyField("EnableGzipCompression", ref enableGzipCompression, value);
      }
    }

    /// <summary>
    /// Gets or sets the OAuth2 client id.
    /// </summary>
    public string OAuth2ClientId {
      get {
        return oAuth2ClientId;
      }
      set {
        SetPropertyField("OAuth2ClientId", ref oAuth2ClientId, value);
      }
    }

    /// <summary>
    /// Gets or sets the OAuth2 client secret.
    /// </summary>
    public string OAuth2ClientSecret {
      get {
        return oAuth2ClientSecret;
      }
      set {
        SetPropertyField("OAuth2ClientSecret", ref oAuth2ClientSecret, value);
      }
    }

    /// <summary>
    /// Gets or sets the OAuth2 access token.
    /// </summary>
    public string OAuth2AccessToken {
      get {
        return oAuth2AccessToken;
      }
      set {
        SetPropertyField("OAuth2AccessToken", ref oAuth2AccessToken, value);
      }
    }

    /// <summary>
    /// Gets or sets the OAuth2 refresh token.
    /// </summary>
    /// <remarks>This key is applicable only when using OAuth2 web / application
    /// flow in offline mode.</remarks>
    public string OAuth2RefreshToken {
      get {
        return oAuth2RefreshToken;
      }
      set {
        SetPropertyField("OAuth2RefreshToken", ref oAuth2RefreshToken, value);
      }
    }

    /// <summary>
    /// Gets or sets the OAuth2 scope.
    /// </summary>
    public string OAuth2Scope {
      get {
        return oAuth2Scope;
      }
      set {
        SetPropertyField("OAuth2Scope", ref oAuth2Scope, value);
      }
    }

    /// <summary>
    /// Gets or sets the OAuth2 redirect URI.
    /// </summary>
    /// <remarks>This key is applicable only when using OAuth2 web flow.
    /// </remarks>
    public string OAuth2RedirectUri {
      get {
        return oAuth2RedirectUri;
      }
      set {
        SetPropertyField("OAuth2RedirectUri", ref oAuth2RedirectUri, value);
      }
    }

    /// <summary>
    /// Gets or sets the OAuth2 mode.
    /// </summary>
    public OAuth2Flow OAuth2Mode {
      get {
        return oAuth2Mode;
      }
      set {
        SetPropertyField("OAuth2Mode", ref oAuth2Mode, value);
      }
    }

    /// <summary>
    /// Gets or sets the OAuth2 service account email.
    /// </summary>
    /// <remarks>This key is applicable only when using OAuth2 service accounts.
    /// </remarks>
    public string OAuth2ServiceAccountEmail {
      get {
        return oAuth2ServiceAccountEmail;
      }
      set {
        SetPropertyField("OAuth2ServiceAccountEmail", ref oAuth2ServiceAccountEmail, value);
      }
    }

    /// <summary>
    /// Gets or sets the OAuth2 prn email.
    /// </summary>
    /// <remarks>This key is applicable only when using OAuth2 service accounts.
    /// </remarks>
    public string OAuth2PrnEmail {
      get {
        return oAuth2PrnEmail;
      }
      set {
        SetPropertyField("OAuth2PrnEmail", ref oAuth2PrnEmail, value);
      }
    }

    /// <summary>
    /// Gets or sets the OAuth2 certificate path.
    /// </summary>
    /// <remarks>This key is applicable only when using OAuth2 service accounts.
    /// </remarks>
    public string OAuth2CertificatePath {
      get {
        return oAuth2CertificatePath;
      }
      set {
        SetPropertyField("OAuth2CertificatePath", ref oAuth2CertificatePath, value);
      }
    }

    /// <summary>
    /// Gets or sets the OAuth2 certificate password.
    /// </summary>
    /// <remarks>This key is applicable only when using OAuth2 service accounts.
    /// </remarks>
    public string OAuth2CertificatePassword {
      get {
        return oAuth2CertificatePassword;
      }
      set {
        SetPropertyField("OAuth2CertificatePassword", ref oAuth2CertificatePassword, value);
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
        SetPropertyField("Email", ref email, value);
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
        SetPropertyField("Password", ref password, value);
      }
    }

    /// <summary>
    /// Gets or sets the auth token to be used in SOAP headers.
    /// </summary>
    public string AuthToken {
      get {
        return authToken;
      }
      set {
        SetPropertyField("AuthToken", ref authToken, value);
      }
    }

    /// <summary>
    /// Gets the default OAuth2 scope.
    /// </summary>
    public virtual string GetDefaultOAuth2Scope() {
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
    private string GetAssemblySignatureFromAppConfigType(Type type) {
      Type appConfigBaseType = typeof(AppConfigBase);
      if (!(type.BaseType == appConfigBaseType || type == appConfigBaseType)) {
        throw new ArgumentException(string.Format("{0} is not derived from {1}.",
            type.FullName, appConfigBaseType.FullName));
      }
      Version version = type.Assembly.GetName().Version;
      string shortName = (string) type.GetField("SHORT_NAME", BindingFlags.NonPublic |
          BindingFlags.Static).GetValue(null);
      return string.Format("{0}/{1}.{2}.{3}", shortName, version.Major, version.Minor,
          version.Revision);
    }

    /// <summary>
    /// Gets the signature for this library.
    /// </summary>
    public string Signature {
      get {
        return string.Format("{0}, {1}, .NET CLR/{2}",
            GetAssemblySignatureFromAppConfigType(this.GetType()),
            GetAssemblySignatureFromAppConfigType(this.GetType().BaseType), Environment.Version);
      }
    }

    /// <summary>
    /// Gets the number of seconds after Jan 1, 1970, 00:00:00
    /// </summary>
    public virtual long UnixTimestamp {
      get {
        TimeSpan unixTime = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
        return (long) unixTime.TotalSeconds;
      }
    }

    /// <summary>
    /// Default constructor for the object.
    /// </summary>
    protected AppConfigBase() {
      logPath = "C:\\";
      logToFile = false;
      logErrorsOnly = false;
      proxy = null;
      maskCredentials = true;
      timeout = DEFAULT_TIMEOUT;
      enableGzipCompression = true;
      oAuth2Mode = OAuth2Flow.APPLICATION;
      oAuth2ClientId = "";
      oAuth2ClientSecret = "";
      oAuth2AccessToken = "";
      oAuth2RefreshToken = "";
      oAuth2Scope = "";
      oAuth2RedirectUri = null;
      oAuth2PrnEmail = "";
      oAuth2ServiceAccountEmail = "";
      authToken = "";
      email = "";
      password = "";
    }

    /// <summary>
    /// Read all settings from App.config.
    /// </summary>
    /// <param name="settings">The parsed app.config settings.</param>
    protected virtual void ReadSettings(Hashtable settings) {
      // Common keys.
      logPath = ReadSetting(settings, LOG_PATH, logPath);
      logToFile = bool.Parse(ReadSetting(settings, LOG_TO_FILE, logToFile.ToString()));
      logErrorsOnly = bool.Parse(ReadSetting(settings, LOG_ERRORS_ONLY,
          logErrorsOnly.ToString()));

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
        this.proxy = proxy;
      } else {
        // System.Net.WebRequest will find a proxy if needed.
        this.proxy = null;
      }
      maskCredentials = bool.Parse(ReadSetting(settings, MASK_CREDENTIALS,
          maskCredentials.ToString()));

      try {
        oAuth2Mode = (OAuth2Flow) Enum.Parse(typeof(OAuth2Flow), ReadSetting(settings, OAUTH2_MODE,
            oAuth2Mode.ToString()));
      } catch (Exception e) {
        // No action.
      }

      oAuth2ClientId = ReadSetting(settings, OAUTH2_CLIENTID, oAuth2ClientId);
      oAuth2ClientSecret = ReadSetting(settings, OAUTH2_CLIENTSECRET, oAuth2ClientSecret);
      oAuth2AccessToken = ReadSetting(settings, OAUTH2_ACCESSTOKEN, oAuth2AccessToken);
      oAuth2RefreshToken = ReadSetting(settings, OAUTH2_REFRESHTOKEN, oAuth2RefreshToken);
      oAuth2Scope = ReadSetting(settings, OAUTH2_SCOPE, oAuth2Scope);
      oAuth2RedirectUri = ReadSetting(settings, OAUTH2_REDIRECTURI, oAuth2RedirectUri);
      oAuth2ServiceAccountEmail = ReadSetting(settings, OAUTH2_SERVICEACCOUNT_EMAIL,
          oAuth2ServiceAccountEmail);
      oAuth2PrnEmail = ReadSetting(settings, OAUTH2_PRN_EMAIL, oAuth2PrnEmail);

      oAuth2CertificatePath = ReadSetting(settings, OAUTH2_JWT_CERTIFICATE_PATH,
          oAuth2CertificatePath);
      oAuth2CertificatePassword = ReadSetting(settings, OAUTH2_JWT_CERTIFICATE_PASSWORD,
          oAuth2CertificatePassword);

      email = ReadSetting(settings, EMAIL, email);
      password = ReadSetting(settings, PASSWORD, password);
      authToken = ReadSetting(settings, AUTHTOKEN, authToken);

      int.TryParse(ReadSetting(settings, TIMEOUT, timeout.ToString()), out timeout);
      int.TryParse(ReadSetting(settings, RETRYCOUNT, retryCount.ToString()), out retryCount);
      bool.TryParse(ReadSetting(settings, ENABLE_GZIP_COMPRESSION,
          enableGzipCompression.ToString()), out enableGzipCompression);
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

    /// <summary>
    /// Raises the <see cref="E:PropertyChanged"/> event.
    /// </summary>
    /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/>
    /// instance containing the event data.</param>
    protected void OnPropertyChanged(PropertyChangedEventArgs e) {
      PropertyChangedEventHandler handler = PropertyChanged;
      if (handler != null) {
        handler(this, e);
      }
    }

    /// <summary>
    /// Sets the specified property field.
    /// </summary>
    /// <typeparam name="T">Type of the property field to be set.</typeparam>
    /// <param name="propertyName">Name of the property.</param>
    /// <param name="field">The property field to be set.</param>
    /// <param name="newValue">The new value to be set to the propery.</param>
    protected void SetPropertyField<T>(string propertyName, ref T field, T newValue) {
      if (!EqualityComparer<T>.Default.Equals(field, newValue)) {
        field = newValue;
        OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
      }
    }

    /// <summary>
    /// Occurs when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;
  }
}
