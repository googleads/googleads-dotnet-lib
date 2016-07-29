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

using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.Util.v201605;
using Google.Api.Ads.Dfp.v201605;

using System;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201605 {
  /// <summary>
  /// This example gets all exchange rates.
  /// </summary>
  public class GetAllExchangeRates : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This example gets all exchange rates.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main() {
      GetAllExchangeRates codeExample = new GetAllExchangeRates();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public void Run(DfpUser user) {
      ExchangeRateService exchangeRateService =
          (ExchangeRateService) user.GetService(DfpService.v201605.ExchangeRateService);

      // Create a statement to select exchange rates.
      StatementBuilder statementBuilder = new StatementBuilder()
          .OrderBy("id ASC")
          .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT);

      // Retrieve a small amount of exchange rates at a time, paging through
      // until all exchange rates have been retrieved.
      ExchangeRatePage page = new ExchangeRatePage();
      try {
        do {
          page = exchangeRateService.getExchangeRatesByStatement(statementBuilder.ToStatement());

          if (page.results != null) {
            // Print out some information for each exchange rate.
            int i = page.startIndex;
            foreach (ExchangeRate exchangeRate in page.results) {
              Console.WriteLine("{0}) Exchange rate with ID \"{1}\", currency code \"{2}\", "
                  + "direction \"{3}\", and exchange rate \"{4}\" was found.",
                  i++,
                  exchangeRate.id,
                  exchangeRate.currencyCode,
                  exchangeRate.direction,
                  exchangeRate.exchangeRate);
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
