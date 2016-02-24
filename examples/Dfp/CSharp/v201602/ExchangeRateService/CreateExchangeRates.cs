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
  /// This code example creates a new exchange rate. To determine which
  /// exchange rates exist, run GetAllExchangeRates.cs.
  /// </summary>
  class CreateExchangeRates : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example creates a new exchange rate. To determine which " +
            "exchange rates exist, run GetAllExchangeRates.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new CreateExchangeRates();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code examples.
    /// </summary>
    /// <param name="user">The DFP user object running the code examples.</param>
    public override void Run(DfpUser user) {
      // Get the ExchangeRateService.
      ExchangeRateService exchangeRateService =
          (ExchangeRateService) user.GetService(DfpService.v201602.ExchangeRateService);

      // Create an exchange rate.
      ExchangeRate exchangeRate = new ExchangeRate();

      // Set the currency code.
      exchangeRate.currencyCode = "AUD";

      // Set the direction of the conversion (from the network currency).
      exchangeRate.direction = ExchangeRateDirection.FROM_NETWORK;

      // Set the conversion value as 1.5 (this value is multiplied by 10,000,000,000)
      exchangeRate.exchangeRate = 15000000000L;

      // Do not refresh exchange rate from Google data. Update manually only.
      exchangeRate.refreshRate = ExchangeRateRefreshRate.FIXED;

      try {
        // Create the exchange rate on the server.
        ExchangeRate[] exchangeRates = exchangeRateService.createExchangeRates(
            new ExchangeRate[] {exchangeRate});

        foreach (ExchangeRate createdExchangeRate in exchangeRates) {
          Console.WriteLine("An exchange rate with ID '{0}', currency code '{1}', " +
              "direction '{2}' and exchange rate '{3}' was created.", exchangeRate.id,
              exchangeRate.currencyCode, exchangeRate.direction,
              (exchangeRate.exchangeRate / 10000000000f));
        }

      } catch (Exception e) {
        Console.WriteLine("Failed to create exchange rates. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}
