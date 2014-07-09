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
using Google.Api.Ads.AdWords.v201406;

using System;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201406 {

  /// <summary>
  /// This code example shows how to retrieve AdWords Express product/service
  /// objects related to specific criteria.
  ///
  /// Tags: ProductServiceService.get
  /// </summary>
  public class GetProductServices : ExampleBase {

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example shows how to retrieve AdWords Express product/service " +
            "objects related to specific criteria.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      GetProductServices codeExample = new GetProductServices();
      Console.WriteLine(codeExample.Description);
      try {
        string productServiceSuggestion = "INSERT_PRODUCT_SERVICE_SUGGESTION_HERE";
        string localeText = "INSERT_LOCALE_TEXT_HERE";
        codeExample.Run(new AdWordsUser(), productServiceSuggestion, localeText);
      } catch (Exception ex) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(ex));
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="productServiceSuggestion">The product/service suggestion.
    /// </param>
    /// <param name="localeText">The locale text.</param>
    public void Run(AdWordsUser user, string productServiceSuggestion, string localeText) {
      // Get the service, which loads the required classes.
      ProductServiceService productServiceService = (ProductServiceService) user.GetService(
          AdWordsService.v201406.ProductServiceService);

      // Create selector.
      Selector selector = new Selector();
      selector.fields = new string[] {"ProductServiceText"};

      // Create predicates.
      Predicate textPredicate = new Predicate();
      textPredicate.field = "ProductServiceText";
      textPredicate.@operator = PredicateOperator.EQUALS;
      textPredicate.values = new string[] {productServiceSuggestion};

      Predicate localePredicate = new Predicate();
      localePredicate.field = "Locale";
      localePredicate.@operator = PredicateOperator.EQUALS;
      localePredicate.values = new string[]{localeText};

      selector.predicates = new Predicate[] {textPredicate, localePredicate};

      // Set the selector paging.
      selector.paging = new Paging();

      int offset = 0;
      int pageSize = 500;

      ProductServicePage page = null;
      try {
        do {
          selector.paging.startIndex = offset;
          selector.paging.numberResults = pageSize;

          // Make the get request.
          page = productServiceService.get(selector);

          // Display the results.
          if (page != null && page.entries != null) {
            int i = offset;
            foreach (ProductService productService in page.entries) {
              Console.WriteLine("Product/service with text '{0}' found", productService.text);
              i++;
            }
          }
          offset += pageSize;
        } while (offset < page.totalNumEntries);
        Console.WriteLine("Number of products/services found: {0}", page.totalNumEntries);
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to retrieve products/services.", ex);
      }
    }
  }
}