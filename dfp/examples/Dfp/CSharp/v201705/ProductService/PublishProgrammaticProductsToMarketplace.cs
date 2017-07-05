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
using Google.Api.Ads.Dfp.Util.v201705;
using Google.Api.Ads.Dfp.v201705;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201705 {
  /// <summary>
  /// This example publishes a programmatic product to Marketplace.
  /// </summary>
  public class PublishProgrammaticProductsToMarketplace : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This example publishes a programmatic product to Marketplace.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    public static void Main() {
      PublishProgrammaticProductsToMarketplace codeExample = 
          new PublishProgrammaticProductsToMarketplace();
      Console.WriteLine(codeExample.Description);

      long productId = long.Parse(_T("INSERT_PRODUCT_ID_HERE"));
      codeExample.Run(new DfpUser(), productId);
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    public void Run(DfpUser user, long productId) {
      // Get the ProductService.
      ProductService productService =
          (ProductService) user.GetService(DfpService.v201705.ProductService);

      // Create statement to select a product template by ID.
      StatementBuilder statementBuilder = new StatementBuilder()
          .Where("id = :id")
          .OrderBy("id ASC")
          .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT)
          .AddValue("id", productId);

      // Set default for page.
      ProductPage page = new ProductPage();
      List<long> productIds = new List<long>();

      try {
        do {
          // Get products by statement.
          page = productService.getProductsByStatement(
              statementBuilder.ToStatement());

          if (page.results != null) {
            int i = page.startIndex;
            foreach (Product product in page.results) {
              Console.WriteLine("{0}) Product with ID ='{1}' will be published.",
                  i++, product.id);
              productIds.Add(product.id);
            }
          }

          statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
        } while (statementBuilder.GetOffset() < page.totalResultSetSize);

        Console.WriteLine("Number of products to be published: {0}", productIds.Count);

        if (productIds.Count > 0) {
          // Modify statement.
          statementBuilder.RemoveLimitAndOffset();

          // Create action.
          Google.Api.Ads.Dfp.v201705.PublishProducts action =
              new Google.Api.Ads.Dfp.v201705.PublishProducts();

          // Perform action.
          UpdateResult result = productService.performProductAction(action,
              statementBuilder.ToStatement());

          // Display results.
          if (result != null && result.numChanges > 0) {
            Console.WriteLine("Number of products published: {0}", result.numChanges);
          } else {
            Console.WriteLine("No products were published.");
          }
        }
      } catch (Exception e) {
        Console.WriteLine("Failed to publish products. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}
