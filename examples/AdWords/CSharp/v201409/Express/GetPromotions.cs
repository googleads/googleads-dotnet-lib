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
using System.Text;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201409 {

  /// <summary>
  /// This code example gets all express businesses. To add an express
  /// business, run AddExpressBusinesses.cs.
  ///
  /// Tags: ExpressBusinessService.get
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
      } catch (Exception ex) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(ex));
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
          user.GetService(AdWordsService.v201409.PromotionService);

      // Set the business ID to the service.
      promotionService.RequestHeader.expressBusinessId = businessId;

      Selector selector = new Selector();
      selector.fields = new String[] {"PromotionId", "Name", "Status", "DestinationUrl",
          "StreetAddressVisible", "CallTrackingEnabled", "ContentNetworkOptedOut", "Budget",
          "PromotionCriteria", "RemainingBudget", "Creatives", "CampaignIds" };

      // Set the selector paging.
      selector.paging = new Paging();

      int offset = 0;
      int pageSize = 500;

      PromotionPage page = null;
      try {
        do {
          selector.paging.startIndex = offset;
          selector.paging.numberResults = pageSize;

          // Get all promotions for the  business.
          page = promotionService.get(selector);

          // Display the results.
          if (page != null && page.entries != null) {
            int i = offset;
            foreach (Promotion promotion in page.entries) {
              // Summary.
              Console.WriteLine("0) Express promotion with name = {1} and id = {2} was found.",
                  i + 1, promotion.id, promotion.name);
              i++;
            }
          }
          offset += pageSize;
        } while (offset < page.totalNumEntries);
        Console.WriteLine("Number of promotions found: {0}", page.totalNumEntries);
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to retrieve promotions.", ex);
      }
    }
  }
}