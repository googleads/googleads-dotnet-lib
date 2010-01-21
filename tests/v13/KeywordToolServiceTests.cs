// Copyright 2010, Google Inc. All Rights Reserved.
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

using com.google.api.adwords.lib;
using com.google.api.adwords.v13;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Text;

namespace com.google.api.adwords.tests.v13 {
  /// <summary>
  /// UnitTests for KeywordToolService class.
  /// </summary>
  [TestFixture]
  class KeywordToolServiceTests : BaseTests {
        /// <summary>
    /// KeywordToolService object to be used in this test.
    /// </summary>
    KeywordToolService keywordToolService;

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public KeywordToolServiceTests()
      : base() {
    }

    /// <summary>
    /// Initialize the test case.
    /// </summary>
    [SetUp]
    public void Init() {
      AdWordsUser user = new AdWordsUser();
      keywordToolService = (KeywordToolService) user.GetService(
          AdWordsService.v13.KeywordToolService);
    }

    /// <summary>
    /// Test whether we can fetch keywords from site.
    /// </summary>
    [Test]
    public void TestGetKeywordsFromSite() {
      Assert.That(keywordToolService.getKeywordsFromSite("www.google.com", false,
        new string[]{"en"}, new string[] {"US"}) is SiteKeywordGroups);
    }

    /// <summary>
    /// Test whether we can fetch keyword variations for a list of seed
    /// keywords.
    /// </summary>
    [Test]
    public void TestGetKeywordVariations() {
      SeedKeyword seedKeyword1 = new SeedKeyword();
      seedKeyword1.negativeSpecified = true;
      seedKeyword1.negative = false;
      seedKeyword1.type = KeywordType.Broad;
      seedKeyword1.text = "Flowers";

      SeedKeyword seedKeyword2 = new SeedKeyword();
      seedKeyword2.negativeSpecified = true;
      seedKeyword2.negative = false;
      seedKeyword2.type = KeywordType.Broad;
      seedKeyword2.text = "House";

      Assert.That(keywordToolService.getKeywordVariations(new SeedKeyword[]
          {seedKeyword1, seedKeyword2}, true, new string[] {"en"}, new string[] {"US"})
          is KeywordVariations);
    }
  }
}
