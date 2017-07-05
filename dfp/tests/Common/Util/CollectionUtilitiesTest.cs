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

using Google.Api.Ads.Common.Util;

using NUnit.Framework;

using System.Collections.Generic;

namespace Google.Api.Ads.Common.Tests.Util
{
  /// <summary>
  /// UnitTests for <see cref="CollectionUtilitiesTest"/> class.
  /// </summary>
  [TestFixture]
  public class CollectionUtilitiesTest
  {
    const long value = long.MinValue;
    const string invalidKey = "INVALID_KEY";
    const string validKey = "VALID_KEY";

    Dictionary<string, long> dictionary;

    /// <summary>
    /// Inits this instance.
    /// </summary>
    [SetUp]
    public void Init() {
      dictionary = new Dictionary<string, long>() {
        {validKey, value}
      };
    }

    /// <summary>
    /// Test for CollectionUtilities.TryGetValue()
    /// </summary>
    [Test]
    public void TestTryGetValue() {
      // Ensure that requesting an invalid key with no default specified returns the default
      // value for that type.
      Assert.AreEqual(0L, CollectionUtilities.TryGetValue(dictionary, invalidKey));

      long validValue = dictionary[validKey];

      // Ensure requesting a valid key returns the expected value.
      Assert.AreEqual(validValue, CollectionUtilities.TryGetValue(dictionary, validKey));

      // Ensure requesting an invalid key with a default specified returns the specified default.
      Assert.AreEqual(validValue,
          CollectionUtilities.TryGetValue(dictionary, invalidKey, validValue));
    }
  }
}
