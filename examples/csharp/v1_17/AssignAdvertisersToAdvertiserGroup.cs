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
  /// This code example assigns a list of advertisers to an advertiser group.
  ///
  /// CAUTION: An advertiser that has campaigns associated with it cannot be
  /// removed from an advertiser group once assigned.
  ///
  /// Tags: advertisergroup.assignAdvertisersToAdvertiserGroup
  /// </summary>
  class AssignAdvertisersToAdvertiserGroup : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example assigns a list of advertisers to an advertiser group. \n\n" +
            "CAUTION: An advertiser that has campaigns associated with it cannot be removed " +
            "from an advertiser group once assigned.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new AssignAdvertisersToAdvertiserGroup();
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

      long[] advertiserIds = new long[] {
          long.Parse(_T("INSERT_ADVERTISER_ID1_HERE")),
          long.Parse(_T("INSERT_ADVERTISER_ID2_HERE"))
      };

      long advertiserGroupId = long.Parse(_T("INSERT_ADVERTISER_GROUP_ID_HERE"));

      try {
        // Assign the advertisers to the advertiser group.
        service.assignAdvertisersToAdvertiserGroup(advertiserGroupId, advertiserIds);

        Console.WriteLine("Assigned advertisers to advertiser group successfully.");
      } catch (Exception ex) {
        Console.WriteLine("Failed to add advertisers to advertiser group. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
