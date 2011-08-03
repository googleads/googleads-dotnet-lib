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
using Google.Api.Ads.Dfp.Util.v201103;
using Google.Api.Ads.Dfp.v201103;

using System;

namespace Google.Api.Ads.Dfp.Examples.v201103 {
  /// <summary>
  /// This code example gets all active placements by using a Statement. To
  /// create a placement, run CreatePlacements.cs.
  ///
  /// Tags: PlacementService.getPlacementsByStatement
  /// </summary>
  class GetPlacementsByStatement : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets all active placements by using a Statement. To create a " +
            "placement, run CreatePlacements.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetPlacementsByStatement();
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
          (PlacementService) user.GetService(DfpService.v201103.PlacementService);

      // Create a Statement to only select active placements.
      Statement statement = new StatementBuilder("WHERE status = :status LIMIT 500").AddValue(
          "status", InventoryStatus.ACTIVE.ToString()).ToStatement();

      try {
        // Get placements by Statement.
        PlacementPage page = placementService.getPlacementsByStatement(statement);

        // Display results.
        if (page.results != null && page.results.Length > 0) {
          int i = page.startIndex;
          foreach (Placement placement in page.results) {
            Console.WriteLine("{0}) Placement with ID = '{1}', name ='{2}', and status = '{3}' " +
              "was found.", i, placement.id, placement.name, placement.status);
            i++;
          }
        }

        Console.WriteLine("Number of results found: {0}", page.totalResultSetSize);
      } catch (Exception ex) {
        Console.WriteLine("Failed to get placement by Statement. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
