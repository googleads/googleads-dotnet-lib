// Copyright 2010, Google Inc. All Rights Reserved.
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
using System.Text;
using System.Web;

namespace com.google.api.adwords.lib {
  /// <summary>
  /// This class wraps the ClientLogin API.
  /// </summary>
  public class AuthToken {
    /// <summary>
    /// Email to be used for authentication.
    /// </summary>
    string email;

    /// <summary>
    /// Password to be used for authentication.
    /// </summary>
    string password;

    /// <summary>
    /// Url endpoint for ClientLogin API.
    /// </summary>
    const string URL = "https://www.google.com/accounts/ClientLogin";

    /// <summary>
    /// Account type to be used with ClientLogin API.
    /// </summary>
    const string ACCOUNT_TYPE = "GOOGLE";

    /// <summary>
    /// Service type to be used with ClientLogin API.
    /// </summary>
    const string SERVICE = "adwords";

    /// <summary>
    /// The source identifier string to be used with ClientLogin API.
    /// </summary>
    string SOURCE = ApplicationConfiguration.companyName + "-AWAPI DotNetLib-" +
        ApplicationConfiguration.version;

    /// <summary>
    /// Public constructor.
    /// </summary>
    /// <param name="email">Email to be used for authentication.</param>
    /// <param name="password">Password to be used for authentication</param>
    public AuthToken(string email, string password) {
      this.email = email;
      this.password = password;
    }

    /// <summary>
    /// Obtains a ClientLogin token for use with AdWords API
    /// </summary>
    /// <returns>The token string.</returns>
    /// <exception cref="AuthTokenException">If the token cannot be obtained,
    /// then an AuthTokenException is thrown with appropriate error code.
    /// </exception>
    public string GetToken() {
      WebRequest webRequest = HttpWebRequest.Create(URL);
      webRequest.Method = "POST";
      webRequest.ContentType = "application/x-www-form-urlencoded";

      string postParams =
          "accountType=" + HttpUtility.UrlEncode(ACCOUNT_TYPE) +
          "&Email=" + HttpUtility.UrlEncode(email) +
          "&Passwd=" + HttpUtility.UrlEncode(password) +
          "&service=" + HttpUtility.UrlEncode(SERVICE) +
          "&source=" + HttpUtility.UrlEncode(SOURCE);

      byte[] postBytes = Encoding.UTF8.GetBytes(postParams);

      Stream strmReq = webRequest.GetRequestStream();
      strmReq.Write(postBytes, 0, postBytes.Length);
      strmReq.Close();

      Hashtable tblResponse = null;
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
        throw new AuthTokenException(AuthTokenErrorCode.Unknown, "", "", "",
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
      Hashtable tblResponse = null;
      WebResponse response = null;

      response = ex.Response;
      tblResponse = ParseResponse(response);

      string url = "";
      string error = "";
      string captchaToken = "";
      string captchaUrl = "";

      foreach (string key in tblResponse.Keys) {
        switch (key) {
          case "Url":
            url = (string) tblResponse[key];
            break;

          case "Error":
            error = (string) tblResponse[key];
            break;

          case "CaptchaToken":
            captchaToken = (string) tblResponse[key];
            break;

          case "CaptchaUrl":
            captchaUrl = (string) tblResponse[key];
            break;
        }
      }

      AuthTokenErrorCode errCode = AuthTokenErrorCode.Unknown;

      try {
        errCode = (AuthTokenErrorCode) Enum.Parse(typeof(AuthTokenErrorCode),
            error, true);
      } catch (ArgumentException) {
        // Enum does not have a tryParse.
      }

      return new AuthTokenException(errCode, url, captchaToken, captchaUrl,
          "Login failed", ex);
    }

    /// <summary>
    /// Parses a successful ClientLogin response into a hashtable.
    /// </summary>
    /// <param name="response">The successful response from ClientLogin
    /// endpoint.</param>
    /// <returns>The parsed response, as key-value pairs, in a Hashtable.
    /// </returns>
    Hashtable ParseResponse(WebResponse response) {
      Hashtable retVal = new Hashtable();
      Stream responseStream = response.GetResponseStream();
      StreamReader reader = new StreamReader(responseStream);

      string sResponse = reader.ReadToEnd();
      string[] splits = sResponse.Split('\n');
      foreach (string split in splits) {
        string[] subsplits = split.Split('=');
        if (subsplits.Length >= 2) {
          if (!string.IsNullOrEmpty(subsplits[0])) {
            if (!retVal.ContainsKey(subsplits[0])) {
              retVal.Add(subsplits[0], split.Substring(
                  subsplits[0].Length + 1));
            }
          }
        }
      }
      return retVal;
    }
  }
}
