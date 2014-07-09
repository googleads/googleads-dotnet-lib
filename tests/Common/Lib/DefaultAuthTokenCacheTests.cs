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

using NUnit.Framework;

using System;

// Disable deprecation warnings for DefaultAuthTokenCache class.
#pragma warning disable 612, 618

namespace Google.Api.Ads.Common.Tests.Lib {
  /// <summary>
  /// Tests for DefaultAuthTokenCache class.
  /// </summary>
  class DefaultAuthTokenCacheTests {
    /// <summary>
    /// The service name.
    /// </summary>
    private const string SERVICE = "adwords";

    /// <summary>
    /// Login email.
    /// </summary>
    private const string EMAIL = "testemail";

    /// <summary>
    /// AuthToken for test purposes.
    /// </summary>
    private const string AUTHTOKEN = "AUTHTOKEN";

    /// <summary>
    /// Cache instance for testing purposes.
    /// </summary>
    private DefaultAuthTokenCache cache = new DefaultAuthTokenCache();

    /// <summary>
    /// Inits this instance.
    /// </summary>
    [SetUp]
    public void Init() {
      cache.Clear();
    }

    /// <summary>
    /// Tests if we can add a token.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestAddToken() {
      string token = cache.AddToken(SERVICE, EMAIL, AUTHTOKEN);
      Assert.AreEqual(AUTHTOKEN, token);
    }

    /// <summary>
    /// Tests if arguments are validated when token is added to cache.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestAddTokenValidatesParams() {
      Assert.Throws(typeof(ArgumentException), delegate() {
        cache.AddToken(null, EMAIL, AUTHTOKEN);
      });
      Assert.Throws(typeof(ArgumentException), delegate() {
        cache.AddToken(SERVICE, null, AUTHTOKEN);
      });
    }

    /// <summary>
    /// Tests if we can retrieve a token after adding it.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestGetToken() {
      cache.AddToken(SERVICE, EMAIL, AUTHTOKEN);
      Assert.AreEqual(AUTHTOKEN, cache.GetToken(SERVICE, EMAIL));
    }

    /// <summary>
    /// Tests if we can clear the cache.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestClear() {
      Assert.Null(cache.GetToken(SERVICE, EMAIL));
    }

    /// <summary>
    /// Tests if we can invalidate a token.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestInvalidateToken() {
      cache.AddToken(SERVICE, EMAIL, AUTHTOKEN);
      Assert.AreEqual(AUTHTOKEN, cache.GetToken(SERVICE, EMAIL));
      cache.InvalidateToken(AUTHTOKEN);
      Assert.Null(cache.GetToken(SERVICE, EMAIL));
    }
  }
}

#pragma warning restore 612, 618
