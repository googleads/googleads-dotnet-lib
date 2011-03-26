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
using Google.Api.Ads.Dfa.v1_13;

using System;
using System.Collections.Generic;
using System.Text;
using Google.Api.Ads.Common.Util;

namespace Google.Api.Ads.Dfa.Examples.v1_13 {
  /// <summary>
  /// This code example displays available placement strategies for a given
  /// search string. Results are limited to 10.
  ///
  /// Tags: strategy.getPlacementStrategiesByCriteria
  /// </summary>
  class GetPlacementStrategies : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example displays available placement strategies for a given search " +
            "string. Results are limited to 10.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetPlacementStrategies();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfaUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The Dfa user object running the code example.
    /// </param>
    public override void Run(DfaUser user) {
      // Create PlacementStrategyRemoteService instance.
      PlacementStrategyRemoteService service = (PlacementStrategyRemoteService) user.GetService(
          DfaService.v1_13.PlacementStrategyRemoteService);

      String searchString = _T("INSERT_SEARCH_STRING_CRITERIA_HERE");

      // Create placement strategy search criteria structure.
      PlacementStrategySearchCriteria placementStrategySearchCriteria =
          new PlacementStrategySearchCriteria();
      placementStrategySearchCriteria.pageSize = 10;
      placementStrategySearchCriteria.searchString = searchString;

      try {
        // Get placement strategy record set.
        PlacementStrategyRecordSet placementStrategyRecordSet =
            service.getPlacementStrategiesByCriteria(placementStrategySearchCriteria);

        // Display placement strategy names, ids and descriptions.
        if (placementStrategyRecordSet != null && placementStrategyRecordSet.records != null) {
          foreach (PlacementStrategy strategy in placementStrategyRecordSet.records) {
            Console.WriteLine("Placement Strategy with name \"{0}\" and id \"{1}\" was found.",
                strategy.name, strategy.id);
          }
        } else {
          Console.WriteLine("No placement strategies found for your criteria.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to retrieve placement strategies. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
