// Copyright 2017, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.AdManager.Lib;
using Google.Api.Ads.AdManager.Util.v201811;
using Google.Api.Ads.AdManager.v201811;

using System;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v201811
{
    /// <summary>
    /// This example gets all premium rates on a specific rate card.
    /// </summary>
    public class GetPremiumRatesForRateCard : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get { return "This example gets all premium rates on a specific rate card."; }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            GetPremiumRatesForRateCard codeExample = new GetPremiumRatesForRateCard();
            long rateCardId = long.Parse("INSERT_RATE_CARD_ID_HERE");
            Console.WriteLine(codeExample.Description);
            try
            {
                codeExample.Run(new AdManagerUser(), rateCardId);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to get premium rates. Exception says \"{0}\"", e.Message);
            }
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user, long rateCardId)
        {
            using (PremiumRateService premiumRateService = user.GetService<PremiumRateService>())
            {
                // Create a statement to select premium rates.
                int pageSize = StatementBuilder.SUGGESTED_PAGE_LIMIT;
                StatementBuilder statementBuilder = new StatementBuilder()
                    .Where("rateCardId = :rateCardId").OrderBy("id ASC").Limit(pageSize)
                    .AddValue("rateCardId", rateCardId);

                // Retrieve a small amount of premium rates at a time, paging through until all
                // premium rates have been retrieved.
                int totalResultSetSize = 0;
                do
                {
                    PremiumRatePage page =
                        premiumRateService.getPremiumRatesByStatement(
                            statementBuilder.ToStatement());

                    // Print out some information for each premium rate.
                    if (page.results != null)
                    {
                        totalResultSetSize = page.totalResultSetSize;
                        int i = page.startIndex;
                        foreach (PremiumRate premiumRate in page.results)
                        {
                            Console.WriteLine(
                                "{0}) Premium rate with ID {1}, " + "premium feature \"{2}\", " +
                                "and rate card ID {3} was found.", i++, premiumRate.id,
                                premiumRate.GetType().Name, premiumRate.rateCardId);
                        }
                    }

                    statementBuilder.IncreaseOffsetBy(pageSize);
                } while (statementBuilder.GetOffset() < totalResultSetSize);

                Console.WriteLine("Number of results found: {0}", totalResultSetSize);
            }
        }
    }
}
