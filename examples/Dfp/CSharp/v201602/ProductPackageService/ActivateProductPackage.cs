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
  /// This code example activates a product package. To determine which product packages exist,
  /// run GetAllProductPackages.cs.
  ///       ProductPackageService.performProductPackageAction
  /// </summary>
  class ActivateProductPackage : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example activates a product package. To determine which product " +
            "packages exist, run GetAllProductPackages.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new ActivateProductPackage();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the ProductPackageService.
      ProductPackageService productPackageService =
          (ProductPackageService) user.GetService(DfpService.v201602.ProductPackageService);

      // Set the ID of the product package.
      long productPackageId = long.Parse(_T("INSERT_PRODUCT_PACKAGE_ID_HERE"));

      // Create statement to select the product package.
      StatementBuilder statementBuilder = new StatementBuilder()
          .Where("id = :id")
          .OrderBy("id ASC")
          .Limit(1)
          .AddValue("id", productPackageId);

      // Set default for page.
      ProductPackagePage page = new ProductPackagePage();
      List<string> productPackageIds = new List<string>();
      int i = 0;

      try {
        do {
          // Get product packages by statement.
          page =
              productPackageService.getProductPackagesByStatement(statementBuilder.ToStatement());

          if (page.results != null && page.results.Length > 0) {
            foreach (ProductPackage productPackage in page.results) {
              Console.WriteLine("{0}) Product package with ID = '{1}', name = '{2}', and status " +
                  "='{3}' will be activated.", i++, productPackage.id, productPackage.name,
                  productPackage.status);
              productPackageIds.Add(productPackage.id.ToString());
            }
          }

          statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
        } while (statementBuilder.GetOffset() < page.totalResultSetSize);

        Console.WriteLine("Number of product packages to be activated: {0}",
            productPackageIds.Count);

        if (productPackageIds.Count > 0) {
          // Modify statement for action.
          statementBuilder.RemoveLimitAndOffset();

          // Create action.
          ActivateProductPackages action = new ActivateProductPackages();

          // Perform action.
          UpdateResult result = productPackageService.performProductPackageAction(action,
              statementBuilder.ToStatement());

          // Display results.
          if (result != null && result.numChanges > 0) {
            Console.WriteLine("Number of product packages activated: {0}", result.numChanges);
          } else {
            Console.WriteLine("No product packages were activated.");
          }
        }
      } catch (Exception e) {
        Console.WriteLine("Failed to activate product packages. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}
