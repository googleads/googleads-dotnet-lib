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
    /// This code example gets non-removed responsive search ads in an ad group. To add
    /// responsive search ads, run AddResponsiveSearchAd.cs. To get ad groups, run
    /// GetAdGroups.cs.
    /// </summary>
    public class GetResponsiveSearchAds : ExampleBase
    {
        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            GetResponsiveSearchAds codeExample = new GetResponsiveSearchAds();
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
                return "This code example gets non-removed responsive search ads in an ad group. " +
                    "To add responsive search ads, run AddResponsiveSearchAd.cs. To get ad " +
                    "groups, run GetAdGroups.cs.";
            }
        }

        /// <summary>
        /// Runs the code example.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="adGroupId">Id of the ad group from which expanded text ads
        /// are retrieved.</param>
        public void Run(AdWordsUser user, long adGroupId)
        {
            using (AdGroupAdService adGroupAdService =
                (AdGroupAdService) user.GetService(AdWordsService.v201809.AdGroupAdService))
            {
                // Create a selector to select all ads for the specified ad group.
                Selector selector = new Selector()
                {
                    fields = new string[]
                    {
                        ResponsiveSearchAd.Fields.Id,
                        AdGroupAd.Fields.Status,
                        ResponsiveSearchAd.Fields.ResponsiveSearchAdHeadlines,
                        ResponsiveSearchAd.Fields.ResponsiveSearchAdDescriptions
                    },
                    ordering = new OrderBy[]
                    {
                        OrderBy.Asc(ResponsiveSearchAd.Fields.Id)
                    },
                    predicates = new Predicate[]
                    {
                        // Restrict the fetch to only the selected ad group id.
                        Predicate.Equals(AdGroupAd.Fields.AdGroupId, adGroupId),

                        // Retrieve only responsive search ads.
                        Predicate.Equals("AdType", AdType.RESPONSIVE_SEARCH_AD.ToString()),
                    },
                    paging = Paging.Default
                };

                AdGroupAdPage page = new AdGroupAdPage();
                try
                {
                    do
                    {
                        // Get the responsive search ads.
                        page = adGroupAdService.get(selector);

                        // Display the results.
                        if (page != null && page.entries != null)
                        {
                            int i = selector.paging.startIndex;

                            foreach (AdGroupAd adGroupAd in page.entries)
                            {
                                ResponsiveSearchAd ad = (ResponsiveSearchAd) adGroupAd.ad;
                                Console.WriteLine(
                                    $"{i + 1} New responsive search ad with ID {ad.id} and " +
                                    $"status {adGroupAd.status} was found.");
                                Console.WriteLine("Headlines:");
                                foreach (AssetLink headline in ad.headlines)
                                {
                                    TextAsset textAsset = headline.asset as TextAsset;
                                    Console.WriteLine($"    {textAsset.assetText}");
                                    if (headline.pinnedFieldSpecified)
                                    {
                                        Console.WriteLine(
                                            $"      (pinned to {headline.pinnedField})");
                                    }
                                }

                                Console.WriteLine("Descriptions:");
                                foreach (AssetLink description in ad.descriptions)
                                {
                                    TextAsset textAsset = description.asset as TextAsset;
                                    Console.WriteLine($"    {textAsset.assetText}");
                                    if (description.pinnedFieldSpecified)
                                    {
                                        Console.WriteLine(
                                            $"      (pinned to {description.pinnedField})");
                                    }
                                }

                                i++;
                            }
                        }

                        selector.paging.IncreaseOffset();
                    } while (selector.paging.startIndex < page.totalNumEntries);

                    Console.WriteLine("Number of responsive search ads found: {0}",
                        page.totalNumEntries);
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException("Failed to get responsive search ads.",
                        e);
                }
            }

        }
    }
}
