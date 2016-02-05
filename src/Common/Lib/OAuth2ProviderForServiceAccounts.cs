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

using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;

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
    /// Audience for generating JWT string.
    /// </summary>
    private const string JWT_AUDIENCE = "https://accounts.google.com/o/oauth2/token";

    /// <summary>
    /// Grant type for generating JWT string.
    /// </summary>
    private const string JWT_GRANT_TYPE = "urn:ietf:params:oauth:grant-type:jwt-bearer";

    /// <summary>
    /// Header for generating JWT string.
    /// </summary>
    private const string JWT_HEADER = "{\"alg\":\"RS256\",\"typ\":\"JWT\"}";

    /// <summary>
    /// Default expiry period for access token.
    /// </summary>
    private const int DEFAULT_EXPIRY_PERIOD = 3600;

    /// <summary>
    /// Gets or sets the service account email for which access token should be
    /// retrieved.
    /// </summary>
    public string ServiceAccountEmail {
      get {
        return config.OAuth2ServiceAccountEmail;
      }
      set {
        config.OAuth2ServiceAccountEmail = value;
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
    /// Gets or sets the JWT certificate path.
    /// </summary>
    public string JwtCertificatePath {
      get {
        return config.OAuth2CertificatePath;
      }
      set {
        config.OAuth2CertificatePath = value;
      }
    }

    /// <summary>
    /// Gets or sets the JWT certificate password.
    /// </summary>
    public string JwtCertificatePassword {
      get {
        return config.OAuth2CertificatePassword;
      }
      set {
        config.OAuth2CertificatePassword = value;
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
    /// OAuth2 parameters are empty: ServiceAccountEmail, Scope,
    /// JwtCertificatePath, JwtCertificatePassword.</exception>
    public void GenerateAccessTokenForServiceAccount() {
      // Mark the usage.
      featureUsageRegistry.MarkUsage(FEATURE_ID);;

      long timestamp = config.UnixTimestamp;
      long expiry = timestamp + DEFAULT_EXPIRY_PERIOD;

      ValidateOAuth2Parameter("ServiceAccountEmail", ServiceAccountEmail);
      ValidateOAuth2Parameter("Scope", Scope);
      ValidateOAuth2Parameter("JwtCertificatePath", JwtCertificatePath);
      ValidateOAuth2Parameter("JwtCertificatePassword", JwtCertificatePassword);

      OAuth2JwtClaimset jwtClaimset = new OAuth2JwtClaimsetBuilder()
          .WithScope(Scope)
          .WithServiceAccountEmail(ServiceAccountEmail)
          .WithImpersonationEmail(PrnEmail)
          .WithAudience(JWT_AUDIENCE)
          .WithTimestamp(timestamp)
          .WithExpiry(expiry)
          .Build();

      string encodedHeader = Base64UrlEncode(Encoding.UTF8.GetBytes(JWT_HEADER));
      string encodedClaimset = Base64UrlEncode(Encoding.UTF8.GetBytes(jwtClaimset.ToJson()));
      string inputForSignature = encodedHeader + "." + encodedClaimset;

      X509Certificate2 jwtCertificate = new X509Certificate2(JwtCertificatePath,
          JwtCertificatePassword);

      string signature = Base64UrlEncode(GetRsaSha256Signature(jwtCertificate,
          Encoding.UTF8.GetBytes(inputForSignature)));
      string jwt = inputForSignature + "." + signature;

      string body = "grant_type=" + HttpUtility.UrlEncode(JWT_GRANT_TYPE) + "&assertion=" +
          HttpUtility.UrlEncode(jwt);

      try {
        CallTokenEndpoint(body);
      } catch (ApplicationException e) {
        throw new AdsOAuthException("Failed to get access token for service account." + "\n" +
            e.Message);
      }
    }

    /// <summary>
    /// Refreshes the access token.
    /// </summary>
    public override void RefreshAccessToken() {
      // Mark the usage.
      featureUsageRegistry.MarkUsage(FEATURE_ID);;

      GenerateAccessTokenForServiceAccount();
    }
  }
}
