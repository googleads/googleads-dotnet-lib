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
  /// The type of OAuth2 flow.
  /// </summary>
  public enum OAuth2Flow {
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
  public interface AdsOAuthProvider : Configurable {
    /// <summary>
    /// Gets or sets the client that is making the request. This value is
    /// obtained from the <a href="https://code.google.com/apis/console">
    /// API console</a> during application registration.
    /// </summary>
    string ClientId { get; set; }

    /// <summary>
    /// Gets or sets the client secret obtained from the
    /// <a href="https://code.google.com/apis/console">API console</a>
    /// during application registration.during application registration.
    /// </summary>
    string ClientSecret { get; set; }

    /// <summary>
    /// Gets or sets the API access your application is requesting. This is
    /// space delimited.
    /// </summary>
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
    OAuthTokensObtainedCallback OnOAuthTokensObtained {
      get;
    }
  }
}
