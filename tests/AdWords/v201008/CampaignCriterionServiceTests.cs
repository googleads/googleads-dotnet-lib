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
  /// UnitTests for <see cref="CampaignCriterionService"/> class.
  /// </summary>
  [TestFixture]
  class CampaignCriterionServiceTests : BaseTests {
    /// <summary>
    /// CampaignCriterionService object to be used in this test.
    /// </summary>
    private CampaignCriterionService campaignCriterionService;

    /// <summary>
    /// The campaign id for which tests are run.
    /// </summary>
    private long campaignId = 0;

    /// <summary>
    /// Default public constructor.
    /// </summary>
      public CampaignCriterionServiceTests() : base() {
    }

    /// <summary>
    /// Initialize the test case.
    /// </summary>
    [SetUp]
    public void Init() {
      TestUtils utils = new TestUtils();
      campaignCriterionService = (CampaignCriterionService)user.GetService(
          AdWordsService.v201008.CampaignCriterionService);
      campaignId = utils.CreateCampaign(user, new ManualCPC());
    }

    /// <summary>
    /// Test whether we can add a negative campaign criterion.
    /// </summary>
    [Test]
    public void TestAddCampaignNegativeCriterion() {
      NegativeCampaignCriterion negativeCriterion = new NegativeCampaignCriterion();
      negativeCriterion.campaignId = campaignId;

      Keyword keyword = new Keyword();
      keyword.matchType = KeywordMatchType.BROAD;
      keyword.text = "jupiter cruise";

      negativeCriterion.criterion = keyword;

      CampaignCriterionOperation operation = new CampaignCriterionOperation();
      operation.@operator = Operator.ADD;
      operation.operand = negativeCriterion;

      CampaignCriterionReturnValue retVal = null;

      Assert.DoesNotThrow(delegate() {
        retVal = campaignCriterionService.mutate(new CampaignCriterionOperation[] {operation});
      });
      Assert.NotNull(retVal);
      Assert.NotNull(retVal.value);
      Assert.AreEqual(retVal.value.Length, 1);
      Assert.NotNull(retVal.value[0]);
    }

    /// <summary>
    /// Test whether we can get all negative campaign criteria.
    /// </summary>
    [Test]
    public void TestGetAllNegativeCampaignCriteria() {
      long criterionId = new TestUtils().CreateCampaignNegativeKeyword(user, campaignId);
      CampaignCriterionSelector selector = new CampaignCriterionSelector();
      CampaignCriterionIdFilter filter = new CampaignCriterionIdFilter();
      filter.campaignId = campaignId;
      selector.idFilters = new CampaignCriterionIdFilter[] {filter};

      CampaignCriterionPage page = null;

      Assert.DoesNotThrow(delegate() {
        page = campaignCriterionService.get(selector);
      });
      Assert.NotNull(page);
      Assert.NotNull(page.entries);
      Assert.AreEqual(page.entries.Length, 1);
      Assert.NotNull(page.entries[0]);
      Assert.AreEqual(page.entries[0].campaignId, campaignId);
      Assert.NotNull(page.entries[0].criterion);
      Assert.AreEqual(page.entries[0].criterion.id, criterionId);
    }

    /// <summary>
    /// Test whether we can delete negative campaign criterion.
    /// </summary>
    [Test]
    public void TestRemoveCampaignNegativeCriterion() {
      long criterionId = new TestUtils().CreateCampaignNegativeKeyword(user, campaignId);

      Criterion criterion = new Criterion();
      criterion.id = criterionId;

      // Create ad group criterion.
      CampaignCriterion campaignCriterion = new CampaignCriterion();
      campaignCriterion.campaignId = campaignId;
      campaignCriterion.criterion = criterion;

      // Create operations.
      CampaignCriterionOperation operation = new CampaignCriterionOperation();
      operation.operand = campaignCriterion;
      operation.@operator = Operator.REMOVE;

      CampaignCriterionReturnValue retVal = null;

      Assert.DoesNotThrow(delegate() {
        retVal = campaignCriterionService.mutate(new CampaignCriterionOperation[] {operation});
      });
      Assert.NotNull(retVal);
      Assert.NotNull(retVal.value);
      Assert.AreEqual(retVal.value.Length, 1);
      Assert.NotNull(retVal.value[0]);
    }
  }
}
