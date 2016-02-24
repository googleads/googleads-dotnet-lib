// Copyright 2015, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.v201602;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201602 {
  /// <summary>
  /// This code example creates new users. To determine which users
  /// exist, run GetAllUsers.cs.
  /// </summary>
  class CreateUsers : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example creates new users. To determine which users " +
            "exist, run GetAllUsers.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new CreateUsers();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the UserService.
      UserService userService = (UserService) user.GetService(DfpService.v201602.UserService);

      // Set the user's email addresses and names.
      List<string[]> emailAndNames = new List<String[]>();
      emailAndNames.Add(new String[] {_T("INSERT_EMAIL_ADDRESS_HERE"), _T("INSERT_NAME_HERE")});
      emailAndNames.Add(new String[] {_T("INSERT_ANOTHER_EMAIL_ADDRESS_HERE"),
          _T("INSERT_ANOTHER_NAME_HERE")});

      // Roles can be obtained by running GetAllRoles.cs.
      long roleId = long.Parse(_T("INSERT_ROLE_ID_HERE"));

      // Create an array to store local user objects.
      User[] users = new User[emailAndNames.Count];

      for (int i = 0; i < users.Length; i++) {
        // Create the new user structure.
        User newUser = new User();
        newUser.email = emailAndNames[i][0];
        newUser.name = emailAndNames[i][1];
        newUser.roleId = roleId;
        newUser.preferredLocale = "en_US";

        users[i] = newUser;
      }

      try {
        // Create the users.
        users = userService.createUsers(users);

        if (users != null) {
          foreach (User newUser in users) {
            Console.WriteLine("A user with ID = '{0}', email = '{1}', and role = '{2}' " +
                "was created.", newUser.id, newUser.email, newUser.roleName);
          }
        } else {
          Console.WriteLine("No users created.");
        }
      } catch (Exception e) {
        Console.WriteLine("Failed to create users. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}
