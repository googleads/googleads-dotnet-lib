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
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.Common.Lib {
  /// <summary>
  /// Called when an AdsOAuthProvider obtained a new access token and optionally
  /// a new refresh token from the OAuth server.
  /// </summary>
  /// <param name="provider">The provider.</param>
  public delegate void OAuthTokensObtainedCallback(AdsOAuthProvider provider);

  /// <summary>
  /// Provides OAuth authorization mechanism for Ads services.
  /// </summary>
  public interface AdsOAuthProvider {
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
    /// Gets the OAuth authorization header to be used with HTTP requests.
    /// </summary>
    /// <param name="protectedUrl">The protected url for which OAuth headers
    /// are to be generated.</param>
    /// <returns>The authorization header.</returns>
    string GetAuthHeader(string protectedUrl);

    /// <summary>
    /// Callback triggered when this provider obtains a new access token or
    /// refresh token from the OAuth server.
    /// </summary>
    OAuthTokensObtainedCallback OnOAuthTokensObtained {
      get;
    }
  }
}
