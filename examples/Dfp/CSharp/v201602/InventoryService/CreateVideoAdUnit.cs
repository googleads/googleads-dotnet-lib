// Copyright 2015, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.v201602;

using System;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201602 {
  /// <summary>
  /// This code example creates a new video ad unit under the effective root
  /// ad unit. To determine which ad units exist, run GetInventoryTree.cs or
  /// GetAllAdUnits.cs.
  /// </summary>
  class CreateVideoAdUnit : SampleBase {
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
      SampleBase codeExample = new CreateVideoAdUnit();
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
          (InventoryService) user.GetService(DfpService.v201602.InventoryService);

      // Get the NetworkService.
      NetworkService networkService =
          (NetworkService) user.GetService(DfpService.v201602.NetworkService);

      // Set the parent ad unit's ID for all ad units to be created under.
      String effectiveRootAdUnitId = networkService.getCurrentNetwork().effectiveRootAdUnitId;

      // Create local ad unit object.
      AdUnit adUnit = new AdUnit();
      adUnit.name = "Video_Ad_Unit";
      adUnit.parentId = effectiveRootAdUnitId;
      adUnit.description = "Ad unit description.";
      adUnit.targetWindow = AdUnitTargetWindow.BLANK;
      adUnit.explicitlyTargeted = true;

      // Create master ad unit size.
      AdUnitSize masterAdUnitSize = new AdUnitSize();
      Size size1 = new Size();
      size1.width = 400;
      size1.height = 300;
      size1.isAspectRatio = false;
      masterAdUnitSize.size = size1;
      masterAdUnitSize.environmentType = EnvironmentType.VIDEO_PLAYER;

      // Create companion sizes.
      AdUnitSize companionAdUnitSize1 = new AdUnitSize();
      Size size2 = new Size();
      size2.width = 300;
      size2.height = 250;
      size2.isAspectRatio = false;
      companionAdUnitSize1.size = size2;
      companionAdUnitSize1.environmentType = EnvironmentType.BROWSER;

      AdUnitSize companionAdUnitSize2 = new AdUnitSize();
      Size size3 = new Size();
      size3.width = 728;
      size3.height = 90;
      size3.isAspectRatio = false;
      companionAdUnitSize2.size = size3;
      companionAdUnitSize2.environmentType = EnvironmentType.BROWSER;

      // Add companions to master ad unit size.
      masterAdUnitSize.companions = new AdUnitSize[] {companionAdUnitSize1, companionAdUnitSize2};

      // Set the size of possible creatives that can match this ad unit.
      adUnit.adUnitSizes = new AdUnitSize[] {masterAdUnitSize};

      try {
        // Create the ad unit on the server.
        AdUnit[] createdAdUnits = inventoryService.createAdUnits(new AdUnit[] {adUnit});

        foreach (AdUnit createdAdUnit in createdAdUnits) {
          Console.WriteLine("A video ad unit with ID \"{0}\" was created under parent with ID " +
              "\"{1}\".", createdAdUnit.id, createdAdUnit.parentId);
        }
      } catch (Exception e) {
        Console.WriteLine("Failed to create video ad units. Exception says \"{0}\"", e.Message);
      }
    }
  }
}
