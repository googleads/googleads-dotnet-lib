// Copyright 2012, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.Dfp.v201204;

using System;

namespace Google.Api.Ads.Dfp.Examples.v201204 {
  /// <summary>
  /// This code example gets all ad units. To create ad units, run
  /// CreateAdUnits.cs.
  ///
  /// Tags: InventoryService.getAdUnitsByStatement
  /// </summary>
  class GetAllAdUnits : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets all ad units. To create ad units, run CreateAdUnits.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetAllAdUnits();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the InventoryService.
      InventoryService inventoryService =
          (InventoryService) user.GetService(DfpService.v201204.InventoryService);

      // Sets defaults for page and statement.
      AdUnitPage page = new AdUnitPage();
      Statement statement = new Statement();
      int offset = 0;

      try {
        do {
          // Create a Statement to get all ad units.
          statement.query = string.Format("LIMIT 500 OFFSET {0}", offset);

          // Get ad units by Statement.
          page = inventoryService.getAdUnitsByStatement(statement);

          if (page.results != null && page.results.Length > 0) {
            int i = page.startIndex;
            foreach (AdUnit adUnit in page.results) {
              Console.WriteLine("{0}) Ad unit with ID = '{1}', name = '{2}' and status = '{3}' " +
                  "was found.", i, adUnit.id, adUnit.name, adUnit.status);
              i++;
            }
          }
          offset += 500;
        } while (offset < page.totalResultSetSize);

        Console.WriteLine("Number of results found: {0}", page.totalResultSetSize);
      } catch (Exception ex) {
        Console.WriteLine("Failed to get ad unit. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
