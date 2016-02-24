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
  /// This code example creates all proposal line items within an IN_PROGRESS package. To determine
  /// which packages exist, run GetAllPackages.cs.
  /// </summary>
  class CreateProposalLineItemsFromPackage : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return " This code example creates all proposal line items within an IN_PROGRESS " +
            "package. To determine which packages exist, run GetAllPackages.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new CreateProposalLineItemsFromPackage();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the PackageService.
      PackageService packageService =
          (PackageService) user.GetService(DfpService.v201602.PackageService);

      // Set the ID of the package to create line items from.
      long packageId = long.Parse(_T("INSERT_PACKAGE_ID_HERE"));

      // Create statement to select the package.
      StatementBuilder statementBuilder = new StatementBuilder()
          .Where("id = :id")
          .OrderBy("id ASC")
          .Limit(1)
          .AddValue("id", packageId);

      // Set default for page.
      PackagePage page = new PackagePage();
      List<string> packageIds = new List<string>();
      int i = 0;

      try {
        // Get the package.
        page = packageService.getPackagesByStatement(statementBuilder.ToStatement());
        Package package = page.results[0];

        Console.WriteLine("Package with ID \"{0}\" will create proposal line items using "
            + "product package with ID \"{1}\"", package.id, package.productPackageId);

        // Modify statement for action.
        statementBuilder.RemoveLimitAndOffset();

        // Create action.
        CreateProposalLineItemsFromPackages action = new CreateProposalLineItemsFromPackages();

        // Perform action.
        UpdateResult result = packageService.performPackageAction(action,
            statementBuilder.ToStatement());

        // Display results.
        if (result != null && result.numChanges > 0) {
          Console.WriteLine("Proposal line items were created for {0} packages.",
              result.numChanges);
        } else {
          Console.WriteLine("No proposal line items were created.");
        }

      } catch (Exception e) {
        Console.WriteLine("Failed to create proposal line items. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}
