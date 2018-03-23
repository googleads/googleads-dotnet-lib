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

using NUnit.Framework;

using Google.Api.Ads.Common.Lib;
using Google.Api.Ads.Common.Tests;
using Google.Api.Ads.Common.Tests.Mocks;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using Google.Apis.Http;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Util;

namespace Google.Api.Ads.Common.Tests.Lib {
  /// <summary>
  /// Tests for OAuth2ProviderForServiceAccounts class.
  /// </summary>
  public class OAuth2ProviderForServiceAccountsTests {
    private OAuth2ProviderForServiceAccounts provider;

    /// <summary>
    /// The dictionary to hold the test data.
    /// </summary>
    private Dictionary<string, string> dictSettings;

    private MockHttpClientFactory mockHttpClientFactory;
    private MockClock mockClock;

    /// <summary>
    /// Signed request for getting access token for a service account with impersonation.
    /// </summary>
    private const string SERVICE_ACCOUNT_REQUEST =
        "assertion=eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJzY29wZSI6IlRFU1RfU0NPUEUiLCJlbWFpbF92" +
        "ZXJpZmllZCI6ZmFsc2UsImlzcyI6InRlc3RAcHJvamVjdC1pZC0xMjMuZXhhbXBsZS5jb20iLCJzdWIiOiJURVN" +
        "UX1BSTl9FTUFJTCIsImF1ZCI6Imh0dHBzOi8vd3d3Lmdvb2dsZWFwaXMuY29tL29hdXRoMi92NC90b2tlbiIsIm" +
        "V4cCI6MTUxNDc3MjA2MSwiaWF0IjoxNTE0NzY4NDYxfQ.B-9BTTQFIzGsL8n_qxWDNxAmkpksYKe_PRp7Bc3pcD" +
        "dk86HpvYbCjxjnw__KDFBRexyHN1fnhvtgFZBsQd9IAU4PjcpF_yD8P9yswpQPL-AOgjBIPHSqA0Lf27fUG87pP" +
        "76KASSdkxAbkjAKXV6vsntNZM72ck23otTwiQ6ZvQz9LvXftsWSUpsWGRbhVOZeqrPoPCjKjrPd4djqIgirQz8W" +
        "eTqn-utbaiYU7EHWPeJsgWBg85su6ppMo9eOl7LEOswcyaBLW_9hnXpydLwRrxQ2on_05V1NOroMzQblRBlYNNE" +
        "O6e8kB6OekgM-HeBTlboaZDRYnnYPn8gzPSrRDQ&grant_type=urn%3Aietf%3Aparams%3Aoauth%3Agrant-" +
        "type%3Ajwt-bearer";

    /// <summary>
    /// Signed request for getting access token for a service account without impersonation.
    /// </summary>
    private const string SERVICE_ACCOUNT_REQUEST_NO_PRN =
        "assertion=eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJzY29wZSI6IlRFU1RfU0NPUEUiLCJlbWFpbF92" +
        "ZXJpZmllZCI6ZmFsc2UsImlzcyI6InRlc3RAcHJvamVjdC1pZC0xMjMuZXhhbXBsZS5jb20iLCJhdWQiOiJodHR" +
        "wczovL3d3dy5nb29nbGVhcGlzLmNvbS9vYXV0aDIvdjQvdG9rZW4iLCJleHAiOjE1MTQ3NzIwNjEsImlhdCI6MT" +
        "UxNDc2ODQ2MX0.os4zaY_O7PSU4qvLw0WT04WoOFUz-eHfraP_Ul1hKd_tLkohZuGRONmHGdaJ42ne43Ki7dwuZ" +
        "lhW74FLwCtDT22paYkla4K4yI2C-1VjsW7uER7lghLCJ6rmXVJ3GQIT7bnC1j2G6nf27tsMoNKC3zuW_L3rrP8j" +
        "oJtMQPP5VFcbCSZgt6bT7pClym9byzQsYt-hwvt4g3iK0oO5JGG9M9qiEf0TtAcRydr2NM6ylOzZZl7E2Hzky02" +
        "EN-9DlYCupTJaD4rTT55Bhk0cCmEGh8Rmv8IO3xUx5z0VBZsV3G-PiMJ45pxKAWtOsUQkujoJ1ucJGjrthFtY59" +
        "KTrlfGzQ&grant_type=urn%3Aietf%3Aparams%3Aoauth%3Agrant-type%3Ajwt-bearer";

