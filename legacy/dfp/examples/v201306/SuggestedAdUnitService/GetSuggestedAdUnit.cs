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

using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.v201306;

using System;
using System.Collections.Generic;
using Google.Api.Ads.Dfp.Util.v201306;

namespace Google.Api.Ads.Dfp.Examples.v201306 {
  /// <summary>
  /// This code example gets a suggested ad unit by its ID. To determine which
  /// suggested ad units exist, run GetAllSuggestedAdUnits.cs. This feature is
  /// only available to DFP premium solution networks.
  ///
  /// Tags: SuggestedAdUnitService.getSuggestedAdUnit
  /// </summary>
  class GetSuggestedAdUnit : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets a suggested ad unit by its ID. To determine which " +
            "suggested ad units exist, run GetAllSuggestedAdUnits.cs. This feature is only " +
            "available to DFP premium solution networks.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetSuggestedAdUnit();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the SuggestedAdUnitService.
      SuggestedAdUnitService suggestedAdUnitService = (SuggestedAdUnitService) user.GetService(
          DfpService.v201306.SuggestedAdUnitService);

      // Set the ID of the suggested ad unit to get.
      String suggestedAdUnitId = _T("INSERT_SUGGESTED_AD_UNIT_ID_HERE");

      try {
        // Get the suggested ad unit.
        SuggestedAdUnit suggestedAdUnit = suggestedAdUnitService.getSuggestedAdUnit(
            suggestedAdUnitId);

        if (suggestedAdUnit != null) {
          Console.WriteLine("Suggested ad unit with ID \"{0}\", and number of requests \"{1}\" " +
              "was found.", suggestedAdUnit.id, suggestedAdUnit.numRequests);
        } else {
          Console.WriteLine("No suggested ad unit found for this ID.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to get suggested ad units. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
