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
using Google.Api.Ads.Dfp.v201208;

using System;
using System.Text;

namespace Google.Api.Ads.Dfp.Examples.v201208 {
  /// <summary>
  /// This code example gets a label by its ID. To determine which labels exist,
  /// run GetAllLabels.cs. This feature is only available to DFP premium
  /// solution networks.
  ///
  /// Tags: LabelService.getLabel
  /// </summary>
  class GetLabel : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets a label by its ID. To determine which labels exist, " +
            "run GetAllLabels.cs. This feature is only available to DFP premium solution networks.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetLabel();
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
          (LabelService) user.GetService(DfpService.v201208.LabelService);

      // Set the ID of the label to get.
      long labelId = long.Parse(_T("INSERT_LABEL_ID_HERE"));

      try {
        // Get the label.
        Label label = labelService.getLabel(labelId);

        if (label != null) {
          StringBuilder builder = new StringBuilder();
          foreach (LabelType labelType in label.types) {
            builder.AppendFormat("{0} | ", labelType);
          }
          Console.WriteLine("Label with ID '{0}, name '{1}' and type '{2}' was found.",
              label.id, label.name, builder.ToString().TrimEnd(' ', '|'));
        } else {
          Console.WriteLine("No label found for this ID.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to get label. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
