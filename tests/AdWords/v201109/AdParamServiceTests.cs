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
using Google.Api.Ads.AdWords.v201109;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.AdWords.Tests.v201109 {
  /// <summary>
  /// UnitTests for <see cref="AdParamService"/> class.
  /// </summary>
  [TestFixture]
  class AdParamServiceTests : BaseTests {
    /// <summary>
    /// AdParamService object to be used in this test.
    /// </summary>
    private AdParamService adParamService;

    /// <summary>
    /// The campaign id for which tests are run.
    /// </summary>
    private long campaignId = 0;

    /// <summary>
    /// The adgroup id for which tests are run.
    /// </summary>
    private long adGroupId = 0;

    /// <summary>
    /// The ad id for which tests are run.
    /// </summary>
    private long adId = 0;

    /// <summary>
    /// The keyword id1 for which tests are run.
    /// </summary>
    private long keywordId = 0;

    /// <summary>
    /// The keyword id2 for which tests are run.
    /// </summary>
    private long keywordIdNoParam = 0;

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public AdParamServiceTests() : base() {
    }

    /// <summary>
    /// Initialize the test case.
    /// </summary>
    [SetUp]
    public void Init() {
      TestUtils utils = new TestUtils();
      adParamService = (AdParamService)user.GetService(AdWordsService.v201109.AdParamService);
      campaignId = utils.CreateCampaign(user, new ManualCPC());
      adGroupId = utils.CreateAdGroup(user, campaignId);
      adId = utils.CreateTextAd(user, adGroupId, true);
      keywordId = utils.CreateKeyword(user, adGroupId);
      utils.SetAdParam(user, adGroupId, keywordId);
      keywordIdNoParam = utils.CreateKeyword(user, adGroupId);
    }

    /// <summary>
    /// Test whether we can fetch all existing ad params in a given ad group.
    /// </summary>
    [Test]
    public void TestGetAllAdParamsForAdGroup() {
      // Create a selector.
      Selector selector = new Selector();
      selector.fields = new string[] {"AdGroupId", "CriterionId", "InsertionText", "ParamIndex"};

      // Set a filter condition.
      Predicate predicate = new Predicate();
      predicate.field = "AdGroupId";
      predicate.@operator = PredicateOperator.EQUALS;
      predicate.values = new string[] {adGroupId.ToString()};

      selector.predicates = new Predicate[] {predicate};

      AdParamPage paramPage = null;

      Assert.DoesNotThrow(delegate() {
        paramPage = adParamService.get(selector);
      });
      Assert.NotNull(paramPage);
      Assert.NotNull(paramPage.entries);
      Assert.AreEqual(paramPage.entries.Length, 1);
    }

    /// <summary>
    /// Test whether we can fetch an existing ad param for a given ad group and
    /// criterion.
    /// </summary>
    [Test]
    public void TestGetAdParam() {
      // Create a selector.
      Selector selector = new Selector();
      selector.fields = new string[] {"AdGroupId", "CriterionId", "InsertionText", "ParamIndex"};

      // Set a filter condition.
      Predicate predicate1 = new Predicate();
      predicate1.field = "AdGroupId";
      predicate1.@operator = PredicateOperator.EQUALS;
      predicate1.values = new string[] {adGroupId.ToString()};

      Predicate predicate2 = new Predicate();
      predicate2.field = "CriterionId";
      predicate2.@operator = PredicateOperator.EQUALS;
      predicate2.values = new string[] {keywordId.ToString()};

      selector.predicates = new Predicate[] {predicate1, predicate2};

      AdParamPage paramPage = adParamService.get(selector);
      Assert.NotNull(paramPage);
      Assert.NotNull(paramPage.entries);
      Assert.AreEqual(paramPage.entries.Length, 1);
    }

    /// <summary>
    /// Test whether we can create a new ad param.
    /// </summary>
    [Test]
    public void TestCreateAdParam() {
      // Prepare for setting ad parameters.
      AdParam adParam = new AdParam();
      adParam.adGroupId = adGroupId;
      adParam.criterionId = keywordIdNoParam;
      adParam.paramIndex = 1;
      adParam.insertionText = "$100";

      AdParamOperation adParamOperation = new AdParamOperation();
      adParamOperation.@operator = Operator.SET;
      adParamOperation.operand = adParam;

      AdParam[] newAdParams = null;

      Assert.DoesNotThrow(delegate() {
        // Set ad parameters.
        newAdParams = adParamService.mutate(new AdParamOperation[] {adParamOperation});
      });
      Assert.NotNull(newAdParams);
      Assert.AreEqual(newAdParams.Length, 1);
      Assert.NotNull(newAdParams[0]);
      Assert.AreEqual(newAdParams[0].adGroupId, adGroupId);
      Assert.AreEqual(newAdParams[0].criterionId, keywordIdNoParam);
      Assert.AreEqual(newAdParams[0].paramIndex, 1);
    }

    /// <summary>
    /// Test whether we can update an existing ad param.
    /// </summary>
    [Test]
    public void TestUpdateAdParam() {
      // Prepare for setting ad parameters.
      AdParam adParam = new AdParam();
      adParam.adGroupId = adGroupId;
      adParam.criterionId = keywordId;
      adParam.paramIndex = 1;
      adParam.insertionText = "$39";

      AdParamOperation adParamOperation = new AdParamOperation();
      adParamOperation.@operator = Operator.SET;
      adParamOperation.operand = adParam;

      // Set ad parameters.
      AdParam[] newAdParams = adParamService.mutate(new AdParamOperation[] {adParamOperation});
      Assert.NotNull(newAdParams);
      Assert.AreEqual(newAdParams.Length, 1);
      Assert.NotNull(newAdParams[0]);
      Assert.AreEqual(newAdParams[0].insertionText, "$39");
    }

    /// <summary>
    /// Test whether we can remove an existing ad param.
    /// </summary>
    [Test]
    public void TestRemoveAdParam() {
      // Prepare for setting ad parameters.
      AdParam adParam = new AdParam();
      adParam.adGroupId = adGroupId;
      adParam.criterionId = keywordId;
      adParam.paramIndex = 1;

      AdParamOperation priceOperation = new AdParamOperation();
      priceOperation.@operator = Operator.REMOVE;
      priceOperation.operand = adParam;

      // Set ad parameters.
      AdParam[] newAdParams = adParamService.mutate(new AdParamOperation[] {priceOperation});
      Assert.NotNull(newAdParams);
      Assert.AreEqual(newAdParams.Length, 1);
      Assert.NotNull(newAdParams[0]);
    }
  }
}
