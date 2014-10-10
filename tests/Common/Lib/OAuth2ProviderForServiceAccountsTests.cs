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
    private MockAppConfig appConfig;
    private OAuth2RequestInterceptor oauth2RequestInterceptor =
        (OAuth2RequestInterceptor) OAuth2RequestInterceptor.Instance;

    /// <summary>
    /// The hashtable to hold the test data.
    /// </summary>
    private Hashtable tblSettings;

    /// <summary>
    /// Signed request for getting access token for a service account.
    /// </summary>
    private const string SERVICE_ACCOUNT_REQUEST = "grant_type=urn%3aietf%3aparams%3aoa" +
        "uth%3agrant-type%3ajwt-bearer&assertion=eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.e" +
        "yJpc3MiOiJPQXV0aDJTZXJ2aWNlQWNjb3VudEVtYWlsIiwgInNjb3BlIjoiT0F1dGgyU2NvcGUiLCA" +
        "iYXVkIjoiaHR0cHM6Ly9hY2NvdW50cy5nb29nbGUuY29tL28vb2F1dGgyL3Rva2VuIiwgImV4cCI6M" +
        "TM1MzkyODU1MSwgImlhdCI6MTM1MzkyNDk1MSwgInBybiI6Ik9BdXRoMlBybkVtYWlsIn0.bYZ4PtQ" +
        "nRVLrmTfaalQtoLyAOZFOqrHjAMV0i62N5RoGosHPH6xC4tmb9FNigdgFrBKzz1z61ussF_ZPVs-PR" +
        "8XceOk9pu-67YCZLo9C2wHLiotl8JE9yR-B1tTSU8L4w0yiAw5HY1GdT1Qa3iB2seA7bDjp5TQjvyw" +
        "h1-PQ6Lw";

    /// <summary>
    /// OAuth2 authorization header.
    /// </summary>
    private const String AUTHORIZATION_HEADER = "Bearer " + TEST_ACCESS_TOKEN;

    // Keys for TestPropertySettersAndGetters.
    private const string TEST_CLIENT_ID = "TEST_CLIENT_ID";
    private const string TEST_CLIENT_SECRET = "TEST_CLIENT_SECRET";
    private const string TEST_ACCESS_TOKEN = OAuth2RequestInterceptor.TEST_ACCESS_TOKEN;
    private const string TEST_SCOPE = "TEST_SCOPE";
    private const string TEST_STATE = "TEST_STATE";
    private readonly OAuthTokensObtainedCallback TEST_CALLBACK =
        delegate(AdsOAuthProvider provider) {};
    private const string TEST_SERVICE_ACCOUNT_EMAIL = "TEST_SERVICE_ACCOUNT_EMAIL";
    private const string TEST_PRN_EMAIL = "TEST_PRN_EMAIL";
    private const string TEST_JWT_CERTIFICATE_PATH = "TEST_JWT_CERTIFICATE_PATH";
    private const string TEST_JWT_CERTIFICATE_PASSWORD = "TEST_JWT_CERTIFICATE_PASSWORD";

    /// <summary>
    /// Initializes the test case.
    /// </summary>
    [SetUp]
    public void Init() {
      tblSettings = new Hashtable();
      tblSettings.Add("OAuth2ClientId", "OAuth2ClientId");
      tblSettings.Add("OAuth2ClientSecret", "OAuth2ClientSecret");
      tblSettings.Add("OAuth2ServiceAccountEmail", "OAuth2ServiceAccountEmail");
      tblSettings.Add("OAuth2PrnEmail", "OAuth2PrnEmail");

      tblSettings.Add("OAuth2AccessToken", "OAuth2AccessToken");
      tblSettings.Add("OAuth2Scope", "OAuth2Scope");
      tblSettings.Add("OAuth2RedirectUri", "OAuth2RedirectUri");
      string tempCertificatePath = Path.GetTempFileName();
      using (FileStream fs = File.OpenWrite(tempCertificatePath)) {
        fs.Write(Resources.certificate, 0, Resources.certificate.Length);
      }
      tblSettings.Add("OAuth2JwtCertificatePath", tempCertificatePath);
      tblSettings.Add("OAuth2JwtCertificatePassword", "notasecret");

      appConfig = new MockAppConfig();
      appConfig.MockReadSettings(tblSettings);
      provider = new OAuth2ProviderForServiceAccounts(appConfig);
      oauth2RequestInterceptor.Intercept = true;
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
      provider = new OAuth2ProviderForServiceAccounts(appConfig);
      Assert.AreEqual(provider.ClientId, appConfig.OAuth2ClientId);
      Assert.AreEqual(provider.ClientSecret, appConfig.OAuth2ClientSecret);
      Assert.AreEqual(provider.AccessToken, appConfig.OAuth2AccessToken);
      Assert.AreEqual(provider.Scope, appConfig.OAuth2Scope);
      Assert.AreEqual(provider.ServiceAccountEmail, appConfig.OAuth2ServiceAccountEmail);
      Assert.AreEqual(provider.PrnEmail, appConfig.OAuth2PrnEmail);
      Assert.AreEqual(provider.JwtCertificatePath, appConfig.OAuth2CertificatePath);
      Assert.AreEqual(provider.JwtCertificatePassword, appConfig.OAuth2CertificatePassword);
    }

    /// <summary>
    /// Tests if we can generate an access token for service accounts.
    /// </summary>
    [Test]
    public void TestGenerateAccessTokenForServiceAccounts() {
      TestUtils.ValidateRequiredParameters(provider, new string[] {"ServiceAccountEmail",
          "Scope", "JwtCertificatePath", "JwtCertificatePassword"},
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
      provider = new OAuth2ProviderForServiceAccounts(appConfig);
      Assert.AreEqual(AUTHORIZATION_HEADER, provider.GetAuthHeader());
    }

    /// <summary>
    /// Tests the property setters and getters.
    /// </summary>
    [Test]
    public void TestPropertySettersAndGetters() {
      provider = new OAuth2ProviderForServiceAccounts(appConfig);

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

      provider.ServiceAccountEmail = TEST_SERVICE_ACCOUNT_EMAIL;
      Assert.AreEqual(provider.ServiceAccountEmail, TEST_SERVICE_ACCOUNT_EMAIL);

      provider.PrnEmail = TEST_PRN_EMAIL;
      Assert.AreEqual(provider.PrnEmail, TEST_PRN_EMAIL);

      provider.JwtCertificatePath = TEST_JWT_CERTIFICATE_PATH;
      Assert.AreEqual(provider.JwtCertificatePath, TEST_JWT_CERTIFICATE_PATH);

      provider.JwtCertificatePassword = TEST_JWT_CERTIFICATE_PASSWORD;
      Assert.AreEqual(provider.JwtCertificatePassword, TEST_JWT_CERTIFICATE_PASSWORD);
    }
  }
}
