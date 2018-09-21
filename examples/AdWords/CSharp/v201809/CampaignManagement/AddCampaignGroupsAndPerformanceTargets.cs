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
using System.Collections.Generic;
using System.Linq;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201809
{
    /// <summary>
    /// This code example adds a campaign group and sets a performance target for that group. To
    /// get campaigns, run GetCampaigns.cs. To download reports, run
    /// DownloadCriteriaReportWithAwql.cs.
    /// </summary>
    public class AddCampaignGroupsAndPerformanceTargets : ExampleBase
    {
        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            AddCampaignGroupsAndPerformanceTargets codeExample =
                new AddCampaignGroupsAndPerformanceTargets();
            Console.WriteLine(codeExample.Description);
            try
            {
                long campaignId1 = long.Parse("INSERT_CAMPAIGN_ID1_HERE");
                long campaignId2 = long.Parse("INSERT_CAMPAIGN_ID2_HERE");
                codeExample.Run(new AdWordsUser(), campaignId1, campaignId2);
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
                return
                    "This code example adds a campaign group and sets a performance target for " +
                    "that group. To get campaigns, run GetCampaigns.cs. To download reports, run " +
                    "DownloadCriteriaReportWithAwql.cs.";
            }
        }

        /// <summary>
        /// Runs the code example.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="campaignId1">Id of the campaign to be added to the campaign group.</param>
        /// <param name="campaignId2">Id of the campaign to be added to the campaign group.</param>
        public void Run(AdWordsUser user, long campaignId1, long campaignId2)
        {
            CampaignGroup campaignGroup = CreateCampaignGroup(user);
            AddCampaignsToGroup(user, campaignGroup.id, new long[]
            {
                campaignId1,
                campaignId2
            });
            CreatePerformanceTarget(user, campaignGroup.id);
            Console.WriteLine("Campaign group and its performance target were setup successfully.");
        }

        /// <summary>
        /// Create a campaign group.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <returns>The campaign group.</returns>
        private static CampaignGroup CreateCampaignGroup(AdWordsUser user)
        {
            using (CampaignGroupService campaignGroupService =
                (CampaignGroupService) user.GetService(AdWordsService.v201809.CampaignGroupService))
            {
                // Create the campaign group.
                CampaignGroup campaignGroup = new CampaignGroup
                {
                    name = "Mars campaign group - " + ExampleUtilities.GetShortRandomString()
                };

                // Create the operation.
                CampaignGroupOperation operation = new CampaignGroupOperation
                {
                    operand = campaignGroup,
                    @operator = Operator.ADD
                };

                try
                {
                    CampaignGroupReturnValue retval = campaignGroupService.mutate(
                        new CampaignGroupOperation[]
                        {
                            operation
                        });

                    // Display the results.
                    CampaignGroup newCampaignGroup = retval.value[0];
                    Console.WriteLine(
                        "Campaign group with ID = '{0}' and name = '{1}' was created.",
                        newCampaignGroup.id, newCampaignGroup.name);
                    return newCampaignGroup;
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException("Failed to add campaign group.", e);
                }
            }
        }


        /// <summary>
        /// Adds multiple campaigns to a campaign group.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="campaignGroupId">The campaign group ID.</param>
        /// <param name="campaignIds">IDs of the campaigns to be added to the campaign group.
        /// </param>
        private static void AddCampaignsToGroup(AdWordsUser user, long campaignGroupId,
            long[] campaignIds)
        {
            using (CampaignService campaignService =
                (CampaignService) user.GetService(AdWordsService.v201809.CampaignService))
            {
                List<CampaignOperation> operations = new List<CampaignOperation>();

                for (int i = 0; i < campaignIds.Length; i++)
                {
                    Campaign campaign = new Campaign
                    {
                        id = campaignIds[i],
                        campaignGroupId = campaignGroupId
                    };

                    CampaignOperation operation = new CampaignOperation
                    {
                        operand = campaign,
                        @operator = Operator.SET
                    };
                    operations.Add(operation);
                }

                try
                {
                    CampaignReturnValue retval = campaignService.mutate(operations.ToArray());
                    List<long> updatedCampaignIds = new List<long>();
                    retval.value.ToList().ForEach(updatedCampaign =>
                        updatedCampaignIds.Add(updatedCampaign.id));

                    // Display the results.
                    Console.WriteLine(
                        "The following campaign IDs were added to the campaign group " +
                        "with ID '{0}':\n\t{1}'", campaignGroupId,
                        string.Join(", ", updatedCampaignIds));
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException(
                        "Failed to add campaigns to campaign group.", e);
                }
            }
        }


        /// <summary>
        /// Creates a performance target for the campaign group.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="campaignGroupId">Campaign group ID.</param>
        /// <returns>The newly created performance target.</returns>
        private static CampaignGroupPerformanceTarget CreatePerformanceTarget(AdWordsUser user,
            long campaignGroupId)
        {
            using (CampaignGroupPerformanceTargetService campaignGroupPerformanceTargetService =
                (CampaignGroupPerformanceTargetService) user.GetService(AdWordsService.v201809
                    .CampaignGroupPerformanceTargetService))
            {
                // Create the performance target.
                CampaignGroupPerformanceTarget campaignGroupPerformanceTarget =
                    new CampaignGroupPerformanceTarget
                    {
                        campaignGroupId = campaignGroupId
                    };

                PerformanceTarget performanceTarget = new PerformanceTarget
                {
                    // Keep the CPC for the campaigns < $3.
                    efficiencyTargetType = EfficiencyTargetType.CPC_LESS_THAN_OR_EQUAL_TO,
                    efficiencyTargetValue = 3000000,

                    // Keep the maximum spend under $50.
                    spendTargetType = SpendTargetType.MAXIMUM
                };
                Money maxSpend = new Money
                {
                    microAmount = 500000000
                };
                performanceTarget.spendTarget = maxSpend;

                // Aim for at least 3000 clicks.
                performanceTarget.volumeTargetValue = 3000;
                performanceTarget.volumeGoalType = VolumeGoalType.MAXIMIZE_CLICKS;

                // Start the performance target today, and run it for the next 90 days.
                System.DateTime startDate = System.DateTime.Now;
                System.DateTime endDate = startDate.AddDays(90);

                performanceTarget.startDate = startDate.ToString("yyyyMMdd");
                performanceTarget.endDate = endDate.ToString("yyyyMMdd");

                campaignGroupPerformanceTarget.performanceTarget = performanceTarget;

                // Create the operation.
                CampaignGroupPerformanceTargetOperation operation =
                    new CampaignGroupPerformanceTargetOperation
                    {
                        operand = campaignGroupPerformanceTarget,
                        @operator = Operator.ADD
                    };

                try
                {
                    CampaignGroupPerformanceTargetReturnValue retval =
                        campaignGroupPerformanceTargetService.mutate(
                            new CampaignGroupPerformanceTargetOperation[]
                            {
                                operation
                            });

                    // Display the results.
                    CampaignGroupPerformanceTarget newCampaignPerfTarget = retval.value[0];

                    Console.WriteLine(
                        "Campaign performance target with id = '{0}' was added for " +
                        "campaign group ID '{1}'.", newCampaignPerfTarget.id,
                        newCampaignPerfTarget.campaignGroupId);
                    return newCampaignPerfTarget;
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException(
                        "Failed to create campaign performance target.", e);
                }
            }
        }

    }
}
