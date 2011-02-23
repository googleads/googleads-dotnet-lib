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

using Google.Api.Ads.Common.Util;
using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.v201010;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Threading;


namespace Google.Api.Ads.Dfp.Tests.v201010 {
  /// <summary>
  /// UnitTests for <see cref="CreativeService"/> class.
  /// </summary>
  [TestFixture]
  public class CreativeServiceTests : BaseTests {
    /// <summary>
    /// UnitTests for <see cref="CreativeService"/> class.
    /// </summary>
    private CreativeService creativeService;

    /// <summary>
    /// The advertiser company id to be used for running tests.
    /// </summary>
    private long advertiserId = 0;

    /// <summary>
    /// The creative 1 to be used for running tests.
    /// </summary>
    private Creative creative1 = null;

    /// <summary>
    /// The creative 2 to be used for running tests.
    /// </summary>
    private Creative creative2 = null;

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public CreativeServiceTests() : base() {
    }

    /// <summary>
    /// Initialize the test case.
    /// </summary>
    [SetUp]
    public void Init() {
      TestUtils utils = new TestUtils();
      creativeService = (CreativeService)user.GetService(DfpService.v201010.CreativeService);
      advertiserId = utils.CreateCompany(user, CompanyType.ADVERTISER).id;
      creative1 = utils.CreateCreative(user, advertiserId);
      creative2 = utils.CreateCreative(user, advertiserId);
    }

    /// <summary>
    /// Test whether we can create a creative.
    /// </summary>
    [Test]
    public void TestCreateCreative() {
      ImageCreative imageCreative = new ImageCreative();
      imageCreative.name = string.Format("Image creative #{0}", new TestUtils().GetTimeStamp());
      imageCreative.advertiserId = advertiserId;
      imageCreative.destinationUrl = "http://www.google.com";
      imageCreative.imageName = "image.jpg";
      imageCreative.imageByteArray = MediaUtilities.GetAssetDataFromUrl(
          "http://www.google.com/intl/en/adwords/select/images/samples/inline.jpg");

      Size imageSize = new Size();
      imageSize.width = 300;
      imageSize.height = 250;

      imageCreative.size = imageSize;

      Creative newCreative = null;

      Assert.DoesNotThrow(delegate() {
        newCreative = creativeService.createCreative(imageCreative);
      });

      Assert.NotNull(newCreative);
      Assert.That(newCreative is ImageCreative);
      Assert.AreEqual(newCreative.advertiserId, imageCreative.advertiserId);
      Assert.AreEqual(newCreative.name, imageCreative.name);
    }

    /// <summary>
    /// Test whether we can create a list of creatives.
    /// </summary>
    [Test]
    public void TestCreateCreatives() {
      ImageCreative imageCreative1 = new ImageCreative();
      imageCreative1.name = string.Format("Image creative #{0}", new TestUtils().GetTimeStamp());
      imageCreative1.advertiserId = advertiserId;
      imageCreative1.destinationUrl = "http://www.google.com";
      imageCreative1.imageName = "image.jpg";
      imageCreative1.imageByteArray = MediaUtilities.GetAssetDataFromUrl(
          "http://www.google.com/intl/en/adwords/select/images/samples/inline.jpg");

      Size imageSize1 = new Size();
      imageSize1.width = 300;
      imageSize1.height = 250;

      imageCreative1.size = imageSize1;

      ImageCreative imageCreative2 = new ImageCreative();
      imageCreative2.name = string.Format("Image creative #{0}", new TestUtils().GetTimeStamp());
      imageCreative2.advertiserId = advertiserId;
      imageCreative2.destinationUrl = "http://www.google.com";
      imageCreative2.imageName = "image.jpg";
      imageCreative2.imageByteArray = MediaUtilities.GetAssetDataFromUrl(
          "http://www.google.com/intl/en/adwords/select/images/samples/skyscraper.jpg");

      Size imageSize2 = new Size();
      imageSize2.width = 120;
      imageSize2.height = 600;

      imageCreative2.size = imageSize2;

      Creative[] newCreatives = null;

      Assert.DoesNotThrow(delegate() {
        newCreatives = creativeService.createCreatives(new Creative[] {imageCreative1,
            imageCreative2});
      });

      Assert.NotNull(newCreatives);
      Assert.AreEqual(newCreatives.Length, 2);
      Assert.NotNull(newCreatives[0]);
      Assert.That(newCreatives[0] is ImageCreative);
      Assert.AreEqual(newCreatives[0].advertiserId, imageCreative1.advertiserId);
      Assert.AreEqual(newCreatives[0].name, imageCreative1.name);
      Assert.NotNull(newCreatives[1]);
      Assert.That(newCreatives[1] is ImageCreative);
      Assert.AreEqual(newCreatives[1].advertiserId, imageCreative2.advertiserId);
      Assert.AreEqual(newCreatives[1].name, imageCreative2.name);
    }

