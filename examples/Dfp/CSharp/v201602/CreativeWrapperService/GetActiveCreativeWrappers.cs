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
using Google.Api.Ads.Dfp.Util.v201602;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201602 {
  /// <summary>
  /// This code example gets all active creative wrappers. To create
  /// creative wrappers, run CreateCreativeWrappers.cs.
  /// </summary>
  class GetActiveCreativeWrappers : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets all active creative wrappers. To create creative " +
            "wrappers, run CreateCreativeWrappers.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetActiveCreativeWrappers();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Create the CreativeWrapperService.
      CreativeWrapperService creativeWrapperService = (CreativeWrapperService) user.GetService(
          DfpService.v201602.CreativeWrapperService);

     // Create a statement to get all active creative wrappers.
     StatementBuilder statementBuilder = new StatementBuilder()
          .Where("status = :status")
          .OrderBy("id ASC")
          .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT)
          .AddValue("status", CreativeWrapperStatus.ACTIVE.ToString());

       // Set default for page.
      CreativeWrapperPage page = new CreativeWrapperPage();

      try {
        do {
          // Get creative wrappers by statement.
          page = creativeWrapperService.getCreativeWrappersByStatement(
              statementBuilder.ToStatement());

          if (page.results != null && page.results.Length > 0) {
            int i = page.startIndex;
            foreach (CreativeWrapper wrapper in page.results) {
              Console.WriteLine("Creative wrapper with ID \'{0}\' applying to label \'{1}\' with " +
                "status \'{2}\' was found.", wrapper.id, wrapper.labelId, wrapper.status);
              i++;
            }
          }

          statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
        } while (statementBuilder.GetOffset() < page.totalResultSetSize);

        Console.WriteLine("Number of results found: {0}", page.totalResultSetSize);
      } catch (Exception e) {
        Console.WriteLine("Failed to get active creative wrappers. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}
