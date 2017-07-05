// Copyright 2014, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.Common.Logging;
using Google.Api.Ads.Common.Util;

using NUnit.Framework;

using System.Collections.Generic;

namespace Google.Api.Ads.Common.Tests.Util {

  /// <summary>
  /// UnitTests for <see cref="DefaultBodyFormatter"/> class.
  /// </summary>
  [TestFixture]
  public class DefaultBodyFormatterTests {
    /// <summary>
    /// The request body to be used for testing.
    /// </summary>
    private const string BODY = "KEY1=foo;KEY2=bar";

    /// <summary>
    /// The keys to be masked in the request.
    /// </summary>
    private ISet<string> KEYS = new HashSet<string>() { "KEY1", "KEY2" };

    /// <summary>
    /// Test for DefaultBodyFormatter.MaskContents method.
    /// </summary>
    [Test]
    public void TestMaskContents() {
      Assert.AreEqual(BODY, new DefaultBodyFormatter().MaskContents(BODY, KEYS));
    }
  }
}