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
using Google.Api.Ads.Dfp.Util.v201611;
using Google.Api.Ads.Dfp.v201611;
using System;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201611 {
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
    public static void Main() {
      GetActiveActivityGroups codeExample = new GetActiveActivityGroups();
      Console.WriteLine(codeExample.Description);
      try {
        codeExample.Run(new DfpUser());
      } catch (Exception e) {
        Console.WriteLine("Failed to get activity groups. Exception says \"{0}\"",
            e.Message);
      }
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public void Run(DfpUser dfpUser) {
      ActivityGroupService activityGroupService =
          (ActivityGroupService) dfpUser.GetService(DfpService.v201611.ActivityGroupService);

      // Create a statement to select activity groups.
      int pageSize = StatementBuilder.SUGGESTED_PAGE_LIMIT;
      StatementBuilder statementBuilder = new StatementBuilder()
          .Where("status = :status")
          .OrderBy("id ASC")
          .Limit(pageSize)
          .AddValue("status", ActivityGroupStatus.ACTIVE.ToString());

      // Retrieve a small amount of activity groups at a time, paging through until all
      // activity groups have been retrieved.
      int totalResultSetSize = 0;
      do {
        ActivityGroupPage page = activityGroupService.getActivityGroupsByStatement(
            statementBuilder.ToStatement());

        // Print out some information for each activity group.
        if (page.results != null) {
          totalResultSetSize = page.totalResultSetSize;
          int i = page.startIndex;
          foreach (ActivityGroup activityGroup in page.results) {
            Console.WriteLine(
                "{0}) Activity group with ID {1} and name \"{2}\" was found.",
                i++,
                activityGroup.id,
                activityGroup.name
            );
          }
        }

        statementBuilder.IncreaseOffsetBy(pageSize);
      } while (statementBuilder.GetOffset() < totalResultSetSize);

      Console.WriteLine("Number of results found: {0}", totalResultSetSize);
    }
  }
}
