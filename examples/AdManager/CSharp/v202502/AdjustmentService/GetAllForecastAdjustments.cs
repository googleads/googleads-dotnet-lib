// Copyright 2020 Google LLC
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
using Google.Api.Ads.AdManager.Util.v202502;
using Google.Api.Ads.AdManager.v202502;

using System;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v202502
{
    /// <summary>
    /// This example gets all forecast adjustments.
    /// </summary>
    public class GetAllForecastAdjustments : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get { return "This example gets all forecast adjustments."; }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            GetAllForecastAdjustments codeExample = new GetAllForecastAdjustments();
            Console.WriteLine(codeExample.Description);
            try
            {
                codeExample.Run(new AdManagerUser());
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to get adjustments. Exception says \"{0}\"", e.Message);
            }
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (AdjustmentService adjustmentService = user.GetService<AdjustmentService>())
            {
                // Create a statement to select adjustments.
                int pageSize = StatementBuilder.SUGGESTED_PAGE_LIMIT;
                StatementBuilder statementBuilder =
                    new StatementBuilder().OrderBy("id ASC").Limit(pageSize);

                // Retrieve a small amount of adjustments at a time, paging through until all
                // adjustments have been retrieved.
                int totalResultSetSize = 0;
                do
                {
                    ForecastAdjustmentPage page =
                        adjustmentService.getForecastAdjustmentsByStatement(
                            statementBuilder.ToStatement());

                    // Print out some information for each adjustment.
                    if (page.results != null)
                    {
                        totalResultSetSize = page.totalResultSetSize;
                        int i = page.startIndex;
                        foreach (ForecastAdjustment adjustment in page.results)
                        {
                            Console.WriteLine("Forecast adjustment with ID {0} and name '{1}' "
                                + "was found.",
                                adjustment.id,
                                adjustment.name);
                        }
                    }

                    statementBuilder.IncreaseOffsetBy(pageSize);
                } while (statementBuilder.GetOffset() < totalResultSetSize);

                Console.WriteLine("Number of results found: {0}", totalResultSetSize);
            }
        }
    }
}
