// Copyright 2012, Google Inc. All Rights Reserved.
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

// Author: api.anash@gmail.com (Anash P. Oommen)

using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.v201206;

using System;

namespace Google.Api.Ads.Dfp.Examples.v201206 {
  /// <summary>
  /// This code example updates all users by adding "Sr." to the end of each
  /// name (after a very large baby boom and lack of creativity). To
  /// determine which users exist, run GetAllUsers.cs.
  ///
  /// Tags: UserService.getUsersByStatement, UserService.updateUsers
  /// </summary>
  class UpdateUsers : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example updates all users by adding 'Sr.' to the end of each " +
            "name (after a very large baby boom and lack of creativity). To " +
            "determine which users exist, run GetAllUsers.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new UpdateUsers();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="dfpUser">The DFP user object running the code example.</param>
    public override void Run(DfpUser dfpUser) {
      // Get the UserService.
      UserService userService = (UserService) dfpUser.GetService(DfpService.v201206.UserService);

      // Create a Statement to get all users.
      Statement statement = new Statement();
      statement.query = "LIMIT 500";

      try {
        // Get users by Statement.
        UserPage page = userService.getUsersByStatement(statement);

        if (page.results != null && page.results.Length > 0) {
          User[] users = page.results;

          // Update each local users object by changing its name.
          foreach (User user in users) {
            user.name = user.name + " Sr.";
          }

          // Update the users on the server.
          users = userService.updateUsers(users);

          if (users != null) {
            foreach (User user in users) {
              Console.WriteLine("A user with ID = '{0}', name ='{1}', and role = '{2}'" +
                  " was updated.", user.id, user.name, user.roleName);
            }
          } else {
            Console.WriteLine("No users updated.");
          }
        } else {
          Console.WriteLine("No users found to update.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to get user by ID. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
