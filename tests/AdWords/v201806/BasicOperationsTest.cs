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

using Google.Api.Ads.AdWords.v201806;

using NUnit.Framework;

using CSharpExamples = Google.Api.Ads.AdWords.Examples.CSharp.v201806;
using VBExamples = Google.Api.Ads.AdWords.Examples.VB.v201806;

namespace Google.Api.Ads.AdWords.Tests.v201806
{
    /// <summary>
    /// Test cases for all the code examples under v201806\BasicOperations.
    /// </summary>
    internal class BasicOperationsTest : VersionedExampleTestsBase
    {
        private long campaignId;
        private long adGroupId;
        private long adId;
        private long keywordId;
        private const long CPC_BID = 4000000;

        /// <summary>
        /// Inits this instance.
        /// </summary>
        [SetUp]
        public void Init()
        {
            campaignId = utils.CreateSearchCampaign(user, BiddingStrategyType.MANUAL_CPC);
            adGroupId = utils.CreateAdGroup(user, campaignId);
            adId = utils.CreateExpandedTextAd(user, adGroupId, false);
            keywordId = utils.CreateKeyword(user, adGroupId);
        }

        /// <summary>
        /// Tests the AddAdGroup VB.NET code example.
        /// </summary>
        [Test]
        public void TestAddAdGroupVBExample()
        {
            RunExample(delegate() { new VBExamples.AddAdGroups().Run(user, campaignId); });
        }

        /// <summary>
        /// Tests the AddAdGroup C# code example.
        /// </summary>
        [Test]
        public void TestAddAdGroupCSharpExample()
        {
            RunExample(delegate() { new CSharpExamples.AddAdGroups().Run(user, campaignId); });
        }

        /// <summary>
        /// Tests the AddCampaign VB.NET code example.
        /// </summary>
        [Test]
        public void TestAddCampaignVBExample()
        {
            RunExample(delegate() { new VBExamples.AddCampaigns().Run(user); });
        }

        /// <summary>
        /// Tests the AddCampaign C# code example.
        /// </summary>
        [Test]
        public void TestAddCampaignCSharpExample()
        {
            RunExample(delegate() { new CSharpExamples.AddCampaigns().Run(user); });
        }

        /// <summary>
        /// Tests the AddKeywords VB.NET code example.
        /// </summary>
        [Test]
        public void TestAddKeywordsVBExample()
        {
            RunExample(delegate() { new VBExamples.AddKeywords().Run(user, adGroupId); });
        }

        /// <summary>
        /// Tests the AddKeywords C# code example.
        /// </summary>
        [Test]
        public void TestAddKeywordsCSharpExample()
        {
            RunExample(delegate() { new CSharpExamples.AddKeywords().Run(user, adGroupId); });
        }

        /// <summary>
        /// Tests the AddExpandedTextAds VB.NET code example.
        /// </summary>
        [Test]
        public void TestAddExpandedTextAdsVBExample()
        {
            RunExample(delegate() { new VBExamples.AddExpandedTextAds().Run(user, adGroupId); });
        }

        /// <summary>
        /// Tests the AddExpandedTextAds C# code example.
        /// </summary>
        [Test]
        public void TestAddExpandedTextAdsCSharpExample()
        {
            RunExample(delegate()
            {
                new CSharpExamples.AddExpandedTextAds().Run(user, adGroupId);
            });
        }

        /// <summary>
        /// Tests the RemoveAdGroup VB.NET code example.
        /// </summary>
        [Test]
        public void TestRemoveAdGroupVBExample()
        {
            RunExample(delegate() { new VBExamples.RemoveAdGroup().Run(user, adGroupId); });
        }

        /// <summary>
        /// Tests the RemoveAdGroup C# code example.
        /// </summary>
        [Test]
        public void TestRemoveAdGroupCSharpExample()
        {
            RunExample(delegate() { new CSharpExamples.RemoveAdGroup().Run(user, adGroupId); });
        }

