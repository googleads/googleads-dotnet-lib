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
  /// Tests for OAuth2ProviderForApplications class.
  /// </summary>
  public class OAuth2ProviderForApplicationsTests {
    private OAuth2ProviderForApplications provider;

    private MockAppConfig appConfig;
    private OAuth2RequestInterceptor oauth2RequestInterceptor =
        (OAuth2RequestInterceptor) OAuth2RequestInterceptor.Instance;

    /// <summary>
    /// The hashtable to hold the test data.
    /// </summary>
    private Hashtable tblSettings;

    // Keys for TestPropertySettersAndGetters.
    private const string TEST_CLIENT_ID = "TEST_CLIENT_ID";
    private const string TEST_CLIENT_SECRET = "TEST_CLIENT_SECRET";
    private const string TEST_ACCESS_TOKEN = OAuth2RequestInterceptor.TEST_ACCESS_TOKEN;
    private const string TEST_REFRESH_TOKEN = OAuth2RequestInterceptor.TEST_REFRESH_TOKEN;
    private const string TEST_SCOPE = "TEST_SCOPE";
    private const string TEST_STATE = "TEST_STATE";
    private const bool TEST_OFFLINE = false;
    private readonly OAuthTokensObtainedCallback TEST_CALLBACK =
        delegate(AdsOAuthProvider provider) {};
    private const string TEST_REDIRECT_URI = "TEST_REDIRECT_URI";

    /// <summary>
    /// Signed request for getting access token for an installed application.
    /// </summary>
    private const string FETCH_ACCESS_TOKEN_REQUEST = "code=https%3a%2f%2faccounts.goog" +
        "le.com%2fo%2foauth2%2fauth%3fscope%3dOAuth2Scope%26state%3d%26redirect_uri%3dO" +
        "Auth2RedirectUri%26response_type%3dcode%26client_id%3dOAuth2ClientId%26access_" +
        "type%3doffline&client_id=OAuth2ClientId&client_secret=OAuth2ClientSecret&redir" +
        "ect_uri=OAuth2RedirectUri&grant_type=authorization_code";

    /// <summary>
    /// Authorization url for OAuth2.
    /// </summary>
    private const string AUTHORIZATION_URL = "https://accounts.google.com/o/oauth2/auth?" +
        "scope=OAuth2Scope&state=&redirect_uri=OAuth2RedirectUri&response_type=code&clie" +
        "nt_id=OAuth2ClientId&access_type=offline";

    /// <summary>
    /// Signed request for rereshing access token for an installed application.
    /// </summary>
    private const string REFRESH_ACCESS_TOKEN_REQUEST = "client_id=OAuth2ClientId&clien" +
        "t_secret=OAuth2ClientSecret&refresh_token=" + TEST_REFRESH_TOKEN +
        "&grant_type=refresh_token";

    /// <summary>
    /// OAuth2 url for revoking a refresh token.
    /// </summary>
    private const string REVOKE_REFRESH_TOKEN_URL = "https://accounts.google.com/o/oauth" +
        "2/revoke?token=" + TEST_REFRESH_TOKEN;

    /// <summary>
    /// OAuth2 authorization header.
    /// </summary>
    private const String AUTHORIZATION_HEADER = "Bearer " + TEST_ACCESS_TOKEN;

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

      tblSettings.Add("OAuth2ClientId", "OAuth2ClientId");
      tblSettings.Add("OAuth2ClientSecret", "OAuth2ClientSecret");
      tblSettings.Add("OAuth2AccessToken", "OAuth2AccessToken");
      tblSettings.Add("OAuth2RefreshToken", "OAuth2RefreshToken");
      tblSettings.Add("OAuth2Scope", "OAuth2Scope");
      tblSettings.Add("OAuth2RedirectUri", "OAuth2RedirectUri");

      appConfig = new MockAppConfig();
      appConfig.MockReadSettings(tblSettings);
      provider = new OAuth2ProviderForApplications(appConfig);
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
      provider = new OAuth2ProviderForApplications(appConfig);
      Assert.AreEqual(provider.ClientId, appConfig.OAuth2ClientId);
      Assert.AreEqual(provider.ClientSecret, appConfig.OAuth2ClientSecret);
      Assert.AreEqual(provider.AccessToken, appConfig.OAuth2AccessToken);
      Assert.AreEqual(provider.RefreshToken, appConfig.OAuth2RefreshToken);
      Assert.AreEqual(provider.Scope, appConfig.OAuth2Scope);
      Assert.AreEqual(provider.RedirectUri, appConfig.OAuth2RedirectUri);
    }

    /// <summary>
    /// Tests the OAuth2 access token expiration logic.
    /// </summary>
    [Test]
    public void TestIsAccessTokenExpiring() {
      MethodInfo mi = typeof(OAuth2ProviderBase).GetMethod("IsAccessTokenExpiring",
          BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy);

      provider.OAuth2RefreshCutoffLimit = 60;

      // Test if token is not expired if there's > 60 seconds remaining.
      provider.UpdatedOn = DateTime.Now.Subtract(new TimeSpan(0, 0, 1800));
      provider.ExpiresIn = 3600;
      Assert.False((bool) mi.Invoke(provider, null));

      // Test if token is expired if there's 60 seconds or lesser remaining.
      provider.UpdatedOn = DateTime.Now.Subtract(new TimeSpan(0, 0, 3600));
      provider.ExpiresIn = 3600 - provider.OAuth2RefreshCutoffLimit;
      Assert.True((bool) mi.Invoke(provider, null));

      // Test if token is expired if there's no time left for expiration.
      provider.UpdatedOn = DateTime.Now.Subtract(new TimeSpan(0, 0, 3700));
      provider.ExpiresIn = 3600;
      Assert.True((bool) mi.Invoke(provider, null));
    }

    /// <summary>
    /// Tests if we can the fetch access and refresh tokens for installed
    /// clients.
    /// </summary>
    [Test]
    public void TestFetchAccessAndRefreshTokens() {
      TestUtils.ValidateRequiredParameters(provider, new string[] {"ClientId", "ClientSecret"},
          delegate() {
            provider.RefreshAccessToken();
          }
      );

      oauth2RequestInterceptor.RequestType =
          OAuth2RequestInterceptor.OAuth2RequestType.FetchAccessAndRefreshToken;
      WebRequestInterceptor.OnBeforeSendResponse callback = delegate(Uri uri,
          WebHeaderCollection headers, String body) {
        Assert.AreEqual(FETCH_ACCESS_TOKEN_REQUEST, body);
      };
      try {
        oauth2RequestInterceptor.BeforeSendResponse += callback;
        provider.FetchAccessAndRefreshTokens(AUTHORIZATION_URL);
        Assert.AreEqual(provider.AccessToken, OAuth2RequestInterceptor.TEST_ACCESS_TOKEN);
        Assert.AreEqual(provider.RefreshToken, OAuth2RequestInterceptor.TEST_REFRESH_TOKEN);
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
      TestUtils.ValidateRequiredParameters(provider, new string[] {"RefreshToken"}, delegate() {
        provider.RefreshAccessToken();
      });
      provider.IsOffline = false;
      Assert.Throws<ArgumentException>(delegate() {
        provider.RefreshAccessToken();
      });
      provider.IsOffline = true;

      oauth2RequestInterceptor.RequestType =
          OAuth2RequestInterceptor.OAuth2RequestType.RefreshAccessToken;
      WebRequestInterceptor.OnBeforeSendResponse callback = delegate(Uri uri,
          WebHeaderCollection headers, String body) {
        Assert.AreEqual(REFRESH_ACCESS_TOKEN_REQUEST, body);
      };
      try {
        provider.RefreshToken = TEST_REFRESH_TOKEN;
        oauth2RequestInterceptor.BeforeSendResponse += callback;
        provider.RefreshAccessToken();
        Assert.AreEqual(provider.RefreshToken, TEST_REFRESH_TOKEN);
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
      TestUtils.ValidateRequiredParameters(provider, new string[] {"RefreshToken"}, delegate() {
        provider.RevokeRefreshToken();
      });

      oauth2RequestInterceptor.RequestType =
          OAuth2RequestInterceptor.OAuth2RequestType.RefreshAccessToken;
      WebRequestInterceptor.OnBeforeSendResponse callback = delegate(Uri uri,
          WebHeaderCollection headers, String body) {
        Assert.AreEqual(uri.AbsoluteUri, REVOKE_REFRESH_TOKEN_URL);
      };
      try {
        provider.RefreshToken = OAuth2RequestInterceptor.TEST_REFRESH_TOKEN;
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
      provider = new OAuth2ProviderForApplications(appConfig);

      TestUtils.ValidateRequiredParameters(provider, new string[] {"ClientId", "Scope"},
          delegate() {
            provider.GetAuthorizationUrl();
          }
      );

      Assert.AreEqual(AUTHORIZATION_URL, provider.GetAuthorizationUrl());
    }

    /// <summary>
    /// Tests if we can retrieve an OAuth2 authorization header.
    /// </summary>
    [Test]
    public void TestGetAuthHeader() {
      provider = new OAuth2ProviderForApplications(appConfig);
      Assert.AreEqual(AUTHORIZATION_HEADER, provider.GetAuthHeader());
    }

    /// <summary>
    /// Tests the property setters and getters.
    /// </summary>
    [Test]
    public void TestPropertySettersAndGetters() {
      provider = new OAuth2ProviderForApplications(appConfig);

      provider.ClientId = TEST_CLIENT_ID;
      Assert.AreEqual(provider.ClientId, TEST_CLIENT_ID);

      provider.ClientSecret = TEST_CLIENT_SECRET;
      Assert.AreEqual(provider.ClientSecret, TEST_CLIENT_SECRET);

      provider.AccessToken = TEST_ACCESS_TOKEN;
      Assert.AreEqual(provider.AccessToken, TEST_ACCESS_TOKEN);

      provider.RefreshToken = TEST_REFRESH_TOKEN;
      Assert.AreEqual(provider.RefreshToken, TEST_REFRESH_TOKEN);

      provider.Scope = TEST_SCOPE;
      Assert.AreEqual(provider.Scope, TEST_SCOPE);

      provider.State = TEST_STATE;
      Assert.AreEqual(provider.State, TEST_STATE);

      provider.IsOffline = TEST_OFFLINE;
      Assert.AreEqual(provider.IsOffline, TEST_OFFLINE);

      provider.OnOAuthTokensObtained = TEST_CALLBACK;
      Assert.AreEqual(provider.OnOAuthTokensObtained, TEST_CALLBACK);

      provider.IsOffline = TEST_OFFLINE;
      Assert.AreEqual(provider.IsOffline, TEST_OFFLINE);

      provider.RedirectUri = TEST_REDIRECT_URI;
      Assert.AreEqual(provider.RedirectUri, TEST_REDIRECT_URI);
    }
  }
}
