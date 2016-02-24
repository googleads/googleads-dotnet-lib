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
  /// This code example creates a product template. To see which product templates exist,
  /// run GetAllProductTemplates.cs.
  /// </summary>
  class CreateProductTemplates : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example creates a product template. To see which product templates " +
            "exist, run GetAllProductTemplates.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new CreateProductTemplates();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code examples.
    /// </summary>
    /// <param name="user">The DFP user object running the code examples.</param>
    public override void Run(DfpUser user) {
      // Get the ProductTemplateService.
      ProductTemplateService productTemplateService =
          (ProductTemplateService) user.GetService(DfpService.v201602.ProductTemplateService);

      // Get the NetworkService.
      NetworkService networkService =
          (NetworkService) user.GetService(DfpService.v201602.NetworkService);

      // Create a product template.
      ProductTemplate productTemplate = new ProductTemplate();
      productTemplate.name = "Product template #" + new Random().Next(int.MaxValue);
      productTemplate.description = "This product template creates standard proposal line items "
          + "targeting Chrome browsers with product segmentation on ad units and geo targeting.";

      // Set the name macro which will be used to generate the names of the products.
      // This will create a segmentation based on the line item type, ad unit, and location.
      productTemplate.nameMacro = "<line-item-type> - <ad-unit> - <template-name> - <location>";

      // Set the product type so the created proposal line items will be trafficked in DFP.
      productTemplate.productType = ProductType.DFP;

      // Set rate type to create CPM priced proposal line items.
      productTemplate.rateType = RateType.CPM;

      // Optionally set the creative rotation of the product to serve one or more creatives.
      productTemplate.roadblockingType = RoadblockingType.ONE_OR_MORE;
      productTemplate.deliveryRateType = DeliveryRateType.AS_FAST_AS_POSSIBLE;

      // Create the master creative placeholder.
      CreativePlaceholder creativeMasterPlaceholder = new CreativePlaceholder();
      creativeMasterPlaceholder.size =
          new Size() {width = 728, height = 90, isAspectRatio = false};

      // Create companion creative placeholders.
      CreativePlaceholder companionCreativePlaceholder = new CreativePlaceholder();
      companionCreativePlaceholder.size =
          new Size() {width = 300, height = 250, isAspectRatio = false};

      // Set the size of creatives that can be associated with the product template.
      productTemplate.creativePlaceholders =
          new CreativePlaceholder[] {creativeMasterPlaceholder, companionCreativePlaceholder};

      // Set the type of proposal line item to be created from the product template.
      productTemplate.lineItemType = LineItemType.STANDARD;

      // Get the root ad unit ID used to target the whole site.
      String rootAdUnitId = networkService.getCurrentNetwork().effectiveRootAdUnitId;

      // Create ad unit targeting for the root ad unit (i.e. the whole network).
      AdUnitTargeting adUnitTargeting = new AdUnitTargeting();
      adUnitTargeting.adUnitId = rootAdUnitId;
      adUnitTargeting.includeDescendants = true;

      // Create geo targeting for the US.
      Location countryLocation = new Location();
      countryLocation.id = 2840L;

      // Create geo targeting for Hong Kong.
      Location regionLocation = new Location();
      regionLocation.id = 2344L;

      GeoTargeting geoTargeting = new GeoTargeting();
      geoTargeting.targetedLocations = new Location[] {countryLocation, regionLocation};

      // Add browser targeting to Chrome on the product template distinct from product
      // segmentation.
      Browser chromeBrowser = new Browser();
      chromeBrowser.id = 500072L;

      BrowserTargeting browserTargeting = new BrowserTargeting();
      browserTargeting.browsers = new Browser[] {chromeBrowser};

      TechnologyTargeting technologyTargeting = new TechnologyTargeting();
      technologyTargeting.browserTargeting = browserTargeting;

      Targeting productTemplateTargeting = new Targeting();
      productTemplateTargeting.technologyTargeting = technologyTargeting;

      productTemplate.builtInTargeting = productTemplateTargeting;

      productTemplate.customizableAttributes = new CustomizableAttributes() {
        allowPlacementTargetingCustomization = true
      };

      // Add inventory and geo targeting as product segmentation.
      ProductSegmentation productSegmentation = new ProductSegmentation();
      productSegmentation.adUnitSegments = new AdUnitTargeting[] {adUnitTargeting};
      productSegmentation.geoSegment = geoTargeting;

      productTemplate.productSegmentation = productSegmentation;

      try {
        // Create the product template on the server.
        ProductTemplate[] productTemplates = productTemplateService.createProductTemplates(
            new ProductTemplate[] {productTemplate});

        foreach (ProductTemplate createdProductTemplate in productTemplates) {
          Console.WriteLine("A product template with ID \"{0}\" and name \"{1}\" was created.",
              createdProductTemplate.id, createdProductTemplate.name);
        }

      } catch (Exception e) {
        Console.WriteLine("Failed to create product templates. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}
