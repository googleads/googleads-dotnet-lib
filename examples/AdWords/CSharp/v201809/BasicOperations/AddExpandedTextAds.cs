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
    /// This code example adds expanded text ads to a given ad group. To list
    /// ad groups, run GetAdGroups.cs.
    /// </summary>
    public class AddExpandedTextAds : ExampleBase
    {
        /// <summary>
        /// Number of ads being added / updated in this code example.
        /// </summary>
        private const int NUMBER_OF_ADS = 5;

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            AddExpandedTextAds codeExample = new AddExpandedTextAds();
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
                return "This code example adds expanded text ads to a given ad group. To list " +
                    "ad groups, run GetAdGroups.cs.";
            }
        }

        /// <summary>
        /// Runs the code example.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="adGroupId">Id of the ad group to which ads are added.
        /// </param>
        public void Run(AdWordsUser user, long adGroupId)
        {
            using (AdGroupAdService adGroupAdService =
                (AdGroupAdService) user.GetService(AdWordsService.v201809.AdGroupAdService))
            {
                List<AdGroupAdOperation> operations = new List<AdGroupAdOperation>();

                for (int i = 0; i < NUMBER_OF_ADS; i++)
                {
                    // Create the expanded text ad.
                    ExpandedTextAd expandedTextAd = new ExpandedTextAd
                    {
                        headlinePart1 = "Cruise #" + i.ToString() + " to Mars",
                        headlinePart2 = "Best Space Cruise Line",
                        headlinePart3 = "For Your Loved Ones",
                        description = "Buy your tickets now!",
                        description2 = "Discount ends soon",
                        finalUrls = new string[]
                        {
                            "http://www.example.com/" + i
                        }
                    };

                    AdGroupAd expandedTextAdGroupAd = new AdGroupAd
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
                        operand = expandedTextAdGroupAd
                    };

                    operations.Add(operation);
                }

                AdGroupAdReturnValue retVal = null;

                try
                {
                    // Create the ads.
                    retVal = adGroupAdService.mutate(operations.ToArray());

                    // Display the results.
                    if (retVal != null && retVal.value != null)
                    {
                        foreach (AdGroupAd adGroupAd in retVal.value)
                        {
                            ExpandedTextAd newAd = adGroupAd.ad as ExpandedTextAd;
                            Console.WriteLine(
                                "Expanded text ad with ID '{0}' and headline '{1} - {2}' " +
                                "was added.", newAd.id, newAd.headlinePart1, newAd.headlinePart2);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No expanded text ads were created.");
                    }

                    adGroupAdService.Close();
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException("Failed to create expanded text ad.", e);
                }
            }

        }
    }
}
