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
using Google.Api.Ads.AdManager.Util.v201808;
using Google.Api.Ads.AdManager.v201808;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v201808
{
    /// <summary>
    /// This code example updates an ad unit's sizes by adding a banner ad size.
    /// </summary>
    public class UpdateAdUnits : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example updates an ad unit's sizes by adding a banner ad size.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            UpdateAdUnits codeExample = new UpdateAdUnits();
            Console.WriteLine(codeExample.Description);

            // Set the ID of the ad unit to update.
            long adUnitId = long.Parse(_T("INSERT_AD_UNIT_ID_HERE"));
            codeExample.Run(new AdManagerUser(), adUnitId);
        }

        /// <summary>
        /// Run the sample code.
        /// </summary>
        public void Run(AdManagerUser user, long adUnitId)
        {
            using (InventoryService inventoryService = user.GetService<InventoryService>())
            {
                // Create a statement to get the ad unit.
                StatementBuilder statementBuilder = new StatementBuilder()
                    .Where("id = :id")
                    .OrderBy("id ASC")
                    .Limit(1)
                    .AddValue("id", adUnitId);

                try
                {
                    // Get ad units by statement.
                    AdUnitPage page =
                        inventoryService.getAdUnitsByStatement(statementBuilder.ToStatement());

                    // Create a 480x60 web ad unit size.
                    AdUnitSize adUnitSize = new AdUnitSize()
                    {
                        size = new Size()
                        {
                            width = 480,
                            height = 60
                        },
                        environmentType = EnvironmentType.BROWSER
                    };

                    AdUnit adUnit = page.results[0];
                    adUnit.adUnitSizes = new AdUnitSize[]
                    {
                        adUnitSize
                    };

                    // Update the ad units on the server.
                    AdUnit[] updatedAdUnits = inventoryService.updateAdUnits(new AdUnit[]
                    {
                        adUnit
                    });

                    foreach (AdUnit updatedAdUnit in updatedAdUnits)
                    {
                        List<string> adUnitSizeStrings = new List<string>();
                        foreach (AdUnitSize size in updatedAdUnit.adUnitSizes)
                        {
                            adUnitSizeStrings.Add(size.fullDisplayString);
                        }

                        Console.WriteLine(
                            "Ad unit with ID \"{0}\", name \"{1}\", and sizes [{2}] was " +
                            "updated.", updatedAdUnit.id, updatedAdUnit.name,
                            String.Join(",", adUnitSizeStrings));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to update ad units. Exception says \"{0}\"",
                        e.Message);
                }
            }
        }
    }
}
