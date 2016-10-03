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
using Google.Api.Ads.Dfp.Util.v201605;
using Google.Api.Ads.Dfp.v201605;

using System;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201605 {
  /// <summary>
  /// This example gets all base rates belonging to a rate card.
  /// </summary>
  public class GetBaseRatesForRateCard : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This example gets all base rates belonging to a rate card.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    public static void Main() {
      GetBaseRatesForRateCard codeExample = new GetBaseRatesForRateCard();
      Console.WriteLine(codeExample.Description);

      long rateCardId = long.Parse(_T("INSERT_RATE_CARD_ID_HERE"));
      codeExample.Run(new DfpUser(), rateCardId);
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    public void Run(DfpUser user, long rateCardId) {
      BaseRateService baseRateService =
          (BaseRateService) user.GetService(DfpService.v201605.BaseRateService);

      // Create a statement to select base rates belonging to a single rate card.
      StatementBuilder statementBuilder = new StatementBuilder()
          .Where("rateCardId = :rateCardId")
          .OrderBy("id ASC")
          .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT)
          .AddValue("rateCardId", rateCardId);

      // Retrieve a small amount of base rates at a time, paging through
      // until all base rates have been retrieved.
      BaseRatePage page = new BaseRatePage();
      try {
        do {
          page = baseRateService.getBaseRatesByStatement(statementBuilder.ToStatement());

          if (page.results != null) {
            // Print out some information for each base rate.
            int i = page.startIndex;
            foreach (BaseRate baseRate in page.results) {
              Console.WriteLine("{0}) Base rate with ID \"{1}\", type \"{2}\", "
                  + "and rate card ID \"{3}\" was found.",
                  i++,
                  baseRate.id,
                  baseRate.GetType().Name,
                  baseRate.rateCardId);
            }
          }

          statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
        } while (statementBuilder.GetOffset() < page.totalResultSetSize);

        Console.WriteLine("Number of results found: {0}", page.totalResultSetSize);
      } catch (Exception e) {
        Console.WriteLine("Failed to get base rates. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}
