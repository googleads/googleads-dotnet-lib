// Copyright 2019 Google LLC
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
using System.Collections.Generic;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v202311
{
    /// <summary>
    /// This code example deactivates all active ad units. To determine which ad
    /// units exist, run GetAllAdUnits.cs or GetInventoryTree.cs.
    /// </summary>
    public class DeActivateAdUnits : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example deactivates an ad unit. To determine which ad units " +
                    "exist, run GetAllAdUnits.cs or GetInventoryTree.cs.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            DeActivateAdUnits codeExample = new DeActivateAdUnits();
            Console.WriteLine(codeExample.Description);
            codeExample.Run(new AdManagerUser());
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (InventoryService inventoryService = user.GetService<InventoryService>())
            {
                // Set the ID of the ad unit to deactivate.
                int adUnitId = int.Parse(_T("INSERT_AD_UNIT_ID_HERE"));

                // Create a statement to select the ad unit.
                StatementBuilder statementBuilder = new StatementBuilder()
                    .Where("id = :id")
                    .OrderBy("id ASC")
                    .Limit(1)
                    .AddValue("id", adUnitId);

                // Set default for page.
                AdUnitPage page = new AdUnitPage();
                List<string> adUnitIds = new List<string>();

                try
                {
                    do
                    {
                        // Get ad units by statement.
                        page = inventoryService.getAdUnitsByStatement(
                            statementBuilder.ToStatement());

                        if (page.results != null)
                        {
                            int i = page.startIndex;
                            foreach (AdUnit adUnit in page.results)
                            {
                                Console.WriteLine(
                                    "{0}) Ad unit with ID ='{1}', name = {2} and status = {3} " +
                                    "will be deactivated.",
                                    i, adUnit.id, adUnit.name, adUnit.status);
                                adUnitIds.Add(adUnit.id);
                                i++;
                            }
                        }

                        statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
                    } while (statementBuilder.GetOffset() < page.totalResultSetSize);

                    Console.WriteLine("Number of ad units to be deactivated: {0}", adUnitIds.Count);

                    // Modify statement for action.
                    statementBuilder.RemoveLimitAndOffset();

                    // Create action.
                    DeactivateAdUnits action = new DeactivateAdUnits();

                    // Perform action.
                    UpdateResult result =
                        inventoryService.performAdUnitAction(action,
                            statementBuilder.ToStatement());

                    // Display results.
                    if (result != null && result.numChanges > 0)
                    {
                        Console.WriteLine("Number of ad units deactivated: {0}", result.numChanges);
                    }
                    else
                    {
                        Console.WriteLine("No ad units were deactivated.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to deactivate ad units. Exception says \"{0}\"",
                        e.Message);
                }
            }
        }
    }
}
