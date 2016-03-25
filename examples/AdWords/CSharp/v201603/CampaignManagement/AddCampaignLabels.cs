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

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201603 {

  /// <summary>
  /// This code example adds a label to multiple campaigns.
  /// </summary>
  public class AddCampaignLabels : ExampleBase {

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      AddCampaignLabels codeExample = new AddCampaignLabels();
      Console.WriteLine(codeExample.Description);
      try {
        long campaignId1 = long.Parse("INSERT_CAMPAIGN_ID1_HERE");
        long campaignId2 = long.Parse("INSERT_CAMPAIGN_ID2_HERE");
        long labelId = long.Parse("INSERT_LABEL_ID_HERE");
        codeExample.Run(new AdWordsUser(), campaignId1, campaignId2, labelId);
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
        return "This code example adds a label to multiple campaigns.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="campaignId1">Id of the campaign to which labels are
    /// added.</param>
    /// <param name="campaignId2">Id of the campaign to which labels are
    /// added.</param>
    /// <param name="labelId">ID of the label to apply.</param>
    public void Run(AdWordsUser user, long campaignId1, long campaignId2, long labelId) {
      try {
        // Get the CampaignService.
        CampaignService campaignService =
            (CampaignService) user.GetService(AdWordsService.v201603.CampaignService);

        // Create label operations.
        List<CampaignLabelOperation> operations = new List<CampaignLabelOperation>();

        foreach (long campaignId in new long[] { campaignId1, campaignId2 }) {
          CampaignLabel campaignLabel = new CampaignLabel();
          campaignLabel.campaignId = campaignId;
          campaignLabel.labelId = labelId;

          CampaignLabelOperation operation = new CampaignLabelOperation();
          operation.operand = campaignLabel;
          operation.@operator = Operator.ADD;

          operations.Add(operation);
        }
        CampaignLabelReturnValue retval = campaignService.mutateLabel(
        operations.ToArray());

        // Display campaign labels.
        if (retval != null && retval.value != null) {
          foreach (CampaignLabel newCampaignLabel in retval.value) {
            Console.WriteLine("Campaign label for campaign ID {0} and label ID {1} was added.\n",
                newCampaignLabel.campaignId, newCampaignLabel.labelId);
          }
        } else {
          Console.WriteLine("No campaign labels were added.");
        }
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to add campaign label.", e);
      }
    }
  }
}
