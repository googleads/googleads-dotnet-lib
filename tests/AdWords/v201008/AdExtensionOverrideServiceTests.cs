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
using Google.Api.Ads.AdWords.v201008;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.AdWords.Tests.v201008 {
  /// <summary>
  /// UnitTests for <see cref="AdExtensionOverrideService"/> class.
  /// </summary>
  [TestFixture]
  class AdExtensionOverrideServiceTests : BaseTests {
    /// <summary>
    /// AdGroupAdService object to be used in this test.
    /// </summary>
    private AdExtensionOverrideService adExtensionOverrideService;

    /// <summary>
    /// The campaign id for which tests are run.
    /// </summary>
    private long campaignId = 0;

    /// <summary>
    /// The adgroup id for which tests are run.
    /// </summary>
    private long adGroupId = 0;

    /// <summary>
    /// The Ad id 1 for which tests are run.
    /// </summary>
    private long adIdNoOverride = 0;

    /// <summary>
    /// The Ad id 2 for which tests are run.
    /// </summary>
    private long adId = 0;

    /// <summary>
    /// Campaign extension id for the test campaign.
    /// </summary>
    private long campaignAdExtensionId = 0;

    /// <summary>
    /// Ad extension override id for the test ad.
    /// </summary>
    private long adExtensionOverrideId = 0;

    /// <summary>
    /// The geo location for running tests.
    /// </summary>
    private GeoLocation location = null;

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public AdExtensionOverrideServiceTests() : base() {
    }

    /// <summary>
    /// Initialize the test case.
    /// </summary>
    [SetUp]
    public void Init() {
      TestUtils utils = new TestUtils();
      adExtensionOverrideService = (AdExtensionOverrideService) user.GetService(
          AdWordsService.v201008.AdExtensionOverrideService);
      campaignId = utils.CreateCampaign(user, new ManualCPC());
      adGroupId = utils.CreateAdGroup(user, campaignId);
      adIdNoOverride = utils.CreateTextAd(user, adGroupId, false);
      campaignAdExtensionId = utils.CreateCampaignAdExtension(user, campaignId);

      Address address = new Address();
      address.streetAddress = "1600 Amphitheatre Pkwy, Mountain View";
      address.countryCode = "US";

      location = utils.GetLocationForAddress(user, address);

      adId = utils.CreateTextAd(user, adGroupId, false);
      adExtensionOverrideId = utils.CreateAdExtensionOverride(user, campaignAdExtensionId, adId,
          location);
    }

    /// <summary>
    /// Test whether we can add ad extension override to a given campaign.
    /// </summary>
    [Test]
    public void TestAddAdExtensionOverride() {
      AdExtensionOverrideOperation operation = new AdExtensionOverrideOperation();
      operation.@operator = Operator.ADD;

      operation.operand = new AdExtensionOverride();
      operation.operand.adId = adIdNoOverride;

      LocationExtension locationExtension = new LocationExtension();
      locationExtension.id = campaignAdExtensionId;

      // Note: Do not populate an address directly. Instead, use
      // GeoLocationService to obtain the location of an address,
      // and use the address as per the location it returns.
      locationExtension.address = location.address;
      locationExtension.geoPoint = location.geoPoint;
      locationExtension.encodedLocation = location.encodedLocation;
      locationExtension.source = LocationExtensionSource.ADWORDS_FRONTEND;

      // Optional: Apply this override within 20 kms.
      operation.operand.overrideInfo = new OverrideInfo();
      operation.operand.overrideInfo.Item = new LocationOverrideInfo();
      operation.operand.overrideInfo.Item.radius = 20;
      operation.operand.overrideInfo.Item.radiusUnits = LocationOverrideInfoRadiusUnits.KILOMETERS;

      operation.operand.adExtension = locationExtension;

      AdExtensionOverrideReturnValue retVal = null;

      Assert.DoesNotThrow(delegate() {
        retVal = adExtensionOverrideService.mutate(new AdExtensionOverrideOperation[] {operation});
      });
      Assert.NotNull(retVal);
      Assert.NotNull(retVal.value);
      Assert.AreEqual(retVal.value.Length, 1);
      Assert.NotNull(retVal.value[0]);
      Assert.AreEqual(retVal.value[0].adId, adIdNoOverride);
      Assert.NotNull(retVal.value[0].adExtension);
      Assert.That(retVal.value[0].adExtension is LocationExtension);
      Assert.AreEqual((retVal.value[0].adExtension as LocationExtension).id, campaignAdExtensionId);
    }

    /// <summary>
    /// Test whether we can get ad extension override to a given campaign.
    /// </summary>
    [Test]
    public void TestGetAdExtensionOverride() {
      AdExtensionOverrideSelector selector = new AdExtensionOverrideSelector();
      selector.campaignIds = new long[] {campaignId};
      selector.statuses = new AdExtensionOverrideStatus[] {AdExtensionOverrideStatus.ACTIVE};
      AdExtensionOverridePage page = null;
      Assert.DoesNotThrow(delegate() {
        page = adExtensionOverrideService.get(selector);
      });
      Assert.NotNull(page);
      Assert.NotNull(page.entries);
      Assert.NotNull(page.entries[0]);
      Assert.AreEqual(page.entries[0].adId, adId);
      Assert.That(page.entries[0].adExtension is LocationExtension);
      Assert.AreEqual((page.entries[0].adExtension as LocationExtension).id, campaignAdExtensionId);
    }
  }
}
