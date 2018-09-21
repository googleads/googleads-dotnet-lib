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
    /// This code example retrieves keywords that are related to a given keyword.
    /// </summary>
    public class GetKeywordIdeas : ExampleBase
    {
        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            GetKeywordIdeas codeExample = new GetKeywordIdeas();
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
                return "This code example retrieves keywords that are related to a given keyword.";
            }
        }

        /// <summary>
        /// Runs the code example.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="adGroupId">ID of the ad group to use for generating ideas.</param>
        public void Run(AdWordsUser user, long? adGroupId)
        {
            using (TargetingIdeaService targetingIdeaService =
                (TargetingIdeaService) user.GetService(AdWordsService.v201809.TargetingIdeaService))
            {
                // Create selector.
                TargetingIdeaSelector selector = new TargetingIdeaSelector
                {
                    requestType = RequestType.IDEAS,
                    ideaType = IdeaType.KEYWORD,
                    requestedAttributeTypes = new AttributeType[]
                    {
                        AttributeType.KEYWORD_TEXT,
                        AttributeType.SEARCH_VOLUME,
                        AttributeType.AVERAGE_CPC,
                        AttributeType.COMPETITION,
                        AttributeType.CATEGORY_PRODUCTS_AND_SERVICES
                    }
                };


                List<SearchParameter> searchParameters = new List<SearchParameter>();

                // Create related to query search parameter.
                RelatedToQuerySearchParameter relatedToQuerySearchParameter =
                    new RelatedToQuerySearchParameter
                    {
                        queries = new string[]
                        {
                            "bakery",
                            "pastries",
                            "birthday cake"
                        }
                    };
                searchParameters.Add(relatedToQuerySearchParameter);

                // Add a language search parameter (optional).
                // The ID can be found in the documentation:
                //   https://developers.google.com/adwords/api/docs/appendix/languagecodes
                LanguageSearchParameter languageParameter = new LanguageSearchParameter();
                Language english = new Language
                {
                    id = 1000
                };
                languageParameter.languages = new Language[]
                {
                    english
                };
                searchParameters.Add(languageParameter);

                // Add network search parameter (optional).
                NetworkSetting networkSetting = new NetworkSetting
                {
                    targetGoogleSearch = true,
                    targetSearchNetwork = false,
                    targetContentNetwork = false,
                    targetPartnerSearchNetwork = false
                };

                NetworkSearchParameter networkSearchParameter = new NetworkSearchParameter
                {
                    networkSetting = networkSetting
                };
                searchParameters.Add(networkSearchParameter);

                // Optional: Use an existing ad group to generate ideas.
                if (adGroupId != null)
                {
                    SeedAdGroupIdSearchParameter seedAdGroupIdSearchParameter =
                        new SeedAdGroupIdSearchParameter
                        {
                            adGroupId = adGroupId.Value
                        };
                    searchParameters.Add(seedAdGroupIdSearchParameter);
                }

                // Set the search parameters.
                selector.searchParameters = searchParameters.ToArray();

                // Set selector paging (required for targeting idea service).
                selector.paging = Paging.Default;

                TargetingIdeaPage page = new TargetingIdeaPage();

                try
                {
                    int i = 0;
                    do
                    {
                        // Get related keywords.
                        page = targetingIdeaService.get(selector);

                        // Display related keywords.
                        if (page.entries != null && page.entries.Length > 0)
                        {
                            foreach (TargetingIdea targetingIdea in page.entries)
                            {
                                Dictionary<AttributeType, Google.Api.Ads.AdWords.v201809.Attribute>
                                    ideas = targetingIdea.data.ToDict();

                                string keyword =
                                    (ideas[AttributeType.KEYWORD_TEXT] as StringAttribute).value;
                                IntegerSetAttribute categorySet =
                                    ideas[AttributeType.CATEGORY_PRODUCTS_AND_SERVICES] as
                                        IntegerSetAttribute;

                                string categories = "";

                                if (categorySet != null && categorySet.value != null)
                                {
                                    categories = string.Join(", ", categorySet.value);
                                }

                                long averageMonthlySearches =
                                    (ideas[AttributeType.SEARCH_VOLUME] as LongAttribute).value;

                                Money averageCpc =
                                    (ideas[AttributeType.AVERAGE_CPC] as MoneyAttribute).value;
                                double competition =
                                    (ideas[AttributeType.COMPETITION] as DoubleAttribute).value;
                                Console.WriteLine(
                                    "Keyword with text '{0}', average monthly search volume {1}, " +
                                    "average CPC {2}, and competition {3:F2} was found with " +
                                    "categories: {4}", keyword, averageMonthlySearches,
                                    averageCpc?.microAmount, competition, categories);

                                Console.WriteLine(
                                    "Keyword with text '{0}', and average monthly search volume " +
                                    "'{1}' was found with categories: {2}", keyword,
                                    averageMonthlySearches, categories);
                                i++;
                            }
                        }

                        selector.paging.IncreaseOffset();
                    } while (selector.paging.startIndex < page.totalNumEntries);

                    Console.WriteLine("Number of related keywords found: {0}",
                        page.totalNumEntries);
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException("Failed to retrieve related keywords.",
                        e);
                }
            }
        }
    }
}
