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
  /// UnitTests for <see cref="AdGroupCriterionService"/> class.
  /// </summary>
  [TestFixture]
  class AdGroupCriterionServiceTests : BaseTests {
    /// <summary>
    /// AdGroupCriterionService object to be used in this test.
    /// </summary>
    private AdGroupCriterionService adGroupCriterionService;

    /// <summary>
    /// The campaign id to be used for tests.
    /// </summary>
    private long campaignId = 0;

    /// <summary>
    /// The AdGroup id to be used for tests (keyword operations).
    /// </summary>
    private long keywordAdGroupId = 0;

    /// <summary>
    /// The AdGroup id to be used for tests (placement operations).
    /// </summary>
    private long placementAdGroupId = 0;

    /// <summary>
    /// The keyword id to be used for tests.
    /// </summary>
    private long keywordId = 0;

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public AdGroupCriterionServiceTests() : base() {
    }

    /// <summary>
    /// Initialize the test case.
    /// </summary>
    [SetUp]
    public void Init() {
      TestUtils utils = new TestUtils();
      adGroupCriterionService =
          (AdGroupCriterionService) user.GetService(AdWordsService.v200909.AdGroupCriterionService);

      campaignId = utils.CreateCampaign(user, new ManualCPC());
      keywordAdGroupId = utils.CreateAdGroup(user, campaignId);
      placementAdGroupId = utils.CreateAdGroup(user, campaignId);

      keywordId = utils.CreateKeyword(user, keywordAdGroupId);
    }

    /// <summary>
    /// Test whether we can add an ad group criterion keyword using v200909.
    /// </summary>
    [Test]
    public void TestAddCriterionKeyword() {
      AdGroupCriterionOperation operation = new AdGroupCriterionOperation();
      operation.@operator = Operator.ADD;
      operation.operand = new BiddableAdGroupCriterion();
      operation.operand.adGroupId = keywordAdGroupId;

      Keyword keyword = new Keyword();
      keyword.matchType = KeywordMatchType.BROAD;
      keyword.text = "mars cruise";
      operation.operand.criterion = keyword;

      Assert.That(adGroupCriterionService.mutate(new AdGroupCriterionOperation[] {operation})
          is AdGroupCriterionReturnValue);
    }

    /// <summary>
    /// Test whether we can add an ad group criterion placement using v200909.
    /// </summary>
    [Test]
    public void TestAddCriterionPlacement() {
      AdGroupCriterionOperation operation = new AdGroupCriterionOperation();
      operation.@operator = Operator.ADD;
      operation.operand = new BiddableAdGroupCriterion();
      operation.operand.adGroupId = placementAdGroupId;

      Placement placement = new Placement();
      placement.url = "www.example.com";
      operation.operand.criterion = placement;

      Assert.That(adGroupCriterionService.mutate(new AdGroupCriterionOperation[] {operation})
          is AdGroupCriterionReturnValue);
    }

    /// <summary>
    /// Test whether we can add cross ad group keywords using v200909.
    /// </summary>
    [Test]
    public void TestAddKeywordCrossAdGroup() {
      AdGroupCriterionOperation operation1 = new AdGroupCriterionOperation();
      operation1.@operator = Operator.ADD;
      operation1.operand = new BiddableAdGroupCriterion();
      operation1.operand.adGroupId = keywordAdGroupId;

      Keyword keyword = new Keyword();
      keyword.matchType = KeywordMatchType.BROAD;
      keyword.text = "mars cruise";
      operation1.operand.criterion = keyword;

      AdGroupCriterionOperation operation2 = new AdGroupCriterionOperation();
      operation2.@operator = Operator.ADD;
      operation2.operand = new BiddableAdGroupCriterion();
      operation2.operand.adGroupId = placementAdGroupId;

      Placement placement = new Placement();
      placement.url = "www.example.com";
      operation2.operand.criterion = keyword;

      Assert.That(adGroupCriterionService.mutate(
          new AdGroupCriterionOperation[] {operation1, operation2}) is AdGroupCriterionReturnValue);
    }

    /// <summary>
    /// Test whether we can fetch active and paused criteria using v200909.
    /// </summary>
    [Test]
    public void TestGetAllActivePausedCriteria() {
      AdGroupCriterionSelector selector = new AdGroupCriterionSelector();
      selector.criterionUse = CriterionUse.BIDDABLE;
      selector.userStatuses = new UserStatus[] {UserStatus.ACTIVE, UserStatus.PAUSED};

      Assert.That(adGroupCriterionService.get(selector) is AdGroupCriterionPage);
    }

    /// <summary>
    /// Test whether we can fetch criteria at campaign level using v200909.
    /// </summary>
    [Test]
    public void TestGetAllCriteriaCampaignLevel() {
      AdGroupCriterionSelector selector = new AdGroupCriterionSelector();
      AdGroupCriterionIdFilter filter = new AdGroupCriterionIdFilter();
      filter.campaignId = campaignId;
      selector.idFilters = new AdGroupCriterionIdFilter[] {filter};

      Assert.That(adGroupCriterionService.get(selector) is AdGroupCriterionPage);
    }

    /// <summary>
    /// Test whether we can fetch criterion at criterion level using v200909.
    /// </summary>
    [Test]
    public void TestGetCriterion() {
      AdGroupCriterionSelector selector = new AdGroupCriterionSelector();
      AdGroupCriterionIdFilter filter = new AdGroupCriterionIdFilter();
      filter.adGroupId = keywordAdGroupId;
      filter.criterionId = keywordId;
      selector.idFilters = new AdGroupCriterionIdFilter[] {filter};

      Assert.That(adGroupCriterionService.get(selector) is AdGroupCriterionPage);
    }

    /// <summary>
    /// Test whether we can delete criterion at ad group level using v200909.
    /// </summary>
    [Test]
    public void TestDeleteCriterion() {
      AdGroupCriterionOperation operation = new AdGroupCriterionOperation();
      operation.@operator = Operator.REMOVE;
      operation.operand = new BiddableAdGroupCriterion();
      operation.operand.adGroupId = keywordAdGroupId;
      operation.operand.criterion = new Criterion();
      operation.operand.criterion.id = keywordId;

      Assert.That(adGroupCriterionService.mutate(new AdGroupCriterionOperation[] {operation})
          is AdGroupCriterionReturnValue);
    }

    /// <summary>
    /// Test whether we can update a keyword using v200909.
    /// </summary>
    [Test]
    public void TestUpdateCriterionKeyword() {
      AdGroupCriterionOperation operation = new AdGroupCriterionOperation();
      operation.@operator = Operator.SET;
      operation.operand = new BiddableAdGroupCriterion();
      operation.operand.adGroupId = keywordAdGroupId;
      (operation.operand as BiddableAdGroupCriterion).userStatus = UserStatus.PAUSED;
      operation.operand.criterion = new Criterion();
      operation.operand.criterion.id = keywordId;

      Assert.That(adGroupCriterionService.mutate(new AdGroupCriterionOperation[] {operation})
          is AdGroupCriterionReturnValue);
    }
  }
}
