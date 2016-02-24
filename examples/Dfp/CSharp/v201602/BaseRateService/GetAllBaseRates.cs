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
  /// This code example gets all base rates. To create product base rates, run 
  /// CreateProductBaseRates.cs. To create product template base rates, run 
  /// CreateProductTemplateBaseRates.cs.
  /// </summary>
  class GetAllBaseRates : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets all base rates. To create product base rates, run " +
            "CreateProductBaseRates.cs. To create product template base rates, run " +
            "CreateProductTemplateBaseRates.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetAllBaseRates();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the BaseRateService.
      BaseRateService baseRateService =
          (BaseRateService) user.GetService(DfpService.v201602.BaseRateService);

      // Create a statement to get all base rates.
      StatementBuilder statementBuilder = new StatementBuilder()
          .OrderBy("id ASC")
          .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT);

      // Sets default for page.
      BaseRatePage page = new BaseRatePage();
      try {
        do {
          // Get base rates by statement.
          page = baseRateService.getBaseRatesByStatement(statementBuilder.ToStatement());

          if (page.results != null && page.results.Length > 0) {
            int i = page.startIndex;
            foreach (BaseRate baseRate in page.results) {
              Console.WriteLine("{0}) Base rate with ID ='{1}' and type '{2}' belonging to rate " +
                  "card '{3}' was found.", i++, baseRate.id, baseRate.GetType().Name,
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
