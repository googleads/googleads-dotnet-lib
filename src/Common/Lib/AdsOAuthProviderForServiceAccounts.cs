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
  /// Provides OAuth authorization mechanism for Ads services when using service
  /// account flow.
  /// </summary>
  public interface AdsOAuthProviderForServiceAccounts : AdsOAuthProvider {
    /// <summary>
    /// Gets or sets the service account email for which access token should be
    /// retrieved..
    /// </summary>
    string ServiceAccountEmail { get; set; }

    /// <summary>
    /// Gets or sets the email of the account for which the call is being made.
    /// </summary>
    string PrnEmail { get; set; }

    /// <summary>
    /// Gets or sets the JWT certificate path.
    /// </summary>
    string JwtCertificatePath { get; set; }

    /// <summary>
    /// Gets or sets the JWT certificate password.
    /// </summary>
    string JwtCertificatePassword { get; set; }

    /// <summary>
    /// Generates the access token for service account.
    /// </summary>
    void GenerateAccessTokenForServiceAccount();
  }
}
