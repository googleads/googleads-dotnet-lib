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
  /// This code example deactivates all creative wrappers belonging to a label.
  /// </summary>
  class DeActivateCreativeWrappers : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example deactivates all creative wrappers belonging to a label.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new DeActivateCreativeWrappers();
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

      long labelId = long.Parse(_T("INSERT_CREATIVE_WRAPPER_LABEL_ID_HERE"));

      try {
        // Create a query to select the active creative wrapper for the given
        // label.
        StatementBuilder statementBuilder = new StatementBuilder()
            .Where ("labelId = :labelId AND status = :status")
            .OrderBy("id ASC")
            .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT)
            .AddValue("status", CreativeWrapperStatus.ACTIVE.ToString())
            .AddValue("labelId", labelId);

        // Set default for page.
        CreativeWrapperPage page = new CreativeWrapperPage();

        do {
          page =
              creativeWrapperService.getCreativeWrappersByStatement(statementBuilder.ToStatement());
          CreativeWrapper[] creativeWrappers = page.results;
          if (creativeWrappers != null) {
            foreach (CreativeWrapper wrapper in creativeWrappers) {
              Console.WriteLine("Creative wrapper with ID \'{0}\' applying to label \'{1}\' with " +
                  "status \'{2}\' will be deactivated.", wrapper.id, wrapper.labelId,
                   wrapper.status);
            }
          }
          statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
        } while (statementBuilder.GetOffset() < page.totalResultSetSize);

        Console.WriteLine("Number of creative wrappers to be deactivated: {0}",
            page.totalResultSetSize);

        // Modify statement for action.
        statementBuilder.RemoveLimitAndOffset();

        // Perform action.
        CreativeWrapperAction action = new DeactivateCreativeWrappers();
        UpdateResult result = creativeWrapperService.performCreativeWrapperAction(action,
            statementBuilder.ToStatement());

        // Display results.
        if (result.numChanges > 0) {
          Console.WriteLine("Number of creative wrappers deactivated: {0}", result.numChanges);
        } else {
          Console.WriteLine("No creative wrappers were deactivated.");
        }

      } catch (Exception e) {
        Console.WriteLine("Failed to create creative wrappers. Exception says \"{0}\"", e.Message);
      }
    }
  }
}
