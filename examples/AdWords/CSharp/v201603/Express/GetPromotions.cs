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
using System.Text;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201603 {

  /// <summary>
  /// This code example gets all express businesses. To add an express
  /// business, run AddExpressBusinesses.cs.
  /// </summary>
  public class GetPromotions : ExampleBase {

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets all express businesses. To add an express business, " +
            "run AddExpressBusinesses.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      GetPromotions codeExample = new GetPromotions();
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
      // Get the PromotionService.
      PromotionService promotionService = (PromotionService)
          user.GetService(AdWordsService.v201603.PromotionService);

      // Set the business ID to the service.
      promotionService.RequestHeader.expressBusinessId = businessId;

      Selector selector = new Selector();
      selector.fields = new String[] {
          Promotion.Fields.PromotionId, Promotion.Fields.Name, Promotion.Fields.Status,
          Promotion.Fields.DestinationUrl, Promotion.Fields.CallTrackingEnabled,
          Promotion.Fields.Budget, Promotion.Fields.PromotionCriteria,
          Promotion.Fields.RemainingBudget, Promotion.Fields.Creatives,
          Promotion.Fields.CampaignIds};


      PromotionPage page = null;
      try {
        do {
          // Get all promotions for the  business.
          page = promotionService.get(selector);

          // Display the results.
          if (page != null && page.entries != null) {
            int i = selector.paging.startIndex;
            foreach (Promotion promotion in page.entries) {
              // Summary.
              Console.WriteLine("0) Express promotion with name = {1} and id = {2} was found.",
                  i + 1, promotion.id, promotion.name);
              i++;
            }
          }
          selector.paging.IncreaseOffset();
        } while (selector.paging.startIndex < page.totalNumEntries);
        Console.WriteLine("Number of promotions found: {0}", page.totalNumEntries);
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to retrieve promotions.", e);
      }
    }
  }
}