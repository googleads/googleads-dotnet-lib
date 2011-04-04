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
using Google.Api.Ads.Dfa.v1_12;

using System;
using System.Collections.Generic;
using System.Text;
using Google.Api.Ads.Common.Util;

namespace Google.Api.Ads.Dfa.Examples.CSharp.v1_12 {
  /// <summary>
  /// This code example displays user role name, id, subnetwork id, number of
  /// assigned users, and assigned permissions for the given search criteria.
  /// Results are limited to the first 10 records.
  /// </summary>
  class GetUserRoles : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This example displays user role name, id, subnetwork id, number of assigned users" +
          ", and assigned permissions for the given search criteria. Results are limited to the" +
          " first 10 records.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetUserRoles();
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
          DfaService.v1_12.UserRoleRemoteService);

      String searchString = _T("INSERT_SEARCH_STRING_CRITERIA_HERE");

      // Set user role search criteria.
      UserRoleSearchCriteria userRoleSearchCriteria = new UserRoleSearchCriteria();
      userRoleSearchCriteria.pageSize = 10;
      userRoleSearchCriteria.searchString = searchString;

      try {
        // Get user roles that match the search criteria.
        UserRoleRecordSet result = service.getUserRoles(userRoleSearchCriteria);

        // Display user role names, ids, subnetwork ids, number of assigned users,
        // and assigned permissions.
        if (result != null && result.userRoles != null) {
          foreach (UserRole userRole in result.userRoles) {
            Console.WriteLine("User role with name \"{0}\", id \"{1}\", subnetwork id \"{2}\", " +
                "and assigned to \"{3}\" users was found.", userRole.name, userRole.id,
                userRole.subnetworkId, userRole.totalAssignedUsers);

            if (userRole.permissions != null && userRole.permissions.Length != 0) {
              Console.WriteLine("    The above user role has the following permissions:");
              foreach (Permission permission in userRole.permissions) {
                Console.WriteLine("        Permission with name \"{0}\" and id \"{1}\".",
                    permission.name, permission.id);
              }
            } else {
              Console.WriteLine("    The above user role has no permissions assigned.");
            }
          }
        } else {
          Console.WriteLine("No user roles found for your criteria.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to retrieve user roles. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
