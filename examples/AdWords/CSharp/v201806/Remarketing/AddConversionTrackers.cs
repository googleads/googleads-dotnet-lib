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
using System.Collections.Generic;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201806
{
    /// <summary>
    /// This code example adds an AdWords conversion tracker and an upload conversion tracker.
    /// </summary>
    public class AddConversionTrackers : ExampleBase
    {
        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            AddConversionTrackers codeExample = new AddConversionTrackers();
            Console.WriteLine(codeExample.Description);
            try
            {
                codeExample.Run(new AdWordsUser());
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
                return "This code example adds an AdWords conversion tracker and an upload " +
                    "conversion tracker.";
            }
        }

        /// <summary>
        /// Runs the code example.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        public void Run(AdWordsUser user)
        {
            using (ConversionTrackerService conversionTrackerService =
                (ConversionTrackerService) user.GetService(AdWordsService.v201806
                    .ConversionTrackerService))
            {
                List<ConversionTracker> conversionTrackers = new List<ConversionTracker>();

                // Create an Adwords conversion tracker.
                AdWordsConversionTracker adWordsConversionTracker = new AdWordsConversionTracker
                {
                    name =
                        "Earth to Mars Cruises Conversion #" + ExampleUtilities.GetRandomString(),
                    category = ConversionTrackerCategory.DEFAULT,

                    // Set optional fields.
                    status = ConversionTrackerStatus.ENABLED,
                    viewthroughLookbackWindow = 15,
                    defaultRevenueValue = 23.41,
                    alwaysUseDefaultRevenueValue = true
                };
                conversionTrackers.Add(adWordsConversionTracker);

                // Create an upload conversion for offline conversion imports.
                UploadConversion uploadConversion = new UploadConversion
                {
                    // Set an appropriate category. This field is optional, and will be set to
                    // DEFAULT if not mentioned.
                    category = ConversionTrackerCategory.LEAD,
                    name = "Upload Conversion #" + ExampleUtilities.GetRandomString(),
                    viewthroughLookbackWindow = 30,
                    ctcLookbackWindow = 90,

                    // Optional: Set the default currency code to use for conversions
                    // that do not specify a conversion currency. This must be an ISO 4217
                    // 3-character currency code such as "EUR" or "USD".
                    // If this field is not set on this UploadConversion, AdWords will use
                    // the account's currency.
                    defaultRevenueCurrencyCode = "EUR",

                    // Optional: Set the default revenue value to use for conversions
                    // that do not specify a conversion value. Note that this value
                    // should NOT be in micros.
                    defaultRevenueValue = 2.50
                };

                // Optional: To upload fractional conversion credits, mark the upload conversion
                // as externally attributed. See
                // https://developers.google.com/adwords/api/docs/guides/conversion-tracking#importing_externally_attributed_conversions
                // to learn more about importing externally attributed conversions.

                // uploadConversion.isExternallyAttributed = true;

                conversionTrackers.Add(uploadConversion);

                try
                {
                    // Create operations.
                    List<ConversionTrackerOperation> operations =
                        new List<ConversionTrackerOperation>();
                    foreach (ConversionTracker conversionTracker in conversionTrackers)
                    {
                        operations.Add(new ConversionTrackerOperation()
                        {
                            @operator = Operator.ADD,
                            operand = conversionTracker
                        });
                    }

                    // Add conversion tracker.
                    ConversionTrackerReturnValue retval =
                        conversionTrackerService.mutate(operations.ToArray());

                    // Display the results.
                    if (retval != null && retval.value != null)
                    {
                        foreach (ConversionTracker conversionTracker in retval.value)
                        {
                            Console.WriteLine(
                                "Conversion with ID {0}, name '{1}', status '{2}' and " +
                                "category '{3}' was added.", conversionTracker.id,
                                conversionTracker.name, conversionTracker.status,
                                conversionTracker.category);
                            if (conversionTracker is AdWordsConversionTracker)
                            {
                                AdWordsConversionTracker newAdWordsConversionTracker =
                                    (AdWordsConversionTracker) conversionTracker;
                                Console.WriteLine(
                                    "Google global site tag:\n{0}\nGoogle event snippet:\n{1}",
                                    newAdWordsConversionTracker.googleGlobalSiteTag,
                                    newAdWordsConversionTracker.googleEventSnippet);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("No conversion trackers were added.");
                    }
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException("Failed to add conversion trackers.", e);
                }
            }
        }
    }
}
