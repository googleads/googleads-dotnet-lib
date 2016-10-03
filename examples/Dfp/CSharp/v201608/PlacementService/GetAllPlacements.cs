// Copyright 2016, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.Dfp.Util.v201608;
using Google.Api.Ads.Dfp.v201608;

using System;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201608 {
  /// <summary>
  /// This example gets all placements.
  /// </summary>
  public class GetAllPlacements : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This example gets all placements.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    public static void Main() {
      GetAllPlacements codeExample = new GetAllPlacements();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    public void Run(DfpUser user) {
      PlacementService placementService =
          (PlacementService) user.GetService(DfpService.v201608.PlacementService);

      // Create a statement to select placements.
      StatementBuilder statementBuilder = new StatementBuilder()
          .OrderBy("id ASC")
          .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT);

      // Retrieve a small amount of placements at a time, paging through
      // until all placements have been retrieved.
      PlacementPage page = new PlacementPage();
      try {
        do {
          page = placementService.getPlacementsByStatement(statementBuilder.ToStatement());

          if (page.results != null) {
            // Print out some information for each placement.
            int i = page.startIndex;
            foreach (Placement placement in page.results) {
              Console.WriteLine("{0}) Placement with ID \"{1}\" and name \"{2}\" was found.",
                  i++,
                  placement.id,
                  placement.name);
            }
          }

          statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
        } while (statementBuilder.GetOffset() < page.totalResultSetSize);

        Console.WriteLine("Number of results found: {0}", page.totalResultSetSize);
      } catch (Exception e) {
        Console.WriteLine("Failed to get placements. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}
