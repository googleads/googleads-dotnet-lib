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

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201602 {
  /// <summary>
  /// This code example updates an ad unit by enabling AdSense to the first
  /// 500. To determine which ad units exist, run GetAllAdUnits.cs or
  /// GetInventoryTree.cs.
  /// </summary>
  class UpdateAdUnits : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example updates an ad unit by enabling AdSense. To determine which " +
            "ad units exist, run GetAllAdUnits.cs or GetInventoryTree.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new UpdateAdUnits();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the sample code.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the InventoryService.
      InventoryService inventoryService =
          (InventoryService) user.GetService(DfpService.v201602.InventoryService);

      // Set the ID of the ad unit to update.
      int adUnitId = int.Parse(_T("INSERT_AD_UNIT_ID_HERE"));

      // Create a statement to get the ad unit.
      StatementBuilder statementBuilder = new StatementBuilder()
          .Where("id = :id")
          .OrderBy("id ASC")
          .Limit(1)
          .AddValue("id", adUnitId);

      try {
        // Get ad units by statement.
        AdUnitPage page = inventoryService.getAdUnitsByStatement(statementBuilder.ToStatement());

        AdUnit adUnit = page.results[0];
        adUnit.inheritedAdSenseSettings.value.adSenseEnabled = true;

        // Update the ad units on the server.
        AdUnit[] updatedAdUnits = inventoryService.updateAdUnits(new AdUnit[] {adUnit});

        foreach (AdUnit updatedAdUnit in updatedAdUnits) {
          Console.WriteLine("Ad unit with ID \"{0}\", name \"{1}\", and is AdSense enabled " +
              "\"{2}\" was updated.", updatedAdUnit.id, updatedAdUnit.name,
              updatedAdUnit.inheritedAdSenseSettings.value.adSenseEnabled);
        }
      } catch (Exception e) {
        Console.WriteLine("Failed to update ad units. Exception says \"{0}\"", e.Message);
      }
    }
  }
}
