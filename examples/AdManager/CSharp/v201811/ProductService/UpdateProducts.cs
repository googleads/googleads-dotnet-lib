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
using Google.Api.Ads.AdManager.Util.v201811;
using Google.Api.Ads.AdManager.v201811;

using System;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v201811
{
    /// <summary>
    /// This code example updates the note of a product. To determine which products exist,
    /// run GetAllProducts.cs.
    /// </summary>
    public class UpdateProducts : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example updates the note of an product. To determine which " +
                    "products exist, run GetAllProducts.cs.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            UpdateProducts codeExample = new UpdateProducts();
            Console.WriteLine(codeExample.Description);
            codeExample.Run(new AdManagerUser());
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (ProductService productService = user.GetService<ProductService>())
            {
                long productId = long.Parse(_T("INSERT_PRODUCT_ID_HERE"));

                // Create a statement to get the product.
                StatementBuilder statementBuilder = new StatementBuilder()
                    .Where("id = :id")
                    .OrderBy("id ASC")
                    .Limit(1)
                    .AddValue("id", productId);

                try
                {
                    // Get products by statement.
                    ProductPage page =
                        productService.getProductsByStatement(statementBuilder.ToStatement());

                    Product product = page.results[0];

                    // Update the product object by changing its note.
                    product.notes = "Product needs further review before approval.";

                    // Update the products on the server.
                    Product[] products = productService.updateProducts(new Product[]
                    {
                        product
                    });

                    if (products != null)
                    {
                        foreach (Product updatedProduct in products)
                        {
                            Console.WriteLine(
                                "Product with ID = '{0}', name = '{1}', and notes = '{2}' was " +
                                "updated.", updatedProduct.id, updatedProduct.name,
                                updatedProduct.notes);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No products updated.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to update products. Exception says \"{0}\"",
                        e.Message);
                }
            }
        }
    }
}
