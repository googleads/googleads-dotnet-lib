// Copyright 2016, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201708;
using Google.Api.Ads.Common.Lib;

using NUnit.Framework;

using CSharpExamples = Google.Api.Ads.AdWords.Examples.CSharp.v201708;
using VBExamples = Google.Api.Ads.AdWords.Examples.VB.v201708;

namespace Google.Api.Ads.AdWords.Tests.v201708 {

  /// <summary>
  /// Test cases for all the code examples under v201708\AdvancedOperations.
  /// </summary>
  internal class AdvancedOperationsTest : VersionedExampleTestsBase {
    private long campaignId;
    private long adGroupId1;
    private long adGroupId2;
    private const double BID_MODIFIER = 0.2;

    private long mobileCampaignId;
    private long mobileAdGroupId;

    private long sharedSetId;

    /// <summary>
    /// Campaign for testing ResponsiveDisplayAd examples.
    /// </summary>
    private long displayCampaignId;

    /// <summary>
    /// Ad group for testing ResponsiveDisplayAd examples.
    /// </summary>
    private long displayAdGroupId;

    /// <summary>
    /// Campaign for DSA examples.
    /// </summary>
    private long dsaCampaignId;

    /// <summary>
    /// Ad group for DSA examples.
    /// </summary>
    private long dsaAdGroupId;

    /// <summary>
    /// Inits this instance.
    /// </summary>
    [SetUp]
    public void Init() {
      campaignId = utils.CreateSearchCampaign(user, BiddingStrategyType.MANUAL_CPC);
      adGroupId1 = utils.CreateAdGroup(user, campaignId);
      adGroupId2 = utils.CreateAdGroup(user, campaignId);
      displayCampaignId = utils.CreateDisplayCampaign(user, BiddingStrategyType.MANUAL_CPM);
      displayAdGroupId = utils.CreateAdGroup(user, displayCampaignId);

      mobileCampaignId = utils.CreateMobileSearchCampaign(user, BiddingStrategyType.MANUAL_CPC);
      mobileAdGroupId = utils.CreateAdGroup(user, mobileCampaignId);

      dsaCampaignId = utils.CreateDSACampaign(user);
      dsaAdGroupId = utils.CreateAdGroup(user, dsaCampaignId, AdGroupType.SEARCH_DYNAMIC_ADS,
          false);

      sharedSetId = utils.CreateSharedKeywordSet(user);
      utils.AttachSharedSetToCampaign(user, campaignId, sharedSetId);

      // Load defaults from config file.
      AdWordsAppConfig appConfig = new AdWordsAppConfig();
      appConfig.OAuth2RefreshToken = appConfig.GMBOAuth2RefreshToken;

      AdsOAuthProviderForApplications oAuth2Provider = new OAuth2ProviderForApplications(appConfig);
      oAuth2Provider.RefreshAccessToken();
    }

    /// <summary>
    /// Tears down the test case.
    /// </summary>
    [TearDown]
    public void TearDown() {
      utils.DetachSharedSetFromCampaign(user, campaignId, sharedSetId);
      utils.DeleteSharedSet(user, sharedSetId);
    }

    /// <summary>
    /// Tests the AddUniversalAppCampaign VB.NET code example.
    /// </summary>
    [Test]
    public void TestAddUniversalAppCampaignVBExample() {
      RunExample(delegate() {
        new VBExamples.AddUniversalAppCampaign().Run(user);
      });
    }

