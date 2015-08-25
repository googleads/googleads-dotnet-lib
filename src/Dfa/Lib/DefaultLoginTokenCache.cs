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

using Google.Api.Ads.Common.Lib;

using System;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.Dfa.Lib {
  /// <summary>
  /// Default implementation of login token cache.
  /// </summary>
  public class DefaultLoginTokenCache : LoginTokenCache {
    /// <summary>
    /// An internal dictionary for caching login tokens.
    /// </summary>
    InMemoryTokenCache<UserToken> tokenCache = new InMemoryTokenCache<UserToken>();

    #region LoginTokenCache Members

    /// <summary>
    /// Adds a login token to cache.
    /// </summary>
    /// <param name="username">The DFA user name.</param>
    /// <param name="password">The DFA user password.</param>
    /// <param name="token">The login token.</param>
    /// <returns>
    /// The login token.
    /// </returns>
    public UserToken AddToken(string username, UserToken token) {
      ValidateParams(username);
      return tokenCache.AddToken(username, token);
    }

    /// <summary>
    /// Gets a login token from cache.
    /// </summary>
    /// <param name="username">The DFA user name.</param>
    /// <returns>
    /// The login token, or null if the cache doesn't have a token.
    /// </returns>
    public UserToken GetToken(string username) {
      ValidateParams(username);
      return tokenCache.GetToken(username);
    }

    /// <summary>
    /// Invalidates a login token.
    /// </summary>
    /// <param name="token">The login token.</param>
    public void InvalidateToken(UserToken token) {
      tokenCache.InvalidateToken(token);
    }

    /// <summary>
    /// Clears the cache.
    /// </summary>
    public void Clear() {
      tokenCache.Clear();
    }

    #endregion

    /// <summary>
    /// Validates the input params.
    /// </summary>
    /// <param name="username">The DFA user name.</param>
    private static void ValidateParams(string username) {
      if (string.IsNullOrEmpty(username)) {
        throw new ArgumentException(DfaErrorMessages.UserNameCannotBeEmpty);
      }
    }
  }
}
