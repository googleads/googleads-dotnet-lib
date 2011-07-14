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

using Google.Api.Ads.Dfa.Lib;
using Google.Api.Ads.Dfa.v1_14;

using System;
using System.Collections.Generic;
using System.Text;
using Google.Api.Ads.Common.Util;

namespace Google.Api.Ads.Dfa.Examples.CSharp.v1_14 {
  /// <summary>
  /// This code example assigns creatives to placements and creates a unique ad
  /// for each assignment. To get creatives, run GetCreatives.cs. To get
  /// placements, run GetPlacement.cs.
  ///
  /// Tags: creative.assignCreativesToPlacements
  /// </summary>
  class AssignCreativesToPlacements : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example assigns creatives to placements and creates a unique ad for " +
            "each assignment. To get creatives, run GetCreatives.cs. To get placements, run " +
            "GetPlacement.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new AssignCreativesToPlacements();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfaUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The Dfa user object running the code example.
    /// </param>
    public override void Run(DfaUser user) {
      // Create CreativeRemoteService instance.
      CreativeRemoteService service = (CreativeRemoteService) user.GetService(
          DfaService.v1_14.CreativeRemoteService);

      long creativeId = long.Parse(_T("INSERT_CREATIVE_ID"));
      long placementId = long.Parse(_T("INSERT_PLACEMENT_ID"));

      // Create creative placement assignment structure.
      CreativePlacementAssignment creativePlacementAssignment =
          new CreativePlacementAssignment();
      creativePlacementAssignment.creativeId = creativeId;
      creativePlacementAssignment.placementId = placementId;
      creativePlacementAssignment.placementIds = new long[] {placementId};

      try {
        // Assign creatives to placements.
        CreativePlacementAssignmentResult[] results =
            service.assignCreativesToPlacements(
                new CreativePlacementAssignment[] {creativePlacementAssignment});
        // Display new ads that resulted from the assignment.
        foreach (CreativePlacementAssignmentResult result in results) {
          Console.WriteLine("Ad with name \"{0}\" and id \"{1}\" was created.", result.adName,
              result.adId);
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to create ad. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
