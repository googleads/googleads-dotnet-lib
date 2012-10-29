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

using Google.Api.Ads.AdWords.v201209;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

using CSharpExamples = Google.Api.Ads.AdWords.Examples.CSharp.v201209;
using VBExamples = Google.Api.Ads.AdWords.Examples.VB.v201209;

namespace Google.Api.Ads.AdWords.Tests.v201209 {
  /// <summary>
  /// Test cases for all the code examples under v201209\BasicOperations.
  /// </summary>
  class BasicOperationsTest : ExampleBaseTests {
    long campaignId;
    long adGroupId;
    long adId;
    long keywordId;

    /// <summary>
    /// Inits this instance.
    /// </summary>
    [SetUp]
    public void Init() {
      campaignId = utils.CreateCampaign(user, new ManualCPC());
      adGroupId = utils.CreateAdGroup(user, campaignId);
      adId = utils.CreateTextAd(user, adGroupId, false);
      keywordId = utils.CreateKeyword(user, adGroupId);
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
    /// Tests the AddKeywords VB.NET code example.
    /// </summary>
    [Test]
    public void TestAddKeywordsVBExample() {
      RunExample(delegate() {
        new VBExamples.AddKeywords().Run(user, adGroupId);
      });
    }

    /// <summary>
    /// Tests the AddKeywords C# code example.
    /// </summary>
    [Test]
    public void TestAddKeywordsCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.AddKeywords().Run(user, adGroupId);
      });
    }

    /// <summary>
    /// Tests the AddTextAds VB.NET code example.
    /// </summary>
    [Test]
    public void TestAddTextAdsVBExample() {
      RunExample(delegate() {
        new VBExamples.AddTextAds().Run(user, adGroupId);
      });
    }

    /// <summary>
    /// Tests the AddTextAds C# code example.
    /// </summary>
    [Test]
    public void TestAddTextAdsCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.AddTextAds().Run(user, adGroupId);
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
        new VBExamples.DeleteAdGroup().Run(user, adGroupId);
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
    /// Tests the DeleteKeyword VB.NET code example.
    /// </summary>
    [Test]
    public void TestDeleteKeywordVBExample() {
      RunExample(delegate() {
        new VBExamples.DeleteKeyword().Run(user, adGroupId, keywordId);
      });
    }

    /// <summary>
    /// Tests the DeleteKeyword C# code example.
    /// </summary>
    [Test]
    public void TestDeleteKeywordCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.DeleteKeyword().Run(user, adGroupId, keywordId);
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
    /// Tests the GetKeywords VB.NET code example.
    /// </summary>
    [Test]
    public void TestGetKeywordsVBExample() {
      RunExample(delegate() {
        new VBExamples.GetKeywords().Run(user);
      });
    }

    /// <summary>
    /// Tests the GetKeywords C# code example.
    /// </summary>
    [Test]
    public void TestGetKeywordsCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.GetKeywords().Run(user);
      });
    }

    /// <summary>
    /// Tests the GetTextAds VB.NET code example.
    /// </summary>
    [Test]
    public void TestGetTextAdsVBExample() {
      RunExample(delegate() {
        new VBExamples.GetTextAds().Run(user, adGroupId);
      });
    }

    /// <summary>
    /// Tests the GetTextAds C# code example.
    /// </summary>
    [Test]
    public void TestGetTextAdsCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.GetTextAds().Run(user, adGroupId);
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
    /// Tests the UpdateKeyword VB.NET code example.
    /// </summary>
    [Test]
    public void TestUpdateKeywordVBExample() {
      RunExample(delegate() {
        new VBExamples.UpdateKeyword().Run(user, adGroupId, keywordId);
      });
    }

    /// <summary>
    /// Tests the UpdateKeyword C# code example.
    /// </summary>
    [Test]
    public void TestUpdateKeywordCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.UpdateKeyword().Run(user, adGroupId, keywordId);
      });
    }
  }
}
