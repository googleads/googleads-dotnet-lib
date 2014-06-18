// Copyright 2013, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.Dfp.v201308;

using System;
using Google.Api.Ads.Dfp.Util.v201308;

namespace Google.Api.Ads.Dfp.Examples.v201308 {
  /// <summary>
  /// This code example gets all active creative wrappers. To create
  /// creative wrappers, run CreateCreativeWrappers.cs.
  ///
  /// Tags: CreativeWrapperService.getCreativeWrapperByStatement
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
          DfpService.v201308.CreativeWrapperService);

      // Set defaults for page and Statement.
      CreativeWrapperPage page = new CreativeWrapperPage();
      Statement statement = new StatementBuilder("")
          .AddValue("status", CreativeWrapperStatus.ACTIVE.ToString())
          .ToStatement();
      int offset = 0;

      try {
        do {
          // Create a Statement to get all active creative wrappers.
          statement.query = string.Format("WHERE status = :status LIMIT 500 OFFSET {0}", offset);

          // Get creative wrappers by Statement.
          page = creativeWrapperService.getCreativeWrappersByStatement(statement);

          if (page.results != null && page.results.Length > 0) {
            int i = page.startIndex;
            foreach (CreativeWrapper wrapper in page.results) {
              Console.WriteLine("Creative wrapper with ID \'{0}\' applying to label \'{1}\' with " +
                "status \'{2}\' was found.", wrapper.id, wrapper.labelId, wrapper.status);
              i++;
            }
          }

          offset += 500;
        } while (offset < page.totalResultSetSize);

        Console.WriteLine("Number of results found: {0}", page.totalResultSetSize);
      } catch (Exception ex) {
        Console.WriteLine("Failed to get active creative wrappers. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
