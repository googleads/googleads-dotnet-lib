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

using Google.Api.Ads.AdWords.v201502;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

using CSharpExamples = Google.Api.Ads.AdWords.Examples.CSharp.v201502;
using VBExamples = Google.Api.Ads.AdWords.Examples.VB.v201502;

namespace Google.Api.Ads.AdWords.Tests.v201502 {
  /// <summary>
  /// Test cases for all the code examples under v201502\BasicOperations.
  /// </summary>
  class BasicOperationsTest : VersionedExampleTestsBase {
    long campaignId;
    long adGroupId;
    long adId;
    long placementId;

    /// <summary>
    /// Inits this instance.
    /// </summary>
    [SetUp]
    public void Init() {
      campaignId = utils.CreateDisplayCampaign(user, BiddingStrategyType.MANUAL_CPM);
      adGroupId = utils.CreateAdGroup(user, campaignId, true);
      adId = utils.CreateTextAd(user, adGroupId, false);
      placementId = utils.CreatePlacement(user, adGroupId);
    }

    /// <summary>
    /// Tests the AddAdGroup VB.NET code example.
    /// </summary>
    [Test]
    public void TestAddAdGroupVBExample() {
      RunExample(delegate() {
        new VBExamples.AddAdGroups().Run(user, campaignId);
      });
    }

