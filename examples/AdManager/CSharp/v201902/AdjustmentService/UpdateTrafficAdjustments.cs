// Copyright 2019, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdManager.Util.v201902;
using Google.Api.Ads.AdManager.v201902;

using System;
using System.Linq;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v201902
{
    /// <summary>
    /// This example adds a historical adjustment of 110% for New Years Day Traffic.
    /// </summary>
    public class UpdateTrafficAdjustments : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get { return "This example adds a historical adjustment of 110% for "
            + "New Years Day Traffic."; }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            GetAllTrafficAdjustments codeExample = new GetAllTrafficAdjustments();
            Console.WriteLine(codeExample.Description);
            try
            {
                codeExample.Run(new AdManagerUser());
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to update adjustments. Exception says \"{0}\"", e.Message);
            }
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (AdjustmentService adjustmentService = user.GetService<AdjustmentService>())
            {
                // Set the ID of the adjustment to update.
                long adjustmentId = long.Parse(_T("INSERT_ADJUSTMENT_ID_HERE"));

                // Create a statement to select adjustments.
                StatementBuilder statementBuilder =
                    new StatementBuilder()
                        .Where("id = :id")
                        .OrderBy("id ASC")
                        .Limit(1)
                        .AddValue("id", adjustmentId);

                TrafficForecastAdjustmentPage page = adjustmentService
                    .getTrafficAdjustmentsByStatement(statementBuilder.ToStatement());

                TrafficForecastAdjustment adjustment = page.results[0];

                // Create a new historical adjustment segment for New Years Day.
                TrafficForecastAdjustmentSegment segment = new TrafficForecastAdjustmentSegment() {
                    basisType = BasisType.HISTORICAL,
                    historicalAdjustment = new HistoricalAdjustment() {
                        referenceDateRange = new DateRange() {
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
                        targetDateRange = new DateRange() {
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
                        milliPercentMultiplier = 110_000L
                    }
                };

                // Add the new historical segment to the adjustment.
                adjustment.forecastAdjustmentSegments = adjustment.forecastAdjustmentSegments
                    .Concat(new TrafficForecastAdjustmentSegment[] { segment }).ToArray();

                TrafficForecastAdjustment[] adjustments =
                    adjustmentService.updateTrafficAdjustments(
                        new TrafficForecastAdjustment[] { adjustment });

                foreach (TrafficForecastAdjustment updatedAdjustment in adjustments)
                {
                    Console.WriteLine("Adjustment with ID {0} and {1} segments was found.",
                        updatedAdjustment.id,
                        updatedAdjustment.forecastAdjustmentSegments.Length);
                }
            }
        }
    }
}
