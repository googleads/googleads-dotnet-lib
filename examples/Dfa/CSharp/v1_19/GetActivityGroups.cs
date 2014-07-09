// Copyright 2012, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.Dfa.v1_19;

using System;
using System.Collections.Generic;
using System.Text;
using Google.Api.Ads.Common.Util;

namespace Google.Api.Ads.Dfa.Examples.CSharp.v1_19 {
  /// <summary>
  /// This code example displays activity groups names and ids for a given
  /// advertiser. To create an advertiser, run CreateAdvertiser.cs.
  ///
  /// Tags: spotlight.getSpotlightActivityGroups
  /// </summary>
  class GetActivityGroups : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example displays activity groups names and ids for a given advertiser. " +
            "To create an advertiser, run CreateAdvertiser.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetActivityGroups();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfaUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The Dfa user object running the code example.
    /// </param>
    public override void Run(DfaUser user) {
      // Create SpotlightRemoteService instance.
      SpotlightRemoteService service = (SpotlightRemoteService) user.GetService(
          DfaService.v1_19.SpotlightRemoteService);

      long advertiserId = long.Parse(_T("INSERT_ADVERTISER_ID_HERE"));

      // Set activity group search criteria structure and
      // use advertiser id as search criteria.
      SpotlightActivityGroupSearchCriteria activityGroupSearchCriteria =
          new SpotlightActivityGroupSearchCriteria();
      activityGroupSearchCriteria.advertiserId = advertiserId;

      try {
        // Get activity group.
        SpotlightActivityGroupRecordSet recordSet =
            service.getSpotlightActivityGroups(activityGroupSearchCriteria);

        // Display activity group names and ids.
        if (recordSet.records != null) {
          foreach (SpotlightActivityGroup result in recordSet.records) {
           Console.WriteLine("Activity group with name \"{0}\" and id \"{1}\" was found.",
              result.name, result.id);
          }
        } else {
          Console.WriteLine("No activity groups found for your criteria.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to retrieve activity groups. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
