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
using Google.Api.Ads.AdManager.Util.v201902;
using Google.Api.Ads.AdManager.v201902;

using System;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v201902
{
    /// <summary>
    ///  This code example updates user team associations by setting the
    ///  overridden access type to read only for all teams that the user belongs
    ///  to. To determine which users exists, run GetAllUsers.cs.
    /// </summary>
    public class UpdateUserTeamAssociations : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example updates user team associations by setting the " +
                    "overridden access type to read only for all teams that the user belongs to. " +
                    "To determine which users exists, run GetAllUsers.cs.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            UpdateUserTeamAssociations codeExample = new UpdateUserTeamAssociations();
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
                // Set the user id of the user team association to update.
                long userId = long.Parse(_T("INSERT_USER_ID_HERE"));

                // Set the team id of the user team association to update.
                long teamId = long.Parse(_T("INSERT_TEAM_ID_HERE"));

                // Create a statement to select the user team association.
                StatementBuilder statementBuilder = new StatementBuilder()
                    .Where("userId = :userId and teamId = :teamId")
                    .OrderBy("userId ASC, teamId ASC")
                    .Limit(1)
                    .AddValue("userId", userId)
                    .AddValue("teamId", teamId);

                try
                {
                    // Get user team associations by statement.
                    UserTeamAssociationPage page =
                        userTeamAssociationService.getUserTeamAssociationsByStatement(
                            statementBuilder.ToStatement());

                    UserTeamAssociation userTeamAssociation = page.results[0];

                    userTeamAssociation.overriddenTeamAccessType = TeamAccessType.READ_ONLY;

                    // Update the user team associations on the server.
                    UserTeamAssociation[] userTeamAssociations =
                        userTeamAssociationService.updateUserTeamAssociations(
                            new UserTeamAssociation[]
                            {
                                userTeamAssociation
                            });

                    if (userTeamAssociations != null)
                    {
                        foreach (UserTeamAssociation updatedUserTeamAssociation in
                            userTeamAssociations)
                        {
                            Console.WriteLine(
                                "User team association between user with ID \"{0}\" and team " +
                                "with ID \"{1}\" was updated to access type \"{2}\".",
                                updatedUserTeamAssociation.userId,
                                updatedUserTeamAssociation.teamId,
                                updatedUserTeamAssociation.overriddenTeamAccessType);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No user team associations updated.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(
                        "Failed to update user team associations. Exception says \"{0}\"",
                        e.Message);
                }
            }
        }
    }
}
