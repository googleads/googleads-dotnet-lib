// Copyright 2011, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.Dfp.v201104;

using System;

namespace Google.Api.Ads.Dfp.Examples.v201104 {
  /// <summary>
  /// This code example gets all users. To create users, run CreateUsers.cs.
  ///
  /// Tags: UserService.getUsersByStatement
  /// </summary>
  class GetAllUsers : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets all users. To create users, run CreateUsers.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetAllUsers();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the UserService.
      UserService userService = (UserService) user.GetService(DfpService.v201104.UserService);

      // Sets defaults for page and Statement.
      UserPage page = new UserPage();
      Statement statement = new Statement();
      int offset = 0;

      try {
        do {
          // Create a Statement to get all users.
          statement.query = string.Format("LIMIT 500 OFFSET {0}", offset);

          // Get users by Statement.
          page = userService.getUsersByStatement(statement);

          if (page.results != null && page.results.Length > 0) {
            int i = page.startIndex;
            foreach (User usr in page.results) {
              Console.WriteLine("{0}) User with ID = '{1}', email = '{2}', and role = '{3}'" +
                  " was found.", i, usr.id, usr.email, usr.roleName);
              i++;
            }
          }
          offset += 500;
        } while (offset < page.totalResultSetSize);

        Console.WriteLine("Number of results found: {0}", page.totalResultSetSize);
      } catch (Exception ex) {
        Console.WriteLine("Failed to get all users. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
