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
  /// This code example updates a promotion for an express businesses. To add
  /// a promotion, run UpdatePromotion.cs.
  /// </summary>
  public class UpdatePromotion : ExampleBase {

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example updates a promotion for an express businesses. To add a " +
            "promotion, run UpdatePromotion.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      UpdatePromotion codeExample = new UpdatePromotion();
      Console.WriteLine(codeExample.Description);
      try {
        long businessId = long.Parse("INSERT_ADWORDS_EXPRESS_BUSINESS_ID_HERE");
        long promotionId = long.Parse("INSERT_PROMOTION_ID_HERE");
        codeExample.Run(new AdWordsUser(), businessId, promotionId);
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
    /// <param name="promotionId">The promotion id.</param>
    public void Run(AdWordsUser user, long businessId, long promotionId) {
      // Get the ExpressBusinessService.
      ExpressBusinessService businessService = (ExpressBusinessService)
          user.GetService(AdWordsService.v201603.ExpressBusinessService);

      // Get the PromotionService
      PromotionService promotionService = (PromotionService)
          user.GetService(AdWordsService.v201603.PromotionService);

      // Set the business ID to the service.
      promotionService.RequestHeader.expressBusinessId = businessId;

      // Update the budget for the promotion
      Promotion promotion = new Promotion();
      promotion.id = promotionId;
      Money newBudget = new Money();
      newBudget.microAmount = 2000000;
      promotion.budget = newBudget;

      PromotionOperation operation = new PromotionOperation();
      operation.@operator = Operator.SET;
      operation.operand = promotion;

      try {
        Promotion[] updatedPromotions = promotionService.mutate(
            new PromotionOperation[] { operation });

        Console.WriteLine("Promotion ID {0} for business ID {1} now has budget micro " +
            "amount {2}.", promotionId, businessId,
            updatedPromotions[0].budget.microAmount);
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to update promotions.", e);
      }
    }
  }
}