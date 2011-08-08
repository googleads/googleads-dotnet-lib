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
using Google.Api.Ads.Dfp.v201101;

using System;

namespace Google.Api.Ads.Dfp.Examples.v201101 {
  /// <summary>
  /// This code example gets a placement by its ID. To determine which
  /// placements exist, run GetAllPlacements.cs.
  ///
  /// Tags: PlacementService.getPlacement
  /// </summary>
  class GetPlacement : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets a placement by its ID. To determine which placements " +
            "exist, run GetAllPlacements.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetPlacement();
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
          (PlacementService) user.GetService(DfpService.v201101.PlacementService);

      // Set the ID of the placement to get.
      long placementId = long.Parse(_T("INSERT_PLACEMENT_ID_HERE"));

      try {
        // Get the placement.
        Placement placement = placementService.getPlacement(placementId);

        if (placement != null) {
          Console.WriteLine("Placement with ID = '{0}', name = '{1}', and status = '{2}' " +
              "was found.", placement.id, placement.name, placement.status);
        } else {
          Console.WriteLine("No placement found for this ID.");
        }

      } catch (Exception ex) {
        Console.WriteLine("Failed to get placement by ID. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
