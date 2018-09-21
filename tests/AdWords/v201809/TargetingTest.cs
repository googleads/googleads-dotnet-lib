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

using Google.Api.Ads.AdWords.v201809;

using NUnit.Framework;

using CSharpExamples = Google.Api.Ads.AdWords.Examples.CSharp.v201809;
using VBExamples = Google.Api.Ads.AdWords.Examples.VB.v201809;

namespace Google.Api.Ads.AdWords.Tests.v201809
{
    /// <summary>
    /// Test cases for all the code examples under v201809\Targeting.
    /// </summary>
    internal class TargetingTest : VersionedExampleTestsBase
    {
        private long campaignId;
        private long campaignCriterionId;

        /// <summary>
        /// Inits this instance.
        /// </summary>
        [SetUp]
        public void Init()
        {
            campaignId = utils.CreateSearchCampaign(user, BiddingStrategyType.MANUAL_CPC);
            campaignCriterionId = utils.AddCampaignTargetingCriteria(user, campaignId);
        }

        /// <summary>
        /// Tests the AddCampaignTargetingCriteria VB.NET code example.
        /// </summary>
        [Test]
        public void TestAddCampaignTargetingCriteriaVBExample()
        {
            RunExample(delegate()
            {
                new VBExamples.AddCampaignTargetingCriteria().Run(user, campaignId, null);
            });
        }

        /// <summary>
        /// Tests the AddCampaignTargetingCriteria C# code example.
        /// </summary>
        [Test]
        public void TestAddCampaignTargetingCriteriaCSharpExample()
        {
            RunExample(delegate()
            {
                new CSharpExamples.AddCampaignTargetingCriteria().Run(user, campaignId, null);
            });
        }

        /// <summary>
        /// Tests the GetCampaignTargetingCriteria VB.NET code example.
        /// </summary>
        [Test]
        public void TestGetCampaignTargetingCriteriaVBExample()
        {
            RunExample(delegate()
            {
                new VBExamples.GetCampaignTargetingCriteria().Run(user, campaignId);
            });
        }

        /// <summary>
        /// Tests the GetCampaignTargetingCriteria C# code example.
        /// </summary>
        [Test]
        public void TestGetCampaignTargetingCriteriaCSharpExample()
        {
            RunExample(delegate()
            {
                new CSharpExamples.GetCampaignTargetingCriteria().Run(user, campaignId);
            });
        }

        /// <summary>
        /// Tests the GetTargetableLanguagesAndCarriers VB.NET code example.
        /// </summary>
        [Test]
        public void TestGetTargetableLanguagesAndCarriersVBExample()
        {
            RunExample(delegate()
            {
                new VBExamples.GetTargetableLanguagesAndCarriers().Run(user);
            });
        }

        /// <summary>
        /// Tests the GetTargetableLanguagesAndCarriers C# code example.
        /// </summary>
        [Test]
        public void TestGetTargetableLanguagesAndCarriersCSharpExample()
        {
            RunExample(delegate()
            {
                new CSharpExamples.GetTargetableLanguagesAndCarriers().Run(user);
            });
        }

        /// <summary>
        /// Tests the LookupLocation VB.NET code example.
        /// </summary>
        [Test]
        public void TestLookupLocationVBExample()
        {
            RunExample(delegate() { new VBExamples.LookupLocation().Run(user); });
        }

        /// <summary>
        /// Tests the LookupLocation C# code example.
        /// </summary>
        [Test]
        public void TestLookupLocationCSharpExample()
        {
            RunExample(delegate() { new CSharpExamples.LookupLocation().Run(user); });
        }

        /// <summary>
        /// Tests the AddCustomerNegativeCriteria VB.NET code example.
        /// </summary>
        [Test]
        public void TestAddCustomerNegativeCriteriaVBExample()
        {
            RunExample(delegate() { new VBExamples.AddCustomerNegativeCriteria().Run(user); });
        }

        /// <summary>
        /// Tests the AddCustomerNegativeCriteria C# code example.
        /// </summary>
        [Test]
        public void TestAddCustomerNegativeCriteriaCSharpExample()
        {
            RunExample(delegate() { new CSharpExamples.AddCustomerNegativeCriteria().Run(user); });
        }
    }
}
