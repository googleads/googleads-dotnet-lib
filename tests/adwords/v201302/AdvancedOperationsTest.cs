// Copyright 2013, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.AdWords.v201302;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

using CSharpExamples = Google.Api.Ads.AdWords.Examples.CSharp.v201302;
using VBExamples = Google.Api.Ads.AdWords.Examples.VB.v201302;

namespace Google.Api.Ads.AdWords.Tests.v201302 {
  /// <summary>
  /// Test cases for all the code examples under v201302\AdvancedOperations.
  /// </summary>
  class AdvancedOperationsTest : VersionedExampleTestsBase {
    long campaignId;
    long adGroupId;
    const double BID_MODIFIER = 0.2;

    /// <summary>
    /// Inits this instance.
    /// </summary>
    [SetUp]
    public void Init() {
      campaignId = utils.CreateCampaign(user, BiddingStrategyType.MANUAL_CPC);
      adGroupId = utils.CreateAdGroup(user, campaignId);
    }

    /// <summary>
    /// Tests the AddClickToDownloadAd VB.NET code example.
    /// </summary>
    [Test]
    public void TestAddClickToDownloadAdVBExample() {
      RunExample(delegate() {
        new VBExamples.AddClickToDownloadAd().Run(user, adGroupId);
      });
    }

    /// <summary>
    /// Tests the AddClickToDownloadAd C# code example.
    /// </summary>
    [Test]
    public void TestAddClickToDownloadAdCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.AddClickToDownloadAd().Run(user, adGroupId);
      });
    }

    /// <summary>
    /// Tests the AddSiteLinks VB.NET code example.
    /// </summary>
    [Test]
    public void TestAddSiteLinksVBExample() {
      RunExample(delegate() {
        new VBExamples.AddSiteLinks().Run(user, campaignId);
      });
    }

    /// <summary>
    /// Tests the AddSiteLinks C# code example.
    /// </summary>
    [Test]
    public void TestAddSiteLinksCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.AddSiteLinks().Run(user, campaignId);
      });
    }

    /// <summary>
    /// Tests the AddAdGroupBidModifier C# code example.
    /// </summary>
    [Test]
    public void TestAddAdGroupBidModifierCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.AddAdGroupBidModifier().Run(user, adGroupId, BID_MODIFIER);
      });
    }

    /// <summary>
    /// Tests the AddAdGroupBidModifier VB.NET code example.
    /// </summary>
    [Test]
    public void TestAddAdGroupBidModifierVBExample() {
      RunExample(delegate() {
        new VBExamples.AddAdGroupBidModifier().Run(user, adGroupId, BID_MODIFIER);
      });
    }

    /// <summary>
    /// Tests the GetAdGroupBidModifiers C# code example.
    /// </summary>
    [Test]
    public void TestGetAdGroupBidModifiersCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.GetAdGroupBidModifiers().Run(user, campaignId);
      });
    }

    /// <summary>
    /// Tests the GetAdGroupBidModifiers VB.NET code example.
    /// </summary>
    [Test]
    public void TestGetAdGroupBidModifiersVBExample() {
      RunExample(delegate() {
        new VBExamples.GetAdGroupBidModifiers().Run(user, campaignId);
      });
    }
  }
}
