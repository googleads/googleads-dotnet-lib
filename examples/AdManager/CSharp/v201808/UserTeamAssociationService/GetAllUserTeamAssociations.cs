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
using Google.Api.Ads.AdManager.Util.v201808;
using Google.Api.Ads.AdManager.v201808;

using System;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v201808
{
    /// <summary>
    /// This example gets all user team associations.
    /// </summary>
    public class GetAllUserTeamAssociations : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get { return "This example gets all user team associations."; }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            GetAllUserTeamAssociations codeExample = new GetAllUserTeamAssociations();
            Console.WriteLine(codeExample.Description);
            try
            {
                codeExample.Run(new AdManagerUser());
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
        public void Run(AdManagerUser user)
        {
            using (UserTeamAssociationService userTeamAssociationService =
                user.GetService<UserTeamAssociationService>())
            {
                // Create a statement to select user team associations.
                int pageSize = StatementBuilder.SUGGESTED_PAGE_LIMIT;
                StatementBuilder statementBuilder =
                    new StatementBuilder().OrderBy("id ASC").Limit(pageSize);

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
                                "{0}) User team association with team id {1} and user id {2} " +
                                "was found.",
                                i++, userTeamAssociation.teamId, userTeamAssociation.userId);
                        }
                    }

                    statementBuilder.IncreaseOffsetBy(pageSize);
                } while (statementBuilder.GetOffset() < totalResultSetSize);

                Console.WriteLine("Number of results found: {0}", totalResultSetSize);
            }
        }
    }
}
