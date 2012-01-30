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
  /// This code example updates a campaign. To get campaigns, run
  /// GetCampaigns.cs.
  ///
  /// Tags: CampaignService.mutate
  /// </summary>
  class UpdateCampaign : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      ExampleBase codeExample = new UpdateCampaign();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser(), codeExample.GetParameters(), Console.Out);
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
      CampaignService campaignService =
          (CampaignService)user.GetService(AdWordsService.v201109.CampaignService);

      long campaignId = long.Parse(parameters["CAMPAIGN_ID"]);

      // Create campaign with updated budget.
      Campaign campaign = new Campaign();
      campaign.id = campaignId;

      Budget budget = new Budget();
      budget.deliveryMethod = BudgetBudgetDeliveryMethod.ACCELERATED;
      campaign.budget = budget;

      // Create the operations.
      CampaignOperation operation = new CampaignOperation();
      operation.@operator = Operator.SET;
      operation.operand = campaign;

      try {
        // Update the campaign.
        CampaignReturnValue retVal = campaignService.mutate((new CampaignOperation[] {operation}));

        // Display the results.
        if (retVal != null && retVal.value != null && retVal.value.Length > 0) {
          Campaign updatedCampaign = retVal.value[0];
          writer.WriteLine("Campaign with name = '{0}' and id = '{1}' was updated.",
              updatedCampaign.name, updatedCampaign.id);
        } else {
          writer.WriteLine("No campaigns were updated.");
        }
      } catch (Exception ex) {
        writer.WriteLine("Failed to update campaign(s). Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
