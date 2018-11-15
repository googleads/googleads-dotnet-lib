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

using System;
using Google.Api.Ads.AdManager.Lib;
using Google.Api.Ads.AdManager.Util.v201811;
using Google.Api.Ads.AdManager.v201811;
using DateTime = Google.Api.Ads.AdManager.v201811.DateTime;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v201811
{
    /// <summary>
    /// This code example gets a forecast for a prospective line item.
    /// </summary>
    public class GetAvailabilityForecast : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example gets a forecast for a prospective line item.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            GetAvailabilityForecast codeExample = new GetAvailabilityForecast();
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

                // Set the ID of the advertiser (company) to forecast for. Setting an advertiser
                // will cause the forecast to apply the appropriate unified blocking rules.
                long advertiserId = long.Parse(_T("INSERT_ADVERTISER_ID_HERE"));

                String rootAdUnitId = networkService.getCurrentNetwork().effectiveRootAdUnitId;

                System.DateTime tomorrow = System.DateTime.Now.AddDays(1);

                // Create prospective line item.
                LineItem lineItem = new LineItem()
                {
                    targeting = new Targeting()
                    {
                        inventoryTargeting = new InventoryTargeting()
                        {
                            targetedAdUnits = new AdUnitTargeting[] {
                                new AdUnitTargeting()
                                {
                                    adUnitId = rootAdUnitId,
                                    includeDescendants = true
                                }
                            }
                        }
                    },
                    creativePlaceholders = new CreativePlaceholder[] {
                        new CreativePlaceholder()
                        {
                            size = new Size()
                            {
                                width = 300,
                                height = 250
                            }
                        }
                    },
                    lineItemType = LineItemType.SPONSORSHIP,
                    // Set the line item to run for 5 days.
                    startDateTime = DateTimeUtilities.FromDateTime(
                        tomorrow, "America/New_York"),
                    endDateTime = DateTimeUtilities.FromDateTime(
                        tomorrow.AddDays(5), "America/New_York"),
                    // Set the cost type to match the unit type.
                    costType = CostType.CPM,
                    primaryGoal = new Goal()
                    {
                        goalType = GoalType.DAILY,
                        unitType = UnitType.IMPRESSIONS,
                        units = 50L
                    }
                };

                try
                {
                    // Get availability forecast.
                    AvailabilityForecastOptions options = new AvailabilityForecastOptions()
                    {
                        includeContendingLineItems = true,
                        // Targeting criteria breakdown can only be included if breakdowns
                        // are not speficied.
                        includeTargetingCriteriaBreakdown = false,
                        breakdown = new ForecastBreakdownOptions
                        {
                            timeWindows = new DateTime[] {
                                lineItem.startDateTime,
                                DateTimeUtilities.FromDateTime(tomorrow.AddDays(1),
                                    "America/New_York"),
                                DateTimeUtilities.FromDateTime(tomorrow.AddDays(2),
                                    "America/New_York"),
                                DateTimeUtilities.FromDateTime(tomorrow.AddDays(3),
                                    "America/New_York"),
                                DateTimeUtilities.FromDateTime(tomorrow.AddDays(4),
                                    "America/New_York"),
                                lineItem.endDateTime
                            },
                            targets = new ForecastBreakdownTarget[] {
                                new ForecastBreakdownTarget()
                                {
                                    // Optional name field to identify this breakdown
                                    // in the response.
                                    name = "United States",
                                    targeting = new Targeting()
                                    {
                                        inventoryTargeting = new InventoryTargeting()
                                        {
                                            targetedAdUnits = new AdUnitTargeting[] {
                                                new AdUnitTargeting()
                                                {
                                                    adUnitId = rootAdUnitId,
                                                    includeDescendants = true
                                                }
                                            }
                                        },
                                        geoTargeting = new GeoTargeting()
                                        {
                                            targetedLocations = new Location[] {
                                                new Location() { id = 2840L }
                                            }
                                        }
                                    }
                                }, new ForecastBreakdownTarget()
                                {
                                    // Optional name field to identify this breakdown
                                    // in the response.
                                    name = "Geneva",
                                    targeting = new Targeting()
                                    {
                                        inventoryTargeting = new InventoryTargeting()
                                        {
                                            targetedAdUnits = new AdUnitTargeting[] {
                                                new AdUnitTargeting()
                                                {
                                                    adUnitId = rootAdUnitId,
                                                    includeDescendants = true
                                                }
                                            }
                                        },
                                        geoTargeting = new GeoTargeting()
                                        {
                                            targetedLocations = new Location[] {
                                                new Location () { id = 20133L }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    };
                    ProspectiveLineItem prospectiveLineItem = new ProspectiveLineItem()
                    {
                        advertiserId = advertiserId,
                        lineItem = lineItem
                    };
                    AvailabilityForecast forecast =
                      forecastService.getAvailabilityForecast(prospectiveLineItem, options);

                    // Display results.
                    long matched = forecast.matchedUnits;
                    double availablePercent =
                        (double)(forecast.availableUnits / (matched * 1.0)) * 100;
                    String unitType = forecast.unitType.ToString().ToLower();
                    Console.WriteLine($"{matched} {unitType} matched.");
                    Console.WriteLine($"{availablePercent}% {unitType} available.");

                    if (forecast.possibleUnitsSpecified)
                    {
                        double possiblePercent =
                            (double)(forecast.possibleUnits / (matched * 1.0)) * 100;
                        Console.WriteLine($"{possiblePercent}% {unitType} possible.");
                    }
                    var contendingLineItems =
                        forecast.contendingLineItems ?? new ContendingLineItem[] { };
                    Console.WriteLine($"{contendingLineItems.Length} contending line items.");

                    if (forecast.breakdowns != null)
                    {
                        foreach (ForecastBreakdown breakdown in forecast.breakdowns)
                        {
                            Console.WriteLine("Forecast breakdown for {0} to {1}",
                                DateTimeUtilities.ToString(breakdown.startTime, "yyyy-MM-dd"),
                                DateTimeUtilities.ToString(breakdown.endTime, "yyyy-MM-dd"));
                            foreach (ForecastBreakdownEntry entry in breakdown.breakdownEntries)
                            {
                                Console.WriteLine($"\t{entry.name}");
                                long breakdownMatched = entry.forecast.matched;
                                Console.WriteLine($"\t\t{breakdownMatched} {unitType} matched.");
                                if (breakdownMatched > 0)
                                {
                                    long breakdownAvailable = entry.forecast.available;
                                    double breakdownAvailablePercent =
                                    (double)(breakdownAvailable / (breakdownMatched * 1.0)) * 100;
                                    Console.WriteLine(
                                        $"\t\t{breakdownAvailablePercent}% {unitType} available");
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to get forecast. Exception says \"{0}\"", e.Message);
                }
            }
        }
    }
}
