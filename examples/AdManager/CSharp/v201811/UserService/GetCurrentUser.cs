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
using Google.Api.Ads.AdManager.v201811;

using System;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v201811
{
    /// <summary>
    /// This code example gets current user. To create users, run CreateUsers.cs.
    /// </summary>
    public class GetCurrentUser : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get { return "This example gets current user. To create users, run CreateUsers.cs."; }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            GetCurrentUser codeExample = new GetCurrentUser();
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
                try
                {
                    // Get the current user.
                    User usr = userService.getCurrentUser();

                    if (usr != null)
                    {
                        Console.WriteLine(
                            "User with ID = '{0}', email = '{1}', and role = '{2}' is the " +
                            "current user.", usr.id, usr.email, usr.roleName);
                    }
                    else
                    {
                        Console.WriteLine("The current user could not be retrieved.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to get current user. Exception says \"{0}\"",
                        e.Message);
                }
            }
        }
    }
}
