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

using Google.Api.Ads.AdManager.Lib;
using Google.Api.Ads.AdManager.Util.v202311;
using Google.Api.Ads.AdManager.v202311;

using System;
using System.Linq;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v202311
{
    /// <summary>
    /// This example creates a new Forecast Adjustment of 110% for New Years Day Traffic.
    /// </summary>
    public class CreateTrafficAdjustments : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get { return "This example creates a new Forecast Adjustment of 110% for "
            + "New Years Day Traffic."; }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            CreateTrafficAdjustments codeExample = new CreateTrafficAdjustments();
            Console.WriteLine(codeExample.Description);

            // Set the ID of the traffic forecast segment to create the adjustment for.
            long trafficForecastSegmentId = 
                long.Parse(_T("INSERT_TRAFFIC_FORECAST_SEGMENT_ID_HERE"));

            try
            {
                codeExample.Run(new AdManagerUser(), trafficForecastSegmentId);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to create adjustments. Exception says \"{0}\"", e.Message);
            }
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user, long trafficForecastSegmentId)
        {
            using (AdjustmentService adjustmentService = user.GetService<AdjustmentService>())
            {

                ForecastAdjustment forecastAdjustment = new ForecastAdjustment() {
                    name = "Forecast Adjustment #" + new Random().Next(int.MaxValue),
                    trafficForecastSegmentId = trafficForecastSegmentId,
                    status = ForecastAdjustmentStatus.ACTIVE,
                    // Adjust next year's New Year's Day values
                    dateRange = new DateRange() {
                        startDate = new Date() {
                            year = System.DateTime.Now.Year + 1,
                            month = 1,
                            day = 1
                         },
                        endDate = new Date() {
                            year = System.DateTime.Now.Year + 1,
                            month = 1,
                            day = 1
                        }
                    },
                    volumeType = ForecastAdjustmentVolumeType.HISTORICAL_BASIS_VOLUME,
                    historicalBasisVolumeSettings = new HistoricalBasisVolumeSettings() {
                        useParentTrafficForecastSegmentTargeting = true,
                        // Base the adjustment on this year's New Years's Day values
                        historicalDateRange = new DateRange() {
                            startDate = new Date() {
                                year = System.DateTime.Now.Year,
                                month = 1,
                                day = 1
                            },
                            endDate = new Date() {
                                year = System.DateTime.Now.Year,
                                month = 1,
                                day = 1
                            }
                        },
                        multiplierMilliPercent = 110_000L
                    }
                };

                ForecastAdjustment[] adjustments = adjustmentService.createForecastAdjustments(
                        new ForecastAdjustment[] { forecastAdjustment });

                foreach (ForecastAdjustment createdAdjustment in adjustments)
                {
                    Console.WriteLine("Adjustment with ID {0} and name '{1}' was created.",
                        createdAdjustment.id,
                        createdAdjustment.name);
                }
            }
        }
    }
}
