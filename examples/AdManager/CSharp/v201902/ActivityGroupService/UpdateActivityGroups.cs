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
using Google.Api.Ads.AdManager.v201902;
using Google.Api.Ads.AdManager.Util.v201902;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v201902
{
    /// <summary>
    /// This code example updates activity groups by adding a company. To
    /// determine which activity groups exist, run GetAllActivityGroups.cs.
    /// </summary>
    public class UpdateActivityGroups : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return
                    "This code example updates activity groups by adding a company. To determine " +
                    "which activity groups exist, run GetAllActivityGroups.cs.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            UpdateActivityGroups codeExample = new UpdateActivityGroups();
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
                // Set the ID of the activity group and the company to update it with.
                int activityGroupId = int.Parse(_T("INSERT_ACTIVITY_GROUP_ID_HERE"));
                long advertiserCompanyId = long.Parse(_T("INSERT_ADVERTISER_COMPANY_ID_HERE"));

                try
                {
                    // Get the activity group.
                    StatementBuilder statementBuilder = new StatementBuilder()
                        .Where("id = :id")
                        .OrderBy("id ASC")
                        .Limit(1)
                        .AddValue("id", activityGroupId);

                    ActivityGroupPage page =
                        activityGroupService.getActivityGroupsByStatement(statementBuilder
                            .ToStatement());

                    ActivityGroup activityGroup = page.results[0];

                    // Update the companies.
                    List<long> companyIds = new List<long>(activityGroup.companyIds);
                    companyIds.Add(advertiserCompanyId);
                    activityGroup.companyIds = companyIds.ToArray();

                    // Update the activity groups on the server.
                    ActivityGroup[] activityGroups = activityGroupService.updateActivityGroups(
                        new ActivityGroup[]
                        {
                            activityGroup
                        });

                    // Display results.
                    foreach (ActivityGroup updatedActivityGroup in activityGroups)
                    {
                        Console.WriteLine(
                            "Activity group with ID \"{0}\" and name \"{1}\" was updated.",
                            updatedActivityGroup.id, updatedActivityGroup.name);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to update activity groups. Exception says \"{0}\"",
                        e.Message);
                }
            }
        }
    }
}
