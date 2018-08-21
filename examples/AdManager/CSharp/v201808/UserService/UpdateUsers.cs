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
using Google.Api.Ads.AdManager.Util.v201808;
using Google.Api.Ads.AdManager.v201808;

using System;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v201808
{
    /// <summary>
    /// This code example updates a user by adding "Sr." to the end of its
    /// name. To determine which users exist, run GetAllUsers.cs.
    /// </summary>
    public class UpdateUsers : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example updates a user by adding 'Sr.' to the end of its " +
                    "name. To determine which users exist, run GetAllUsers.cs.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            UpdateUsers codeExample = new UpdateUsers();
            Console.WriteLine(codeExample.Description);
            codeExample.Run(new AdManagerUser());
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (UserService userService = user.GetService<UserService>())
            {
                // Set the user to update.
                long userId = long.Parse(_T("INSERT_USER_ID_HERE"));

                // Create a statement to get all users.
                StatementBuilder statementBuilder = new StatementBuilder()
                    .Where("id = :userId")
                    .OrderBy("id ASC")
                    .Limit(1)
                    .AddValue("userId", userId);


                try
                {
                    // Get users by statement.
                    UserPage page = userService.getUsersByStatement(statementBuilder.ToStatement());

                    User userToUpdate = page.results[0];

                    // Update user object by changing its name.
                    userToUpdate.name += " Sr.";

                    // Update the users on the server.
                    User[] users = userService.updateUsers(new User[]
                    {
                        userToUpdate
                    });

                    if (users != null)
                    {
                        foreach (User updatedUser in users)
                        {
                            Console.WriteLine(
                                "A user with ID = '{0}', name ='{1}', and role = '{2}'" +
                                " was updated.", updatedUser.id, updatedUser.name,
                                updatedUser.roleName);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No users updated.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to get user by ID. Exception says \"{0}\"",
                        e.Message);
                }
            }
        }
    }
}
