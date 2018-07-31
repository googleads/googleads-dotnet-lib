// Copyright 2018 Google LLC
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

using Google.Api.Ads.Common.OAuth;

using System;

namespace Google.Api.Ads.Common.Lib
{
    /// <summary>
    /// Called when an AdsOAuthProvider obtained a new access token and optionally
    /// a new refresh token from the OAuth server.
    /// </summary>
    /// <param name="provider">The provider.</param>
    public delegate void OAuthTokensObtainedCallback(AdsOAuthProvider provider);

    /// <summary>
    /// The type of OAuth2 flow.
    /// </summary>
    public enum OAuth2Flow
    {
        /// <summary>
        /// Web and installed application flows.
        /// </summary>
        APPLICATION,

        /// <summary>
        /// Service account flow.
        /// </summary>
        SERVICE_ACCOUNT
    }

    /// <summary>
    /// Provides OAuth authorization mechanism for Ads services.
    /// </summary>
    public interface AdsOAuthProvider : Configurable
    {
        /// <summary>
        /// Gets or sets the client that is making the request. This value is
        /// obtained from the <a href="https://code.google.com/apis/console">
        /// API console</a> during application registration.
        /// </summary>
        [Obsolete("Use the Config property to read or write settings.")]
        string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the client secret obtained from the
        /// <a href="https://code.google.com/apis/console">API console</a>
        /// during application registration.during application registration.
        /// </summary>
        [Obsolete("Use the Config property to read or write settings.")]
        string ClientSecret { get; set; }

        /// <summary>
        /// Gets or sets the API access your application is requesting. This is
        /// space delimited.
        /// </summary>
        [Obsolete("Use the Config property to read or write settings.")]
        string Scope { get; set; }

        /// <summary>
        /// Gets or sets a parameter that your application can use for keeping
        /// state. The OAuth Authorization Server roundtrips this parameter.
        /// </summary>
        string State { get; set; }

        /// <summary>
        /// Gets the type of token returned by the server. This field will
        /// always have the value Bearer for now.
        /// </summary>
        string TokenType { get; }

        /// <summary>
        /// Gets or sets the token that can be sent to a Google API for
        /// authentication.
        /// </summary>
        [Obsolete("Use the Config property to read or write settings.")]
        string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the time at which access token was retrieved.
        /// </summary>
        DateTime UpdatedOn { get; set; }

        /// <summary>
        /// Gets the remaining lifetime on the access token.
        /// </summary>
        int ExpiresIn { get; set; }

        /// <summary>
        /// Refreshes the access token if expiring.
        /// </summary>
        void RefreshAccessToken();

        /// <summary>
        /// Refreshes the access token if expiring.
        /// </summary>
        void RefreshAccessTokenIfExpiring();

        /// <summary>
        /// Gets the OAuth authorization header to be used with HTTP requests.
        /// </summary>
        /// <returns>The authorization header.</returns>
        string GetAuthHeader();

        /// <summary>
        /// Callback triggered when this provider obtains a new access token or
        /// refresh token from the OAuth server.
        /// </summary>
        OAuthTokensObtainedCallback OnOAuthTokensObtained { get; }
    }

    /// <summary>
    /// Provides OAuth authorization mechanism for Ads services when using Web and
    /// Installed application flows.
    /// </summary>
    public interface AdsOAuthProviderForApplications : AdsOAuthProvider
    {
        /// <summary>
        /// Indicates if your application needs to access APIs when the user is not
        /// present at the browser. This is defaulted to true.
        /// </summary>
        bool IsOffline { get; set; }

        /// <summary>
        /// Gets or sets where the url where the response is sent. This should be a
        /// registered redirect uri on the
        /// <a href="https://code.google.com/apis/console">API console</a>.
        /// </summary>
        [Obsolete("Use the Config property to read or write settings.")]
        string RedirectUri { get; set; }

        /// <summary>
        /// Gets or sets a token that may be used to obtain a new access token.
        /// Refresh tokens are valid until the user revokes access.
        /// </summary>
        [Obsolete("Use the Config property to read or write settings.")]
        string RefreshToken { get; set; }

        /// <summary>
        /// Gets the authorization URL.
        /// </summary>
        /// <returns>The authorization url.</returns>
        string GetAuthorizationUrl();

        /// <summary>
        /// Fetches the access and optionally the refresh token if applicable.
        /// </summary>
        /// <param name="authorizationCode">The authorization code returned by
        /// OAuth server.</param>
        /// <returns>True if the tokens were fetched successfully, false otherwise.
        /// </returns>
        bool FetchAccessAndRefreshTokens(string authorizationCode);

        /// <summary>
        /// Refreshes the access token if offline mode is used.
        /// </summary>
        void RefreshAccessTokenInOfflineMode();

        /// <summary>
        /// Revokes the refresh token if offline mode is used.
        /// </summary>
        void RevokeRefreshToken();
    }

    /// <summary>
    /// Provides OAuth authorization mechanism for Ads services when using service
    /// account flow.
    /// </summary>
    public interface AdsOAuthProviderForServiceAccounts : AdsOAuthProvider
    {
        /// <summary>
        /// Gets the service account email for which access token should be
        /// retrieved.
        /// </summary>
        [Obsolete("Use the Config property to read or write settings.")]
        string ServiceAccountEmail { get; }

        /// <summary>
        /// Gets or sets the email of the account for which the call is being made.
        /// </summary>
        [Obsolete("Use the Config property to read or write settings.")]
        string PrnEmail { get; set; }

        /// <summary>
        /// Generates the access token for service account.
        /// </summary>
        void GenerateAccessTokenForServiceAccount();
    }

    /// <summary>
    /// Legacy base class for OAuth provider. Maintained for backward
    /// compatibility purposes.
    /// </summary>
    public abstract class OAuth2ProviderBase : AdsOAuthProviderImpl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OAuth2ProviderBase"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public OAuth2ProviderBase(AppConfig config) : base(config)
        {
        }
    }

    /// <summary>
    /// Legacy base class for OAuth provider. Maintained for backward
    /// compatibility purposes.
    /// </summary>
    public class OAuth2ProviderForApplications : OAuth2ProviderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OAuth2ProviderForApplications"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public OAuth2ProviderForApplications(AppConfig config) : base(config)
        {
        }
    }

    /// <summary>
    /// Legacy base class for OAuth provider. Maintained for backward
    /// compatibility purposes.
    /// </summary>
    public class OAuth2ProviderForServiceAccounts : OAuth2ProviderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OAuth2ProviderForServiceAccounts"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public OAuth2ProviderForServiceAccounts(AppConfig config) : base(config)
        {
        }
    }
}
