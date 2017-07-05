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
using Google.Api.Ads.AdWords.Util.Shopping.v201705;
using Google.Api.Ads.AdWords.v201705;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201705 {

  /// <summary>
  /// This code example adds a Shopping campaign.
  /// </summary>
  public class AddShoppingCampaign : ExampleBase {

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example adds a Shopping campaign.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      AddShoppingCampaign codeExample = new AddShoppingCampaign();
      Console.WriteLine(codeExample.Description);
      try {
        long budgetId = long.Parse("INSERT_BUDGET_ID_HERE");
        long merchantId = long.Parse("INSERT_MERCHANT_ID_HERE");
        bool createDefaultPartition = false;
        codeExample.Run(new AdWordsUser(), budgetId, merchantId, createDefaultPartition);
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
    /// <param name="createDefaultPartition">If set to true, a default
    /// partition will be created. If running the AddProductPartition.cs
    /// example right after this example, make sure this stays set to
    /// false.</param>
    public void Run(AdWordsUser user, long budgetId, long merchantId,
        bool createDefaultPartition) {
      // Get the required services.
      CampaignService campaignService = (CampaignService) user.GetService(
          AdWordsService.v201705.CampaignService);
      AdGroupService adGroupService = (AdGroupService) user.GetService(
          AdWordsService.v201705.AdGroupService);
      AdGroupAdService adGroupAdService = (AdGroupAdService) user.GetService(
          AdWordsService.v201705.AdGroupAdService);

      try {
        Campaign campaign = CreateCampaign(budgetId, merchantId, campaignService);
        Console.WriteLine("Campaign with name '{0}' and ID '{1}' was added.", campaign.name,
              campaign.id);

        AdGroup adGroup = CreateAdGroup(adGroupService, campaign);
        Console.WriteLine("Ad group with name '{0}' and ID '{1}' was added.", adGroup.name,
              adGroup.id);

        AdGroupAd adGroupAd = CreateProductAd(adGroupAdService, adGroup);
        Console.WriteLine("Product ad with ID {0}' was added.", adGroupAd.ad.id);

        if (createDefaultPartition) {
          CreateDefaultPartitionTree(user, adGroup.id);
        }
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to create shopping campaign.", e);
      }
    }

    /// <summary>
    /// Creates the default partition.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="adGroupId">The ad group ID.</param>
    void CreateDefaultPartitionTree(AdWordsUser user, long adGroupId) {
      // Get the AdGroupCriterionService.
      AdGroupCriterionService adGroupCriterionService =
          (AdGroupCriterionService) user.GetService(
              AdWordsService.v201705.AdGroupCriterionService);

      // Build a new ProductPartitionTree using an empty set of criteria.
      ProductPartitionTree partitionTree =
          ProductPartitionTree.CreateAdGroupTree(adGroupId, new List<AdGroupCriterion>());
      partitionTree.Root.AsBiddableUnit().CpcBid = 1000000;

      try {
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

        Console.WriteLine("Final tree: {0}", partitionTree);
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to set shopping product partition.", e);
      }
    }

    /// <summary>
    /// Creates the Product Ad.
    /// </summary>
    /// <param name="adGroupAdService">The AdGroupAdService instance.</param>
    /// <param name="adGroup">The ad group.</param>
    /// <returns>The Product Ad.</returns>
    private static AdGroupAd CreateProductAd(AdGroupAdService adGroupAdService, AdGroup adGroup) {
      // Create product ad.
      ProductAd productAd = new ProductAd();

      // Create ad group ad.
      AdGroupAd adGroupAd = new AdGroupAd();
      adGroupAd.adGroupId = adGroup.id;
      adGroupAd.ad = productAd;

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
    /// Creates the ad group in a Shopping campaign.
    /// </summary>
    /// <param name="adGroupService">The AdGroupService instance.</param>
    /// <param name="campaign">The Shopping campaign.</param>
    /// <returns>The ad group.</returns>
    private static AdGroup CreateAdGroup(AdGroupService adGroupService, Campaign campaign) {
      // Create ad group.
      AdGroup adGroup = new AdGroup();
      adGroup.campaignId = campaign.id;
      adGroup.name = "Ad Group #" + ExampleUtilities.GetRandomString();

      // Create operation.
      AdGroupOperation operation = new AdGroupOperation();
      operation.operand = adGroup;
      operation.@operator = Operator.ADD;

      // Make the mutate request.
      AdGroupReturnValue retval = adGroupService.mutate(new AdGroupOperation[] { operation });
      return retval.value[0];
    }

    /// <summary>
    /// Creates the shopping campaign.
    /// </summary>
    /// <param name="budgetId">The budget id.</param>
    /// <param name="merchantId">The Merchant Center id.</param>
    /// <param name="campaignService">The CampaignService instance.</param>
    /// <returns>The Shopping campaign.</returns>
    private static Campaign CreateCampaign(long budgetId, long merchantId,
        CampaignService campaignService) {
      // Create campaign.
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
  }
}
