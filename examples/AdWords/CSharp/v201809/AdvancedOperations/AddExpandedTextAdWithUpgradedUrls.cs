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
using Google.Api.Ads.AdWords.v201809;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201809
{
    /// <summary>
    /// This code example adds an expanded text ad that uses advanced features
    /// of upgraded URLs.
    /// </summary>
    public class AddExpandedTextAdWithUpgradedUrls : ExampleBase
    {
        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            AddExpandedTextAdWithUpgradedUrls codeExample = new AddExpandedTextAdWithUpgradedUrls();
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
                return
                    "This code example adds an expanded text ad that uses advanced features of " +
                    "upgraded URLs.";
            }
        }

        /// <summary>
        /// Runs the code example.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="adGroupId">ID of the ad group to which ad is added.
        /// </param>
        public void Run(AdWordsUser user, long adGroupId)
        {
            using (AdGroupAdService adGroupAdService =
                (AdGroupAdService) user.GetService(AdWordsService.v201809.AdGroupAdService))
            {
                // Create the expanded text ad.
                ExpandedTextAd expandedTextAd = new ExpandedTextAd()
                {
                    headlinePart1 = "Luxury Cruise to Mars",
                    headlinePart2 = "Visit the Red Planet in style.",
                    description = "Low-gravity fun for everyone!",
                };

                // Specify a tracking URL for 3rd party tracking provider. You may
                // specify one at customer, campaign, ad group, ad, criterion or
                // feed item levels.
                expandedTextAd.trackingUrlTemplate =
                    "http://tracker.example.com/?season={_season}&promocode={_promocode}&u={lpurl}";

                // Since your tracking URL has two custom parameters, provide their
                // values too. This can be provided at campaign, ad group, ad, criterion
                // or feed item levels.
                CustomParameter seasonParameter = new CustomParameter
                {
                    key = "season",
                    value = "christmas"
                };

                CustomParameter promoCodeParameter = new CustomParameter
                {
                    key = "promocode",
                    value = "NYC123"
                };

                expandedTextAd.urlCustomParameters = new CustomParameters
                {
                    parameters = new CustomParameter[]
                    {
                        seasonParameter,
                        promoCodeParameter
                    }
                };

                // Specify a list of final URLs. This field cannot be set if URL field is
                // set. This may be specified at ad, criterion and feed item levels.
                expandedTextAd.finalUrls = new string[]
                {
                    "http://www.example.com/cruise/space/",
                    "http://www.example.com/locations/mars/"
                };

                // Specify a list of final mobile URLs. This field cannot be set if URL
                // field is set, or finalUrls is unset. This may be specified at ad,
                // criterion and feed item levels.
                expandedTextAd.finalMobileUrls = new string[]
                {
                    "http://mobile.example.com/cruise/space/",
                    "http://mobile.example.com/locations/mars/"
                };

                AdGroupAd adGroupAd = new AdGroupAd
                {
                    adGroupId = adGroupId,
                    ad = expandedTextAd,

                    // Optional: Set the status.
                    status = AdGroupAdStatus.PAUSED
                };

                // Create the operation.
                AdGroupAdOperation operation = new AdGroupAdOperation
                {
                    @operator = Operator.ADD,
                    operand = adGroupAd
                };

                AdGroupAdReturnValue retVal = null;

                try
                {
                    // Create the ads.
                    retVal = adGroupAdService.mutate(new AdGroupAdOperation[]
                    {
                        operation
                    });

                    // Display the results.
                    if (retVal != null && retVal.value != null)
                    {
                        ExpandedTextAd newExpandedTextAd = retVal.value[0].ad as ExpandedTextAd;

                        Console.WriteLine(
                            "Expanded text ad with ID '{0}' and headline '{1} - {2}' was added.",
                            newExpandedTextAd.id, newExpandedTextAd.headlinePart1,
                            newExpandedTextAd.headlinePart2);

                        Console.WriteLine("Upgraded URL properties:");

                        Console.WriteLine("  Final URLs: {0}",
                            string.Join(", ", newExpandedTextAd.finalUrls));
                        Console.WriteLine("  Final Mobile URLs: {0}",
                            string.Join(", ", newExpandedTextAd.finalMobileUrls));
                        Console.WriteLine("  Tracking URL template: {0}",
                            newExpandedTextAd.trackingUrlTemplate);

                        List<string> parameters = new List<string>();
                        foreach (CustomParameter customParam in newExpandedTextAd
                            .urlCustomParameters.parameters)
                        {
                            parameters.Add(string.Format("{0}={1}", customParam.key,
                                customParam.value));
                        }

                        Console.WriteLine("  Custom parameters: {0}",
                            string.Join(", ", parameters.ToArray()));
                    }
                    else
                    {
                        Console.WriteLine("No expanded text ads were created.");
                    }
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException("Failed to create expanded text ad.", e);
                }
            }
        }
    }
}
