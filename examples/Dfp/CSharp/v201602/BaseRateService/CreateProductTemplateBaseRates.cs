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
  /// This code example creates a product template base rate. To determine which base rates exist,
  /// run GetAllBaseRates.cs.
  /// </summary>
  class CreateProductTemplateBaseRates : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example creates a product template base rate. To determine which base " +
            "rates exist, run GetAllBaseRates.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new CreateProductTemplateBaseRates();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the BaseRateService.
      BaseRateService baseRateService =
          (BaseRateService) user.GetService(DfpService.v201602.BaseRateService);

      // Set the rate card ID to add the base rate to.
      long rateCardId = long.Parse(_T("INSERT_RATE_CARD_ID_HERE"));

      // Set the product template to apply this base rate to.
      long productTemplateId = long.Parse(_T("INSERT_PRODUCT_TEMPLATE_ID_HERE"));

      // Create a base rate for a product template.
      ProductTemplateBaseRate productTemplateBaseRate = new ProductTemplateBaseRate();

      // Set the rate card ID that the product template base rate belongs to.
      productTemplateBaseRate.rateCardId = rateCardId;

      // Set the product template the base rate will be applied to.
      productTemplateBaseRate.productTemplateId = productTemplateId;

      // Create a rate worth $2 and set that on the product template base rate.
      productTemplateBaseRate.rate = new Money() {currencyCode = "USD", microAmount = 2000000L};

      try {
        // Create the base rate on the server.
        BaseRate[] baseRates = baseRateService.createBaseRates(
            new BaseRate[] {productTemplateBaseRate});

        foreach (BaseRate createdBaseRate in baseRates) {
          Console.WriteLine("A product template base rate with ID '{0}' and rate '{1} {2}' was " +
              "created.", createdBaseRate.id, createdBaseRate.GetType().Name,
              (((ProductTemplateBaseRate) createdBaseRate).rate.microAmount / 1000000f),
              ((ProductTemplateBaseRate) createdBaseRate).rate.currencyCode);
        }
      } catch (Exception e) {
          Console.WriteLine("Failed to create base rates. Exception says \"{0}\"", e.Message);
      }
    }
  }
}
