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
using Google.Api.Ads.AdWords.v201109_1;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201109_1 {
  /// <summary>
  /// This code example updates the bid of a placement. To get placement, run
  /// GetPlacements.cs.
  ///
  /// Tags: AdGroupCriterionService.mutate
  /// </summary>
  public class UpdatePlacement : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      ExampleBase codeExample = new UpdatePlacement();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser(), codeExample.GetParameters(), Console.Out);
    }

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example updates the bid of a placement. To get placement, run " +
            "GetPlacements.cs.";
      }
    }

    /// <summary>
    /// Gets the list of parameter names required to run this code example.
    /// </summary>
    /// <returns>
    /// A list of parameter names for this code example.
    /// </returns>
    public override string[] GetParameterNames() {
      return new string[] {"ADGROUP_ID", "PLACEMENT_ID"};
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
      // Get the AdGroupCriterionService.
      AdGroupCriterionService adGroupCriterionService =
          (AdGroupCriterionService) user.GetService(AdWordsService.v201109_1.AdGroupCriterionService);

      long adGroupId = long.Parse(parameters["ADGROUP_ID"]);
      long placementId = long.Parse(parameters["PLACEMENT_ID"]);

      // Since we are not updating any placement-specific fields, it is enough to
      // create a criterion object.
      Criterion criterion = new Criterion();
      criterion.id = placementId;

      // Create ad group criterion.
      BiddableAdGroupCriterion biddableAdGroupCriterion = new BiddableAdGroupCriterion();
      biddableAdGroupCriterion.adGroupId = adGroupId;
      biddableAdGroupCriterion.criterion = criterion;

      // Create the bids.
      // Create bids.
      ManualCPMAdGroupCriterionBids bids = new ManualCPMAdGroupCriterionBids();
      bids.maxCpm = new Bid();
      bids.maxCpm.amount = new Money();
      bids.maxCpm.amount.microAmount = 1000000;

      biddableAdGroupCriterion.bids = bids;

      // Create the operation.
      AdGroupCriterionOperation operation = new AdGroupCriterionOperation();
      operation.@operator = Operator.SET;
      operation.operand = biddableAdGroupCriterion;

      try {
        // Update the placement.
        AdGroupCriterionReturnValue retVal =
            adGroupCriterionService.mutate(new AdGroupCriterionOperation[] {operation});

        // Display the results.
        if (retVal != null && retVal.value != null && retVal.value.Length > 0) {
          AdGroupCriterion adGroupCriterion = retVal.value[0];
          long bidAmount = 0;
          if (adGroupCriterion is BiddableAdGroupCriterion) {
            bidAmount = ((adGroupCriterion as BiddableAdGroupCriterion).bids as
                ManualCPCAdGroupCriterionBids).maxCpc.amount.microAmount;
          }
          writer.WriteLine("Placement with ad group id = '{0}', id = '{1}' was updated with " +
              "bid amount = '{2}' micros.", adGroupCriterion.adGroupId,
              adGroupCriterion.criterion.id, bidAmount);
        } else {
          writer.WriteLine("No placement was updated.");
        }
      } catch (Exception ex) {
        writer.WriteLine("Failed to update placement. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
