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
  /// This code example updates a campaign. To get campaigns, run
  /// GetCampaigns.cs.
  /// </summary>
  public class UpdateCampaign : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      UpdateCampaign codeExample = new UpdateCampaign();
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
        return "This code example updates a campaign. To get campaigns, run GetCampaigns.cs.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="campaignId">Id of the campaign to be updated.</param>
    public void Run(AdWordsUser user, long campaignId) {
      // Get the CampaignService.
      CampaignService campaignService =
          (CampaignService)user.GetService(AdWordsService.v201603.CampaignService);

      // Create the campaign.
      Campaign campaign = new Campaign();
      campaign.id = campaignId;
      campaign.status = CampaignStatus.PAUSED;

      // Create the operation.
      CampaignOperation operation = new CampaignOperation();
      operation.@operator = Operator.SET;
      operation.operand = campaign;

      try {
        // Update the campaign.
        CampaignReturnValue retVal = campaignService.mutate((new CampaignOperation[] {operation}));

        // Display the results.
        if (retVal != null && retVal.value != null && retVal.value.Length > 0) {
          Campaign updatedCampaign = retVal.value[0];
          Console.WriteLine("Campaign with name = '{0}' and id = '{1}' was updated.",
              updatedCampaign.name, updatedCampaign.id);
        } else {
          Console.WriteLine("No campaigns were updated.");
        }
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to update campaign.", e);
      }
    }
  }
}
