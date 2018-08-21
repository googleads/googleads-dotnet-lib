// Copyright 2018 Google LLC
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
using Google.Api.Ads.AdWords.v201806;

using System;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201806
{
    /// <summary>
    /// This code example demonstrates adjusting one conversion, but you can add more than one
    /// operation in a single mutate request.
    /// </summary>
    public class UploadOfflineConversionAdjustments : ExampleBase
    {
        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            UploadOfflineConversionAdjustments codeExample =
                new UploadOfflineConversionAdjustments();
            Console.WriteLine(codeExample.Description);
            try
            {
                string conversionName = "INSERT_CONVERSION_NAME_HERE";
                string gclid = "INSERT_GOOGLE_CLICK_ID_HERE";
                string conversionTime = "INSERT_CONVERSION_TIME_HERE";
                OfflineConversionAdjustmentType adjustmentType =
                    (OfflineConversionAdjustmentType) Enum.Parse(
                        typeof(OfflineConversionAdjustmentType), "INSERT_ADJUSTMENT_TYPE_HERE");
                string adjustmentTime = "INSERT_ADJUSTMENT_TIME_HERE";
                double adjustedValue = double.Parse("INSERT_ADJUSTED_VALUE_HERE");

                codeExample.Run(new AdWordsUser(), conversionName, gclid, conversionTime,
                    adjustmentType, adjustmentTime, adjustedValue);
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception occurred while running this code example. {0}",
                    ExampleUtilities.FormatException(e));
            }
        }

        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example demonstrates adjusting one conversion, but you can add " +
                    "more than one operation in a single mutate request.";
            }
        }

        /// <summary>
        /// Runs the code example.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="conversionName">Name of the conversion to make adjustments.</param>
        /// <param name="gclid">The google click ID for the adjustment.</param>
        /// <param name="conversionTime">The conversion time.</param>
        /// <param name="adjustmentType">The type of conversion adjustment.</param>
        /// <param name="adjustmentTime">The conversion adjustment time.</param>
        /// <param name="adjustedValue">The conversion adjustment value.</param>
        public void Run(AdWordsUser user, string conversionName, string gclid,
            string conversionTime, OfflineConversionAdjustmentType adjustmentType,
            string adjustmentTime, double adjustedValue)
        {
            using (OfflineConversionAdjustmentFeedService service =
                (OfflineConversionAdjustmentFeedService) user.GetService(AdWordsService.v201806
                    .OfflineConversionAdjustmentFeedService))
            {
                // Associate conversion adjustments with the existing named conversion
                // tracker. The GCLID should have been uploaded before with a
                // conversion.
                GclidOfflineConversionAdjustmentFeed feed =
                    new GclidOfflineConversionAdjustmentFeed()
                    {
                        conversionName = conversionName,
                        googleClickId = gclid,
                        conversionTime = conversionTime,
                        adjustmentType = adjustmentType,
                        adjustmentTime = adjustmentTime,
                        adjustedValue = adjustedValue
                    };

                // Create the operation.
                var operation = new OfflineConversionAdjustmentFeedOperation()
                {
                    @operator = Operator.ADD,
                    operand = feed
                };

                try
                {
                    // Issue a request to the servers for adjustments of the conversion.
                    OfflineConversionAdjustmentFeedReturnValue retval = service.mutate(
                        new OfflineConversionAdjustmentFeedOperation[]
                        {
                            operation
                        });
                    GclidOfflineConversionAdjustmentFeed updatedFeed =
                        (GclidOfflineConversionAdjustmentFeed) retval.value[0];
                    Console.WriteLine(
                        "Uploaded conversion adjustment value of '{0}' for Google " +
                        "Click ID '{1}'.", updatedFeed.conversionName, updatedFeed.googleClickId);
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException("Failed to update conversion adjustment.",
                        e);
                }
            }
        }
    }
}
