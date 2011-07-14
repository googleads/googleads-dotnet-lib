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

using Google.Api.Ads.Dfa.Lib;
using Google.Api.Ads.Dfa.v1_14;

using System;
using System.Collections.Generic;
using System.Text;
using Google.Api.Ads.Common.Util;

namespace Google.Api.Ads.Dfa.Examples.CSharp.v1_14 {
  /// <summary>
  /// This code example creates a user role in a given DFA subnetwork. To get
  /// the subnetwork id, run GetSubnetworks.cs. To get the available
  /// permissions, run GetAvailablePermissions.cs. To get the parent user role
  /// id, run GetUserRoles.cs.
  ///
  /// Tags: userrole.saveUserRole
  /// </summary>
  class CreateUserRole : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example creates a user role in a given DFA subnetwork. To get the " +
            "subnetwork id, run GetSubnetworks.cs. To get the available permissions, run " +
            "GetAvailablePermissions.cs. To get the parent user role id, run GetUserRoles.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new CreateUserRole();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfaUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The Dfa user object running the code example.
    /// </param>
    public override void Run(DfaUser user) {
      // Create UserRoleRemoteService instance.
      UserRoleRemoteService service = (UserRoleRemoteService) user.GetService(
          DfaService.v1_14.UserRoleRemoteService);

      String userRoleName = _T("INSERT_USER_ROLE_NAME_HERE");
      long subnetworkId = long.Parse(_T("INSERT_SUBNETWORK_ID_HERE"));
      long parentUserRoleId = long.Parse(_T("INSERT_PARENT_USER_ROLE_ID_HERE"));
      long permission1Id = long.Parse(_T("INSERT_FIRST_PERMISSION_ID_HERE"));
      long permission2Id = long.Parse(_T("INSERT_SECOND_PERMISSION_ID_HERE"));

      // Create user role structure.
      UserRole userRole = new UserRole();
      userRole.id = 0;
      userRole.name = userRoleName;
      userRole.subnetworkId = subnetworkId;
      userRole.parentUserRoleId = parentUserRoleId;

      // Create a permission object to represent each permission this user role
      // has.
      Permission permission1 = new Permission();
      permission1.id = permission1Id;
      Permission permission2 = new Permission();
      permission2.id = permission2Id;

      // Add the permissions to the user role.
      userRole.permissions = new Permission[] {permission1, permission2};

      try {
        // Create user role.
        UserRoleSaveResult userRoleResult = service.saveUserRole(userRole);

        if (userRoleResult != null) {
          // Display user role id.
          Console.WriteLine("User role with id \"{0} was created.", userRoleResult.id);
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to create user role. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
