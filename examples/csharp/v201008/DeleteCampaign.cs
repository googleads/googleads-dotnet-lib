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
using Google.Api.Ads.AdWords.v201008;

using System;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201008 {
  /// <summary>
  /// This code example deletes a campaign by setting the status to 'DELETED'.
  /// To get campaigns, run GetAllCampaigns.cs.
  ///
  /// Tags: CampaignService.mutate
  /// </summary>
  class DeleteCampaign : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example deletes a campaign by setting the status to 'DELETED'. " +
            "To get campaigns, run GetAllCampaigns.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new DeleteCampaign();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the CampaignService.
      CampaignService campaignService = (CampaignService) user.GetService(
          AdWordsService.v201008.CampaignService);

      long campaignId = long.Parse(_T("INSERT_CAMPAIGN_ID_HERE"));

      // Create campaign with DELETED status.
      Campaign campaign = new Campaign();
      campaign.id = campaignId;
      campaign.status = CampaignStatus.DELETED;

      // Create operations.
      CampaignOperation operation = new CampaignOperation();
      operation.operand = campaign;
      operation.@operator = Operator.SET;

      try {
        // Delete campaign.
        CampaignReturnValue retVal = campaignService.mutate(new CampaignOperation[] {operation});

        // Display campaigns.
        if (retVal != null && retVal.value != null && retVal.value.Length > 0) {
          foreach (Campaign temp in retVal.value) {
            Console.WriteLine("Campaign with name = \"{0}\" and id = \"{1}\" was deleted.",
               temp.name, temp.id);
          }
        } else {
          Console.WriteLine("No campaigns were deleted.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to delete campaigns. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
