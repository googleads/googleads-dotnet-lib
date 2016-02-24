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

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201602 {
  /// <summary>
  /// This code example gets all roles. This example can be used to determine
  /// which role ID is needed when getting and creating users.
  /// </summary>
  class GetAllRoles : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets all roles. This example can be used to determine which " +
            "role ID is needed when getting and creating users.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetAllRoles();
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

      try {
        // Get all roles.
        Role[] roles = userService.getAllRoles();
        int numRoles = 0;
        if (roles != null && roles.Length > 0) {
          foreach (Role role in roles) {
            Console.WriteLine("Role with ID = '{0}' and name ='{1}' was found.", role.id,
                role.name);
          }
          numRoles = roles.Length;
        }

        Console.WriteLine("Number of results found: " + numRoles);
      } catch (Exception e) {
        Console.WriteLine("Failed to get all roles. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}
