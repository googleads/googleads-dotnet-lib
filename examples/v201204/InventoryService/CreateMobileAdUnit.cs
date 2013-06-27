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

using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.v201204;

using System;

namespace Google.Api.Ads.Dfp.Examples.v201204 {
  /// <summary>
  /// This code example creates a new mobile ad unit under the effective root
  /// ad unit. Mobile features need to be enabled on your account to use mobile
  /// targeting. To determine which ad units exist, run GetInventoryTree.cs or
  /// GetAllAdUnits.cs.
  ///
  /// Tags: InventoryService.createAdUnits, NetworkService.getCurrentNetwork
  /// </summary>
  class CreateMobileAdUnit : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example creates a new mobile ad unit under the effective  ad unit. " +
            "Mobile features need to be enabled on your account to use mobile targeting. To " +
            "determine which ad units exist, run GetInventoryTree.cs or GetAllAdUnits.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new CreateMobileAdUnit();
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
          (InventoryService) user.GetService(DfpService.v201204.InventoryService);

      // Get the NetworkService.
      NetworkService networkService =
          (NetworkService) user.GetService(DfpService.v201204.NetworkService);

      // Set the parent ad unit's ID for all ad units to be created under.
      String effectiveRootAdUnitId = networkService.getCurrentNetwork().effectiveRootAdUnitId;

      // Create local ad unit object.
      AdUnit adUnit = new AdUnit();
      adUnit.name = "Mobile_Ad_Unit";
      adUnit.parentId = effectiveRootAdUnitId;
      adUnit.description = "Ad unit description.";
      adUnit.targetWindow = AdUnitTargetWindow.BLANK;
      adUnit.targetPlatform = TargetPlatform.MOBILE;

      // Create ad unit size.
      AdUnitSize adUnitSize = new AdUnitSize();
      Size size = new Size();
      size.width = 400;
      size.height = 300;
      size.isAspectRatio = false;
      adUnitSize.size = size;
      adUnitSize.environmentType = EnvironmentType.BROWSER;

      // Set the size of possible creatives that can match this ad unit.
      adUnit.adUnitSizes = new AdUnitSize[] {adUnitSize};

      try {
        // Create the ad unit on the server.
        adUnit = inventoryService.createAdUnit(adUnit);

        if (adUnit != null) {
          Console.WriteLine("An ad unit with ID \"{0}\" was created under parent with ID \"{1}\".",
              adUnit.id, adUnit.parentId);
        } else {
          Console.WriteLine("No ad units created.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to create ad units. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