    /// <summary>
    /// Tests the AddAdGroup C# code example.
    /// </summary>
    [Test]
    public void TestAddAdGroupCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.AddAdGroups().Run(user, campaignId);
      });
    }

    /// <summary>
    /// Tests the AddCampaign VB.NET code example.
    /// </summary>
    [Test]
    public void TestAddCampaignVBExample() {
      RunExample(delegate() {
        new VBExamples.AddCampaigns().Run(user);
      });
    }

    /// <summary>
    /// Tests the AddCampaign C# code example.
    /// </summary>
    [Test]
    public void TestAddCampaignCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.AddCampaigns().Run(user);
      });
    }

    /// <summary>
    /// Tests the AddPlacements VB.NET code example.
    /// </summary>
    [Test]
    public void TestAddPlacementsVBExample() {
      RunExample(delegate() {
        new VBExamples.AddPlacements().Run(user, adGroupId);
      });
    }

    /// <summary>
    /// Tests the AddPlacements C# code example.
    /// </summary>
    [Test]
    public void TestAddPlacementsCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.AddPlacements().Run(user, adGroupId);
      });
    }

    /// <summary>
    /// Tests the AddThirdPartyRedirectAd VB.NET code example.
    /// </summary>
    [Test]
    public void TestAddThirdPartyRedirectAdVBExample() {
      RunExample(delegate() {
        new VBExamples.AddThirdPartyRedirectAds().Run(user, adGroupId);
      });
    }

    /// <summary>
    /// Tests the AddThirdPartyRedirectAd C# code example.
    /// </summary>
    [Test]
    public void TestAddThirdPartyRedirectAdCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.AddThirdPartyRedirectAds().Run(user, adGroupId);
      });
    }

    /// <summary>
    /// Tests the RemoveAdGroup VB.NET code example.
    /// </summary>
    [Test]
    public void TestRemoveAdGroupVBExample() {
      RunExample(delegate() {
        new VBExamples.RemoveAdGroup().Run(user, adGroupId);
      });
    }

    /// <summary>
    /// Tests the RemoveAdGroup C# code example.
    /// </summary>
    [Test]
    public void TestRemoveAdGroupCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.RemoveAdGroup().Run(user, adGroupId);
      });
    }

    /// <summary>
    /// Tests the RemoveAd VB.NET code example.
    /// </summary>
    [Test]
    public void TestRemoveAdVBExample() {
      RunExample(delegate() {
        new VBExamples.RemoveAd().Run(user, adGroupId, adId);
      });
    }

    /// <summary>
    /// Tests the RemoveAd C# code example.
    /// </summary>
    [Test]
    public void TestRemoveAdCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.RemoveAd().Run(user, adGroupId, adId);
      });
    }

    /// <summary>
    /// Tests the RemoveCampaign VB.NET code example.
    /// </summary>
    [Test]
    public void TestRemoveCampaignVBExample() {
      RunExample(delegate() {
        new VBExamples.RemoveCampaign().Run(user, campaignId);
      });
    }

    /// <summary>
    /// Tests the RemoveCampaign C# code example.
    /// </summary>
    [Test]
    public void TestRemoveCampaignCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.RemoveCampaign().Run(user, campaignId);
      });
    }

    /// <summary>
    /// Tests the RemovePlacement VB.NET code example.
    /// </summary>
    [Test]
    public void TestRemovePlacementVBExample() {
      RunExample(delegate() {
        new VBExamples.RemovePlacement().Run(user, adGroupId, placementId);
      });
    }

    /// <summary>
    /// Tests the RemovePlacement C# code example.
    /// </summary>
    [Test]
    public void TestRemovePlacementCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.RemovePlacement().Run(user, adGroupId, placementId);
      });
    }

    /// <summary>
    /// Tests the GetAdGroups VB.NET code example.
    /// </summary>
    [Test]
    public void TestGetAdGroupsVBExample() {
      RunExample(delegate() {
        new VBExamples.GetAdGroups().Run(user, campaignId);
      });
    }

    /// <summary>
    /// Tests the GetAdGroups C# code example.
    /// </summary>
    [Test]
    public void TestGetAdGroupsCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.GetAdGroups().Run(user, campaignId);
      });
    }

    /// <summary>
    /// Tests the GetCampaigns VB.NET code example.
    /// </summary>
    [Test]
    public void TestGetCampaignsVBExample() {
      RunExample(delegate() {
        new VBExamples.GetCampaigns().Run(user);
      });
    }

    /// <summary>
    /// Tests the GetCampaigns C# code example.
    /// </summary>
    [Test]
    public void TestGetCampaignsCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.GetCampaigns().Run(user);
      });
    }

    /// <summary>
    /// Tests the GetCampaignsWithAwql VB.NET code example.
    /// </summary>
    [Test]
    public void TestGetCampaignsWithAwqlVBExample() {
      RunExample(delegate() {
        new VBExamples.GetCampaignsWithAwql().Run(user);
      });
    }

    /// <summary>
    /// Tests the GetCampaignsWithAwql C# code example.
    /// </summary>
    [Test]
    public void TestGetCampaignsWithAwqlCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.GetCampaignsWithAwql().Run(user);
      });
    }

    /// <summary>
    /// Tests the GetPlacements VB.NET code example.
    /// </summary>
    [Test]
    public void TestGetPlacementsVBExample() {
      RunExample(delegate() {
        new VBExamples.GetPlacements().Run(user);
      });
    }

    /// <summary>
    /// Tests the GetPlacements C# code example.
    /// </summary>
    [Test]
    public void TestGetPlacementsCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.GetPlacements().Run(user);
      });
    }

    /// <summary>
    /// Tests the GetThirdPartyRedirectAds VB.NET code example.
    /// </summary>
    [Test]
    public void TestGetThirdPartyRedirectAdsVBExample() {
      RunExample(delegate() {
        new VBExamples.GetThirdPartyRedirectAds().Run(user, adGroupId);
      });
    }

    /// <summary>
    /// Tests the GetThirdPartyRedirectAds C# code example.
    /// </summary>
    [Test]
    public void TestGetThirdPartyRedirectAdsCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.GetThirdPartyRedirectAds().Run(user, adGroupId);
      });
    }

    /// <summary>
    /// Tests the PauseAd VB.NET code example.
    /// </summary>
    [Test]
    public void TestPauseAdVBExample() {
      RunExample(delegate() {
        new VBExamples.PauseAd().Run(user, adGroupId, adId);
      });
    }

    /// <summary>
    /// Tests the PauseAd C# code example.
    /// </summary>
    [Test]
    public void TestPauseAdCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.PauseAd().Run(user, adGroupId, adId);
      });
    }

    /// <summary>
    /// Tests the UpdateAdGroup VB.NET code example.
    /// </summary>
    [Test]
    public void TestUpdateAdGroupVBExample() {
      RunExample(delegate() {
        new VBExamples.UpdateAdGroup().Run(user, adGroupId);
      });
    }

    /// <summary>
    /// Tests the UpdateAdGroup C# code example.
    /// </summary>
    [Test]
    public void TestUpdateAdGroupCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.UpdateAdGroup().Run(user, adGroupId);
      });
    }

    /// <summary>
    /// Tests the UpdateCampaign VB.NET code example.
    /// </summary>
    [Test]
    public void TestUpdateCampaignVBExample() {
      RunExample(delegate() {
        new VBExamples.UpdateCampaign().Run(user, campaignId);
      });
    }

    /// <summary>
    /// Tests the UpdateCampaign C# code example.
    /// </summary>
    [Test]
    public void TestUpdateCampaignCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.UpdateCampaign().Run(user, campaignId);
      });
    }

    /// <summary>
    /// Tests the UpdatePlacement VB.NET code example.
    /// </summary>
    [Test]
    public void TestUpdatePlacementVBExample() {
      RunExample(delegate() {
        new VBExamples.UpdatePlacement().Run(user, adGroupId, placementId);
      });
    }

    /// <summary>
    /// Tests the UpdatePlacement C# code example.
    /// </summary>
    [Test]
    public void TestUpdatePlacementCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.UpdatePlacement().Run(user, adGroupId, placementId);
      });
    }
  }
}
