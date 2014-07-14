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
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201406 {
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
      UpdatePlacement codeExample = new UpdatePlacement();
      Console.WriteLine(codeExample.Description);
      try {
        long adGroupId = long.Parse("INSERT_ADGROUP_ID_HERE");
        long placementId = long.Parse("INSERT_PLACEMENT_ID_HERE");
        codeExample.Run(new AdWordsUser(), adGroupId, placementId);
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
        return "This code example updates the bid of a placement. To get placement, run " +
            "GetPlacements.cs.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="adGroupId">Id of the ad group that contains the placement.
    /// </param>
    /// <param name="placementId">Id of the placement to be updated.</param>
    public void Run(AdWordsUser user, long adGroupId, long placementId) {
      // Get the AdGroupCriterionService.
      AdGroupCriterionService adGroupCriterionService =
          (AdGroupCriterionService) user.GetService(AdWordsService.v201406.AdGroupCriterionService);

      // Since we are not updating any placement-specific fields, it is enough to
      // create a criterion object.
      Criterion criterion = new Criterion();
      criterion.id = placementId;

      // Create ad group criterion.
      BiddableAdGroupCriterion biddableAdGroupCriterion = new BiddableAdGroupCriterion();
      biddableAdGroupCriterion.adGroupId = adGroupId;
      biddableAdGroupCriterion.criterion = criterion;

      // Create the bids.
      BiddingStrategyConfiguration biddingConfig = new BiddingStrategyConfiguration();
      CpmBid cpmBid = new CpmBid();
      cpmBid.bid = new Money();
      cpmBid.bid.microAmount = 1000000;
      biddingConfig.bids = new Bids[] {cpmBid};

      biddableAdGroupCriterion.biddingStrategyConfiguration = biddingConfig;

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
          foreach (Bids bids in (adGroupCriterion as BiddableAdGroupCriterion).
              biddingStrategyConfiguration.bids) {
            if (bids is CpmBid) {
              bidAmount = (bids as CpmBid).bid.microAmount;
              break;
            }
          }
          Console.WriteLine("Placement with ad group id = '{0}', id = '{1}' was updated with " +
              "bid amount = '{2}' micros.", adGroupCriterion.adGroupId,
              adGroupCriterion.criterion.id, bidAmount);
        } else {
          Console.WriteLine("No placement was updated.");
        }
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to update placement.", ex);
      }
    }
  }
}
