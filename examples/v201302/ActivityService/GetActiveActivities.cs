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
using System.Collections.Generic;

namespace Google.Api.Ads.Dfp.Examples.v201302 {
  /// <summary>
  /// This code example gets all active activities. To create activities,
  /// run CreateActivities.cs.
  ///
  /// Tags: ActivityService.getActivitiesByStatement
  /// </summary>
  class GetActiveActivities : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets all active activities. To create activities, run " +
            "CreateActivities.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetActiveActivities();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the ActivityService.
      ActivityService activityService =
          (ActivityService) user.GetService(DfpService.v201302.ActivityService);

      // Set the ID of the activity group these activities are associated with.
      long activityGroupId = long.Parse(_T("INSERT_ACTIVITY_GROUP_ID_HERE"));

      // Statement parts to help build a statement that only selects active
      // activities.
      Statement filterStatement = new StatementBuilder("").
          AddValue("activityGroupId", activityGroupId).
          AddValue("status", ActivityStatus.ACTIVE.ToString()).
          ToStatement();

      int offset = 0;
      ActivityPage page;

      try {
        do {
          filterStatement.query = "WHERE activityGroupId = :activityGroupId and status = :status " +
              "ORDER BY id LIMIT 500 OFFSET " + offset;

          page = activityService.getActivitiesByStatement(filterStatement);

          // Display results.
          if (page.results != null) {
            int i = page.startIndex;
            foreach (Activity activity in page.results) {
              Console.WriteLine("{0}) Activity with ID \"{1}\", name \"{2}\", and type \"{3}\" " +
                  "was found.", i, activity.id, activity.name, activity.type);
              i++;
            }
          }

          offset += 500;
        } while (offset < page.totalResultSetSize);

        Console.WriteLine("Number of results found: {0}.", page.totalResultSetSize);
      } catch (Exception ex) {
        Console.WriteLine("Failed to get activities. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
