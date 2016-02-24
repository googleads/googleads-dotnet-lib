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

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201602 {
  /// <summary>
  /// This code example gets all exchange rates. To create exchange rates, run
  /// CreateExchangeRates.cs.
  /// </summary>
  class GetAllExchangeRates : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets all exchange rates. To create exchange rates, " +
            "run CreateExchangeRates.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetAllExchangeRates();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the ExchangeRateService.
      ExchangeRateService exchangeRateService =
          (ExchangeRateService) user.GetService(DfpService.v201602.ExchangeRateService);

      // Create a statement to get all exchange rates.
      StatementBuilder statementBuilder = new StatementBuilder()
          .OrderBy("id ASC")
          .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT);

      // Sets default for page.
      ExchangeRatePage page = new ExchangeRatePage();
      try {
        do {
          // Get exchange rates by statement.
          page = exchangeRateService
              .getExchangeRatesByStatement(statementBuilder.ToStatement());

          if (page.results != null && page.results.Length > 0) {
            int i = page.startIndex;
            foreach (ExchangeRate exchangeRate in page.results) {
              Console.WriteLine("{0}) Exchange rate with ID '{1}', currency code '{2}', " +
                  "direction '{3}' and exchange rate '{4}' was found.", i++,
                  exchangeRate.id, exchangeRate.currencyCode, exchangeRate.direction,
                  (exchangeRate.exchangeRate / 10000000000f));
            }
          }

          statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
        } while (statementBuilder.GetOffset() < page.totalResultSetSize);

        Console.WriteLine("Number of results found: {0}", page.totalResultSetSize);
      } catch (Exception e) {
        Console.WriteLine("Failed to get exchange rates. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}
