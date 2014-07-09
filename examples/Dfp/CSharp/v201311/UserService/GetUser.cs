// Copyright 2013, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.Dfp.v201311;

using System;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201311 {
  /// <summary>
  /// This code example gets a user by its ID. To create users, run
  /// CreateUsers.cs.
  ///
  /// Tags: UserService.getUser
  /// </summary>
  class GetUser : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This example gets a user by its ID. To create users, run CreateUsers.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetUser();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the UserService.
      UserService userService = (UserService) user.GetService(DfpService.v201311.UserService);

      // Set the ID of the user to get.
      long userId = long.Parse(_T("INSERT_USER_ID_HERE"));

      try {
        // Get the user.
        User usr = userService.getUser(userId);

        if (usr != null) {
          Console.WriteLine("User with ID = '{0}', email = '{1}', and role = '{2}' was found.",
              usr.id, usr.email, usr.roleName);
        } else {
          Console.WriteLine("No user found for this ID.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to get user by ID. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
