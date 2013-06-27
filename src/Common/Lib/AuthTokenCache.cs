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
  /// Defines a cache for storing auth tokens.
  /// </summary>
  [Obsolete("ClientLogin API is deprecated, see " +
      "https://developers.google.com/accounts/docs/AuthForInstalledApps for details. OAuth2 is " +
      "the recommended authentication mechanism. You can refer to the code examples for details " +
      " on how to use OAUth2 in your application. You can use Util\\OAuth2TokenGenerator.cs for " +
      "generating OAuth2 refresh tokens for offline access to various Ads* APIs.")]
  public interface AuthTokenCache {
    /// <summary>
    /// Adds an auth token to cache.
    /// </summary>
    /// <param name="service">The ClientLogin service for which this auth token
    /// is generated.</param>
    /// <param name="email">The login email.</param>
    /// <param name="password">The login password.</param>
    /// <param name="token">The auth token.</param>
    /// <returns>The auth token.</returns>
    string AddToken(string service, string email, string password, string token);

    /// <summary>
    /// Gets an auth token from cache.
    /// </summary>
    /// <param name="service">The ClientLogin service for which this auth token
    /// is generated.</param>
    /// <param name="email">The login email.</param>
    /// <param name="password">The login password.</param>
    /// <returns>The auth token, or null if the cache doesn't have a token.
    /// </returns>
    string GetToken(string service, string email, string password);

    /// <summary>
    /// Invalidates an auth token.
    /// </summary>
    /// <param name="token">The auth token.</param>
    void InvalidateToken(string token);

    /// <summary>
    /// Clears the cache.
    /// </summary>
    void Clear();
  }
}
