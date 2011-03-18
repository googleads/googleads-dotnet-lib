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
      adGroupCriterionService = (AdGroupCriterionService) user.GetService(
          AdWordsService.v201101.AdGroupCriterionService);

      campaignId = utils.CreateCampaign(user, new ManualCPC());
      keywordAdGroupId = utils.CreateAdGroup(user, campaignId);
      placementAdGroupId = utils.CreateAdGroup(user, campaignId);

      keywordId = utils.CreateKeyword(user, keywordAdGroupId);
    }

    /// <summary>
    /// Test whether we can add an ad group criterion keyword.
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
    /// Test whether we can add an ad group criterion placement.
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
    /// Test whether we can add cross ad group keywords.
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

      Assert.That(adGroupCriterionService.mutate(new AdGroupCriterionOperation[] {
          operation1, operation2}) is AdGroupCriterionReturnValue);
    }

    /// <summary>
    /// Test whether we can fetch active and paused biddable criteria.
    /// </summary>
    [Test]
    public void TestGetAllActivePausedBiddableCriteria() {
      // Create a selector.
      Selector selector = new Selector();
      selector.fields = new string[] {"Id", "AdGroupId", "KeywordText", "Status", "CriterionUse"};

      // Set filter conditions.
      Predicate statusPredicate = new Predicate();
      statusPredicate.field = "Status";
      statusPredicate.@operator = PredicateOperator.IN;
      statusPredicate.values = new string[] {UserStatus.ACTIVE.ToString(),
          UserStatus.PAUSED.ToString()};

      Predicate criterionUsePredicate = new Predicate();
      criterionUsePredicate.field = "CriterionUse";
      criterionUsePredicate.@operator = PredicateOperator.EQUALS;
      criterionUsePredicate.values = new string[] {CriterionUse.BIDDABLE.ToString()};

      selector.predicates = new Predicate[] {statusPredicate, criterionUsePredicate};

      AdGroupCriterionPage page = null;
      Assert.DoesNotThrow(delegate() {
        page = adGroupCriterionService.get(selector);
      });
      Assert.NotNull(page);
      Assert.NotNull(page.entries);
      Assert.Greater(page.entries.Length, 0);
      for (int i = 0; i <page.entries.Length; i++) {
        AdGroupCriterion agc = page.entries[i];
        Assert.NotNull(agc);
        Assert.IsInstanceOf<BiddableAdGroupCriterion>(agc);
        BiddableAdGroupCriterion biddableAgc = (BiddableAdGroupCriterion) agc;
        Assert.That(biddableAgc.userStatus == UserStatus.ACTIVE ||
            biddableAgc.userStatus == UserStatus.PAUSED);
      }
    }

    /// <summary>
    /// Test whether we can fetch all biddable criteria at campaign level.
    /// </summary>
    [Test]
    public void TestGetAllBiddableCriteriaCampaignLevel() {
      // Create a selector.
      Selector selector = new Selector();
      selector.fields = new string[] {"Id", "AdGroupId", "KeywordText", "Status", "CriterionUse"};

      // Set filter conditions.
      Predicate campaignPredicate = new Predicate();
      campaignPredicate.field = "CampaignId";
      campaignPredicate.@operator = PredicateOperator.EQUALS;
      campaignPredicate.values = new string[] {campaignId.ToString()};

      Predicate statusPredicate = new Predicate();
      statusPredicate.field = "Status";
      statusPredicate.@operator = PredicateOperator.IN;
      statusPredicate.values = new string[] {UserStatus.ACTIVE.ToString(),
          UserStatus.PAUSED.ToString()};

      Predicate criterionUsePredicate = new Predicate();
      criterionUsePredicate.field = "CriterionUse";
      criterionUsePredicate.@operator = PredicateOperator.EQUALS;
      criterionUsePredicate.values = new string[] {CriterionUse.BIDDABLE.ToString()};

      selector.predicates = new Predicate[] {campaignPredicate, statusPredicate,
          criterionUsePredicate};

      AdGroupCriterionPage page = null;
      Assert.DoesNotThrow(delegate() {
        page = adGroupCriterionService.get(selector);
      });
      Assert.NotNull(page);
      Assert.NotNull(page.entries);
      Assert.Greater(page.entries.Length, 0);
    }

    /// <summary>
    /// Test whether we can fetch a criterion by its id and adgroup id.
    /// </summary>
    [Test]
    public void TestGetCriterion() {
      // Create a selector.
      Selector selector = new Selector();
      selector.fields = new string[] {"Id", "AdGroupId", "KeywordText", "Status", "CriterionUse"};

      // Set filter conditions.
      Predicate statusPredicate = new Predicate();
      statusPredicate.field = "AdGroupId";
      statusPredicate.@operator = PredicateOperator.EQUALS;
      statusPredicate.values = new string[] {keywordAdGroupId.ToString()};

      Predicate criterionUsePredicate = new Predicate();
      criterionUsePredicate.field = "Id";
      criterionUsePredicate.@operator = PredicateOperator.EQUALS;
      criterionUsePredicate.values = new string[] {keywordId.ToString()};

      selector.predicates = new Predicate[] {statusPredicate, criterionUsePredicate};
      AdGroupCriterionPage page = null;
      Assert.DoesNotThrow(delegate() {
        page = adGroupCriterionService.get(selector);
      });
      Assert.NotNull(page);
      Assert.NotNull(page.entries);
      Assert.AreEqual(page.entries.Length, 1);

      Assert.NotNull(page.entries[0]);
      Assert.AreEqual(page.entries[0].adGroupId, keywordAdGroupId);
      Assert.NotNull(page.entries[0].criterion);
      Assert.AreEqual(page.entries[0].criterion.id, keywordId);
    }

    /// <summary>
    /// Test whether we can delete criterion at ad group level.
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
    /// Test whether we can update a keyword.
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
