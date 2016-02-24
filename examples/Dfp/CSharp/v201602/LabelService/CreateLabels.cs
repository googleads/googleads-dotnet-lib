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
using Google.Api.Ads.Dfp.v201602;

using System;
using System.Text;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201602 {
  /// <summary>
  /// This code example creates new labels. To determine which labels exist, run
  /// GetAllLabels.cs. This feature is only available to DFP premium solution
  /// networks.
  /// </summary>
  class CreateLabels : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example creates new labels. To determine which labels exist, run " +
            "GetAllLabels.cs. This feature is only available to DFP premium solution networks.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new CreateLabels();
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

      try {
        // Create an array to store local label objects.
        Label[] labels = new Label[5];

        for (int i = 0; i < 5; i++) {
          Label label = new Label();
          label.name = "Label #" + GetTimeStamp();
          label.types = new LabelType[] {LabelType.COMPETITIVE_EXCLUSION};
          labels[i] = label;
        }

        // Create the labels on the server.
        labels = labelService.createLabels(labels);

        if (labels != null) {
          foreach (Label label in labels) {
            StringBuilder builder = new StringBuilder();
            foreach (LabelType labelType in label.types) {
              builder.AppendFormat("{0} | ", labelType);
            }

            Console.WriteLine("A label with ID '{0}', name '{1}', and type '{2}' was created.",
                label.id, label.name, builder.ToString().TrimEnd(' ', '|'));
          }
        } else {
          Console.WriteLine("No labels created.");
        }
      } catch (Exception e) {
        Console.WriteLine("Failed to create labels. Exception says \"{0}\"", e.Message);
      }
    }
  }
}
