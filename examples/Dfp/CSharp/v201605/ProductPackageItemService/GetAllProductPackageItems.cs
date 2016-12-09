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
  /// This example gets all product package items.
  /// </summary>
  public class GetAllProductPackageItems : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This example gets all product package items.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    public static void Main() {
      GetAllProductPackageItems codeExample = new GetAllProductPackageItems();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    public void Run(DfpUser user) {
      ProductPackageItemService productPackageItemService =
          (ProductPackageItemService) user.GetService(DfpService.v201605.ProductPackageItemService);

      // Create a statement to select product package items.
      StatementBuilder statementBuilder = new StatementBuilder()
          .OrderBy("id ASC")
          .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT);

      // Retrieve a small amount of product package items at a time, paging through
      // until all product package items have been retrieved.
      ProductPackageItemPage page = new ProductPackageItemPage();
      try {
        do {
          page = productPackageItemService.getProductPackageItemsByStatement(
              statementBuilder.ToStatement());

          if (page.results != null) {
            // Print out some information for each product package item.
            int i = page.startIndex;
            foreach (ProductPackageItem productPackageItem in page.results) {
              Console.WriteLine("{0}) Product package item with ID \"{1}\", product id \"{2}\" , "
                  + "and product package id \"{3}\" was found.",
                  i++,
                  productPackageItem.id,
                  productPackageItem.productId,
                  productPackageItem.productPackageId);
            }
          }

          statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
        } while (statementBuilder.GetOffset() < page.totalResultSetSize);

        Console.WriteLine("Number of results found: {0}", page.totalResultSetSize);
      } catch (Exception e) {
        Console.WriteLine("Failed to get product package items. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}
