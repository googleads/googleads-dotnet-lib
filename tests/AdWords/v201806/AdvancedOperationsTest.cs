// Copyright 2018 Google LLC
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
using Google.Api.Ads.AdWords.v201806;
using Google.Api.Ads.Common.Lib;

using NUnit.Framework;

using CSharpExamples = Google.Api.Ads.AdWords.Examples.CSharp.v201806;
using VBExamples = Google.Api.Ads.AdWords.Examples.VB.v201806;

namespace Google.Api.Ads.AdWords.Tests.v201806
{
    /// <summary>
    /// Test cases for all the code examples under v201806\AdvancedOperations.
    /// </summary>
    internal class AdvancedOperationsTest : VersionedExampleTestsBase
    {
        private long campaignId;
        private long adGroupId1;
        private long adGroupId2;
        private const double BID_MODIFIER = 0.2;

        private long mobileCampaignId;
        private long mobileAdGroupId;

        private long sharedSetId;

        /// <summary>
        /// Campaign for testing GmailAd examples.
        /// </summary>
        private long gmailCampaignId;

        /// <summary>
        /// Ad group for testing GmailAd examples.
        /// </summary>
        private long gmailAdGroupId;

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
        /// The budget ID for AddShoppingDynamicRemarketingCampaign example.
        /// </summary>
        private long budgetId;

        /// <summary>
        /// The user list ID for AddShoppingDynamicRemarketingCampaign example.
        /// </summary>
        private long userListId;

        /// <summary>
        /// Inits this instance.
        /// </summary>
        [SetUp]
        public void Init()
        {
            campaignId = utils.CreateSearchCampaign(user, BiddingStrategyType.MANUAL_CPC);
            adGroupId1 = utils.CreateAdGroup(user, campaignId);
            adGroupId2 = utils.CreateAdGroup(user, campaignId);
            displayCampaignId = utils.CreateDisplayCampaign(user, BiddingStrategyType.MANUAL_CPM);
            displayAdGroupId = utils.CreateAdGroup(user, displayCampaignId);

            mobileCampaignId =
                utils.CreateMobileSearchCampaign(user, BiddingStrategyType.MANUAL_CPC);
            mobileAdGroupId = utils.CreateAdGroup(user, mobileCampaignId);

            dsaCampaignId = utils.CreateDSACampaign(user);
            dsaAdGroupId = utils.CreateAdGroup(user, dsaCampaignId, AdGroupType.SEARCH_DYNAMIC_ADS,
                false);

            gmailCampaignId = utils.CreateGmailCampaign(user);
            gmailAdGroupId =
                utils.CreateAdGroup(user, gmailCampaignId, AdGroupType.DISPLAY_STANDARD, true);

            sharedSetId = utils.CreateSharedKeywordSet(user);
            utils.AttachSharedSetToCampaign(user, campaignId, sharedSetId);

            budgetId = utils.CreateBudget(user);
            userListId = utils.CreateUserList(user);

            // Load defaults from config file.
            AdWordsAppConfig appConfig = new AdWordsAppConfig();
            appConfig.OAuth2RefreshToken = appConfig.GMBOAuth2RefreshToken;

            AdsOAuthProviderForApplications oAuth2Provider =
                new OAuth2ProviderForApplications(appConfig);
            oAuth2Provider.RefreshAccessToken();
        }

        /// <summary>
        /// Tears down the test case.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            utils.DetachSharedSetFromCampaign(user, campaignId, sharedSetId);
            utils.DeleteSharedSet(user, sharedSetId);
        }

        /// <summary>
        /// Tests the AddUniversalAppCampaign VB.NET code example.
        /// </summary>
        [Test]
        public void TestAddUniversalAppCampaignVBExample()
        {
            RunExample(delegate() { new VBExamples.AddUniversalAppCampaign().Run(user); });
        }

        /// <summary>
        /// Tests the AddUniversalAppCampaign C# code example.
        /// </summary>
        [Test]
        public void TestAddUniversalAppCampaignCSharpExample()
        {
            RunExample(delegate() { new CSharpExamples.AddUniversalAppCampaign().Run(user); });
        }

        /// <summary>
        /// Tests the AddResponsiveDisplayAd VB.NET code example.
        /// </summary>
        [Test]
        public void TestAddResponsiveDisplayAdVBExample()
        {
            RunExample(delegate()
            {
                new VBExamples.AddResponsiveDisplayAd().Run(user, displayAdGroupId);
            });
        }

        /// <summary>
        /// Tests the AddResponsiveDisplayAd C# code example.
        /// </summary>
        [Test]
        public void TestAddResponsiveDisplayAdCSharpExample()
        {
            RunExample(delegate()
            {
                new CSharpExamples.AddResponsiveDisplayAd().Run(user, displayAdGroupId);
            });
        }

