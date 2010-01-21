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
  /// Unittest for TrafficEstimatorService.
  /// </summary>
  [TestFixture]
  class TrafficEstimatorServiceTests : BaseTests {
    /// <summary>
    /// TrafficEstimatorService object to be used in this test.
    /// </summary>
    TrafficEstimatorService trafficEstimatorService;

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public TrafficEstimatorServiceTests()
      : base() {
    }

    /// <summary>
    /// Initialize the test case.
    /// </summary>
    [SetUp]
    public void Init() {
      AdWordsUser user = new AdWordsUser();
      trafficEstimatorService =
          (TrafficEstimatorService) user.GetService(AdWordsService.v13.TrafficEstimatorService);
    }

    /// <summary>
    /// Test whether we can check keyword traffic.
    /// </summary>
    [Test]
    public void TestCheckKeywordTraffic() {
      KeywordTrafficRequest request = new KeywordTrafficRequest();
      request.keywordText = "Flowers";
      request.keywordType = KeywordType.Broad;
      request.language = "en";
      Assert.That(trafficEstimatorService.checkKeywordTraffic(new KeywordTrafficRequest[] {request})
          is KeywordTraffic[]);
    }

    /// <summary>
    /// Test whether we can estimate ad group list.
    /// </summary>
    [Test]
    public void TestEstimateAdGroupList() {
      AdGroupRequest request = new AdGroupRequest();

      KeywordRequest keywordRequest = new KeywordRequest();
      keywordRequest.maxCpcSpecified = true;
      keywordRequest.maxCpc = 1000000;
      keywordRequest.negativeSpecified = true;
      keywordRequest.negative = false;
      keywordRequest.typeSpecified = true;
      keywordRequest.type = KeywordType.Broad;
      keywordRequest.text = "Flowers";

      request.keywordRequests = new KeywordRequest[] {keywordRequest};
      request.maxCpc = 1000000;

      Assert.That(trafficEstimatorService.estimateAdGroupList(new AdGroupRequest[] {request})
          is AdGroupEstimate[]);
    }

    /// <summary>
    /// Test whether we can estimate campaign list.
    /// </summary>
    [Test]
    public void TestEstimateCampaignList() {
      CampaignRequest request = new CampaignRequest();
      AdGroupRequest adGroupRequest = new AdGroupRequest();

      KeywordRequest keywordRequest = new KeywordRequest();
      keywordRequest.maxCpcSpecified = true;
      keywordRequest.maxCpc = 1000000;
      keywordRequest.negativeSpecified = true;
      keywordRequest.negative = false;
      keywordRequest.typeSpecified = true;
      keywordRequest.type = KeywordType.Broad;
      keywordRequest.text = "Flowers";

      adGroupRequest.keywordRequests = new KeywordRequest[] {keywordRequest};
      adGroupRequest.maxCpcSpecified = true;
      adGroupRequest.maxCpc = 1000000;

      request.geoTargeting = new GeoTarget();
      request.geoTargeting.cityTargets = new CityTargets();
      request.geoTargeting.cityTargets.cities = new string[] {"New York, NY US"};

      request.languageTargeting = new string[] {"en"};
      request.networkTargeting =
          new NetworkType[] {NetworkType.GoogleSearch, NetworkType.SearchNetwork};
      Assert.That(trafficEstimatorService.estimateCampaignList(new CampaignRequest[] {request})
          is CampaignEstimate[]);
    }

    /// <summary>
    /// Test whether we can estimate keyword list.
    /// </summary>
    [Test]
    public void TestEstimateKeywordList() {
      KeywordRequest request1 = new KeywordRequest();
      request1.maxCpcSpecified = true;
      request1.maxCpc = 1000000;
      request1.negativeSpecified = true;
      request1.negative = false;
      request1.text = "Flowers";
      request1.typeSpecified = true;
      request1.type = KeywordType.Broad;

      KeywordRequest request2 = new KeywordRequest();
      request2.maxCpcSpecified = true;
      request2.maxCpc = 2000000;
      request2.negativeSpecified = true;
      request2.negative = false;
      request2.text = "House";
      request2.typeSpecified = true;
      request2.type = KeywordType.Broad;

      Assert.That(trafficEstimatorService.estimateKeywordList(
          new KeywordRequest[] {request1, request2}) is KeywordEstimate[]);
    }
  }
}
