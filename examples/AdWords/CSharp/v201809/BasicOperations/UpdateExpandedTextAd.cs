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

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201809
{
    /// <summary>
    /// This code example updates an expanded text ad. To get expanded text ads,
    /// run GetExpandedTextAds.cs.
    /// </summary>
    public class UpdateExpandedTextAd : ExampleBase
    {
        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            UpdateExpandedTextAd codeExample = new UpdateExpandedTextAd();
            Console.WriteLine(codeExample.Description);
            try
            {
                long adId = long.Parse("INSERT_AD_ID_HERE");
                codeExample.Run(new AdWordsUser(), adId);
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
                return "This code example updates an expanded text ad. To get expanded text ads, " +
                    "run GetExpandedTextAds.cs.";
            }
        }

        /// <summary>
        /// Runs the code example.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="adId">Id of the ad to be updated.</param>
        public void Run(AdWordsUser user, long adId)
        {
            using (AdService adService =
                (AdService) user.GetService(AdWordsService.v201809.AdService))
            {
                // Create an expanded text ad using the provided ad ID.
                ExpandedTextAd expandedTextAd = new ExpandedTextAd
                {
                    id = adId,

                    // Update some properties of the expanded text ad.
                    headlinePart1 = "Cruise to Pluto #" + ExampleUtilities.GetShortRandomString(),
                    headlinePart2 = "Tickets on sale now",
                    description = "Best space cruise ever.",
                    finalUrls = new string[]
                    {
                        "http://www.example.com/"
                    },
                    finalMobileUrls = new string[]
                    {
                        "http://www.example.com/mobile"
                    }
                };

                // Create ad group ad operation and add it to the list.
                AdOperation operation = new AdOperation
                {
                    operand = expandedTextAd,
                    @operator = Operator.SET
                };

                try
                {
                    // Update the ad on the server.
                    AdReturnValue result = adService.mutate(new AdOperation[]
                    {
                        operation
                    });
                    ExpandedTextAd updatedAd = (ExpandedTextAd) result.value[0];

                    // Print out some information.
                    Console.WriteLine("Expanded text ad with ID {0} was updated.", updatedAd.id);
                    Console.WriteLine(
                        "Headline part 1: {0}\nHeadline part 2: {1}\nDescription: {2}" +
                        "\nFinal URL: {3}\nFinal mobile URL: {4}", updatedAd.headlinePart1,
                        updatedAd.headlinePart2, updatedAd.description, updatedAd.finalUrls[0],
                        updatedAd.finalMobileUrls[0]);
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException("Failed to update expanded text ad.", e);
                }
            }
        }
    }
}
