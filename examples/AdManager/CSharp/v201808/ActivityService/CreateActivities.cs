// Copyright 2018, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdManager.v201808;

using System;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v201808
{
    /// <summary>
    /// This code example creates new activities. To determine which activities
    /// exist, run GetAllActivities.cs.
    /// </summary>
    public class CreateActivities : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example creates new activities. To determine which activities " +
                    "exist, run GetAllActivities.cs.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            CreateActivities codeExample = new CreateActivities();
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
                // Set the ID of the activity group this activity is associated with.
                int activityGroupId = int.Parse(_T("INSERT_ACTIVITY_GROUP_ID_HERE"));

                // Create a daily visits activity.
                Activity dailyVisitsActivity = new Activity();
                dailyVisitsActivity.name = "Activity #" + GetTimeStamp();
                dailyVisitsActivity.activityGroupId = activityGroupId;
                dailyVisitsActivity.type = ActivityType.DAILY_VISITS;

                // Create a custom activity.
                Activity customActivity = new Activity();
                customActivity.name = "Activity #" + GetTimeStamp();
                customActivity.activityGroupId = activityGroupId;
                customActivity.type = ActivityType.CUSTOM;

                try
                {
                    // Create the activities on the server.
                    Activity[] activities = activityService.createActivities(new Activity[]
                    {
                        dailyVisitsActivity,
                        customActivity
                    });

                    // Display results.
                    if (activities != null)
                    {
                        foreach (Activity newActivity in activities)
                        {
                            Console.WriteLine(
                                "An activity with ID \"{0}\", name \"{1}\", and type \"{2}\" " +
                                "was created.", newActivity.id, newActivity.name, newActivity.type);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No activities were created.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to create activities. Exception says \"{0}\"",
                        e.Message);
                }
            }
        }
    }
}
