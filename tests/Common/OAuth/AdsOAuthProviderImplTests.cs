// Copyright 2018 Google LLC
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
using Google.Api.Ads.Common.OAuth;
using Google.Api.Ads.Common.Tests.Mocks;
using Google.Apis.Auth.OAuth2.Responses;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Google.Api.Ads.Common.Tests.OAuth {

  /// <summary>
  /// Tests for <see cref="AdsOAuthProviderImpl"/> class.
  /// </summary>
  public class AdsOAuthProviderImplTests {
    /* Test keys for verification purposes.*/

    private const string TEST_CLIENT_ID = "TEST_CLIENT_ID";
    private const string TEST_CLIENT_SECRET = "TEST_CLIENT_SECRET";
    private const string TEST_REDIRECT_URI = "TEST_REDIRECT_URI";
    private const string TEST_ACCESS_TOKEN = "TEST_ACCESS_TOKEN";
    private const string ACCESS_TOKEN_TYPE = "ACCESS_TOKEN_TYPE";
    private static readonly DateTime ISSUED_AT = new DateTime(2001, 11, 24, 15, 45, 23);
    private readonly TimeSpan EXPIRES_IN = TimeSpan.FromSeconds(3000);
    private const string TEST_SCOPE = "TEST_SCOPE";
    private const string TEST_STATE = "TEST_STATE";
    private const string TEST_REFRESH_TOKEN = "TEST_REFRESH_TOKEN";
    private const string TEST_PRN_EMAIL = "TEST_PRN_EMAIL";
    private const string TEST_AUTHORIZATION_CODE = "TEST_AUTHORIZATION_CODE";
    private const string TEST_AUTHORIZATION_URL = "TEST_AUTHORIZATION_URL";
    private const string TEST_ACCESS_TOKEN_NEW = "TEST_ACCESS_TOKEN_NEW";
    private const string TEST_REFRESH_TOKEN_NEW = "TEST_REFRESH_TOKEN_NEW";
    private static readonly DateTime ISSUED_AT_NEW = new DateTime(2002, 11, 24, 15, 45, 23);
    private static readonly TimeSpan EXPIRES_IN_NEW = TimeSpan.FromSeconds(2000);

    /// <summary>
    /// Settings to be used during normal API call tests.
    /// </summary>
    private readonly Dictionary<string, string> ALL_SETTINGS = new Dictionary<string, string>() {
      { "OAuth2ClientId", TEST_CLIENT_ID },
      { "OAuth2ClientSecret", TEST_CLIENT_SECRET },
      { "OAuth2Scope", TEST_SCOPE },
      { "OAuth2AccessToken", TEST_ACCESS_TOKEN },
      { "OAuth2RefreshToken", TEST_REFRESH_TOKEN },
      { "OAuth2RedirectUri", TEST_REDIRECT_URI },
      { "OAuth2PrnEmail", TEST_PRN_EMAIL },
      { "OAuth2SecretsJsonPath", GetSecretsFilePath() }
    };

    /// <summary>
    /// Settings to be used during API call tests where there is a mocked server interaction,
    /// and new tokens are obtained.
    /// </summary>
    private readonly Dictionary<string, string> ALL_SETTINGS_NEW =
        new Dictionary<string, string>() {
      { "OAuth2ClientId", TEST_CLIENT_ID },
      { "OAuth2ClientSecret", TEST_CLIENT_SECRET },
      { "OAuth2Scope", TEST_SCOPE },
      { "OAuth2AccessToken", TEST_ACCESS_TOKEN_NEW },
      { "OAuth2RefreshToken", TEST_REFRESH_TOKEN_NEW },
      { "OAuth2RedirectUri", TEST_REDIRECT_URI },
      { "OAuth2PrnEmail", TEST_PRN_EMAIL },
      { "OAuth2SecretsJsonPath", GetSecretsFilePath() }
    };

    /// <summary>
    /// The application configuration to be used in tests.
    /// </summary>
    private MockAppConfig appConfig;

    /// <summary>
    /// The new application configuration to be used in tests where there is a mocked server
    /// interaction, and new tokens are obtained.
    /// </summary>
    private MockAppConfig newAppConfig;

    /// <summary>
    /// A mock class for <see cref="AdsOAuthProviderImpl"/>. Calls to underlying Google.Auth
    /// libraries have been mocked out to make actual server calls, and tests are restricted
    /// to verify if correct parameters are passed, responses are returned, and exceptions are
    /// raised.
    /// </summary>
    private class MockAdsOAuthProviderImpl : AdsOAuthProviderImpl {

      /// <summary>
      /// Gets or sets the authorization code for test verification purposes.
      /// </summary>
      internal string AuthorizationCode {
        get; set;
      }

      /// <summary>
      /// Gets or sets the new application configuration whenever there's a mocked server
      /// interaction, and new tokens are obtained.
      /// </summary>
      private AppConfig NewAppConfig {
        get; set;
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="MockAdsOAuthProviderImpl"/> class.
      /// </summary>
      /// <param name="appConfig">The application configuration.</param>
      /// <param name="newAppConfig">The new application configuration whenever there's a mocked
      /// server interaction, and new tokens are obtained.</param>
      internal MockAdsOAuthProviderImpl(AppConfig appConfig, AppConfig newAppConfig)
          : base(appConfig) {
        this.NewAppConfig = newAppConfig;
      }

      /// <summary>
      /// Exchanges the authorization code for access and refresh tokens.
      /// </summary>
      /// <param name="code">The authorization code.</param>
      /// <returns>
      /// The token response.
      /// </returns>
      protected override TokenResponse ExchangeCodeForToken(string code) {
        Assert.AreEqual(AuthorizationCode, code);
        return GetResponse();
      }

      /// <summary>
      /// Refreshes the access token.
      /// </summary>
      /// <param name="refreshToken">The refresh token.</param>
      /// <returns>
      /// The token response.
      /// </returns>
      protected override TokenResponse GetAccessTokenForAuthorizationCodeFlow() {
        return GetResponse();
      }

      /// <summary>
      /// Revokes the refresh token asynchronously.
      /// </summary>
      /// <param name="refreshToken">The refresh token.</param>
      protected override void RevokeRefreshToken(string refreshToken) {
        Assert.AreEqual(Config.OAuth2RefreshToken, refreshToken);
      }

      /// <summary>
      /// Gets the access token in service account flow.
      /// </summary>
      /// <returns>
      /// The token response.
      /// </returns>
      protected override TokenResponse GetAccessTokenForServiceAccount() {
        return GetResponse();
      }

      /// <summary>
      /// Gets the token response.
      /// </summary>
      /// <returns>The token response.</returns>
      private TokenResponse GetResponse() {
        return new TokenResponse() {
          AccessToken = NewAppConfig.OAuth2AccessToken,
          RefreshToken = NewAppConfig.OAuth2RefreshToken,
          Scope = NewAppConfig.OAuth2Scope,
          ExpiresInSeconds = (int) EXPIRES_IN_NEW.TotalSeconds,
          IssuedUtc = ISSUED_AT_NEW
        };
      }
    }

    /// <summary>
    /// Initializes the test case.
    /// </summary>
    [SetUp]
    public void Init() {
      appConfig = new MockAppConfig();
      appConfig.MockReadSettings(ALL_SETTINGS);

      newAppConfig = new MockAppConfig();
      newAppConfig.MockReadSettings(ALL_SETTINGS_NEW);
    }

    /// <summary>
    /// Tests the property setters and getters.
    /// </summary>
    [Test]
    public void TestProperties() {
      MockAdsOAuthProviderImpl provider = new MockAdsOAuthProviderImpl(appConfig, newAppConfig);

      TimeSpan EXPIRES_IN_PROPERTY_TESTING = TimeSpan.FromSeconds(800);

      provider.ExpiresIn = (int) EXPIRES_IN_PROPERTY_TESTING.TotalSeconds;
      Assert.AreEqual(EXPIRES_IN_PROPERTY_TESTING.TotalSeconds, provider.ExpiresIn);

      provider.IsOffline = true;
      Assert.AreEqual(true, provider.IsOffline);
    }

    /// <summary>
    /// Tests the <see cref="AdsOAuthProviderImpl.GetAuthorizationUrl" /> method.
    /// </summary>
    [Test]
    public void TestGetAuthorizationUrl() {
      MockAdsOAuthProviderImpl provider = new MockAdsOAuthProviderImpl(appConfig, newAppConfig);
      string temp;

      // Scope is a required parameter.
      temp = provider.Config.OAuth2Scope;
      provider.Config.OAuth2Scope = "";
      Assert.Throws<ArgumentNullException>(delegate () {
        provider.GetAuthorizationUrl();
      });
      provider.Config.OAuth2Scope = temp;

      // RedirectUri is a required parameter.
      temp = provider.Config.OAuth2RedirectUri;
      provider.Config.OAuth2RedirectUri = "";
      Assert.Throws<ArgumentNullException>(delegate () {
        provider.GetAuthorizationUrl();
      });
      provider.Config.OAuth2RedirectUri = temp;

      // Attempt a normal call, to validate the parameter passing.
      Assert.DoesNotThrow(delegate () {
        provider.GetAuthorizationUrl();
      });
    }

    [Test]
    public void TestGetAuthorizationUrl_NoDuplicateParameters() {
      MockAdsOAuthProviderImpl provider = new MockAdsOAuthProviderImpl(appConfig, newAppConfig);
      Assert.AreEqual(1, Regex.Matches(provider.GetAuthorizationUrl(), "access_type=").Count);
    }

    /// <summary>
    /// Tests the <see cref="AdsOAuthProviderImpl.GetAuthHeader" /> method.
    /// </summary>
    [Test]
    public void TestGetAuthHeader() {
      MockAdsOAuthProviderImpl provider = new MockAdsOAuthProviderImpl(appConfig, appConfig);

      // Note: The logic of expiring v/s non-expiring token refresh is in the Google.Auth library.
      // Since the test mocks out calls to the Google.Auth library, we restrict this test to verify
      // that the header formatting is correct.
      string expected = $"Bearer {TEST_ACCESS_TOKEN}";
      Assert.AreEqual(expected, provider.GetAuthHeader());
    }

    /// <summary>
    /// Tests the <see cref="AdsOAuthProviderImpl.FetchAccessAndRefreshTokens(string)" /> method.
    /// </summary>
    [Test]
    public void TestFetchAccessAndRefreshTokens() {
      MockAdsOAuthProviderImpl provider = new MockAdsOAuthProviderImpl(appConfig, newAppConfig);
      provider.AuthorizationCode = TEST_AUTHORIZATION_CODE;

      provider.FetchAccessAndRefreshTokens(TEST_AUTHORIZATION_CODE);

      Assert.AreEqual(TEST_ACCESS_TOKEN_NEW, provider.Config.OAuth2AccessToken);
      Assert.AreEqual(TEST_REFRESH_TOKEN_NEW, provider.Config.OAuth2RefreshToken);
      Assert.AreEqual(EXPIRES_IN_NEW.TotalSeconds, provider.ExpiresIn);
      Assert.AreEqual(ISSUED_AT_NEW, provider.UpdatedOn);
    }

    /// <summary>
    /// Tests the <see cref="AdsOAuthProviderImpl.RevokeRefreshToken" /> method.
    /// </summary>
    [Test]
    public void TestRevokeRefreshToken() {
      MockAdsOAuthProviderImpl provider = new MockAdsOAuthProviderImpl(appConfig, newAppConfig);

      appConfig.OAuth2Mode = OAuth2Flow.SERVICE_ACCOUNT;
      Assert.DoesNotThrow(delegate () {
        provider.RevokeRefreshToken();
      });
    }

    /// <summary>
    /// Tests the <see cref="AdsOAuthProviderImpl.GetAccessTokenForServiceAccount" /> method.
    /// </summary>
    [Test]
    public void TestGenerateAccessTokenForServiceAccount() {
      MockAdsOAuthProviderImpl provider = new MockAdsOAuthProviderImpl(appConfig, newAppConfig);

      appConfig.OAuth2Mode = OAuth2Flow.SERVICE_ACCOUNT;
      provider.GenerateAccessTokenForServiceAccount();

      Assert.AreEqual(TEST_ACCESS_TOKEN_NEW, provider.Config.OAuth2AccessToken);
      Assert.AreEqual((int) EXPIRES_IN_NEW.TotalSeconds, provider.ExpiresIn);
      Assert.AreEqual(ISSUED_AT_NEW, provider.UpdatedOn);
    }

    /// <summary>
    /// Tests the <see cref="AdsOAuthProviderImpl.RefreshAccessToken" /> method.
    /// </summary>
    [Test]
    public void TestRefreshAccessToken() {
      MockAdsOAuthProviderImpl provider = new MockAdsOAuthProviderImpl(appConfig, newAppConfig);

      // Application flow, offline = false.
      appConfig.OAuth2Mode = OAuth2Flow.APPLICATION;
      provider.IsOffline = false;
      Assert.Throws<ArgumentException>(delegate () {
        provider.RefreshAccessToken();
      });

      // Application flow, offline = true.
      appConfig.OAuth2Mode = OAuth2Flow.APPLICATION;
      provider.IsOffline = true;
      provider.RefreshAccessToken();

      Assert.AreEqual(TEST_ACCESS_TOKEN_NEW, provider.Config.OAuth2AccessToken);
      Assert.AreEqual((int) EXPIRES_IN_NEW.TotalSeconds, provider.ExpiresIn);
      Assert.AreEqual(ISSUED_AT_NEW, provider.UpdatedOn);

      // Service account flow.
      appConfig.OAuth2Mode = OAuth2Flow.SERVICE_ACCOUNT;
      provider.RefreshAccessToken();

      Assert.AreEqual(TEST_ACCESS_TOKEN_NEW, provider.Config.OAuth2AccessToken);
      Assert.AreEqual((int) EXPIRES_IN_NEW.TotalSeconds, provider.ExpiresIn);
      Assert.AreEqual(ISSUED_AT_NEW, provider.UpdatedOn);
    }

    /// <summary>
    /// Gets the JSON secrets file path for test purposes.
    /// </summary>
    /// <returns>The JSON secrets file path.</returns>
    private static string GetSecretsFilePath() {
      string tempSecretsPath = Path.GetTempFileName();
      using (FileStream fs = File.OpenWrite(tempSecretsPath)) {
        byte[] bytes = Encoding.UTF8.GetBytes(Resources.secret);
        fs.Write(bytes, 0, bytes.Length);
      }
      return tempSecretsPath;
    }
  }
}
