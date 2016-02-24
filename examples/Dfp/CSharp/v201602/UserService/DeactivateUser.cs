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
using Google.Api.Ads.Dfp.Util.v201602;
using Google.Api.Ads.Dfp.v201602;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201602 {
  /// <summary>
  /// This code example deactivates a user. Deactivated users can no longer make
  /// requests to the API. The user making the request cannot deactivate itself.
  /// To determine which users exist, run GetAllUsers.cs.
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
      UserService userService = (UserService) user.GetService(DfpService.v201602.UserService);

      // Set the ID of the user to deactivate
      long userId = long.Parse(_T("INSERT_USER_ID_HERE"));

      // Create statement text to select user by id.
      StatementBuilder statementBuilder = new StatementBuilder()
          .Where("id = :userId")
          .OrderBy("id ASC")
          .Limit(1)
          .AddValue("userId", userId);

      // Sets default for page.
      UserPage page = new UserPage();
      List<string> userIds = new List<string>();

      try {
        do {
          // Get users by statement.
          page = userService.getUsersByStatement(statementBuilder.ToStatement());

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

          statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
        } while (statementBuilder.GetOffset() < page.totalResultSetSize);

        Console.WriteLine("Number of users to be deactivated: {0}", page.totalResultSetSize);

        if (userIds.Count > 0) {
          // Modify statement for action.
          statementBuilder.RemoveLimitAndOffset();

          // Create action.
          DeactivateUsers action = new DeactivateUsers();

          // Perform action.
          UpdateResult result = userService.performUserAction(action,
              statementBuilder.ToStatement());

          // Display results.
          if (result != null && result.numChanges > 0) {
            Console.WriteLine("Number of users deactivated: {0}" + result.numChanges);
          } else {
            Console.WriteLine("No users were deactivated.");
          }
        }
      } catch (Exception e) {
        Console.WriteLine("Failed to deactivate users. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}
