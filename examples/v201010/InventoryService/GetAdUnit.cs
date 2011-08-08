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

using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.v201010;

using System;

namespace Google.Api.Ads.Dfp.Examples.v201010 {
  /// <summary>
  /// This code example gets an ad unit by its ID. To determine which ad units
  /// exist, run GetInventoryTree.cs or GetAllAdUnits.cs.
  ///
  /// Tags: InventoryService.getAdUnit
  /// </summary>
  class GetAdUnit : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets an ad unit by its ID. To determine which ad units " +
            "exist, run GetInventoryTree.cs or GetAllAdUnits.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetAdUnit();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the InventoryService.
      InventoryService inventoryService =
          (InventoryService) user.GetService(DfpService.v201010.InventoryService);

      // Set the ID of the ad unit to get.
      String adUnitId = _T("INSERT_AD_UNIT_ID_HERE");

      try {
        // Get the ad unit.
        AdUnit adUnit = inventoryService.getAdUnit(adUnitId);

        if (adUnit != null) {
          Console.WriteLine("Ad unit with ID = '{0}', name = '{1}' and status = '{2}' was found.",
              adUnit.id, adUnit.name, adUnit.status);
        } else {
          Console.WriteLine("No ad unit found for this ID.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to get ad unit. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
