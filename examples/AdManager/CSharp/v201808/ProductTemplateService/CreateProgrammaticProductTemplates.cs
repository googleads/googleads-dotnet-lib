// Copyright 2018, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.AdManager.Lib;
using Google.Api.Ads.AdManager.v201808;

using System;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v201808
{
    /// <summary>
    /// This example creates a programmatic product template. Your network must have
    /// sales management enabled to run this example.
    ///
    /// To publish the created product template to Marketplace, you must create a
    /// ProductTemplateBaseRate with a Marketplace rate card.
    /// </summary>
    public class CreateProgrammaticProductTemplates : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This example creates a programmatic product template. Your network must " +
                    "have sales management enabled to run this example. To publish the created " +
                    "product template to Marketplace, you must create a ProductTemplateBaseRate " +
                    "with a Marketplace rate card.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            CreateProgrammaticProductTemplates codeExample =
                new CreateProgrammaticProductTemplates();
            Console.WriteLine(codeExample.Description);
            codeExample.Run(new AdManagerUser());
        }

        /// <summary>
        /// Run the code examples.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (ProductTemplateService productTemplateService =
                user.GetService<ProductTemplateService>())

                using (NetworkService networkService =
                    user.GetService<NetworkService>())
                {
                    // Create a product template.
                    ProductTemplate productTemplate = new ProductTemplate();
                    productTemplate.name = "Programmatic product template #" +
                        new Random().Next(int.MaxValue);
                    productTemplate.description =
                        "This product template creates programmatic proposal line " +
                        "items targeting all ad units with product segmentation on geo targeting.";

                    // Set the name macro which will be used to generate the names of the products.
                    // This will create a segmentation based on the line item type, ad unit, and
                    // location.
                    productTemplate.nameMacro =
                        "<line-item-type> - <ad-unit> - <template-name> - <location>";

                    // Set the product type so the created proposal line items will be trafficked in
                    // DFP.
                    productTemplate.productType = ProductType.DFP;

                    // Set required Marketplace information.
                    productTemplate.productTemplateMarketplaceInfo =
                        new ProductTemplateMarketplaceInfo()
                        {
                            adExchangeEnvironment = AdExchangeEnvironment.DISPLAY,
                        };

                    // Set rate type to create CPM priced proposal line items.
                    productTemplate.rateType = RateType.CPM;

                    // Create the creative placeholder.
                    CreativePlaceholder creativePlaceholder = new CreativePlaceholder();
                    creativePlaceholder.size = new Size()
                    {
                        width = 300,
                        height = 250,
                        isAspectRatio = false
                    };

                    // Set the size of creatives that can be associated with the product template.
                    productTemplate.creativePlaceholders = new CreativePlaceholder[]
                    {
                        creativePlaceholder
                    };

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
                    geoTargeting.targetedLocations = new Location[]
                    {
                        countryLocation,
                        regionLocation
                    };

                    // Add inventory and geo targeting as product segmentation.
                    ProductSegmentation productSegmentation = new ProductSegmentation();
                    productSegmentation.adUnitSegments = new AdUnitTargeting[]
                    {
                        adUnitTargeting
                    };
                    productSegmentation.geoSegment = geoTargeting;

                    productTemplate.productSegmentation = productSegmentation;

                    try
                    {
                        // Create the product template on the server.
                        ProductTemplate[] productTemplates =
                            productTemplateService.createProductTemplates(new ProductTemplate[]
                            {
                                productTemplate
                            });

                        foreach (ProductTemplate createdProductTemplate in productTemplates)
                        {
                            Console.WriteLine(
                                "A programmatic product template with ID \"{0}\" " +
                                "and name \"{1}\" was created.", createdProductTemplate.id,
                                createdProductTemplate.name);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(
                            "Failed to create product templates. Exception says \"{0}\"",
                            e.Message);
                    }
                }
        }
    }
}
