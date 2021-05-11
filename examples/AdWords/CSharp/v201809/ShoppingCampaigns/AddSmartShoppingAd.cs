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
using Google.Api.Ads.AdWords.Util.Shopping.v201809;
using Google.Api.Ads.AdWords.v201809;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201809
{
    /// <summary>
    /// This code example adds a Smart Shopping campaign with an ad group and ad group ad.
    /// </summary>
    public class AddSmartShoppingAd : ExampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example adds a Smart Shopping campaign with an ad group and " +
                    "ad group ad.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            AddSmartShoppingAd codeExample = new AddSmartShoppingAd();
            Console.WriteLine(codeExample.Description);
            try
            {
                long merchantId = long.Parse("INSERT_MERCHANT_ID_HERE");
                bool createDefaultPartition = false;
                codeExample.Run(new AdWordsUser(), merchantId, createDefaultPartition);
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception occurred while running this code example. {0}",
                    ExampleUtilities.FormatException(e));
            }
        }

        /// <summary>
        /// Runs the code example.
        /// </summary>
        /// <param name="user">The Google Ads user.</param>
        /// <param name="merchantId">The Merchant Center ID for the new campaign.</param>
        /// <param name="createDefaultPartition">If <c>true</c>, a default product partition for
        /// all products will be created.</param>
        public void Run(AdWordsUser user, long merchantId, bool createDefaultPartition)
        {
            Budget budget = CreateBudget(user);
            Campaign campaign =
                CreateSmartShoppingCampaign(user, budget.budgetId, merchantId);
            AdGroup adGroup = CreateSmartShoppingAdGroup(user, campaign.id);
            CreateSmartShoppingAd(user, adGroup.id);
            if (createDefaultPartition)
            {
                CreateDefaultPartition(user, adGroup.id);
            }
        }

        /// <summary>
        /// Creates a non-shared budget for a Smart Shopping campaign. Smart Shopping campaigns
        /// support only non-shared budgets.
        /// </summary>
        /// <param name="user">The Google Ads user.</param>
        /// <returns>The newly created budget.</returns>
        private Budget CreateBudget(AdWordsUser user)
        {
            using (BudgetService budgetService =
               (BudgetService) user.GetService(AdWordsService.v201809.BudgetService))
            {
                // Create a budget.
                Budget budget = new Budget()
                {
                    name = "Interplanetary Cruise #" + ExampleUtilities.GetRandomString(),
                    amount = new Money()
                    {
                        // This budget equals 50.00 units of your account's currency, e.g.,
                        // 50 USD if your currency is USD.
                        microAmount = 50_000_000L
                    },
                    deliveryMethod = BudgetBudgetDeliveryMethod.STANDARD,
                    // Non-shared budgets are required for Smart Shopping campaigns.
                    isExplicitlyShared = false
                };

                // Create operation.
                BudgetOperation budgetOperation = new BudgetOperation()
                {
                    operand = budget,
                    @operator = Operator.ADD
                };

                // Add the budget.
                Budget newBudget = budgetService.mutate(
                    new BudgetOperation[] { budgetOperation }).value[0];
                Console.WriteLine($"Budget with name '{newBudget.name}' and ID " +
                    $"{newBudget.budgetId} was added.");
                return newBudget;
            }
        }

        /// <summary>
        /// Creates a Smart Shopping campaign.
        /// </summary>
        /// <param name="user">The Google Ads user.</param>
        /// <param name="budgetId">The budget ID.</param>
        /// <param name="merchantId">The merchant center account ID.</param>
        /// <returns>The newly created campaign.</returns>
        private Campaign CreateSmartShoppingCampaign(AdWordsUser user, long budgetId,
            long merchantId)
        {
            using (CampaignService campaignService =
                (CampaignService) user.GetService(AdWordsService.v201809.CampaignService))
            {
                // Create a campaign with required and optional settings.
                Campaign campaign = new Campaign()
                {
                    name = "Smart Shopping campaign #" + ExampleUtilities.GetRandomString(),

                    // The advertisingChannelType is what makes this a Shopping campaign.
                    advertisingChannelType = AdvertisingChannelType.SHOPPING,

                    // Sets the advertisingChannelSubType to SHOPPING_GOAL_OPTIMIZED_ADS to
                    // make this a Smart Shopping campaign.
                    advertisingChannelSubType =
                        AdvertisingChannelSubType.SHOPPING_GOAL_OPTIMIZED_ADS,

                    // Recommendation: Set the campaign to PAUSED when creating it to stop
                    // the ads from immediately serving. Set to ENABLED once you've added
                    // targeting and the ads are ready to serve.
                    status = CampaignStatus.PAUSED,

                    // Set a budget.
                    budget = new Budget()
                    {
                        budgetId = budgetId
                    },

                    // Set bidding strategy. Only MAXIMIZE_CONVERSION_VALUE is supported.
                    biddingStrategyConfiguration = new BiddingStrategyConfiguration()
                    {
                        biddingStrategyType = BiddingStrategyType.MAXIMIZE_CONVERSION_VALUE
                    },

                    // All Shopping campaigns need a ShoppingSetting.
                    settings = new Setting[]
                    {
                        new ShoppingSetting()
                        {
                            salesCountry = "US",
                            merchantId = merchantId
                        }
                    }
                };

                // Create operation.
                CampaignOperation campaignOperation = new CampaignOperation()
                {
                    operand = campaign,
                    @operator = Operator.ADD
                };

                // Make the mutate request and retrieve the result.
                Campaign newCampaign =
                    campaignService.mutate(new CampaignOperation[] { campaignOperation }).value[0];

                // Display result.
                Console.WriteLine($"Smart Shopping campaign with name '{newCampaign.name}' and " +
                    $"ID {newCampaign.id} was added.");
                return newCampaign;
            }
        }

        /// <summary>
        /// Creates a Smart Shopping ad group by setting the ad group type to
        /// SHOPPING_GOAL_OPTIMIZED_ADS.
        /// </summary>
        /// <param name="user">The Google Ads user.</param>
        /// <param name="campaignId">The campaign ID.</param>
        private AdGroup CreateSmartShoppingAdGroup(AdWordsUser user, long campaignId)
        {
            using (AdGroupService adGroupService =
                (AdGroupService) user.GetService(AdWordsService.v201809.AdGroupService))
            {
                // Create ad group.
                AdGroup adGroup = new AdGroup()
                {
                    campaignId = campaignId,
                    name = "Smart Shopping ad group #" + ExampleUtilities.GetRandomString(),

                    // Set the ad group type to SHOPPING_GOAL_OPTIMIZED_ADS.
                    adGroupType = AdGroupType.SHOPPING_GOAL_OPTIMIZED_ADS
                };

                // Create operation.
                AdGroupOperation adGroupOperation = new AdGroupOperation()
                {
                    operand = adGroup,
                    @operator = Operator.ADD
                };


                // Make the mutate request.
                AdGroup newAdGroup =
                    adGroupService.mutate(new AdGroupOperation[] { adGroupOperation }).value[0];

                // Display result.
                Console.WriteLine($"Smart Shopping ad group with name '{adGroup.name}' and " +
                    $"ID {adGroup.id} was added.");

                return newAdGroup;
            }
        }

        /// <summary>
        /// Creates the smart shopping ad.
        /// </summary>
        /// <param name="user">The Google Ads user.</param>
        /// <param name="adGroupId">The ad group ID.</param>
        private void CreateSmartShoppingAd(AdWordsUser user, long adGroupId)
        {
            using (AdGroupAdService adGroupAdService =
                (AdGroupAdService) user.GetService(AdWordsService.v201809.AdGroupAdService))
            {
                // Create ad group ad.
                AdGroupAd adGroupAd = new AdGroupAd()
                {
                    adGroupId = adGroupId,
                    // Create a Smart Shopping ad (Goal-optimized Shopping ad).
                    ad = new GoalOptimizedShoppingAd() { }
                };


                // Create operation.
                AdGroupAdOperation adGroupAdOperation = new AdGroupAdOperation()
                {
                    operand = adGroupAd,
                    @operator = Operator.ADD
                };


                // Make the mutate request.
                AdGroupAd newAdGroupAd = adGroupAdService.mutate(
                    new AdGroupAdOperation[] { adGroupAdOperation }).value[0];

                // Display result.
                Console.WriteLine($"Smart Shopping ad with ID {newAdGroupAd.ad.id} was added.");
            }
        }

        /// <summary>
        /// Creates the default partition.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="adGroupId">The ad group ID.</param>
        private void CreateDefaultPartition(AdWordsUser user, long adGroupId)
        {
            // Create an ad group criterion for 'All products' using the ProductPartitionTree
            // utility.
            ProductPartitionTree productPartitionTree =
                ProductPartitionTree.CreateAdGroupTree(adGroupId, new List<AdGroupCriterion>());
            AdGroupCriterionOperation[] mutateOperations =
                productPartitionTree.GetMutateOperations();

            using (AdGroupCriterionService adGroupCriterionService =
                (AdGroupCriterionService) user.GetService(
                    AdWordsService.v201809.AdGroupCriterionService))
            {
                AdGroupCriterionReturnValue adGroupCriterionResult =
                    adGroupCriterionService.mutate(mutateOperations);

                // Display result.
                foreach (AdGroupCriterion adGroupCriterion in adGroupCriterionResult.value)
                {
                    Console.WriteLine(
                        $"Ad group criterion with ID {adGroupCriterion.criterion.id} in ad " +
                        $"group with ID {adGroupCriterion.adGroupId} was added.");
                }
            }

        }
    }
}
