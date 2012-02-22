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
  /// This code example displays advertiser group name, id, and advertiser
  /// count for the given search criteria. Results are limited to the first 10
  /// records.
  ///
  /// Tags: advertisergroup.getAdvertiserGroups
  /// </summary>
  class GetAdvertiserGroups : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example displays advertiser group name, id, and advertiser count for " +
            "the given search criteria. Results are limited to the first 10 records.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetAdvertiserGroups();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfaUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The Dfa user object running the code example.
    /// </param>
    public override void Run(DfaUser user) {
      // Create AdvertiserGroupRemoteService instance.
      AdvertiserGroupRemoteService service = (AdvertiserGroupRemoteService) user.GetService(
          DfaService.v1_17.AdvertiserGroupRemoteService);

      String searchString = _T("INSERT_SEARCH_STRING_CRITERIA_HERE");

      // Create advertiser group search criteria structure.
      AdvertiserGroupSearchCriteria advGroupSearchCriteria = new AdvertiserGroupSearchCriteria();
      advGroupSearchCriteria.pageSize = 10;
      advGroupSearchCriteria.searchString = searchString;

      try {
        // Get advertiser group record set.
        AdvertiserGroupRecordSet advertiserGroupRecordSet =
          service.getAdvertiserGroups(advGroupSearchCriteria);

        // Display advertiser group names, ids and advertiser count.
        if (advertiserGroupRecordSet != null && advertiserGroupRecordSet.records != null) {
          foreach (AdvertiserGroup advertiserGroup in advertiserGroupRecordSet.records) {
            Console.WriteLine("Advertiser Group with name \"{0}\", id \"{1}\", containing \"{2}\"" +
                " advertisers was found.", advertiserGroup.name, advertiserGroup.id,
                advertiserGroup.advertiserCount);
          }
        } else {
          Console.WriteLine("No advertiser groups found for your search criteria.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to retrieve advertiser groups. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
