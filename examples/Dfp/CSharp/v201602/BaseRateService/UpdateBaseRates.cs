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
  /// This code example updates a base rate's value. To determine which base rates
  /// exist, run GetAllBaseRates.cs.
  /// </summary>
  class UpdateBaseRates : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example updates a base rate's value. To determine which base rates " +
            "exist, run GetAllBaseRates.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new UpdateBaseRates();
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

      long baseRateId = long.Parse(_T("INSERT_BASE_RATE_ID_HERE"));

      // Create a statement to get the baseRate.
      StatementBuilder statementBuilder = new StatementBuilder()
          .Where("id = :id")
          .OrderBy("id ASC")
          .Limit(1)
          .AddValue("id", baseRateId);

      try {
        // Get base rates by statement.
        BaseRatePage page =
            baseRateService.getBaseRatesByStatement(statementBuilder.ToStatement());

        BaseRate baseRate = page.results[0];

        // Update base rate value to $3 USD.
        Money newRate = new Money() {currencyCode = "USD", microAmount = 3000000L};

        if (baseRate is ProductTemplateBaseRate) {
          ((ProductTemplateBaseRate) baseRate).rate = newRate;
        } else if (baseRate is ProductBaseRate) {
          ((ProductBaseRate) baseRate).rate = newRate;
        }

        // Update the base rates on the server.
        BaseRate[] baseRates = baseRateService.updateBaseRates(new BaseRate[] {baseRate});

        if (baseRates != null) {
          foreach (BaseRate updatedBaseRate in baseRates) {
            Console.WriteLine("Base rate with ID ='{0}' and type '{1}' belonging to rate card " +
                "'{2}' was updated.", baseRate.id, baseRate.GetType().Name, baseRate.rateCardId);
          }
        } else {
          Console.WriteLine("No base rates updated.");
        }
      } catch (Exception e) {
        Console.WriteLine("Failed to update base rates. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}
