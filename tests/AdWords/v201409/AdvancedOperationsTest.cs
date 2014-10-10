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

using Google.Api.Ads.AdWords.v201409;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

using CSharpExamples = Google.Api.Ads.AdWords.Examples.CSharp.v201409;
using VBExamples = Google.Api.Ads.AdWords.Examples.VB.v201409;
using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.Common.Lib;

namespace Google.Api.Ads.AdWords.Tests.v201409 {
  /// <summary>
  /// Test cases for all the code examples under v201409\AdvancedOperations.
  /// </summary>
  class AdvancedOperationsTest : VersionedExampleTestsBase {
    long campaignId;
    long adGroupId1;
    long adGroupId2;
    const double BID_MODIFIER = 0.2;
    string placesAccessToken = "";

    /// <summary>
    /// Inits this instance.
    /// </summary>
    [SetUp]
    public void Init() {
      campaignId = utils.CreateSearchCampaign(user, BiddingStrategyType.MANUAL_CPC);
      adGroupId1 = utils.CreateAdGroup(user, campaignId);
      adGroupId2 = utils.CreateAdGroup(user, campaignId);

      // Load defaults from config file.
      AdWordsAppConfig appConfig = new AdWordsAppConfig();
      appConfig.OAuth2RefreshToken = appConfig.PlacesOAuth2RefreshToken;

      AdsOAuthProviderForApplications oAuth2Provider = new OAuth2ProviderForApplications(appConfig);
      oAuth2Provider.RefreshAccessToken();
      
      placesAccessToken = oAuth2Provider.AccessToken;
    }

    /// <summary>
    /// Tests the AddClickToDownloadAd VB.NET code example.
    /// </summary>
    [Test]
    public void TestAddClickToDownloadAdVBExample() {
      RunExample(delegate() {
        new VBExamples.AddClickToDownloadAd().Run(user, adGroupId1);
      });
    }

    /// <summary>
    /// Tests the AddClickToDownloadAd C# code example.
    /// </summary>
    [Test]
    public void TestAddClickToDownloadAdCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.AddClickToDownloadAd().Run(user, adGroupId1);
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
        new CSharpExamples.AddAdGroupBidModifier().Run(user, adGroupId1, BID_MODIFIER);
      });
    }

    /// <summary>
    /// Tests the AddAdGroupBidModifier VB.NET code example.
    /// </summary>
    [Test]
    public void TestAddAdGroupBidModifierVBExample() {
      RunExample(delegate() {
        new VBExamples.AddAdGroupBidModifier().Run(user, adGroupId1, BID_MODIFIER);
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

    /// <summary>
    /// Tests the AddPlacesLocationExtension C# code example.
    /// </summary>
    [Test]
    public void TestAddPlacesLocationExtensionCSharpExample() {
      AdWordsAppConfig config = (AdWordsAppConfig) user.Config;

      RunExample(delegate() {
        new CSharpExamples.AddPlacesLocationExtension().Run(user, config.PlacesLoginEmail,
            placesAccessToken);
      });
    }

    /// <summary>
    /// Tests the AddPlacesLocationExtension VB.NET code example.
    /// </summary>
    [Test]
    public void TestAddPlacesLocationExtensionVBExample() {
      AdWordsAppConfig config = (AdWordsAppConfig) user.Config;

      RunExample(delegate() {
        new VBExamples.AddPlacesLocationExtension().Run(user, config.PlacesLoginEmail,
            placesAccessToken);
      });
    }

    /// <summary>
    /// Tests the AddAdCustomizers C# code example.
    /// </summary>
    [Test]
    public void TestAddAdCustomizersCSharpExample() {
      AdWordsAppConfig config = (AdWordsAppConfig) user.Config;

      RunExample(delegate() {
        new CSharpExamples.AddAdCustomizers().Run(user, adGroupId1, adGroupId2);
      });
    }

    /// <summary>
    /// Tests the AddAdCustomizers VB.NET code example.
    /// </summary>
    [Test]
    public void TestAddAdCustomizersVBExample() {
      AdWordsAppConfig config = (AdWordsAppConfig) user.Config;

      RunExample(delegate() {
        new VBExamples.AddAdCustomizers().Run(user, adGroupId1, adGroupId2);
      });
    }
  }
}
