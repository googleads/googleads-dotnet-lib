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
using Google.Api.Ads.Dfp.Util.v201411;
using Google.Api.Ads.Dfp.v201411;

using System;
using System.Text;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201411 {
  /// <summary>
  /// This code example gets all labels. To create labels, run CreateLabels.cs.
  /// This feature is only available to DFP premium solution networks.
  ///
  /// Tags: LabelService.getLabelsByStatement
  /// </summary>
  class GetAllLabels : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets all labels. To create labels, run CreateLabels.cs. This " +
            "feature is only available to DFP premium solution networks.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetAllLabels();
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
          (LabelService) user.GetService(DfpService.v201411.LabelService);

      // Create a statement to get all labels.
      StatementBuilder statementBuilder = new StatementBuilder()
          .OrderBy("id ASC")
          .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT);

      // Set default for page.
      LabelPage page = new LabelPage();

      try {
        do {
          // Get labels by statement.
          page = labelService.getLabelsByStatement(statementBuilder.ToStatement());

          if (page.results != null) {
            int i = page.startIndex;
            foreach (Label label in page.results) {
              StringBuilder builder = new StringBuilder();
              foreach (LabelType labelType in label.types) {
                builder.AppendFormat("{0} | ", labelType);
              }

              Console.WriteLine("{0}) Label with ID '{1}', name '{2}' and type '{3}' was found.",
                  i, label.id, label.name, builder.ToString().TrimEnd(' ', '|'));
              i++;
            }
          }
          statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
        } while (statementBuilder.GetOffset() < page.totalResultSetSize);
      } catch (Exception ex) {
        Console.WriteLine("Failed to get labels. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
