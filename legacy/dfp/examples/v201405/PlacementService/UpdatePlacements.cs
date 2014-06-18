// Copyright 2014, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.Dfp.v201405;

using System;

namespace Google.Api.Ads.Dfp.Examples.v201405 {
  /// <summary>
  /// This code example updates all placements to allow for AdSense targeting
  /// up to the first 500. To determine which placements exist, run
  /// GetAllPlacements.cs.
  ///
  /// Tags: PlacementService.getPlacementsByStatement
  /// Tags: PlacementService.updatePlacements
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
          (PlacementService) user.GetService(DfpService.v201405.PlacementService);

      // Create a statement to select first 500 placements.
      Statement filterStatement = new Statement();
      filterStatement.query = "LIMIT 500";

      try {
        // Get placements by statement.
        PlacementPage page = placementService.getPlacementsByStatement(filterStatement);

        if (page.results != null) {
          Placement[] placements = page.results;

          // Update each local placement object by enabling AdSense targeting.
          foreach (Placement placement in placements) {
            placement.targetingDescription = (string.IsNullOrEmpty(placement.description))?
                "Generic description" : placement.description;
            placement.targetingAdLocation = "All images on sports pages.";
            placement.targetingSiteName = "http://code.google.com";
            placement.isAdSenseTargetingEnabled = true;
          }

          // Update the placements on the server.
          placements = placementService.updatePlacements(placements);

          // Display results.
          if (placements != null) {
            foreach (Placement placement in placements) {
              Console.WriteLine("A placement with ID \"{0}\", name \"{1}\", and AdSense targeting" +
                  " enabled \"{2}\" was updated.", placement.id, placement.name,
                  placement.isAdSenseTargetingEnabled);
            }
          } else {
            Console.WriteLine("No placements updated.");
          }
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to update placements. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
