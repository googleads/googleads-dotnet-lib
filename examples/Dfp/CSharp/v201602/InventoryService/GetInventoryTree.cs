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
using System.Text;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201602 {
  /// <summary>
  /// This code example retrieves a previously created ad units and creates
  /// a tree.
  /// </summary>
  class GetInventoryTree : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example retrieves a previously created ad units and creates a tree.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetInventoryTree();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      try {
        // Get all ad units.
        AdUnit[] allAdUnits = GetAllAdUnits(user);

        // Find the root ad unit. rootAdUnit can also be set to child unit to
        // only build and display a portion of the tree.
        // i.e. AdUnit adUnit =
        //          inventoryService.getAdUnit("INSERT_AD_UNIT_HERE")
        AdUnit rootAdUnit = FindRootAdUnit(user);

        if (rootAdUnit == null) {
          Console.WriteLine("Could not build tree. No root ad unit found.");
        } else {
          BuildAndDisplayAdUnitTree(rootAdUnit, allAdUnits);
        }
      } catch (Exception e) {
        Console.WriteLine("Failed to get ad unit. Exception says \"{0}\"", e.Message);
      }
    }

    /// <summary>
    /// Gets all ad units for this user.
    /// </summary>
    /// <param name="user">The DfpUser to get the ad units for.</param>
    /// <returns>All ad units for this user.</returns>
    private static AdUnit[] GetAllAdUnits(DfpUser user) {
      // Create list to hold all ad units.
      List<AdUnit> adUnits = new List<AdUnit>();

      // Get InventoryService.
      InventoryService inventoryService =
          (InventoryService) user.GetService(DfpService.v201602.InventoryService);

      // Create a statement to get all ad units.
      StatementBuilder statementBuilder = new StatementBuilder()
          .OrderBy("id ASC")
          .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT);

      // Set default for page.
      AdUnitPage page = new AdUnitPage();

      do {
        // Get ad units by statement.
        page = inventoryService.getAdUnitsByStatement(statementBuilder.ToStatement());

        if (page.results != null && page.results.Length > 0) {
          adUnits.AddRange(page.results);
        }
        statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
      } while (statementBuilder.GetOffset() < page.totalResultSetSize);
      return adUnits.ToArray();
    }

    /// <summary>
    /// Finds the root ad unit for the user.
    /// </summary>
    /// <param name="user">The DfpUser to get the root ad unit for.</param>
    /// <returns>The ad unit representing the root ad unit or null if one
    /// is not found.</returns>
    private static AdUnit FindRootAdUnit(DfpUser user) {
      // Get InventoryService.
      InventoryService inventoryService =
          (InventoryService) user.GetService(DfpService.v201602.InventoryService);

      // Create a statement to only select the root ad unit.
      StatementBuilder statementBuilder = new StatementBuilder()
          .Where("parentId IS NULL")
          .OrderBy("id ASC")
          .Limit(1);

      // Get ad units by statement.
      AdUnitPage page = inventoryService.getAdUnitsByStatement(statementBuilder.ToStatement());

      if (page.results != null && page.results.Length > 0) {
        return page.results[0];
      }
      return null;
    }

    /// <summary>
    /// Builds and displays an ad unit tree from an array of ad units underneath
    /// the root ad unit.
    /// </summary>
    /// <param name="root">The root ad unit to build the tree under.</param>
    /// <param name="units">The array of ad units.</param>
    private static void BuildAndDisplayAdUnitTree(AdUnit root, AdUnit[] units) {
      Dictionary<String, List<AdUnit>> treeMap = new Dictionary<String, List<AdUnit>>();

      foreach (AdUnit unit in units) {
        if (unit.parentId != null) {
          if (treeMap.ContainsKey(unit.parentId) == false) {
            treeMap.Add(unit.parentId, new List<AdUnit>());
          }
          treeMap[unit.parentId].Add(unit);
        }
      }

      if (root != null) {
        DisplayInventoryTree(root, treeMap);
      } else {
        Console.WriteLine("No root unit found.");
      }
    }

    /// <summary>
    /// Displays the ad unit tree beginning at the root ad unit.
    /// </summary>
    /// <param name="root">The root ad unit</param>
    /// <param name="treeMap">The map of id to list of ad units</param>
    private static void DisplayInventoryTree(AdUnit root, Dictionary<String,
        List<AdUnit>> treeMap) {
      DisplayInventoryTreeHelper(root, treeMap, 0);
    }

    /// <summary>
    /// Helper for displaying inventory units.
    /// </summary>
    /// <param name="root">The root inventory unit.</param>
    /// <param name="treeMap">The map of id to List of inventory units.</param>
    /// <param name="depth">The depth the tree has reached.</param>
    private static void DisplayInventoryTreeHelper(AdUnit root,
        Dictionary<String, List<AdUnit>> treeMap, int depth) {
      Console.WriteLine(GenerateTab(depth) + root.name + " (" + root.id + ")");

      if (treeMap.ContainsKey(root.id)) {
        foreach (AdUnit child in treeMap[root.id]) {
          DisplayInventoryTreeHelper(child, treeMap, depth + 1);
        }
      }
    }

    /// <summary>
    /// Generates a String of tabs to represent branching to children.
    /// </summary>
    /// <param name="depth">A depth from 0 to max(depth).</param>
    /// <returns>A string to insert in front of the root unit.</returns>
    private static String GenerateTab(int depth) {
      StringBuilder builder = new StringBuilder();
      if (depth != 0) {
        builder.Append("  ");
      }

      for (int i = 1; i < depth; i++) {
        builder.Append("|  ");
      }
      builder.Append("+--");
      return builder.ToString();
    }
  }
}
