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
  /// Test cases for all the code examples under v201109_1\Targeting.
  /// </summary>
  class TargetingTest : ExampleBaseTests {
    /// <summary>
    /// Inits this instance.
    /// </summary>
    [SetUp]
    public void Init() {
      parameters = new Dictionary<string, string>();

      long campaignId = utils.CreateCampaign(user, new ManualCPC());
      long campaignCriterionId = utils.AddCampaignTargetingCriteria(user, campaignId);
      parameters["CAMPAIGN_ID"] = campaignId.ToString();
    }

    /// <summary>
    /// Tests the AddCampaignTargetingCriteria VB.NET code example.
    /// </summary>
    [Test]
    public void TestAddCampaignTargetingCriteriaVBExample() {
      RunExample(new VBExamples.AddCampaignTargetingCriteria());
    }

    /// <summary>
    /// Tests the AddCampaignTargetingCriteria C# code example.
    /// </summary>
    [Test]
    public void TestAddCampaignTargetingCriteriaCSharpExample() {
      RunExample(new CSharpExamples.AddCampaignTargetingCriteria());
    }

    /// <summary>
    /// Tests the GetCampaignTargetingCriteria VB.NET code example.
    /// </summary>
    [Test]
    public void TestGetCampaignTargetingCriteriaVBExample() {
      RunExample(new VBExamples.GetCampaignTargetingCriteria());
    }

    /// <summary>
    /// Tests the GetCampaignTargetingCriteria C# code example.
    /// </summary>
    [Test]
    public void TestGetCampaignTargetingCriteriaCSharpExample() {
      RunExample(new CSharpExamples.GetCampaignTargetingCriteria());
    }

    /// <summary>
    /// Tests the GetTargetableLanguagesAndCarriers VB.NET code example.
    /// </summary>
    [Test]
    public void TestGetTargetableLanguagesAndCarriersVBExample() {
      RunExample(new VBExamples.GetTargetableLanguagesAndCarriers());
    }

    /// <summary>
    /// Tests the GetTargetableLanguagesAndCarriers C# code example.
    /// </summary>
    [Test]
    public void TestGetTargetableLanguagesAndCarriersCSharpExample() {
      RunExample(new CSharpExamples.GetTargetableLanguagesAndCarriers());
    }

    /// <summary>
    /// Tests the LookupLocation VB.NET code example.
    /// </summary>
    [Test]
    public void TestLookupLocationVBExample() {
      RunExample(new VBExamples.LookupLocation());
    }

    /// <summary>
    /// Tests the LookupLocation C# code example.
    /// </summary>
    [Test]
    public void TestLookupLocationCSharpExample() {
      RunExample(new CSharpExamples.LookupLocation());
    }

  }
}
