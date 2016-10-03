// Copyright 2016, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.Dfp.Util.v201605;
using Google.Api.Ads.Dfp.v201605;

using System;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201605 {
  /// <summary>
  /// This example gets all active labels.
  /// </summary>
  public class GetActiveLabels : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This example gets all active labels.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    public static void Main() {
      GetActiveLabels codeExample = new GetActiveLabels();
      Console.WriteLine(codeExample.Description);

      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    public void Run(DfpUser user) {
      LabelService labelService =
          (LabelService) user.GetService(DfpService.v201605.LabelService);

      // Create a statement to select labels.
      StatementBuilder statementBuilder = new StatementBuilder()
          .Where("isActive = :isActive")
          .OrderBy("id ASC")
          .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT)
          .AddValue("isActive", true);

      // Retrieve a small amount of labels at a time, paging through
      // until all labels have been retrieved.
      LabelPage page = new LabelPage();
      try {
        do {
          page = labelService.getLabelsByStatement(statementBuilder.ToStatement());

          if (page.results != null) {
            // Print out some information for each label.
            int i = page.startIndex;
            foreach (Label label in page.results) {
              Console.WriteLine("{0}) Label with ID \"{1}\" and name \"{2}\" was found.",
                  i++,
                  label.id,
                  label.name);
            }
          }

          statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
        } while (statementBuilder.GetOffset() < page.totalResultSetSize);

        Console.WriteLine("Number of results found: {0}", page.totalResultSetSize);
      } catch (Exception e) {
        Console.WriteLine("Failed to get labels. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}
