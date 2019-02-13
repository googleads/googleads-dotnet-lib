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
using Google.Api.Ads.AdManager.Util.v201902;
using Google.Api.Ads.AdManager.v201902;

using System;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v201902
{
    /// <summary>
    /// This code example updates the description of a placement.
    /// </summary>
    public class UpdatePlacements : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get { return "This code example updates the description of a placement."; }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            UpdatePlacements codeExample = new UpdatePlacements();
            Console.WriteLine(codeExample.Description);

            // Set the ID of the placement to update.
            long placementId = long.Parse(_T("INSERT_PLACEMENT_ID_HERE"));
            codeExample.Run(new AdManagerUser(), placementId);
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user, long placementId)
        {
            using (PlacementService placementService = user.GetService<PlacementService>())
            {
                // Create a statement to select a placement by ID.
                StatementBuilder statementBuilder = new StatementBuilder()
                    .Where("id = :id")
                    .OrderBy("id ASC")
                    .Limit(1)
                    .AddValue("id", placementId);

                try
                {
                    // Get placements by statement.
                    PlacementPage page =
                        placementService.getPlacementsByStatement(statementBuilder.ToStatement());

                    if (page.results != null)
                    {
                        Placement placement = page.results[0];

                        // Update local placement object by changing the description.
                        placement.description = "This placement includes all leaderboards.";

                        // Update the placement on the server.
                        Placement[] placements = placementService.updatePlacements(new Placement[]
                        {
                            placement
                        });

                        // Display results.
                        if (placements != null)
                        {
                            foreach (Placement updatedPlacement in placements)
                            {
                                Console.WriteLine(
                                    "A placement with ID \"{0}\", name \"{1}\", and description " +
                                    "\"{2}\" was updated.", updatedPlacement.id,
                                    updatedPlacement.name, updatedPlacement.description);
                            }
                        }
                        else
                        {
                            Console.WriteLine("No placements updated.");
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to update placements. Exception says \"{0}\"",
                        e.Message);
                }
            }
        }
    }
}
