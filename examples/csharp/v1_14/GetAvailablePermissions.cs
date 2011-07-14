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
  /// This code example displays all of the available permissions that a user
  /// role or subnetwork may be endowed with. To get a subnetwork id, run
  /// GetSubnetworks.cs.
  ///
  /// A user role may not be set with more permissions than the subnetwork it
  /// belongs to. You may enter a subnetwork id to see the maximum permissions a
  /// user role belonging to it can have, or enter "0" as the subnetwork id to
  /// see all possible permissions.
  ///
  /// Tags: userrole.getAvailablePermissions
  /// </summary>
  class GetAvailablePermissions : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example displays all of the available permissions that a user role " +
          "or subnetwork may be endowed with. To get a subnetwork id, run GetSubnetworks.cs.\n\n" +
          "A user role may not be set with more permissions than the subnetwork it belongs to. " +
          "You may enter a subnetwork id to see the maximum permissions a user role belonging to " +
          "it can have, or enter '0' as the subnetwork id to see all possible permissions.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetAvailablePermissions();
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

      long subnetworkId = long.Parse(_T("INSERT_SUBNETWORK_ID_HERE"));

      try {
        // Get available permissions.
        Permission[] permissions = service.getAvailablePermissions(subnetworkId);

        // Display permission name and its id.
        if (permissions != null) {
          foreach (Permission permission in permissions) {
            Console.WriteLine("Permission with name \"{0}\" and id \"{1}\" was found.",
                permission.name, permission.id);
          }
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to get permission. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
