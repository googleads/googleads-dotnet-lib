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
using Google.Api.Ads.Dfp.Util.v201602;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201602 {
  /// <summary>
  /// This code example gets all activities. To create activities, run
  /// CreateActivities.cs.
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
          (ActivityService) user.GetService(DfpService.v201602.ActivityService);

      int totalResultsCounter = 0;

      try {
        StatementBuilder statementBuilder = new StatementBuilder()
            .OrderBy("id ASC")
            .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT);

        ActivityPage page = new ActivityPage();

        do {
          // Get activities by statement.
          page = activityService.getActivitiesByStatement(statementBuilder.ToStatement());

          // Display results.
          if (page.results != null) {
            foreach (Activity activity in page.results) {
              Console.WriteLine("{0}) Activity with ID \"{1}\", name \"{2}\" and type \"{3}\" " +
                  "was found.\n", totalResultsCounter, activity.id, activity.name,
                  activity.type);
              totalResultsCounter++;
            }
          }
          statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
        } while (statementBuilder.GetOffset() < page.totalResultSetSize);
        Console.WriteLine("Number of results found: {0}.", totalResultsCounter);
      } catch (Exception e) {
        Console.WriteLine("Failed to get activities. Exception says \"{0}\"", e.Message);
      }
    }
  }
}
