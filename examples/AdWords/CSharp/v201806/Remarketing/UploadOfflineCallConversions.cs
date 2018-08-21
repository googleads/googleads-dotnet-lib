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
    /// This code example imports offline call conversion values for calls related to the
    /// ads in your account.
    /// </summary>
    public class UploadOfflineCallConversions : ExampleBase
    {
        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            string conversionName = "INSERT_CONVERSION_NAME_HERE";

            // For times use the format yyyyMMdd HHmmss tz. For more details on formats, see:
            // https://developers.google.com/adwords/api/docs/appendix/codes-formats#date-and-time-formats
            // For time zones, see:
            // https://developers.google.com/adwords/api/docs/appendix/codes-formats#timezone-ids

            //  The conversion time should be after the call start time.
            string conversionTime = "INSERT_CONVERSION_TIME_HERE";
            string callStartTime = "INSERT_CALL_START_TIME_HERE";

            string callerId = "INSERT_CALLER_ID_HERE";
            double conversionValue = double.Parse("INSERT_CONVERSION_VALUE_HERE");

            UploadOfflineCallConversions codeExample = new UploadOfflineCallConversions();
            Console.WriteLine(codeExample.Description);
            try
            {
                codeExample.Run(new AdWordsUser(), conversionName, callStartTime, callerId,
                    conversionTime, conversionValue);
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
                return
                    "This code example imports offline call conversion values for calls related " +
                    "to the ads in your account.";
            }
        }

        /// <summary>
        /// Runs the code example.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="conversionName">The name of the call conversion to be updated.</param>
        /// <param name="callStartTime">The call start time.</param>
        /// <param name="conversionValue">The conversion value to be uploaded.</param>
        /// <param name="callerId">The caller ID to be uploaded.</param>
        /// <param name="conversionTime">The conversion time, in yyyymmdd hhmmss
        /// format.</param>
        public void Run(AdWordsUser user, string conversionName, string callStartTime,
            string callerId, string conversionTime, double conversionValue)
        {
            using (OfflineCallConversionFeedService offlineCallConversionFeedService =
                (OfflineCallConversionFeedService) user.GetService(AdWordsService.v201806
                    .OfflineCallConversionFeedService))
            {
                // Associate offline call conversions with the existing named conversion tracker.
                // If this tracker was newly created, it may be a few hours before it can accept
                // conversions.
                OfflineCallConversionFeed feed = new OfflineCallConversionFeed
                {
                    callerId = callerId,
                    callStartTime = callStartTime,
                    conversionName = conversionName,
                    conversionTime = conversionTime,
                    conversionValue = conversionValue
                };

                OfflineCallConversionFeedOperation offlineCallConversionOperation =
                    new OfflineCallConversionFeedOperation
                    {
                        @operator = Operator.ADD,
                        operand = feed
                    };

                try
                {
                    // This example uploads only one call conversion, but you can upload
                    // multiple call conversions by passing additional operations.
                    OfflineCallConversionFeedReturnValue offlineCallConversionReturnValue =
                        offlineCallConversionFeedService.mutate(
                            new OfflineCallConversionFeedOperation[]
                            {
                                offlineCallConversionOperation
                            });

                    // Display results.
                    foreach (OfflineCallConversionFeed feedResult in
                        offlineCallConversionReturnValue.value)
                    {
                        Console.WriteLine(
                            "Uploaded offline call conversion value of {0} for caller ID '{1}'.",
                            feedResult.conversionValue, feedResult.callerId);
                    }
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException(
                        "Failed to upload offline call conversions.", e);
                }
            }
        }

    }
}
