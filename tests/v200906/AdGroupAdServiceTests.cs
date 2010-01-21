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
  class AdGroupAdServiceTests : BaseTests {
    /// <summary>
    /// The campaign id for which tests are run.
    /// </summary>
    long campaignId = 0;

    /// <summary>
    /// The adgroup id for which tests are run.
    /// </summary>
    long adGroupId = 0;

    /// <summary>
    /// The Ad id for which tests are run.
    /// </summary>
    long adId = 0;

    /// <summary>
    /// AdGroupAdService object to be used in this test.
    /// </summary>
    AdGroupAdService adGroupAdService;

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public AdGroupAdServiceTests()
      : base() {
    }

    /// <summary>
    /// Initialize the test case.
    /// </summary>
    [SetUp]
    public void Init() {
      AdWordsUser user = new AdWordsUser();
      TestUtils utils = new TestUtils();
      adGroupAdService = 
          (AdGroupAdService) user.GetService(AdWordsService.v200906.AdGroupAdService);

      campaignId = utils.CreateCampaign(user, true);
      adGroupId = utils.CreateAdGroup(user, campaignId);
      adId = utils.CreateTextAd(user, adGroupId);
    }

    /// <summary>
    /// Test whether we can add a text ad using v200906.
    /// </summary>
    [Test]
    public void TestAddTextAd() {
      AdGroupAdOperation adGroupAdOperation = new AdGroupAdOperation();
      adGroupAdOperation.operatorSpecified = true;
      adGroupAdOperation.@operator = Operator.ADD;
      adGroupAdOperation.operand = new AdGroupAd();
      adGroupAdOperation.operand.adGroupIdSpecified = true;
      adGroupAdOperation.operand.adGroupId = adGroupId;
      TextAd ad = new TextAd();
      ad.url = "http://www.example.com";
      ad.displayUrl = "example.com";
      ad.description1 = "Visit the Red Planet in style.";
      ad.description2 = "Low-gravity fun for everyone!";
      ad.headline = "Luxury Cruise to Mars";
      adGroupAdOperation.operand.ad = ad;

      Assert.That(adGroupAdService.mutate(new AdGroupAdOperation[] {adGroupAdOperation})
          is AdGroupAdReturnValue);
  }

    /// <summary>
    /// Test whether we can fetch all ads using v200906.
    /// </summary>
    [Test]
    public void TestGetAllAdsCampaignLevel() {
      AdGroupAdSelector selector = new AdGroupAdSelector();
      selector.campaignIds = new long[] {campaignId};

      Assert.That(adGroupAdService.get(selector) is AdGroupAdPage);
    }

    /// <summary>
    /// Test whether we can fetch an ad using v200906.
    /// </summary>
    [Test]
    public void TestGetAd() {
      AdGroupAdSelector selector = new AdGroupAdSelector();
      selector.adGroupIds = new long[] {adGroupId};
      selector.adIds = new long[] {adId};

      Assert.That(adGroupAdService.get(selector) is AdGroupAdPage);
    }

    /// <summary>
    /// Test whether we can update an ad using v200906.
    /// </summary>
    [Test]
    public void TestUpdateAd() {
      AdGroupAdOperation operation = new AdGroupAdOperation();
      operation.operatorSpecified = true;
      operation.@operator = Operator.SET;
      operation.operand = new AdGroupAd();
      operation.operand.adGroupId = adGroupId;
      operation.operand.adGroupIdSpecified = true;
      operation.operand.ad = new Ad();
      operation.operand.ad.id = adId;
      operation.operand.ad.idSpecified = true;
      operation.operand.statusSpecified = true;
      operation.operand.status = AdGroupAdStatus.PAUSED;

      Assert.That(adGroupAdService.mutate(new AdGroupAdOperation[] {operation})
          is AdGroupAdReturnValue);
    }
  }
}
