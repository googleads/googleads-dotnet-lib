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
    /// This code example adds an ad customizer feed. Then it adds an ad in two
    /// different ad groups that uses the feed to populate dynamic data.
    /// </summary>
    public class AddAdCustomizers : ExampleBase
    {
        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            AddAdCustomizers codeExample = new AddAdCustomizers();
            Console.WriteLine(codeExample.Description);
            try
            {
                long adGroupId1 = long.Parse("INSERT_ADGROUP_ID_HERE");
                long adGroupId2 = long.Parse("INSERT_ADGROUP_ID_HERE");
                string feedName = "INSERT_FEED_NAME_HERE";
                codeExample.Run(new AdWordsUser(), adGroupId1, adGroupId2, feedName);
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
                return "This code example adds an ad customizer feed. Then it adds an ad in two " +
                    "different ad groups that uses the feed to populate dynamic data.";
            }
        }

        /// <summary>
        /// Runs the code example.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="adGroupId1">Id of the first adgroup to which ads with ad
        /// customizers are added.</param>
        /// <param name="adGroupId2">Id of the second adgroup to which ads with ad
        /// customizers are added.</param>
        /// <param name="feedName">Name of the feed to be created.</param>
        public void Run(AdWordsUser user, long adGroupId1, long adGroupId2, string feedName)
        {
            try
            {
                // Create a customizer feed. One feed per account can be used for all ads.
                AdCustomizerFeed adCustomizerFeed = CreateCustomizerFeed(user, feedName);

                // Add feed items containing the values we'd like to place in ads.
                CreateCustomizerFeedItems(user, new long[]
                {
                    adGroupId1,
                    adGroupId2
                }, adCustomizerFeed);

                // All set! We can now create ads with customizations.
                CreateAdsWithCustomizations(user, new long[]
                {
                    adGroupId1,
                    adGroupId2
                }, feedName);
            }
            catch (Exception e)
            {
                throw new System.ApplicationException("Failed to add ad customizers.", e);
            }
        }

        /// <summary>
        /// Creates a new Feed for ad customizers.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="feedName">Name of the feed to be created.</param>
        /// <returns>A new Ad customizer feed.</returns>
        private static AdCustomizerFeed CreateCustomizerFeed(AdWordsUser user, string feedName)
        {
            using (AdCustomizerFeedService adCustomizerFeedService =
                (AdCustomizerFeedService) user.GetService(AdWordsService.v201809
                    .AdCustomizerFeedService))
            {
                AdCustomizerFeed feed = new AdCustomizerFeed()
                {
                    feedName = feedName,
                    feedAttributes = new AdCustomizerFeedAttribute[]
                    {
                        new AdCustomizerFeedAttribute()
                        {
                            name = "Name",
                            type = AdCustomizerFeedAttributeType.STRING
                        },
                        new AdCustomizerFeedAttribute()
                        {
                            name = "Price",
                            type = AdCustomizerFeedAttributeType.PRICE
                        },
                        new AdCustomizerFeedAttribute()
                        {
                            name = "Date",
                            type = AdCustomizerFeedAttributeType.DATE_TIME
                        },
                    }
                };

                AdCustomizerFeedOperation feedOperation = new AdCustomizerFeedOperation()
                {
                    operand = feed,
                    @operator = (Operator.ADD)
                };

                AdCustomizerFeed addedFeed = adCustomizerFeedService.mutate(
                    new AdCustomizerFeedOperation[]
                    {
                        feedOperation
                    }).value[0];

                Console.WriteLine(
                    "Created ad customizer feed with ID = {0} and name = '{1}' and " +
                    "attributes: ", addedFeed.feedId, addedFeed.feedName);

                foreach (AdCustomizerFeedAttribute feedAttribute in addedFeed.feedAttributes)
                {
                    Console.WriteLine("  ID: {0}, name: '{1}', type: {2}", feedAttribute.id,
                        feedAttribute.name, feedAttribute.type);
                }

                return addedFeed;
            }
        }

        /// <summary>
        /// Restricts the feed item to an ad group.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="feedItem">The feed item.</param>
        /// <param name="adGroupId">The ad group ID.</param>
        private static void RestrictFeedItemToAdGroup(AdWordsUser user, FeedItem feedItem,
            long? adGroupId)
        {
            FeedItemAdGroupTarget adGroupTarget = new FeedItemAdGroupTarget()
            {
                feedId = feedItem.feedId,
                feedItemId = feedItem.feedItemId,
                adGroupId = adGroupId.Value
            };

            using (FeedItemTargetService feedItemTargetService =
                (FeedItemTargetService) user.GetService(
                    AdWordsService.v201809.FeedItemTargetService))
            {
                FeedItemTargetOperation operation = new FeedItemTargetOperation()
                {
                    @operator = Operator.ADD,
                    operand = adGroupTarget
                };

                FeedItemTargetReturnValue retval = feedItemTargetService.mutate(
                    new FeedItemTargetOperation[]
                    {
                        operation
                    });
                FeedItemAdGroupTarget newAdGroupTarget = (FeedItemAdGroupTarget) retval.value[0];
                Console.WriteLine(
                    "Feed item target for feed ID {0} and feed item ID {1}" +
                    " was created to restrict serving to ad group ID {2}", newAdGroupTarget.feedId,
                    newAdGroupTarget.feedItemId, newAdGroupTarget.adGroupId);
            }
        }

        /// <summary>
        /// Creates feed items with the values to use in ad customizations for each
        /// ad group in <code>adGroupIds</code>.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="adGroupIds">IDs of adgroups to which ad customizations are
        /// made.</param>
        /// <param name="adCustomizerFeed">The ad customizer feed.</param>
        private static void CreateCustomizerFeedItems(AdWordsUser user, long[] adGroupIds,
            AdCustomizerFeed adCustomizerFeed)
        {
            using (FeedItemService feedItemService =
                (FeedItemService) user.GetService(AdWordsService.v201809.FeedItemService))
            {
                List<FeedItemOperation> feedItemOperations = new List<FeedItemOperation>();

                DateTime marsDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                feedItemOperations.Add(CreateFeedItemAddOperation(adCustomizerFeed, "Mars",
                    "$1234.56", marsDate.ToString("yyyyMMdd HHmmss")));

                DateTime venusDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 15);
                feedItemOperations.Add(CreateFeedItemAddOperation(adCustomizerFeed, "Venus",
                    "$1450.00", venusDate.ToString("yyyyMMdd HHmmss")));

                FeedItemReturnValue feedItemReturnValue =
                    feedItemService.mutate(feedItemOperations.ToArray());

                foreach (FeedItem addedFeedItem in feedItemReturnValue.value)
                {
                    Console.WriteLine("Added feed item with ID {0}", addedFeedItem.feedItemId);
                }

                // Add feed item targeting to restrict the feed item to specific ad groups.
                RestrictFeedItemToAdGroup(user, feedItemReturnValue.value[0], adGroupIds[0]);
                RestrictFeedItemToAdGroup(user, feedItemReturnValue.value[1], adGroupIds[1]);
            }
        }

        /// <summary>
        /// Creates a FeedItemOperation that will create a FeedItem with the
        /// specified values when sent to FeedItemService.mutate.
        /// </summary>
        /// <param name="adCustomizerFeed">The ad customizer feed.</param>
        /// <param name="name">The value for the name attribute of the FeedItem.
        /// </param>
        /// <param name="price">The value for the price attribute of the FeedItem.
        /// </param>
        /// <param name="date">The value for the date attribute of the FeedItem.
        /// </param>
        /// <returns>A new FeedItemOperation for adding a FeedItem.</returns>
        private static FeedItemOperation CreateFeedItemAddOperation(
            AdCustomizerFeed adCustomizerFeed, string name, string price, string date)
        {
            FeedItem feedItem = new FeedItem()
            {
                feedId = adCustomizerFeed.feedId,

                // FeedAttributes appear in the same order as they were created
                // - Name, Price, Date. See CreateCustomizerFeed method for details.
                attributeValues = new FeedItemAttributeValue[]
                {
                    new FeedItemAttributeValue()
                    {
                        feedAttributeId = adCustomizerFeed.feedAttributes[0].id,
                        stringValue = name
                    },

                    new FeedItemAttributeValue()
                    {
                        feedAttributeId = adCustomizerFeed.feedAttributes[1].id,
                        stringValue = price
                    },

                    new FeedItemAttributeValue()
                    {
                        feedAttributeId = adCustomizerFeed.feedAttributes[2].id,
                        stringValue = date
                    }
                },
            };

            return new FeedItemOperation()
            {
                operand = feedItem,
                @operator = Operator.ADD
            };
        }

        /// <summary>
        /// Creates text ads that use ad customizations for the specified ad group
        /// IDs.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="adGroupIds">IDs of the ad groups to which customized ads
        /// are added.</param>
        /// <param name="feedName">Name of the feed to be used.</param>
        private static void CreateAdsWithCustomizations(AdWordsUser user, long[] adGroupIds,
            string feedName)
        {
            using (AdGroupAdService adGroupAdService =
                (AdGroupAdService) user.GetService(AdWordsService.v201809.AdGroupAdService))
            {
                ExpandedTextAd expandedTextAd = new ExpandedTextAd()
                {
                    headlinePart1 = string.Format("Luxury Cruise to {{={0}.Name}}", feedName),
                    headlinePart2 = string.Format("Only {{={0}.Price}}", feedName),
                    description =
                        string.Format("Offer ends in {{=countdown({0}.Date)}}!", feedName),
                    finalUrls = new string[]
                    {
                        "http://www.example.com"
                    }
                };

                // We add the same ad to both ad groups. When they serve, they will show
                // different values, since they match different feed items.
                List<AdGroupAdOperation> adGroupAdOperations = new List<AdGroupAdOperation>();
                foreach (long adGroupId in adGroupIds)
                {
                    AdGroupAd adGroupAd = new AdGroupAd()
                    {
                        adGroupId = adGroupId,
                        ad = expandedTextAd
                    };

                    AdGroupAdOperation adGroupAdOperation = new AdGroupAdOperation()
                    {
                        operand = adGroupAd,
                        @operator = Operator.ADD
                    };

                    adGroupAdOperations.Add(adGroupAdOperation);
                }

                AdGroupAdReturnValue adGroupAdReturnValue =
                    adGroupAdService.mutate(adGroupAdOperations.ToArray());

                foreach (AdGroupAd addedAd in adGroupAdReturnValue.value)
                {
                    Console.WriteLine("Created an ad with ID {0}, type '{1}' and status '{2}'.",
                        addedAd.ad.id, addedAd.ad.AdType, addedAd.status);
                }
            }
        }
    }
}
