// Copyright 2014, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.AdWords.v201402;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

using CSharpExamples = Google.Api.Ads.AdWords.Examples.CSharp.v201402;
using VBExamples = Google.Api.Ads.AdWords.Examples.VB.v201402;

namespace Google.Api.Ads.AdWords.Tests.v201402 {
  /// <summary>
  /// Test cases for all the code examples under v201402\BasicOperations.
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
    /// Tests the DeleteAdGroup VB.NET code example.
    /// </summary>
    [Test]
    public void TestDeleteAdGroupVBExample() {
      RunExample(delegate() {
        new VBExamples.DeleteAdGroup().Run(user, adGroupId);
      });
    }

    /// <summary>
    /// Tests the DeleteAdGroup C# code example.
    /// </summary>
    [Test]
    public void TestDeleteAdGroupCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.DeleteAdGroup().Run(user, adGroupId);
      });
    }

    /// <summary>
    /// Tests the DeleteAd VB.NET code example.
    /// </summary>
    [Test]
    public void TestDeleteAdVBExample() {
      RunExample(delegate() {
        new VBExamples.DeleteAd().Run(user, adGroupId, adId);
      });
    }

    /// <summary>
    /// Tests the DeleteAd C# code example.
    /// </summary>
    [Test]
    public void TestDeleteAdCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.DeleteAd().Run(user, adGroupId, adId);
      });
    }

    /// <summary>
    /// Tests the DeleteCampaign VB.NET code example.
    /// </summary>
    [Test]
    public void TestDeleteCampaignVBExample() {
      RunExample(delegate() {
        new VBExamples.DeleteCampaign().Run(user, campaignId);
      });
    }

    /// <summary>
    /// Tests the DeleteCampaign C# code example.
    /// </summary>
    [Test]
    public void TestDeleteCampaignCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.DeleteCampaign().Run(user, campaignId);
      });
    }

    /// <summary>
    /// Tests the DeletePlacement VB.NET code example.
    /// </summary>
    [Test]
    public void TestDeletePlacementVBExample() {
      RunExample(delegate() {
        new VBExamples.DeletePlacement().Run(user, adGroupId, placementId);
      });
    }

    /// <summary>
    /// Tests the DeletePlacement C# code example.
    /// </summary>
    [Test]
    public void TestDeletePlacementCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.DeletePlacement().Run(user, adGroupId, placementId);
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
