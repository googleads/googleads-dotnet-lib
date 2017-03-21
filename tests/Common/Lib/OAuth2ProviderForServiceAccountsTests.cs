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

namespace Google.Api.Ads.Common.Tests.Lib {
  /// <summary>
  /// Tests for OAuth2ProviderForServiceAccounts class.
  /// </summary>
  public class OAuth2ProviderForServiceAccountsTests {
    private OAuth2ProviderForServiceAccounts provider;
    private OAuth2RequestInterceptor oauth2RequestInterceptor =
        (OAuth2RequestInterceptor) OAuth2RequestInterceptor.Instance;

    /// <summary>
    /// The dictionary to hold the test data.
    /// </summary>
    private Dictionary<string, string> dictSettings;

    /// <summary>
    /// Signed request for getting access token for a service account.
    /// </summary>
    private const string SERVICE_ACCOUNT_REQUEST =
        "grant_type=urn%3aietf%3aparams%3aoauth%3agrant-type%3ajwt-bearer&assertion=" +
        "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJlZWUtODMyQHNtb290aC1sb29wLT" +
        "Q3Mi5pYW0uZ3NlcnZpY2VhY2NvdW50LmNvbSIsICJzY29wZSI6IlRFU1RfU0NPUEUiLCAiYXVkI" +
        "joiaHR0cHM6Ly9hY2NvdW50cy5nb29nbGUuY29tL28vb2F1dGgyL3Rva2VuIiwgImV4cCI6MTM1" +
        "MzkyODU1MSwgImlhdCI6MTM1MzkyNDk1MSwgInBybiI6IlRFU1RfUFJOX0VNQUlMIn0.H_fKtS_" +
        "54G0Gr9_cq1BM3_xcje_PuXV7N0ImW4T2HaGrTLTq2oBqiBi0Cfjqi_HkjvN4kJYylmSrI9LQLs" +
        "qKoD5n_1f1YvjxqE4kZhraYsoJLP9IOI2q-Gjzt3POxZU8DBCCI_9NL21TXeXO4QMywGclnkkzZ" +
        "LDinQeLsnqbTDwFaVuQyIna4LfhnXLhpnLg8SKpnda_70Ly1QE2E6ZGKY2iMlDNq2XqRdx3fFYA" +
        "psRxmFnikv_yf5VgZ-0KKE6TnmBOhnyAH6AJvYcmhDkebW-0oFNlkyjh_6rw16YRm7MO9AOAKxW" +
        "J21EJPdGHvYbNfKm6HlSOeKD6-zrpzI4xdA";

    /// <summary>
    /// OAuth2 authorization header.
    /// </summary>
    private const String AUTHORIZATION_HEADER = "Bearer " + TEST_ACCESS_TOKEN;

    // Test values for various properties.
    private const string TEST_CLIENT_ID = "TEST_CLIENT_ID";
    private const string TEST_CLIENT_SECRET = "TEST_CLIENT_SECRET";
    private const string TEST_REDIRECT_URI = "TEST_REDIRECT_URI";
    private const string TEST_ACCESS_TOKEN = OAuth2RequestInterceptor.TEST_ACCESS_TOKEN;
    private const string TEST_SCOPE = "TEST_SCOPE";
    private const string TEST_STATE = "TEST_STATE";
    private readonly OAuthTokensObtainedCallback TEST_CALLBACK =
        delegate(AdsOAuthProvider provider) {};
    private const string TEST_PRN_EMAIL = "TEST_PRN_EMAIL";
    private readonly string TEST_SECRETS_FILE = GetSecretsFilePath();

    /// <summary>
    /// Initializes the test case.
    /// </summary>
    [SetUp]
    public void Init() {
      dictSettings = InitSettingsDict(true);
      oauth2RequestInterceptor.Intercept = true;
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
        fs.Write(Resources.secret, 0, Resources.secret.Length);
      }
      return tempSecretsPath;
    }

    /// <summary>
    /// Tears down the test case.
    /// </summary>
    [TearDown]
    public void TearDown() {
      oauth2RequestInterceptor.Intercept = false;
    }

    /// <summary>
    /// Tests the default constructor.
    /// </summary>
    [Test]
    public void TestConstructor() {
      MockAppConfig config = new MockAppConfig();
      config.MockReadSettings(dictSettings);
      provider = new OAuth2ProviderForServiceAccounts(config);

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
      provider = new OAuth2ProviderForServiceAccounts(config);

      TestUtils.ValidateRequiredParameters(provider, new string[] {"ServiceAccountEmail",
          "Scope", "JwtPrivateKey"},
          delegate() {
            provider.GenerateAccessTokenForServiceAccount();
          }
      );
      oauth2RequestInterceptor.RequestType =
          OAuth2RequestInterceptor.OAuth2RequestType.FetchAccessTokenForServiceAccount;
      WebRequestInterceptor.OnBeforeSendResponse callback = delegate(Uri uri,
          WebHeaderCollection headers, String body) {
        Assert.AreEqual(SERVICE_ACCOUNT_REQUEST, body);
      };
      try {
        oauth2RequestInterceptor.BeforeSendResponse += callback;
        provider.GenerateAccessTokenForServiceAccount();
        Assert.AreEqual(provider.AccessToken, OAuth2RequestInterceptor.TEST_ACCESS_TOKEN);
        Assert.AreEqual(provider.TokenType, OAuth2RequestInterceptor.ACCESS_TOKEN_TYPE);
        Assert.AreEqual(provider.ExpiresIn.ToString(), OAuth2RequestInterceptor.EXPIRES_IN);
      } finally {
        oauth2RequestInterceptor.BeforeSendResponse -= callback;
      }
    }

    /// <summary>
    /// Tests if we can retrieve an OAuth2 authorization header.
    /// </summary>
    [Test]
    public void TestGetAuthHeader() {
      MockAppConfig config = new MockAppConfig();
      config.MockReadSettings(dictSettings);
      provider = new OAuth2ProviderForServiceAccounts(config);
      Assert.AreEqual(AUTHORIZATION_HEADER, provider.GetAuthHeader());
    }

    /// <summary>
    /// Tests the property setters and getters.
    /// </summary>
    [Test]
    public void TestPropertySettersAndGetters() {
      MockAppConfig config = new MockAppConfig();
      config.MockReadSettings(dictSettings);
      provider = new OAuth2ProviderForServiceAccounts(config);

      provider.ClientId = TEST_CLIENT_ID;
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
