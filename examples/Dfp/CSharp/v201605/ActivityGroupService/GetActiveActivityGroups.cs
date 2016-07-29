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
  /// This example gets all active activity groups.
  /// </summary>
  public class GetActiveActivityGroups : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This example gets all active activity groups.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main() {
      GetActiveActivityGroups codeExample = new GetActiveActivityGroups();
      Console.WriteLine(codeExample.Description);

      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public void Run(DfpUser user) {
      ActivityGroupService activityGroupService =
          (ActivityGroupService) user.GetService(DfpService.v201605.ActivityGroupService);

      // Create a statement to select activity groups.
      StatementBuilder statementBuilder = new StatementBuilder()
          .Where("status = :status")
          .OrderBy("id ASC")
          .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT)
          .AddValue("status", ActivityGroupStatus.ACTIVE.ToString());

      // Retrieve a small amount of activity groups at a time, paging through
      // until all activity groups have been retrieved.
      ActivityGroupPage page = new ActivityGroupPage();
      try {
        do {
          page = activityGroupService.getActivityGroupsByStatement(statementBuilder.ToStatement());

          if (page.results != null) {
            // Print out some information for each activity group.
            int i = page.startIndex;
            foreach (ActivityGroup activityGroup in page.results) {
              Console.WriteLine("{0}) Activity group with ID \"{1}\" and name \"{2}\" was found.",
                  i++,
                  activityGroup.id,
                  activityGroup.name);
            }
          }

          statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
        } while (statementBuilder.GetOffset() < page.totalResultSetSize);

        Console.WriteLine("Number of results found: {0}", page.totalResultSetSize);
      } catch (Exception e) {
        Console.WriteLine("Failed to get activity groups. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}
