// Copyright 2016, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.Dfp.Util.v201702;
using Google.Api.Ads.Dfp.v201702;
using System;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201702 {
  /// <summary>
  /// This example gets all ad unit sizes.
  /// </summary>
  public class GetAllAdUnitSizes : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This example gets all ad unit sizes.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    public static void Main() {
      GetAllAdUnitSizes codeExample = new GetAllAdUnitSizes();
      Console.WriteLine(codeExample.Description);
      try {
        codeExample.Run(new DfpUser());
      } catch (Exception e) {
        Console.WriteLine("Failed to get ad unit sizes. Exception says \"{0}\"",
            e.Message);
      }
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    public void Run(DfpUser dfpUser) {
      using (InventoryService inventoryService =
          (InventoryService) dfpUser.GetService(DfpService.v201702.InventoryService)) {

        // Create a statement to select ad unit sizes.
        StatementBuilder statementBuilder = new StatementBuilder();

        AdUnitSize[] adUnitSizes = inventoryService.getAdUnitSizesByStatement(
            statementBuilder.ToStatement());

        // Print out some information for each ad unit size.
        int i = 0;
        foreach (AdUnitSize adUnitSize in adUnitSizes) {
          Console.WriteLine(
              "{0}) Ad unit size with dimensions \"{1}\" was found.",
              i++,
              adUnitSize.fullDisplayString
          );
        }

        Console.WriteLine("Number of results found: {0}", adUnitSizes.Length);
      }
    }
  }
}
