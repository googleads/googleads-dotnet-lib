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
  /// UnitTests for <see cref="DataServiceTests"/> class.
  /// </summary>
  [TestFixture]
  class DataServiceTests : BaseTests {
    /// <summary>
    /// DataService object to be used in this test.
    /// </summary>
    private DataService dataService;

    /// <summary>
    /// The campaign id for which tests are run.
    /// </summary>
    private long campaignId = 0;

    /// <summary>
    /// The adgroup id for which tests are run.
    /// </summary>
    private long adGroupId = 0;

    /// <summary>
    /// The criterion id for which tests are run.
    /// </summary>
    private long criterionId = 0;

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public DataServiceTests() : base() {
    }

    /// <summary>
    /// Initialize the test case.
    /// </summary>
    [SetUp]
    public void Init() {
      dataService = (DataService)user.GetService(AdWordsService.v201101.DataService);
      TestUtils utils = new TestUtils();
      campaignId = utils.CreateCampaign(user, new ManualCPC());
      adGroupId = utils.CreateAdGroup(user, campaignId);
      criterionId = utils.CreateKeyword(user, adGroupId);
    }

    /// <summary>
    /// Test whether we can fetch existing bid landscape for a given ad group
    /// and criterion.
    /// </summary>
    [Test]
    public void TestGetCriterionBidLandscape() {
      // Create the selector.
      Selector selector = new Selector();
      selector.fields = new string[] {"AdGroupId", "CriterionId", "StartDate", "EndDate", "Bid",
          "LocalClicks", "LocalCost", "MarginalCpc", "LocalImpressions"};

      // Create the filters.
      Predicate adGroupPredicate = new Predicate();
      adGroupPredicate.field = "AdGroupId";
      adGroupPredicate.@operator = PredicateOperator.IN;
      adGroupPredicate.values = new string[] {adGroupId.ToString()};

      Predicate criterionPredicate = new Predicate();
      criterionPredicate.field = "CriterionId";
      criterionPredicate.@operator = PredicateOperator.IN;
      criterionPredicate.values = new string[] {criterionId.ToString()};

      selector.predicates = new Predicate[] {adGroupPredicate, criterionPredicate};

      CriterionBidLandscapePage page = null;

      Assert.DoesNotThrow(delegate() {
        page = dataService.getCriterionBidLandscape(selector);
      });

      if (page != null && page.entries != null && page.entries.Length > 0) {
        foreach (CriterionBidLandscape bidLandscape in page.entries) {
          Assert.NotNull(bidLandscape);
          Assert.AreEqual(bidLandscape.adGroupId, adGroupId);
          Assert.AreEqual(bidLandscape.criterionId, criterionId);
        }
      }
    }

    /// <summary>
    /// Test whether we can fetch existing bid landscape for a given ad group.
    /// </summary>
    [Test]
    public void TestGetAdGroupBidLandscape() {
      // Create selector.
      Selector selector = new Selector();
      selector.fields = new string[] {"AdGroupId", "LandscapeType", "LandscapeCurrent",
          "StartDate", "EndDate", "Bid", "LocalClicks", "LocalCost", "MarginalCpc",
          "LocalImpressions"};

      // Create the filters.
      Predicate adGroupPredicate = new Predicate();
      adGroupPredicate.field = "AdGroupId";
      adGroupPredicate.@operator = PredicateOperator.IN;
      adGroupPredicate.values = new string[] {adGroupId.ToString()};

      selector.predicates = new Predicate[] {adGroupPredicate};

      AdGroupBidLandscapePage page = null;

      Assert.DoesNotThrow(delegate() {
        page = dataService.getAdGroupBidLandscape(selector);
      });

      if (page != null && page.entries != null && page.entries.Length > 0) {
        foreach (AdGroupBidLandscape bidLandscape in page.entries) {
          Assert.NotNull(bidLandscape);
          Assert.AreEqual(bidLandscape.adGroupId, adGroupId);
        }
      }
    }
  }
}
