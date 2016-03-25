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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201603;

using System;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201603 {

  /// <summary>
  /// This code example adds a new promotion to an express business. To get
  /// promotions, run GetPromotions.cs.
  /// </summary>
  public class AddPromotion : ExampleBase {

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example adds a new promotion to an express business. To get " +
            "promotions, run GetPromotions.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      AddPromotion codeExample = new AddPromotion();
      Console.WriteLine(codeExample.Description);
      try {
        long businessId = long.Parse("INSERT_ADWORDS_EXPRESS_BUSINESS_ID_HERE");
        codeExample.Run(new AdWordsUser(), businessId);
      } catch (Exception e) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(e));
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="businessId">The AdWords Express business id.</param>
    public void Run(AdWordsUser user, long businessId) {
      // Get the PromotionService
      PromotionService promotionService = (PromotionService)
          user.GetService(AdWordsService.v201603.PromotionService);

      // Set the business ID to the service.
      promotionService.RequestHeader.expressBusinessId = businessId;

      // First promotion
      Promotion marsTourPromotion = new Promotion();
      Money budget = new Money();
      budget.microAmount = 1000000L;
      marsTourPromotion.name = "Mars Tour Promotion " + ExampleUtilities.GetShortRandomString();
      marsTourPromotion.status = PromotionStatus.PAUSED;
      marsTourPromotion.destinationUrl = "http://www.example.com";
      marsTourPromotion.budget = budget;
      marsTourPromotion.callTrackingEnabled = true;

      // Criteria

      // Criterion - Travel Agency product service
      ProductService productService = new ProductService();
      productService.text = "Travel Agency";

      // Criterion - English language
      // The ID can be found in the documentation:
      // https://developers.google.com/adwords/api/docs/appendix/languagecodes
      Language language = new Language();
      language.id = 1000L;

      // Criterion - State of California
      Location location = new Location();
      location.id = 21137L;

      marsTourPromotion.criteria = new Criterion[] { productService, language, location };

      // Creative
      Creative creative = new Creative();
      creative.headline = "Standard Mars Trip";
      creative.line1 = "Fly coach to Mars";
      creative.line2 = "Free in-flight pretzels";

      marsTourPromotion.creatives = new Creative[] { creative };

      PromotionOperation operation = new PromotionOperation();
      operation.@operator = Operator.ADD;
      operation.operand = marsTourPromotion;

      try {
        Promotion[] addedPromotions = promotionService.mutate(
            new PromotionOperation[] { operation });

        Console.WriteLine("Added promotion ID {0} with name {1} to business ID {2}.",
        addedPromotions[0].id, addedPromotions[0].name, businessId);
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to add promotions.", e);
      }
    }
  }
}
