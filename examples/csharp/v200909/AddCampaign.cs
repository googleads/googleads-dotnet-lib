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
using Google.Api.Ads.AdWords.v200909;

using System;
using System.IO;
using System.Net;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v200909 {
  /// <summary>
  /// This code example adds a campaign. To get campaigns, run
  /// GetAllCampaigns.cs.
  ///
  /// Tags: CampaignService.mutate
  /// </summary>
  class AddCampaign : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example adds a campaign. To get campaigns, run GetAllCampaigns.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new AddCampaign();
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
      CampaignService campaignService =
          (CampaignService) user.GetService(AdWordsService.v200909.CampaignService);

      // Create campaign.
      Campaign campaign = new Campaign();
      campaign.name = "Interplanetary Cruise #" + GetTimeStamp();
      campaign.status = CampaignStatus.PAUSED;
      campaign.biddingStrategy = new ManualCPC();

      Budget budget = new Budget();
      budget.period = BudgetBudgetPeriod.DAILY;
      budget.deliveryMethod = BudgetBudgetDeliveryMethod.STANDARD;
      budget.amount = new Money();
      budget.amount.microAmount = 50000000;

      campaign.budget = budget;

      // Create operations.
      CampaignOperation operation = new CampaignOperation();
      operation.@operator = Operator.ADD;
      operation.operand = campaign;

      try {
        // Add campaign.
        CampaignReturnValue retVal = campaignService.mutate((new CampaignOperation[] {operation}));

        // Display campaigns.
        if (retVal != null && retVal.value != null) {
         foreach (Campaign campaignResult in retVal.value) {
           Console.WriteLine("Campaign with name = '{0}' and id = '{1}' was added.",
              campaignResult.name, campaignResult.id);
         }
        } else {
          Console.WriteLine("No campaigns were added.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to add Campaign. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
