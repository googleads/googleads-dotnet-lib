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
using System.Web.Services.Protocols;

namespace com.google.api.adwords.tests.v13 {
  /// <summary>
  /// UnitTests for <see cref="AdService"/> class.
  /// </summary>
  [TestFixture]
  class AdServiceTests : BaseTests {
    /// <summary>
    /// AdService object to be used in this test.
    /// </summary>
    AdService adService;

    /// <summary>
    /// The campaign id for which tests are run.
    /// </summary>
    long campaignId = 0;

    /// <summary>
    /// The adgroup id for which tests are run.
    /// </summary>
    long adGroupId = 0;

    /// <summary>
    /// The ad id for which tests are run.
    /// </summary>
    long adId = 0;

    /// <summary>
    /// Test utility class to assist in testing.
    /// </summary>
    TestUtils util = new TestUtils();

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public AdServiceTests()
      : base() {
    }

    /// <summary>
    /// Initialize the test case.
    /// </summary>
    [SetUp]
    public void Init() {
      AdWordsUser user = new AdWordsUser();
      adService = (AdService) user.GetService(AdWordsService.v13.AdService);
      campaignId = util.CreateCampaign(user, true);
      adGroupId = util.CreateAdGroup(user, campaignId);
      adId = util.CreateTextAd(user, adGroupId);
    }

    /// <summary>
    /// Test whether we can add an image ad using v13.
    /// </summary>
    [Test]
    public void TestAddAdsImageAd() {
      ImageAd imgAd = new ImageAd();
      imgAd.adGroupId = adGroupId;
      imgAd.destinationUrl = "http://www.example.com";
      imgAd.displayUrl = "http://www.example.com";
      imgAd.image = new Image();
      imgAd.image.data = util.GetSandboxImage();
      imgAd.image.name = "Test image";
      object results = adService.addAds(new Ad[] {imgAd});
      Assert.That(results == null || results is Ad[]);
    }

    /// <summary>
    /// Test whether we can add a text ad to an existing ad group using v13.
    /// </summary>
    [Test]
    public void TestAddAdsTextAd() {
      TextAd ad = new TextAd();
      ad.adGroupId = adGroupId;
      ad.destinationUrl = "http://www.example.com";
      ad.displayUrl = "example.com";
      ad.statusSpecified = true;
      ad.status = AdStatus.Enabled;
      ad.description1 = "Visit the Red Planet in style.";
      ad.description2 = "Low-gravity fun for everyone!";
      ad.headline = "Luxury Cruise to Mars";

      object results = adService.addAds(new Ad[] {ad});
      Assert.That(results == null || results is Ad[]);
    }

    /// <summary>
    /// Test whether we can add a text ad with HTML encoded URL using v13.
    /// </summary>
    [Test]
    public void TestAddAdsTextAdHtmlEncodeUrl() {
      TextAd ad = new TextAd();
      ad.adGroupId = adGroupId;
      ad.destinationUrl = "http://www.example.com?key1=value1&key2=value2";
      ad.displayUrl = "example.com";
      ad.statusSpecified = true;
      ad.status = AdStatus.Enabled;
      ad.description1 = "Visit the Red Planet in style.";
      ad.description2 = "Low-gravity fun for everyone!";
      ad.headline = "Luxury Cruise to Mars";

      object results = adService.addAds(new Ad[] {ad});
      Assert.That(results == null || results is Ad[]);
    }

    /// <summary>
    /// Test whether we can add a text ad with HTML encoded headline using v13.
    /// </summary>
    [Test]
    public void TestAddAdsTextAdHtmlEncodeHeadline() {
      TextAd ad = new TextAd();
      ad.adGroupId = adGroupId;
      ad.destinationUrl = "http://www.example.com";
      ad.displayUrl = "example.com";
      ad.status = AdStatus.Enabled;
      ad.description1 = "Visit the Red Planet in style.";
      ad.description2 = "Low-gravity fun for everyone!";
      ad.headline = "Luxury Cruise & More";

      object results = adService.addAds(new Ad[] {ad});
      Assert.That(results == null || results is Ad[]);
    }

    /// <summary>
    /// Test whether we can add a text ad with non-ASCII characters in description using v13.
    /// </summary>
    [Test]
    public void TestAddAdsTextAdNonASCIIDescription() {
      TextAd ad = new TextAd();
      ad.adGroupId = adGroupId;
      ad.destinationUrl = "http://www.example.com";
      ad.displayUrl = "example.com";
      ad.status = AdStatus.Enabled;
      ad.description1 = "Visit the Red Planet in style.";
      ad.description2 = "See other planets through fénêtre!";
      ad.headline = "Luxury Cruise to Mars";

      object results = adService.addAds(new Ad[] {ad});
      Assert.That(results == null || results is Ad[]);
    }

