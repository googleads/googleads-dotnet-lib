// Copyright 2014, Google Inc. All Rights Reserved.
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

// Author: Chris Seeley  (https://github.com/Narwalter)

using System;

namespace Google.Api.Ads.Common.Lib {

  /// <summary>
  /// Builder utility for OAuth2 Service Account claimsets.
  /// </summary>
  public class OAuth2JwtClaimsetBuilder {

    /// <summary>
    /// Gets or sets the scope for the claimset.
    /// </summary>
    public string scope { get; set; }

    /// <summary>
    /// Gets or sets the audience for the claimset.
    /// </summary>
    public string audience { get; set; }

    /// <summary>
    /// Gets or sets the service account email for the claimset.
    /// </summary>
    public string serviceAccountEmail { get; set; }

    /// <summary>
    /// Gets or sets the email of the user for the claimset.
    /// </summary>
    public string impersonationEmail { get; set; }

    /// <summary>
    /// Gets or sets the timestamp for the claimset.
    /// </summary>
    public long timestamp { get; set; }

    /// <summary>
    /// Gets or sets the expiry for the claimset.
    /// </summary>
    public long expiry { get; set; }

    /// <summary>
    /// Sets the scope for the claimset.
    /// </summary>
    /// <param name="scope">The scope for the claimset</param>
    /// <returns>This builder, for chaining method calls</returns>
    public OAuth2JwtClaimsetBuilder WithScope(string scope) {
      this.scope = scope;
      return this;
    }

    /// <summary>
    /// Sets the audience for the claimset.
    /// </summary>
    /// <param name="audience">The audience for the claimset</param>
    /// <returns>This builder, for chaining method calls</returns>
    public OAuth2JwtClaimsetBuilder WithAudience(string audience) {
      this.audience = audience;
      return this;
    }

    /// <summary>
    /// Sets the service account email for the claimset.
    /// </summary>
    /// <param name="serviceAccountEmail">The service account email for the claimset</param>
    /// <returns>This builder, for chaining method calls</returns>
    public OAuth2JwtClaimsetBuilder WithServiceAccountEmail(string serviceAccountEmail) {
      this.serviceAccountEmail = serviceAccountEmail;
      return this;
    }

    /// <summary>
    /// Sets the email of the user for the claimset.
    /// </summary>
    /// <param name="impersonationEmail">The email of the user for the claimset</param>
    /// <returns>This builder, for chaining method calls</returns>
    public OAuth2JwtClaimsetBuilder WithImpersonationEmail(string impersonationEmail) {
      this.impersonationEmail = impersonationEmail;
      return this;
    }

    /// <summary>
    /// Sets the timestamp for the claimset.
    /// </summary>
    /// <param name="timestamp">The timestamp for the claimset</param>
    /// <returns>This builder, for chaining method calls</returns>
    public OAuth2JwtClaimsetBuilder WithTimestamp(long timestamp) {
      this.timestamp = timestamp;
      return this;
    }

    /// <summary>
    /// Sets the expiry for the claimset.
    /// </summary>
    /// <param name="expiry">The expiry for the claimset</param>
    /// <returns>This builder, for chaining method calls</returns>
    public OAuth2JwtClaimsetBuilder WithExpiry(long expiry) {
      this.expiry = expiry;
      return this;
    }

    /// <summary>
    /// Builds a JWT claimset from the current builder settings.
    /// </summary>
    /// <returns>A JWT claimset</returns>
    public OAuth2JwtClaimset Build() {
      OAuth2JwtClaimset claimset = new OAuth2JwtClaimset();
      claimset.scope = this.scope;
      claimset.audience = this.audience;
      claimset.serviceAccountEmail = this.serviceAccountEmail;
      claimset.impersonationEmail = this.impersonationEmail;
      claimset.timestamp = this.timestamp;
      claimset.expiry = this.expiry;
      return claimset;
    }
  }
}
