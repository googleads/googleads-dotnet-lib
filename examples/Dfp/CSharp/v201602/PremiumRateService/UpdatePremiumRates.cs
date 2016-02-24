// Copyright 2015, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.Util.v201602;
using Google.Api.Ads.Dfp.v201602;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201602 {
  /// <summary>
  /// This code example updates a premium rate to add a flat fee to an existing feature premium.
  /// To determine which premium rates exist, run GetAllPremiumRates.cs.
  /// </summary>
  class UpdatePremiumRates : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example updates a premium rate to add a flat fee to an existing " +
            "feature premium. To determine which premium rates exist, run GetAllPremiumRates.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new UpdatePremiumRates();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the PremiumRateService.
      PremiumRateService premiumRateService =
          (PremiumRateService) user.GetService(DfpService.v201602.PremiumRateService);

      long premiumRateId = long.Parse(_T("INSERT_PREMIUM_RATE_ID_HERE"));

      // Create a statement to get the premium rate.
      StatementBuilder statementBuilder = new StatementBuilder()
          .Where("id = :id")
          .OrderBy("id ASC")
          .Limit(1)
          .AddValue("id", premiumRateId);

      try {
        // Get premium rates by statement.
        PremiumRatePage page =
            premiumRateService.getPremiumRatesByStatement(statementBuilder.ToStatement());

        PremiumRate premiumRate = page.results[0];

        // Create a flat fee based premium rate value with a 10% increase.
        PremiumRateValue flatFeePremiumRateValue = new PremiumRateValue();
        flatFeePremiumRateValue.premiumFeature = premiumRate.premiumFeature;
        flatFeePremiumRateValue.rateType = RateType.CPM;
        flatFeePremiumRateValue.adjustmentSize = 10000L;
        flatFeePremiumRateValue.adjustmentType = PremiumAdjustmentType.PERCENTAGE;

        // Update the premium rate's values to include a flat fee premium rate.
        List<PremiumRateValue> existingPremiumRateValues = (premiumRate.premiumRateValues != null)
            ? new List<PremiumRateValue>(premiumRate.premiumRateValues)
            : new List<PremiumRateValue>();

        existingPremiumRateValues.Add(flatFeePremiumRateValue);
        premiumRate.premiumRateValues = existingPremiumRateValues.ToArray();

        // Update the premium rates on the server.
        PremiumRate[] premiumRates =
            premiumRateService.updatePremiumRates(new PremiumRate[] {premiumRate});

        if (premiumRates != null) {
          foreach (PremiumRate updatedPremiumRate in premiumRates) {
            Console.WriteLine("Premium rate with ID '{1}' associated with rate card ID '{2}' " +
                "was updated.", updatedPremiumRate.id,
                updatedPremiumRate.premiumFeature.GetType().Name,
                updatedPremiumRate.rateCardId);
          }
        } else {
          Console.WriteLine("No premium rates updated.");
        }
      } catch (Exception e) {
        Console.WriteLine("Failed to update premium rates. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}
