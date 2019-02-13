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
    public class CreateTrafficAdjustments : SampleBase
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
            using (NetworkService networkService = user.GetService<NetworkService>())
            {
                String rootAdUnitId = networkService.getCurrentNetwork().effectiveRootAdUnitId;

                TrafficTimeSeriesFilterCriteria criteria = new TrafficTimeSeriesFilterCriteria() {
                    // Target all USA traffic.
                    targeting = new Targeting() {
                        inventoryTargeting = new InventoryTargeting() {
                            targetedAdUnits = new AdUnitTargeting[] {
                                new AdUnitTargeting() {
                                    adUnitId = rootAdUnitId,
                                    includeDescendants = true
                                }
                            }
                        },
                        geoTargeting = new GeoTargeting() {
                            targetedLocations = new Location[] {
                                new Location() {
                                    id = 2840 // United States
                                }
                            }
                        }
                    },
                    // Adjust only 1920x1080 video traffic.
                    adUnitSizes = new AdUnitSize[] {
                        new AdUnitSize() {
                            size = new Size() {
                                width = 1920,
                                height = 1080
                            },
                            environmentType = EnvironmentType.VIDEO_PLAYER
                        }
                    }
                };

                // Create a new historical adjustment targeting next year's January with
                // 105% of this year's January.
                var januarySegment = new TrafficForecastAdjustmentSegment() {
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
                                day = 30
                            }
                        },
                        targetDateRange = new DateRange() {
                            startDate = new Date() {
                                year = System.DateTime.Now.Year + 1,
                                month = 1,
                                day = 30
                            },
                            endDate = new Date() {
                                year = new System.DateTime().Year + 1,
                                month = 1,
                                day = 30
                            }
                        },
                        milliPercentMultiplier = 105_000L
                    }
                };

                // Create a new absolute adjustment of 500,000 ad opportunities for Christmas
                // and 1M ad opportunities for boxing day.
                var holidaySegment = new TrafficForecastAdjustmentSegment() {
                    basisType = BasisType.ABSOLUTE,
                    adjustmentTimeSeries = new TimeSeries() {
                        timeSeriesDateRange = new DateRange() {
                            startDate = new Date() {
                                year = System.DateTime.Now.Year,
                                month = 12,
                                day = 24
                            },
                            endDate = new Date() {
                                year = System.DateTime.Now.Year,
                                month = 12,
                                day = 25
                            }
                        },
                        valuePeriodType = PeriodType.DAILY,
                        timeSeriesValues = new long[] { 500_000, 1_000_000}
                    }
                };

                // Create a new absolute adjustment of 900,000 ad opportunities for the first
                // week in September.
                var septemberSegment = new TrafficForecastAdjustmentSegment() {
                    basisType = BasisType.ABSOLUTE,
                    adjustmentTimeSeries = new TimeSeries() {
                        timeSeriesDateRange = new DateRange() {
                            startDate = new Date() {
                                year = System.DateTime.Now.Year + 1,
                                month = 9,
                                day = 1
                            },
                            endDate = new Date() {
                                year = System.DateTime.Now.Year + 1,
                                month = 9,
                                day = 7
                            }
                        },
                        valuePeriodType = PeriodType.CUSTOM,
                        timeSeriesValues = new long[] { 900_000 }
                    }
                };


                TrafficForecastAdjustment adjustment = new TrafficForecastAdjustment() {
                    filterCriteria = criteria,
                    forecastAdjustmentSegments = new TrafficForecastAdjustmentSegment[] {
                        januarySegment, holidaySegment, septemberSegment
                    }
                };

                TrafficForecastAdjustment[] adjustments =
                    adjustmentService.updateTrafficAdjustments(
                        new TrafficForecastAdjustment[] { adjustment });

                foreach (TrafficForecastAdjustment updatedAdjustment in adjustments)
                {
                    Console.WriteLine("Adjustment with ID {0} and {1} segments was "
                        + "created or updated.",
                        updatedAdjustment.id,
                        updatedAdjustment.forecastAdjustmentSegments.Length);
                }
            }
        }
    }
}
