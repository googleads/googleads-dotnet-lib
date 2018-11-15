// Copyright 2017, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdManager.Util.v201811;
using Google.Api.Ads.AdManager.v201811;

using System;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v201811
{
    /// <summary>
    /// This example gets all user team associations (i.e. teams) for a given user.
    /// </summary>
    public class GetUserTeamAssociationsForUser : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return
                    "This example gets all user team associations (i.e. teams) for a given user.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            GetUserTeamAssociationsForUser codeExample = new GetUserTeamAssociationsForUser();
            long userId = long.Parse("INSERT_USER_ID_HERE");
            Console.WriteLine(codeExample.Description);
            try
            {
                codeExample.Run(new AdManagerUser(), userId);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to get user team associations. Exception says \"{0}\"",
                    e.Message);
            }
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user, long userId)
        {
            using (UserTeamAssociationService userTeamAssociationService =
                user.GetService<UserTeamAssociationService>())
            {
                // Create a statement to select user team associations.
                int pageSize = StatementBuilder.SUGGESTED_PAGE_LIMIT;
                StatementBuilder statementBuilder = new StatementBuilder()
                    .Where("userId = :userId")
                    .OrderBy("userId ASC, teamId ASC")
                    .Limit(pageSize)
                    .AddValue("userId", userId);

                // Retrieve a small amount of user team associations at a time, paging through until
                // all user team associations have been retrieved.
                int totalResultSetSize = 0;
                do
                {
                    UserTeamAssociationPage page =
                        userTeamAssociationService.getUserTeamAssociationsByStatement(
                            statementBuilder.ToStatement());

                    // Print out some information for each user team association.
                    if (page.results != null)
                    {
                        totalResultSetSize = page.totalResultSetSize;
                        int i = page.startIndex;
                        foreach (UserTeamAssociation userTeamAssociation in page.results)
                        {
                            Console.WriteLine(
                                "{0}) User team association with user ID {1} and team ID {2} was " +
                                "found.",
                                i++, userTeamAssociation.userId, userTeamAssociation.teamId);
                        }
                    }

                    statementBuilder.IncreaseOffsetBy(pageSize);
                } while (statementBuilder.GetOffset() < totalResultSetSize);

                Console.WriteLine("Number of results found: {0}", totalResultSetSize);
            }
        }
    }
}
