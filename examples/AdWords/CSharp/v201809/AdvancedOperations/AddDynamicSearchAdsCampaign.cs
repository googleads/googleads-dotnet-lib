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
    /// This code example adds a Dynamic Search Ads campaign. To get campaigns, run GetCampaigns.cs.
    /// </summary>
    public class AddDynamicSearchAdsCampaign : ExampleBase
    {
        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            AddDynamicSearchAdsCampaign codeExample = new AddDynamicSearchAdsCampaign();
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
                return "This code example adds a Dynamic Search Ads campaign. To get campaigns, " +
                    "run GetCampaigns.cs.";
            }
        }

        /// <summary>
        /// Runs the code example.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        public void Run(AdWordsUser user)
        {
            Budget budget = CreateBudget(user);
            Campaign campaign = CreateCampaign(user, budget);
            AdGroup adGroup = CreateAdGroup(user, campaign.id);
            CreateExpandedDSA(user, adGroup.id);
            AddWebPageCriteria(user, adGroup.id);
            Console.WriteLine("Dynamic Search Ads campaign setup is complete.");
        }

        /// <summary>
        /// Creates the budget.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <returns>The newly created budget.</returns>
        private static Budget CreateBudget(AdWordsUser user)
        {
            using (BudgetService budgetService =
                (BudgetService) user.GetService(AdWordsService.v201809.BudgetService))
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
                    Budget newBudget = budgetRetval.value[0];
                    Console.WriteLine("Budget with ID = '{0}' and name = '{1}' was created.",
                        newBudget.budgetId, newBudget.name);
                    budgetService.Close();
                    return newBudget;
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException("Failed to add budget.", e);
                }
            }
        }


        /// <summary>
        /// Creates the campaign.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="budget">The campaign budget.</param>
        /// <returns>The newly created campaign.</returns>
        private static Campaign CreateCampaign(AdWordsUser user, Budget budget)
        {
            using (CampaignService campaignService =
                (CampaignService) user.GetService(AdWordsService.v201809.CampaignService))
            {
                // Create a Dynamic Search Ads campaign.
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

                // Required: Set the campaign's Dynamic Search Ads settings.
                DynamicSearchAdsSetting dynamicSearchAdsSetting = new DynamicSearchAdsSetting
                {
                    // Required: Set the domain name and language.
                    domainName = "example.com",
                    languageCode = "en"
                };

                // Set the campaign settings.
                campaign.settings = new Setting[]
                {
                    dynamicSearchAdsSetting
                };

                // Optional: Set the start date.
                campaign.startDate = DateTime.Now.AddDays(1).ToString("yyyyMMdd");

                // Optional: Set the end date.
                campaign.endDate = DateTime.Now.AddYears(1).ToString("yyyyMMdd");

                // Create the operation.
                CampaignOperation operation = new CampaignOperation
                {
                    @operator = Operator.ADD,
                    operand = campaign
                };

                try
                {
                    // Add the campaign.
                    CampaignReturnValue retVal = campaignService.mutate(new CampaignOperation[]
                    {
                        operation
                    });

                    // Display the results.
                    Campaign newCampaign = retVal.value[0];
                    Console.WriteLine("Campaign with id = '{0}' and name = '{1}' was added.",
                        newCampaign.id, newCampaign.name);
                    return newCampaign;
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException("Failed to add campaigns.", e);
                }
            }
        }


        /// <summary>
        /// Creates an ad group.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="campaignId">The campaign ID.</param>
        /// <returns>the newly created ad group.</returns>
        private static AdGroup CreateAdGroup(AdWordsUser user, long campaignId)
        {
            using (AdGroupService adGroupService =
                (AdGroupService) user.GetService(AdWordsService.v201809.AdGroupService))
            {
                // Create the ad group.
                AdGroup adGroup = new AdGroup
                {
                    // Required: Set the ad group's type to Dynamic Search Ads.
                    adGroupType = AdGroupType.SEARCH_DYNAMIC_ADS,

                    name = string.Format("Earth to Mars Cruises #{0}",
                        ExampleUtilities.GetRandomString()),
                    campaignId = campaignId,
                    status = AdGroupStatus.PAUSED,

                    // Recommended: Set a tracking URL template for your ad group if you want to use
                    // URL tracking software.
                    trackingUrlTemplate = "http://tracker.example.com/traveltracker/{escapedlpurl}"
                };

                // Set the ad group bids.
                BiddingStrategyConfiguration biddingConfig = new BiddingStrategyConfiguration();

                CpcBid cpcBid = new CpcBid
                {
                    bid = new Money
                    {
                        microAmount = 3000000
                    }
                };

                biddingConfig.bids = new Bids[]
                {
                    cpcBid
                };

                adGroup.biddingStrategyConfiguration = biddingConfig;

                // Create the operation.
                AdGroupOperation operation = new AdGroupOperation
                {
                    @operator = Operator.ADD,
                    operand = adGroup
                };

                try
                {
                    // Create the ad group.
                    AdGroupReturnValue retVal = adGroupService.mutate(new AdGroupOperation[]
                    {
                        operation
                    });

                    // Display the results.
                    AdGroup newAdGroup = retVal.value[0];
                    Console.WriteLine("Ad group with id = '{0}' and name = '{1}' was created.",
                        newAdGroup.id, newAdGroup.name);
                    return newAdGroup;
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException("Failed to create ad group.", e);
                }
            }
        }


        /// <summary>
        /// Creates an expanded Dynamic Search Ad.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="adGroupId">ID of the ad group in which ad is created.</param>
        /// <returns>The newly created ad.</returns>
        private static ExpandedDynamicSearchAd CreateExpandedDSA(AdWordsUser user, long adGroupId)
        {
            using (AdGroupAdService adGroupAdService =
                (AdGroupAdService) user.GetService(AdWordsService.v201809.AdGroupAdService))
            {
                // Create an Expanded Dynamic Search Ad. This ad will have its headline, display URL
                // and final URL auto-generated at serving time according to domain name specific
                // information provided by DynamicSearchAdsSetting at the campaign level.
                ExpandedDynamicSearchAd expandedDSA = new ExpandedDynamicSearchAd
                {
                    // Set the ad description.
                    description = "Buy your tickets now!",
                    description2 = "Discount ends soon"
                };

                // Create the ad group ad.
                AdGroupAd adGroupAd = new AdGroupAd
                {
                    adGroupId = adGroupId,
                    ad = expandedDSA,

                    // Optional: Set the status.
                    status = AdGroupAdStatus.PAUSED
                };

                // Create the operation.
                AdGroupAdOperation operation = new AdGroupAdOperation
                {
                    @operator = Operator.ADD,
                    operand = adGroupAd
                };

                try
                {
                    // Create the ad.
                    AdGroupAdReturnValue retval = adGroupAdService.mutate(new AdGroupAdOperation[]
                    {
                        operation
                    });

                    // Display the results.
                    AdGroupAd newAdGroupAd = retval.value[0];
                    ExpandedDynamicSearchAd newAd = newAdGroupAd.ad as ExpandedDynamicSearchAd;
                    Console.WriteLine(
                        "Expanded Dynamic Search Ad with ID '{0}' and description '{1}' " +
                        "was added.", newAd.id, newAd.description);
                    return newAd;
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException(
                        "Failed to create Expanded Dynamic Search Ad.", e);
                }
            }
        }


        /// <summary>
        /// Adds a web page criterion to target Dynamic Search Ads.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="adGroupId">The ad group ID.</param>
        /// <returns>The newly created web page criterion.</returns>
        private static BiddableAdGroupCriterion AddWebPageCriteria(AdWordsUser user, long adGroupId)
        {
            using (AdGroupCriterionService adGroupCriterionService =
                (AdGroupCriterionService) user.GetService(AdWordsService.v201809
                    .AdGroupCriterionService))
            {
                // Create a webpage criterion for special offers for mars cruise.
                WebpageParameter param = new WebpageParameter
                {
                    criterionName = "Special offers for mars"
                };

                WebpageCondition urlCondition = new WebpageCondition
                {
                    operand = WebpageConditionOperand.URL,
                    argument = "/marscruise/special"
                };

                WebpageCondition titleCondition = new WebpageCondition
                {
                    operand = WebpageConditionOperand.PAGE_TITLE,
                    argument = "Special Offer"
                };

                param.conditions = new WebpageCondition[]
                {
                    urlCondition,
                    titleCondition
                };

                Webpage webpage = new Webpage
                {
                    parameter = param
                };

                // Create biddable ad group criterion.
                BiddableAdGroupCriterion biddableAdGroupCriterion = new BiddableAdGroupCriterion
                {
                    adGroupId = adGroupId,
                    criterion = webpage,
                    userStatus = UserStatus.PAUSED
                };

                // Optional: set a custom bid.
                BiddingStrategyConfiguration biddingStrategyConfiguration =
                    new BiddingStrategyConfiguration();
                CpcBid bid = new CpcBid()
                {
                    bid = new Money()
                    {
                        microAmount = 10000000L
                    }
                };
                biddingStrategyConfiguration.bids = new Bids[]
                {
                    bid
                };
                biddableAdGroupCriterion.biddingStrategyConfiguration =
                    biddingStrategyConfiguration;

                // Create the operation.
                AdGroupCriterionOperation operation = new AdGroupCriterionOperation
                {
                    @operator = Operator.ADD,
                    operand = biddableAdGroupCriterion
                };

                try
                {
                    AdGroupCriterionReturnValue result = adGroupCriterionService.mutate(
                        new AdGroupCriterionOperation[]
                        {
                            operation
                        });

                    BiddableAdGroupCriterion newCriterion =
                        (BiddableAdGroupCriterion) result.value[0];
                    Console.WriteLine(
                        "Webpage criterion with ID = '{0}' was added to ad group ID '{1}'.",
                        newCriterion.criterion.id, newCriterion.adGroupId);
                    return newCriterion;
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException("Failed to create webpage criterion.", e);
                }
            }
        }

    }
}
