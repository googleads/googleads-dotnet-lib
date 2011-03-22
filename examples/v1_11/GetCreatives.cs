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
  /// This code example retrieves available creatives for a given advertiser
  /// and displays the name and id. To create an advertiser, run
  /// CreateAdvertiser.cs. Results are limited to the first 10.
  /// </summary>
  class GetCreatives : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example retrieves available creatives for a given advertiser and " +
            "displays the name and id. To create an advertiser, run CreateAdvertiser.cs. " +
            "Results are limited to the first 10.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetCreatives();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfaUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The Dfa user object running the code example.
    /// </param>
    public override void Run(DfaUser user) {
      // Create CreativeRemoteService instance.
      CreativeRemoteService service = (CreativeRemoteService) user.GetService(
          DfaService.v1_11.CreativeRemoteService);

      long advertiserId = long.Parse(_T("INSERT_ADVERTISER_ID_HERE"));

      // Set up creative search criteria structure.
      CreativeSearchCriteria creativeSearchCriteria = new CreativeSearchCriteria();
      creativeSearchCriteria.pageSize = 10;
      creativeSearchCriteria.advertiserId = advertiserId;

      try {
        // Get creatives for the selected criteria.
        CreativeRecordSet creatives = service.getCreatives(creativeSearchCriteria);

        // Display creative name and its id.
        if (creatives.records != null) {
          foreach (CreativeBase result in creatives.records) {
            Console.WriteLine("Creative with name \"{0}\" and id \"{1}\" was found.",
                result.name, result.id);
          }
        } else {
          Console.WriteLine("No creatives found for your criteria");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to retrieve creatives. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