        /// <summary>
        /// Tests the AddAdGroupBidModifier C# code example.
        /// </summary>
        [Test]
        public void TestAddAdGroupBidModifierCSharpExample()
        {
            RunExample(delegate()
            {
                new CSharpExamples.AddAdGroupBidModifier().Run(user, adGroupId1, BID_MODIFIER);
            });
        }

        /// <summary>
        /// Tests the AddAdGroupBidModifier VB.NET code example.
        /// </summary>
        [Test]
        public void TestAddAdGroupBidModifierVBExample()
        {
            RunExample(delegate()
            {
                new VBExamples.AddAdGroupBidModifier().Run(user, adGroupId1, BID_MODIFIER);
            });
        }

        /// <summary>
        /// Tests the GetAdGroupBidModifiers C# code example.
        /// </summary>
        [Test]
        public void TestGetAdGroupBidModifiersCSharpExample()
        {
            RunExample(delegate()
            {
                new CSharpExamples.GetAdGroupBidModifiers().Run(user, campaignId);
            });
        }

        /// <summary>
        /// Tests the GetAdGroupBidModifiers VB.NET code example.
        /// </summary>
        [Test]
        public void TestGetAdGroupBidModifiersVBExample()
        {
            RunExample(
                delegate() { new VBExamples.GetAdGroupBidModifiers().Run(user, campaignId); });
        }

        /// <summary>
        /// Tests the AddAdCustomizers C# code example.
        /// </summary>
        [Test]
        public void TestAddAdCustomizersCSharpExample()
        {
            string feedName = "AdCustomizerFeed" + utils.GetTimeStampAlpha();
            RunExample(delegate()
            {
                new CSharpExamples.AddAdCustomizers().Run(user, adGroupId1, adGroupId2,
                    feedName);
            });
        }

        /// <summary>
        /// Tests the AddAdCustomizers VB.NET code example.
        /// </summary>
        [Test]
        public void TestAddAdCustomizersVBExample()
        {
            string feedName = "AdCustomizerFeed" + utils.GetTimeStampAlpha();
            RunExample(delegate()
            {
                new VBExamples.AddAdCustomizers().Run(user, adGroupId1, adGroupId2, feedName);
            });
        }

        /// <summary>
        /// Tests the CreateAndAttachSharedKeywordSet C# code example.
        /// </summary>
        [Test]
        public void TestCreateAndAttachSharedKeywordSetCSharpExample()
        {
            RunExample(delegate()
            {
                new CSharpExamples.CreateAndAttachSharedKeywordSet().Run(user, campaignId);
            });
        }

        /// <summary>
        /// Tests the AddAdCustomizers VB.NET code example.
        /// </summary>
        [Test]
        public void TestCreateAndAttachSharedKeywordSetVBExample()
        {
            RunExample(delegate()
            {
                new VBExamples.CreateAndAttachSharedKeywordSet().Run(user, campaignId);
            });
        }

        /// <summary>
        /// Tests the FindAndRemoveCriteriaFromSharedSet C# code example.
        /// </summary>
        [Test]
        public void TestFindAndRemoveCriteriaFromSharedSetCSharpExample()
        {
            RunExample(delegate()
            {
                new CSharpExamples.FindAndRemoveCriteriaFromSharedSet().Run(user, campaignId);
            });
        }

        /// <summary>
        /// Tests the FindAndRemoveCriteriaFromSharedSet VB.NET code example.
        /// </summary>
        [Test]
        public void TestFindAndRemoveCriteriaFromSharedSetVBExample()
        {
            RunExample(delegate()
            {
                new VBExamples.FindAndRemoveCriteriaFromSharedSet().Run(user, campaignId);
            });
        }

        /// <summary>
        /// Tests the AddHtml5Ad C# code example.
        /// </summary>
        [Test]
        public void TestAddHtml5AdCSharpExample()
        {
            RunExample(delegate() { new CSharpExamples.AddHtml5Ad().Run(user, adGroupId1); });
        }

        /// <summary>
        /// Tests the AddHtml5Ad VB.NET code example.
        /// </summary>
        [Test]
        public void TestAddHtml5AdVBExample()
        {
            RunExample(delegate() { new VBExamples.AddHtml5Ad().Run(user, adGroupId1); });
        }

        /// <summary>
        /// Tests the AddDynamicSearchAdsCampaign C# code example.
        /// </summary>
        [Test]
        public void TestAddDynamicSearchAdsCampaignCSharpExample()
        {
            RunExample(delegate() { new CSharpExamples.AddDynamicSearchAdsCampaign().Run(user); });
        }

        /// <summary>
        /// Tests the AddDynamicSearchAdsCampaign VB.NET code example.
        /// </summary>
        [Test]
        public void TestAddDynamicSearchAdsCampaignVBExample()
        {
            RunExample(delegate() { new VBExamples.AddDynamicSearchAdsCampaign().Run(user); });
        }

