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
  /// This code example gets a delivery forecast for existing line items. To determine
  /// which line items exist, run GetAllLineItems.cs.
  /// </summary>
  class GetDeliveryForecastByIds : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets a delivery forecast for existing line items. To determine "
            + "which line items exist, run GetAllLineItems.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetDeliveryForecastByIds();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // [START forecasting_4] MOE:strip_line
      // Get the ForecastService.
      ForecastService forecastService =
          (ForecastService) user.GetService(DfpService.v201602.ForecastService);
      // [END forecasting_4] MOE:strip_line

      // Set the line item to get a forecast for.
      long lineItemId1 = long.Parse(_T("INSERT_LINE_ITEM_ID_HERE"));
      long lineItemId2 = long.Parse(_T("INSERT_LINE_ITEM_ID_HERE"));

      try {
        // [START forecasting_5] MOE:strip_line
        // Get a delivery forecast for the line items.
        DeliveryForecastOptions options = new DeliveryForecastOptions();
        options.ignoredLineItemIds = new long[]{};
        DeliveryForecast forecast = forecastService.getDeliveryForecastByIds(
            new long[] {lineItemId1, lineItemId2}, options);

        // Display results.
        foreach (LineItemDeliveryForecast lineItemForecast in forecast.lineItemDeliveryForecasts) {
          String unitType = lineItemForecast.unitType.GetType().Name.ToLower();
          Console.WriteLine("Forecast for line item {0}:", lineItemForecast.lineItemId);
          Console.WriteLine("\t{0} {1} matched", lineItemForecast.matchedUnits, unitType);
          Console.WriteLine("\t{0} {1} delivered", lineItemForecast.deliveredUnits, unitType);
          Console.WriteLine("\t{0} {1} predicted", lineItemForecast.predictedDeliveryUnits,
              unitType);
        }
        // [END forecasting_5] MOE:strip_line
      } catch (Exception e) {
        Console.WriteLine("Failed to get forecast by id. Exception says \"{0}\"", e.Message);
      }
    }
  }
}
