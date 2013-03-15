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
  /// This code example gets all activities. To create activities, run
  /// CreateActivities.cs.
  ///
  /// Tags: ActivityService.getActivitiesByStatement
  /// Tags: ActivityGroupService.getActivityGroupsByStatement
  /// </summary>
  class GetAllActivities : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets all activities. To create activities, run " +
            "CreateActivities.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetAllActivities();
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

      int totalResultsCounter = 0;

      try {
        long[] activityGroupIds = getAllActivityGroupIds(user);

        foreach (long activityGroupId in activityGroupIds) {
          Statement filterStatement = new StatementBuilder("").AddValue(
              "activityGroupId", activityGroupId).ToStatement();

          ActivityPage page = new ActivityPage();
          int offset = 0;

          do {
            filterStatement.query = "WHERE activityGroupId = :activityGroupId ORDER BY id " +
                "LIMIT 500 OFFSET " + offset;
            // Get activities by statement.
            page = activityService.getActivitiesByStatement(filterStatement);

            // Display results.
            if (page.results != null) {
              foreach (Activity activity in page.results) {
                Console.WriteLine("{0}) Activity with ID \"{1}\", name \"{2}\", type \"{3}\", " +
                    "and activity group ID \"{4}\" was found.\n", totalResultsCounter,
                    activity.id, activity.name, activity.type, activityGroupId);
                totalResultsCounter++;
              }
            }
            offset += 500;
          } while (offset < page.totalResultSetSize);
        }
        Console.WriteLine("Number of results found: {0}.", totalResultsCounter);
      } catch (Exception ex) {
        Console.WriteLine("Failed to get contacts. Exception says \"{0}\"", ex.Message);
      }
    }

    /// <summary>
    /// Get the list of all activity group ids.
    /// </summary>
    /// <param name="user">The DfpUser.</param>
    /// <returns>A list of ids for all the activity groups.</returns>
    private long[] getAllActivityGroupIds(DfpUser user) {
      List<long> activityGroupIds = new List<long>();

      // Get the ActivityGroupService.
      ActivityGroupService activityGroupService =
          (ActivityGroupService) user.GetService(DfpService.v201302.ActivityGroupService);

      ActivityGroupPage page;
      Statement filterStatement = new Statement();
      int offset = 0;

      do {
        // Create a statement to get all activity groups.
        filterStatement.query = "ORDER BY id LIMIT 500 OFFSET " + offset.ToString();

        // Get activity groups by statement.
        page = activityGroupService.getActivityGroupsByStatement(filterStatement);
        if (page.results != null) {
          foreach (ActivityGroup activityGroup in page.results) {
            activityGroupIds.Add(activityGroup.id);
          }
        }

        offset += 500;
      } while (offset < page.totalResultSetSize);

      return activityGroupIds.ToArray();
    }
  }
}