        /// <summary>
        /// Tests the RemoveAd VB.NET code example.
        /// </summary>
        [Test]
        public void TestRemoveAdVBExample()
        {
            RunExample(delegate() { new VBExamples.RemoveAd().Run(user, adGroupId, adId); });
        }

        /// <summary>
        /// Tests the RemoveAd C# code example.
        /// </summary>
        [Test]
        public void TestRemoveAdCSharpExample()
        {
            RunExample(delegate() { new CSharpExamples.RemoveAd().Run(user, adGroupId, adId); });
        }

        /// <summary>
        /// Tests the RemoveCampaign VB.NET code example.
        /// </summary>
        [Test]
        public void TestRemoveCampaignVBExample()
        {
            RunExample(delegate() { new VBExamples.RemoveCampaign().Run(user, campaignId); });
        }

        /// <summary>
        /// Tests the RemoveCampaign C# code example.
        /// </summary>
        [Test]
        public void TestRemoveCampaignCSharpExample()
        {
            RunExample(delegate() { new CSharpExamples.RemoveCampaign().Run(user, campaignId); });
        }

        /// <summary>
        /// Tests the RemoveKeyword VB.NET code example.
        /// </summary>
        [Test]
        public void TestRemoveKeywordVBExample()
        {
            RunExample(delegate()
            {
                new VBExamples.RemoveKeyword().Run(user, adGroupId, keywordId);
            });
        }

        /// <summary>
        /// Tests the RemoveKeyword C# code example.
        /// </summary>
        [Test]
        public void TestRemoveKeywordCSharpExample()
        {
            RunExample(delegate()
            {
                new CSharpExamples.RemoveKeyword().Run(user, adGroupId, keywordId);
            });
        }

        /// <summary>
        /// Tests the GetAdGroups VB.NET code example.
        /// </summary>
        [Test]
        public void TestGetAdGroupsVBExample()
        {
            RunExample(delegate() { new VBExamples.GetAdGroups().Run(user, campaignId); });
        }

        /// <summary>
        /// Tests the GetAdGroups C# code example.
        /// </summary>
        [Test]
        public void TestGetAdGroupsCSharpExample()
        {
            RunExample(delegate() { new CSharpExamples.GetAdGroups().Run(user, campaignId); });
        }

        /// <summary>
        /// Tests the GetCampaigns VB.NET code example.
        /// </summary>
        [Test]
        public void TestGetCampaignsVBExample()
        {
            RunExample(delegate() { new VBExamples.GetCampaigns().Run(user); });
        }

        /// <summary>
        /// Tests the GetCampaigns C# code example.
        /// </summary>
        [Test]
        public void TestGetCampaignsCSharpExample()
        {
            RunExample(delegate() { new CSharpExamples.GetCampaigns().Run(user); });
        }

        /// <summary>
        /// Tests the GetKeywords VB.NET code example.
        /// </summary>
        [Test]
        public void TestGetKeywordsVBExample()
        {
            RunExample(delegate() { new VBExamples.GetKeywords().Run(user, adGroupId); });
        }

        /// <summary>
        /// Tests the GetKeywords C# code example.
        /// </summary>
        [Test]
        public void TestGetKeywordsCSharpExample()
        {
            RunExample(delegate() { new CSharpExamples.GetKeywords().Run(user, adGroupId); });
        }

        /// <summary>
        /// Tests the GetExpandedTextAds VB.NET code example.
        /// </summary>
        [Test]
        public void TestGetExpandedTextAdsVBExample()
        {
            RunExample(delegate() { new VBExamples.GetExpandedTextAds().Run(user, adGroupId); });
        }

        /// <summary>
        /// Tests the GetExpandedTextAds C# code example.
        /// </summary>
        [Test]
        public void TestGetExpandedTextAdsCSharpExample()
        {
            RunExample(delegate()
            {
                new CSharpExamples.GetExpandedTextAds().Run(user, adGroupId);
            });
        }

