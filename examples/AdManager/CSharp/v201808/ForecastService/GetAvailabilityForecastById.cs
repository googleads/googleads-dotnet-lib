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
using Google.Api.Ads.AdManager.v201808;

using System;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v201808
{
    /// <summary>
    /// This code example gets a forecast for an existing line item. To determine
    /// which line items exist, run GetAllLineItems.cs.
    /// </summary>
    public class GetAvailabilityForecastById : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example gets a forecast for an existing line item. " +
                    "To determine which line items exist, run GetAllLineItems.cs.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            GetAvailabilityForecastById codeExample = new GetAvailabilityForecastById();
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
                long lineItemId = long.Parse(_T("INSERT_LINE_ITEM_ID_HERE"));

                try
                {
                    // Get forecast for line item.
                    AvailabilityForecastOptions options = new AvailabilityForecastOptions();
                    options.includeContendingLineItems = true;
                    options.includeTargetingCriteriaBreakdown = true;
                    AvailabilityForecast forecast =
                        forecastService.getAvailabilityForecastById(lineItemId, options);

                    // Display results.
                    long matched = forecast.matchedUnits;
                    double availablePercent =
                        (double) (forecast.availableUnits / (matched * 1.0)) * 100;
                    String unitType = forecast.unitType.ToString().ToLower();

                    Console.WriteLine("{0} {1} matched.\n{2} % {3} available.", matched, unitType,
                        availablePercent, unitType);
                    if (forecast.possibleUnitsSpecified)
                    {
                        double possiblePercent =
                            (double) (forecast.possibleUnits / (matched * 1.0)) * 100;
                        Console.WriteLine(possiblePercent + "% " + unitType + " possible.\n");
                    }

                    Console.WriteLine("{0} contending line items.",
                        (forecast.contendingLineItems != null)
                            ? forecast.contendingLineItems.Length
                            : 0);
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
