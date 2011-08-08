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
  /// Provides OAuth authorization mechanism for Ads services.
  /// </summary>
  public interface AdsOAuthProvider {
    /// <summary>
    /// Generates the OAuth access token.
    /// </summary>
    void GenerateAccessToken();

    /// <summary>
    /// Gets the AuthorizationHeader value to be set on outgoing HTTP calls.
    /// </summary>
    /// <param name="apiCallUrl">The url to which API call is being made.
    /// </param>
    /// <returns>Gets the AuthorizationHeader value to be set on outgoing HTTP
    /// calls.</returns>
    string GetAuthHeader(string apiCallUrl);
  }
}
