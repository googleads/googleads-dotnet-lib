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
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;

namespace Google.Api.Ads.Common.Lib {
  /// <summary>
  /// This class wraps the ClientLogin API. See
  /// http://code.google.com/apis/accounts/docs/AuthForInstalledApps.html
  /// for details.
  /// </summary>
  public class AuthToken {
    /// <summary>
    /// Url endpoint for ClientLogin API.
    /// </summary>
    private readonly Uri URL = new Uri("https://www.google.com/accounts/ClientLogin");
    //private readonly Uri URL = new Uri("https://www.google.com/accounts/ClientLogin");

    /// <summary>
    /// The prefix to be appended for captcha urls.
    /// </summary>
    private readonly string CaptchaUrlPrefix = "http://www.google.com/accounts/";

    /// <summary>
    /// Account type to be used with ClientLogin API.
    /// </summary>
    private const string ACCOUNT_TYPE = "GOOGLE";

    /// <summary>
    /// The source identifier string to be used with ClientLogin API.
    /// </summary>
    private readonly string SOURCE;

    /// <summary>
    /// Email to be used for authentication.
    /// </summary>
    private string email;

    /// <summary>
    /// Password to be used for authentication.
    /// </summary>
    private string password;

    /// <summary>
    /// Service type to be used with ClientLogin API.
    /// </summary>
    private string service;

    /// <summary>
    /// The HTTP web proxy to be used when making HTTP requests.
    /// </summary>
    private WebProxy proxy;

    /// <summary>
    /// Timeout in milliseconds for the HTTP web connection.
    /// </summary>
    private int timeout;

    /// <summary>
    /// Public constructor.
    /// </summary>
    /// <param name="email">Email to be used for authentication.</param>
    /// <param name="password">Password to be used for authentication</param>
    /// <param name="config">The configuration object for use with this object.
    /// </param>
    /// <param name="service">The gaia service name for which tokens are being
    /// generated.</param>
    public AuthToken(AppConfigBase config, string service, string email, string password)
        : this(config, service, email, password, config.Proxy, config.Timeout) {
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
    /// <param name="proxy">The HTTP web proxy for making HTTP calls.</param>
    public AuthToken(AppConfigBase config, string service, string email, string password,
        WebProxy proxy) : this(config, service, email, password, proxy, config.Timeout) {
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
    /// <param name="proxy">The HTTP web proxy for making HTTP calls.</param>
    /// <param name="timeout">Timeout in milliseconds for the web connection.
    /// </param>
    public AuthToken(AppConfigBase config, string service, string email, string password,
        WebProxy proxy, int timeout) {
      if (config == null) {
        throw new ArgumentNullException("config");
      }
      this.email = email;
      this.password = password;
      this.service = service;
      this.SOURCE = string.Format(CultureInfo.InvariantCulture, "Google-{0}", config.Signature);
      this.proxy = proxy;
      this.timeout = timeout;
    }

    /// <summary>
    /// Obtains a ClientLogin token for use with various Ads APIs.
    /// </summary>
    /// <returns>The token string.</returns>
    /// <exception cref="AuthTokenException">If the token cannot be obtained,
    /// then an AuthTokenException is thrown with appropriate error code.
    /// </exception>
    public string GetToken() {
      WebRequest webRequest = HttpWebRequest.Create(URL);
      webRequest.Method = "POST";
      webRequest.ContentType = "application/x-www-form-urlencoded";

      if (this.proxy != null) {
        webRequest.Proxy = proxy;
      }
      webRequest.Timeout = timeout;

      string postParams =
          "accountType=" + HttpUtility.UrlEncode(ACCOUNT_TYPE) +
          "&Email=" + HttpUtility.UrlEncode(email) +
          "&Passwd=" + HttpUtility.UrlEncode(password) +
          "&service=" + HttpUtility.UrlEncode(service) +
          "&source=" + HttpUtility.UrlEncode(SOURCE);

      byte[] postBytes = Encoding.UTF8.GetBytes(postParams);
      webRequest.ContentLength = postBytes.Length;

      using (Stream strmReq = webRequest.GetRequestStream()) {
        strmReq.Write(postBytes, 0, postBytes.Length);
      }

      Dictionary<string, string> tblResponse = null;
      WebResponse response = null;

      try {
        response = webRequest.GetResponse();
      } catch (WebException ex) {
        AuthTokenException authException = ExtractException(ex);
        throw authException;
      }

      tblResponse = ParseResponse(response);

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
    private AuthTokenException ExtractException(WebException ex) {
      Dictionary<string, string> tblResponse = null;

      using (WebResponse response = ex.Response) {
        tblResponse = ParseResponse(response);
      }

      Uri url = tblResponse.ContainsKey("Url")? new Uri(tblResponse["Url"]) : null;
      string error = tblResponse.ContainsKey("Error") ? tblResponse["Error"] : String.Empty;
      string captchaToken = tblResponse.ContainsKey("CaptchaToken") ?
          tblResponse["CaptchaToken"] : String.Empty;
      Uri captchaUrl = tblResponse.ContainsKey("CaptchaUrl") ?
          new Uri(CaptchaUrlPrefix + tblResponse["CaptchaUrl"]) : null;

      AuthTokenErrorCode errCode = AuthTokenErrorCode.Unknown;

      try {
        errCode = (AuthTokenErrorCode) Enum.Parse(typeof(AuthTokenErrorCode),
            error, true);
      } catch (ArgumentException) {
        // Enum does not have a tryParse.
      }

      string info = tblResponse.ContainsKey("Info") ? tblResponse["Info"] : String.Empty;

      return new AuthTokenException(errCode, url, captchaToken, captchaUrl, info,
          CommonErrorMessages.AuthTokenLoginFailed, ex);
    }

    /// <summary>
    /// Parses a successful ClientLogin response into a hashtable.
    /// </summary>
    /// <param name="response">The successful response from ClientLogin
    /// endpoint.</param>
    /// <returns>The parsed response, as key-value pairs, in a Hashtable.
    /// </returns>
    private static Dictionary<string, string> ParseResponse(WebResponse response) {
      Dictionary<string, string> retVal = new Dictionary<string, string>();

      using (StreamReader reader = new StreamReader(response.GetResponseStream())) {
        string sResponse = reader.ReadToEnd();
        string[] splits = sResponse.Split('\n');
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
      }
      return retVal;
    }
  }
}
