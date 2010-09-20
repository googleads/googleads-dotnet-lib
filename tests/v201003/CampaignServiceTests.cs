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
using com.google.api.adwords.v201003;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Text;

namespace com.google.api.adwords.tests.v201003 {
  /// <summary>
  /// UnitTests for <see cref="CampaignService"/> class.
  /// </summary>
  [TestFixture]
  class CampaignServiceTests : BaseTests {
    /// <summary>
    /// CampaignService object to be used in this test.
    /// </summary>
    private CampaignService campaignService;

    /// <summary>
    /// The campaign id 1 for which tests are run.
    /// </summary>
    private long campaignId1 = 0;

    /// <summary>
    /// The campaign id 1 for which tests are run.
    /// </summary>
    private long campaignId2 = 0;

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public CampaignServiceTests() : base() {
    }

    /// <summary>
    /// Initialize the test case.
    /// </summary>
    [SetUp]
    public void Init() {
      TestUtils utils = new TestUtils();
      campaignService = (CampaignService)user.GetService(AdWordsService.v201003.CampaignService);
      campaignId1 = utils.CreateCampaign(user, true);
      campaignId2 = utils.CreateCampaign(user, true);
    }

    /// <summary>
    /// Test whether we can add campaign.
    /// </summary>
    [Test]
    public void TestAddCampaign() {
      // Create campaign.
      Campaign campaign = new Campaign();
      campaign.name = "Test Campaign #" + new TestUtils().GetTimeStamp();
      campaign.statusSpecified = true;
      campaign.status = CampaignStatus.PAUSED;
      campaign.biddingStrategy = new ManualCPC();

      Budget budget = new Budget();
      budget.periodSpecified = true;
      budget.period = BudgetBudgetPeriod.DAILY;
      budget.deliveryMethodSpecified = true;
      budget.deliveryMethod = BudgetBudgetDeliveryMethod.STANDARD;
      budget.amount = new Money();
      budget.amount.microAmountSpecified = true;
      budget.amount.microAmount = 1000000;

      campaign.budget = budget;

      // Create operations.
      CampaignOperation operation = new CampaignOperation();
      operation.operatorSpecified = true;
      operation.@operator = Operator.ADD;
      operation.operand = campaign;

      Assert.That(campaignService.mutate(new CampaignOperation[] {operation})
          is CampaignReturnValue);
    }

    /// <summary>
    /// Test whether we can get a campaign by id.
    /// </summary>
    [Test]
    public void TestGetCampaign() {
      CampaignSelector selector = new CampaignSelector();
      selector.ids = new long[] {campaignId1};

      CampaignPage page = campaignService.get(selector);
      Assert.NotNull(page);
      Assert.NotNull(page.entries);
      Assert.AreEqual(page.entries.Length, 1);
      Assert.NotNull(page.entries[0]);
      Assert.AreEqual(page.entries[0].id, campaignId1);
    }

    /// <summary>
    /// Test whether we can fetch all existing campaigns.
    /// </summary>
    [Test]
    public void TestGetAllCampaigns() {
      CampaignSelector selector = new CampaignSelector();

      CampaignPage page = campaignService.get(selector);
      Assert.NotNull(page);
      Assert.NotNull(page.entries);
    }

    /// <summary>
    /// Test whether we can update an existing campaign.
    /// </summary>
    [Test]
    public void TestUpdateCampaign() {
      // Create campaign with updated budget.
      Campaign campaign = new Campaign();
      campaign.id = campaignId1;
      campaign.idSpecified = true;

      Budget budget = new Budget();
      budget.deliveryMethodSpecified = true;
      budget.deliveryMethod = BudgetBudgetDeliveryMethod.ACCELERATED;
      campaign.budget = budget;

      // Create operation.
      CampaignOperation operation = new CampaignOperation();
      operation.operatorSpecified = true;
      operation.@operator = Operator.SET;
      operation.operand = campaign;

      CampaignReturnValue result = campaignService.mutate((new CampaignOperation[] {operation}));
      Assert.NotNull(result);
      Assert.NotNull(result.value);
      Assert.AreEqual(result.value.Length, 1);
      Assert.NotNull(result.value[0]);
      Assert.AreEqual(result.value[0].id, campaignId1);
    }

    /// <summary>
    /// Test whether we can delete a campaign.
    /// </summary>
    [Test]
    public void TestDeleteCampaign() {
      // Create campaign with DELETED status.
      Campaign campaign = new Campaign();
      campaign.id = campaignId1;
      campaign.idSpecified = true;
      campaign.status = CampaignStatus.DELETED;
      campaign.statusSpecified = true;

      // Create operations.
      CampaignOperation operation = new CampaignOperation();
      operation.operand = campaign;
      operation.@operator = Operator.SET;
      operation.operatorSpecified = true;

      CampaignReturnValue result = null;

      Assert.DoesNotThrow(delegate() {
        result = campaignService.mutate(new CampaignOperation[] { operation });
      });
      Assert.NotNull(result);
      Assert.NotNull(result.value);
      Assert.AreEqual(result.value.Length, 1);
      Assert.NotNull(result.value[0]);
      Assert.AreEqual(result.value[0].id, campaignId1);
    }
  }
}
