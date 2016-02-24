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
using System.Collections.Generic;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201602 {
  /// <summary>
  /// This code example activates a product template. To determine which product templates
  /// exist, run GetAllProductTemplates.cs.
  /// </summary>
  class ActivateProductTemplates : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example activates a product template. To determine which product " +
            "templates exist, run GetAllProductTemplates.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new ActivateProductTemplates();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the ProductTemplateService.
      ProductTemplateService productTemplateService =
          (ProductTemplateService) user.GetService(DfpService.v201602.ProductTemplateService);

      // Set the ID of the product template to activate.
      long productTemplateId = long.Parse(_T("INSERT_PRODUCT_TEMPLATE_ID_HERE"));

      // Create statement to select a product template by ID.
      StatementBuilder statementBuilder = new StatementBuilder()
          .Where("id = :id")
          .OrderBy("id ASC")
          .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT)
          .AddValue("id", productTemplateId);

      // Set default for page.
      ProductTemplatePage page = new ProductTemplatePage();
      List<string> productTemplateIds = new List<string>();

      try {
        do {
          // Get product templates by statement.
          page = productTemplateService.getProductTemplatesByStatement(
              statementBuilder.ToStatement());

          if (page.results != null && page.results.Length > 0) {
            int i = page.startIndex;
            foreach (ProductTemplate productTemplate in page.results) {
              Console.WriteLine("{0}) Product template with ID ='{1}' will be activated.",
                  i++, productTemplate.id);
              productTemplateIds.Add(productTemplate.id.ToString());
            }
          }

          statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
        } while (statementBuilder.GetOffset() < page.totalResultSetSize);

        Console.WriteLine("Number of product templates to be activated: {0}",
            productTemplateIds.Count);

        if (productTemplateIds.Count > 0) {
          // Modify statement.
          statementBuilder.RemoveLimitAndOffset();

          // Create action.
          Google.Api.Ads.Dfp.v201602.ActivateProductTemplates action =
              new Google.Api.Ads.Dfp.v201602.ActivateProductTemplates();

          // Perform action.
          UpdateResult result = productTemplateService.performProductTemplateAction(action,
              statementBuilder.ToStatement());

          // Display results.
          if (result != null && result.numChanges > 0) {
            Console.WriteLine("Number of product templates activated: {0}", result.numChanges);
          } else {
            Console.WriteLine("No product templates were activated.");
          }
        }
      } catch (Exception e) {
        Console.WriteLine("Failed to activate product templates. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}
