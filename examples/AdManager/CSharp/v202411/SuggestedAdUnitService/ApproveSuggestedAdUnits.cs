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
using Google.Api.Ads.AdManager.v202411;

using System;
using System.Collections.Generic;

using Google.Api.Ads.AdManager.Util.v202411;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v202411
{
    /// <summary>
    /// This code example approves all suggested ad units with 50 or more
    /// requests. This feature is only available to Ad Manager 360 networks.
    /// </summary>
    public class ApproveSuggestedAdUnits : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return
                    "This code example approves all suggested ad units with 50 or more requests. " +
                    "This feature is only available to Ad Manager 360 networks.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            ApproveSuggestedAdUnits codeExample = new ApproveSuggestedAdUnits();
            Console.WriteLine(codeExample.Description);
            codeExample.Run(new AdManagerUser());
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (SuggestedAdUnitService suggestedAdUnitService =
                user.GetService<SuggestedAdUnitService>())
            {
                // Set the number of requests for suggested ad units greater than which to approve.
                long NUMBER_OF_REQUESTS = 50L;

                // Create statement to select all suggested ad units that are highly requested.
                StatementBuilder statementBuilder = new StatementBuilder()
                    .Where("numRequests > :numRequests").OrderBy("id ASC")
                    .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT)
                    .AddValue("numRequests", NUMBER_OF_REQUESTS);

                // Set default for page.
                SuggestedAdUnitPage page = new SuggestedAdUnitPage();

                try
                {
                    do
                    {
                        // Get suggested ad units by statement.
                        page = suggestedAdUnitService.getSuggestedAdUnitsByStatement(
                            statementBuilder.ToStatement());

                        int i = 0;
                        if (page != null && page.results != null)
                        {
                            foreach (SuggestedAdUnit suggestedAdUnit in page.results)
                            {
                                Console.WriteLine(
                                    "{0}) Suggested ad unit with ID \"{1}\", and \"{2}\" will be " +
                                    "approved.", i, suggestedAdUnit.id,
                                    suggestedAdUnit.numRequests);
                                i++;
                            }
                        }

                        statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
                    } while (statementBuilder.GetOffset() < page.totalResultSetSize);

                    Console.WriteLine("Number of suggested ad units to be approved: " +
                        page.totalResultSetSize);

                    // Modify statement for action.
                    statementBuilder.RemoveLimitAndOffset();

                    // Create action.
                    Google.Api.Ads.AdManager.v202411.ApproveSuggestedAdUnits action =
                        new Google.Api.Ads.AdManager.v202411.ApproveSuggestedAdUnits();

                    // Perform action.
                    SuggestedAdUnitUpdateResult result =
                        suggestedAdUnitService.performSuggestedAdUnitAction(action,
                            statementBuilder.ToStatement());

                    // Display results.
                    if (result != null && result.numChanges > 0)
                    {
                        Console.WriteLine("Number of new ad units created: " +
                            result.newAdUnitIds.Length);
                    }
                    else
                    {
                        Console.WriteLine("No suggested ad units were approved.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(
                        "Failed to approve suggested ad units. Exception says \"{0}\"", e.Message);
                }
            }
        }
    }
}
