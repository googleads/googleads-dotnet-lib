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
using Google.Api.Ads.Dfp.Util.v201602;

using System;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201602 {
  /// <summary>
  /// This code example gets all web target platform ad unit sizes.
  /// </summary>
  class GetAdUnitSizes : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets all web target platform ad unit sizes.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetAdUnitSizes();
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

      StatementBuilder statementBuilder = new StatementBuilder()
          .Where("targetPlatform = :targetPlatform")
          .AddValue("targetPlatform", "WEB");

      try {
        // Get all ad unit sizes.
        AdUnitSize[] adUnitSizes = inventoryService.getAdUnitSizesByStatement(
            statementBuilder.ToStatement());

        // Display results.
        if (adUnitSizes != null) {
          for (int i = 0; i < adUnitSizes.Length; i++) {
            AdUnitSize adUnitSize = adUnitSizes[i];
            Console.WriteLine("{0}) Ad unit size ({1}x{2}) was found.\n", i,
                adUnitSize.size.width, adUnitSize.size.height);
          }
        } else {
          Console.WriteLine("No ad unit sizes found.");
        }
      } catch (Exception e) {
        Console.WriteLine("Failed to get ad unit sizes. Exception says \"{0}\"", e.Message);
      }
    }
  }
}
