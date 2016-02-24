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
  /// This code example updates the notes of a product package. To determine which product packages
  /// exist, run GetAllProductPackages.cs.
  ///       ProductPackageService.updateProductPackages
  /// </summary>
  class UpdateProductPackages : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example updates the notes of a product package. To determine which " +
            "product packages exist, run GetAllProductPackages.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new UpdateProductPackages();
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

      long productPackageId = long.Parse(_T("INSERT_PRODUCT_PACKAGE_ID_HERE"));

      // Create a statement to get the product package.
      StatementBuilder statementBuilder = new StatementBuilder()
          .Where("id = :id")
          .OrderBy("id ASC")
          .Limit(1)
          .AddValue("id", productPackageId);

      try {
        // Get the product package.
        ProductPackagePage page =
            productPackageService.getProductPackagesByStatement(statementBuilder.ToStatement());

        ProductPackage productPackage = page.results[0];

        // Update the product package object by changing its notes.
        productPackage.notes = "This product package is not to be sold before the end of the "
            + "month.";

        // Update the product packages on the server.
        ProductPackage[] productPackages =
            productPackageService.updateProductPackages(new ProductPackage[] {productPackage});

        if (productPackages != null) {
          foreach (ProductPackage updatedProductPackage in productPackages) {
            Console.WriteLine("Product package with ID = \"{0}\", name = \"{1}\", and " +
                "notes = \"{2}\" was updated.", updatedProductPackage.id,
                updatedProductPackage.name, updatedProductPackage.notes);
          }
        } else {
          Console.WriteLine("No product packages updated.");
        }
      } catch (Exception e) {
        Console.WriteLine("Failed to update product packages. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}
