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

using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.Util.v201608;
using Google.Api.Ads.Dfp.v201608;

using System;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201608 {
  /// <summary>
  /// This example gets all premium rates on a specific rate card.
  /// </summary>
  public class GetPremiumRatesForRateCard : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This example gets all premium rates on a specific rate card.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    public static void Main() {
      GetPremiumRatesForRateCard codeExample = new GetPremiumRatesForRateCard();
      Console.WriteLine(codeExample.Description);

      long rateCardId = long.Parse(_T("INSERT_RATE_CARD_ID_HERE"));
      codeExample.Run(new DfpUser(), rateCardId);
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    public void Run(DfpUser user, long rateCardId) {
      PremiumRateService premiumRateService =
          (PremiumRateService) user.GetService(DfpService.v201608.PremiumRateService);

      // Create a statement to select premium rates.
      StatementBuilder statementBuilder = new StatementBuilder()
          .Where("rateCardId = :rateCardId")
          .OrderBy("id ASC")
          .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT)
          .AddValue("rateCardId", rateCardId);

      // Retrieve a small amount of premium rates at a time, paging through
      // until all premium rates have been retrieved.
      PremiumRatePage page = new PremiumRatePage();
      try {
        do {
          page = premiumRateService.getPremiumRatesByStatement(statementBuilder.ToStatement());

          if (page.results != null) {
            // Print out some information for each premium rate.
            int i = page.startIndex;
            foreach (PremiumRate premiumRate in page.results) {
              Console.WriteLine("{0}) Premium rate with ID \"{1}\", premium feature \"{2}\", "
                  + "and rate card ID \"{3}\" was found.",
                  i++,
                  premiumRate.id,
                  premiumRate.GetType().Name,
                  premiumRate.rateCardId);
            }
          }

          statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
        } while (statementBuilder.GetOffset() < page.totalResultSetSize);

        Console.WriteLine("Number of results found: {0}", page.totalResultSetSize);
      } catch (Exception e) {
        Console.WriteLine("Failed to get premium rates. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}
