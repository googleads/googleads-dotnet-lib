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
using Google.Api.Ads.AdManager.Util.v202411;
using Google.Api.Ads.AdManager.v202411;

using System;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v202411
{
    /// <summary>
    /// This example updates a Forecast Adjustment's name.
    /// </summary>
    public class UpdateForecastAdjustments : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get { return "This example updates a Forecast Adjustment's name."; }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            UpdateForecastAdjustments codeExample = new UpdateForecastAdjustments();
            Console.WriteLine(codeExample.Description);
            try
            {
                codeExample.Run(new AdManagerUser());
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to update adjustments. Exception says \"{0}\"", e.Message);
            }
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (AdjustmentService adjustmentService = user.GetService<AdjustmentService>())
            {
                // Set the ID of the adjustment to update.
                long adjustmentId = long.Parse(_T("INSERT_ADJUSTMENT_ID_HERE"));

                // Create a statement to select adjustments.
                StatementBuilder statementBuilder =
                    new StatementBuilder()
                        .Where("id = :id")
                        .OrderBy("id ASC")
                        .Limit(1)
                        .AddValue("id", adjustmentId);

                ForecastAdjustmentPage page = adjustmentService
                    .getForecastAdjustmentsByStatement(statementBuilder.ToStatement());

                ForecastAdjustment adjustment = page.results[0];
                adjustment.name += " (updated)";

                ForecastAdjustment[] adjustments = adjustmentService.updateForecastAdjustments(
                    new ForecastAdjustment[] { adjustment });

                foreach (ForecastAdjustment updatedAdjustment in adjustments)
                {
                    Console.WriteLine("Forecast adjustment with ID {0} and name '{1}' was found.",
                        updatedAdjustment.id,
                        updatedAdjustment.name);
                }
            }
        }
    }
}
