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
using Google.Api.Ads.Common.Util;

using System;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201809
{
    /// <summary>
    /// This code example adds a Shopping campaign for Showcase ads.
    /// </summary>
    public class AddShoppingCampaignForShowcaseAds : ExampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get { return "This code example adds a Shopping campaign for Showcase ads."; }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            AddShoppingCampaignForShowcaseAds codeExample = new AddShoppingCampaignForShowcaseAds();
            Console.WriteLine(codeExample.Description);
            try
            {
                long budgetId = long.Parse("INSERT_BUDGET_ID_HERE");
                long merchantId = long.Parse("INSERT_MERCHANT_ID_HERE");
                codeExample.Run(new AdWordsUser(), budgetId, merchantId);
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
        /// <param name="user">The AdWords user.</param>
        /// <param name="budgetId">The budget id.</param>
        /// <param name="merchantId">The Merchant Center account ID.</param>
        public void Run(AdWordsUser user, long budgetId, long merchantId)
        {
            try
            {
                Campaign campaign = CreateCampaign(user, budgetId, merchantId);
                Console.WriteLine("Campaign with name '{0}' and ID '{1}' was added.", campaign.name,
                    campaign.id);

                AdGroup adGroup = CreateAdGroup(user, campaign);
                Console.WriteLine("Ad group with name '{0}' and ID '{1}' was added.", adGroup.name,
                    adGroup.id);

                AdGroupAd adGroupAd = CreateShowcaseAd(user, adGroup);
                Console.WriteLine("Showcase ad with ID '{0}' was added.", adGroupAd.ad.id);

                ProductPartitionTree partitionTree = CreateProductPartition(user, adGroup.id);
                Console.WriteLine("Final tree: {0}", partitionTree);
            }
            catch (Exception e)
            {
                throw new System.ApplicationException(
                    "Failed to create shopping campaign for " + "showcase ads.", e);
            }
        }

        /// <summary>
        /// Creates the Shopping campaign.
        /// </summary>
        /// <param name="user">The AdWords user for which the campaign is created.</param>
        /// <param name="budgetId">The budget ID.</param>
        /// <param name="merchantId">The Merchant Center ID.</param>
        /// <returns>The newly created Shopping campaign.</returns>
        private static Campaign CreateCampaign(AdWordsUser user, long budgetId, long merchantId)
        {
            using (CampaignService campaignService =
                (CampaignService) user.GetService(AdWordsService.v201809.CampaignService))
            {
                // Create the campaign.
                Campaign campaign = new Campaign
                {
                    name = "Shopping campaign #" + ExampleUtilities.GetRandomString(),

                    // The advertisingChannelType is what makes this a Shopping campaign.
                    advertisingChannelType = AdvertisingChannelType.SHOPPING,

                    // Recommendation: Set the campaign to PAUSED when creating it to prevent
                    // the ads from immediately serving. Set to ENABLED once you've added
                    // targeting and the ads are ready to serve.
                    status = CampaignStatus.PAUSED,

                    // Set shared budget (required).
                    budget = new Budget
                    {
                        budgetId = budgetId
                    }
                };

                // Set bidding strategy (required).
                BiddingStrategyConfiguration biddingStrategyConfiguration =
                    new BiddingStrategyConfiguration
                    {
                        // Note: Showcase ads require that the campaign has a ManualCpc
                        // BiddingStrategyConfiguration.
                        biddingStrategyType = BiddingStrategyType.MANUAL_CPC
                    };

                campaign.biddingStrategyConfiguration = biddingStrategyConfiguration;

                // All Shopping campaigns need a ShoppingSetting.
                ShoppingSetting shoppingSetting = new ShoppingSetting
                {
                    salesCountry = "US",
                    campaignPriority = 0,
                    merchantId = merchantId,

                    // Set to "true" to enable Local Inventory Ads in your campaign.
                    enableLocal = true
                };
                campaign.settings = new Setting[]
                {
                    shoppingSetting
                };

                // Create operation.
                CampaignOperation campaignOperation = new CampaignOperation
                {
                    operand = campaign,
                    @operator = Operator.ADD
                };

                // Make the mutate request.
                CampaignReturnValue retval = campaignService.mutate(new CampaignOperation[]
                {
                    campaignOperation
                });
                return retval.value[0];
            }
        }

        /// <summary>
        /// Creates the ad group in a Shopping campaign.
        /// </summary>
        /// <param name="user">The AdWords user for which the ad group is created.</param>
        /// <param name="campaign">The Shopping campaign.</param>
        /// <returns>The newly created ad group.</returns>
        private static AdGroup CreateAdGroup(AdWordsUser user, Campaign campaign)
        {
            using (AdGroupService adGroupService =
                (AdGroupService) user.GetService(AdWordsService.v201809.AdGroupService))
            {
                // Create ad group.
                AdGroup adGroup = new AdGroup
                {
                    campaignId = campaign.id,
                    name = "Ad Group #" + ExampleUtilities.GetRandomString(),

                    // Required: Set the ad group type to SHOPPING_SHOWCASE_ADS.
                    adGroupType = AdGroupType.SHOPPING_SHOWCASE_ADS
                };

                // Required: Set the ad group's bidding strategy configuration.
                BiddingStrategyConfiguration biddingConfiguration = new BiddingStrategyConfiguration
                {
                    // Optional: Set the bids.
                    bids = new Bids[]
                    {
                        new CpcBid()
                        {
                            bid = new Money()
                            {
                                microAmount = 100000
                            }
                        }
                    }
                };

                adGroup.biddingStrategyConfiguration = biddingConfiguration;

                // Create the operation.
                AdGroupOperation operation = new AdGroupOperation
                {
                    operand = adGroup,
                    @operator = Operator.ADD
                };

                // Make the mutate request.
                AdGroupReturnValue retval = adGroupService.mutate(new AdGroupOperation[]
                {
                    operation
                });
                return retval.value[0];
            }
        }

        /// <summary>
        /// Creates the Showcase ad.
        /// </summary>
        /// <param name="user">The AdWords user for which the ad is created.</param>
        /// <param name="adGroup">The ad group in which the ad is created.</param>
        /// <returns>The newly created Showcase ad.</returns>
        private static AdGroupAd CreateShowcaseAd(AdWordsUser user, AdGroup adGroup)
        {
            using (AdGroupAdService adGroupAdService =
                (AdGroupAdService) user.GetService(AdWordsService.v201809.AdGroupAdService))
            {
                // Create the Showcase ad.
                ShowcaseAd showcaseAd = new ShowcaseAd
                {
                    // Required: set the ad's name, final URLs and display URL.
                    name = "Showcase ad " + ExampleUtilities.GetShortRandomString(),
                    finalUrls = new string[]
                    {
                        "http://example.com/showcase"
                    },
                    displayUrl = "example.com"
                };

                // Required: Set the ad's expanded image.
                Image expandedImage = new Image
                {
                    mediaId = UploadImage(user, "https://goo.gl/IfVlpF")
                };
                showcaseAd.expandedImage = expandedImage;

                // Optional: Set the collapsed image.
                Image collapsedImage = new Image
                {
                    mediaId = UploadImage(user, "https://goo.gl/NqTxAE")
                };
                showcaseAd.collapsedImage = collapsedImage;

                // Create ad group ad.
                AdGroupAd adGroupAd = new AdGroupAd
                {
                    adGroupId = adGroup.id,
                    ad = showcaseAd
                };

                // Create operation.
                AdGroupAdOperation operation = new AdGroupAdOperation
                {
                    operand = adGroupAd,
                    @operator = Operator.ADD
                };

                // Make the mutate request.
                AdGroupAdReturnValue retval = adGroupAdService.mutate(new AdGroupAdOperation[]
                {
                    operation
                });
                return retval.value[0];
            }
        }

        /// <summary>
        /// Creates a product partition tree.
        /// </summary>
        /// <param name="user">The AdWords user for which the product partition is created.</param>
        /// <param name="adGroupId">Ad group ID.</param>
        /// <returns>The product partition.</returns>
        private static ProductPartitionTree CreateProductPartition(AdWordsUser user, long adGroupId)
        {
            using (AdGroupCriterionService adGroupCriterionService =
                (AdGroupCriterionService) user.GetService(AdWordsService.v201809
                    .AdGroupCriterionService))
            {
                // Build a new ProductPartitionTree using the ad group's current set of criteria.
                ProductPartitionTree partitionTree =
                    ProductPartitionTree.DownloadAdGroupTree(user, adGroupId);

                Console.WriteLine("Original tree: {0}", partitionTree);

                // Clear out any existing criteria.
                ProductPartitionNode rootNode = partitionTree.Root.RemoveAllChildren();

                // Make the root node a subdivision.
                rootNode = rootNode.AsSubdivision();

                // Add a unit node for condition = NEW to include it.
                rootNode.AddChild(
                    ProductDimensions.CreateCanonicalCondition(ProductCanonicalConditionCondition
                        .NEW));

                // Add a unit node for condition = USED to include it.
                rootNode.AddChild(
                    ProductDimensions.CreateCanonicalCondition(ProductCanonicalConditionCondition
                        .USED));

                // Exclude everything else.
                rootNode.AddChild(ProductDimensions.CreateCanonicalCondition()).AsExcludedUnit();

                // Make the mutate request, using the operations returned by the
                // ProductPartitionTree.
                AdGroupCriterionOperation[] mutateOperations = partitionTree.GetMutateOperations();

                if (mutateOperations.Length == 0)
                {
                    Console.WriteLine(
                        "Skipping the mutate call because the original tree and the current " +
                        "tree are logically identical.");
                }
                else
                {
                    adGroupCriterionService.mutate(mutateOperations);
                }

                // The request was successful, so create a new ProductPartitionTree based on the
                // updated state of the ad group.
                partitionTree = ProductPartitionTree.DownloadAdGroupTree(user, adGroupId);
                return partitionTree;
            }
        }

        /// <summary>
        /// Uploads an image.
        /// </summary>
        /// <param name="user">The AdWords user for which the image is uploaded.</param>
        /// <param name="url">The image URL.</param>
        /// <returns>The uploaded image.</returns>
        private static long UploadImage(AdWordsUser user, string url)
        {
            using (MediaService mediaService =
                (MediaService) user.GetService(AdWordsService.v201809.MediaService))
            {
                // Create the image.
                Image image = new Image
                {
                    data = MediaUtilities.GetAssetDataFromUrl(url, user.Config),
                    type = MediaMediaType.IMAGE
                };

                // Upload the image.
                Media[] result = mediaService.upload(new Media[]
                {
                    image
                });
                return result[0].mediaId;
            }
        }
    }
}
