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
using Google.Api.Ads.AdWords.v200909;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.AdWords.Tests.v200909 {
  /// <summary>
  /// UnitTests for <see cref="AdGroupService"/> class.
  /// </summary>
  [TestFixture]
  class AdGroupServiceTests : BaseTests {
    /// <summary>
    /// AdGroupService object to be used in this test.
    /// </summary>
    private AdGroupService adGroupService;

    /// <summary>
    /// The campaign id for which tests are run. (cpc operations)
    /// </summary>
    private long cpcCampaignId = 0;

    /// <summary>
    /// The campaign id for which tests are run. (cpm operations)
    /// </summary>
    private long cpmCampaignId = 0;

    /// <summary>
    /// The adgroup id for which tests are run.
    /// </summary>
    private long adGroupId1 = 0;

    /// <summary>
    /// The adgroup id for which tests are run.
    /// </summary>
    private long adGroupId2 = 0;

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public AdGroupServiceTests() : base() {
    }

    /// <summary>
    /// Initialize the test case.
    /// </summary>
    [SetUp]
    public void Init() {
      TestUtils utils = new TestUtils();
      adGroupService = (AdGroupService) user.GetService(AdWordsService.v200909.AdGroupService);
      cpcCampaignId = utils.CreateCampaign(user, new ManualCPC());
      cpmCampaignId = utils.CreateCampaign(user, new ManualCPM());
      adGroupId1 = utils.CreateAdGroup(user, cpcCampaignId);
      adGroupId2 = utils.CreateAdGroup(user, cpcCampaignId);
    }

    /// <summary>
    /// Test whether we can add an ad group for keywords using v200909.
    /// </summary>
    [Test]
    public void TestAddAdGroupKeyword() {
      AdGroupOperation operation = new AdGroupOperation();

      operation.@operator = Operator.ADD;
      operation.operand = new AdGroup();
      operation.operand.campaignId = cpcCampaignId;
      operation.operand.name =
          string.Format("AdGroup {0}", DateTime.Now.ToString("yyyy-M-d H:m:s.ffffff"));
      operation.operand.status = AdGroupStatus.ENABLED;

      ManualCPCAdGroupBids bids = new ManualCPCAdGroupBids();
      bids.keywordMaxCpc = new Bid();
      bids.keywordMaxCpc.amount = new Money();
      bids.keywordMaxCpc.amount.microAmount = 180000;
      operation.operand.bids = bids;

      Assert.That(adGroupService.mutate(new AdGroupOperation[] {operation}) is AdGroupReturnValue);
    }

    /// <summary>
    /// Test whether we can add an ad group for placements using v200909.
    /// </summary>
    [Test]
    public void TestAddAdGroupPlacement() {
      AdGroupOperation operation = new AdGroupOperation();

      operation.@operator = Operator.ADD;
      operation.operand = new AdGroup();
      operation.operand.campaignId = cpcCampaignId;
      operation.operand.name =
          string.Format("AdGroup {0}", DateTime.Now.ToString("yyyy-M-d H:m:s.ffffff"));
      operation.operand.status = AdGroupStatus.ENABLED;

      ManualCPCAdGroupBids bids = new ManualCPCAdGroupBids();
      bids.siteMaxCpc = new Bid();
      bids.siteMaxCpc.amount = new Money();
      bids.siteMaxCpc.amount.microAmount = 150000;
      operation.operand.bids = bids;

      Assert.That(adGroupService.mutate(new AdGroupOperation[] {operation}) is AdGroupReturnValue);
    }

    /// <summary>
    /// Test whether we can add an ad group for all criteria types v200909.
    /// </summary>
    [Test]
    public void TestAddAdGroupKeywordPlacement() {
      AdGroupOperation operation = new AdGroupOperation();

      operation.@operator = Operator.ADD;
      operation.operand = new AdGroup();
      operation.operand.campaignId = cpcCampaignId;
      operation.operand.name =
          string.Format("AdGroup {0}", DateTime.Now.ToString("yyyy-M-d H:m:s.ffffff"));
      operation.operand.status = AdGroupStatus.ENABLED;

      ManualCPCAdGroupBids bids = new ManualCPCAdGroupBids();

      bids.siteMaxCpc = new Bid();
      bids.siteMaxCpc.amount = new Money();
      bids.siteMaxCpc.amount.microAmount = 1000000;

      bids.keywordMaxCpc = new Bid();
      bids.keywordMaxCpc.amount = new Money();
      bids.keywordMaxCpc.amount.microAmount = 180000;

      operation.operand.bids = bids;

      Assert.That(adGroupService.mutate(new AdGroupOperation[] {operation})
          is AdGroupReturnValue);
    }

    /// <summary>
    /// Test whether we can fetch an existing ad group using v200909.
    /// </summary>
    [Test]
    public void TestGetAdGroup() {
      AdGroupSelector selector = new AdGroupSelector();
      selector.campaignId = cpcCampaignId;
      selector.adGroupIds = new long[] {adGroupId1};

      Assert.That(adGroupService.get(selector) is AdGroupPage);
    }

    /// <summary>
    /// Test whether we can fetch all existing ad groups using v200909.
    /// </summary>
    [Test]
    public void TestGetAdGroups() {
      AdGroupSelector selector = new AdGroupSelector();
      selector.campaignId = cpcCampaignId;

      Assert.That(adGroupService.get(selector) is AdGroupPage);
    }

    /// <summary>
    /// Test whether we can update an existing ad group using v200909.
    /// </summary>
    [Test]
    public void TestUpdateAdGroup() {
      AdGroupOperation operation = new AdGroupOperation();
      operation.@operator = Operator.SET;
      operation.operand = new AdGroup();
      operation.operand.campaignId = cpcCampaignId;
      operation.operand.id = adGroupId1;
      operation.operand.status = AdGroupStatus.PAUSED;

      ManualCPCAdGroupBids bids = new ManualCPCAdGroupBids();
      bids.keywordMaxCpc = new Bid();
      bids.keywordMaxCpc.amount = new Money();
      bids.keywordMaxCpc.amount.microAmount = 200000;

      operation.operand.bids = bids;

      Assert.That(adGroupService.mutate(new AdGroupOperation[] {operation}) is AdGroupReturnValue);
    }
  }
}
