// Copyright 2013, Google Inc. All Rights Reserved.
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

// Author: thagikura@google.com (Takeshi Hagikura)

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201302;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201302 {
  /// <summary>
  /// This code example sets the enhanced bit in a given campaign. To get campaigns, run
  /// GetCampaigns.cs.
  ///
  /// Tags: CampaignService.mutate
  /// </summary>
  public class SetCampaignEnhanced : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SetCampaignEnhanced codeExample = new SetCampaignEnhanced();
      Console.WriteLine(codeExample.Description);
      try {
        long campaignId = long.Parse("INSERT_CAMPAIGN_ID_HERE");
        codeExample.Run(new AdWordsUser(), campaignId);
      } catch (Exception ex) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(ex));
      }
    }

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example sets the enhanced bit in a given campaign using the forward " +
            "compatibility map attribute. To get campaigns, run GetCampaigns.cs.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="adGroupId">Id of the campaign to be enhanced.</param>
    public void Run(AdWordsUser user, long campaignId) {
      // Get the CampaignService.
      CampaignService campaignService = (CampaignService) user.GetService(
          AdWordsService.v201302.CampaignService);

      // Campaign to be updated with the enhanced value.
      // Note: After setting the enhanced value to true, setting it back to false
      // will generate an ApiError.
      Campaign campaign = new Campaign();
      campaign.id = campaignId;
      campaign.enhanced = true;

      // Create operation.
      CampaignOperation operation = new CampaignOperation();
      operation.@operator = Operator.SET;
      operation.operand = campaign;

      try {
        // Change campaign.
        CampaignReturnValue result = campaignService.mutate(new CampaignOperation[] { operation });
        // Display changed campaign.
        if (result.value != null) {
          Campaign updatedCampaign = result.value[0];

          Console.WriteLine("Campaign with ID {0} has been set enhanced to '{1}'.",
                updatedCampaign.id, updatedCampaign.enhanced);
        } else {
          Console.WriteLine("No campaigns were enhanced.");
        }
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to enhance campaign.", ex);
      }
    }
  }
}
