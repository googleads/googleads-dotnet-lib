// Copyright 2015, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.v201505;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Threading;


namespace Google.Api.Ads.Dfp.Tests.v201505 {
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
      creativeService = (CreativeService)user.GetService(DfpService.v201505.CreativeService);
      advertiserId = utils.CreateCompany(user, CompanyType.ADVERTISER).id;
      creative1 = utils.CreateCreative(user, advertiserId);
      creative2 = utils.CreateCreative(user, advertiserId);
    }

    /// <summary>
    /// Test whether we can create a list of creatives.
    /// </summary>
    [Test]
    public void TestCreateCreatives() {
      // Create an array to store local image creative objects.
      Creative[] imageCreatives = new ImageCreative[2];

      for (int i = 0; i < 2; i++) {
        // Create creative size.
        Size size = new Size();
        size.width = 300;
        size.height = 250;

        // Create an image creative.
        ImageCreative imageCreative = new ImageCreative();
        imageCreative.name = string.Format("Image creative #{0}", i);
        imageCreative.advertiserId = advertiserId;
        imageCreative.destinationUrl = "http://www.google.com";
        imageCreative.size = size;

        // Create image asset.
        CreativeAsset creativeAsset = new CreativeAsset();
        creativeAsset.fileName = "image.jpg";
        creativeAsset.assetByteArray = MediaUtilities.GetAssetDataFromUrl(
            "http://www.google.com/intl/en/adwords/select/images/samples/inline.jpg");
        creativeAsset.size = size;
        imageCreative.primaryImageAsset = creativeAsset;

        imageCreatives[i] = imageCreative;
      }

      Creative[] newCreatives = null;

      Assert.DoesNotThrow(delegate() {
        newCreatives = creativeService.createCreatives(imageCreatives);
      });

      Assert.NotNull(newCreatives);
      Assert.AreEqual(newCreatives.Length, 2);
      Assert.NotNull(newCreatives[0]);
      Assert.That(newCreatives[0] is ImageCreative);
      Assert.AreEqual(newCreatives[0].advertiserId, advertiserId);
      Assert.AreEqual(newCreatives[0].name, "Image creative #0");
      Assert.NotNull(newCreatives[1]);
      Assert.That(newCreatives[1] is ImageCreative);
      Assert.AreEqual(newCreatives[1].advertiserId, advertiserId);
      Assert.AreEqual(newCreatives[1].name, "Image creative #1");
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
