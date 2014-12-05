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

using Google.Api.Ads.Common.Logging;
using Google.Api.Ads.Common.Util;

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace Google.Api.Ads.Common.Lib {

  /// <summary>
  /// This class wraps the ClientLogin API. See
  /// http://code.google.com/apis/accounts/docs/AuthForInstalledApps.html
  /// for details.
  /// </summary>
  [Obsolete("ClientLogin API is deprecated, see " +
      "https://developers.google.com/accounts/docs/AuthForInstalledApps for details. OAuth2 is " +
      "the recommended authentication mechanism. You can refer to the code examples for details " +
      " on how to use OAUth2 in your application. You can use Util\\OAuth2TokenGenerator.cs for " +
      "generating OAuth2 refresh tokens for offline access to various Ads* APIs.")]
  public class AuthToken : Configurable {

    /// <summary>
    /// Url endpoint for ClientLogin API.
    /// </summary>
    private static readonly Uri url = new Uri("https://www.google.com/accounts/ClientLogin");

    /// <summary>
    /// The prefix to be appended for captcha urls.
    /// </summary>
    private readonly string CaptchaUrlPrefix = "http://www.google.com/accounts/";

    /// <summary>
    /// Account type to be used with ClientLogin API.
    /// </summary>
    private const string ACCOUNT_TYPE = "GOOGLE";

    /// <summary>
    /// Service type to be used with ClientLogin API.
    /// </summary>
    private string service;

    /// <summary>
    /// The configuration class to configure this instance.
    /// </summary>
    private AppConfig config;

    /// <summary>
    /// The list of request headers to mask in the logs.
    /// </summary>
    private readonly HashSet<string> REQUEST_HEADERS_TO_MASK = new HashSet<string> {
        "Email", "Passwd"
    };

    /// <summary>
    /// The list of response fields to mask in the logs.
    /// </summary>
    private readonly HashSet<string> RESPONSE_FIELDS_TO_MASK = new HashSet<string> {
        "Auth"
    };

    /// <summary>
    /// The cache for storing authtokens.
    /// </summary>
    private static AuthTokenCache cache = new DefaultAuthTokenCache();

    /// <summary>
    /// Gets the URL.
    /// </summary>
    public static Uri Url {
      get {
        return url;
      }
    }

    /// <summary>
    /// Gets the SOURCE parameter for calling ClientLogin API.
    /// </summary>
    private string SOURCE {
      get {
        return string.Format(CultureInfo.InvariantCulture, "Google-{0}", config.Signature);
      }
    }

    /// <summary>
    /// Gets or sets the email to be used for authentication..
    /// </summary>
    public string Email {
      get {
        return config.Email;
      }
      set {
        config.Email = value;
      }
    }

    /// <summary>
    /// Gets or sets the password to be used for authentication.
    /// </summary>
    public string Password {
      get {
        return config.Password;
      }
      set {
        config.Password = value;
      }
    }

    /// <summary>
    /// Gets or sets the service type to be used with ClientLogin API..
    /// </summary>
    /// <value>
    /// The service.
    /// </value>
    public string Service {
      get {
        return service;
      }
      set {
        service = value;
      }
    }

    /// <summary>
    /// Gets or sets the configuration class to configure this instance.
    /// </summary>
    /// <value>
    /// The config.
    /// </value>
    public AppConfig Config {
      get {
        return config;
      }
      set {
        config = value;
      }
    }

    /// <summary>
    /// Gets or sets the cache for storing authtokens..
    /// </summary>
    public static AuthTokenCache Cache {
      get {
        return cache;
      }
      set {
        cache = value;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthToken"/> class.
    /// </summary>
    public AuthToken()
      : this(null, null) {
    }

    /// <summary>
    /// Public constructor.
    /// </summary>
    /// <param name="email">Email to be used for authentication.</param>
    /// <param name="password">Password to be used for authentication</param>
    /// <param name="config">The configuration object for use with this object.
    /// </param>
    /// <param name="service">The gaia service name for which tokens are being
    /// generated.</param>
    [Obsolete("Use AuthToken(AppConfig config, string service) constructor instead.")]
    public AuthToken(AppConfig config, string service, string email, string password) {
      this.config = config;
      this.Email = email;
      this.Password = password;
      this.service = service;
    }

    /// <summary>
    /// Public constructor.
    /// </summary>
    /// <param name="config">The configuration object for use with this object.
    /// </param>
    /// <param name="service">The gaia service name for which tokens are being
    /// generated.</param>
    public AuthToken(AppConfig config, string service) {
      this.config = config;
      this.service = service;
    }

    /// <summary>
    /// Obtains a ClientLogin token for use with various Ads APIs.
    /// </summary>
    /// <returns>The token string.</returns>
    /// <exception cref="AuthTokenException">If the token cannot be obtained,
    /// then an AuthTokenException is thrown with appropriate error code.
    /// </exception>
    public string GetToken() {
      DeprecationUtilities.ShowDeprecationMessage(this.GetType());

      if (string.IsNullOrEmpty(Email)) {
        throw new ArgumentNullException(CommonErrorMessages.EmailCannotBeNull);
      }

      if (string.IsNullOrEmpty(Password)) {
        throw new ArgumentNullException(CommonErrorMessages.PasswordCannotBeNull);
      }

      string cachedToken = cache.GetToken(this.service, Email);
      if (cachedToken == null) {
        lock (this.GetType()) {
          cachedToken = cache.GetToken(this.service, Email);
          return (cachedToken != null) ? cachedToken : cache.AddToken(this.service, Email,
              GenerateToken());
        }
      }
      return cachedToken;
    }

    /// <summary>
    /// Generates a ClientLogin token for use with various Ads APIs.
    /// </summary>
    /// <returns>The token string.</returns>
    /// <exception cref="AuthTokenException">If the token cannot be obtained,
    /// then an AuthTokenException is thrown with appropriate error code.
    /// </exception>
    private string GenerateToken() {
      WebRequest webRequest = HttpWebRequest.Create(url);
      webRequest.Method = "POST";
      webRequest.ContentType = "application/x-www-form-urlencoded";

      webRequest.Proxy = config.Proxy;
      webRequest.Timeout = config.Timeout;

      string postParams =
          "accountType=" + HttpUtility.UrlEncode(ACCOUNT_TYPE) +
          "&Email=" + HttpUtility.UrlEncode(Email) +
          "&Passwd=" + HttpUtility.UrlEncode(Password) +
          "&service=" + HttpUtility.UrlEncode(service) +
          "&source=" + HttpUtility.UrlEncode(SOURCE);

      byte[] postBytes = Encoding.UTF8.GetBytes(postParams);
      webRequest.ContentLength = postBytes.Length;

      using (Stream strmReq = webRequest.GetRequestStream()) {
        strmReq.Write(postBytes, 0, postBytes.Length);
      }

      LogEntry logEntry = new LogEntry(config, new DefaultDateTimeProvider());

      logEntry.LogRequest(webRequest, postParams, REQUEST_HEADERS_TO_MASK);

      Dictionary<string, string> tblResponse = null;
      WebResponse response = null;

      try {
        response = webRequest.GetResponse();
        string contents = MediaUtilities.GetStreamContentsAsString(response.GetResponseStream());
        logEntry.LogResponse(response, false, contents, RESPONSE_FIELDS_TO_MASK,
            new KeyValueMessageFormatter());
        logEntry.Flush();

        tblResponse = ParseResponse(contents);
      } catch (WebException e) {
        string contents = "";
        response = e.Response;

        try {
          contents = MediaUtilities.GetStreamContentsAsString(response.GetResponseStream());
        } catch {
          contents = e.Message;
        }

        logEntry.LogResponse(response, true, contents, RESPONSE_FIELDS_TO_MASK,
            new KeyValueMessageFormatter());
        logEntry.Flush();

        AuthTokenException authException = ExtractException(e, contents);
        throw authException;
      } finally {
        if (response != null) {
          response.Close();
        }
      }

      if (tblResponse.ContainsKey("Auth")) {
        return (string) tblResponse["Auth"];
      } else {
        throw new AuthTokenException(AuthTokenErrorCode.Unknown, null, String.Empty, null,
            "Login failed - Could not find Auth key in response", null);
      }
    }

    /// <summary>
    /// Extracts a ClientLogin failure and wraps it into
    /// an AuthTokenException.
    /// </summary>
    /// <param name="ex">The exception originally thrown by webrequest
    /// to ClientLogin endpoint.</param>
    /// <returns></returns>
    private AuthTokenException ExtractException(WebException ex, string contents) {
      Uri url = null;
      string error = String.Empty;
      string captchaToken = String.Empty;
      Uri captchaUrl = null;
      string info = String.Empty;
      string errorMessage = CommonErrorMessages.AuthTokenLoginFailed;
      AuthTokenErrorCode errCode = AuthTokenErrorCode.Unknown;

      try {
        Dictionary<string, string> tblResponse = ParseResponse(contents);

        if (tblResponse.ContainsKey("Url")) {
          url = new Uri(tblResponse["Url"]);
        }
        if (tblResponse.ContainsKey("Error")) {
          error = tblResponse["Error"];
        }
        if (tblResponse.ContainsKey("CaptchaToken")) {
          captchaToken = tblResponse["CaptchaToken"];
        }
        if (tblResponse.ContainsKey("CaptchaUrl")) {
          captchaUrl = new Uri(CaptchaUrlPrefix + tblResponse["CaptchaUrl"]);
        }

        try {
          errCode = (AuthTokenErrorCode) Enum.Parse(typeof(AuthTokenErrorCode),
              error, true);
        } catch (ArgumentException) {
          // Enum does not have a tryParse.
        }

        if (tblResponse.ContainsKey("Info")) {
          info = tblResponse["Info"];
        }
      } catch (Exception) {
        errorMessage = CommonErrorMessages.FailedToParseAuthTokenException;
      }
      return new AuthTokenException(errCode, url, captchaToken, captchaUrl, info,
          errorMessage, ex);
    }

    /// <summary>
    /// Parses a successful ClientLogin response into a hashtable.
    /// </summary>
    /// <param name="response">The successful response from ClientLogin
    /// endpoint.</param>
    /// <returns>The parsed response, as key-value pairs, in a Hashtable.
    /// </returns>
    private static Dictionary<string, string> ParseResponse(string contents) {
      Dictionary<string, string> retVal = new Dictionary<string, string>();

      string[] splits = contents.Split('\n');
      foreach (string split in splits) {
        string[] subsplits = split.Split('=');
        if (subsplits.Length >= 2) {
          if (!string.IsNullOrEmpty(subsplits[0])) {
            if (!retVal.ContainsKey(subsplits[0])) {
              retVal.Add(subsplits[0], split.Substring(subsplits[0].Length + 1));
            }
          }
        }
      }
      return retVal;
    }
  }
}
