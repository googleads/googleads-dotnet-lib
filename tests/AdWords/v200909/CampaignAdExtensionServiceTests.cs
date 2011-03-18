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
  /// UnitTests for <see cref="CampaignAdExtensionService"/> class.
  /// </summary>
  [TestFixture]
  class CampaignAdExtensionServiceTests : BaseTests {
    /// <summary>
    /// CampaignAdExtensionService object to be used in this test.
    /// </summary>
    private CampaignAdExtensionService campaignAdExtensionService;

    /// <summary>
    /// The campaign id for which tests are run.
    /// </summary>
    private long campaignId = 0;

    /// <summary>
    /// The campaign ad extension id for which tests are run.
    /// </summary>
    private long campaignAdExtensionId = 0;

    /// <summary>
    /// The geo location for running tests.
    /// </summary>
    private GeoLocation location = null;
    
    /// <summary>
    /// Default public constructor.
    /// </summary>
    public CampaignAdExtensionServiceTests() : base() {
    }

    /// <summary>
    /// Initialize the test case.
    /// </summary>
    [SetUp]
    public void Init() {
      TestUtils utils = new TestUtils();
      campaignAdExtensionService = (CampaignAdExtensionService)user.GetService(
          AdWordsService.v200909.CampaignAdExtensionService);
      campaignId = utils.CreateCampaign(user, true);
      campaignAdExtensionId = utils.CreateCampaignAdExtension(user, campaignId);

      Address address = new Address();
      address.streetAddress = "1600 Amphitheatre Pkwy, Mountain View";
      address.countryCode = "US";

      location = utils.GetLocationForAddress(user, address);
    }

    /// <summary>
    /// Test whether we can add ad extension to a given campaign.
    /// </summary>
    [Test]
    public void TestAddCampaignAdExtension() {
      CampaignAdExtensionOperation operation = new CampaignAdExtensionOperation();
      operation.operatorSpecified = true;
      operation.@operator = Operator.ADD;

      CampaignAdExtension extension = new CampaignAdExtension();
      extension.campaignIdSpecified = true;
      extension.campaignId = campaignId;
      extension.statusSpecified = true;
      extension.status = CampaignAdExtensionStatus.ACTIVE;

      LocationExtension locationExtension = new LocationExtension();

      // Note: Do not populate an address directly. Instead, use
      // GeoLocationService to obtain the location of an address,
      // and use the address as per the location it returns.
      locationExtension.address = location.address;
      locationExtension.geoPoint = location.geoPoint;
      locationExtension.encodedLocation = location.encodedLocation;
      locationExtension.sourceSpecified = true;
      locationExtension.source = LocationExtensionSource.ADWORDS_FRONTEND;

      extension.adExtension = locationExtension;
      operation.operand = extension;
      CampaignAdExtensionReturnValue retval =
          campaignAdExtensionService.mutate(new CampaignAdExtensionOperation[] {operation});
      Assert.NotNull(retval);
      Assert.NotNull(retval.value);
      Assert.AreEqual(retval.value.Length, 1);
      Assert.NotNull(retval.value[0]);
      Assert.AreEqual(retval.value[0].campaignId, campaignId);
      Assert.NotNull(retval.value[0].adExtension);
      Assert.That(retval.value[0].adExtension is LocationExtension);
    }

    /// <summary>
    /// Test whether we can get all ad extensions in a campaign.
    /// </summary>
    [Test]
    public void TestGetAllCampaignAdExtensions() {
      CampaignAdExtensionSelector selector = new CampaignAdExtensionSelector();
      selector.campaignIds = new long[] {campaignId};

      CampaignAdExtensionPage page = campaignAdExtensionService.get(selector);
      Assert.NotNull(page);
      Assert.NotNull(page.entries);
      Assert.AreEqual(page.entries.Length, 1);
      Assert.NotNull(page.entries[0]);
      Assert.AreEqual(page.entries[0].campaignId, campaignId);
      Assert.NotNull(page.entries[0].adExtension);
      Assert.AreEqual(page.entries[0].adExtension.id, campaignAdExtensionId);
    }

    /// <summary>
    /// Test whether we can delete a campaign ad extension.
    /// </summary>
    [Test]
    public void TestDeleteCampaignAdExtension() {
      CampaignAdExtension campaignAdExtension = new CampaignAdExtension();
      campaignAdExtension.campaignIdSpecified = true;
      campaignAdExtension.campaignId = campaignId;
      campaignAdExtension.adExtension = new AdExtension();
      campaignAdExtension.adExtension.idSpecified = true;
      campaignAdExtension.adExtension.id = campaignAdExtensionId;

      CampaignAdExtensionOperation operation = new CampaignAdExtensionOperation();
      operation.operand = campaignAdExtension;
      operation.operatorSpecified = true;
      operation.@operator = Operator.REMOVE;

      CampaignAdExtensionReturnValue retval = null;
      
      Assert.DoesNotThrow(delegate() {
        retval = campaignAdExtensionService.mutate(new CampaignAdExtensionOperation[] {operation});
      });
      Assert.NotNull(retval);
      Assert.NotNull(retval.value);
      Assert.AreEqual(retval.value.Length, 1);
      Assert.NotNull(retval.value[0]);
      Assert.AreEqual(retval.value[0].campaignId, campaignId);
      Assert.NotNull(retval.value[0].adExtension);
      Assert.AreEqual(retval.value[0].adExtension.id, campaignAdExtensionId);
    }
  }
}
