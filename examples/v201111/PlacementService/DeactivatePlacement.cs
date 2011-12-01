// Copyright 2011, Google Inc. All Rights Reserved.
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

// Author: api.anash@gmail.com (Anash P. Oommen)

using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.Util.v201111;
using Google.Api.Ads.Dfp.v201111;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.Dfp.Examples.v201111 {
  /// <summary>
  /// This code example deactivates all active placements. To determine which
  /// placements exist, run GetAllPlacements.cs.
  ///
  /// Tags: PlacementService.getPlacementsByStatement
  /// Tags: PlacementService.performPlacementAction
  /// </summary>
  class DeactivatePlacement : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example deactivates all active placements. To determine which " +
            "placements exist, run GetAllPlacements.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new DeactivatePlacement();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the PlacementService.
      PlacementService placementService =
          (PlacementService) user.GetService(DfpService.v201111.PlacementService);

      // Create Statement text to select active placements.
      String statementText = "WHERE status = :status LIMIT 500";
      Statement statement = new StatementBuilder("").AddValue("status",
          InventoryStatus.ACTIVE.ToString()).ToStatement();

      // Sets defaults for page and offset.
      PlacementPage page = new PlacementPage();
      int offset = 0;
      List<string> placementIds = new List<string>();

      try {
        do {
          // Create a Statement to page through active placements.
          statement.query = string.Format("{0} OFFSET {1}", statementText, offset);

          // Get placements by Statement.
          page = placementService.getPlacementsByStatement(statement);

          if (page.results != null && page.results.Length > 0) {
            int i = page.startIndex;
            foreach (Placement placement in page.results) {
              Console.WriteLine("{0}) Placement with ID ='{1}', name ='{2}', and status ='{3}'" +
                  " will be deactivated.", i, placement.id, placement.name, placement.status);
              placementIds.Add(placement.id.ToString());
              i++;
            }
          }

          offset += 500;
        } while (offset < page.totalResultSetSize);

        Console.WriteLine("Number of placements to be deactivated: {0}", placementIds.Count);

        if (placementIds.Count > 0) {
          // Create action Statement.
          statement = new StatementBuilder(
              string.Format("WHERE id IN ({0})", string.Join(",", placementIds.ToArray()))).
              ToStatement();

          // Create action.
          DeactivatePlacements action = new DeactivatePlacements();

          // Perform action.
          UpdateResult result = placementService.performPlacementAction(action, statement);

          // Display results.
          if (result != null && result.numChanges > 0) {
            Console.WriteLine("Number of placements deactivated: {0}", result.numChanges);
          } else {
            Console.WriteLine("No placements were deactivated.");
          }
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to deactivate placements. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
