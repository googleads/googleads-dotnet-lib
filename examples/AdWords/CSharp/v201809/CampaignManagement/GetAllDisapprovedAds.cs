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
using Google.Api.Ads.AdWords.v201809;

using System;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201809
{
    /// <summary>
    /// This code example retrieves all the disapproved ads in a given campaign.
    /// </summary>
    public class GetAllDisapprovedAds : ExampleBase
    {
        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            GetAllDisapprovedAds codeExample = new GetAllDisapprovedAds();
            Console.WriteLine(codeExample.Description);
            try
            {
                long campaignId = long.Parse("INSERT_CAMPAIGN_ID_HERE");
                codeExample.Run(new AdWordsUser(), campaignId);
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception occurred while running this code example. {0}",
                    ExampleUtilities.FormatException(e));
            }
        }

        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example retrieves all the disapproved ads in a given campaign.";
            }
        }

        /// <summary>
        /// Runs the code example.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="campaignId">Id of the campaign for which disapproved ads
        /// are retrieved.</param>
        public void Run(AdWordsUser user, long campaignId)
        {
            using (AdGroupAdService adGroupAdService =
                (AdGroupAdService) user.GetService(AdWordsService.v201809.AdGroupAdService))
            {
                // Create the selector.
                Selector selector = new Selector()
                {
                    fields = new string[]
                    {
                        Ad.Fields.Id,
                        AdGroupAd.Fields.PolicySummary
                    },
                    predicates = new Predicate[]
                    {
                        Predicate.Equals(AdGroup.Fields.CampaignId, campaignId),
                        Predicate.Equals(AdGroupAdPolicySummary.Fields.CombinedApprovalStatus,
                            PolicyApprovalStatus.DISAPPROVED.ToString())
                    },
                    paging = Paging.Default
                };

                AdGroupAdPage page = new AdGroupAdPage();
                int disapprovedAdsCount = 0;

                try
                {
                    do
                    {
                        // Get the disapproved ads.
                        page = adGroupAdService.get(selector);

                        // Display the results.
                        if (page != null && page.entries != null)
                        {
                            foreach (AdGroupAd adGroupAd in page.entries)
                            {
                                AdGroupAdPolicySummary policySummary = adGroupAd.policySummary;
                                disapprovedAdsCount++;
                                Console.WriteLine(
                                    "Ad with ID {0} and type '{1}' was disapproved with the " +
                                    "following policy topic entries: ", adGroupAd.ad.id,
                                    adGroupAd.ad.AdType);
                                // Display the policy topic entries related to the ad disapproval.
                                foreach (PolicyTopicEntry policyTopicEntry in policySummary
                                    .policyTopicEntries)
                                {
                                    Console.WriteLine("  topic id: {0}, topic name: '{1}'",
                                        policyTopicEntry.policyTopicId,
                                        policyTopicEntry.policyTopicName);
                                    // Display the attributes and values that triggered the policy
                                    // topic.
                                    if (policyTopicEntry.policyTopicEvidences != null)
                                    {
                                        foreach (PolicyTopicEvidence evidence in policyTopicEntry
                                            .policyTopicEvidences)
                                        {
                                            Console.WriteLine("    evidence type: {0}",
                                                evidence.policyTopicEvidenceType);
                                            if (evidence.evidenceTextList != null)
                                            {
                                                for (int i = 0;
                                                    i < evidence.evidenceTextList.Length;
                                                    i++)
                                                {
                                                    Console.WriteLine(
                                                        "      evidence text[{0}]: {1}", i,
                                                        evidence.evidenceTextList[i]);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        selector.paging.IncreaseOffset();
                    } while (selector.paging.startIndex < page.totalNumEntries);

                    Console.WriteLine("Number of disapproved ads found: {0}", disapprovedAdsCount);
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException("Failed to get disapproved ads.", e);
                }
            }
        }
    }
}
