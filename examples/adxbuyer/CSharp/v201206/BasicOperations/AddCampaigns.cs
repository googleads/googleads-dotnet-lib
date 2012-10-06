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
using Google.Api.Ads.AdWords.v201206;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201206 {
  /// <summary>
  /// This code example adds campaigns. To get campaigns, run GetCampaigns.cs.
  ///
  /// Tags: CampaignService.mutate
  /// </summary>
  public class AddCampaigns : ExampleBase {
    /// <summary>
    /// Number of items being added / updated in this code example.
    /// </summary>
    const int NUM_ITEMS = 5;

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      AddCampaigns codeExample = new AddCampaigns();
      Console.WriteLine(codeExample.Description);
      try {
        codeExample.Run(new AdWordsUser());
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
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    public void Run(AdWordsUser user) {
      // Get the CampaignService.
      CampaignService campaignService =
          (CampaignService) user.GetService(AdWordsService.v201109.CampaignService);

      List<CampaignOperation> operations = new List<CampaignOperation>();

      for (int i = 0; i < NUM_ITEMS; i++) {
        // Create the campaign.
        Campaign campaign = new Campaign();
        campaign.name = "Interplanetary Cruise #" + ExampleUtilities.GetRandomString();
        campaign.status = CampaignStatus.PAUSED;
        campaign.biddingStrategy = new ManualCPM();

        // Set the campaign budget.
        Budget budget = new Budget();
        budget.period = BudgetBudgetPeriod.DAILY;
        budget.deliveryMethod = BudgetBudgetDeliveryMethod.STANDARD;
        budget.amount = new Money();
        budget.amount.microAmount = 50000000;

        campaign.budget = budget;

        // Set targetContentNetwork true. Other network targeting is not available
        // for Ad Exchange Buyers.
        campaign.networkSetting = new NetworkSetting();
        campaign.networkSetting.targetGoogleSearch = false;
        campaign.networkSetting.targetSearchNetwork = false;
        campaign.networkSetting.targetContentNetwork = true;
        campaign.networkSetting.targetPartnerSearchNetwork = false;

        // Enable campaign for Real-time bidding.
        RealTimeBiddingSetting rtbSetting = new RealTimeBiddingSetting();
        rtbSetting.optIn = true;

        // Optional: Set Display network targeting settings.
        // It can only be enabled, shown only for demonstration purposes.
        // If not set this setting is enabled by default on ADD operations.
        TargetRestrictSetting displaySetting = new TargetRestrictSetting();
        displaySetting.useAdGroup = true;

        campaign.settings = new Setting[] {rtbSetting, displaySetting};

        // Optional: Set the start date.
        campaign.startDate = DateTime.Now.AddDays(1).ToString("yyyyMMdd");

        // Optional: Set the end date.
        campaign.endDate = DateTime.Now.AddYears(1).ToString("yyyyMMdd");

        // Optional: Set the campaign ad serving optimization status.
        campaign.adServingOptimizationStatus = AdServingOptimizationStatus.ROTATE;

        // Optional: Set the frequency cap.
        FrequencyCap frequencyCap = new FrequencyCap();
        frequencyCap.impressions = 5;
        frequencyCap.level = Level.ADGROUP;
        frequencyCap.timeUnit = TimeUnit.DAY;
        campaign.frequencyCap = frequencyCap;

        // Create the operation.
        CampaignOperation operation = new CampaignOperation();
        operation.@operator = Operator.ADD;
        operation.operand = campaign;

        operations.Add(operation);
      }

      try {
        // Add the campaign.
        CampaignReturnValue retVal = campaignService.mutate(operations.ToArray());

        // Display the results.
        if (retVal != null && retVal.value != null && retVal.value.Length > 0) {
          foreach (Campaign newCampaign in retVal.value) {
            Console.WriteLine("Campaign with name = '{0}' and id = '{1}' was added.",
                newCampaign.name, newCampaign.id);
          }
        } else {
          Console.WriteLine("No campaigns were added.");
        }
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to add campaigns.", ex);
      }
    }
  }
}
