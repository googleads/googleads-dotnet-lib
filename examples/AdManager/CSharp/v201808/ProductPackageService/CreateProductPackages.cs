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
    /// This code example creates a product package. To determine which product packages exist,
    /// run GetAllProductPackages.cs.
    /// </summary>
    public class CreateProductPackages : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example creates a product package. To determine which product " +
                    "packges exist, run GetAllProductPackages.cs.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            CreateProductPackages codeExample = new CreateProductPackages();
            Console.WriteLine(codeExample.Description);
            codeExample.Run(new AdManagerUser());
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (ProductPackageService productPackageService =
                user.GetService<ProductPackageService>())
            {
                // Set the ID of the rate card to associate the product package with.
                long rateCardId = long.Parse(_T("INSERT_RATE_CARD_ID_HERE"));

                // Create a product package.
                ProductPackage productPackage = new ProductPackage();
                productPackage.name = "Product package #" + new Random().Next(int.MaxValue);
                productPackage.rateCardIds = new long[]
                {
                    rateCardId
                };

                try
                {
                    // Create the product packages on the server.
                    ProductPackage[] packages = productPackageService.createProductPackages(
                        new ProductPackage[]
                        {
                            productPackage
                        });

                    foreach (ProductPackage createdProductPackage in packages)
                    {
                        Console.WriteLine(
                            "A product package with ID \"{0}\" and name \"{1}\" was created.",
                            createdProductPackage.id, createdProductPackage.name);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to create product packages. Exception says \"{0}\"",
                        e.Message);
                }
            }
        }
    }
}
