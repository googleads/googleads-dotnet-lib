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
using Google.Api.Ads.Dfp.Util.v201111;
using Google.Api.Ads.Dfp.v201111;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.Dfp.Examples.v201111 {
  /// <summary>
  /// This code example deactivates all active labels. To determine which labels
  /// exist, run GetAllLabels.cs. This feature is only available to DFP premium
  /// solution networks.
  ///
  /// Tags: LabelService.getLabelsByStatement, LabelService.performLabelAction
  /// </summary>
  class DeactivateActiveLabels : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example deactivates all active labels. To determine which labels exist," +
            " run GetAllLabels.cs. This feature is only available to DFP premium solution " +
            "networks.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new DeactivateActiveLabels();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the LabelService.
      LabelService labelService =
          (LabelService) user.GetService(DfpService.v201111.LabelService);

      // Create statement text to select active labels.
      String statementText = "WHERE isActive = :isActive LIMIT 500";
      Statement filterStatement = new StatementBuilder("").AddValue("isActive", true).ToStatement();

      // Set defaults for page and offset.
      LabelPage page = new LabelPage();
      int offset = 0;
      List<string> labelIds = new List<string>();

      try {
        do {
          // Create a statement to page through active labels.
          filterStatement.query = statementText + " OFFSET " + offset;

          // Get labels by statement.
          page = labelService.getLabelsByStatement(filterStatement);

          if (page.results != null) {
            int i = page.startIndex;
            foreach (Label label in page.results) {
              Console.WriteLine("{0}) Label with ID '{1}', name '{2}' will be deactivated.",
                  i, label.id, label.name);
              labelIds.Add(label.id.ToString());
              i++;
            }
          }
          offset += 500;
        } while (offset < page.totalResultSetSize);

        Console.WriteLine("Number of labels to be deactivated: " + labelIds.Count);

        if (labelIds.Count > 0) {
          // Modify statement for action.
          filterStatement.query = "WHERE id IN (" + string.Join(", ", labelIds.ToArray()) + ")";

          // Create action.
          DeactivateLabels action = new DeactivateLabels();

          // Perform action.
          UpdateResult result = labelService.performLabelAction(action, filterStatement);

          // Display results.
          if (result != null && result.numChanges > 0) {
            Console.WriteLine("Number of labels deactivated: " + result.numChanges);
          } else {
            Console.WriteLine("No labels were deactivated.");
          }
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to deactivate labels. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
