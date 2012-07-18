// Copyright 2012, Google Inc. All Rights Reserved.
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

using System.Collections.Generic;
using System.Net;
using System.Web;
using System.IO;
using Google.Api.Ads.Common.Util;
using System.Text;

namespace Google.Api.Ads.Common.OAuth.Lib {
  /// <summary>
  /// Implements OAuth2 protocol, draft 10, web server flow for authenticating
  /// users. See http://code.google.com/apis/accounts/docs/OAuth2WebServer.html
  /// for details.
  /// </summary>
  public class OAuth2Provider : OAuth2 {
    /// <summary>
    /// The OAuth2 endpoint for obtaining an authorization token.
    /// </summary>
    private string AUTH_ENDPOINT = "https://accounts.google.com/o/oauth2/auth";

    /// <summary>
    /// The OAuth2 endpoint for obtaining or refreshing an access token.
    /// </summary>
    private string TOKEN_ENDPOINT = "https://accounts.google.com/o/oauth2/token";

    /// <summary>
    /// The OAuth2 endpoint for revoking a refresh token programmatically.
    /// </summary>
    private string REVOKE_ENDPOINT = "https://accounts.google.com/o/oauth2/revoke";

    /// <summary>
    /// The OAuth2 redirect url to be used if your application is a desktop
    /// application. To use this url, your client should be registered as an
    /// installed application on the
    /// <a href="https://code.google.com/apis/console">API console</a>.
    /// </summary>
    public const string OFFLINE_REDIRECT_URL = "urn:ietf:wg:oauth:2.0:oob";

    /// <summary>
    /// Determines if the Google OAuth 2.0 endpoint returns an authorization
    /// code.
    /// </summary>
    private const string RESPONSE_TYPE = "code";

    /// <summary>
    /// Indicates the client that is making the request. This value is obtained
    /// from the <a href="https://code.google.com/apis/console">API console</a>
    /// during application registration.
    /// </summary>
    private string clientId = "";

    /// <summary>
    /// Determines where the response is sent. This should be a registered
    /// redirect uri on the <a href="https://code.google.com/apis/console">
    /// API console</a>.
    /// </summary>
    string redirectUri = "";

    /// <summary>
    /// Indicates the API access your application is requesting. This is space
    /// delimited.
    /// </summary>
    string scope;

    /// <summary>
    /// A parameter that your application can use for keeping state. The
    /// OAuth Authorization Server roundtrips this parameter.
    /// </summary>
    string state;

    /// <summary>
    /// Indicates if your application needs to access APIs when the user is not
    /// present at the browser. This is defaulted to true.
    /// </summary>
    bool isOffline = true;

    /// <summary>
    /// The client secret obtained from the
    /// <a href="https://code.google.com/apis/console">API console</a>
    /// during application registration.during application registration.
    /// </summary>
    private string clientSecret = "";

    /// <summary>
    /// Indicates the type of token returned by the server. This field will
    /// always have the value Bearer for now.
    /// </summary>
    string tokenType;

    /// <summary>
    /// The remaining lifetime on the access token.
    /// </summary>
    private int expiresIn;

    /// <summary>
    /// The token that can be sent to a Google API for authentication.
    /// </summary>
    string accessToken = "";

    /// <summary>
    /// A token that may be used to obtain a new access token. Refresh tokens
    /// are valid until the user revokes access.
    /// </summary>
    string refreshToken = "";

    /// <summary>
    /// The configuration object to be used with this client.
    /// </summary>
    AppConfigBase config = null;

    /// <summary>
    /// Gets or sets the client that is making the request. This value is
    /// obtained from the <a href="https://code.google.com/apis/console">
    /// API console</a> during application registration.
    /// </summary>
    public string ClientId {
      get {
        return clientId;
      }
      set {
        clientId = value;
      }
    }

    /// <summary>
    /// Gets or sets where the url where the response is sent. This should be a
    /// registered redirect uri on the
    /// <a href="https://code.google.com/apis/console">API console</a>.
    /// </summary>
    public string RedirectUri {
      get {
        return redirectUri;
      }
      set {
        redirectUri = value;
      }
    }

    /// <summary>
    /// Gets or sets the API access your application is requesting. This is
    /// space delimited.
    /// </summary>
    public string Scope {
      get {
        return scope;
      }
      set {
        scope = value;
      }
    }

    /// <summary>
    /// Gets or sets a parameter that your application can use for keeping
    /// state. The OAuth Authorization Server roundtrips this parameter.
    /// </summary>
    public string State {
      get {
        return state;
      }
      set {
        state = value;
      }
    }

    /// <summary>
    /// Indicates if your application needs to access APIs when the user is not
    /// present at the browser. This is defaulted to true.
    /// </summary>
    public bool IsOffline {
      get {
        return isOffline;
      }
      set {
        isOffline = value;
      }
    }

