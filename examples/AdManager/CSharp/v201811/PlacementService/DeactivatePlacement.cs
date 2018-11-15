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
using Google.Api.Ads.AdManager.Util.v201811;
using Google.Api.Ads.AdManager.v201811;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v201811
{
    /// <summary>
    /// This code example deactivates a placement. To determine which
    /// placements exist, run GetAllPlacements.cs.
    /// </summary>
    public class DeactivatePlacement : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example deactivates a placement. To determine which " +
                    "placements exist, run GetAllPlacements.cs.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            DeactivatePlacement codeExample = new DeactivatePlacement();
            Console.WriteLine(codeExample.Description);
            codeExample.Run(new AdManagerUser());
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (PlacementService placementService = user.GetService<PlacementService>())
            {
                // Create statement to select active placements.
                StatementBuilder statementBuilder = new StatementBuilder()
                    .Where("status = :status")
                    .OrderBy("id ASC")
                    .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT)
                    .AddValue("status", InventoryStatus.ACTIVE.ToString());

                // Sets default for page.
                PlacementPage page = new PlacementPage();
                List<string> placementIds = new List<string>();

                try
                {
                    do
                    {
                        // Get placements by statement.
                        page = placementService.getPlacementsByStatement(statementBuilder
                            .ToStatement());

                        if (page.results != null)
                        {
                            int i = page.startIndex;
                            foreach (Placement placement in page.results)
                            {
                                Console.WriteLine(
                                    "{0}) Placement with ID ='{1}', name ='{2}', and " +
                                    "status ='{3}' will be deactivated.",
                                    i, placement.id, placement.name, placement.status);
                                placementIds.Add(placement.id.ToString());
                                i++;
                            }
                        }

                        statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
                    } while (statementBuilder.GetOffset() < page.totalResultSetSize);

                    Console.WriteLine("Number of placements to be deactivated: {0}",
                        placementIds.Count);

                    if (placementIds.Count > 0)
                    {
                        // Modify statement for action.
                        statementBuilder.RemoveLimitAndOffset();

                        // Create action.
                        DeactivatePlacements action = new DeactivatePlacements();

                        // Perform action.
                        UpdateResult result =
                            placementService.performPlacementAction(action,
                                statementBuilder.ToStatement());

                        // Display results.
                        if (result != null && result.numChanges > 0)
                        {
                            Console.WriteLine("Number of placements deactivated: {0}",
                                result.numChanges);
                        }
                        else
                        {
                            Console.WriteLine("No placements were deactivated.");
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to deactivate placements. Exception says \"{0}\"",
                        e.Message);
                }
            }
        }
    }
}
