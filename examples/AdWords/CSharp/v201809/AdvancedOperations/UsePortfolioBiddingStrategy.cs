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
    /// This code example adds a portfolio bidding strategy and uses it to
    /// construct a campaign.
    /// </summary>
    public class UsePortfolioBiddingStrategy : ExampleBase
    {
        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            UsePortfolioBiddingStrategy codeExample = new UsePortfolioBiddingStrategy();
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
                return "This code example adds a portfolio bidding strategy and uses it to " +
                    "construct a campaign.";
            }
        }

        /// <summary>
        /// Runs the specified code example.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        public void Run(AdWordsUser user)
        {
            string BIDDINGSTRATEGY_NAME = "Maximize Clicks " + ExampleUtilities.GetRandomString();
            const long BID_CEILING = 2000000;
            const long SPEND_TARGET = 20000000;

            string BUDGET_NAME =
                "Shared Interplanetary Budget #" + ExampleUtilities.GetRandomString();
            const long BUDGET_AMOUNT = 30000000;

            string CAMPAIGN_NAME = "Interplanetary Cruise #" + ExampleUtilities.GetRandomString();

            try
            {
                SharedBiddingStrategy portfolioBiddingStrategy =
                    CreateBiddingStrategy(user, BIDDINGSTRATEGY_NAME, BID_CEILING, SPEND_TARGET);
                Console.WriteLine(
                    "Portfolio bidding strategy with name '{0}' and ID {1} of type " +
                    "{2} was created.", portfolioBiddingStrategy.name, portfolioBiddingStrategy.id,
                    portfolioBiddingStrategy.biddingScheme.BiddingSchemeType);

                Budget sharedBudget = CreateSharedBudget(user, BUDGET_NAME, BUDGET_AMOUNT);

                Campaign newCampaign = CreateCampaignWithBiddingStrategy(user, CAMPAIGN_NAME,
                    portfolioBiddingStrategy.id, sharedBudget.budgetId);

                Console.WriteLine(
                    "Campaign with name '{0}', ID {1} and bidding scheme ID {2} was " + "created.",
                    newCampaign.name, newCampaign.id,
                    newCampaign.biddingStrategyConfiguration.biddingStrategyId);
            }
            catch (Exception e)
            {
                throw new System.ApplicationException(
                    "Failed to create campaign that uses portfolio " + "bidding strategy.", e);
            }
        }

        /// <summary>
        /// Creates the portfolio bidding strategy.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="name">The bidding strategy name.</param>
        /// <param name="bidCeiling">The bid ceiling.</param>
        /// <param name="spendTarget">The spend target.</param>
        /// <returns>The bidding strategy object.</returns>
        private SharedBiddingStrategy CreateBiddingStrategy(AdWordsUser user, string name,
            long bidCeiling, long spendTarget)
        {
            using (BiddingStrategyService biddingStrategyService =
                (BiddingStrategyService) user.GetService(AdWordsService.v201809
                    .BiddingStrategyService))
            {
                // Create a portfolio bidding strategy.
                SharedBiddingStrategy portfolioBiddingStrategy = new SharedBiddingStrategy
                {
                    name = name
                };

                TargetSpendBiddingScheme biddingScheme = new TargetSpendBiddingScheme
                {
                    // Optionally set additional bidding scheme parameters.
                    bidCeiling = new Money
                    {
                        microAmount = bidCeiling
                    },

                    spendTarget = new Money
                    {
                        microAmount = spendTarget
                    }
                };

                portfolioBiddingStrategy.biddingScheme = biddingScheme;

                // Create operation.
                BiddingStrategyOperation operation = new BiddingStrategyOperation
                {
                    @operator = Operator.ADD,
                    operand = portfolioBiddingStrategy
                };

                return biddingStrategyService.mutate(new BiddingStrategyOperation[]
                {
                    operation
                }).value[0];
            }
        }

        /// <summary>
        /// Creates an explicit budget to be used only to create the Campaign.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="name">The budget name.</param>
        /// <param name="amount">The budget amount.</param>
        /// <returns>The budget object.</returns>
        private Budget CreateSharedBudget(AdWordsUser user, string name, long amount)
        {
            using (BudgetService budgetService =
                (BudgetService) user.GetService(AdWordsService.v201809.BudgetService))
            {
                // Create a shared budget
                Budget budget = new Budget
                {
                    name = name,
                    amount = new Money
                    {
                        microAmount = amount
                    },
                    deliveryMethod = BudgetBudgetDeliveryMethod.STANDARD,
                    isExplicitlyShared = true
                };

                // Create operation.
                BudgetOperation operation = new BudgetOperation
                {
                    operand = budget,
                    @operator = Operator.ADD
                };

                // Make the mutate request.
                return budgetService.mutate(new BudgetOperation[]
                {
                    operation
                }).value[0];
            }
        }

        /// <summary>
        /// Creates the campaign with a portfolio bidding strategy.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="name">The campaign name.</param>
        /// <param name="biddingStrategyId">The bidding strategy id.</param>
        /// <param name="sharedBudgetId">The shared budget id.</param>
        /// <returns>The campaign object.</returns>
        private Campaign CreateCampaignWithBiddingStrategy(AdWordsUser user, string name,
            long biddingStrategyId, long sharedBudgetId)
        {
            using (CampaignService campaignService =
                (CampaignService) user.GetService(AdWordsService.v201809.CampaignService))
            {
                // Create campaign.
                Campaign campaign = new Campaign
                {
                    name = name,
                    advertisingChannelType = AdvertisingChannelType.SEARCH,

                    // Recommendation: Set the campaign to PAUSED when creating it to prevent
                    // the ads from immediately serving. Set to ENABLED once you've added
                    // targeting and the ads are ready to serve.
                    status = CampaignStatus.PAUSED,

                    // Set the budget.
                    budget = new Budget
                    {
                        budgetId = sharedBudgetId
                    }
                };

                // Set bidding strategy (required).
                BiddingStrategyConfiguration biddingStrategyConfiguration =
                    new BiddingStrategyConfiguration
                    {
                        biddingStrategyId = biddingStrategyId
                    };

                campaign.biddingStrategyConfiguration = biddingStrategyConfiguration;

                // Set network targeting (recommended).
                NetworkSetting networkSetting = new NetworkSetting
                {
                    targetGoogleSearch = true,
                    targetSearchNetwork = true,
                    targetContentNetwork = true
                };
                campaign.networkSetting = networkSetting;

                // Create operation.
                CampaignOperation operation = new CampaignOperation
                {
                    operand = campaign,
                    @operator = Operator.ADD
                };

                return campaignService.mutate(new CampaignOperation[]
                {
                    operation
                }).value[0];
            }
        }
    }
}
