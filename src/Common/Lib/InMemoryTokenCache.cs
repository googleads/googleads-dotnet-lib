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

// Author: api.anash@gmail.com (Anash P. Oommen)

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Google.Api.Ads.Common.Lib {
  /// <summary>
  /// Implements an in-memory cache for holding tokens.
  /// </summary>
  public class InMemoryTokenCache<T> where T: class {
    /// <summary>
    /// An internal dictionary for caching tokens.
    /// </summary>
    Dictionary<string, T> tokenMap = new Dictionary<string, T>();

    /// <summary>
    /// Adds a token to cache.
    /// </summary>
    /// <param name="signature">The token signature, used as a key.</param>
    /// <param name="token">The token to be cached.</param>
    /// <returns>
    /// The cached token.
    /// </returns>
    public T AddToken(string signature, T token) {
      ValidateParams(signature);
      lock (tokenMap) {
        tokenMap[signature] = token;
        return token;
      }
    }

    /// <summary>
    /// Gets a token from cache.
    /// </summary>
    /// <param name="signature">The token signature, used as a key.</param>
    /// <returns>
    /// The cached token, or null if the cache doesn't have a token.
    /// </returns>
    public T GetToken(string signature) {
      ValidateParams(signature);
      lock (tokenMap) {
        return tokenMap.ContainsKey(signature) ? tokenMap[signature] : default(T);
      }
    }

    /// <summary>
    /// Invalidates a cached token.
    /// </summary>
    /// <param name="token">The cached token.</param>
    public void InvalidateToken(T token) {
      lock (tokenMap) {
        foreach (string key in tokenMap.Keys) {
          if (tokenMap[key] == token) {
            tokenMap.Remove(key);
            break;
          }
        }
      }
    }

    /// <summary>
    /// Clears the cache.
    /// </summary>
    public void Clear() {
      lock (tokenMap) {
        tokenMap.Clear();
      }
    }

    /// <summary>
    /// Validates the input params.
    /// </summary>
    /// <param name="signature">The token signature, used as a key.</param>
    private static void ValidateParams(string signature) {
      if (string.IsNullOrEmpty(signature)) {
        throw new ArgumentException(CommonErrorMessages.SignatureCannotBeNull);
      }
    }
  }
}
