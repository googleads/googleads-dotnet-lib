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

using System;

namespace Google.Api.Ads.Common.Lib {
  /// <summary>
  /// Interface to be implemented by OAuth1a protocol handlers.
  /// </summary>
  public interface OAuth2 : AdsOAuthProvider {
    /// <summary>
    /// Gets or sets a value indicating whether OAuth2 request mode is offline.
    /// </summary>
    bool IsOffline {
      get;
      set;
    }

    /// <summary>
    /// Refreshes the access token if offline mode is used..
    /// </summary>
    void RefreshAccessToken();

    /// <summary>
    /// Generates the access token for service account.
    /// </summary>
    void GenerateAccessTokenForServiceAccount();

    /// <summary>
    /// Revokes the refresh token if offline mode is used.
    /// </summary>
    void RevokeRefreshToken();
  }
}
