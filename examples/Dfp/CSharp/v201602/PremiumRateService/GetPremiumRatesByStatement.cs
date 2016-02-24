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

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201602 {
  /// <summary>
  /// This code example gets all premium rates on a specific rate card. To see what rate cards
  /// exist, run GetAllRateCards.cs.
  /// </summary>
  class GetPremiumRatesByStatement : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets all premium rates on a specific rate card. To see what " +
            "rate cards exist, run GetAllRateCards.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetPremiumRatesByStatement();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the PremiumRateService.
      PremiumRateService premiumRateService =
          (PremiumRateService) user.GetService(DfpService.v201602.PremiumRateService);

      // Set the ID of the rate card to get premium rates for.
      long rateCardId = long.Parse(_T("INSERT_RATE_CARD_ID_HERE"));

      // Create a statement to get all premium rates belonging to a specific rate card.
      StatementBuilder statementBuilder = new StatementBuilder()
          .Where("rateCardId = :rateCardId")
          .OrderBy("id ASC")
          .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT)
          .AddValue("rateCardId", rateCardId);

      // Set default for page.
      PremiumRatePage page = new PremiumRatePage();

      try {
        do {
          // Get premium rates by statement.
          page = premiumRateService.getPremiumRatesByStatement(statementBuilder.ToStatement());

          if (page.results != null && page.results.Length > 0) {
            int i = page.startIndex;
            foreach (PremiumRate premiumRate in page.results) {
              Console.WriteLine("{0}) Premium rate with ID '{1}' of type '{2}' assigned to rate " +
                  "card with ID '{3}' was found.", i++, premiumRate.id,
                  premiumRate.premiumFeature.GetType().Name, premiumRate.rateCardId);
            }
          }
          statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
        } while (statementBuilder.GetOffset() < page.totalResultSetSize);
        Console.WriteLine("Number of results found: {0}", page.totalResultSetSize);
      } catch (Exception e) {
        Console.WriteLine("Failed to get premium rates by statement. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}
