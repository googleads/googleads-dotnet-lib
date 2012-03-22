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
using Google.Api.Ads.AdWords.v201109;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

using CSharpExamples = Google.Api.Ads.AdWords.Examples.CSharp.v201109;
using VBExamples = Google.Api.Ads.AdWords.Examples.VB.v201109;

namespace Google.Api.Ads.AdWords.Tests.v201109 {
  /// <summary>
  /// Test cases for all the code examples under v201109\Optimization.
  /// </summary>
  class OptimizationTest : ExampleBaseTests {
    /// <summary>
    /// Inits this instance.
    /// </summary>
    [SetUp]
    public void Init() {
      parameters = new Dictionary<string, string>();

      long campaignId = utils.CreateCampaign(user, new ManualCPM());
      long adGroupId = utils.CreateAdGroup(user, campaignId, true);
      parameters["CAMPAIGN_ID"] = campaignId.ToString();
      parameters["ADGROUP_ID"] = adGroupId.ToString();
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
