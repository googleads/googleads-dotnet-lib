// Copyright 2015, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdWords.v201506;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

using CSharpExamples = Google.Api.Ads.AdWords.Examples.CSharp.v201506;
using VBExamples = Google.Api.Ads.AdWords.Examples.VB.v201506;

namespace Google.Api.Ads.AdWords.Tests.v201506 {
  /// <summary>
  /// Test cases for all the code examples under v201506\CampaignManagement.
  /// </summary>
  class CampaignManagementTest : VersionedExampleTestsBase {
    long campaignId;
    long adGroupId;

    /// <summary>
    /// Inits this instance.
    /// </summary>
    [SetUp]
    public void Init() {
      campaignId = utils.CreateDisplayCampaign(user, BiddingStrategyType.MANUAL_CPM);
      adGroupId = utils.CreateAdGroup(user, campaignId, true);
    }

    /// <summary>
    /// Tests the AddKeywordsInBulk VB.NET code example.
    /// </summary>
    [Test]
    public void TestAddPlacementsInBulkVBExample() {
      RunExample(delegate() {
        new VBExamples.AddPlacementsInBulk().Run(user, adGroupId);
      });
    }

    /// <summary>
    /// Tests the AddKeywordsInBulk C# code example.
    /// </summary>
    [Test]
    public void TestAddPlacementsInBulkCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.AddPlacementsInBulk().Run(user, adGroupId);
      });
    }

    /// <summary>
    /// Tests the GetAllDisapprovedAds VB.NET code example.
    /// </summary>
    [Test]
    public void TestGetAllDisapprovedAdsVBExample() {
      RunExample(delegate() {
        new VBExamples.GetAllDisapprovedAds().Run(user, campaignId);
      });
    }

    /// <summary>
    /// Tests the GetAllDisapprovedAds C# code example.
    /// </summary>
    [Test]
    public void TestGetAllDisapprovedAdsCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.GetAllDisapprovedAds().Run(user, campaignId);
      });
    }

    /// <summary>
    /// Tests the GetAllDisapprovedAdsWithAwql VB.NET code example.
    /// </summary>
    [Test]
    public void TestGetAllDisapprovedAdsWithAwqlVBExample() {
      RunExample(delegate() {
        new VBExamples.GetAllDisapprovedAdsWithAwql().Run(user, campaignId);
      });
    }

    /// <summary>
    /// Tests the GetAllDisapprovedAdsWithAwql C# code example.
    /// </summary>
    [Test]
    public void TestGetAllDisapprovedAdsWithAwqlCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.GetAllDisapprovedAdsWithAwql().Run(user, campaignId);
      });
    }
  }
}
