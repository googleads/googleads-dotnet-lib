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

using System;
using Google.Api.Ads.Dfa.Lib;

// Disable deprecation warnings for DefaultAuthTokenCache class.
#pragma warning disable 612, 618

namespace Google.Api.Ads.Dfa.Tests.Lib {
  /// <summary>
  /// Tests for DefaultAuthTokenCache class.
  /// </summary>
  class DefaultLoginTokenCacheTests {
    /// <summary>
    /// Dfa user for testing purposes.
    /// </summary>
    private const string USERNAME = "testuser";

    /// <summary>
    /// Dfa password for testing purposes.
    /// </summary>
    private const string PASSWORD = "testpassword";

    /// <summary>
    /// LoginToken for test purposes.
    /// </summary>
    private readonly UserToken USERTOKEN = new UserToken(USERNAME, PASSWORD);

    /// <summary>
    /// Cache instance for testing purposes.
    /// </summary>
    private DefaultLoginTokenCache cache = new DefaultLoginTokenCache();

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
      UserToken token = cache.AddToken(USERNAME, USERTOKEN);
      Assert.AreEqual(USERTOKEN, token);
    }

    /// <summary>
    /// Tests if arguments are validated when token is added to cache.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestAddTokenValidatesParams() {
      Assert.Throws(typeof(ArgumentException), delegate() {
        cache.AddToken(null, USERTOKEN);
      });
    }

    /// <summary>
    /// Tests if we can retrieve a token after adding it.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestGetToken() {
      cache.AddToken(USERNAME, USERTOKEN);
      Assert.AreEqual(USERTOKEN, cache.GetToken(USERNAME));
    }

    /// <summary>
    /// Tests if we can clear the cache.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestClear() {
      Assert.Null(cache.GetToken(USERNAME));
    }

    /// <summary>
    /// Tests if we can invalidate a token.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestInvalidateToken() {
      cache.AddToken(USERNAME, USERTOKEN);
      Assert.AreEqual(USERTOKEN, cache.GetToken(USERNAME));
      cache.InvalidateToken(USERTOKEN);
      Assert.Null(cache.GetToken(USERNAME));
    }
  }
}

#pragma warning restore 612, 618