    /// <summary>
    /// Gets or sets the client secret obtained from the
    /// <a href="https://code.google.com/apis/console">API console</a>
    /// during application registration.during application registration.
    /// </summary>
    public string ClientSecret {
      get {
        return clientSecret;
      }
      set {
        clientSecret = value;
      }
    }

    /// <summary>
    /// Gets the type of token returned by the server. This field will
    /// always have the value Bearer for now.
    /// </summary>
    public string TokenType {
      get {
        return tokenType;
      }
    }

    /// <summary>
    /// Gets the remaining lifetime on the access token.
    /// </summary>
    public int ExpiresIn {
      get {
        return expiresIn;
      }
    }

    /// <summary>
    /// Gets or sets the token that can be sent to a Google API for
    /// authentication.
    /// </summary>
    public string AccessToken {
      get {
        return accessToken;
      }
      set {
        accessToken = value;
      }
    }

    /// <summary>
    /// Gets or sets a token that may be used to obtain a new access token.
    /// Refresh tokens are valid until the user revokes access.
    /// </summary>
    public string RefreshToken {
      get {
        return refreshToken;
      }
      set {
        refreshToken = value;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OAuth2"/> class.
    /// </summary>
    /// <param name="config">The config.</param>
    public OAuth2Provider(AppConfigBase config) {
      this.config = config;
      this.clientId = config.OAuth2ClientId;
      this.clientSecret = config.OAuth2ClientSecret;
      this.accessToken = config.OAuth2AccessToken;
      this.refreshToken = config.OAuth2RefreshToken;
      this.scope = config.OAuth2Scope;
      this.redirectUri = config.OAuth2RedirectUri;
    }

    #region OAuth2 methods

    /// <summary>
    /// Builds the OAuth2 authorization url.
    /// </summary>
    /// <returns>The Authorization url that the user needs to visit to authorize
    /// the application.</returns>
    public string GetAuthorizationUrl() {
      string accessType = (isOffline) ? "offline" : "online";
      string redirectUrl = (string.IsNullOrEmpty(redirectUri)) ? OFFLINE_REDIRECT_URL : redirectUri;

      return string.Format("{0}?scope={1}&state={2}&redirect_uri={3}&response_type={4}&" +
          "client_id={5}&access_type={6}", AUTH_ENDPOINT, HttpUtility.UrlEncode(scope),
          HttpUtility.UrlEncode(state), HttpUtility.UrlEncode(redirectUrl),
          HttpUtility.UrlEncode(RESPONSE_TYPE), HttpUtility.UrlEncode(clientId),
          HttpUtility.UrlEncode(accessType));
    }

    /// <summary>
    /// Gets the OAuth access and refresh tokens.
    /// </summary>
    /// <param name="authorizationCode">The authorization code obtained from the
    /// Authorization url after the user authorizes the application to make API
    /// calls.</param>
    /// <remarks>Refresh tokens are obtained only if access mode was set to
    /// offline.</remarks>
    public bool FetchAccessAndRefreshTokens(string authorizationCode) {
      string redirectUrl = (string.IsNullOrEmpty(redirectUri)) ? OFFLINE_REDIRECT_URL : redirectUri;

      WebRequest request = HttpWebRequest.Create(TOKEN_ENDPOINT);
      request.Method = "POST";
      request.ContentType = "application/x-www-form-urlencoded";

      using (StreamWriter writer = new StreamWriter(request.GetRequestStream())) {
        writer.Write(string.Format("code={0}&client_id={1}&client_secret={2}&redirect_uri={3}" +
            "&grant_type={4}", HttpUtility.UrlEncode(authorizationCode),
            HttpUtility.UrlEncode(clientId), HttpUtility.UrlEncode(clientSecret),
            HttpUtility.UrlEncode(redirectUrl), HttpUtility.UrlEncode("authorization_code")));
      }

      try {
        WebResponse response = request.GetResponse();
        MemoryStream memStream = new MemoryStream();
        MediaUtilities.CopyStream(response.GetResponseStream(), memStream);
        string contents = Encoding.UTF8.GetString(memStream.ToArray());
        Dictionary<string, string> values = ParseJsonObjectResponse(contents);
        if (values.ContainsKey("access_token")) {
          this.accessToken = values["access_token"];
        }
        if (values.ContainsKey("refresh_token")) {
          this.refreshToken = values["refresh_token"];
        }
        if (values.ContainsKey("token_type")) {
          this.tokenType = values["token_type"];
        }
        if (values.ContainsKey("expires_in")) {
          this.expiresIn = int.Parse(values["expires_in"]);
        }

        if (this.OnOAuthTokensObtained != null) {
          this.OnOAuthTokensObtained(this);
        }

        return true;
      } catch (WebException ex) {
        throw new AdsOAuthException("Failed to get access token.\n" +
            GetResponseText(ex.Response));
      }
    }

    /// <summary>
    /// Refreshes the access token.
    /// </summary>
    /// <remarks>This method should be used only when access mode is set to
    /// offline.</remarks>
    public void RefreshAccessToken() {
      WebRequest request = HttpWebRequest.Create(TOKEN_ENDPOINT);
      request.Method = "POST";
      request.ContentType = "application/x-www-form-urlencoded";

      using (StreamWriter writer = new StreamWriter(request.GetRequestStream())) {
        writer.Write(string.Format("client_id={0}&client_secret={1}&refresh_token={2}" +
            "&grant_type={3}", HttpUtility.UrlEncode(clientId), HttpUtility.UrlEncode(clientSecret),
            HttpUtility.UrlEncode(refreshToken), HttpUtility.UrlEncode("refresh_token")));
      }

      try {
        WebResponse response = request.GetResponse();
        string contents = GetResponseText(response);
        Dictionary<string, string> values = ParseJsonObjectResponse(contents);
        if (values.ContainsKey("access_token")) {
          this.accessToken = values["access_token"];
        }
        if (values.ContainsKey("refresh_token")) {
          this.refreshToken = values["refresh_token"];
        }
        if (values.ContainsKey("token_type")) {
          this.tokenType = values["token_type"];
        }
        if (values.ContainsKey("expires_in")) {
          this.expiresIn = int.Parse(values["expires_in"]);
        }

        if (this.OnOAuthTokensObtained != null) {
          this.OnOAuthTokensObtained(this);
        }
      } catch (WebException ex) {
        throw new AdsOAuthException("Failed to refresh access token.\n" +
            GetResponseText(ex.Response));
      }
    }

    /// <summary>
    /// Revokes the refresh token.
    /// </summary>
    public void RevokeRefreshToken() {
      string url = string.Format("{0}?token={1}", REVOKE_ENDPOINT, refreshToken);
      WebRequest request = HttpWebRequest.Create(url);
      try {
        WebResponse response = request.GetResponse();
      } catch (WebException ex) {
        throw new AdsOAuthException("Failed to revoke refresh token.\n" +
            GetResponseText(ex.Response));
      }
    }

    /// <summary>
    /// Gets the auth header.
    /// </summary>
    /// <param name="url">The url for which auth header is to be generated.
    /// </param>
    /// <returns>The auth header.</returns>
    public string GetAuthHeader(string url) {
      return "Bearer " + this.AccessToken;
    }

    #endregion

    /// <summary>
    /// Gets the response text from a web response.
    /// </summary>
    /// <param name="response">The web response.</param>
    /// <returns>The web response contents.</returns>
    private static string GetResponseText(WebResponse response) {
      MemoryStream memStream = new MemoryStream();
      MediaUtilities.CopyStream(response.GetResponseStream(), memStream);
      return Encoding.UTF8.GetString(memStream.ToArray());
    }

    /// <summary>
    /// Parses a json object response.
    /// </summary>
    /// <param name="contents">The json contents.</param>
    /// <returns>A dictionary of key-value pairs.</returns>
    /// <remarks>This is not a full-blown json parser, it can handle only cases
    /// where the response is a json object without nested objects or arrays.
    /// </remarks>
    private Dictionary<string, string> ParseJsonObjectResponse(string contents) {
      Dictionary<string, string> retval = new Dictionary<string, string>();
      string[] splits = contents.Trim(new char[] {' ', '{', '}'}).Split(',');
      foreach (string split in splits) {
        string[] subSplits = split.Trim(new char[] {' ', '\n'}).Split(':');
        if (subSplits.Length == 2) {
          retval.Add(subSplits[0].Trim(new char[] {'"', ' '}),
              subSplits[1].Trim(new char[] {'"', ' '}));
        }
      }
      return retval;
    }

    #region AdsOAuthProvider

    /// <summary>
    /// Callback triggered when this provider obtains a new access token or
    /// refresh token from the OAuth server.
    /// </summary>
    OAuthTokensObtainedCallback oAuthTokensObtained =
        new OAuthTokensObtainedCallback(TokensUpdatedCallback);

    /// <summary>
    /// Callback triggered when this provider obtains a new access token or
    /// refresh token from the OAuth server.
    /// </summary>
    public OAuthTokensObtainedCallback OnOAuthTokensObtained {
      get {
        return oAuthTokensObtained;
      }
      set {
        oAuthTokensObtained = value;
      }
    }

    /// <summary>
    /// Default callback when this provider obtains a new access token or
    /// refresh token from the OAuth server.
    /// </summary>
    /// <param name="provider">The provider.</param>
    private static void TokensUpdatedCallback(AdsOAuthProvider provider) {
    }

    #endregion
  }
}