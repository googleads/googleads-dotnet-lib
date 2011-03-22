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
using Google.Api.Ads.Dfa.v1_11;

using System;
using System.Collections.Generic;
using System.Text;
using Google.Api.Ads.Common.Util;

namespace Google.Api.Ads.Dfa.Examples.v1_11 {
  /// <summary>
  /// This code example displays subnetwork names, ids, and subnetwork ids for
  /// a given search string. Results are limited to 10.
  /// </summary>
  class GetSubnetworks : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example displays subnetwork names, ids, and subnetwork ids for a given " +
            "search string. Results are limited to 10.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetSubnetworks();
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
          DfaService.v1_11.SubnetworkRemoteService);

      String searchString = _T("INSERT_SEARCH_STRING_CRITERIA_HERE");

      // Set subnetwork search criteria.
      SubnetworkSearchCriteria searchCriteria = new SubnetworkSearchCriteria();
      searchCriteria.pageSize = 10;
      searchCriteria.searchString = searchString;

      try {
        // Get subnetworks.
        SubnetworkRecordSet subnetworks = service.getSubnetworks(searchCriteria);

        // Display subnetwork names, ids, and subnetwork ids.
        if (subnetworks != null && subnetworks.records != null) {
          foreach (Subnetwork subNetwork in subnetworks.records) {
            Console.WriteLine("Subnetwork with name \"{0}\", id \"{1}\", and Subnetwork id " +
                "\"{2}\" was found.", subNetwork.name, subNetwork.id, subNetwork.networkId);
          }
        } else {
          Console.WriteLine("No subnetworks found for your criteria.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to retrieve subnetworks. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
