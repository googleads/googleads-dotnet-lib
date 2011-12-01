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
using Google.Api.Ads.Dfp.v201111;

using System;
using System.Collections.Generic;
using Google.Api.Ads.Dfp.Util.v201111;

namespace Google.Api.Ads.Dfp.Examples.v201111 {
  /// <summary>
  /// This code example approves all suggested ad units with 50 or more
  /// requests. This feature is only available to DFP premium solution networks.
  ///
  /// Tags: SuggestedAdUnitService.getSuggestedAdUnitsByStatement
  /// Tags: SuggestedAdUnitService.performSuggestedAdUnitAction
  /// </summary>
  class ApproveSuggestedAdUnits : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example approves all suggested ad units with 50 or more requests. " +
            "This feature is only available to DFP premium solution networks.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new ApproveSuggestedAdUnits();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the SuggestedAdUnitService.
      SuggestedAdUnitService suggestedAdUnitService = (SuggestedAdUnitService) user.GetService(
          DfpService.v201111.SuggestedAdUnitService);

      // Set the number of requests to 50 or more.
      long NUMBER_OF_REQUESTS = 50L;

      // Create statement text to select all suggested ad units.
      string statementText = "LIMIT 500";
      Statement filterStatement = new StatementBuilder(statementText).ToStatement();

      // Set defaults for page and offset.
      SuggestedAdUnitPage page = new SuggestedAdUnitPage();
      int offset = 0;
      List<string> suggestedAdUnitIds = new List<string>();

      try {
        do {
          // Create a statement to page through suggested ad units.
          filterStatement.query = statementText + " OFFSET " + offset.ToString();

          // Get suggested ad units by statement.
          page = suggestedAdUnitService.getSuggestedAdUnitsByStatement(filterStatement);

          if (page.results != null) {
            int i = page.startIndex;

            foreach (SuggestedAdUnit suggestedAdUnit in page.results) {
              if (suggestedAdUnit.numRequests >= NUMBER_OF_REQUESTS) {
                Console.WriteLine("{0}) Suggested ad unit with ID \"{1}\", and \"{2}\" will be " +
                    "approved.", i, suggestedAdUnit.id, suggestedAdUnit.numRequests);
                suggestedAdUnitIds.Add(suggestedAdUnit.id);
              }
              i++;
            }
          }

          offset += 500;
        } while (offset < page.totalResultSetSize);

        Console.WriteLine("Number of suggested ad units to be approved: " +
            suggestedAdUnitIds.Count);

        if (suggestedAdUnitIds.Count > 0) {
          // Modify statement for action.
          filterStatement.query = "WHERE id IN (" + String.Join(",", suggestedAdUnitIds.ToArray()) +
              ")";

          // Create action.
          ApproveSuggestedAdUnit action = new ApproveSuggestedAdUnit();

          // Perform action.
          UpdateResult result = suggestedAdUnitService.performSuggestedAdUnitAction(
              action, filterStatement);

          // Display results.
          if (result != null && result.numChanges > 0) {
            Console.WriteLine("Number of suggested ad units approved: " + result.numChanges);
          } else {
            Console.WriteLine("No suggested ad units were approved.");
          }
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to approve suggested ad units. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
