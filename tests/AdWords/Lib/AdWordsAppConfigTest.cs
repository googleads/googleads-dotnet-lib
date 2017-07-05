// Copyright 2016, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.AdWords.Lib;

using System;

using NUnit.Framework;

namespace Google.Api.Ads.AdWords.Tests.Lib {

  /// <summary>
  /// Test cases for AdWordsAppConfig.
  /// </summary>
  internal class AdWordsAppConfigTests {
    /// <summary>
    /// A user agent string with control chars in it.
    /// </summary>
    private const string CONTROL_CHARS_USERAGENT = "Useragent \u0001";

    /// <summary>
    /// A user agent string with unicode characters in it.
    /// </summary>
    private const string UNICODE_USERAGENT = "Useragent \u1234";

    /// <summary>
    /// A user agent with only printable ASCII characters in it.
    /// </summary>
    private const string ASCII_USERAGENT = "Useragent";

    /// <summary>
    /// Tests to make sure User agent can be set properly, and neccessary
    /// validations are done.
    /// </summary>
    [Test]
    [Category("Small")]
    public void TestSetUserAgent() {
      AdWordsAppConfig config = new AdWordsAppConfig();

      Assert.Throws(typeof(ArgumentException), delegate() {
        config.UserAgent = CONTROL_CHARS_USERAGENT;
      });

      Assert.Throws(typeof(ArgumentException), delegate() {
        config.UserAgent = UNICODE_USERAGENT;
      });

      Assert.DoesNotThrow(delegate() {
        config.UserAgent = ASCII_USERAGENT;
      });
    }
  }
}
