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
using com.google.api.adwords.v201008;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Text;

namespace com.google.api.adwords.tests.v201008 {
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
      mediaService = (MediaService)user.GetService(AdWordsService.v201008.MediaService);
    }

    /// <summary>
    /// Test whether we can fetch all existing image media.
    /// </summary>
    [Test]
    public void TestGetAllImageMedia() {
      // Create selector.
      MediaSelector selector = new MediaSelector();
      selector.mediaType = MediaMediaType.IMAGE;
      selector.mediaTypeSpecified = true;

      // Get all images.
      MediaPage page = mediaService.get(selector);

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
      image.typeSpecified = true;
      image.name = "Sample Image #" + utils.GetTimeStamp();

      // Upload image.
      Media[] result = mediaService.upload(new Media[] { image });

      Assert.NotNull(result);
      Assert.AreEqual(result.Length, 1);
      Assert.NotNull(result[0]);
    }
  }
}