    /// <summary>
    /// OAuth2 authorization header.
    /// </summary>
    private const String AUTHORIZATION_HEADER = "Bearer " + TEST_ACCESS_TOKEN;

    // Test values for various properties.
    private const string TEST_CLIENT_ID = "TEST_CLIENT_ID";
    private const string TEST_CLIENT_SECRET = "TEST_CLIENT_SECRET";
    private const string TEST_REDIRECT_URI = "TEST_REDIRECT_URI";
    private const string TEST_ACCESS_TOKEN = OAuth2RequestInterceptor.TEST_ACCESS_TOKEN;
    private const string ACCESS_TOKEN_TYPE = OAuth2RequestInterceptor.ACCESS_TOKEN_TYPE;
    private const string EXPIRES_IN = OAuth2RequestInterceptor.EXPIRES_IN;
    private const string TEST_SCOPE = "TEST_SCOPE";
    private const string TEST_STATE = "TEST_STATE";
    private readonly OAuthTokensObtainedCallback TEST_CALLBACK =
        delegate(AdsOAuthProvider provider) {};
    private const string TEST_PRN_EMAIL = "TEST_PRN_EMAIL";
    private const string SERVICE_ACCOUNT_RESPONSE = "{\r\n\"access_token\" : \"" +
       TEST_ACCESS_TOKEN + "\",\r\n\"token_type\" : \"" + ACCESS_TOKEN_TYPE + "\"" +
       ",\r\n \"expires_in\" : " + EXPIRES_IN + "\r\n}";

    /// <summary>
    /// Initializes the test case.
    /// </summary>
    [SetUp]
    public void Init() {
      dictSettings = InitSettingsDict(true);
      mockHttpClientFactory = new MockHttpClientFactory() {
        messageHandler = new MockHttpMessageHandler() {
          Response = SERVICE_ACCOUNT_RESPONSE
        }
      };
      mockClock = new MockClock();
    }

