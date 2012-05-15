// Copyright 2012, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdWords.v201109_1;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

using CSharpExamples = Google.Api.Ads.AdWords.Examples.CSharp.v201109_1;
using VBExamples = Google.Api.Ads.AdWords.Examples.VB.v201109_1;

namespace Google.Api.Ads.AdWords.Tests.v201109_1 {
  /// <summary>
  /// Test cases for all the code examples under v201109_1\Optimization.
  /// </summary>
  class OptimizationTest : ExampleBaseTests {
    /// <summary>
    /// Inits this instance.
    /// </summary>
    [SetUp]
    public void Init() {
      parameters = new Dictionary<string, string>();

      long campaignId = utils.CreateCampaign(user, new ManualCPC());
      long adGroupId = utils.CreateAdGroup(user, campaignId);
      long keywordId = utils.CreateKeyword(user, adGroupId);
      parameters["CAMPAIGN_ID"] = campaignId.ToString();
      parameters["ADGROUP_ID"] = adGroupId.ToString();
      parameters["KEYWORD_ID"] = keywordId.ToString();
    }

    /// <summary>
    /// Tests the EstimateKeywordTraffic VB.NET code example.
    /// </summary>
    [Test]
    public void TestEstimateKeywordTrafficVBExample() {
      RunExample(new VBExamples.EstimateKeywordTraffic());
    }

    /// <summary>
    /// Tests the EstimateKeywordTraffic C# code example.
    /// </summary>
    [Test]
    public void TestEstimateKeywordTrafficCSharpExample() {
      RunExample(new CSharpExamples.EstimateKeywordTraffic());
    }

    /// <summary>
    /// Tests the GetAdGroupBidSimulations VB.NET code example.
    /// </summary>
    [Test]
    public void TestGetAdGroupBidSimulationsVBExample() {
      RunExample(new VBExamples.GetAdGroupBidSimulations());
    }

    /// <summary>
    /// Tests the GetAdGroupBidSimulations C# code example.
    /// </summary>
    [Test]
    public void TestGetAdGroupBidSimulationsCSharpExample() {
      RunExample(new CSharpExamples.GetAdGroupBidSimulations());
    }

    /// <summary>
    /// Tests the GetKeywordBidSimulations VB.NET code example.
    /// </summary>
    [Test]
    public void TestGetKeywordBidSimulationsVBExample() {
      RunExample(new VBExamples.GetKeywordBidSimulations());
    }

    /// <summary>
    /// Tests the GetKeywordBidSimulations C# code example.
    /// </summary>
    [Test]
    public void TestGetKeywordBidSimulationsCSharpExample() {
      RunExample(new CSharpExamples.GetKeywordBidSimulations());
    }

    /// <summary>
    /// Tests the GetKeywordIdeas VB.NET code example.
    /// </summary>
    [Test]
    public void TestGetKeywordIdeasVBExample() {
      RunExample(new VBExamples.GetKeywordIdeas());
    }

    /// <summary>
    /// Tests the GetKeywordIdeas C# code example.
    /// </summary>
    [Test]
    public void TestGetKeywordIdeasCSharpExample() {
      RunExample(new CSharpExamples.GetKeywordIdeas());
    }

    /// <summary>
    /// Tests the GetPlacementIdeas VB.NET code example.
    /// </summary>
    [Test]
    public void TestGetPlacementIdeasVBExample() {
      RunExample(new VBExamples.GetPlacementIdeas());
    }

    /// <summary>
    /// Tests the GetPlacementIdeas C# code example.
    /// </summary>
    [Test]
    public void TestGetPlacementIdeasCSharpExample() {
      RunExample(new CSharpExamples.GetPlacementIdeas());
    }
  }
}
