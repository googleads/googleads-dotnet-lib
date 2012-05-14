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
using Google.Api.Ads.Dfp.Util.v201204;
using Google.Api.Ads.Dfp.v201204;

using System;

namespace Google.Api.Ads.Dfp.Examples.v201204 {
  /// <summary>
  /// This code example updates the descriptions of all active labels by
  /// updating its description up to the first 500. To determine which labels
  /// exist, run GetAllLabels.cs. This feature is only available to DFP premium
  /// solution networks.
  ///
  /// Tags: LabelService.getLabelsByStatement, LabelService.updateLabels
  /// </summary>
  class UpdateLabels : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example updates the descriptions of all active labels by updating " +
            "its description up to the first 500. To determine which labels exist, run " +
            "GetAllLabels.cs. This feature is only available to DFP premium solution networks.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new UpdateLabels();
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
          (LabelService) user.GetService(DfpService.v201204.LabelService);

      // Create a statement to only select labels that are competitive
      // exclusion.
      Statement filterStatement = new StatementBuilder("WHERE isActive = :isActive LIMIT 500").
          AddValue("isActive", true).ToStatement();

      try {
        // Get the labels by statement.
        LabelPage page = labelService.getLabelsByStatement(filterStatement);

        if (page.results != null) {
          Label[] labels = page.results;

          // Update each local label object by updating its description.
          foreach (Label label in labels) {
            label.description = "These labels are still competiting with each other.";
          }

          // Update the labels on the server.
          labels = labelService.updateLabels(labels);

          if (labels != null) {
            foreach (Label label in labels) {
              Console.WriteLine("A label with ID '{0}' and name '{1}' was updated.",
                  label.id, label.name);
            }
          } else {
            Console.WriteLine("No labels updated.");
          }
        } else {
          Console.WriteLine("No labels found to update.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to update labels. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
