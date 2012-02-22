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
using Google.Api.Ads.Dfa.v1_17;

using System;
using System.Collections.Generic;
using System.Text;
using Google.Api.Ads.Common.Util;

namespace Google.Api.Ads.Dfa.Examples.CSharp.v1_17 {
  /// <summary>
  /// This code example retrieves available creative groups for a given
  /// advertiser and displays the name, id, advertiser id, and group number.
  /// To get an advertiser id, run GetAdvertisers.cs. Results are limited
  /// to the first 10.
  ///
  /// Tags: creativegroup.getCreativeGroups
  /// </summary>
  class GetCreativeGroups : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example retrieves available creative groups for a given advertiser " +
            "and displays the name, id, advertiser id, and group number. To get an advertiser id," +
            " run GetAdvertisers.cs. Results are limited to the first 10.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetCreativeGroups();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfaUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The Dfa user object running the code example.
    /// </param>
    public override void Run(DfaUser user) {
      // Create CreativeGroupRemoteService instance.
      CreativeGroupRemoteService service = (CreativeGroupRemoteService) user.GetService(
          DfaService.v1_17.CreativeGroupRemoteService);

      long advertiserId = long.Parse(_T("INSERT_ADVERTISER_ID_HERE"));

      // Set up creative group search criteria structure.
      CreativeGroupSearchCriteria creativeGroupSearchCriteria = new CreativeGroupSearchCriteria();
      creativeGroupSearchCriteria.advertiserIds = new long[] {advertiserId};

      try {
        // Get creatives groups for the selected criteria.
        CreativeGroupRecordSet creativeGroups =
            service.getCreativeGroups(creativeGroupSearchCriteria);

        // Display creative group names, ids, advertiser ids, and group numbers.
        if (creativeGroups != null && creativeGroups.records != null) {
          foreach (CreativeGroup creativeGroup in creativeGroups.records) {
            Console.WriteLine("Creative group with name \"{0}\" , id \"{1}\", advertiser id " +
                "\"{2}\" and group number \"{3}\" was found.", creativeGroup.name,
                creativeGroup.id, creativeGroup.advertiserId, creativeGroup.groupNumber);
          }
        } else {
          Console.WriteLine("No creative groups found for your search criteria");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to retrieve creative groups. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
