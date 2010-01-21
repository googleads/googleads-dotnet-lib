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
using com.google.api.adwords.v200906;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Text;

namespace com.google.api.adwords.tests.v200906 {
  /// <summary>
  /// UnitTests for <see cref="AdGroupAdService"/> class.
  /// </summary>
  [TestFixture]
  class CampaignCriterionServiceTests : BaseTests {
    /// <summary>
    /// The campaign id for which tests are run.
    /// </summary>
    long campaignId = 0;

    /// <summary>
    /// CampaignCriterionService object to be used in this test.
    /// </summary>
    CampaignCriterionService campaignCriterionService;

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public CampaignCriterionServiceTests()
      : base() {
    }

    /// <summary>
    /// Initialize the test case.
    /// </summary>
    [SetUp]
    public void SetUp() {
      AdWordsUser user = new AdWordsUser();
      TestUtils utils = new TestUtils();
      campaignCriterionService = (CampaignCriterionService) user.GetService(
          AdWordsService.v200906.CampaignCriterionService);

      campaignId = utils.CreateCampaign(user, true);
    }

    /// <summary>
    /// Test whether we can add an ad group criterion keyword using v200906.
    /// </summary>
    [Test]
    public void TestAddCriterionKeyword() {
      CampaignCriterionOperation operation = new CampaignCriterionOperation();
      operation.@operator = Operator.ADD;
      operation.operatorSpecified = true;

      NegativeCampaignCriterion criterion = new NegativeCampaignCriterion();
      criterion.criterion = new Keyword();
      (criterion.criterion as Keyword).matchTypeSpecified = true;
      (criterion.criterion as Keyword).matchType = KeywordMatchType.BROAD;
      (criterion.criterion as Keyword).text = "mars cruise";
      operation.operand = criterion;
      operation.operand.campaignIdSpecified = true;
      operation.operand.campaignId = campaignId;

      Assert.That(campaignCriterionService.mutate(new CampaignCriterionOperation[] {operation})
          is CampaignCriterionReturnValue);
    }

    /// <summary>
    /// Test whether we can fetch criteria at campaign level using v200906.
    /// </summary>
    [Test]
    public void TestGetAllCriteriaCampaignLevel() {
      CampaignCriterionSelector selector = new CampaignCriterionSelector();
      CampaignCriterionIdFilter filter = new CampaignCriterionIdFilter();
      filter.campaignId = campaignId;
      filter.campaignIdSpecified = true;
      selector.idFilters = new CampaignCriterionIdFilter[] {filter};

      Assert.That(campaignCriterionService.get(selector) is CampaignCriterionPage);
    }

    /// <summary>
    /// Test whether we can delete criterion at campaign level using v200906.
    /// </summary>
    [Test]
    public void TestDeleteCriterion() {
      CampaignCriterionOperation operation1 = new CampaignCriterionOperation();
      operation1.@operator = Operator.ADD;
      operation1.operatorSpecified = true;
      operation1.operand = new CampaignCriterion();

      NegativeCampaignCriterion criterion1 = new NegativeCampaignCriterion();
      criterion1.criterion = new Keyword();
      (criterion1.criterion as Keyword).matchTypeSpecified = true;
      (criterion1.criterion as Keyword).matchType = KeywordMatchType.BROAD;
      (criterion1.criterion as Keyword).text = "mars cruise";
      operation1.operand = criterion1;
      operation1.operand.campaignIdSpecified = true;
      operation1.operand.campaignId = campaignId;

      CampaignCriterionReturnValue retVal =
        campaignCriterionService.mutate(new CampaignCriterionOperation[] {operation1});
      if (retVal != null && retVal.value != null && retVal.value.Length > 0) {
        long tempCriterionId = retVal.value[0].criterion.id;

        CampaignCriterionOperation operation2 = new CampaignCriterionOperation();
        operation2.@operator = Operator.REMOVE;
        operation2.operatorSpecified = true;
        operation2.operand = new CampaignCriterion();

        NegativeCampaignCriterion criterion2 = new NegativeCampaignCriterion();
        criterion2.criterion = new Keyword();
        (criterion2.criterion as Keyword).idSpecified = true;
        (criterion2.criterion as Keyword).id = tempCriterionId;
        (criterion2.criterion as Keyword).matchTypeSpecified = true;
        (criterion2.criterion as Keyword).matchType = KeywordMatchType.BROAD;
        (criterion2.criterion as Keyword).text = "mars cruise";
        criterion2.campaignIdSpecified = true;
        criterion2.campaignId = campaignId;

        operation2.operand = criterion2;
        Assert.That(campaignCriterionService.mutate(new CampaignCriterionOperation[] {operation2})
            is CampaignCriterionReturnValue);
      } else {
        Assert.Fail("Could not create campaign negative criteria.");
      }
    }
  }
}
