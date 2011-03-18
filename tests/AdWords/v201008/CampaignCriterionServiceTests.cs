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
using com.google.api.adwords.v201008;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Text;

namespace com.google.api.adwords.tests.v201008 {
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
      campaignId = utils.CreateCampaign(user, true);
    }

    /// <summary>
    /// Test whether we can add a campaign negative criterion.
    /// </summary>
    [Test]
    public void TestAddCampaignNegativeCriterion() {
      NegativeCampaignCriterion negativeCriterion = new NegativeCampaignCriterion();
      negativeCriterion.campaignIdSpecified = true;
      negativeCriterion.campaignId = campaignId;

      Keyword keyword = new Keyword();
      keyword.matchTypeSpecified = true;
      keyword.matchType = KeywordMatchType.BROAD;
      keyword.text = "jupiter cruise";

      negativeCriterion.criterion = keyword;

      CampaignCriterionOperation operation = new CampaignCriterionOperation();
      operation.operatorSpecified = true;
      operation.@operator = Operator.ADD;
      operation.operand = negativeCriterion;

      CampaignCriterionReturnValue result = null;

      Assert.DoesNotThrow(delegate() {
        result = campaignCriterionService.mutate(new CampaignCriterionOperation[] { operation });
      });
      Assert.NotNull(result);
      Assert.NotNull(result.value);
      Assert.AreEqual(result.value.Length, 1);
      Assert.NotNull(result.value[0]);
    }

    /// <summary>
    /// Test whether we can get all campaign negative criteria.
    /// </summary>
    [Test]
    public void TestGetAllCampaignNegativeCriteria() {
      long criterionId = new TestUtils().CreateCampaignNegativeKeyword(user, campaignId);
      CampaignCriterionSelector selector = new CampaignCriterionSelector();
      CampaignCriterionIdFilter filter = new CampaignCriterionIdFilter();
      filter.campaignIdSpecified = true;
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
    /// Test whether we can delete campaign negative criterion.
    /// </summary>
    [Test]
    public void TestRemoveCampaignNegativeCriterion() {
      long criterionId = new TestUtils().CreateCampaignNegativeKeyword(user, campaignId);

      Criterion criterion = new Criterion();
      criterion.id = criterionId;
      criterion.idSpecified = true;

      // Create ad group criterion.
      CampaignCriterion campaignCriterion = new CampaignCriterion();
      campaignCriterion.campaignId = campaignId;
      campaignCriterion.campaignIdSpecified = true;
      campaignCriterion.criterion = criterion;

      // Create operations.
      CampaignCriterionOperation operation = new CampaignCriterionOperation();
      operation.operand = campaignCriterion;
      operation.@operator = Operator.REMOVE;
      operation.operatorSpecified = true;

      CampaignCriterionReturnValue retval = null;

      Assert.DoesNotThrow(delegate() {
        retval = campaignCriterionService.mutate(new CampaignCriterionOperation[] {operation});
      });
      Assert.NotNull(retval);
      Assert.NotNull(retval.value);
      Assert.AreEqual(retval.value.Length, 1);
      Assert.NotNull(retval.value[0]);
    }
  }
}