        /// <summary>
        /// Tests the PauseAd VB.NET code example.
        /// </summary>
        [Test]
        public void TestPauseAdVBExample()
        {
            RunExample(delegate() { new VBExamples.PauseAd().Run(user, adGroupId, adId); });
        }

        /// <summary>
        /// Tests the PauseAd C# code example.
        /// </summary>
        [Test]
        public void TestPauseAdCSharpExample()
        {
            RunExample(delegate() { new CSharpExamples.PauseAd().Run(user, adGroupId, adId); });
        }

        /// <summary>
        /// Tests the UpdateAdGroup VB.NET code example.
        /// </summary>
        [Test]
        public void TestUpdateAdGroupVBExample()
        {
            VBExamples.UpdateAdGroup example = new VBExamples.UpdateAdGroup();
            RunExample(delegate()
            {
                example.Run(user, adGroupId, CPC_BID);
                example.Run(user, adGroupId, null);
            });
        }

        /// <summary>
        /// Tests the UpdateAdGroup C# code example.
        /// </summary>
        [Test]
        public void TestUpdateAdGroupCSharpExample()
        {
            CSharpExamples.UpdateAdGroup example = new CSharpExamples.UpdateAdGroup();
            RunExample(delegate()
            {
                example.Run(user, adGroupId, CPC_BID);
                example.Run(user, adGroupId, null);
            });
        }

        /// <summary>
        /// Tests the UpdateCampaign VB.NET code example.
        /// </summary>
        [Test]
        public void TestUpdateCampaignVBExample()
        {
            RunExample(delegate() { new VBExamples.UpdateCampaign().Run(user, campaignId); });
        }

        /// <summary>
        /// Tests the UpdateCampaign C# code example.
        /// </summary>
        [Test]
        public void TestUpdateCampaignCSharpExample()
        {
            RunExample(delegate() { new CSharpExamples.UpdateCampaign().Run(user, campaignId); });
        }

        /// <summary>
        /// Tests the UpdateKeyword VB.NET code example.
        /// </summary>
        [Test]
        public void TestUpdateKeywordVBExample()
        {
            RunExample(delegate()
            {
                new VBExamples.UpdateKeyword().Run(user, adGroupId, keywordId);
            });
        }

        /// <summary>
        /// Tests the UpdateKeyword C# code example.
        /// </summary>
        [Test]
        public void TestUpdateKeywordCSharpExample()
        {
            RunExample(delegate()
            {
                new CSharpExamples.UpdateKeyword().Run(user, adGroupId, keywordId);
            });
        }

        /// <summary>
        /// Tests the AddResponsiveSearchAd VB.NET code example.
        /// </summary>
        [Test]
        public void TestAddResponsiveSearchAdVBExample()
        {
            RunExample(delegate() { new VBExamples.AddResponsiveSearchAd().Run(user, adGroupId); });
        }

        /// <summary>
        /// Tests the AddResponsiveSearchAd C# code example.
        /// </summary>
        [Test]
        public void TestAddResponsiveSearchAdCSharpExample()
        {
            RunExample(delegate()
            {
                new CSharpExamples.AddResponsiveSearchAd().Run(user, adGroupId);
            });
        }

        /// <summary>
        /// Tests the UpdateExpandedTextAd VB.NET code example.
        /// </summary>
        [Test]
        public void TestUpdateExpandedTextAdVBExample()
        {
            RunExample(delegate() { new VBExamples.UpdateExpandedTextAd().Run(user, adId); });
        }

        /// <summary>
        /// Tests the UpdateExpandedTextAd C# code example.
        /// </summary>
        [Test]
        public void TestUpdateExpandedTextAdCSharpExample()
        {
            RunExample(delegate() { new CSharpExamples.UpdateExpandedTextAd().Run(user, adId); });
        }
    }
}
