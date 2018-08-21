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
    /// This code example adds a responsive search ad to a given ad group. To get ad groups,
    /// run GetAdGroups.cs.
    /// </summary>
    public class AddResponsiveSearchAd : ExampleBase
    {
        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            AddResponsiveSearchAd codeExample = new AddResponsiveSearchAd();
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
                return "This code example adds a responsive search ad to a given ad group. " +
                    "To get ad groups, run GetAdGroups.cs.";
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
                (AdGroupAdService) user.GetService(AdWordsService.v201806.AdGroupAdService))
            {
                // Create a responsive search ad.
                ResponsiveSearchAd responsiveSearchAd = new ResponsiveSearchAd()
                {
                    finalUrls = new string[]
                    {
                        "http://www.example.com/cruise"
                    },
                    path1 = "all-inclusive",
                    path2 = "deals",
                    headlines = new AssetLink[]
                    {
                        new AssetLink()
                        {
                            asset = new TextAsset()
                            {
                                assetText =
                                    "Cruise to Mars #" + ExampleUtilities.GetShortRandomString(),
                            },
                            // Set a pinning to always choose this asset for HEADLINE_1.
                            // Pinning is optional; if no pinning is set, then headlines
                            // and descriptions will be rotated and the ones that perform
                            // best will be used more often.
                            pinnedField = ServedAssetFieldType.HEADLINE_1
                        },
                        new AssetLink()
                        {
                            asset = new TextAsset()
                            {
                                assetText = "Best Space Cruise Line",
                            }
                        },
                        new AssetLink()
                        {
                            asset = new TextAsset()
                            {
                                assetText = "Experience the Stars",
                            }
                        },
                    },
                    descriptions = new AssetLink[]
                    {
                        new AssetLink()
                        {
                            asset = new TextAsset()
                            {
                                assetText = "Buy your tickets now",
                            }
                        },
                        new AssetLink()
                        {
                            asset = new TextAsset()
                            {
                                assetText = "Visit the Red Planet",
                            }
                        },
                    }
                };

                // Create ad group ad.
                AdGroupAd adGroupAd = new AdGroupAd()
                {
                    adGroupId = adGroupId,
                    ad = responsiveSearchAd,

                    // Optional: Set additional settings.
                    status = AdGroupAdStatus.PAUSED
                };


                // Create ad group ad operation and add it to the list.
                AdGroupAdOperation operation = new AdGroupAdOperation()
                {
                    operand = adGroupAd,
                    @operator = Operator.ADD
                };

                try
                {
                    // Add the responsive search ad on the server.
                    AdGroupAdReturnValue retval = adGroupAdService.mutate(new AdGroupAdOperation[]
                    {
                        operation
                    });

                    // Print out some information for the created ad.
                    foreach (AdGroupAd newAdGroupAd in retval.value)
                    {
                        ResponsiveSearchAd newAd = (ResponsiveSearchAd) newAdGroupAd.ad;
                        Console.WriteLine(
                            $"New responsive search ad with ID {newAd.id} was added.");
                        Console.WriteLine("Headlines:");
                        foreach (AssetLink headline in newAd.headlines)
                        {
                            TextAsset textAsset = headline.asset as TextAsset;
                            Console.WriteLine($"    {textAsset.assetText}");
                            if (headline.pinnedFieldSpecified)
                            {
                                Console.WriteLine($"      (pinned to {headline.pinnedField})");
                            }
                        }

                        Console.WriteLine("Descriptions:");
                        foreach (AssetLink description in newAd.descriptions)
                        {
                            TextAsset textAsset = description.asset as TextAsset;
                            Console.WriteLine($"    {textAsset.assetText}");
                            if (description.pinnedFieldSpecified)
                            {
                                Console.WriteLine($"      (pinned to {description.pinnedField})");
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException("Failed to create responsive search ad.",
                        e);
                }
            }
        }
    }
}
