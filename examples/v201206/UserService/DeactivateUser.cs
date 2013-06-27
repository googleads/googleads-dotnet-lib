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
using Google.Api.Ads.Dfp.Util.v201206;
using Google.Api.Ads.Dfp.v201206;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.Dfp.Examples.v201206 {
  /// <summary>
  /// This code example deactivates a user. Deactivated users can no longer make
  /// requests to the API. The user making the request cannot deactivate itself.
  /// To determine which users exist, run GetAllUsers.cs.
  ///
  /// Tags: UserService.getUsersByStatement, UserService.performUserAction
  /// </summary>
  class DeactivateUser : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example deactivates a user. Deactivated users can no longer make " +
            "requests to the API. The user making the request cannot deactivate itself. " +
            "To determine which users exist, run GetAllUsers.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new DeactivateUser();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the UserService.
      UserService userService = (UserService) user.GetService(DfpService.v201206.UserService);

      // Set the ID of the user to deactivate
      long userId = long.Parse(_T("INSERT_USER_ID_HERE"));

      // Create Statement text to select user by id.
      string statementText = "WHERE id = :userId LIMIT 500";
      Statement statement = new StatementBuilder("").AddValue("userId", userId).ToStatement();

      // Sets defaults for page and offset.
      UserPage page = new UserPage();
      int offset = 0;
      List<string> userIds = new List<string>();

      try {
        do {
          // Create a Statement to page through users.
          statement.query = string.Format("{0} OFFSET {1}", statementText, offset);

          // Get users by Statement.
          page = userService.getUsersByStatement(statement);

          if (page.results != null && page.results.Length > 0) {
            int i = page.startIndex;
            foreach (User userResult in page.results) {
              Console.WriteLine("{0}) User with ID = '{1}', email = '{2}', and status = '{3}'" +
                 " will be deactivated.", i, userResult.id, userResult.email,
                 userResult.isActive ? "ACTIVE" : "INACTIVE");
              userIds.Add(userResult.id.ToString());
              i++;
            }
          }

          offset += 500;
        } while (offset < page.totalResultSetSize);

        Console.WriteLine("Number of users to be deactivated: {0}", page.totalResultSetSize);

        if (userIds.Count > 0) {
          // Create action Statement.
          statement = new StatementBuilder(
              string.Format("WHERE id IN ({0})", string.Join(",", userIds.ToArray()))).
              ToStatement();

          // Create action.
          DeactivateUsers action = new DeactivateUsers();

          // Perform action.
          UpdateResult result = userService.performUserAction(action, statement);

          // Display results.
          if (result != null && result.numChanges > 0) {
            Console.WriteLine("Number of users deactivated: {0}" + result.numChanges);
          } else {
            Console.WriteLine("No users were deactivated.");
          }
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to deactivate users. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
