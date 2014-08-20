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
using Google.Api.Ads.Dfp.v201311;
using Google.Api.Ads.Dfp.Util.v201311;

using System;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201311 {
  /// <summary>
  /// This code example updates activity expected URLs. To determine which
  /// activities exist, run GetAllActivities.cs.
  ///
  /// Tags: ActivityService.getActivity
  /// Tags: ActivityService.updateActivities
  /// </summary>
  class UpdateActivities : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example updates activity expected URLs. To determine which activities " +
            "exist, run GetAllActivities.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new UpdateActivities();
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
          (ActivityService) user.GetService(DfpService.v201311.ActivityService);

      // Set the ID of the activity to update.
      int activityId = int.Parse(_T("INSERT_ACTIVITY_ID_HERE"));

      try {
        // Get the activity.
        Activity activity = activityService.getActivity(activityId);

        // Update the expected URL.
        activity.expectedURL = "https://www.google.com";

        // Update the activity on the server.
        Activity[] activities = activityService.updateActivities(new Activity[] {activity});

        // Display results.
        if (activities != null) {
          foreach (Activity updatedActivity in activities) {
            Console.WriteLine("Activity with ID \"{0}\" and name \"{1}\" was updated.",
                updatedActivity.id, updatedActivity.name);
          }
        } else {
          Console.WriteLine("No activities were updated.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to update activities. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
