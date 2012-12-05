// Copyright 2012, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.Common.OAuth.Lib;
using Google.Api.Ads.Common.Tests;
using Google.Api.Ads.Common.Tests.Mocks;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Google.Api.Ads.Common.OAuth.Tests {
  public class OAuth2ProviderTests {
    OAuth2Provider provider;
    MockAppConfig appConfig;
    OAuth2RequestInterceptor oauth2RequestInterceptor =
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
    /// Signed request for getting access token for an installed application.
    /// </summary>
    private const string FETCH_ACCESS_TOKEN_REQUEST = "code=https%3a%2f%2faccounts.goog" +
        "le.com%2fo%2foauth2%2fauth%3fscope%3dOAuth2Scope%26state%3d%26redirect_uri%3dO" +
        "Auth2RedirectUri%26response_type%3dcode%26client_id%3dOAuth2ClientId%26access_" +
        "type%3doffline&client_id=OAuth2ClientId&client_secret=OAuth2ClientSecret&redir" +
        "ect_uri=OAuth2RedirectUri&grant_type=authorization_code";

    /// <summary>
    /// Signed request for rereshing access token for a service account.
    /// </summary>
    private const string REFRESH_ACCESS_TOKEN_REQUEST = "client_id=OAuth2ClientId&clien" +
        "t_secret=OAuth2ClientSecret&refresh_token=" +
        OAuth2RequestInterceptor.REFRESH_TOKEN + "&grant_type=refresh_token";

    /// <summary>
    /// Authorization url for OAuth2.
    /// </summary>
    private const string AUTHORIZATION_URL = "https://accounts.google.com/o/oauth2/auth?" +
        "scope=OAuth2Scope&state=&redirect_uri=OAuth2RedirectUri&response_type=code&clie" +
        "nt_id=OAuth2ClientId&access_type=offline";

    /// <summary>
    /// OAuth2 url for revoking a refresh token.
    /// </summary>
    private const string REVOKE_REFRESH_TOKEN_URL = "https://accounts.google.com/o/oauth" +
        "2/revoke?token=" + OAuth2RequestInterceptor.REFRESH_TOKEN;

    /// <summary>
    /// OAuth2 authorization header.
    /// </summary>
    private const String AUTHORIZATION_HEADER = "Bearer OAuth2AccessToken";

    /// <summary>
    /// OAuth2 authorization code.
    /// </summary>
    private const string AUTHORIZATION_CODE = "AuthorizationCode";

    /// <summary>
    /// Initializes the test case.
    /// </summary>
    [SetUp]
    public void Init() {
      tblSettings = new Hashtable();
      tblSettings.Add("LogPath", "Test LogPath");
      tblSettings.Add("LogToConsole", "false");
      tblSettings.Add("LogToFile", "false");
      tblSettings.Add("LogErrorsOnly", "false");
      tblSettings.Add("ProxyServer", "http://localhost/");
      tblSettings.Add("ProxyUser", "ProxyUser");
      tblSettings.Add("ProxyPassword", "ProxyPassword");
      tblSettings.Add("ProxyDomain", "ProxyDomain");
      tblSettings.Add("MaskCredentials", "false");
      tblSettings.Add("Timeout", "20");
      tblSettings.Add("RetryCount", "5");

      tblSettings.Add("OAuthConsumerKey", "OAuthConsumerKey");
      tblSettings.Add("OAuthConsumerSecret", "OAuthConsumerSecret");
      tblSettings.Add("OAuthScope", "OAuthScope");
      tblSettings.Add("OAuth2ClientId", "OAuth2ClientId");
      tblSettings.Add("OAuth2ClientSecret", "OAuth2ClientSecret");
      tblSettings.Add("OAuth2ServiceAccountEmail", "OAuth2ServiceAccountEmail");
      tblSettings.Add("OAuth2PrnEmail", "OAuth2PrnEmail");

      tblSettings.Add("OAuth2AccessToken", "OAuth2AccessToken");
      tblSettings.Add("OAuth2RefreshToken", "OAuth2RefreshToken");
      tblSettings.Add("OAuth2Scope", "OAuth2Scope");
      tblSettings.Add("OAuth2RedirectUri", "OAuth2RedirectUri");
      string tempCertificatePath = Path.GetTempFileName();
      using (FileStream fs = File.OpenWrite(tempCertificatePath)) {
        fs.Write(TestResources.certificate, 0, TestResources.certificate.Length);
      }
      tblSettings.Add("OAuth2JwtCertificatePath", tempCertificatePath);
      tblSettings.Add("OAuth2JwtCertificatePassword", "notasecret");

      appConfig = new MockAppConfig();
      appConfig.MockReadSettings(tblSettings);
      provider = new OAuth2Provider(appConfig);
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
      provider = new OAuth2Provider(appConfig);
      Assert.AreEqual(provider.ClientId, appConfig.OAuth2ClientId);
      Assert.AreEqual(provider.ClientSecret, appConfig.OAuth2ClientSecret);
      Assert.AreEqual(provider.AccessToken, appConfig.OAuth2AccessToken);
      Assert.AreEqual(provider.RefreshToken, appConfig.OAuth2RefreshToken);
      Assert.AreEqual(provider.Scope, appConfig.OAuth2Scope);
      Assert.AreEqual(provider.RedirectUri, appConfig.OAuth2RedirectUri);
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
      oauth2RequestInterceptor.RequestType =
          OAuth2RequestInterceptor.OAuth2RequestType.FetchAccessTokenForServiceAccount;
      WebRequestInterceptor.OnBeforeSendResponse callback = delegate(Uri uri,
          WebHeaderCollection headers, String body) {
        Assert.AreEqual(SERVICE_ACCOUNT_REQUEST, body);
      };
      try {
        oauth2RequestInterceptor.BeforeSendResponse += callback;
        provider.GenerateAccessTokenForServiceAccount();
        Assert.AreEqual(provider.AccessToken, OAuth2RequestInterceptor.ACCESS_TOKEN);
        Assert.AreEqual(provider.TokenType, OAuth2RequestInterceptor.ACCESS_TOKEN_TYPE);
        Assert.AreEqual(provider.ExpiresIn.ToString(), OAuth2RequestInterceptor.EXPIRES_IN);
      } finally {
        oauth2RequestInterceptor.BeforeSendResponse -= callback;
      }
    }

    /// <summary>
    /// Tests if we can the fetch access and refresh tokens for installed
    /// clients.
    /// </summary>
    [Test]
    public void TestFetchAccessAndRefreshTokens() {
      oauth2RequestInterceptor.RequestType =
          OAuth2RequestInterceptor.OAuth2RequestType.FetchAccessAndRefreshToken;
      WebRequestInterceptor.OnBeforeSendResponse callback = delegate(Uri uri,
          WebHeaderCollection headers, String body) {
        Assert.AreEqual(FETCH_ACCESS_TOKEN_REQUEST, body);
      };
      try {
        oauth2RequestInterceptor.BeforeSendResponse += callback;
        provider.FetchAccessAndRefreshTokens(AUTHORIZATION_URL);
        Assert.AreEqual(provider.AccessToken, OAuth2RequestInterceptor.ACCESS_TOKEN);
        Assert.AreEqual(provider.RefreshToken, OAuth2RequestInterceptor.REFRESH_TOKEN);
        Assert.AreEqual(provider.TokenType, OAuth2RequestInterceptor.ACCESS_TOKEN_TYPE);
        Assert.AreEqual(provider.ExpiresIn.ToString(), OAuth2RequestInterceptor.EXPIRES_IN);
      } finally {
        oauth2RequestInterceptor.BeforeSendResponse -= callback;
      }
    }

    /// <summary>
    /// Tests if we can refresh an access token for installed clients.
    /// </summary>
    [Test]
    public void TestRefreshAccessToken() {
      oauth2RequestInterceptor.RequestType =
          OAuth2RequestInterceptor.OAuth2RequestType.RefreshAccessToken;
      WebRequestInterceptor.OnBeforeSendResponse callback = delegate(Uri uri,
          WebHeaderCollection headers, String body) {
        Assert.AreEqual(REFRESH_ACCESS_TOKEN_REQUEST, body);
      };
      try {
        provider.RefreshToken = OAuth2RequestInterceptor.REFRESH_TOKEN;
        oauth2RequestInterceptor.BeforeSendResponse += callback;
        provider.RefreshAccessToken();
        Assert.AreEqual(provider.RefreshToken, OAuth2RequestInterceptor.REFRESH_TOKEN);
        Assert.AreEqual(provider.TokenType, OAuth2RequestInterceptor.ACCESS_TOKEN_TYPE);
        Assert.AreEqual(provider.ExpiresIn.ToString(), OAuth2RequestInterceptor.EXPIRES_IN);
      } finally {
        oauth2RequestInterceptor.BeforeSendResponse -= callback;
      }
    }

    /// <summary>
    /// Tests if we can revoke a refresh token.
    /// </summary>
    [Test]
    public void TestRevokeRefreshToken() {
      oauth2RequestInterceptor.RequestType =
          OAuth2RequestInterceptor.OAuth2RequestType.RefreshAccessToken;
      WebRequestInterceptor.OnBeforeSendResponse callback = delegate(Uri uri,
          WebHeaderCollection headers, String body) {
        Assert.AreEqual(uri.AbsoluteUri, REVOKE_REFRESH_TOKEN_URL);
      };
      try {
        provider.RefreshToken = OAuth2RequestInterceptor.REFRESH_TOKEN;
        oauth2RequestInterceptor.BeforeSendResponse += callback;
        provider.RevokeRefreshToken();
      } finally {

        oauth2RequestInterceptor.BeforeSendResponse -= callback;
      }
    }

    /// <summary>
    /// Tests if we can retrieve an authorization url for OAuth2 authorization
    /// process.
    /// </summary>
    [Test]
    public void TestGetAuthorizationUrl() {
      provider = new OAuth2Provider(appConfig);
      Assert.AreEqual(AUTHORIZATION_URL, provider.GetAuthorizationUrl());
    }

    /// <summary>
    /// Tests if we can retrieve an OAuth2 authorization header.
    /// </summary>
    [Test]
    public void TestGetAuthHeader() {
      provider = new OAuth2Provider(appConfig);
      Assert.AreEqual(AUTHORIZATION_HEADER, provider.GetAuthHeader(null));
    }

    /// <summary>
    /// Tests the property setters and getters.
    /// </summary>
    [Test]
    public void TestPropertySettersAndGetters() {
      provider = new OAuth2Provider(appConfig);

      provider.ClientId = "TEST_CLIENT_ID";
      Assert.AreEqual(provider.ClientId, "TEST_CLIENT_ID");

      provider.ClientSecret = "TEST_CLIENT_SECRET";
      Assert.AreEqual(provider.ClientSecret, "TEST_CLIENT_SECRET");

      provider.AccessToken = "TEST_ACCESS_TOKEN";
      Assert.AreEqual(provider.AccessToken, "TEST_ACCESS_TOKEN");

      provider.RefreshToken = "TEST_REFRESH_TOKEN";
      Assert.AreEqual(provider.RefreshToken, "TEST_REFRESH_TOKEN");

      provider.Scope = "TEST_SCOPE";
      Assert.AreEqual(provider.Scope, "TEST_SCOPE");

      provider.State = "TEST_STATE";
      Assert.AreEqual(provider.State, "TEST_STATE");

      provider.IsOffline = false;
      Assert.AreEqual(provider.IsOffline, false);

      OAuthTokensObtainedCallback callback = delegate(AdsOAuthProvider provider1) {
      };

      provider.OnOAuthTokensObtained = callback;
      Assert.AreEqual(provider.OnOAuthTokensObtained, callback);

      provider.IsOffline = false;
      Assert.AreEqual(provider.IsOffline, false);

      provider.RedirectUri = "TEST_REDIRECT_URI";
      Assert.AreEqual(provider.RedirectUri, "TEST_REDIRECT_URI");

      provider.ServiceAccountEmail = "TEST_SERVICE_ACCOUNT_EMAIL";
      Assert.AreEqual(provider.ServiceAccountEmail, "TEST_SERVICE_ACCOUNT_EMAIL");

      provider.PrnEmail = "TEST_PRN_EMAIL";
      Assert.AreEqual(provider.PrnEmail, "TEST_PRN_EMAIL");

      provider.JwtCertificatePath = "TEST_JWT_CERTIFICATE_PATH";
      Assert.AreEqual(provider.JwtCertificatePath, "TEST_JWT_CERTIFICATE_PATH");

      provider.JwtCertificatePassword = "TEST_JWT_CERTIFICATE_PASSWORD";
      Assert.AreEqual(provider.JwtCertificatePassword, "TEST_JWT_CERTIFICATE_PASSWORD");
    }
  }
}
