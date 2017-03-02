// Copyright 2017, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdWords.Util.Shopping.v201702;
using Google.Api.Ads.AdWords.v201702;
using Google.Api.Ads.Common.Util;

using System;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201702 {

  /// <summary>
  /// This code example adds a Shopping campaign for Showcase ads.
  /// </summary>
  public class AddShoppingCampaignForShowcaseAds : ExampleBase {

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example adds a Shopping campaign for Showcase ads.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      AddShoppingCampaignForShowcaseAds codeExample = new AddShoppingCampaignForShowcaseAds();
      Console.WriteLine(codeExample.Description);
      try {
        long budgetId = long.Parse("INSERT_BUDGET_ID_HERE");
        long merchantId = long.Parse("INSERT_MERCHANT_ID_HERE");
        codeExample.Run(new AdWordsUser(), budgetId, merchantId);
      } catch (Exception e) {
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
    public void Run(AdWordsUser user, long budgetId, long merchantId) {
      try {
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
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to create shopping campaign for " +
            "showcase ads.", e);
      }
    }

    /// <summary>
    /// Creates the Shopping campaign.
    /// </summary>
    /// <param name="user">The AdWords user for which the campaign is created.</param>
    /// <param name="budgetId">The budget ID.</param>
    /// <param name="merchantId">The Merchant Center ID.</param>
    /// <returns>The newly created Shopping campaign.</returns>
    private static Campaign CreateCampaign(AdWordsUser user, long budgetId, long merchantId) {
      CampaignService campaignService = (CampaignService) user.GetService(
          AdWordsService.v201702.CampaignService);
      // Create the campaign.
      Campaign campaign = new Campaign();
      campaign.name = "Shopping campaign #" + ExampleUtilities.GetRandomString();

      // The advertisingChannelType is what makes this a Shopping campaign.
      campaign.advertisingChannelType = AdvertisingChannelType.SHOPPING;

      // Recommendation: Set the campaign to PAUSED when creating it to prevent
      // the ads from immediately serving. Set to ENABLED once you've added
      // targeting and the ads are ready to serve.
      campaign.status = CampaignStatus.PAUSED;

      // Set shared budget (required).
      campaign.budget = new Budget();
      campaign.budget.budgetId = budgetId;

      // Set bidding strategy (required).
      BiddingStrategyConfiguration biddingStrategyConfiguration =
          new BiddingStrategyConfiguration();
      biddingStrategyConfiguration.biddingStrategyType = BiddingStrategyType.MANUAL_CPC;

      campaign.biddingStrategyConfiguration = biddingStrategyConfiguration;

      // All Shopping campaigns need a ShoppingSetting.
      ShoppingSetting shoppingSetting = new ShoppingSetting();
      shoppingSetting.salesCountry = "US";
      shoppingSetting.campaignPriority = 0;
      shoppingSetting.merchantId = merchantId;

      // Set to "true" to enable Local Inventory Ads in your campaign.
      shoppingSetting.enableLocal = true;
      campaign.settings = new Setting[] { shoppingSetting };

      // Create operation.
      CampaignOperation campaignOperation = new CampaignOperation();
      campaignOperation.operand = campaign;
      campaignOperation.@operator = Operator.ADD;

      // Make the mutate request.
      CampaignReturnValue retval = campaignService.mutate(
          new CampaignOperation[] { campaignOperation });

      return retval.value[0];
    }

    /// <summary>
    /// Creates the ad group in a Shopping campaign.
    /// </summary>
    /// <param name="user">The AdWords user for which the ad group is created.</param>
    /// <param name="campaign">The Shopping campaign.</param>
    /// <returns>The newly created ad group.</returns>
    private static AdGroup CreateAdGroup(AdWordsUser user, Campaign campaign) {
      AdGroupService adGroupService = (AdGroupService) user.GetService(
        AdWordsService.v201702.AdGroupService);
      // Create ad group.
      AdGroup adGroup = new AdGroup();
      adGroup.campaignId = campaign.id;
      adGroup.name = "Ad Group #" + ExampleUtilities.GetRandomString();

      // Required: Set the ad group type to SHOPPING_SHOWCASE_ADS.
      adGroup.adGroupType = AdGroupType.SHOPPING_SHOWCASE_ADS;

      // Required: Set the ad group's bidding strategy configuration.
      BiddingStrategyConfiguration biddingConfiguration = new BiddingStrategyConfiguration();

      // Showcase ads require either ManualCpc or EnhancedCpc.
      biddingConfiguration.biddingStrategyType = BiddingStrategyType.MANUAL_CPC;

      // Optional: Set the bids.
      biddingConfiguration.bids = new Bids[] {
        new CpcBid() {
          bid = new Money() {
            microAmount = 100000
          }
        }
      };

      adGroup.biddingStrategyConfiguration = biddingConfiguration;

      // Create the operation.
      AdGroupOperation operation = new AdGroupOperation();
      operation.operand = adGroup;
      operation.@operator = Operator.ADD;

      // Make the mutate request.
      AdGroupReturnValue retval = adGroupService.mutate(new AdGroupOperation[] { operation });
      return retval.value[0];
    }

    /// <summary>
    /// Creates the Showcase ad.
    /// </summary>
    /// <param name="user">The AdWords user for which the ad is created.</param>
    /// <param name="adGroup">The ad group in which the ad is created.</param>
    /// <returns>The newly created Showcase ad.</returns>
    private static AdGroupAd CreateShowcaseAd(AdWordsUser user, AdGroup adGroup) {
      AdGroupAdService adGroupAdService = (AdGroupAdService) user.GetService(
        AdWordsService.v201702.AdGroupAdService);
      // Create the Showcase ad.
      ShowcaseAd showcaseAd = new ShowcaseAd();

      // Required: set the ad's name, final URLs and display URL.
      showcaseAd.name = "Showcase ad " + ExampleUtilities.GetShortRandomString();
      showcaseAd.finalUrls = new string[] { "http://example.com/showcase" };
      showcaseAd.displayUrl = "example.com";

      // Required: Set the ad's expanded image.
      Image expandedImage = new Image();
      expandedImage.mediaId = UploadImage(user, "https://goo.gl/IfVlpF");
      showcaseAd.expandedImage = expandedImage;

      // Optional: Set the collapsed image.
      Image collapsedImage = new Image();
      collapsedImage.mediaId = UploadImage(user, "https://goo.gl/NqTxAE");
      showcaseAd.collapsedImage = collapsedImage;

      // Create ad group ad.
      AdGroupAd adGroupAd = new AdGroupAd();
      adGroupAd.adGroupId = adGroup.id;
      adGroupAd.ad = showcaseAd;

      // Create operation.
      AdGroupAdOperation operation = new AdGroupAdOperation();
      operation.operand = adGroupAd;
      operation.@operator = Operator.ADD;

      // Make the mutate request.
      AdGroupAdReturnValue retval = adGroupAdService.mutate(
        new AdGroupAdOperation[] { operation });

      return retval.value[0];
    }

    /// <summary>
    /// Creates a product partition tree.
    /// </summary>
    /// <param name="user">The AdWords user for which the product partition is created.</param>
    /// <param name="adGroupId">Ad group ID.</param>
    /// <returns>The product partition.</returns>
    private static ProductPartitionTree CreateProductPartition(AdWordsUser user, long adGroupId) {
      AdGroupCriterionService adGroupCriterionService =
        (AdGroupCriterionService) user.GetService(
          AdWordsService.v201702.AdGroupCriterionService);

      // Build a new ProductPartitionTree using the ad group's current set of criteria.
      ProductPartitionTree partitionTree =
        ProductPartitionTree.DownloadAdGroupTree(user, adGroupId);

      Console.WriteLine("Original tree: {0}", partitionTree);

      // Clear out any existing criteria.
      ProductPartitionNode rootNode = partitionTree.Root.RemoveAllChildren();

      // Make the root node a subdivision.
      rootNode = rootNode.AsSubdivision();

      // Add a unit node for condition = NEW to include it.
      rootNode.AddChild(ProductDimensions.CreateCanonicalCondition(
          ProductCanonicalConditionCondition.NEW));

      // Add a unit node for condition = USED to include it.
      rootNode.AddChild(ProductDimensions.CreateCanonicalCondition(
          ProductCanonicalConditionCondition.USED));

      // Exclude everything else.
      rootNode.AddChild(ProductDimensions.CreateCanonicalCondition()).AsExcludedUnit();

      // Make the mutate request, using the operations returned by the ProductPartitionTree.
      AdGroupCriterionOperation[] mutateOperations = partitionTree.GetMutateOperations();

      if (mutateOperations.Length == 0) {
        Console.WriteLine("Skipping the mutate call because the original tree and the current " +
                          "tree are logically identical.");
      } else {
        adGroupCriterionService.mutate(mutateOperations);
      }

      // The request was successful, so create a new ProductPartitionTree based on the updated
      // state of the ad group.
      partitionTree = ProductPartitionTree.DownloadAdGroupTree(user, adGroupId);
      return partitionTree;
    }

    /// <summary>
    /// Uploads an image.
    /// </summary>
    /// <param name="user">The AdWords user for which the image is uploaded.</param>
    /// <param name="url">The image URL.</param>
    /// <returns>The uploaded image.</returns>
    private static long UploadImage(AdWordsUser user, string url) {
      MediaService mediaService = (MediaService) user.GetService(
        AdWordsService.v201702.MediaService);

      // Create the image.
      Image image = new Image();
      image.data = MediaUtilities.GetAssetDataFromUrl(url);
      image.type = MediaMediaType.IMAGE;

      // Upload the image.
      Media[] result = mediaService.upload(new Media[] { image });
      return result[0].mediaId;
    }
  }
}
