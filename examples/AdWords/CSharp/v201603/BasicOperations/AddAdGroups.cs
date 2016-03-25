// Copyright 2016, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdWords.v201603;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201603 {
  /// <summary>
  /// This code example illustrates how to create ad groups. To create
  /// campaigns, run AddCampaigns.cs.
  /// </summary>
  public class AddAdGroups : ExampleBase {
    /// <summary>
    /// Number of items being added / updated in this code example.
    /// </summary>
    const int NUM_ITEMS = 5;

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      AddAdGroups codeExample = new AddAdGroups();
      Console.WriteLine(codeExample.Description);
      try {
        long campaignId = long.Parse("INSERT_CAMPAIGN_ID_HERE");
        codeExample.Run(new AdWordsUser(), campaignId);
      } catch (Exception e) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(e));
      }
    }

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example illustrates how to create ad groups. To create campaigns, " +
            "run AddCampaigns.cs";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="campaignId">Id of the campaign to which ad groups are
    /// added.</param>
    public void Run(AdWordsUser user, long campaignId) {
      // Get the AdGroupService.
      AdGroupService adGroupService =
          (AdGroupService) user.GetService(AdWordsService.v201603.AdGroupService);

      List<AdGroupOperation> operations = new List<AdGroupOperation>();

      for (int i = 0; i < NUM_ITEMS; i++) {
        // Create the ad group.
        AdGroup adGroup = new AdGroup();
        adGroup.name = string.Format("Earth to Mars Cruises #{0}",
            ExampleUtilities.GetRandomString());
        adGroup.status = AdGroupStatus.ENABLED;
        adGroup.campaignId = campaignId;

        // Set the ad group bids.
        BiddingStrategyConfiguration biddingConfig = new BiddingStrategyConfiguration();

        CpcBid cpcBid = new CpcBid();
        cpcBid.bid = new Money();
        cpcBid.bid.microAmount = 10000000;

        biddingConfig.bids = new Bids[] {cpcBid};

        adGroup.biddingStrategyConfiguration = biddingConfig;

        // Optional: Set targeting restrictions.
        // Depending on the criterionTypeGroup value, most TargetingSettingDetail
        // only affect Display campaigns. However, the USER_INTEREST_AND_LIST value
        // works for RLSA campaigns - Search campaigns targeting using a
        // remarketing list.
        TargetingSetting targetingSetting = new TargetingSetting();

        // Restricting to serve ads that match your ad group placements.
        // This is equivalent to choosing "Target and bid" in the UI.
        TargetingSettingDetail placementDetail = new TargetingSettingDetail();
        placementDetail.criterionTypeGroup = CriterionTypeGroup.PLACEMENT;
        placementDetail.targetAll = false;

        // Using your ad group verticals only for bidding. This is equivalent
        // to choosing "Bid only" in the UI.
        TargetingSettingDetail verticalDetail = new TargetingSettingDetail();
        verticalDetail.criterionTypeGroup = CriterionTypeGroup.VERTICAL;
        verticalDetail.targetAll = true;

        targetingSetting.details = new TargetingSettingDetail[] {placementDetail, verticalDetail};

        adGroup.settings = new Setting[] {targetingSetting};

        // Create the operation.
        AdGroupOperation operation = new AdGroupOperation();
        operation.@operator = Operator.ADD;
        operation.operand = adGroup;

        operations.Add(operation);
      }

      try {
        // Create the ad group.
        AdGroupReturnValue retVal = adGroupService.mutate(operations.ToArray());

        // Display the results.
        if (retVal != null && retVal.value != null && retVal.value.Length > 0) {
          foreach (AdGroup newAdGroup in retVal.value) {
            Console.WriteLine("Ad group with id = '{0}' and name = '{1}' was created.",
                newAdGroup.id, newAdGroup.name);
          }
        } else {
          Console.WriteLine("No ad groups were created.");
        }
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to create ad groups.", e);
      }
    }
  }
}
