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
  /// This code example retrieves available creative field values for a given
  /// string and displays the names and ids.
  ///
  /// Tags: creativefield.getCreativeFieldValues
  /// </summary>
  class GetCreativeFieldValues : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example retrieves available creative field values for a given string " +
            "and displays the names and ids.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetCreativeFieldValues();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfaUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The Dfa user object running the code example.
    /// </param>
    public override void Run(DfaUser user) {
      // Create CreativeFieldRemoteService instance.
      CreativeFieldRemoteService service = (CreativeFieldRemoteService) user.GetService(
          DfaService.v1_20.CreativeFieldRemoteService);

      String searchString = _T("INSERT_SEARCH_STRING_CRITERIA_HERE");

      // Set up creative field value search criteria structure.
      CreativeFieldValueSearchCriteria creativeValueSearchCriteria =
          new CreativeFieldValueSearchCriteria();
      creativeValueSearchCriteria.pageSize = 10;
      creativeValueSearchCriteria.searchString = searchString;

      try {
        // Get creative field values for the selected criteria.
        CreativeFieldValueRecordSet creativeFieldValues =
            service.getCreativeFieldValues(creativeValueSearchCriteria);

        // Display creative field value names and ids.
        if (creativeFieldValues != null && creativeFieldValues.records != null) {
          foreach (CreativeFieldValue creativeFieldValue in creativeFieldValues.records) {
            Console.WriteLine("Creative field value with name \"{0}\" and id \"{1}\" was found.",
                creativeFieldValue.name, creativeFieldValue.id);
          }
        } else {
          Console.WriteLine("No creative field values found for your criteria");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to retrieve creative field values. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
