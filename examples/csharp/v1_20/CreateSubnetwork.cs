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

using Google.Api.Ads.Dfa.Lib;
using Google.Api.Ads.Dfa.v1_20;

using System;
using System.Collections.Generic;
using System.Text;
using Google.Api.Ads.Common.Util;

namespace Google.Api.Ads.Dfa.Examples.CSharp.v1_20 {
  /// <summary>
  /// This code example creates a subnetwork in a given DFA network. To get
  /// the network id, run Authenticate.cs. To get the available permissions,
  /// run GetAvailablePermissions.cs.
  ///
  /// Tags: subnetwork.saveSubnetwork
  /// </summary>
  class CreateSubnetwork : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example creates a subnetwork in a given DFA network. To get the " +
            "network id, run Authenticate.cs. To get the available permissions, run " +
            "GetAvailablePermissions.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new CreateSubnetwork();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfaUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The Dfa user object running the code example.
    /// </param>
    public override void Run(DfaUser user) {
      // Create SubnetworkRemoteService instance.
      SubnetworkRemoteService service = (SubnetworkRemoteService) user.GetService(
          DfaService.v1_20.SubnetworkRemoteService);

      long networkId = long.Parse(_T("INSERT_NETWORK_ID_HERE"));
      String subnetworkName = _T("INSERT_SUBNETWORK_NAME_HERE");
      long permission1 = long.Parse(_T("INSERT_FIRST_PERMISSION_ID_HERE"));
      long permission2 = long.Parse(_T("INSERT_SECOND_PERMISSION_ID_HERE"));

      // Create subnetwork structure.
      Subnetwork subnetwork = new Subnetwork();
      subnetwork.id = 0;
      subnetwork.name = subnetworkName;
      subnetwork.networkId = networkId;

      // Create an array of all permissions assigned to this subnetwork and add
      // it to the subnetwork structure. To get list of available permissions,
      // run GetAvailablePermissions.cs.
      subnetwork.availablePermissions = new long[] {permission1, permission2};

      try {
        // Create subnetwork.
        SubnetworkSaveResult subnetworkSaveResult = service.saveSubnetwork(subnetwork);

        // Display subnetwork id.
        Console.WriteLine("Subnetwork with id \"{0}\" was created.", subnetworkSaveResult.id);
      } catch (Exception ex) {
        Console.WriteLine("Failed to create subnetwork. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
