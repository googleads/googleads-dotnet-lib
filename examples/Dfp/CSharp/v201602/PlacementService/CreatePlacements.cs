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
using Google.Api.Ads.Dfp.Util.v201602;
using Google.Api.Ads.Dfp.v201602;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201602 {
  /// <summary>
  /// This code example creates new placements for various ad unit sizes. To
  /// determine which placements exist, run GetAllPlacements.cs.
  /// </summary>
  class CreatePlacements : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example creates new placements for various ad unit sizes. To determine " +
            "which placements exist, run GetAllPlacements.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new CreatePlacements();
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

      // Get the PlacementService.
      PlacementService placementService =
          (PlacementService) user.GetService(DfpService.v201602.PlacementService);

      // Create local placement object to store skyscraper ad units.
      Placement skyscraperAdUnitPlacement = new Placement();
      skyscraperAdUnitPlacement.name = string.Format("Skyscraper AdUnit Placement #{0}",
          this.GetTimeStamp());
      skyscraperAdUnitPlacement.description = "Contains ad units that can hold creatives " +
          "of size 120x600";

      // Create local placement object to store medium square ad units.
      Placement mediumSquareAdUnitPlacement = new Placement();
      mediumSquareAdUnitPlacement.name = string.Format("Medium Square AdUnit Placement #{0}",
          this.GetTimeStamp());
      mediumSquareAdUnitPlacement.description = "Contains ad units that can hold creatives " +
          "of size 300x250";

      // Create local placement object to store banner ad units.
      Placement bannerAdUnitPlacement = new Placement();
      bannerAdUnitPlacement.name = string.Format("Banner AdUnit Placement #{0}",
          this.GetTimeStamp());
      bannerAdUnitPlacement.description = "Contains ad units that can hold creatives " +
          "of size 468x60";

      List<Placement> placementList = new List<Placement>();

      // Get the first 500 ad units.
      StatementBuilder statementBuilder = new StatementBuilder()
          .OrderBy("id ASC")
          .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT);

      List<string> mediumSquareTargetedUnitIds = new List<string>();
      List<string> skyscraperTargetedUnitIds = new List<string>();
      List<string> bannerTargetedUnitIds = new List<string>();

      try {
        AdUnitPage page = inventoryService.getAdUnitsByStatement(statementBuilder.ToStatement());

        // Separate the ad units by size.
        if (page.results != null) {
          foreach (AdUnit adUnit in page.results) {
            if (adUnit.parentId != null && adUnit.adUnitSizes != null) {
              foreach (AdUnitSize adUnitSize in adUnit.adUnitSizes) {
                Size size = adUnitSize.size;
                if (size.width == 300 && size.height == 250) {
                  if (!mediumSquareTargetedUnitIds.Contains(adUnit.id)) {
                    mediumSquareTargetedUnitIds.Add(adUnit.id);
                  }
                } else if (size.width == 120 && size.height == 600) {
                  if (!skyscraperTargetedUnitIds.Contains(adUnit.id)) {
                    skyscraperTargetedUnitIds.Add(adUnit.id);
                  }
                } else if (size.width == 468 && size.height == 60) {
                  if (!bannerTargetedUnitIds.Contains(adUnit.id)) {
                    bannerTargetedUnitIds.Add(adUnit.id);
                  }
                }
              }
            }
          }
        }
        mediumSquareAdUnitPlacement.targetedAdUnitIds = mediumSquareTargetedUnitIds.ToArray();
        skyscraperAdUnitPlacement.targetedAdUnitIds = skyscraperTargetedUnitIds.ToArray();
        bannerAdUnitPlacement.targetedAdUnitIds = bannerTargetedUnitIds.ToArray();


        // Only create placements with one or more ad unit.
        if (mediumSquareAdUnitPlacement.targetedAdUnitIds.Length != 0) {
          placementList.Add(mediumSquareAdUnitPlacement);
        }

        if (skyscraperAdUnitPlacement.targetedAdUnitIds.Length != 0) {
          placementList.Add(skyscraperAdUnitPlacement);
        }

        if (bannerAdUnitPlacement.targetedAdUnitIds.Length != 0) {
          placementList.Add(bannerAdUnitPlacement);
        }

        Placement[] placements =
          placementService.createPlacements(placementList.ToArray());

        // Display results.
        if (placements != null) {
          foreach (Placement placement in placements) {
            Console.Write("A placement with ID = '{0}', name ='{1}', and containing " +
                "ad units {{", placement.id, placement.name);

            foreach (string adUnitId in placement.targetedAdUnitIds) {
              Console.Write("{0}, ", adUnitId);
            }
            Console.WriteLine("} was created.");
          }
        } else {
          Console.WriteLine("No placements created.");
        }

      } catch (Exception e) {
        Console.WriteLine("Failed to create placements. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}