    /// <summary>
    /// Test whether we can fetch an existing creative.
    /// </summary>
    [Test]
    public void TestGetCreative() {
      Creative localCreative = null;
      Assert.DoesNotThrow(delegate() {
        localCreative = creativeService.getCreative(creative1.id);
      });
      Assert.NotNull(localCreative);
      Assert.AreEqual(localCreative.id, creative1.id);
      Assert.AreEqual(localCreative.GetType(), creative1.GetType());
      Assert.AreEqual(localCreative.name, creative1.name);
      Assert.AreEqual(localCreative.advertiserId, creative1.advertiserId);
      Assert.AreEqual(localCreative.previewUrl, creative1.previewUrl);
      Assert.AreEqual(localCreative.size.height, creative1.size.height);
      Assert.AreEqual(localCreative.size.width, creative1.size.width);
    }

    /// <summary>
    /// Test whether we can fetch a list of existing creatives that match given
    /// statement.
    /// </summary>
    [Test]
    public void TestGetCreativesByStatement() {
      Statement statement = new Statement();
      statement.query = string.Format("WHERE id = '{0}' LIMIT 500", creative1.id);

      CreativePage page = null;

      Assert.DoesNotThrow(delegate() {
        page = creativeService.getCreativesByStatement(statement);
      });
      Assert.NotNull(page);
      Assert.NotNull(page.results);
      Assert.AreEqual(page.totalResultSetSize, 1);
      Assert.NotNull(page.results[0]);
      Assert.AreEqual(page.results[0].id, creative1.id);
      Assert.AreEqual(page.results[0].GetType(), creative1.GetType());
      Assert.AreEqual(page.results[0].name, creative1.name);
      Assert.AreEqual(page.results[0].advertiserId, creative1.advertiserId);
      Assert.AreEqual(page.results[0].previewUrl, creative1.previewUrl);
      Assert.AreEqual(page.results[0].size.height, creative1.size.height);
      Assert.AreEqual(page.results[0].size.width, creative1.size.width);
    }

    /// <summary>
    /// Test whether we can update a creative.
    /// </summary>
    [Test]
    public void TestUpdateCreative() {
      ImageCreative imageCreative = (ImageCreative) creative1;
      imageCreative.destinationUrl = "http://news.google.com";

      Creative newCreative = null;

      Assert.DoesNotThrow(delegate() {
        newCreative = creativeService.updateCreative(creative1);
      });

      Assert.NotNull(newCreative);
      Assert.AreEqual(newCreative.id, creative1.id);
      Assert.AreEqual(newCreative.name, creative1.name);
      Assert.AreEqual(newCreative.advertiserId, creative1.advertiserId);
      Assert.AreEqual(newCreative.size.height, creative1.size.height);
      Assert.AreEqual(newCreative.size.width, creative1.size.width);
      Assert.That(newCreative is ImageCreative);
      Assert.AreEqual((newCreative as ImageCreative).destinationUrl, imageCreative.destinationUrl);
    }

    /// <summary>
    /// Test whether we can update a list of creatives.
    /// </summary>
    [Test]
    public void TestUpdateCreatives() {
      ImageCreative imageCreative1 = (ImageCreative) creative1;
      imageCreative1.destinationUrl = "http://news.google.com";

      ImageCreative imageCreative2 = (ImageCreative) creative2;
      imageCreative2.destinationUrl = "http://finance.google.com";

      Creative[] newCreatives = null;

      Assert.DoesNotThrow(delegate() {
        newCreatives = creativeService.updateCreatives(new Creative[] {creative1, creative2});
      });

      Assert.NotNull(newCreatives);
      Assert.AreEqual(newCreatives.Length, 2);

      Assert.AreEqual(newCreatives[0].id, creative1.id);
      Assert.AreEqual(newCreatives[0].name, creative1.name);
      Assert.AreEqual(newCreatives[0].advertiserId, creative1.advertiserId);
      Assert.AreEqual(newCreatives[0].size.height, creative1.size.height);
      Assert.AreEqual(newCreatives[0].size.width, creative1.size.width);
      Assert.That(newCreatives[0] is ImageCreative);
      Assert.AreEqual((newCreatives[0] as ImageCreative).destinationUrl,
          imageCreative1.destinationUrl);

      Assert.AreEqual(newCreatives[1].id, creative2.id);
      Assert.AreEqual(newCreatives[1].name, creative2.name);
      Assert.AreEqual(newCreatives[1].advertiserId, creative2.advertiserId);
      Assert.AreEqual(newCreatives[1].size.height, creative2.size.height);
      Assert.AreEqual(newCreatives[1].size.width, creative2.size.width);
      Assert.That(newCreatives[1] is ImageCreative);
      Assert.AreEqual((newCreatives[1] as ImageCreative).destinationUrl,
          imageCreative2.destinationUrl);
    }
  }
}
