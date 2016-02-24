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
  /// This code example updates a fixed exchange rate's value. To determine which
  /// exchange rates exist, run GetAllExchangeRates.cs.
  /// </summary>
  class UpdateExchangeRates : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example updates a fixed exchange rate's value. To determine which " +
          "exchange rates exist, run GetAllExchangeRates.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new UpdateExchangeRates();
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

      // Set the ID of the exchange rate.
      long exchangeRateId = long.Parse(_T("INSERT_EXCHANGE_RATE_ID_HERE"));

      // Create a statement to get the exchange rate.
      StatementBuilder statementBuilder = new StatementBuilder()
          .Where("id = :exchangeRateId and refreshRate = :refreshRate")
          .OrderBy("id ASC")
          .Limit(1)
          .AddValue("exchangeRateId", exchangeRateId)
          .AddValue("refreshRate", ExchangeRateRefreshRate.FIXED.ToString());

      try {
        // Get exchange rates by statement.
        ExchangeRatePage page = exchangeRateService
            .getExchangeRatesByStatement(statementBuilder.ToStatement());

        ExchangeRate exchangeRate = page.results[0];

        // Update the exchange rate value to 1.5.
        exchangeRate.exchangeRate = 15000000000L;

        // Update the exchange rate on the server.
        ExchangeRate[] exchangeRates = exchangeRateService
            .updateExchangeRates(new ExchangeRate[] {exchangeRate});

        if (exchangeRates != null) {
          foreach (ExchangeRate updatedExchangeRate in exchangeRates) {
            Console.WriteLine("An exchange rate with ID '{0}', currency code '{1}', " +
                "direction '{2}' and exchange rate '{3}' was updated.", exchangeRate.id,
                exchangeRate.currencyCode, exchangeRate.direction,
                (exchangeRate.exchangeRate / 10000000000f));
          }
        } else {
          Console.WriteLine("No exchange rates updated.");
        }
      } catch (Exception e) {
        Console.WriteLine("Failed to update exchange rates. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}