    /// <summary>
    /// Test whether we can add a text ad, with policy vioalation, to an existing ad group
    /// using v13.
    /// Policy violations:
    ///  - exclamation point in a title
    ///  - excessive capitalization
    ///  - excessive use of exclamation point in description
    /// </summary>
    [Test]
    public void TestAddAdsTextAdFault() {
      TextAd ad = new TextAd();
      ad.id = adId;
      ad.adGroupId = adGroupId;
      ad.destinationUrl = "http://www.example.com";
      ad.displayUrl = "example.com";
      ad.status = AdStatus.Enabled;
      ad.description1 = "Visit the Red Planet in style.";
      ad.description2 = "Low-gravity fun for EVERYONE!!!";
      ad.headline = "Luxury Cruise to Mars!";

      Assert.Throws(typeof(InvalidParameterException),
        delegate() {
          adService.addAds(new Ad[] {ad});
        });
    }

    /// <summary>
    /// Test whether we can check an ad using v13.
    /// </summary>
    [Test]
    public void TestCheckAds() {
      TextAd ad = new TextAd();
      ad.id = adGroupId;
      ad.destinationUrl = "http://www.example.com";
      ad.displayUrl = "example.com";
      ad.status = AdStatus.Enabled;
      ad.description1 = "Visit the Red Planet in style.";
      ad.description2 = "Low-gravity fun for everyone!";
      ad.headline = "Luxury Cruise to Mars!";

      string[] languageTarget = new string[] {"en"};

      GeoTarget geoTarget = new GeoTarget();
      geoTarget.cityTargets = new CityTargets();
      geoTarget.cityTargets.cities = new string[] {"New York, NY US"};


      object results = adService.checkAds(new Ad[] {ad}, languageTarget, geoTarget);
      Assert.That(results == null || results is ApiError[]);
    }

    /// <summary>
    /// Test whether we can check an ad with policy violation using v13.
    /// </summary>
    [Test]
    public void TestCheckAdsFault() {
      TextAd ad = new TextAd();
      ad.id = adGroupId;
      ad.destinationUrl = "http://www.example.com";
      ad.displayUrl = "example.com";
      ad.status = AdStatus.Enabled;
      ad.description1 = "Visit the Red Planet in style.";
      ad.description2 = "Low-gravity fun for everyone!!!";
      ad.headline = "Luxury Cruise to Mars!";

      GeoTarget geoTarget = new GeoTarget();
      geoTarget.cityTargets = new CityTargets();
      geoTarget.cityTargets.cities = new string[] {"New York, NY US"};

      object results = adService.checkAds(new Ad[] {ad}, new string[] {"en"}, geoTarget);
      Assert.That(results != null && results is ApiError[]);
    }

    /// <summary>
    /// Test whether we can find businesses using v13.
    /// </summary>
    [Test]
    public void TestFindBusinesses() {
      object results = adService.findBusinesses("A Business Corp.", "123 Amphitheatre Pkwy.", "US");
      Assert.That(results == null || results is Business[]);
    }

    /// <summary>
    /// Test whether we can fetch existing active ads from given ad group using v13.
    /// </summary>
    [Test]
    public void TestGetActiveAds() {
      object results = adService.getActiveAds(new long[] {adGroupId});
      Assert.That(results == null || results is Ad[]);
    }

    /// <summary>
    /// Test whether we can fetch an existing ad from given ad group using v13.
    /// </summary>
    [Test]
    public void TestGetAd() {
      Assert.That(adService.getAd(adGroupId, adId) is Ad);
    }

    /// <summary>
    /// Test whether we can fetch stats for existing ad group using v13.
    /// </summary>
    [Test]
    public void TestGetAdStats() {
      object results = adService.getAdStats(adGroupId, new long[] {adId}, new DateTime(2008, 1, 1),
          new DateTime(2008, 1, 31));
      Assert.That(results == null || results is StatsRecord[]);
    }

    /// <summary>
    /// Test whether we can fetch all ads from a list of ad groups using v13.
    /// </summary>
    [Test]
    public void TestGetAllAds() {
      object results = adService.getAllAds(new long[] {adGroupId});
      Assert.That(results == null || results is Ad[]);
    }

    /// <summary>
    /// Test whether we can fetch my businesses using v13.
    /// </summary>
    [Test]
    public void TestGetMyBusinesses() {
      object results = adService.getMyBusinesses();
      Assert.That(results == null || results is Business[]);
    }

    /// <summary>
    /// Test whether we can fetch my videos using v13.
    /// </summary>
    [Test]
    public void TestGetMyVideos() {
      object results = adService.getMyVideos();
      Assert.That(results == null || results is Video[]);
    }

    /// <summary>
    /// Test whether we can update a list of existing ads using v13.
    /// </summary>
    [Test]
    public void TestUpdateAds() {
      Ad ad = new TextAd();
      ad.adGroupId = adGroupId;
      ad.id = adId;
      ad.statusSpecified = true;
      ad.status = AdStatus.Paused;

      adService.updateAds(new Ad[] {ad});
      Assert.Pass();
    }
  }
}
