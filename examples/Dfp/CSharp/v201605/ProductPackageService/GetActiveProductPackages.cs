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
using Google.Api.Ads.Dfp.Util.v201605;
using Google.Api.Ads.Dfp.v201605;

using System;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201605 {
  /// <summary>
  /// This example gets all active product packages.
  /// </summary>
  public class GetActiveProductPackages : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This example gets all active product packages.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main() {
      GetActiveProductPackages codeExample = new GetActiveProductPackages();
      Console.WriteLine(codeExample.Description);

      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public void Run(DfpUser user) {
      ProductPackageService productPackageService =
          (ProductPackageService) user.GetService(DfpService.v201605.ProductPackageService);

      // Create a statement to select product packages.
      StatementBuilder statementBuilder = new StatementBuilder()
          .Where("status = :status")
          .OrderBy("id ASC")
          .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT)
          .AddValue("status", ProductPackageStatus.ACTIVE.ToString());

      // Retrieve a small amount of product packages at a time, paging through
      // until all product packages have been retrieved.
      ProductPackagePage page = new ProductPackagePage();
      try {
        do {
          page = productPackageService.getProductPackagesByStatement(
              statementBuilder.ToStatement());

          if (page.results != null) {
            // Print out some information for each product package.
            int i = page.startIndex;
            foreach (ProductPackage productPackage in page.results) {
              Console.WriteLine("{0}) Product package with ID \"{1}\" and name \"{2}\" was found.",
                  i++,
                  productPackage.id,
                  productPackage.name);
            }
          }

          statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
        } while (statementBuilder.GetOffset() < page.totalResultSetSize);

        Console.WriteLine("Number of results found: {0}", page.totalResultSetSize);
      } catch (Exception e) {
        Console.WriteLine("Failed to get product packages. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}
