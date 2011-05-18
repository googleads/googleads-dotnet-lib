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
using System.Collections.Generic;

namespace Google.Api.Ads.Dfp.Examples.v201103 {
  /// <summary>
  /// This code example activates all deactivated LICAs for the line item. To
  /// determine which LICAs exist, run GetAllLicas.cs.
  /// </summary>
  class ActivateLicas : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example activates all deactivated LICAs for the line item. To " +
            "determine which LICAs exist, run GetAllLicas.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new ActivateLicas();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the LineItemCreativeAssociationService.
      LineItemCreativeAssociationService licaService = (LineItemCreativeAssociationService)
          user.GetService(DfpService.v201103.LineItemCreativeAssociationService);

      // Set the line item to get LICAs by.
      long lineItemId = long.Parse(_T("INSERT_LINE_ITEM_ID_HERE"));

      String statementText = "WHERE lineItemId = :lineItemId and status = :status LIMIT 500";
      Statement statement = new StatementBuilder("").AddValue("lineItemId", lineItemId).
          AddValue("status", LineItemCreativeAssociationStatus.INACTIVE.ToString()).ToStatement();

      // Sets defaults for page and offset.
      LineItemCreativeAssociationPage page = new LineItemCreativeAssociationPage();
      int offset = 0;
      List<string> creativeIds = new List<string>();

      try {
        do {
          // Create a Statement to page through active LICAs.
          statement.query = string.Format("{0} OFFSET {1}", statementText, offset);

          // Get LICAs by Statement.
          page = licaService.getLineItemCreativeAssociationsByStatement(statement);

          if (page.results != null && page.results.Length > 0) {
            int i = page.startIndex;
            foreach (LineItemCreativeAssociation lica in page.results) {
              Console.WriteLine("{0}) LICA with line item ID = '{1}', creative ID ='{2}' and " +
                  "status ='{3}' will be activated.", i, lica.lineItemId, lica.creativeId,
                  lica.status);
              i++;
              creativeIds.Add(lica.creativeId.ToString());
            }
          }

          offset += 500;
        } while (offset < page.totalResultSetSize);

        Console.WriteLine("Number of LICAs to be activated: {0}", creativeIds.Count);

        if (creativeIds.Count > 0) {
          // Create action Statement.
          statement = new StatementBuilder(
              string.Format("WHERE lineItemId = :lineItemId and creativeId IN ({0})",
                  string.Join(",", creativeIds.ToArray()))).
              AddValue("lineItemId", lineItemId).ToStatement();

          // Create action.
          ActivateLineItemCreativeAssociations action =
              new ActivateLineItemCreativeAssociations();

          // Perform action.
          UpdateResult result =
              licaService.performLineItemCreativeAssociationAction(action, statement);

          // Display results.
          if (result != null && result.numChanges > 0) {
            Console.WriteLine("Number of LICAs activated: {0}", result.numChanges);
          } else {
            Console.WriteLine("No LICAs were activated.");
          }
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to activate LICAs. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
