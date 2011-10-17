// Copyright 2011, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201109;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.AdWords.Tests.v201109 {
  /// <summary>
  /// UnitTests for <see cref="MediaService"/> class.
  /// </summary>
  [TestFixture]
  class MediaServiceTests : BaseTests {
    /// <summary>
    /// MediaService object to be used in this test.
    /// </summary>
    private MediaService mediaService;

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public MediaServiceTests() : base() {
    }

    /// <summary>
    /// Initialize the test case.
    /// </summary>
    [SetUp]
    public void Init() {
      mediaService = (MediaService) user.GetService(AdWordsService.v201109.MediaService);

      // Create image.
      TestUtils utils = new TestUtils();
      Image image = new Image();
      image.data = utils.GetSandboxImage();
      image.type = MediaMediaType.IMAGE;

      // Upload image.
      mediaService.upload(new Media[] {image});
    }

    /// <summary>
    /// Test whether we can fetch all existing image media.
    /// </summary>
    [Test]
    public void TestGetAllImageMedia() {
      // Create a selector.
      Selector selector = new Selector();
      selector.fields = new string[] {"MediaId", "Type", "Width", "Height", "MimeType"};

      // Create a filter.
      Predicate predicate = new Predicate();
      predicate.@operator = PredicateOperator.EQUALS;
      predicate.field = "Type";
      predicate.values = new string[] {MediaMediaType.IMAGE.ToString()};

      selector.predicates = new Predicate[] {predicate};

      // Get all images.
      MediaPage page = null;

      Assert.DoesNotThrow(delegate() {
        page = mediaService.get(selector);
      });

      Assert.NotNull(page);
      Assert.NotNull(page.entries);
    }

    /// <summary>
    /// Test whether we can fetch all existing image media.
    /// </summary>
    [Test]
    public void TestUploadImageMedia() {
      // Create image.
      TestUtils utils = new TestUtils();
      Image image = new Image();
      image.data = utils.GetSandboxImage();
      image.type = MediaMediaType.IMAGE;

      // Upload image.
      Media[] result = mediaService.upload(new Media[] {image});

      Assert.NotNull(result);
      Assert.AreEqual(result.Length, 1);
      Assert.NotNull(result[0]);
    }
  }
}
