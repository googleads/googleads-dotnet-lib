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

using Google.Api.Ads.Common.Lib;
using Google.Api.Ads.Common.Tests.Mocks;

using NUnit.Framework;

// Disable deprecation warnings for AuthToken class.
#pragma warning disable 612, 618

namespace Google.Api.Ads.Common.Tests.Lib {
  /// <summary>
  /// Tests for AuthToken class.
  /// </summary>
  class AuthTokenTest {
    /// <summary>
    /// Mock config instance for testing purposes.
    /// </summary>
    private readonly AppConfig config = new MockAppConfig();
    /// <summary>
    /// Mock cache for testing purposes.
    /// </summary>
    private readonly AuthTokenCache cache = new MockAuthTokenCache();

    /// <summary>
    /// Service name.
    /// </summary>
    private const string SERVICE = "adwords";

    /// <summary>
    /// Login email.
    /// </summary>
    private const string EMAIL = "testemail";

    /// <summary>
    /// Login password.
    /// </summary>
    private const string PASSWORD = "testpassword";

    /// <summary>
    /// WebRequestInterceptor for intercepting calls to ClientLogin service.
    /// </summary>
    protected ClientLoginRequestInterceptor clientLoginInterceptor =
      ClientLoginRequestInterceptor.Instance as ClientLoginRequestInterceptor;

    /// <summary>
    /// Old cache before mocking.
    /// </summary>
    AuthTokenCache oldCache;

    /// <summary>
    /// Inits the tests.
    /// </summary>
    [SetUp]
    public void Init() {
      config.Email = EMAIL;
      config.Password = PASSWORD;
      oldCache = AuthToken.Cache;
      AuthToken.Cache = cache;
      clientLoginInterceptor.Intercept = true;
    }

    /// <summary>
    /// Tears down the tests.
    /// </summary>
    [TearDown]
    public void TearDown() {
      AuthToken.Cache = oldCache;
      clientLoginInterceptor.Intercept = false;
    }

    /// <summary>
    /// Tests the default constructor.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestDefaultConstructor() {
      AuthToken authToken = new AuthToken();
      Assert.Null(authToken.Config);
      Assert.Null(authToken.Service);
    }

    /// <summary>
    /// Tests the overloaded constructor.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestOverloadedConstructor() {
      AuthToken authToken = new AuthToken(config, SERVICE);
      Assert.AreEqual(authToken.Config, config);
      Assert.AreEqual(authToken.Service, SERVICE);
    }

    /// <summary>
    /// Tests the property setters and getters.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestProperties() {
      AuthToken authToken = new AuthToken();
      authToken.Email = EMAIL;
      Assert.AreEqual(authToken.Email, EMAIL);

      authToken.Password = PASSWORD;
      Assert.AreEqual(authToken.Password, PASSWORD);

      authToken.Service = SERVICE;
      Assert.AreEqual(authToken.Service, SERVICE);
    }

    /// <summary>
    /// Tests the cache setter and getter methods.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestCacheSetterGetter() {
      MockAuthTokenCache testCache = new MockAuthTokenCache();
      AuthToken.Cache = testCache;
      Assert.AreEqual(AuthToken.Cache, testCache);
    }

    /// <summary>
    /// Tests if we can get an authToken successfully.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestGetToken() {
      AuthToken authToken = new AuthToken(config, SERVICE);
      TestUtils.ValidateRequiredParameters(authToken, new string[] {"Email", "Password"},
          delegate() {
            authToken.GetToken();
          }
      );
      string token = authToken.GetToken();
      Assert.AreEqual(token, ClientLoginRequestInterceptor.AUTH_TOKEN);
    }

    /// <summary>
    /// Tests if we can get an AuthTokenException correctly when the server
    /// throws an exception.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestGetTokenThrowsException() {
      try {
        clientLoginInterceptor.RaiseException = true;
        try {
          AuthToken authToken = new AuthToken(config, SERVICE);
          string token = authToken.GetToken();
          Assert.Fail();
        } catch (AuthTokenException ex) {
          Assert.AreEqual(ex.ErrorCode, AuthTokenErrorCode.BadAuthentication);
        }
      } finally {
        clientLoginInterceptor.RaiseException = false;
      }
    }
  }
}

#pragma warning restore 612, 618
