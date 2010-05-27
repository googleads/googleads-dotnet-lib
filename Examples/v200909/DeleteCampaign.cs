// Copyright 2010, Google Inc. All Rights Reserved.
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

using com.google.api.adwords.lib;
using com.google.api.adwords.v200909;

using System;
using System.Collections.Generic;
using System.Text;

namespace com.google.api.adwords.examples.v200909 {
  /// <summary>
  /// This code example deletes a campaign by setting the status to 'DELETED'.
  /// To get campaigns, run GetAllCampaigns.cs.
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
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the CampaignService.
      CampaignService campaignService = (CampaignService) user.GetService(
          AdWordsService.v200909.CampaignService);

      long campaignId = long.Parse(_T("INSERT_CAMPAIGN_ID_HERE"));

      // Create campaign with DELETED status.
      Campaign campaign = new Campaign();
      campaign.id = campaignId;
      campaign.idSpecified = true;
      campaign.status = CampaignStatus.DELETED;
      campaign.statusSpecified = true;

      // Create operations.
      CampaignOperation operation = new CampaignOperation();
      operation.operand = campaign;
      operation.@operator = Operator.SET;
      operation.operatorSpecified = true;

      try {
        // Delete campaign.
        CampaignReturnValue result = campaignService.mutate(new CampaignOperation[] { operation });

        // Display campaigns.
        if (result != null && result.value != null && result.value.Length > 0) {
          foreach (Campaign temp in result.value) {
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
