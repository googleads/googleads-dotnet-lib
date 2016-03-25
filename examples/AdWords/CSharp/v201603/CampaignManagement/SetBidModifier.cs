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
  /// This code example sets a bid modifier for the mobile platform on given
  /// campaign. The campaign must be an enhanced type of campaign. To get
  /// campaigns, run GetCampaigns.cs. To enhance a campaign, run
  /// SetCampaignEnhanced.cs.
  /// </summary>
  public class SetBidModifier : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SetBidModifier codeExample = new SetBidModifier();
      Console.WriteLine(codeExample.Description);
      try {
        long campaignId = long.Parse("INSERT_CAMPAIGN_ID_HERE");
        double bidModifier = double.Parse("INSERT_BID_MODIFIER_HERE");
        codeExample.Run(new AdWordsUser(), campaignId, bidModifier);
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
        return "This code example sets a bid modifier for the mobile platform on given " +
            "campaign. The campaign must be an enhanced type of campaign. To get campaigns, " +
            "run GetCampaigns.cs. To enhance a campaign, run SetCampaignEnhanced.cs.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="adGroupId">Id of the campaign whose bid should be modified.
    /// </param>
    /// <param name="bidModifier">The bid modifier.</param>
    public void Run(AdWordsUser user, long campaignId, double bidModifier) {
      // Get the CampaignCriterionService.
      CampaignCriterionService campaignCriterionService =
          (CampaignCriterionService) user.GetService(
              AdWordsService.v201603.CampaignCriterionService);

      // Create mobile platform. The ID can be found in the documentation.
      // https://developers.google.com/adwords/api/docs/appendix/platforms
      Platform mobile = new Platform();
      mobile.id = 30001;

      // Create criterion with modified bid.
      CampaignCriterion criterion = new CampaignCriterion();
      criterion.campaignId = campaignId;
      criterion.criterion = mobile;
      criterion.bidModifier = bidModifier;

      // Create SET operation.
      CampaignCriterionOperation operation = new CampaignCriterionOperation();
      operation.@operator = Operator.SET;
      operation.operand = criterion;

      try {
        // Update campaign criteria.
        CampaignCriterionReturnValue result = campaignCriterionService.mutate(
            new CampaignCriterionOperation[] {operation});

        // Display campaign criteria.
        if (result.value != null) {
          foreach (CampaignCriterion newCriterion in result.value) {
            Console.WriteLine("Campaign criterion with campaign id '{0}', criterion id '{1}', " +
                "and type '{2}' was modified with bid {3:F2}.", newCriterion.campaignId,
                newCriterion.criterion.id, newCriterion.criterion.type, newCriterion.bidModifier);
          }
        } else {
          Console.WriteLine("No campaign criteria were modified.");
        }
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to set bid modifier for campaign.", e);
      }
    }
  }
}
