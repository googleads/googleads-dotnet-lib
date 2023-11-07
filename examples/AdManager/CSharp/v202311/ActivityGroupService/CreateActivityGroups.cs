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

using System;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v202311
{
    /// <summary>
    /// This code example creates new activity groups. To determine which activity
    /// groups exist, run GetAllActivityGroups.cs.
    /// </summary>
    public class CreateActivityGroups : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return
                    "This code example creates new activity groups. To determine which activity " +
                    "groups exist, run GetAllActivityGroups.cs.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            CreateActivityGroups codeExample = new CreateActivityGroups();
            Console.WriteLine(codeExample.Description);
            codeExample.Run(new AdManagerUser());
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (ActivityGroupService activityGroupService =
                user.GetService<ActivityGroupService>())
            {
                // Set the ID of the advertiser company this activity group is associated
                // with.
                long advertiserCompanyId = long.Parse(_T("INSERT_ADVERTISER_COMPANY_ID_HERE"));

                // Create a short-term activity group.
                ActivityGroup shortTermActivityGroup = new ActivityGroup();
                shortTermActivityGroup.name = "Short-term activity group #" + GetTimeStamp();
                shortTermActivityGroup.companyIds = new long[]
                {
                    advertiserCompanyId
                };
                shortTermActivityGroup.clicksLookback = 1;
                shortTermActivityGroup.impressionsLookback = 1;

                // Create a long-term activity group.
                ActivityGroup longTermActivityGroup = new ActivityGroup();
                longTermActivityGroup.name = "Long-term activity group #" + GetTimeStamp();
                longTermActivityGroup.companyIds = new long[]
                {
                    advertiserCompanyId
                };
                longTermActivityGroup.clicksLookback = 30;
                longTermActivityGroup.impressionsLookback = 30;

                try
                {
                    // Create the activity groups on the server.
                    ActivityGroup[] activityGroups = activityGroupService.createActivityGroups(
                        new ActivityGroup[]
                        {
                            shortTermActivityGroup,
                            longTermActivityGroup
                        });

                    // Display results.
                    if (activityGroups != null)
                    {
                        foreach (ActivityGroup activityGroup in activityGroups)
                        {
                            Console.WriteLine(
                                "An activity group with ID \"{0}\" and name \"{1}\" was created.",
                                activityGroup.id, activityGroup.name);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No activity groups were created.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to create activity groups. Exception says \"{0}\"",
                        e.Message);
                }
            }
        }
    }
}
