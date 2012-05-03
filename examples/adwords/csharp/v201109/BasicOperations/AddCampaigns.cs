// Copyright 2012, Google Inc. All Rights Reserved.
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
  /// This code example adds campaigns. To get campaigns, run GetCampaigns.cs.
  ///
  /// Tags: CampaignService.mutate
  /// </summary>
  public class AddCampaigns : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      ExampleBase codeExample = new AddCampaigns();
      Console.WriteLine(codeExample.Description);
      try {
        codeExample.Run(new AdWordsUser(), codeExample.GetParameters(), Console.Out);
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
        return "This code example adds campaigns. To get campaigns, run GetCampaigns.cs.";
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
      Campaign campaign1 = new Campaign();
      campaign1.name = "Interplanetary Cruise #" + ExampleUtilities.GetTimeStamp();
      campaign1.status = CampaignStatus.PAUSED;
      campaign1.biddingStrategy = new ManualCPC();

      // Set the campaign budget.
      Budget budget1 = new Budget();
      budget1.period = BudgetBudgetPeriod.DAILY;
      budget1.deliveryMethod = BudgetBudgetDeliveryMethod.STANDARD;
      budget1.amount = new Money();
      budget1.amount.microAmount = 50000000;

      campaign1.budget = budget1;

      // Set the campaign network options.
      campaign1.networkSetting = new NetworkSetting();
      campaign1.networkSetting.targetGoogleSearch = true;
      campaign1.networkSetting.targetSearchNetwork = true;
      campaign1.networkSetting.targetContentContextual = false;
      campaign1.networkSetting.targetContentNetwork = false;
      campaign1.networkSetting.targetPartnerSearchNetwork = false;

      // Optional: Set the start date.
      campaign1.startDate = DateTime.Now.AddDays(1).ToString("yyyyMMdd");

      // Optional: Set the end date.
      campaign1.endDate = DateTime.Now.AddYears(1).ToString("yyyyMMdd");

      // Optional: Set the campaign ad serving optimization status.
      campaign1.adServingOptimizationStatus = AdServingOptimizationStatus.ROTATE;

      // Optional: Set the frequency cap.
      FrequencyCap frequencyCap1 = new FrequencyCap();
      frequencyCap1.impressions = 5;
      frequencyCap1.level = Level.ADGROUP;
      frequencyCap1.timeUnit = TimeUnit.DAY;
      campaign1.frequencyCap = frequencyCap1;

      // Create the operation.
      CampaignOperation operation1 = new CampaignOperation();
      operation1.@operator = Operator.ADD;
      operation1.operand = campaign1;

      // Create the campaign.
      Campaign campaign2 = new Campaign();
      campaign2.name = "Interplanetary Cruise Banners #" + ExampleUtilities.GetTimeStamp();
      campaign2.status = CampaignStatus.PAUSED;
      campaign2.biddingStrategy = new ManualCPC();

      // Set the campaign budget.
      Budget budget2 = new Budget();
      budget2.period = BudgetBudgetPeriod.DAILY;
      budget2.deliveryMethod = BudgetBudgetDeliveryMethod.STANDARD;
      budget2.amount = new Money();
      budget2.amount.microAmount = 30000000;

      campaign2.budget = budget2;

      // Set the campaign network options.
      campaign2.networkSetting = new NetworkSetting();
      campaign2.networkSetting.targetGoogleSearch = true;
      campaign2.networkSetting.targetSearchNetwork = true;
      campaign2.networkSetting.targetContentContextual = false;
      campaign2.networkSetting.targetContentNetwork = false;
      campaign2.networkSetting.targetPartnerSearchNetwork = false;

      // Optional: Set the start date.
      campaign2.startDate = DateTime.Now.AddDays(1).ToString("yyyyMMdd");

      // Optional: Set the end date.
      campaign2.endDate = DateTime.Now.AddYears(1).ToString("yyyyMMdd");

      // Optional: Set the campaign ad serving optimization status.
      campaign2.adServingOptimizationStatus = AdServingOptimizationStatus.ROTATE;

      // Optional: Set the frequency cap.
      FrequencyCap frequencyCap2 = new FrequencyCap();
      frequencyCap2.impressions = 5;
      frequencyCap2.level = Level.ADGROUP;
      frequencyCap2.timeUnit = TimeUnit.DAY;
      campaign2.frequencyCap = frequencyCap2;

      // Create the operation.
      CampaignOperation operation2 = new CampaignOperation();
      operation2.@operator = Operator.ADD;
      operation2.operand = campaign2;

      try {
        // Add the campaign.
        CampaignReturnValue retVal = campaignService.mutate(
            (new CampaignOperation[] {operation1, operation2}));

        // Display the results.
        if (retVal != null && retVal.value != null && retVal.value.Length > 0) {
          foreach (Campaign newCampaign in retVal.value) {
            writer.WriteLine("Campaign with name = '{0}' and id = '{1}' was added.",
                newCampaign.name, newCampaign.id);
          }
        } else {
          writer.WriteLine("No campaigns were added.");
        }
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to add campaigns.", ex);
      }
    }
  }
}