        /// <summary>
        /// Tests the AddDynamicPageFeed C# code example.
        /// </summary>
        [Test]
        public void TestAddDynamicPageFeedCSharpExample()
        {
            RunExample(delegate()
            {
                new CSharpExamples.AddDynamicPageFeed().Run(user, dsaCampaignId, dsaAdGroupId);
            });
        }

        /// <summary>
        /// Tests the AddDynamicPageFeed VB.NET code example.
        /// </summary>
        [Test]
        public void TestAddDynamicPageFeedVBExample()
        {
            RunExample(delegate()
            {
                new VBExamples.AddDynamicPageFeed().Run(user, dsaCampaignId, dsaAdGroupId);
            });
        }

        /// <summary>
        /// Tests the UsePortfolioBiddingStrategy C# code example.
        /// </summary>
        [Test]
        public void TestUsePortfolioBiddingStrategyCSharpExample()
        {
            RunExample(delegate() { new CSharpExamples.UsePortfolioBiddingStrategy().Run(user); });
        }

        /// <summary>
        /// Tests the UsePortfolioBiddingStrategy VB.NET code example.
        /// </summary>
        [Test]
        public void TestUsePortfolioBiddingStrategyVBExample()
        {
            RunExample(delegate() { new VBExamples.UsePortfolioBiddingStrategy().Run(user); });
        }

        /// <summary>
        /// Tests the AddExpandedTextAdWithUpgradedUrls C# code example.
        /// </summary>
        [Test]
        public void TestAddExpandedTextAdWithUpgradedUrlsCSharpExample()
        {
            RunExample(delegate()
            {
                new CSharpExamples.AddExpandedTextAdWithUpgradedUrls().Run(user, adGroupId1);
            });
        }

        /// <summary>
        /// Tests the AddExpandedTextAdWithUpgradedUrls VB.NET code example.
        /// </summary>
        [Test]
        public void TestAddExpandedTextAdWithUpgradedUrlsVBExample()
        {
            RunExample(delegate()
            {
                new VBExamples.AddExpandedTextAdWithUpgradedUrls().Run(user, adGroupId1);
            });
        }

        /// <summary>
        /// Tests the AddGmailAd C# code example.
        /// </summary>
        [Test]
        public void TestAddGmailAdCSharpExample()
        {
            RunExample(delegate() { new CSharpExamples.AddGmailAd().Run(user, gmailAdGroupId); });
        }

        /// <summary>
        /// Tests the AddGmailAd VB.NET code example.
        /// </summary>
        [Test]
        public void TestAddGmailAdVBExample()
        {
            RunExample(delegate() { new VBExamples.AddGmailAd().Run(user, gmailAdGroupId); });
        }

        /// <summary>
        /// Tests the AddShoppingDynamicRemarketingCampaign C# code example.
        /// </summary>
        [Test]
        public void TestAddShoppingDynamicRemarketingCampaignCSharpExample()
        {
            AdWordsAppConfig config = (AdWordsAppConfig) user.Config;
            RunExample(delegate()
            {
                new CSharpExamples.AddShoppingDynamicRemarketingCampaign().Run(user,
                    config.MerchantCenterId, budgetId, userListId);
            });
        }

        /// <summary>
        /// Tests the AddShoppingDynamicRemarketingCampaign VB.NET code example.
        /// </summary>
        [Test]
        public void TestAddShoppingDynamicRemarketingCampaignVBExample()
        {
            AdWordsAppConfig config = (AdWordsAppConfig) user.Config;
            RunExample(delegate()
            {
                new VBExamples.AddShoppingDynamicRemarketingCampaign().Run(user,
                    config.MerchantCenterId, budgetId, userListId);
            });
        }

        /// <summary>
        /// Tests the AddMultiAssetResponsiveDisplayAd C# code example.
        /// </summary>
        [Test]
        public void TestAddMultiAssetResponsiveDisplayAdCSharpExample()
        {
            AdWordsAppConfig config = (AdWordsAppConfig) user.Config;
            RunExample(delegate()
            {
                new CSharpExamples.AddMultiAssetResponsiveDisplayAd().Run(user,
                    displayAdGroupId);
            });
        }

        /// <summary>
        /// Tests the AddMultiAssetResponsiveDisplayAd VB.NET code example.
        /// </summary>
        [Test]
        public void TestAddMultiAssetResponsiveDisplayAdVBExample()
        {
            AdWordsAppConfig config = (AdWordsAppConfig) user.Config;
            RunExample(delegate()
            {
                new VBExamples.AddMultiAssetResponsiveDisplayAd().Run(user, displayAdGroupId);
            });
        }
    }
}
