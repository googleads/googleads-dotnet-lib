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
  /// This code example updates the bid of an ad group criterion. To get
  /// ad group criteria, run GetAllAdGroupCriteria.cs.
  ///
  /// Tags: AdGroupCriterionService.mutate
  /// </summary>
  class UpdateAdGroupCriterion : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example updates the bid of an ad group criterion. To get ad group " +
            "criteria, run GetAllAdGroupCriteria.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new UpdateAdGroupCriterion();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the AdGroupCriterionService.
      AdGroupCriterionService adGroupCriterionService =
          (AdGroupCriterionService) user.GetService(AdWordsService.v200909.AdGroupCriterionService);

      long adGroupId = long.Parse(_T("INSERT_AD_GROUP_ID_HERE"));
      long criterionId = long.Parse(_T("INSERT_CRITERION_ID_HERE"));

      Criterion criterion = new Criterion();
      criterion.id = criterionId;

      // Create ad group criterion.
      BiddableAdGroupCriterion biddableAdGroupCriterion = new BiddableAdGroupCriterion();
      biddableAdGroupCriterion.adGroupId = adGroupId;
      biddableAdGroupCriterion.criterion = criterion;

      // Create bids.
      ManualCPCAdGroupCriterionBids bids = new ManualCPCAdGroupCriterionBids();
      bids.maxCpc = new Bid();
      bids.maxCpc.amount = new Money();
      bids.maxCpc.amount.microAmount = 10000;

      biddableAdGroupCriterion.bids = bids;

      // Create operations.
      AdGroupCriterionOperation operation = new AdGroupCriterionOperation();
      operation.@operator = Operator.SET;
      operation.operand = biddableAdGroupCriterion;

      try {
      // Update ad group criteria.
        AdGroupCriterionReturnValue retVal =
            adGroupCriterionService.mutate(new AdGroupCriterionOperation[] {operation});

        // Display ad group criteria.
        if (retVal != null && retVal.value != null) {
          foreach (AdGroupCriterion adGroupCriterion in retVal.value) {
            long bidAmount = 0;
            if (adGroupCriterion is BiddableAdGroupCriterion) {
              bidAmount = ((adGroupCriterion as BiddableAdGroupCriterion).bids as
                  ManualCPCAdGroupCriterionBids).maxCpc.amount.microAmount;
            }
            Console.WriteLine("Ad group criterion with ad group id = '{0}', criterion id = '{1}'" +
                " and type = '{2}' was updated with bid amount = '{3}' micros.",
                adGroupCriterion.adGroupId, adGroupCriterion.criterion.id,
                adGroupCriterion.criterion.CriterionType, bidAmount);

          }
        } else {
          Console.WriteLine("No ad group criteria were updated.\n");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to update ad group criteria. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
