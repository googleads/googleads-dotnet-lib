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
  /// This code example restricts the products that will be included in the
  /// campaign by setting a ProductScope.
  ///
  /// Tags: CampaignCriterionService.mutate
  /// </summary>
  public class AddProductScope : ExampleBase {

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example restricts the products that will be included in the campaign " +
            "by setting a ProductScope.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      AddProductScope codeExample = new AddProductScope();
      Console.WriteLine(codeExample.Description);
      try {
        long campaignId = long.Parse("INSERT_CAMPAIGN_ID_HERE");
        codeExample.Run(new AdWordsUser(), campaignId);
      } catch (Exception ex) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(ex));
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="campaignId">The campaign id to add product scope.</param>
    public void Run(AdWordsUser user, long campaignId) {
      // Get the CampaignCriterionService.
      CampaignCriterionService campaignCriterionService =
          (CampaignCriterionService) user.GetService(
              AdWordsService.v201409.CampaignCriterionService);

      ProductScope productScope = new ProductScope();
      // This set of dimensions is for demonstration purposes only. It would be
      // extremely unlikely that you want to include so many dimensions in your
      // product scope.
      ProductBrand nexusBrand = new ProductBrand();
      nexusBrand.value = "Nexus";

      ProductCanonicalCondition newProducts = new ProductCanonicalCondition();
      newProducts.condition = ProductCanonicalConditionCondition.NEW;

      ProductCustomAttribute customAttribute = new ProductCustomAttribute();
      customAttribute.type = ProductDimensionType.CUSTOM_ATTRIBUTE_0;
      customAttribute.value = "my attribute value";

      ProductOfferId bookOffer = new ProductOfferId();
      bookOffer.value = "book1";

      ProductType mediaProducts = new ProductType();
      mediaProducts.type = ProductDimensionType.PRODUCT_TYPE_L1;
      mediaProducts.value = "Media";

      ProductType bookProducts = new ProductType();
      bookProducts.type = ProductDimensionType.PRODUCT_TYPE_L2;
      bookProducts.value = "Books";

      // The value for the bidding category is a fixed ID for the
      // 'Luggage & Bags' category. You can retrieve IDs for categories from
      // the ConstantDataService. See the 'GetProductCategoryTaxonomy' example
      // for more details.
      ProductBiddingCategory luggageBiddingCategory = new ProductBiddingCategory();
      luggageBiddingCategory.type = ProductDimensionType.BIDDING_CATEGORY_L1;
      luggageBiddingCategory.value = -5914235892932915235;

      productScope.dimensions = new ProductDimension[] {nexusBrand, newProducts, bookOffer,
          mediaProducts, luggageBiddingCategory};

      CampaignCriterion campaignCriterion = new CampaignCriterion();
      campaignCriterion.campaignId = campaignId;
      campaignCriterion.criterion = productScope;

      // Create operation.
      CampaignCriterionOperation operation = new CampaignCriterionOperation();
      operation.operand = campaignCriterion;
      operation.@operator = Operator.ADD;

      try {
        // Make the mutate request.
        CampaignCriterionReturnValue result = campaignCriterionService.mutate(
            new CampaignCriterionOperation[] { operation });

        Console.WriteLine("Created a ProductScope criterion with ID '{0}'",
              result.value[0].criterion.id);
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to set shopping product scope.", ex);
      }
    }
  }
}
