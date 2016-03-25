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
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201603 {
  /// <summary>
  /// This code example updates the bid of a keyword. To get keyword, run
  /// GetKeywords.cs.
  /// </summary>
  public class UpdateKeyword : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      UpdateKeyword codeExample = new UpdateKeyword();
      Console.WriteLine(codeExample.Description);
      try {
        long adGroupId = long.Parse("INSERT_ADGROUP_ID_HERE");
        long keywordId = long.Parse("INSERT_KEYWORD_ID_HERE");
        codeExample.Run(new AdWordsUser(), adGroupId, keywordId);
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
        return "This code example updates the bid of a keyword. To get keyword, run " +
            "GetKeywords.cs.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="adGroupId">Id of the ad group that contains the keyword.
    /// </param>
    /// <param name="keywordId">Id of the keyword to be updated.</param>
    public void Run(AdWordsUser user, long adGroupId, long keywordId) {
      // Get the AdGroupCriterionService.
      AdGroupCriterionService adGroupCriterionService =
          (AdGroupCriterionService) user.GetService(AdWordsService.v201603.AdGroupCriterionService);

      // Since we are not updating any keyword-specific fields, it is enough to
      // create a criterion object.
      Criterion criterion = new Criterion();
      criterion.id = keywordId;

      // Create ad group criterion.
      BiddableAdGroupCriterion biddableAdGroupCriterion = new BiddableAdGroupCriterion();
      biddableAdGroupCriterion.adGroupId = adGroupId;
      biddableAdGroupCriterion.criterion = criterion;

      // Create the bids.
      BiddingStrategyConfiguration biddingConfig = new BiddingStrategyConfiguration();
      CpcBid cpcBid = new CpcBid();
      cpcBid.bid = new Money();
      cpcBid.bid.microAmount = 1000000;
      biddingConfig.bids = new Bids[] {cpcBid};

      biddableAdGroupCriterion.biddingStrategyConfiguration = biddingConfig;

      // Create the operation.
      AdGroupCriterionOperation operation = new AdGroupCriterionOperation();
      operation.@operator = Operator.SET;
      operation.operand = biddableAdGroupCriterion;

      try {
        // Update the keyword.
        AdGroupCriterionReturnValue retVal =
            adGroupCriterionService.mutate(new AdGroupCriterionOperation[] {operation});

        // Display the results.
        if (retVal != null && retVal.value != null && retVal.value.Length > 0) {
          AdGroupCriterion adGroupCriterion = retVal.value[0];
          long bidAmount = 0;
          foreach (Bids bids in (adGroupCriterion as BiddableAdGroupCriterion).
              biddingStrategyConfiguration.bids) {
            if (bids is CpcBid) {
              bidAmount = (bids as CpcBid).bid.microAmount;
              break;
            }
          }

          Console.WriteLine("Keyword with ad group id = '{0}', id = '{1}' was updated with " +
              "bid amount = '{2}' micros.", adGroupCriterion.adGroupId,
              adGroupCriterion.criterion.id, bidAmount);
        } else {
          Console.WriteLine("No keyword was updated.");
        }
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to update keyword.", e);
      }
    }
  }
}
