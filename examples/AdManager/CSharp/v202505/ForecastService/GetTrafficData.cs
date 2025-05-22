// Copyright 2020 Google LLC
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

using System;
using Google.Api.Ads.AdManager.Lib;
using Google.Api.Ads.AdManager.Util.v202505;
using Google.Api.Ads.AdManager.v202505;
using DateTime = Google.Api.Ads.AdManager.v202505.DateTime;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v202505
{
    /// <summary>
    /// This example gets run-of-network traffic for the previous and next 7 days.
    /// </summary>
    public class GetTrafficData : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This example gets run-of-network traffic for the previous and next 7 days.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            GetTrafficData codeExample = new GetTrafficData();
            Console.WriteLine(codeExample.Description);
            codeExample.Run(new AdManagerUser());
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (ForecastService forecastService = user.GetService<ForecastService>())
            using (NetworkService networkService = user.GetService<NetworkService>())
            {

                String rootAdUnitId = networkService.getCurrentNetwork().effectiveRootAdUnitId;

                // Set the date range to include
                System.DateTime startDate = System.DateTime.Today.AddDays(-7);
                System.DateTime endDate = System.DateTime.Today.AddDays(7);

                TrafficDataRequest trafficDataRequest = new TrafficDataRequest() {
                    requestedDateRange = new DateRange() {
                        startDate =
                            DateTimeUtilities.FromDateTime(startDate, "America/New_York").date,
                        endDate = DateTimeUtilities.FromDateTime(endDate, "America/New_York").date
                    },
                    targeting = new Targeting() {
                        inventoryTargeting = new InventoryTargeting()
                        {
                            targetedAdUnits = new AdUnitTargeting[] {
                                new AdUnitTargeting() {
                                 adUnitId = rootAdUnitId,
                                 includeDescendants = true
                                }
                            }
                        }
                    }
                };

                try
                {
                    TrafficDataResponse trafficData =
                        forecastService.getTrafficData(trafficDataRequest);

                    TimeSeries historicalTimeSeries = trafficData.historicalTimeSeries;
                    if (historicalTimeSeries != null)
                    {
                        Date historicalStartDate =
                              historicalTimeSeries.timeSeriesDateRange.startDate;
                        System.DateTime startDateTime = new System.DateTime(
                            historicalStartDate.year,
                            historicalStartDate.month,
                            historicalStartDate.day);
                        for (int i = 0; i < historicalTimeSeries.values.Length; i++) {
                          Console.WriteLine("{0}: {1} historical ad opportunities",
                              startDateTime.AddDays(i).ToString("yyyy-MM-dd"),
                              historicalTimeSeries.values[i]);
                        }
                    }

                    TimeSeries forecastedTimeSeries = trafficData.forecastedTimeSeries;
                    if (forecastedTimeSeries != null)
                    {
                        Date forecastedStartDate =
                              forecastedTimeSeries.timeSeriesDateRange.startDate;
                        System.DateTime startDateTime = new System.DateTime(
                            forecastedStartDate.year,
                            forecastedStartDate.month,
                            forecastedStartDate.day);
                        for (int i = 0; i < forecastedTimeSeries.values.Length; i++) {
                          Console.WriteLine("{0}: {1} forecasted ad opportunities",
                              startDateTime.AddDays(i).ToString("yyyy-MM-dd"),
                              forecastedTimeSeries.values[i]);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to get traffic data. Exception says \"{0}\"",
                        e.Message);
                }
            }
        }
    }
}
