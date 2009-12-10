// Copyright 2009, Google Inc. All Rights Reserved.
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
using System.IO;
using System.Net;

namespace com.google.api.adwords.samples.v200909 {
  /// <summary>
  /// This example updates a campaign. To get campaigns, run GetAllCampaigns.cs.
  /// </summary>
  class UpdateCampaign : SampleBase {
    /// <summary>
    /// Returns a description about the sample code.
    /// </summary>
    public override string Description {
      get {
        return "This example updates a campaign. To get campaigns, run GetAllCampaigns.cs.";
      }
    }

    /// <summary>
    /// Run the sample code.
    /// </summary>
    /// <param name="user">The AdWords user object running the sample.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the CampaignService.
      CampaignService campaignService =
          (CampaignService)user.GetService(AdWordsService.v200909.CampaignService);

      long campaignId = long.Parse(_T("INSERT_CAMPAIGN_ID_HERE"));

      // Create campaign with updated budget.
      Campaign campaign = new Campaign();
      campaign.id = campaignId;
      campaign.idSpecified = true;

      Budget budget = new Budget();
      budget.deliveryMethodSpecified = true;
      budget.deliveryMethod = BudgetBudgetDeliveryMethod.ACCELERATED;
      campaign.budget = budget;

      // Create operation.
      CampaignOperation operation = new CampaignOperation();
      operation.operatorSpecified = true;
      operation.@operator = Operator.SET;
      operation.operand = campaign;

      try {
        // Update campaign.
        CampaignReturnValue result = campaignService.mutate((new CampaignOperation[] {operation}));

        // Display campaigns.
        if (result != null && result.value != null) {
          foreach (Campaign campaignResult in result.value) {
            Console.WriteLine("Campaign with name = '{0}' and id = '{1}' was updated.",
                campaignResult.name, campaignResult.id);
          }
        } else {
          Console.WriteLine("No campaigns were updated.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to update Campaign(s). Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
