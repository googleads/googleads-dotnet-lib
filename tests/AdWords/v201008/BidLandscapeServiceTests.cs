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
  /// UnitTests for <see cref="BidLandScapeService"/> class.
  /// </summary>
  [TestFixture]
  class BidLandscapeServiceTests : BaseTests {
    /// <summary>
    /// BidLandscapeService object to be used in this test.
    /// </summary>
    private BidLandscapeService bidLandscapeService;

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
    public BidLandscapeServiceTests() : base() {
    }

    /// <summary>
    /// Initialize the test case.
    /// </summary>
    [SetUp]
    public void Init() {
      bidLandscapeService = (BidLandscapeService)user.GetService(
          AdWordsService.v201008.BidLandscapeService);
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
    public void TestGetBidLandscape() {
      // Create selector.
      CriterionBidLandscapeSelector selector = new CriterionBidLandscapeSelector();

      // Create id filter.
      BidLandscapeIdFilter idFilter = new BidLandscapeIdFilter();
      idFilter.adGroupId = adGroupId;
      idFilter.criterionId = criterionId;
      selector.idFilters = new BidLandscapeIdFilter[] {idFilter};

      // Get bid landscape for ad group criteria.
      BidLandscape[] bidLandscapes = null;

      Assert.DoesNotThrow(delegate() {
        bidLandscapes = bidLandscapeService.getBidLandscape(selector);
      });

      if (bidLandscapes != null && bidLandscapes.Length > 0) {
        foreach (BidLandscape bidLandscape in bidLandscapes) {
          Assert.NotNull(bidLandscape);
          Assert.AreEqual(bidLandscape.adGroupId, adGroupId);
          Assert.That(bidLandscape is CriterionBidLandscape);
          Assert.AreEqual((bidLandscape as CriterionBidLandscape).criterionId, criterionId);
        }
      }
    }
  }
}
