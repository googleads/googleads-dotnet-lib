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
  /// Test cases for all the code examples under v201109\BasicOperations.
  /// </summary>
  class BasicOperationsTest : ExampleBaseTests {
    /// <summary>
    /// Inits this instance.
    /// </summary>
    [SetUp]
    public void Init() {
      parameters = new Dictionary<string, string>();
      long campaignId = utils.CreateCampaign(user, new ManualCPC());
      long adGroupId = utils.CreateAdGroup(user, campaignId);
      long adId = utils.CreateTextAd(user, adGroupId, false);
      long keywordId = utils.CreateKeyword(user, adGroupId);

      parameters["CAMPAIGN_ID"] = campaignId.ToString();
      parameters["ADGROUP_ID"] = adGroupId.ToString();
      parameters["AD_ID"] = adId.ToString();
      parameters["KEYWORD_ID"] = keywordId.ToString();
    }

    /// <summary>
    /// Tests the AddAdGroup VB.NET code example.
    /// </summary>
    [Test]
    public void TestAddAdGroupVBExample() {
      RunExample(new VBExamples.AddAdGroups());
    }

    /// <summary>
    /// Tests the AddAdGroup C# code example.
    /// </summary>
    [Test]
    public void TestAddAdGroupCSharpExample() {
      RunExample(new CSharpExamples.AddAdGroups());
    }

    /// <summary>
    /// Tests the AddCampaign VB.NET code example.
    /// </summary>
    [Test]
    public void TestAddCampaignVBExample() {
      RunExample(new VBExamples.AddCampaigns());
    }

    /// <summary>
    /// Tests the AddCampaign C# code example.
    /// </summary>
    [Test]
    public void TestAddCampaignCSharpExample() {
      RunExample(new CSharpExamples.AddCampaigns());
    }

    /// <summary>
    /// Tests the AddKeywords VB.NET code example.
    /// </summary>
    [Test]
    public void TestAddKeywordsVBExample() {
      RunExample(new VBExamples.AddKeywords());
    }

    /// <summary>
    /// Tests the AddKeywords C# code example.
    /// </summary>
    [Test]
    public void TestAddKeywordsCSharpExample() {
      RunExample(new CSharpExamples.AddKeywords());
    }

    /// <summary>
    /// Tests the AddTextAds VB.NET code example.
    /// </summary>
    [Test]
    public void TestAddTextAdsVBExample() {
      RunExample(new VBExamples.AddTextAds());
    }

    /// <summary>
    /// Tests the AddTextAds C# code example.
    /// </summary>
    [Test]
    public void TestAddTextAdsCSharpExample() {
      RunExample(new CSharpExamples.AddTextAds());
    }

    /// <summary>
    /// Tests the DeleteAdGroup VB.NET code example.
    /// </summary>
    [Test]
    public void TestDeleteAdGroupVBExample() {
      RunExample(new VBExamples.DeleteAdGroup());
    }

    /// <summary>
    /// Tests the DeleteAdGroup C# code example.
    /// </summary>
    [Test]
    public void TestDeleteAdGroupCSharpExample() {
      RunExample(new CSharpExamples.DeleteAdGroup());
    }

    /// <summary>
    /// Tests the DeleteAd VB.NET code example.
    /// </summary>
    [Test]
    public void TestDeleteAdVBExample() {
      RunExample(new VBExamples.DeleteAd());
    }

    /// <summary>
    /// Tests the DeleteAd C# code example.
    /// </summary>
    [Test]
    public void TestDeleteAdCSharpExample() {
      RunExample(new CSharpExamples.DeleteAd());
    }

    /// <summary>
    /// Tests the DeleteCampaign VB.NET code example.
    /// </summary>
    [Test]
    public void TestDeleteCampaignVBExample() {
      RunExample(new VBExamples.DeleteCampaign());
    }

    /// <summary>
    /// Tests the DeleteCampaign C# code example.
    /// </summary>
    [Test]
    public void TestDeleteCampaignCSharpExample() {
      RunExample(new CSharpExamples.DeleteCampaign());
    }

    /// <summary>
    /// Tests the DeleteKeyword VB.NET code example.
    /// </summary>
    [Test]
    public void TestDeleteKeywordVBExample() {
      RunExample(new VBExamples.DeleteKeyword());
    }

    /// <summary>
    /// Tests the DeleteKeyword C# code example.
    /// </summary>
    [Test]
    public void TestDeleteKeywordCSharpExample() {
      RunExample(new CSharpExamples.DeleteKeyword());
    }

    /// <summary>
    /// Tests the GetAdGroups VB.NET code example.
    /// </summary>
    [Test]
    public void TestGetAdGroupsVBExample() {
      RunExample(new VBExamples.GetAdGroups());
    }

    /// <summary>
    /// Tests the GetAdGroups C# code example.
    /// </summary>
    [Test]
    public void TestGetAdGroupsCSharpExample() {
      RunExample(new CSharpExamples.GetAdGroups());
    }

    /// <summary>
    /// Tests the GetCampaigns VB.NET code example.
    /// </summary>
    [Test]
    public void TestGetCampaignsVBExample() {
      RunExample(new VBExamples.GetCampaigns());
    }

    /// <summary>
    /// Tests the GetCampaigns C# code example.
    /// </summary>
    [Test]
    public void TestGetCampaignsCSharpExample() {
      RunExample(new CSharpExamples.GetCampaigns());
    }

    /// <summary>
    /// Tests the GetKeywords VB.NET code example.
    /// </summary>
    [Test]
    public void TestGetKeywordsVBExample() {
      RunExample(new VBExamples.GetKeywords());
    }

    /// <summary>
    /// Tests the GetKeywords C# code example.
    /// </summary>
    [Test]
    public void TestGetKeywordsCSharpExample() {
      RunExample(new CSharpExamples.GetKeywords());
    }

    /// <summary>
    /// Tests the GetTextAds VB.NET code example.
    /// </summary>
    [Test]
    public void TestGetTextAdsVBExample() {
      RunExample(new VBExamples.GetTextAds());
    }

    /// <summary>
    /// Tests the GetTextAds C# code example.
    /// </summary>
    [Test]
    public void TestGetTextAdsCSharpExample() {
      RunExample(new CSharpExamples.GetTextAds());
    }

    /// <summary>
    /// Tests the PauseAd VB.NET code example.
    /// </summary>
    [Test]
    public void TestPauseAdVBExample() {
      RunExample(new VBExamples.PauseAd());
    }

    /// <summary>
    /// Tests the PauseAd C# code example.
    /// </summary>
    [Test]
    public void TestPauseAdCSharpExample() {
      RunExample(new CSharpExamples.PauseAd());
    }

    /// <summary>
    /// Tests the UpdateAdGroup VB.NET code example.
    /// </summary>
    [Test]
    public void TestUpdateAdGroupVBExample() {
      RunExample(new VBExamples.UpdateAdGroup());
    }

    /// <summary>
    /// Tests the UpdateAdGroup C# code example.
    /// </summary>
    [Test]
    public void TestUpdateAdGroupCSharpExample() {
      RunExample(new CSharpExamples.UpdateAdGroup());
    }

    /// <summary>
    /// Tests the UpdateCampaign VB.NET code example.
    /// </summary>
    [Test]
    public void TestUpdateCampaignVBExample() {
      RunExample(new VBExamples.UpdateCampaign());
    }

    /// <summary>
    /// Tests the UpdateCampaign C# code example.
    /// </summary>
    [Test]
    public void TestUpdateCampaignCSharpExample() {
      RunExample(new CSharpExamples.UpdateCampaign());
    }

    /// <summary>
    /// Tests the UpdateKeyword VB.NET code example.
    /// </summary>
    [Test]
    public void TestUpdateKeywordVBExample() {
      RunExample(new VBExamples.UpdateKeyword());
    }

    /// <summary>
    /// Tests the UpdateKeyword C# code example.
    /// </summary>
    [Test]
    public void TestUpdateKeywordCSharpExample() {
      RunExample(new CSharpExamples.UpdateKeyword());
    }
  }
}
