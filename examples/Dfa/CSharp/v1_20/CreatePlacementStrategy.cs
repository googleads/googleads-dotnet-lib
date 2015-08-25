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

using Google.Api.Ads.Dfa.Lib;
using Google.Api.Ads.Dfa.v1_20;

using System;
using System.Collections.Generic;
using System.Text;
using Google.Api.Ads.Common.Util;

namespace Google.Api.Ads.Dfa.Examples.CSharp.v1_20 {
  /// <summary>
  /// This code example creates a placement strategy with the given name.
  /// </summary>
  class CreatePlacementStrategy : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example creates a placement strategy with the given name.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new CreatePlacementStrategy();
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
          DfaService.v1_20.PlacementStrategyRemoteService);

      String placementStrategyName = _T("INSERT_PLACEMENT_STRATEGY_NAME_HERE");

      // Create placement strategy structure.
      PlacementStrategy placementStrategy = new PlacementStrategy();
      placementStrategy.id = 0;
      placementStrategy.name = placementStrategyName;

      try {
        // Create placement strategy.
        PlacementStrategySaveResult placementStrategySaveResult =
            service.savePlacementStrategy(placementStrategy);

        if (placementStrategySaveResult != null) {
          // Display placement strategy id.
          Console.WriteLine("Placement Strategy with id \"{0}\" was created.",
              placementStrategySaveResult.id);
        }
      } catch (Exception e) {
        Console.WriteLine("Failed to create placement strategy. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}
