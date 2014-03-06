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
using Google.Api.Ads.Dfp.v201302;
using Google.Api.Ads.Dfp.Util.v201302;

using System;

namespace Google.Api.Ads.Dfp.Examples.v201302 {
  /// <summary>
  /// This code example gets all active activity groups. To create activity
  /// groups, run CreateActivityGroups.cs.
  ///
  /// Tags: ContactService.getContactsByStatement
  /// </summary>
  class GetActiveActivityGroups : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets all active activity groups. To create activity groups, " +
            "run CreateActivityGroups.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetActiveActivityGroups();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the ActivityGroupService.
      ActivityGroupService activityGroupService =
          (ActivityGroupService) user.GetService(DfpService.v201302.ActivityGroupService);

      Statement filterStatement = new StatementBuilder("").AddValue(
          "status", ActivityGroupStatus.ACTIVE.ToString()).ToStatement();

      // Set defaults for page and filterStatement.
      ActivityGroupPage page;
      int offset = 0;

      try {
        do {
          // Create a statement to get all content.
          filterStatement.query = "WHERE status = :status ORDER BY id LIMIT 500 OFFSET " +
              offset.ToString();


          // Get contacts by statement.
          page = activityGroupService.getActivityGroupsByStatement(filterStatement);

          if (page.results != null) {
            int i = page.startIndex;
            foreach (ActivityGroup activityGroup in page.results) {
              Console.WriteLine("{0}) Activity group with ID \"{1}\" and name \"{2}\" was found.",
                  i, activityGroup.id, activityGroup.name);
              i++;
            }
          }
          offset += 500;
        } while (offset < page.totalResultSetSize);

        Console.WriteLine("Number of results found: " + page.totalResultSetSize);
      } catch (Exception ex) {
        Console.WriteLine("Failed to get activity groups. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
