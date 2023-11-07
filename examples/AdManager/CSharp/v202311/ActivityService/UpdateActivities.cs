// Copyright 2019 Google LLC
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

using Google.Api.Ads.AdManager.Lib;
using Google.Api.Ads.AdManager.v202311;
using Google.Api.Ads.AdManager.Util.v202311;

using System;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v202311
{
    /// <summary>
    /// This code example updates activity expected URLs. To determine which
    /// activities exist, run GetAllActivities.cs.
    /// </summary>
    public class UpdateActivities : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example updates activity expected URLs. To determine which " +
                    "activities exist, run GetAllActivities.cs.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            UpdateActivities codeExample = new UpdateActivities();
            Console.WriteLine(codeExample.Description);
            codeExample.Run(new AdManagerUser());
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (ActivityService activityService = user.GetService<ActivityService>())
            {
                // Set the ID of the activity to update.
                int activityId = int.Parse(_T("INSERT_ACTIVITY_ID_HERE"));

                try
                {
                    // Get the activity.
                    StatementBuilder statemetnBuilder = new StatementBuilder()
                        .Where("id = :id")
                        .OrderBy("id ASC")
                        .Limit(1)
                        .AddValue("id", activityId);

                    ActivityPage page =
                        activityService.getActivitiesByStatement(statemetnBuilder.ToStatement());
                    Activity activity = page.results[0];

                    // Update the expected URL.
                    activity.expectedURL = "https://www.google.com";

                    // Update the activity on the server.
                    Activity[] activities = activityService.updateActivities(new Activity[]
                    {
                        activity
                    });

                    foreach (Activity updatedActivity in activities)
                    {
                        Console.WriteLine("Activity with ID \"{0}\" and name \"{1}\" was updated.",
                            updatedActivity.id, updatedActivity.name);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to update activities. Exception says \"{0}\"",
                        e.Message);
                }
            }
        }
    }
}
