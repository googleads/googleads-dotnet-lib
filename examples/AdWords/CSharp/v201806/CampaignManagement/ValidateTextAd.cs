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
    /// This code example shows how to use the validateOnly header to validate
    /// an expanded text ad. No objects will be created, but exceptions will
    /// still be thrown.
    /// </summary>
    public class ValidateTextAd : ExampleBase
    {
        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            ValidateTextAd codeExample = new ValidateTextAd();
            Console.WriteLine(codeExample.Description);
            try
            {
                long adGroupId = long.Parse("INSERT_ADGROUP_ID_HERE");
                codeExample.Run(new AdWordsUser(), adGroupId);
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
                return "This code example shows how to use the validateOnly header to validate " +
                    "an expanded text ad. No objects will be created, but exceptions will still " +
                    "be thrown.";
            }
        }

        /// <summary>
        /// Runs the code example.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="adGroupId">Id of the ad group to which text ads are
        /// added.</param>
        public void Run(AdWordsUser user, long adGroupId)
        {
            using (AdGroupAdService adGroupAdService =
                (AdGroupAdService) user.GetService(AdWordsService.v201806.AdGroupAdService))
            {
                // Set the validateOnly headers.
                adGroupAdService.RequestHeader.validateOnly = true;

                // Create your expanded text ad.
                ExpandedTextAd expandedTextAd = new ExpandedTextAd()
                {
                    headlinePart1 = "Luxury Cruise to Mars",
                    headlinePart2 = "Visit the Red Planet in style.",
                    description = "Low-gravity fun for everyone!!",
                    finalUrls = new string[]
                    {
                        "http://www.example.com"
                    }
                };

                AdGroupAd adGroupAd = new AdGroupAd()
                {
                    adGroupId = adGroupId,
                    ad = expandedTextAd
                };

                AdGroupAdOperation operation = new AdGroupAdOperation()
                {
                    @operator = Operator.ADD,
                    operand = adGroupAd
                };

                try
                {
                    adGroupAdService.mutate(new AdGroupAdOperation[]
                    {
                        operation
                    });
                    // Since validation is ON, result will be null.
                    Console.WriteLine("Expanded text ad validated successfully.");
                }
                catch (AdWordsApiException e)
                {
                    // This block will be hit if there is a validation error from the server.
                    Console.WriteLine(
                        "There were validation error(s) while adding expanded text ad.");

                    if (e.ApiException != null)
                    {
                        foreach (ApiError error in ((ApiException) e.ApiException).errors)
                        {
                            Console.WriteLine("  Error type is '{0}' and fieldPath is '{1}'.",
                                error.ApiErrorType, error.fieldPath);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException("Failed to validate expanded text ad.",
                        e);
                }
            }
        }
    }
}
