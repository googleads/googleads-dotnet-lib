// Copyright 2011, Google Inc. All Rights Reserved.
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

// Author: api.anash@gmail.com (Anash P. Oommen)

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201109;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201109 {
  /// <summary>
  /// This code example deletes a campaign by setting the status to 'DELETED'.
  /// To get campaigns, run GetCampaigns.cs.
  ///
  /// Tags: CampaignService.mutate
  /// </summary>
  class DeleteCampaign : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      ExampleBase codeExample = new DeleteCampaign();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser(), codeExample.GetParameters(), Console.Out);
    }

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example deletes a campaign by setting the status to 'DELETED'. " +
            "To get campaigns, run GetCampaigns.cs.";
      }
    }

    /// <summary>
    /// Gets the list of parameter names required to run this code example.
    /// </summary>
    /// <returns>
    /// A list of parameter names for this code example.
    /// </returns>
    public override string[] GetParameterNames() {
      return new string[] {"CAMPAIGN_ID"};
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="parameters">The parameters for running the code
    /// example.</param>
    /// <param name="writer">The stream writer to which script output should be
    /// written.</param>
    public override void Run(AdWordsUser user, Dictionary<string, string> parameters,
        TextWriter writer) {
      // Get the CampaignService.
      CampaignService campaignService = (CampaignService) user.GetService(
          AdWordsService.v201109.CampaignService);

      long campaignId = long.Parse(parameters["CAMPAIGN_ID"]);

      // Create campaign with DELETED status.
      Campaign campaign = new Campaign();
      campaign.id = campaignId;

      // When deleting a campaign, rename it to avoid name collisions with new
      // campaigns.
      campaign.name = "Deleted Campaign - " + ExampleUtilities.GetTimeStamp();
      campaign.status = CampaignStatus.DELETED;

      // Create the operation.
      CampaignOperation operation = new CampaignOperation();
      operation.operand = campaign;
      operation.@operator = Operator.SET;

      try {
        // Delete the campaign.
        CampaignReturnValue retVal = campaignService.mutate(new CampaignOperation[] {operation});

        // Display the results.
        if (retVal != null && retVal.value != null && retVal.value.Length > 0) {
          Campaign deletedCampaign = retVal.value[0];
          writer.WriteLine("Campaign with id = \"{0}\" was renamed to \"{1}\" and deleted.",
              deletedCampaign.id, deletedCampaign.name);
        } else {
          writer.WriteLine("No campaigns were deleted.");
        }
      } catch (Exception ex) {
        writer.WriteLine("Failed to delete campaigns. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
