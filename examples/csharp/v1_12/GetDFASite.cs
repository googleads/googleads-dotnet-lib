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
  /// This code example gets existing DFA sites based on a given search
  /// criteria. Results are limited to the first 10.
  /// </summary>
  class GetDFASite : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets existing DFA sites based on a given search criteria. " +
            "Results are limited to the first 10.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetDFASite();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfaUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The Dfa user object running the code example.
    /// </param>
    public override void Run(DfaUser user) {
      // Create SiteRemoteService instance.
      SiteRemoteService service = (SiteRemoteService) user.GetService(
          DfaService.v1_12.SiteRemoteService);

      String searchString = _T("INSERT_SEARCH_STRING_CRITERIA_HERE");

      // Create DFA site search criteria structure.
      DfaSiteSearchCriteria searchCriteria = new DfaSiteSearchCriteria();
      searchCriteria.pageSize = 10;
      searchCriteria.searchString = searchString;

      try {
        // Get the sites.

        DfaSiteRecordSet dfaSiteRecordSet = service.getDfaSites(searchCriteria);

        // Display DFA site names and ids.
        if (dfaSiteRecordSet != null && dfaSiteRecordSet.records != null) {
          foreach (DfaSite dfaSite in dfaSiteRecordSet.records) {
            Console.WriteLine("DFA site with name \"{0}\" and id \"{1}\" was found.",
                dfaSite.name, dfaSite.id);
          }
        } else {
          Console.WriteLine("No DFA sites found for your search criteria.");
        }

      } catch (Exception ex) {
        Console.WriteLine("Failed to retrieve DFA sites. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
