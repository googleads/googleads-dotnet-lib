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

using Google.Api.Ads.Common.Logging;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Http;
using Google.Apis.Util;
using System;

namespace Google.Api.Ads.Common.Lib {

  /// <summary>
  /// Provides OAuth authorization mechanism for Ads services when using service
  /// account flow.
  /// </summary>
  public class OAuth2ProviderForServiceAccounts : OAuth2ProviderBase,
      AdsOAuthProviderForServiceAccounts {

    /// <summary>
    /// The feature ID for this class.
    /// </summary>
    private const AdsFeatureUsageRegistry.Features FEATURE_ID =
        AdsFeatureUsageRegistry.Features.OAuthServiceAccountFlow;

    /// <summary>
    /// Default expiry period for access token.
    /// </summary>
    private const int DEFAULT_EXPIRY_PERIOD = 3600;

    /// <summary>
    /// The HttpClientFactory used for requesting Access tokens.
    /// </summary>
    public IHttpClientFactory HttpClientFactory { get; set; } = null;

    /// <summary>
    /// The clock used for requesting Access tokens.
    /// </summary>
    public IClock Clock { get; set; } = SystemClock.Default;

    /// <summary>
    /// Gets or sets the JWT private key.
    /// </summary>
    public string JwtPrivateKey {
      get {
        return config.OAuth2PrivateKey;
      }
    }

    /// <summary>
    /// Gets or sets the service account email for which access token should be
    /// retrieved.
    /// </summary>
    public string ServiceAccountEmail {
      get {
        return config.OAuth2ServiceAccountEmail;
      }
    }

    /// <summary>
    /// Gets or sets the email of the account for which the call is being made.
    /// </summary>
    public string PrnEmail {
      get {
        return config.OAuth2PrnEmail;
      }
      set {
        config.OAuth2PrnEmail = value;
      }
    }

    /// <summary>
    /// Initializes a new instance of the OAuth2ProviderForServiceAccounts class.
    /// </summary>
    /// <param name="config">The config.</param>
    public OAuth2ProviderForServiceAccounts(AppConfig config)
      : base(config) {
    }

    /// <summary>
    /// Gets the access token for service account.
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown if one of the following
    /// OAuth2 parameters are empty: ServiceAccountEmail, JwtPrivateKey, Scope
    /// </exception>
    public void GenerateAccessTokenForServiceAccount() {
      // Mark the usage.
      featureUsageRegistry.MarkUsage(FEATURE_ID);

      long timestamp = config.UnixTimestamp;
      long expiry = timestamp + DEFAULT_EXPIRY_PERIOD;

      ValidateOAuth2Parameter("ServiceAccountEmail", ServiceAccountEmail);
      ValidateOAuth2Parameter("JwtPrivateKey", JwtPrivateKey);
      ValidateOAuth2Parameter("Scope", Scope);

      ServiceAccountCredential credential = new ServiceAccountCredential(
          new ServiceAccountCredential.Initializer(ServiceAccountEmail) {
            Scopes = new[] { Scope },
            HttpClientFactory = this.HttpClientFactory,
            Clock = this.Clock
          }.FromPrivateKey(JwtPrivateKey)
      );

      credential.GetAccessTokenForRequestAsync().Wait();
      this.AccessToken = credential.Token.AccessToken;
      ExpiresIn = (int)credential.Token.ExpiresInSeconds.GetValueOrDefault();
      UpdatedOn = DateTime.UtcNow;
      if (this.OnOAuthTokensObtained != null) {
        this.OnOAuthTokensObtained(this);
      }

    }

    /// <summary>
    /// Refreshes the access token.
    /// </summary>
    public override void RefreshAccessToken() {
      // Mark the usage.
      featureUsageRegistry.MarkUsage(FEATURE_ID);

      GenerateAccessTokenForServiceAccount();
    }
  }
}
