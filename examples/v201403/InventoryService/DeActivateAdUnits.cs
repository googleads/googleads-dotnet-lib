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
using Google.Api.Ads.Dfp.Util.v201403;
using Google.Api.Ads.Dfp.v201403;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.Dfp.Examples.v201403 {
  /// <summary>
  /// This code example deactivates all active ad units. To determine which ad
  /// units exist, run GetAllAdUnits.cs or GetInventoryTree.cs.
  ///
  /// Tags: InventoryService.getAdUnitsByStatement
  /// Tags: InventoryService.performAdUnitAction
  /// </summary>
  class DeActivateAdUnits : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example deactivates all active ad units. To determine which ad units " +
            "exist, run GetAllAdUnits.cs or GetInventoryTree.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new DeActivateAdUnits();
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
          (InventoryService) user.GetService(DfpService.v201403.InventoryService);

      // Create Statement text to select active ad units.
      string statementText = "WHERE status = :status LIMIT 500";

      Statement statement = new StatementBuilder("").AddValue("status",
          InventoryStatus.ACTIVE.ToString()).ToStatement();

      // Sets defaults for page and offset.
      AdUnitPage page = new AdUnitPage();
      int offset = 0;
      List<string> adUnitIds = new List<string>();

      try {
        do {
          // Create a Statement to page through active ad units.
          statement.query = string.Format("{0} OFFSET {1}", statementText, offset);

          // Get ad units by Statement.
          page = inventoryService.getAdUnitsByStatement(statement);

          if (page.results != null && page.results.Length > 0) {
            int i = page.startIndex;
            foreach (AdUnit adUnit in page.results) {
              Console.WriteLine("{0}) Ad unit with ID ='{1}', name = {2} and status = {3} will" +
                  " be deactivated.", i, adUnit.id, adUnit.name, adUnit.status);
              adUnitIds.Add(adUnit.id);
              i++;
            }
          }

          offset += 500;
        } while (offset < page.totalResultSetSize);

        Console.WriteLine("Number of ad units to be deactivated: {0}", adUnitIds.Count);

        if (adUnitIds.Count > 0) {
          // Create action Statement.
          statement = new StatementBuilder(
              string.Format("WHERE id IN ({0})", string.Join(",", adUnitIds.ToArray()))).
              ToStatement();

          // Create action.
          DeactivateAdUnits action = new DeactivateAdUnits();

          // Perform action.
          UpdateResult result = inventoryService.performAdUnitAction(action, statement);

          // Display results.
          if (result != null && result.numChanges > 0) {
            Console.WriteLine("Number of ad units deactivated: {0}", result.numChanges);
          } else {
            Console.WriteLine("No ad units were deactivated.");
          }
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to deactivate ad units. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
