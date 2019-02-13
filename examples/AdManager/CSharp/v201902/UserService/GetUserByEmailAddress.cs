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
using Google.Api.Ads.AdManager.Util.v201902;
using Google.Api.Ads.AdManager.v201902;

using System;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v201902
{
    /// <summary>
    /// This example gets users by email.
    /// </summary>
    public class GetUserByEmailAddress : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get { return "This example gets users by email."; }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            GetUserByEmailAddress codeExample = new GetUserByEmailAddress();
            string emailAddress = "INSERT_EMAIL_ADDRESS_HERE";
            Console.WriteLine(codeExample.Description);
            try
            {
                codeExample.Run(new AdManagerUser(), emailAddress);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to get users. Exception says \"{0}\"", e.Message);
            }
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user, string emailAddress)
        {
            using (UserService userService = user.GetService<UserService>())
            {
                // Create a statement to select users.
                int pageSize = StatementBuilder.SUGGESTED_PAGE_LIMIT;
                StatementBuilder statementBuilder = new StatementBuilder()
                    .Where("email = :email")
                    .OrderBy("id ASC")
                    .Limit(pageSize)
                    .AddValue("email", emailAddress);

                // Retrieve a small amount of users at a time, paging through until all
                // users have been retrieved.
                int totalResultSetSize = 0;
                do
                {
                    UserPage page = userService.getUsersByStatement(statementBuilder.ToStatement());

                    // Print out some information for each user.
                    if (page.results != null)
                    {
                        totalResultSetSize = page.totalResultSetSize;
                        int i = page.startIndex;
                        foreach (User usr in page.results)
                        {
                            Console.WriteLine("{0}) User with ID {1} and name \"{2}\" was found.",
                                i++, usr.id, usr.name);
                        }
                    }

                    statementBuilder.IncreaseOffsetBy(pageSize);
                } while (statementBuilder.GetOffset() < totalResultSetSize);

                Console.WriteLine("Number of results found: {0}", totalResultSetSize);
            }
        }
    }
}
