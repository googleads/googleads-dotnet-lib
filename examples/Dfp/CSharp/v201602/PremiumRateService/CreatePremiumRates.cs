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
using Google.Api.Ads.Dfp.v201602;

using System;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201602 {
  /// <summary>
  /// This code example creates a premium rate. To determine which premium rates exist,
  /// run GetAllPremiumRates.cs.
  /// </summary>
  class CreatePremiumRates : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example creates a premium rate. To determine which premium rates " +
            "exist, run GetAllPremiumRates.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new CreatePremiumRates();
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

      // Set the rate card ID to add the premium rate to.
      long rateCardId = long.Parse(_T("INSERT_RATE_CARD_ID_HERE"));

      PremiumRate premiumRate = new PremiumRate();

      // Create an ad unit premium to apply to the rate card.
      AdUnitPremiumFeature adUnitPremiumFeature = new AdUnitPremiumFeature();

      // Create a CPM based premium rate value with adjustments in micro amounts.
      // This will adjust a CPM priced proposal line item that has
      // inventory targeting specified by 2 units of the currency associated with
      // the rate card (this comes from absolute value adjustment).
      PremiumRateValue cpmPremiumRateValue = new PremiumRateValue();
      cpmPremiumRateValue.premiumFeature = adUnitPremiumFeature;
      cpmPremiumRateValue.rateType = RateType.CPM;
      cpmPremiumRateValue.adjustmentSize = 2000000L;
      cpmPremiumRateValue.adjustmentType = PremiumAdjustmentType.ABSOLUTE_VALUE;

      // Create a CPC based premium rate value with adjustments in milli amounts.
      // This will adjust a CPC priced proposal line item that has
      // inventory targeting specified by 10% of the cost associated with the rate
      // card (this comes from a percentage adjustment).
      PremiumRateValue cpcPremiumRateValue = new PremiumRateValue();
      cpcPremiumRateValue.premiumFeature = adUnitPremiumFeature;
      cpcPremiumRateValue.rateType = RateType.CPC;
      cpcPremiumRateValue.adjustmentSize = 10000L;
      cpcPremiumRateValue.adjustmentType = PremiumAdjustmentType.PERCENTAGE;

      // Associate premium rate with the rate card and set premium information.
      // This premium will apply for proposal line items targeting 'any' ad unit
      // for both CPM and CPC rate types.
      premiumRate.rateCardId = rateCardId;
      premiumRate.pricingMethod = PricingMethod.ANY_VALUE;
      premiumRate.premiumFeature = adUnitPremiumFeature;
      premiumRate.premiumRateValues =
          new PremiumRateValue[] {cpmPremiumRateValue, cpcPremiumRateValue};

      try {
        // Create the premium rate on the server.
        PremiumRate[] premiumRates = premiumRateService
            .createPremiumRates(new PremiumRate[] {premiumRate});

        foreach (PremiumRate createdPremiumRate in premiumRates) {
          Console.WriteLine("A premium rate for '{0}' was added to the rate card with "
              + "ID of '{1}'.", createdPremiumRate.premiumFeature.GetType().Name,
              createdPremiumRate.rateCardId);
        }
      } catch (Exception e) {
          Console.WriteLine("Failed to create premium rates. Exception says \"{0}\"", e.Message);
      }
    }
  }
}