    /// <summary>
    /// Initializes the settings table.
    /// </summary>
    /// <param name="setJsonSecretsFile">True, if a value should be set to
    /// if set to OAuth2SecretsJsonPath property, false otherwise.</param>
    /// <returns>The initialized settings dictionary.</returns>
    private Dictionary<string, string> InitSettingsDict(bool setJsonSecretsFile) {
      Dictionary<string, string> retval = new Dictionary<string, string>();
      retval.Add("OAuth2ClientId", TEST_CLIENT_ID);
      retval.Add("OAuth2ClientSecret", TEST_CLIENT_SECRET);
      retval.Add("OAuth2PrnEmail", TEST_PRN_EMAIL);
      retval.Add("OAuth2AccessToken", TEST_ACCESS_TOKEN);
      retval.Add("OAuth2Scope", TEST_SCOPE);
      retval.Add("OAuth2RedirectUri", TEST_REDIRECT_URI);
      retval.Add("OAuth2SecretsJsonPath", GetSecretsFilePath());
      return retval;
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

    /// <summary>
    /// Tests the default constructor.
    /// </summary>
    [Test]
    public void TestConstructor() {
      MockAppConfig config = new MockAppConfig();
      config.MockReadSettings(dictSettings);
      provider = new OAuth2ProviderForServiceAccounts(config) {
        HttpClientFactory = mockHttpClientFactory,
        Clock = mockClock 
      };

      Assert.AreEqual(provider.ClientId, config.OAuth2ClientId);
      Assert.AreEqual(provider.ClientSecret, config.OAuth2ClientSecret);
      Assert.AreEqual(provider.AccessToken, config.OAuth2AccessToken);
      Assert.AreEqual(provider.Scope, config.OAuth2Scope);
      Assert.AreEqual(provider.ServiceAccountEmail, config.OAuth2ServiceAccountEmail);
      Assert.AreEqual(provider.PrnEmail, config.OAuth2PrnEmail);
      Assert.AreEqual(provider.JwtPrivateKey, config.OAuth2PrivateKey);
      Assert.AreEqual(provider.ServiceAccountEmail, config.OAuth2ServiceAccountEmail);
    }

    /// <summary>
    /// Tests if we can generate an access token for service accounts.
    /// </summary>
    [Test]
    public void TestGenerateAccessTokenForServiceAccounts() {
      MockAppConfig config = new Mocks.MockAppConfig();
      config.MockReadSettings(dictSettings);
      provider = new OAuth2ProviderForServiceAccounts(config) {
        HttpClientFactory = mockHttpClientFactory,
        Clock = mockClock
      };

      TestUtils.ValidateRequiredParameters(provider, new string[] {"ServiceAccountEmail",
          "Scope", "JwtPrivateKey"},
          delegate() {
            provider.GenerateAccessTokenForServiceAccount();
          }
      );

      provider.GenerateAccessTokenForServiceAccount();
      Assert.AreEqual(mockHttpClientFactory.messageHandler.LastRequest, SERVICE_ACCOUNT_REQUEST);
      Assert.AreEqual(provider.AccessToken, OAuth2RequestInterceptor.TEST_ACCESS_TOKEN);
      Assert.AreEqual(provider.TokenType, OAuth2RequestInterceptor.ACCESS_TOKEN_TYPE);
      Assert.AreEqual(provider.ExpiresIn.ToString(), OAuth2RequestInterceptor.EXPIRES_IN);

      // Test no impersonation with empty string.
      config.SetPropertyFieldForTests("OAuth2PrnEmail", "");
      provider.GenerateAccessTokenForServiceAccount();
      Assert.AreEqual(mockHttpClientFactory.messageHandler.LastRequest,
          SERVICE_ACCOUNT_REQUEST_NO_PRN);

      // Test no impersonation with null.
      config.SetPropertyFieldForTests("OAuth2PrnEmail", null);
      provider.GenerateAccessTokenForServiceAccount();
      Assert.AreEqual(mockHttpClientFactory.messageHandler.LastRequest,
          SERVICE_ACCOUNT_REQUEST_NO_PRN);
    }

    /// <summary>
    /// Tests if we can retrieve an OAuth2 authorization header.
    /// </summary>
    [Test]
    public void TestGetAuthHeader() {
      MockAppConfig config = new MockAppConfig();
      config.MockReadSettings(dictSettings);
      provider = new OAuth2ProviderForServiceAccounts(config) {
        HttpClientFactory = mockHttpClientFactory,
        Clock = mockClock
      };
      Assert.AreEqual(AUTHORIZATION_HEADER, provider.GetAuthHeader());
    }

    /// <summary>
    /// Tests the property setters and getters.
    /// </summary>
    [Test]
    public void TestPropertySettersAndGetters() {
      MockAppConfig config = new MockAppConfig();
      config.MockReadSettings(dictSettings);
      provider = new OAuth2ProviderForServiceAccounts(config) {
        HttpClientFactory = mockHttpClientFactory,
        Clock = mockClock,
        ClientId = TEST_CLIENT_ID
      };
      Assert.AreEqual(provider.ClientId, TEST_CLIENT_ID);

      provider.ClientSecret = TEST_CLIENT_SECRET;
      Assert.AreEqual(provider.ClientSecret, TEST_CLIENT_SECRET);

      provider.AccessToken = TEST_ACCESS_TOKEN;
      Assert.AreEqual(provider.AccessToken, TEST_ACCESS_TOKEN);

      provider.Scope = TEST_SCOPE;
      Assert.AreEqual(provider.Scope, TEST_SCOPE);

      provider.State = TEST_STATE;
      Assert.AreEqual(provider.State, TEST_STATE);

      provider.OnOAuthTokensObtained = TEST_CALLBACK;
      Assert.AreEqual(provider.OnOAuthTokensObtained, TEST_CALLBACK);
    }
  }
}
