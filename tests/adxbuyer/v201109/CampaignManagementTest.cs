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
  /// Test cases for all the code examples under v201109\CampaignManagement.
  /// </summary>
  class CampaignManagementTest : ExampleBaseTests {
    /// <summary>
    /// Inits this instance.
    /// </summary>
    [SetUp]
    public void Init() {
      parameters = new Dictionary<string, string>();

      long campaignId = utils.CreateCampaign(user, new ManualCPM());
      long adGroupId = utils.CreateAdGroup(user, campaignId, true);
      long adId = utils.CreateTextAd(user, adGroupId, false);

      parameters["CAMPAIGN_ID"] = campaignId.ToString();
      parameters["ADGROUP_ID"] = adGroupId.ToString();
      parameters["AD_ID"] = adId.ToString();
    }

    /// <summary>
    /// Tests the AddKeywordsInBulk VB.NET code example.
    /// </summary>
    [Test]
    public void TestAddPlacementsInBulkVBExample() {
      RunExample(new VBExamples.AddPlacementsInBulk());
    }

    /// <summary>
    /// Tests the AddKeywordsInBulk C# code example.
    /// </summary>
    [Test]
    public void TestAddPlacementsInBulkCSharpExample() {
      RunExample(new CSharpExamples.AddPlacementsInBulk());
    }

    /// <summary>
    /// Tests the GetAllDisapprovedAds VB.NET code example.
    /// </summary>
    [Test]
    public void TestGetAllDisapprovedAdsVBExample() {
      RunExample(new VBExamples.GetAllDisapprovedAds());
    }

    /// <summary>
    /// Tests the GetAllDisapprovedAds C# code example.
    /// </summary>
    [Test]
    public void TestGetAllDisapprovedAdsCSharpExample() {
      RunExample(new CSharpExamples.GetAllDisapprovedAds());
    }
  }
}
