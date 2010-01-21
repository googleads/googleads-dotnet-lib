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
  /// UnitTests for <see cref="AdGroupService"/> class.
  /// </summary>
  [TestFixture]
  class AdGroupServiceTests : BaseTests {
    /// <summary>
    /// AdGroupService object to be used in this test.
    /// </summary>
    AdGroupService adGroupService;

    /// <summary>
    /// The campaign id for which tests are run. (cpc operations)
    /// </summary>
    int cpcCampaignId = 0;

    /// <summary>
    /// The campaign id for which tests are run. (cpm operations)
    /// </summary>
    int cpmCampaignId = 0;

    /// <summary>
    /// The adgroup id for which tests are run.
    /// </summary>
    long adGroupId1 = 0;

    /// <summary>
    /// The adgroup id for which tests are run.
    /// </summary>
    long adGroupId2 = 0;

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public AdGroupServiceTests()
      : base() {
    }

    /// <summary>
    /// Initialize the test case.
    /// </summary>
    [SetUp]
    public void Init() {
      AdWordsUser user = new AdWordsUser();
      adGroupService = (AdGroupService) user.GetService(AdWordsService.v13.AdGroupService);
      TestUtils utils = new TestUtils();
      cpcCampaignId = (int) utils.CreateCampaign(user, true);
      cpmCampaignId = (int) utils.CreateCampaign(user, false);
      adGroupId1 = utils.CreateAdGroup(user, cpcCampaignId);
      adGroupId2 = utils.CreateAdGroup(user, cpcCampaignId);
    }

    /// <summary>
    /// Test whether we can add a cpc ad group using v13.
    /// </summary>
    [Test]
    public void TestAddAdGroupCpc() {
      AdGroup adGroup = new AdGroup();
      adGroup.campaignId = cpcCampaignId;
      adGroup.keywordMaxCpc = 1000000;
      adGroup.keywordMaxCpcSpecified = true;

      Assert.That(adGroupService.addAdGroup(cpcCampaignId, adGroup) is AdGroup);
    }

    /// <summary>
    /// Test whether we can add a cpm ad group using v13.
    /// </summary>
    [Test]
    public void TestAddAdGroupCpm() {
      AdGroup adGroup = new AdGroup();
      adGroup.campaignId = cpmCampaignId;
      adGroup.siteMaxCpm = 5000000;
      adGroup.siteMaxCpmSpecified = true;

      Assert.That(adGroupService.addAdGroup(cpmCampaignId, adGroup) is AdGroup);
    }

    /// <summary>
    /// Test whether we can add a site cpc ad group using v13.
    /// </summary>
    [Test]
    public void TestAddAdGroupSiteCpc() {
      AdGroup adGroup = new AdGroup();
      adGroup.campaignId = cpcCampaignId;
      adGroup.siteMaxCpc = 3500000;
      adGroup.siteMaxCpcSpecified = true;

      Assert.That(adGroupService.addAdGroup(cpcCampaignId, adGroup) is AdGroup);
    }

    /// <summary>
    /// Test whether we can add a list of ad groups using v13.
    /// </summary>
    [Test]
    public void TestAddAdGroupList() {
      AdGroup adGroup1 = new AdGroup();
      adGroup1.campaignId = cpcCampaignId;
      adGroup1.keywordMaxCpc = 1000000;
      adGroup1.keywordMaxCpcSpecified = true;

      AdGroup adGroup2 = new AdGroup();
      adGroup2.campaignId = cpcCampaignId;
      adGroup2.keywordMaxCpc = 2000000;
      adGroup2.keywordMaxCpcSpecified = true;

      Assert.That(adGroupService.addAdGroupList(cpcCampaignId, new AdGroup[] {adGroup1, adGroup2})
          is AdGroup[]);
    }

    /// <summary>
    /// Test whether we can fetch active ad groups using v13.
    /// </summary>
    [Test]
    public void TestGetActiveAdGroups() {
      object retval = adGroupService.getActiveAdGroups(cpcCampaignId);
      Assert.That(retval is AdGroup[] || retval == null);
    }

    /// <summary>
    /// Test whether we can fetch an existing ad group using v13.
    /// </summary>
    [Test]
    public void TestGetAdGroup() {
      Assert.That(adGroupService.getAdGroup(adGroupId1) is AdGroup);
    }

    /// <summary>
    /// Test whether we can fetch a list of existing ad groups using v13.
    /// </summary>
    [Test]
    public void TestGetAdGroupList() {
      Assert.That(adGroupService.getAdGroupList(new long[] {adGroupId1, adGroupId2}) is AdGroup[]);
    }

    /// <summary>
    /// Test whether we can fetch stats for existing ad group using v13.
    /// </summary>
    [Test]
    public void TestGetAdGroupStats() {
      object result = adGroupService.getAdGroupStats(cpcCampaignId,
          new long[] {adGroupId1, adGroupId2}, new DateTime(2009, 1, 1), new DateTime(2009, 1, 31));

      Assert.That(result == null || result is StatsRecord[]);
    }

    /// <summary>
    /// Test whether we can fetch all existing ad groups from given campaign using v13.
    /// </summary>
    [Test]
    public void TestGetAllAdGroups() {
      object result = adGroupService.getAllAdGroups(cpcCampaignId);
      Assert.That(result == null || result is AdGroup[]);
    }

    /// <summary>
    /// Test whether we can update an existing ad group using v13.
    /// </summary>
    [Test]
    public void TestUpdateAdGroup() {
      AdGroup adGroup = new AdGroup();
      adGroup.campaignId = cpcCampaignId;
      adGroup.id = adGroupId1;
      adGroup.keywordMaxCpcSpecified = true;
      adGroup.keywordMaxCpc = 2500000;

      adGroupService.updateAdGroup(adGroup);
      Assert.Pass();
    }

    /// <summary>
    /// Test whether we can update a list of existing ad groups using v13.
    /// </summary>
    [Test]
    public void TestUpdateAdGroupList() {
      AdGroup adGroup1 = new AdGroup();
      adGroup1.campaignId = cpcCampaignId;
      adGroup1.id = adGroupId1;
      adGroup1.keywordMaxCpcSpecified = true;
      adGroup1.keywordMaxCpc = 2400000;

      AdGroup adGroup2 = new AdGroup();
      adGroup2.campaignId = cpcCampaignId;
      adGroup2.id = adGroupId2;
      adGroup2.keywordMaxCpcSpecified = true;
      adGroup2.keywordMaxCpc = 500000;

      adGroupService.updateAdGroupList(new AdGroup[] {adGroup1, adGroup2});
      Assert.Pass();
    }
  }
}
