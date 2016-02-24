// Copyright 2015, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.Dfp.Util.v201602;
using Google.Api.Ads.Dfp.v201602;

using System;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201602 {
  /// <summary>
  /// This code example updates a label's description. To determine which labels
  /// exist, run GetAllLabels.cs. This feature is only available to DFP premium
  /// solution networks.
  /// </summary>
  class UpdateLabels : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example updates a label's description. To determine which labels " +
            "exist, run GetAllLabels.cs. This feature is only available to DFP premium solution " +
            "networks.";
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
          (LabelService) user.GetService(DfpService.v201602.LabelService);

      // Set the ID of the label to deactivate.
      int labelId = int.Parse(_T("INSERT_LABEL_ID_HERE"));

      // Create a statement to select the label.
      StatementBuilder statementBuilder = new StatementBuilder()
          .Where("id = :id")
          .OrderBy("id ASC")
          .Limit(1)
          .AddValue("id", labelId);

      try {
        // Get the labels by statement.
        LabelPage page = labelService.getLabelsByStatement(statementBuilder.ToStatement());

        Label label = page.results[0];

        // Update the label description.
        label.description = "New label description.";

        // Update the label on the server.
        Label[] labels = labelService.updateLabels(new Label[] {label});

        foreach (Label updatedLabel in labels) {
          Console.WriteLine("A label with ID '{0}' and name '{1}' was updated.",
              updatedLabel.id, updatedLabel.name);
        }

      } catch (Exception e) {
        Console.WriteLine("Failed to update labels. Exception says \"{0}\"", e.Message);
      }
    }
  }
}
