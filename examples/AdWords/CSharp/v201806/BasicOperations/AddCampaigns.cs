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

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201806
{
    /// <summary>
    /// This code example adds campaigns. To get campaigns, run GetCampaigns.cs.
    /// </summary>
    public class AddCampaigns : ExampleBase
    {
        /// <summary>
        /// Number of items being added / updated in this code example.
        /// </summary>
        private const int NUM_ITEMS = 5;

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            AddCampaigns codeExample = new AddCampaigns();
            Console.WriteLine(codeExample.Description);
            try
            {
                codeExample.Run(new AdWordsUser());
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
                return "This code example adds campaigns. To get campaigns, run GetCampaigns.cs.";
            }
        }

        /// <summary>
        /// Runs the code example.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        public void Run(AdWordsUser user)
        {
            using (CampaignService campaignService =
                (CampaignService) user.GetService(AdWordsService.v201806.CampaignService))
            {
                Budget budget = CreateBudget(user);

                List<CampaignOperation> operations = new List<CampaignOperation>();

                for (int i = 0; i < NUM_ITEMS; i++)
                {
                    // Create the campaign.
                    Campaign campaign = new Campaign
                    {
                        name = "Interplanetary Cruise #" + ExampleUtilities.GetRandomString(),
                        advertisingChannelType = AdvertisingChannelType.SEARCH,

                        // Recommendation: Set the campaign to PAUSED when creating it to prevent
                        // the ads from immediately serving. Set to ENABLED once you've added
                        // targeting and the ads are ready to serve.
                        status = CampaignStatus.PAUSED
                    };

                    BiddingStrategyConfiguration biddingConfig = new BiddingStrategyConfiguration
                    {
                        biddingStrategyType = BiddingStrategyType.MANUAL_CPC
                    };
                    campaign.biddingStrategyConfiguration = biddingConfig;

                    campaign.budget = new Budget
                    {
                        budgetId = budget.budgetId
                    };

                    // Set the campaign network options.
                    campaign.networkSetting = new NetworkSetting
                    {
                        targetGoogleSearch = true,
                        targetSearchNetwork = true,
                        targetContentNetwork = false,
                        targetPartnerSearchNetwork = false
                    };

                    // Set the campaign settings for Advanced location options.
                    GeoTargetTypeSetting geoSetting = new GeoTargetTypeSetting
                    {
                        positiveGeoTargetType = GeoTargetTypeSettingPositiveGeoTargetType.DONT_CARE,
                        negativeGeoTargetType = GeoTargetTypeSettingNegativeGeoTargetType.DONT_CARE
                    };

                    campaign.settings = new Setting[]
                    {
                        geoSetting
                    };

                    // Optional: Set the start date.
                    campaign.startDate = DateTime.Now.AddDays(1).ToString("yyyyMMdd");

                    // Optional: Set the end date.
                    campaign.endDate = DateTime.Now.AddYears(1).ToString("yyyyMMdd");

                    // Optional: Set the frequency cap.
                    FrequencyCap frequencyCap = new FrequencyCap
                    {
                        impressions = 5,
                        level = Level.ADGROUP,
                        timeUnit = TimeUnit.DAY
                    };
                    campaign.frequencyCap = frequencyCap;

                    // Create the operation.
                    CampaignOperation operation = new CampaignOperation
                    {
                        @operator = Operator.ADD,
                        operand = campaign
                    };

                    operations.Add(operation);
                }

                try
                {
                    // Add the campaign.
                    CampaignReturnValue retVal = campaignService.mutate(operations.ToArray());

                    // Display the results.
                    if (retVal != null && retVal.value != null && retVal.value.Length > 0)
                    {
                        foreach (Campaign newCampaign in retVal.value)
                        {
                            Console.WriteLine(
                                "Campaign with name = '{0}' and id = '{1}' was added.",
                                newCampaign.name, newCampaign.id);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No campaigns were added.");
                    }
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException("Failed to add campaigns.", e);
                }

            }
        }

        /// <summary>
        /// Creates the budget for the campaign.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <returns>The budget instance.</returns>
        private static Budget CreateBudget(AdWordsUser user)
        {
            using (BudgetService budgetService =
                (BudgetService) user.GetService(AdWordsService.v201806.BudgetService))
            {
                // Create the campaign budget.
                Budget budget = new Budget
                {
                    name = "Interplanetary Cruise Budget #" + ExampleUtilities.GetRandomString(),
                    deliveryMethod = BudgetBudgetDeliveryMethod.STANDARD,
                    amount = new Money
                    {
                        microAmount = 500000
                    }
                };

                BudgetOperation budgetOperation = new BudgetOperation
                {
                    @operator = Operator.ADD,
                    operand = budget
                };

                try
                {
                    BudgetReturnValue budgetRetval = budgetService.mutate(new BudgetOperation[]
                    {
                        budgetOperation
                    });
                    return budgetRetval.value[0];
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException("Failed to add shared budget.", e);
                }
            }

        }
    }
}
