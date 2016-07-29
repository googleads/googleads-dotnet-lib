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
  /// This example gets all active creative wrappers.
  /// </summary>
  public class GetActiveCreativeWrappers : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This example gets all active creative wrappers.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main() {
      GetActiveCreativeWrappers codeExample = new GetActiveCreativeWrappers();
      Console.WriteLine(codeExample.Description);

      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public void Run(DfpUser user) {
      CreativeWrapperService creativeWrapperService =
          (CreativeWrapperService) user.GetService(DfpService.v201605.CreativeWrapperService);

      // Create a statement to select creative wrappers.
      StatementBuilder statementBuilder = new StatementBuilder()
          .Where("status = :status")
          .OrderBy("id ASC")
          .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT)
          .AddValue("status", CreativeWrapperStatus.ACTIVE.ToString());

      // Retrieve a small amount of creative wrappers at a time, paging through
      // until all creative wrappers have been retrieved.
      CreativeWrapperPage page = new CreativeWrapperPage();
      try {
        do {
          page = creativeWrapperService.getCreativeWrappersByStatement(
              statementBuilder.ToStatement());

          if (page.results != null) {
            // Print out some information for each creative wrapper.
            int i = page.startIndex;
            foreach (CreativeWrapper creativeWrapper in page.results) {
              Console.WriteLine("{0}) Creative wrapper with ID \"{1}\" "
                  + "and label ID \"{2}\" was found.",
                  i++,
                  creativeWrapper.id,
                  creativeWrapper.labelId);
            }
          }

          statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
        } while (statementBuilder.GetOffset() < page.totalResultSetSize);

        Console.WriteLine("Number of results found: {0}", page.totalResultSetSize);
      } catch (Exception e) {
        Console.WriteLine("Failed to get creative wrappers. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}
