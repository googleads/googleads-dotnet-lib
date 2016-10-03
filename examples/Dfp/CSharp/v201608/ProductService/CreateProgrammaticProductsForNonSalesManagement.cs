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
using Google.Api.Ads.Dfp.Util.v201608;
using Google.Api.Ads.Dfp.v201608;

using System;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201608 {
  /// <summary>
  /// This example creates a programmatic product for those not using sales management.
  /// </summary>
  public class CreateProgrammaticProductsForNonSalesManagement : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This example creates a programmatic product for those not using sales management.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    public static void Main() {
      CreateProgrammaticProductsForNonSalesManagement codeExample =
          new CreateProgrammaticProductsForNonSalesManagement();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code examples.
    /// </summary>
    public void Run(DfpUser user) {
      // Get the ProductService.
      ProductService productTemplateService =
          (ProductService) user.GetService(DfpService.v201608.ProductService);

      // Get the NetworkService.
      NetworkService networkService =
          (NetworkService) user.GetService(DfpService.v201608.NetworkService);

      // Create a product.
      Product product = new Product();
      product.name = "Non-sales programmatic product #" + new Random().Next(int.MaxValue);
      
      // Set required Marketplace information.
      product.productMarketplaceInfo = new ProductMarketplaceInfo() {
        additionalTerms = "Additional terms for the product",
        adExchangeEnvironment = AdExchangeEnvironment.DISPLAY
      };

      // Set common required fields for a programmatic product.
      product.productType = ProductType.DFP;
      product.rateType = RateType.CPM;
      product.lineItemType = LineItemType.STANDARD;
      product.priority = 8;
      product.environmentType = EnvironmentType.BROWSER;
      product.rate = new Money() {currencyCode = "USD", microAmount = 6000000L};
      
      CreativePlaceholder placeholder = new CreativePlaceholder();
      placeholder.size = new Size() {width = 300, height = 250, isAspectRatio = false};
      product.creativePlaceholders = new CreativePlaceholder[] { placeholder };

      // Create inventory targeting to serve to run of network..
      String rootAdUnitId = networkService.getCurrentNetwork().effectiveRootAdUnitId;
      AdUnitTargeting adUnitTargeting = new AdUnitTargeting();
      adUnitTargeting.adUnitId = rootAdUnitId;
      adUnitTargeting.includeDescendants = true;

      Targeting productTargeting = new Targeting();
      productTargeting.inventoryTargeting = new InventoryTargeting() {
        targetedAdUnits = new AdUnitTargeting[] { adUnitTargeting }
      };

      product.builtInTargeting = productTargeting;
  
      try {
        // Create the product on the server.
        Product[] products = productTemplateService.createProducts(
            new Product[] {product});

        foreach (Product createdProduct in products) {
          Console.WriteLine("A programmatic product with ID \"{0}\" and name \"{1}\" was created.",
              createdProduct.id, createdProduct.name);
        }

      } catch (Exception e) {
        Console.WriteLine("Failed to create products. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}
