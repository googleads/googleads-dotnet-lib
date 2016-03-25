// Copyright 2013, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.Common.Logging;
using Google.Api.Ads.Common.Util;

using System;
using System.Collections.Generic;
using System.Net;
using System.Web;

namespace Google.Api.Ads.Common.Lib {

  /// <summary>
  /// Provides OAuth authorization mechanism for Ads services when using Web and
  /// installed application flows.
  /// </summary>
  public class OAuth2ProviderForApplications : OAuth2ProviderBase,
      AdsOAuthProviderForApplications {

    /// <summary>
    /// The feature ID for this class.
    /// </summary>
    private const AdsFeatureUsageRegistry.Features FEATURE_ID =
        AdsFeatureUsageRegistry.Features.OAuthApplicationFlow;

    /// <summary>
    /// The OAuth2 endpoint for revoking a refresh token programmatically.
    /// </summary>
    private string REVOKE_ENDPOINT {
      get {
        return OAUTH_SERVER + "/o/oauth2/revoke";
      }
    }

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
    /// Indicates if your application needs to access APIs when the user is not
    /// present at the browser. This is defaulted to true.
    /// </summary>
    private bool isOffline = true;

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
    /// Gets or sets where the url where the response is sent. This should be a
    /// registered redirect uri on the
    /// <a href="https://code.google.com/apis/console">API console</a>.
    /// </summary>
    public string RedirectUri {
      get {
        return config.OAuth2RedirectUri;
      }
      set {
        config.OAuth2RedirectUri = value;
      }
    }

    /// <summary>
    /// Initializes a new instance of the OAuth2ProviderForApplications class.
    /// </summary>
    /// <param name="config">The config.</param>
    public OAuth2ProviderForApplications(AppConfig config)
      : base(config) {
    }

    /// <summary>
    /// Builds the OAuth2 authorization url.
    /// </summary>
    /// <returns>The Authorization url that the user needs to visit to authorize
    /// the application.</returns>
    /// <exception cref="ArgumentNullException">Thrown if one of the following
    /// OAuth2 parameters are empty: Scope, ClientId</exception>
    public string GetAuthorizationUrl() {
      // Mark the usage.
      featureUsageRegistry.MarkUsage(FEATURE_ID);

      string accessType = (isOffline) ? "offline" : "online";
      string redirectUrl = (string.IsNullOrEmpty(RedirectUri)) ? OFFLINE_REDIRECT_URL : RedirectUri;

      ValidateOAuth2Parameter("Scope", Scope);
      ValidateOAuth2Parameter("ClientId", ClientId);

      return string.Format("{0}?scope={1}&state={2}&redirect_uri={3}&response_type={4}&" +
          "client_id={5}&access_type={6}", AUTH_ENDPOINT, HttpUtility.UrlEncode(Scope),
          HttpUtility.UrlEncode(State), HttpUtility.UrlEncode(redirectUrl),
          HttpUtility.UrlEncode(RESPONSE_TYPE), HttpUtility.UrlEncode(ClientId),
          HttpUtility.UrlEncode(accessType));
    }

    /// <summary>
    /// Gets the OAuth access and refresh tokens.
    /// </summary>
    /// <param name="authorizationCode">The authorization code obtained from the
    /// Authorization url after the user authorizes the application to make API
    /// calls.</param>
    /// <returns>
    /// True if the tokens were fetched successfully, false otherwise.
    /// </returns>
    /// <remarks>
    /// Refresh tokens are obtained only if access mode was set to
    /// offline.
    /// </remarks>
    /// <exception cref="ArgumentNullException">Thrown if one of the following
    /// OAuth2 parameters are empty: ClientId, ClientSecret, AuthorizationCode.
    /// </exception>
    public bool FetchAccessAndRefreshTokens(string authorizationCode) {
      // Mark the usage.
      featureUsageRegistry.MarkUsage(FEATURE_ID);

      string redirectUrl = (string.IsNullOrEmpty(RedirectUri)) ? OFFLINE_REDIRECT_URL : RedirectUri;

      if (string.IsNullOrEmpty(authorizationCode)) {
        throw new ArgumentNullException(CommonErrorMessages.OAuth2AuthorizationCodeIsEmpty);
      }

      ValidateOAuth2Parameter("ClientId", ClientId);
      ValidateOAuth2Parameter("ClientSecret", ClientSecret);

      string body = string.Format("code={0}&client_id={1}&client_secret={2}&redirect_uri={3}" +
          "&grant_type={4}", HttpUtility.UrlEncode(authorizationCode),
          HttpUtility.UrlEncode(ClientId), HttpUtility.UrlEncode(ClientSecret),
          HttpUtility.UrlEncode(redirectUrl), HttpUtility.UrlEncode("authorization_code"));

      try {
        CallTokenEndpoint(body);
      } catch (ApplicationException e) {
        throw new AdsOAuthException("Failed to get access token." + "\n" + e.Message);
      }
      return true;
    }

    /// <summary>
    /// Refreshes the access token.
    /// </summary>
    /// <remarks>This method should be used only when access mode is set to
    /// offline.</remarks>
    /// <exception cref="ArgumentNullException">Thrown if one of the following
    /// OAuth2 parameters are empty: ClientId, ClientSecret, RefreshToken.
    /// </exception>
    public void RefreshAccessTokenInOfflineMode() {
      // Mark the usage.
      featureUsageRegistry.MarkUsage(FEATURE_ID);

      ValidateOAuth2Parameter("RefreshToken", RefreshToken);
      ValidateOAuth2Parameter("ClientId", ClientId);
      ValidateOAuth2Parameter("ClientSecret", ClientSecret);

      string body = string.Format("client_id={0}&client_secret={1}&refresh_token={2}" +
          "&grant_type={3}", HttpUtility.UrlEncode(ClientId), HttpUtility.UrlEncode(ClientSecret),
          HttpUtility.UrlEncode(RefreshToken), HttpUtility.UrlEncode("refresh_token"));

      try {
        CallTokenEndpoint(body);
      } catch (ApplicationException e) {
        throw new AdsOAuthException("Failed to refresh access token." + "\n" + e.Message);
      }
    }

    /// <summary>
    /// Revokes the refresh token.
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown if one of the following
    /// OAuth2 parameters are empty: RefreshToken.</exception>
    public void RevokeRefreshToken() {
      // Mark the usage.
      featureUsageRegistry.MarkUsage(FEATURE_ID);

      ValidateOAuth2Parameter("RefreshToken", RefreshToken);

      string url = string.Format("{0}?token={1}", REVOKE_ENDPOINT, RefreshToken);

      WebRequest request = HttpUtilities.BuildRequest(url, "GET", config);

      LogEntry logEntry = new LogEntry(this.Config, new DefaultDateTimeProvider());
      logEntry.LogRequest(request, "", new HashSet<string>());

      WebResponse response = null;

      try {
        response = request.GetResponse();

        string contents = MediaUtilities.GetStreamContentsAsString(response.GetResponseStream());
        logEntry.LogResponse(response, false, contents);
        logEntry.Flush();
      } catch (WebException e) {
        string contents = HttpUtilities.GetErrorResponseBody(e);
        logEntry.LogResponse(response, true, contents);
        logEntry.Flush();

        throw new AdsOAuthException("Failed to revoke refresh token.\n" + contents, e);
      } finally {
        if (response != null) {
          response.Close();
        }
      }
    }

    /// <summary>
    /// Refreshes the access token.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown if IsOffline is false.
    /// </exception>
    /// <exception cref="ArgumentNullException">Thrown if RefreshToken is empty.
    /// </exception>
    public override void RefreshAccessToken() {
      // Mark the usage.
      featureUsageRegistry.MarkUsage(FEATURE_ID);

      ValidateOAuth2Parameter("RefreshToken", RefreshToken);
      if (!IsOffline) {
        throw new ArgumentException(CommonErrorMessages.OAuth2IsNotInOfflineMode);
      }
      RefreshAccessTokenInOfflineMode();
    }

    /// <summary>
    /// Gets the auth header.
    /// </summary>
    /// <returns>The auth header.</returns>
    public override string GetAuthHeader() {
      // Mark the usage.
      featureUsageRegistry.MarkUsage(FEATURE_ID);

      return base.GetAuthHeader();
    }

    /// <summary>
    /// Refreshes the access token if expiring.
    /// </summary>
    public override void RefreshAccessTokenIfExpiring() {
      // Mark the usage.
      featureUsageRegistry.MarkUsage(FEATURE_ID);

      base.RefreshAccessTokenIfExpiring();
    }
  }
}
