// Copyright 2010, Google Inc. All Rights Reserved.
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
  /// This code example creates new ad units under the effective root ad unit.
  /// To determine which ad units exist, run GetAdUnitTree.cs or
  /// GetAllAdUnits.cs.
  /// </summary>
  class CreateAdUnits : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example creates new ad units under the effective root ad unit. To " +
            "determine which ad units exist, run GetAdUnitTree.cs or GetAllAdUnits.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new CreateAdUnits();
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

      // Get the NetworkService.
      NetworkService networkService =
          (NetworkService) user.GetService(DfpService.v201010.NetworkService);

      string effectiveRootAdUnitId = networkService.getCurrentNetwork().effectiveRootAdUnitId;

      // Create an array to store local ad unit objects.
      AdUnit[] adUnits = new AdUnit[5];

      for (int i = 0; i < 5; i++) {
        AdUnit adUnit = new AdUnit();
        adUnit.name = string.Format("Ad_Unit_{0}", i);
        adUnit.parentId = effectiveRootAdUnitId;

        adUnit.description = "Ad unit description.";
        adUnit.targetWindow = AdUnitTargetWindow.BLANK;

        // Set the size of possible creatives that can match this ad unit.
        Size size = new Size();
        size.width = 300;
        size.height = 250;

        adUnit.sizes = new Size[] {size};
        adUnits[i] = adUnit;
      }

      try {
        // Create the ad units on the server.
        adUnits = inventoryService.createAdUnits(adUnits);

        if (adUnits != null) {
          foreach (AdUnit adUnit in adUnits) {
            Console.WriteLine("An ad unit with ID = '{0}' was created under parent with " +
                "ID = '{1}'.", adUnit.id, adUnit.parentId);
          }
        } else {
          Console.WriteLine("No ad units created.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to create ad units. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
