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
    /// This code example retrieves all expanded text ads given an existing ad group.
    /// To add expanded text ads to an existing ad group, run AddExpandedTextAds.cs.
    /// </summary>
    public class GetExpandedTextAds : ExampleBase
    {
        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            GetExpandedTextAds codeExample = new GetExpandedTextAds();
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
                return "This code example retrieves all expanded text ads given an existing ad " +
                    "group. To add expanded text ads to an existing ad group, " +
                    "run AddExpandedTextAds.cs.";
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
                // Create a selector.
                Selector selector = new Selector()
                {
                    fields = new string[]
                    {
                        ExpandedTextAd.Fields.Id,
                        AdGroupAd.Fields.Status,
                        ExpandedTextAd.Fields.HeadlinePart1,
                        ExpandedTextAd.Fields.HeadlinePart2,
                        ExpandedTextAd.Fields.Description,
                    },
                    ordering = new OrderBy[]
                    {
                        OrderBy.Asc(ExpandedTextAd.Fields.Id)
                    },
                    predicates = new Predicate[]
                    {
                        // Restrict the fetch to only the selected ad group id.
                        Predicate.Equals(AdGroupAd.Fields.AdGroupId, adGroupId),

                        // Retrieve only expanded text ads.
                        Predicate.Equals("AdType", "EXPANDED_TEXT_AD"),

                        // By default disabled ads aren't returned by the selector. To return
                        // them include the DISABLED status in the statuses field.
                        Predicate.In(AdGroupAd.Fields.Status, new string[]
                        {
                            AdGroupAdStatus.ENABLED.ToString(),
                            AdGroupAdStatus.PAUSED.ToString(),
                            AdGroupAdStatus.DISABLED.ToString()
                        })
                    },
                    paging = Paging.Default
                };

                AdGroupAdPage page = new AdGroupAdPage();

                try
                {
                    do
                    {
                        // Get the expanded text ads.
                        page = adGroupAdService.get(selector);

                        // Display the results.
                        if (page != null && page.entries != null)
                        {
                            int i = selector.paging.startIndex;

                            foreach (AdGroupAd adGroupAd in page.entries)
                            {
                                ExpandedTextAd expandedTextAd = (ExpandedTextAd) adGroupAd.ad;
                                Console.WriteLine(
                                    "{0} : Expanded text ad with ID '{1}', headline '{2} - {3}' " +
                                    "and description '{4} was found.", i + 1, expandedTextAd.id,
                                    expandedTextAd.headlinePart1, expandedTextAd.headlinePart2,
                                    expandedTextAd.description);
                                i++;
                            }
                        }

                        selector.paging.IncreaseOffset();
                    } while (selector.paging.startIndex < page.totalNumEntries);

                    Console.WriteLine("Number of expanded text ads found: {0}",
                        page.totalNumEntries);
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException("Failed to get expanded text ads", e);
                }
            }

        }
    }
}
