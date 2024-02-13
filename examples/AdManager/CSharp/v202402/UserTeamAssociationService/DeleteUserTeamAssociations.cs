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
using Google.Api.Ads.AdManager.Util.v202402;
using Google.Api.Ads.AdManager.v202402;

using System;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v202402
{
    /// <summary>
    /// This code example removes the user from all its teams. To determine which
    /// users exist, run GetAllUsers.cs.
    /// </summary>
    public class DeleteUserTeamAssociation : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return
                    "This code example removes the user from all its teams. To determine which " +
                    "users exist, run GetAllUsers.cs.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            DeleteUserTeamAssociation codeExample = new DeleteUserTeamAssociation();
            Console.WriteLine(codeExample.Description);
            codeExample.Run(new AdManagerUser());
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (UserTeamAssociationService userTeamAssociationService =
                user.GetService<UserTeamAssociationService>())
            {
                // Set the user to remove from its teams.
                long userId = long.Parse(_T("INSERT_USER_ID_HERE"));

                // Create filter text to select user team associations by the user ID.
                StatementBuilder statementBuilder = new StatementBuilder()
                    .Where("userId = :userId")
                    .OrderBy("userId ASC, teamId ASC")
                    .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT)
                    .AddValue("userId", userId);

                // Set default for page.
                UserTeamAssociationPage page = new UserTeamAssociationPage();

                try
                {
                    do
                    {
                        // Get user team associations by statement.
                        page = userTeamAssociationService.getUserTeamAssociationsByStatement(
                            statementBuilder.ToStatement());

                        if (page.results != null)
                        {
                            int i = page.startIndex;
                            foreach (UserTeamAssociation userTeamAssociation in page.results)
                            {
                                Console.WriteLine(
                                    "{0}) User team association between user with ID \"{1}\" and " +
                                    "team with ID \"{2}\" will be deleted.", i,
                                    userTeamAssociation.userId, userTeamAssociation.teamId);
                                i++;
                            }
                        }

                        statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
                    } while (statementBuilder.GetOffset() < page.totalResultSetSize);

                    Console.WriteLine("Number of teams that the user will be removed from: " +
                        page.totalResultSetSize);

                    if (page.totalResultSetSize > 0)
                    {
                        // Modify statement for action.
                        statementBuilder.RemoveLimitAndOffset();

                        // Create action.
                        DeleteUserTeamAssociations action = new DeleteUserTeamAssociations();

                        // Perform action.
                        UpdateResult result =
                            userTeamAssociationService.performUserTeamAssociationAction(action,
                                statementBuilder.ToStatement());

                        // Display results.
                        if (result != null && result.numChanges > 0)
                        {
                            Console.WriteLine("Number of teams that the user was removed from: " +
                                result.numChanges);
                        }
                        else
                        {
                            Console.WriteLine("No user team associations were deleted.");
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(
                        "Failed to delete user team associations. Exception says \"{0}\"",
                        e.Message);
                }
            }
        }
    }
}
