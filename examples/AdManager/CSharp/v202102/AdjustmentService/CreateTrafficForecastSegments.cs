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
using Google.Api.Ads.AdManager.Util.v202102;
using Google.Api.Ads.AdManager.v202102;

using System;
using System.Linq;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v202102
{
    /// <summary>
    /// This example creates a traffic forecast segment for all ad units in the United States."
    /// </summary>
    public class CreateTrafficForecastSegments : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get { return "This example creates a traffic forecast segment for all ad units in the "
              + "United States"; }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            CreateTrafficForecastSegments codeExample = new CreateTrafficForecastSegments();
            Console.WriteLine(codeExample.Description);
            try
            {
                codeExample.Run(new AdManagerUser());
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to create segments. Exception says \"{0}\"", e.Message);
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

                TrafficForecastSegment segment = new TrafficForecastSegment() {
                    name = "Forecast segment #" + new Random().Next(int.MaxValue),
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
                };

                TrafficForecastSegment[] createdSegments =
                    adjustmentService.createTrafficForecastSegments(
                        new TrafficForecastSegment[] { segment });

                foreach (TrafficForecastSegment createdSegment in createdSegments )
                {
                    Console.WriteLine("Traffic forecast segment with ID {0} and name '{1}' was "
                        + "created.",
                        createdSegment.id,
                        createdSegment.name);
                }
            }
        }
    }
}
