// Copyright 2015, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.Util.v201602;
using Google.Api.Ads.Dfp.v201602;

using System;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201602 {
  /// <summary>
  /// This code example updates all placements to allow for AdSense targeting
  /// up to the first 500. To determine which placements exist, run
  /// GetAllPlacements.cs.
  /// </summary>
  class UpdatePlacements : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example updates all placements to allow for AdSense targeting up to " +
            "the first 500. To determine which placements exist, run GetAllPlacements.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new UpdatePlacements();
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
          (PlacementService) user.GetService(DfpService.v201602.PlacementService);

      // Set the ID of the placement to update.
      long placementId = long.Parse(_T("INSERT_PLACEMENT_ID_HERE"));

      // Create a statement to select a placement by ID.
      StatementBuilder statementBuilder = new StatementBuilder()
          .Where("id = :id")
          .OrderBy("id ASC")
          .Limit(1)
          .AddValue("id", placementId);

      try {
        // Get placements by statement.
        PlacementPage page = placementService.getPlacementsByStatement(
            statementBuilder.ToStatement());

        if(page.results != null) {
          Placement placement = page.results[0];

          // Update local placement object by enabling AdSense targeting.
          placement.targetingDescription = (string.IsNullOrEmpty(placement.description))?
              "Generic description" : placement.description;
          placement.targetingAdLocation = "All images on sports pages.";
          placement.targetingSiteName = "http://code.google.com";
          placement.isAdSenseTargetingEnabled = true;

          // Update the placement on the server.
          Placement[] placements = placementService.updatePlacements(new Placement[] {placement});

          // Display results.
          if (placements != null) {
            foreach (Placement updatedPlacement in placements) {
              Console.WriteLine("A placement with ID \"{0}\", name \"{1}\", and AdSense targeting" +
                  " enabled \"{2}\" was updated.", updatedPlacement.id, updatedPlacement.name,
                  updatedPlacement.isAdSenseTargetingEnabled);
            }
          } else {
            Console.WriteLine("No placements updated.");
          }
        }
      } catch (Exception e) {
        Console.WriteLine("Failed to update placements. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}
