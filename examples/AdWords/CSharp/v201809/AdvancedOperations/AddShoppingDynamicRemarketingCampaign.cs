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
using Google.Api.Ads.Common.Util;

using System;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201809
{
    /// <summary>
    /// This code example adds a Shopping dynamic remarketing campaign for the Display Network
    /// via the following steps:
    /// <list type="bullet">
    ///   <item>
    ///     <description>Creates a new Display Network campaign.</description>
    ///   </item>
    ///   <item>
    ///     <description>Links the campaign with Merchant Center.</description>
    ///   </item>
    ///   <item>
    ///     <description>Links the user list to the ad group.</description>
    ///   </item>
    ///   <item>
    ///     <description>Creates a responsive display ad to render the dynamic text.</description>
    ///   </item>
    /// </list>
    /// </summary>
    public class AddShoppingDynamicRemarketingCampaign : ExampleBase
    {
        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            AddShoppingDynamicRemarketingCampaign codeExample =
                new AddShoppingDynamicRemarketingCampaign();
            Console.WriteLine(codeExample.Description);
            try
            {
                // The ID of the merchant center account from which to source product feed data.
                long merchantId = long.Parse("INSERT_MERCHANT_CENTER_ID_HERE");

                // The ID of a shared budget to associate with the campaign.
                long budgetId = long.Parse("INSERT_BUDGET_ID_HERE");

                // The ID of a user list to target.
                long userListId = long.Parse("INSERT_USER_LIST_ID_HERE");
                codeExample.Run(new AdWordsUser(), merchantId, budgetId, userListId);
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
                return "This code example adds a Shopping dynamic remarketing campaign for the " +
                    "Display Network via the following steps:\n" +
                    "*  Creates a new Display Network campaign.\n" +
                    "*  Links the campaign with Merchant Center.\n" +
                    "*  Links the user list to the ad group.\n" +
                    "*  Creates a responsive display ad to render the dynamic text.";
            }
        }

        /// <summary>
        /// Runs the code example.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="merchantId">The ID of the merchant center account from which to source
        /// product feed data.</param>
        /// <param name="budgetId">The ID of a shared budget to associate with the campaign.</param>
        /// <param name="userListId">The ID of a user list to target.</param>
        public void Run(AdWordsUser user, long merchantId, long budgetId, long userListId)
        {
            try
            {
                Campaign campaign = CreateCampaign(user, merchantId, budgetId);
                Console.WriteLine("Campaign with name '{0}' and ID {1} was added.", campaign.name,
                    campaign.id);

                AdGroup adGroup = CreateAdGroup(user, campaign);
                Console.WriteLine("Ad group with name '{0}' and ID {1} was added.", adGroup.name,
                    adGroup.id);

                AdGroupAd adGroupAd = CreateAd(user, adGroup);
                Console.WriteLine("Responsive display ad with ID {0} was added.", adGroupAd.ad.id);

                AttachUserList(user, adGroup, userListId);
                Console.WriteLine("User list with ID {0} was attached to ad group with ID {1}.",
                    userListId, adGroup.id);
            }
            catch (Exception e)
            {
                throw new System.ApplicationException(
                    "Failed to create Shopping dynamic remarketing " +
                    "campaign for the Display Network.", e);
            }
        }

        /// <summary>
        /// Creates a Shopping dynamic remarketing campaign object (not including ad group level and
        /// below). This creates a Display campaign with the merchant center feed attached.
        /// Merchant Center is used for the product information in combination with a user list
        /// which contains hits with <code>ecomm_prodid</code> specified. See
        /// <a href="https://developers.google.com/adwords-remarketing-tag/parameters#retail">
        /// the guide</a> for more detail.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="merchantId">The ID of the Merchant Center account.</param>
        /// <param name="budgetId">The ID of the budget to use for the campaign.</param>
        /// <returns>The campaign that was created.</returns>
        private static Campaign CreateCampaign(AdWordsUser user, long merchantId, long budgetId)
        {
            using (CampaignService campaignService =
                (CampaignService) user.GetService(AdWordsService.v201809.CampaignService))
            {
                Campaign campaign = new Campaign
                {
                    name = "Shopping campaign #" + ExampleUtilities.GetRandomString(),
                    // Dynamic remarketing campaigns are only available on the Google Display
                    // Network.
                    advertisingChannelType = AdvertisingChannelType.DISPLAY,
                    status = CampaignStatus.PAUSED
                };

                Budget budget = new Budget
                {
                    budgetId = budgetId
                };
                campaign.budget = budget;

                // This example uses a Manual CPC bidding strategy, but you should select the
                // strategy that best aligns with your sales goals. More details here:
                //   https://support.google.com/adwords/answer/2472725
                BiddingStrategyConfiguration biddingStrategyConfiguration =
                    new BiddingStrategyConfiguration
                    {
                        biddingStrategyType = BiddingStrategyType.MANUAL_CPC
                    };
                campaign.biddingStrategyConfiguration = biddingStrategyConfiguration;

                ShoppingSetting setting = new ShoppingSetting
                {
                    // Campaigns with numerically higher priorities take precedence over those with
                    // lower priorities.
                    campaignPriority = 0,

                    // Set the Merchant Center account ID from which to source products.
                    merchantId = merchantId,

                    // Display Network campaigns do not support partition by country. The only
                    // supported value is "ZZ". This signals that products from all countries are
                    // available in the campaign. The actual products which serve are based on the
                    // products tagged in the user list entry.
                    salesCountry = "ZZ",

                    // Optional: Enable local inventory ads (items for sale in physical stores.)
                    enableLocal = true
                };

                campaign.settings = new Setting[]
                {
                    setting
                };

                CampaignOperation op = new CampaignOperation
                {
                    operand = campaign,
                    @operator = Operator.ADD
                };

                CampaignReturnValue result = campaignService.mutate(new CampaignOperation[]
                {
                    op
                });
                return result.value[0];
            }
        }

        /// <summary>
        /// Creates an ad group in the specified campaign.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="campaign">The campaign to which the ad group should be attached.</param>
        /// <returns>The ad group that was created.</returns>
        private static AdGroup CreateAdGroup(AdWordsUser user, Campaign campaign)
        {
            using (AdGroupService adGroupService =
                (AdGroupService) user.GetService(AdWordsService.v201809.AdGroupService))
            {
                AdGroup group = new AdGroup
                {
                    name = "Dynamic remarketing ad group",
                    campaignId = campaign.id,
                    status = AdGroupStatus.ENABLED
                };

                AdGroupOperation op = new AdGroupOperation
                {
                    operand = group,
                    @operator = Operator.ADD
                };
                AdGroupReturnValue result = adGroupService.mutate(new AdGroupOperation[]
                {
                    op
                });
                return result.value[0];
            }
        }

        /// <summary>
        /// Attach a user list to an ad group. The user list provides positive targeting and feed
        /// information to drive the dynamic content of the ad.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="adGroup">The ad group which will have the user list attached.</param>
        /// <param name="userListId">The user list to use for targeting and dynamic content.</param>
        /// <remarks>User lists must be attached at the ad group level for positive targeting in
        /// Shopping dynamic remarketing campaigns.</remarks>
        private static void AttachUserList(AdWordsUser user, AdGroup adGroup, long userListId)
        {
            using (AdGroupCriterionService adGroupCriterionService =
                (AdGroupCriterionService) user.GetService(AdWordsService.v201809
                    .AdGroupCriterionService))
            {
                CriterionUserList userList = new CriterionUserList
                {
                    userListId = userListId
                };
                BiddableAdGroupCriterion adGroupCriterion = new BiddableAdGroupCriterion
                {
                    criterion = userList,
                    adGroupId = adGroup.id
                };

                AdGroupCriterionOperation op = new AdGroupCriterionOperation
                {
                    operand = adGroupCriterion,
                    @operator = Operator.ADD
                };

                adGroupCriterionService.mutate(new AdGroupCriterionOperation[]
                {
                    op
                });
            }
        }

        /// <summary>
        /// Creates an ad for serving dynamic content in a remarketing campaign.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="adGroup">The ad group under which to create the ad.</param>
        /// <returns>The ad that was created.</returns>
        private static AdGroupAd CreateAd(AdWordsUser user, AdGroup adGroup)
        {
            using (AdGroupAdService adService =
                (AdGroupAdService) user.GetService(AdWordsService.v201809.AdGroupAdService))
            {
                ResponsiveDisplayAd ad = new ResponsiveDisplayAd
                {
                    // This ad format does not allow the creation of an image using the
                    // Image.data field. An image must first be created using the MediaService,
                    // and Image.mediaId must be populated when creating the ad.
                    marketingImage = UploadImage(user, "https://goo.gl/3b9Wfh"),

                    shortHeadline = "Travel",
                    longHeadline = "Travel the World",
                    description = "Take to the air!",
                    businessName = "Interplanetary Cruises",
                    finalUrls = new string[]
                    {
                        "http://www.example.com/"
                    },

                    // Optional: Call to action text.
                    // Valid texts: https://support.google.com/adwords/answer/7005917
                    callToActionText = "Apply Now",

                    // Optional: Set dynamic display ad settings, composed of landscape logo
                    // image, promotion text, and price prefix.
                    dynamicDisplayAdSettings = CreateDynamicDisplayAdSettings(user),

                    // Optional: Create a logo image and set it to the ad.
                    logoImage = UploadImage(user, "https://goo.gl/mtt54n"),

                    // Optional: Create a square marketing image and set it to the ad.
                    squareMarketingImage = UploadImage(user, "https://goo.gl/mtt54n")
                };

                // Whitelisted accounts only: Set color settings using hexadecimal values.
                // Set allowFlexibleColor to false if you want your ads to render by always
                // using your colors strictly.
                // ad.mainColor = "#0000ff";
                // ad.accentColor = "#ffff00";
                // ad.allowFlexibleColor = false;

                // Whitelisted accounts only: Set the format setting that the ad will be
                // served in.
                // ad.formatSetting = DisplayAdFormatSetting.NON_NATIVE;

                AdGroupAd adGroupAd = new AdGroupAd
                {
                    ad = ad,
                    adGroupId = adGroup.id
                };

                AdGroupAdOperation op = new AdGroupAdOperation
                {
                    operand = adGroupAd,
                    @operator = Operator.ADD
                };

                AdGroupAdReturnValue result = adService.mutate(new AdGroupAdOperation[]
                {
                    op
                });
                return result.value[0];
            }
        }

        /// <summary>
        /// Creates the additional content (images, promo text, etc.) supported by dynamic ads.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <returns>The DynamicSettings object to be used.</returns>
        private static DynamicSettings CreateDynamicDisplayAdSettings(AdWordsUser user)
        {
            Image logo = UploadImage(user, "https://goo.gl/dEvQeF");

            DynamicSettings dynamicSettings = new DynamicSettings
            {
                landscapeLogoImage = logo,
                pricePrefix = "as low as",
                promoText = "Free shipping!"
            };
            return dynamicSettings;
        }

        /// <summary>
        /// Uploads the image from the specified <paramref name="url"/>.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="url">The image URL.</param>
        /// <returns>ID of the uploaded image.</returns>
        private static Image UploadImage(AdWordsUser user, string url)
        {
            using (MediaService mediaService =
                (MediaService) user.GetService(AdWordsService.v201809.MediaService))
            {
                // Create the image.
                Image image = new Image()
                {
                    data = MediaUtilities.GetAssetDataFromUrl(url, user.Config),
                    type = MediaMediaType.IMAGE
                };

                // Upload the image and return the ID.
                return (Image) mediaService.upload(new Media[]
                {
                    image
                })[0];
            }
        }
    }
}
