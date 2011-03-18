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
using Google.Api.Ads.AdWords.v201101;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.AdWords.Tests.v201101 {
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
      campaignService = (CampaignService)user.GetService(AdWordsService.v201101.CampaignService);
      campaignId1 = utils.CreateCampaign(user, new ManualCPC());
      campaignId2 = utils.CreateCampaign(user, new ManualCPC());
    }

    /// <summary>
    /// Test whether we can add campaign.
    /// </summary>
    [Test]
    public void TestAddCampaign() {
      // Create campaign.
      Campaign campaign = new Campaign();
      campaign.name = "Test Campaign #" + new TestUtils().GetTimeStamp();
      campaign.status = CampaignStatus.PAUSED;
      campaign.biddingStrategy = new ManualCPC();

      Budget budget = new Budget();
      budget.period = BudgetBudgetPeriod.DAILY;
      budget.deliveryMethod = BudgetBudgetDeliveryMethod.STANDARD;
      budget.amount = new Money();
      budget.amount.microAmount = 1000000;

      campaign.budget = budget;

      // Create operations.
      CampaignOperation operation = new CampaignOperation();
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
      Selector selector = new Selector();
      selector.fields = new string[] {"Id", "Name", "Status"};

      // Create a filter.
      Predicate predicate = new Predicate();
      predicate.@operator = PredicateOperator.EQUALS;
      predicate.field = "Id";
      predicate.values = new string[] {campaignId1.ToString()};

      selector.predicates = new Predicate[] {predicate};

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
      Selector selector = new Selector();
      selector.fields = new string[] {"Id", "Name", "Status"};

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

      Budget budget = new Budget();
      budget.deliveryMethod = BudgetBudgetDeliveryMethod.ACCELERATED;
      campaign.budget = budget;

      // Create operation.
      CampaignOperation operation = new CampaignOperation();
      operation.@operator = Operator.SET;
      operation.operand = campaign;

      CampaignReturnValue retVal = campaignService.mutate((new CampaignOperation[] {operation}));
      Assert.NotNull(retVal);
      Assert.NotNull(retVal.value);
      Assert.AreEqual(retVal.value.Length, 1);
      Assert.NotNull(retVal.value[0]);
      Assert.AreEqual(retVal.value[0].id, campaignId1);
    }

    /// <summary>
    /// Test whether we can delete a campaign.
    /// </summary>
    [Test]
    public void TestDeleteCampaign() {
      // Create campaign with DELETED status.
      Campaign campaign = new Campaign();
      campaign.id = campaignId1;
      campaign.status = CampaignStatus.DELETED;

      // Create operations.
      CampaignOperation operation = new CampaignOperation();
      operation.operand = campaign;
      operation.@operator = Operator.SET;

      CampaignReturnValue retVal = null;

      Assert.DoesNotThrow(delegate() {
        retVal = campaignService.mutate(new CampaignOperation[] {operation});
      });
      Assert.NotNull(retVal);
      Assert.NotNull(retVal.value);
      Assert.AreEqual(retVal.value.Length, 1);
      Assert.NotNull(retVal.value[0]);
      Assert.AreEqual(retVal.value[0].id, campaignId1);
    }
  }
}