    /// <summary>
    /// Tests the AddUniversalAppCampaign C# code example.
    /// </summary>
    [Test]
    public void TestAddUniversalAppCampaignCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.AddUniversalAppCampaign().Run(user);
      });
    }

    /// <summary>
    /// Tests the AddClickToDownloadAd VB.NET code example.
    /// </summary>
    [Test]
    public void TestAddClickToDownloadAdVBExample() {
      RunExample(delegate() {
        new VBExamples.AddClickToDownloadAd().Run(user, mobileAdGroupId);
      });
    }

    /// <summary>
    /// Tests the AddClickToDownloadAd C# code example.
    /// </summary>
    [Test]
    public void TestAddClickToDownloadAdCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.AddClickToDownloadAd().Run(user, mobileAdGroupId);
      });
    }

    /// <summary>
    /// Tests the AddResponsiveDisplayAd VB.NET code example.
    /// </summary>
    [Test]
    public void TestAddResponsiveDisplayAdVBExample() {
      RunExample(delegate() {
        new VBExamples.AddResponsiveDisplayAd().Run(user, displayAdGroupId);
      });
    }

    /// <summary>
    /// Tests the AddResponsiveDisplayAd C# code example.
    /// </summary>
    [Test]
    public void TestAddResponsiveDisplayAdCSharpExample() {
      RunExample(delegate() {
        new CSharpExamples.AddResponsiveDisplayAd().Run(user, displayAdGroupId);
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
    /// Tests the AddAdCustomizers C# code example.
    /// </summary>
    [Test]
    public void TestAddAdCustomizersCSharpExample() {
      AdWordsAppConfig config = (AdWordsAppConfig) user.Config;
      string feedName = "AdCustomizerFeed" + utils.GetTimeStampAlpha();
      RunExample(delegate() {
        new CSharpExamples.AddAdCustomizers().Run(user, adGroupId1, adGroupId2, feedName);
      });
    }

    /// <summary>
    /// Tests the AddAdCustomizers VB.NET code example.
    /// </summary>
    [Test]
    public void TestAddAdCustomizersVBExample() {
      AdWordsAppConfig config = (AdWordsAppConfig) user.Config;
      string feedName = "AdCustomizerFeed" + utils.GetTimeStampAlpha();
      RunExample(delegate() {
        new VBExamples.AddAdCustomizers().Run(user, adGroupId1, adGroupId2, feedName);
      });
    }

    /// <summary>
    /// Tests the CreateAndAttachSharedKeywordSet C# code example.
    /// </summary>
    [Test]
    public void TestCreateAndAttachSharedKeywordSetCSharpExample() {
      AdWordsAppConfig config = (AdWordsAppConfig) user.Config;
      RunExample(delegate() {
        new CSharpExamples.CreateAndAttachSharedKeywordSet().Run(user, campaignId);
      });
    }

    /// <summary>
    /// Tests the AddAdCustomizers VB.NET code example.
    /// </summary>
    [Test]
    public void TestCreateAndAttachSharedKeywordSetVBExample() {
      AdWordsAppConfig config = (AdWordsAppConfig) user.Config;
      RunExample(delegate() {
        new VBExamples.CreateAndAttachSharedKeywordSet().Run(user, campaignId);
      });
    }

    /// <summary>
    /// Tests the FindAndRemoveCriteriaFromSharedSet C# code example.
    /// </summary>
    [Test]
    public void TestFindAndRemoveCriteriaFromSharedSetCSharpExample() {
      AdWordsAppConfig config = (AdWordsAppConfig) user.Config;
      RunExample(delegate() {
        new CSharpExamples.FindAndRemoveCriteriaFromSharedSet().Run(user, campaignId);
      });
    }

    /// <summary>
    /// Tests the FindAndRemoveCriteriaFromSharedSet VB.NET code example.
    /// </summary>
    [Test]
    public void TestFindAndRemoveCriteriaFromSharedSetVBExample() {
      AdWordsAppConfig config = (AdWordsAppConfig) user.Config;
      RunExample(delegate() {
        new VBExamples.FindAndRemoveCriteriaFromSharedSet().Run(user, campaignId);
      });
    }

    /// <summary>
    /// Tests the AddHtml5Ad C# code example.
    /// </summary>
    [Test]
    public void TestAddHtml5AdCSharpExample() {
      AdWordsAppConfig config = (AdWordsAppConfig) user.Config;
      RunExample(delegate() {
        new CSharpExamples.AddHtml5Ad().Run(user, adGroupId1);
      });
    }

    /// <summary>
    /// Tests the AddHtml5Ad VB.NET code example.
    /// </summary>
    [Test]
    public void TestAddHtml5AdVBExample() {
      AdWordsAppConfig config = (AdWordsAppConfig) user.Config;
      RunExample(delegate() {
        new VBExamples.AddHtml5Ad().Run(user, adGroupId1);
      });
    }

    /// <summary>
    /// Tests the AddDynamicSearchAdsCampaign C# code example.
    /// </summary>
    [Test]
    public void TestAddDynamicSearchAdsCampaignCSharpExample() {
      AdWordsAppConfig config = (AdWordsAppConfig) user.Config;
      RunExample(delegate () {
        new CSharpExamples.AddDynamicSearchAdsCampaign().Run(user);
      });
    }

    /// <summary>
    /// Tests the AddDynamicSearchAdsCampaign VB.NET code example.
    /// </summary>
    [Test]
    public void TestAddDynamicSearchAdsCampaignVBExample() {
      AdWordsAppConfig config = (AdWordsAppConfig) user.Config;
      RunExample(delegate () {
        new VBExamples.AddDynamicSearchAdsCampaign().Run(user);
      });
    }

    /// <summary>
    /// Tests the AddDynamicPageFeed C# code example.
    /// </summary>
    [Test]
    public void TestAddDynamicPageFeedCSharpExample() {
      AdWordsAppConfig config = (AdWordsAppConfig) user.Config;
      RunExample(delegate () {
        new CSharpExamples.AddDynamicPageFeed().Run(user, dsaCampaignId, dsaAdGroupId);
      });
    }

    /// <summary>
    /// Tests the AddDynamicPageFeed VB.NET code example.
    /// </summary>
    [Test]
    public void TestAddDynamicPageFeedVBExample() {
      AdWordsAppConfig config = (AdWordsAppConfig) user.Config;
      RunExample(delegate () {
        new VBExamples.AddDynamicPageFeed().Run(user, dsaCampaignId, dsaAdGroupId);
      });
    }

    /// <summary>
    /// Tests the UsePortfolioBiddingStrategy C# code example.
    /// </summary>
    [Test]
    public void TestUsePortfolioBiddingStrategyCSharpExample() {
      AdWordsAppConfig config = (AdWordsAppConfig) user.Config;
      RunExample(delegate() {
        new CSharpExamples.UsePortfolioBiddingStrategy().Run(user);
      });
    }

    /// <summary>
    /// Tests the UsePortfolioBiddingStrategy VB.NET code example.
    /// </summary>
    [Test]
    public void TestUsePortfolioBiddingStrategyVBExample() {
      AdWordsAppConfig config = (AdWordsAppConfig) user.Config;
      RunExample(delegate() {
        new VBExamples.UsePortfolioBiddingStrategy().Run(user);
      });
    }

    /// <summary>
    /// Tests the AddExpandedTextAdWithUpgradedUrls C# code example.
    /// </summary>
    [Test]
    public void TestAddExpandedTextAdWithUpgradedUrlsCSharpExample() {
      AdWordsAppConfig config = (AdWordsAppConfig) user.Config;
      RunExample(delegate() {
        new CSharpExamples.AddExpandedTextAdWithUpgradedUrls().Run(user, adGroupId1);
      });
    }

    /// <summary>
    /// Tests the AddExpandedTextAdWithUpgradedUrls VB.NET code example.
    /// </summary>
    [Test]
    public void TestAddExpandedTextAdWithUpgradedUrlsVBExample() {
      AdWordsAppConfig config = (AdWordsAppConfig) user.Config;
      RunExample(delegate() {
        new VBExamples.AddExpandedTextAdWithUpgradedUrls().Run(user, adGroupId1);
      });
    }
  }
}