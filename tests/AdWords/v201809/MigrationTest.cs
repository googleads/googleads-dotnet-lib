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
    /// Test cases for all the code examples under v201809\Migration.
    /// </summary>
    internal class MigrationTest : VersionedExampleTestsBase
    {
        private long campaignId;
        private long adGroupId;
        private long adId;

        /// <summary>
        /// Inits this instance.
        /// </summary>
        [SetUp]
        public void Init()
        {
            campaignId = utils.CreateSearchCampaign(user, BiddingStrategyType.MANUAL_CPC);
            adGroupId = utils.CreateAdGroup(user, campaignId);
            adId = utils.CreateExpandedTextAd(user, adGroupId, false);
        }

        /// <summary>
        /// Tests the MigrateToExtensionSettings VB.NET code example.
        /// </summary>
        [Test]
        public void MigrateToExtensionSettingsVBExample()
        {
            RunExample(delegate() { new VBExamples.MigrateToExtensionSettings().Run(user); });
        }

        /// <summary>
        /// Tests the MigrateToExtensionSettings C# code example.
        /// </summary>
        [Test]
        public void MigrateToExtensionSettingsCSharpExample()
        {
            RunExample(delegate() { new CSharpExamples.MigrateToExtensionSettings().Run(user); });
        }
    }
}
