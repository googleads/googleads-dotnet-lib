// Copyright 2018, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.AdManager.Lib;
using Google.Api.Ads.AdManager.v201811;

using System;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v201811
{
    /// <summary>
    /// This code example gets a delivery forecast for existing line items. To determine
    /// which line items exist, run GetAllLineItems.cs.
    /// </summary>
    public class GetDeliveryForecastByIds : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example gets a delivery forecast for existing line items. " +
                    "To determine which line items exist, run GetAllLineItems.cs.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            GetDeliveryForecastByIds codeExample = new GetDeliveryForecastByIds();
            Console.WriteLine(codeExample.Description);
            codeExample.Run(new AdManagerUser());
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (ForecastService forecastService = user.GetService<ForecastService>())
            {

                // Set the line item to get a forecast for.
                long lineItemId1 = long.Parse(_T("INSERT_LINE_ITEM_ID_HERE"));
                long lineItemId2 = long.Parse(_T("INSERT_LINE_ITEM_ID_HERE"));

                try
                {
                    // Get a delivery forecast for the line items.
                    DeliveryForecastOptions options = new DeliveryForecastOptions();
                    options.ignoredLineItemIds = new long[]
                    {
                    };
                    DeliveryForecast forecast = forecastService.getDeliveryForecastByIds(new long[]
                    {
                        lineItemId1,
                        lineItemId2
                    }, options);

                    // Display results.
                    foreach (LineItemDeliveryForecast lineItemForecast in forecast
                        .lineItemDeliveryForecasts)
                    {
                        String unitType = lineItemForecast.unitType.GetType().Name.ToLower();
                        Console.WriteLine("Forecast for line item {0}:",
                            lineItemForecast.lineItemId);
                        Console.WriteLine("\t{0} {1} matched", lineItemForecast.matchedUnits,
                            unitType);
                        Console.WriteLine("\t{0} {1} delivered", lineItemForecast.deliveredUnits,
                            unitType);
                        Console.WriteLine("\t{0} {1} predicted",
                            lineItemForecast.predictedDeliveryUnits, unitType);
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to get forecast by id. Exception says \"{0}\"",
                        e.Message);
                }
            }
        }
    }
}
