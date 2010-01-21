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
using com.google.api.adwords.v200909;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Text;

namespace com.google.api.adwords.tests.v200909 {
  /// <summary>
  /// UnitTests for <see cref="AdExtensionOverrideService"/> class.
  /// </summary>
  [TestFixture]
  class AdExtensionOverrideServiceTests : BaseTests {
    /// <summary>
    /// AdGroupAdService object to be used in this test.
    /// </summary>
    AdExtensionOverrideService adExtensionOverrideService;

    /// <summary>
    /// The campaign id for which tests are run.
    /// </summary>
    long campaignId = 0;

    /// <summary>
    /// The adgroup id for which tests are run.
    /// </summary>
    long adGroupId = 0;

    /// <summary>
    /// The Ad id for which tests are run.
    /// </summary>
    long adId = 0;

    /// <summary>
    /// Campaign extension id for the test campaign.
    /// </summary>
    long campaignAdExtensionId = 0;

    /// <summary>
    /// The AdWords user to be used for tests.
    /// </summary>
    AdWordsUser user = new AdWordsUser();

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public AdExtensionOverrideServiceTests()
      : base() {
    }

    /// <summary>
    /// Initialize the test case.
    /// </summary>
    [SetUp]
    public void Init() {
      TestUtils utils = new TestUtils();
      adExtensionOverrideService = (AdExtensionOverrideService) user.GetService(
          AdWordsService.v200909.AdExtensionOverrideService);
      campaignId = utils.CreateCampaign(user, true);
      adGroupId = utils.CreateAdGroup(user, campaignId);
      adId = utils.CreateTextAd(user, adGroupId);
      campaignAdExtensionId = utils.CreateCampaignAdExtension(user, campaignId);
    }

    /// <summary>
    /// Test whether we can add ad extension override to a given campaign.
    /// </summary>
    [Test]
    public void TestAddAdExtensionOverride() {
      AdExtensionOverrideOperation operation = new AdExtensionOverrideOperation();
      operation.operatorSpecified = true;
      operation.@operator = Operator.ADD;

      operation.operand = new AdExtensionOverride();
      operation.operand.adIdSpecified = true;
      operation.operand.adId = adId;

      Address address = new Address();
      address.streetAddress = "1600 Amphitheatre Pkwy, Mountain View";
      address.countryCode = "US";

      GeoLocation location = new TestUtils().GetLocationForAddress(user, address);

      LocationExtension locationExtension = new LocationExtension();

      locationExtension.idSpecified = true;
      locationExtension.id = campaignAdExtensionId;

      // Note: Do not populate an address directly. Instead, use
      // GeoLocationService to obtain the location of an address,
      // and use the address as per the location it returns.
      locationExtension.address = location.address;
      locationExtension.geoPoint = location.geoPoint;
      locationExtension.encodedLocation = location.encodedLocation;
      locationExtension.sourceSpecified = true;
      locationExtension.source = LocationExtensionSource.ADWORDS_FRONTEND;

      // Optional: Apply this override within 20 kms.
      operation.operand.overrideInfo = new OverrideInfo();
      operation.operand.overrideInfo.Item = new LocationOverrideInfo();
      operation.operand.overrideInfo.Item.radiusSpecified = true;
      operation.operand.overrideInfo.Item.radius = 20;
      operation.operand.overrideInfo.Item.radiusUnitsSpecified = true;
      operation.operand.overrideInfo.Item.radiusUnits = LocationOverrideInfoRadiusUnits.KILOMETERS;

      operation.operand.adExtension = locationExtension;

      Assert.That(adExtensionOverrideService.mutate(
          new AdExtensionOverrideOperation[] {operation}) is AdExtensionOverrideReturnValue);
    }

    /// <summary>
    /// Test whether we can get ad extension override to a given campaign.
    /// </summary>
    [Test]
    public void TestGetAdExtensionOverride() {
      AdExtensionOverrideSelector selector = new AdExtensionOverrideSelector();
      selector.campaignIds = new long[] {campaignId};
      selector.statuses = new AdExtensionOverrideStatus[] {AdExtensionOverrideStatus.ACTIVE};
      Assert.That(adExtensionOverrideService.get(selector) is AdExtensionOverridePage);
    }
  }
}
