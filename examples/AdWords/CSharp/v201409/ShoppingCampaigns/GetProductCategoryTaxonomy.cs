// Copyright 2014, Google Inc. All Rights Reserved.
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

// Author: api.anash@gmail.com (Anash P. Oommen)

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201409;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201409 {

  /// <summary>
  /// This code example fetches the set of valid ProductBiddingCategories.
  ///
  /// Tags: ConstantDataService.getProductBiddingCategoryData
  /// </summary>
  public class GetProductCategoryTaxonomy : ExampleBase {

    /// <summary>
    /// Stores details about a product category and its hierarchy.
    /// </summary>
    class ProductCategory {
      /// <summary>
      /// The product category id.
      /// </summary>
      long id;

      /// <summary>
      /// The product category name.
      /// </summary>
      string name;

      /// <summary>
      /// The product category children.
      /// </summary>
      List<ProductCategory> children;

      /// <summary>
      /// Gets or sets the product category id.
      /// </summary>
      public long Id {
        get {
          return id;
        }
        set {
          id = value;
        }
      }

      /// <summary>
      /// Gets or sets the product category name.
      /// </summary>
      public string Name {
        get {
          return name;
        }
        set {
          name = value;
        }
      }

      /// <summary>
      /// Gets or sets the product category children.
      /// </summary>
      public List<ProductCategory> Children {
        get {
          return children;
        }
      }
    }

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example fetches the set of valid ProductBiddingCategories.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      GetProductCategoryTaxonomy codeExample = new GetProductCategoryTaxonomy();
      Console.WriteLine(codeExample.Description);
      try {
        codeExample.Run(new AdWordsUser());
      } catch (Exception ex) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(ex));
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    public void Run(AdWordsUser user) {
      // Get the ConstantDataService.
      ConstantDataService constantDataService = (ConstantDataService) user.GetService(
          AdWordsService.v201409.ConstantDataService);

      Selector selector = new Selector();
      Predicate predicate = new Predicate();
      predicate.field = "Country";
      predicate.@operator = PredicateOperator.IN;
      predicate.values = new string[] { "US" };

      try {
        ProductBiddingCategoryData[] results =
            constantDataService.getProductBiddingCategoryData(selector);

        Dictionary<long, ProductCategory> biddingCategories =
            new Dictionary<long, ProductCategory>();
        List<ProductCategory> rootCategories = new List<ProductCategory>();

        foreach (ProductBiddingCategoryData productBiddingCategory in results) {
          long id = productBiddingCategory.dimensionValue.value;
          long parentId = 0;
          string name = productBiddingCategory.displayValue[0].value;

          if (productBiddingCategory.parentDimensionValue != null) {
            parentId = productBiddingCategory.parentDimensionValue.value;
          }

          if (biddingCategories.ContainsKey(id)) {
            biddingCategories.Add(id, new ProductCategory());
          }

          ProductCategory category = biddingCategories[id];

          if (parentId != 0) {
            if (biddingCategories.ContainsKey(parentId)) {
              biddingCategories.Add(parentId, new ProductCategory());
            }
            ProductCategory parent = biddingCategories[parentId];
            parent.Children.Add(category);
          } else {
            rootCategories.Add(category);
          }

          category.Id = id;
          category.Name = name;
        }

        DisplayProductCategories(rootCategories, "");
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to set shopping product category.", ex);
      }
    }

    /// <summary>
    /// Displays the product categories.
    /// </summary>
    /// <param name="categories">The product categories.</param>
    /// <param name="prefix">The prefix for display purposes.</param>
    void DisplayProductCategories(List<ProductCategory> categories, string prefix) {
      foreach (ProductCategory category in categories) {
        Console.WriteLine("{0}{1} [{2}]", prefix, category.Name, category.Id);
        DisplayProductCategories(category.Children, string.Format("{0}{1} > ",
            prefix, category.Name));
      }
    }
  }
}
