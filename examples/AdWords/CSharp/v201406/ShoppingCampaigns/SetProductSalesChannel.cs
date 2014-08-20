// Copyright 2014, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdWords.v201406;

using System;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201406 {

  /// <summary>
  /// This code example sets the product sales channel.
  ///
  /// Tags: CampaignCriterionService.mutate
  /// </summary>
  public class SetProductSalesChannel : ExampleBase {

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example sets the product sales channel.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SetProductSalesChannel codeExample = new SetProductSalesChannel();
      Console.WriteLine(codeExample.Description);
      try {
        long campaignId = long.Parse("INSERT_CAMPAIGN_ID_HERE");
        codeExample.Run(new AdWordsUser(), campaignId);
      } catch (Exception ex) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(ex));
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="campaignId">Id of the campaign for which shopping channel
    /// is set.</param>
    public void Run(AdWordsUser user, long campaignId) {
      // Get the CampaignCriterionService.
      CampaignCriterionService campaignCriterionService =
          (CampaignCriterionService) user.GetService(
              AdWordsService.v201406.CampaignCriterionService);

      // ProductSalesChannel is a fixed id criterion, with the possible values
      // defined here.
      // ONLINE: 200
      // LOCAL: 201
      ProductSalesChannel productSalesChannel = new ProductSalesChannel();
      productSalesChannel.id = 200;

      CampaignCriterion campaignCriterion = new CampaignCriterion();
      campaignCriterion.campaignId = campaignId;
      campaignCriterion.criterion = productSalesChannel;

      // Create operation.
      CampaignCriterionOperation operation = new CampaignCriterionOperation();
      operation.operand = campaignCriterion;
      operation.@operator = Operator.ADD;

      try {
        // Make the mutate request.
        CampaignCriterionReturnValue retVal = campaignCriterionService.mutate(
            new CampaignCriterionOperation[] { operation });

        if (retVal != null && retVal.value != null) {
          // Display campaign targets.
          foreach (CampaignCriterion criterion in retVal.value) {
            Console.WriteLine("Campaign criteria of type '{0}' was set to campaign with" +
                " id = '{1}'.", criterion.criterion.CriterionType, criterion.campaignId);
          }
        }
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to set shopping product channel.", ex);
      }
    }
  }
}
