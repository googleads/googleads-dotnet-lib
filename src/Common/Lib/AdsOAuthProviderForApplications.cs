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

using System;

namespace Google.Api.Ads.Common.Lib {
  /// <summary>
  /// Provides OAuth authorization mechanism for Ads services when using Web and
  /// Installed application flows.
  /// </summary>
  public interface AdsOAuthProviderForApplications : AdsOAuthProvider {
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
    string RedirectUri { get; set; }

    /// <summary>
    /// Gets or sets a token that may be used to obtain a new access token.
    /// Refresh tokens are valid until the user revokes access.
    /// </summary>
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
}
