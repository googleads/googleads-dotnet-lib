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
using com.google.api.adwords.v13;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Text;

namespace com.google.api.adwords.tests.v13 {
  /// <summary>
  /// UnitTests for CriterionService.
  /// </summary>
  [TestFixture]
  class CriterionServiceTests : BaseTests {
    /// <summary>
    /// CriterionService object to be used in this test.
    /// </summary>
    CriterionService criterionService;

    /// <summary>
    /// The campaign id for which tests are run. (cpc operations)
    /// </summary>
    long cpcCampaignId = 0;

    /// <summary>
    /// The adgroup id for which tests are run.
    /// </summary>
    long adGroupId1 = 0;

    /// <summary>
    /// The keyword id for which tests are run.
    /// </summary>
    long keywordId = 0;

    /// <summary>
    /// The campaign id for which tests are run. (cpm operations)
    /// </summary>
    long cpmCampaignId = 0;

    /// <summary>
    /// The adgroup id for which tests are run.
    /// </summary>
    long adGroupId2 = 0;

    /// <summary>
    /// The AdWords user for which this test is run.
    /// </summary>
    AdWordsUser user;
    /// <summary>
    /// Default public constructor.
    /// </summary>
    public CriterionServiceTests()
      : base() {
    }

    /// <summary>
    /// Initialize the test case.
    /// </summary>
    [SetUp]
    public void Init() {
      user = new AdWordsUser();
      criterionService = (CriterionService) user.GetService(AdWordsService.v13.CriterionService);
      TestUtils utils = new TestUtils();
      cpcCampaignId = utils.CreateCampaign(user, true);
      adGroupId1 = utils.CreateAdGroup(user, cpcCampaignId);
      keywordId =  utils.CreateTestKeyword(user, adGroupId1);
      cpmCampaignId = utils.CreateCampaign(user, false);
      adGroupId2 = utils.CreateAdGroup(user, cpmCampaignId);
    }

    /// <summary>
    /// Test whether we can check a list of criteria using v13.
    /// </summary>
    /// <returns></returns>
    [Test]
    public void TestCheckCriteria() {
      Keyword keyword = new Keyword();
      keyword.adGroupId = adGroupId1;
      keyword.destinationUrl = "http://www.example.com";
      keyword.language = "en";
      keyword.maxCpcSpecified = true;
      keyword.maxCpc = 1000000;
      keyword.text = "car insurance";
      keyword.type = KeywordType.Broad;

      GeoTarget target = new GeoTarget();
      target.cityTargets = new CityTargets();
      target.cityTargets.cities = new string[] {"New York, NY US"};
      Assert.That(criterionService.checkCriteria(new Criterion[] {keyword}, new string[] {"en"},
          target) is ApiError[]);
    }

    /// <summary>
    /// Test whether we can check a list of criteria with policy violations.
    /// </summary>
    /// <returns></returns>
    [Test]
    public void TestCheckCriteriaFault() {
      Keyword keyword = new Keyword();
      keyword.adGroupId = adGroupId1;
      keyword.destinationUrl = "http://www.example.com";
      keyword.language = "en";
      keyword.maxCpcSpecified = true;
      keyword.maxCpc = 1000000;
      keyword.text = "car insurance!!!";
      keyword.type = KeywordType.Broad;

      GeoTarget target = new GeoTarget();
      target.cityTargets = new CityTargets();
      target.cityTargets.cities = new string[] {"New York, NY US"};
      Assert.That(criterionService.checkCriteria(new Criterion[] {keyword}, new string[] {"en"},
          target) is ApiError[]);
    }

    /// <summary>
    /// Test whether we can fetch all criteria from given ad group using v13.
    /// </summary>
    /// <returns></returns>
    [Test]
    public void TestGetAllCriteria() {
      Assert.That(criterionService.getAllCriteria(adGroupId1) is Criterion[]);
    }

    /// <summary>
    /// Test whether we can fetch campaign negative criteria using v13.
    /// </summary>
    /// <returns></returns>
    [Test]
    public void TestGetCampaignNegativeCriteria() {
      TestUtils utils = new TestUtils();
      long campaignCriteria = utils.CreateCampaignNegativeKeyword(user, cpcCampaignId);
      Assert.That(criterionService.getCampaignNegativeCriteria((int) cpcCampaignId) is Criterion[]);
    }

    /// <summary>
    /// Test whether we can fetch criteria from an ad group using v13.
    /// </summary>
    /// <returns></returns>
    [Test]
    public void TestGetCriteria() {
      Assert.That(criterionService.getCriteria(adGroupId1, new long[]{keywordId}) is Criterion[]);
    }

    /// <summary>
    /// Test whether we can fetch stats for given criteria using v13.
    /// </summary>
    /// <returns></returns>
    [Test]
    public void TestGetCriterionStats() {
      Assert.That(criterionService.getCriterionStats(adGroupId1, new long[]{keywordId},
          new DateTime(2009, 1, 1), new DateTime(2009, 1, 31)) is StatsRecord[]);
    }

    /// <summary>
    /// Test whether we can remove a criteria using v13.
    /// </summary>
    /// <returns></returns>
    [Test]
    public void TestRemoveCriteria() {
      TestUtils utils = new TestUtils();
      long tempKeyword = utils.CreateTestKeyword(user, adGroupId1);
      criterionService.removeCriteria(adGroupId1, new long[]{tempKeyword});
      Assert.Pass();
    }

    /// <summary>
    /// Test whether we can set campaign negative criteria using v13.
    /// </summary>
    /// <returns></returns>
    [Test]
    public void TestSetCampaignNegativeCriteria() {
      Keyword keyword = new Keyword();
      keyword.adGroupId = adGroupId1;
      keyword.destinationUrl = "http://www.example.com";
      keyword.language = "en";
      keyword.maxCpcSpecified = true;
      keyword.maxCpc = 1000000;
      keyword.text = "car insurance";
      keyword.type = KeywordType.Broad;

      criterionService.setCampaignNegativeCriteria((int) cpcCampaignId, new Criterion[] {keyword});
      Assert.Pass();
    }

    /// <summary>
    /// Test whether we can update existing criteria using v13.
    /// </summary>
    [Test]
    public void TestUpdateCriteria() {
      TestUtils utils = new TestUtils();
      long tempKeyword = utils.CreateTestKeyword(user, adGroupId1);

      Keyword keyword = new Keyword();
      keyword.id = tempKeyword;
      keyword.pausedSpecified = true;
      keyword.paused = true;

      criterionService.updateCriteria(new Criterion[] {keyword});
      Assert.Pass();
    }
  }
}
