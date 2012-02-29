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
  /// This code example adds a campaign. To get campaigns, run GetCampaigns.cs.
  ///
  /// Tags: CampaignService.mutate
  /// </summary>
  class AddCampaign : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      ExampleBase codeExample = new AddCampaign();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser(), codeExample.GetParameters(), Console.Out);
    }

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example adds a campaign. To get campaigns, run GetCampaigns.cs.";
      }
    }

    /// <summary>
    /// Gets the list of parameter names required to run this code example.
    /// </summary>
    /// <returns>
    /// A list of parameter names for this code example.
    /// </returns>
    public override string[] GetParameterNames() {
      return new string[] {};
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
      CampaignService campaignService =
          (CampaignService) user.GetService(AdWordsService.v201109.CampaignService);

      // Create the campaign.
      Campaign campaign = new Campaign();
      campaign.name = "Interplanetary Cruise #" + ExampleUtilities.GetTimeStamp();
      campaign.status = CampaignStatus.PAUSED;
      campaign.biddingStrategy = new ManualCPM();

      // Set the campaign budget.
      Budget budget = new Budget();
      budget.period = BudgetBudgetPeriod.DAILY;
      budget.deliveryMethod = BudgetBudgetDeliveryMethod.STANDARD;
      budget.amount = new Money();
      budget.amount.microAmount = 50000000;

      campaign.budget = budget;

      // Set the campaign network options to ContentNetwork. Set other network
      // options to false.
      campaign.networkSetting = new NetworkSetting();
      campaign.networkSetting.targetGoogleSearch = false;
      campaign.networkSetting.targetSearchNetwork = false;
      campaign.networkSetting.targetContentContextual = false;
      campaign.networkSetting.targetContentNetwork = true;
      campaign.networkSetting.targetPartnerSearchNetwork = false;

      // Enable campaign for Real-time bidding.
      RealTimeBiddingSetting rtbSetting = new RealTimeBiddingSetting();
      rtbSetting.optIn = true;
      campaign.settings = new Setting[] {rtbSetting};

      // Create the operation.
      CampaignOperation operation = new CampaignOperation();
      operation.@operator = Operator.ADD;
      operation.operand = campaign;

      try {
        // Add the campaign.
        CampaignReturnValue retVal = campaignService.mutate((new CampaignOperation[] {operation}));

        // Display the results.
        if (retVal != null && retVal.value != null && retVal.value.Length > 0) {
          Campaign newCampaign = retVal.value[0];
          writer.WriteLine("Campaign with name = '{0}' and id = '{1}' was added.",
              newCampaign.name, newCampaign.id);
        } else {
          writer.WriteLine("No campaigns were added.");
        }
      } catch (Exception ex) {
        writer.WriteLine("Failed to add campaign. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
