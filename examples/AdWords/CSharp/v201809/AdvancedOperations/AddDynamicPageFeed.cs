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
    /// This code example adds a page feed to specify precisely which URLs to use with your
    /// Dynamic Search Ads campaign. To create a Dynamic Search Ads campaign, run
    /// AddDynamicSearchAdsCampaign.cs. To get campaigns, run GetCampaigns.cs.
    /// </summary>
    public class AddDynamicPageFeed : ExampleBase
    {
        /// <summary>
        /// The criterion type to be used for DSA page feeds.
        /// </summary>
        /// <remarks>DSA page feeds use criterionType field instead of the placeholderType field
        /// unlike most other feed types.</remarks>
        private const int DSA_PAGE_FEED_CRITERION_TYPE = 61;

        /// <summary>
        /// ID that corresponds to the page URLs.
        /// </summary>
        private const int DSA_PAGE_URLS_FIELD_ID = 1;

        /// <summary>
        /// ID that corresponds to the labels.
        /// </summary>
        private const int DSA_LABEL_FIELD_ID = 2;

        /// <summary>
        /// Class to keep track of DSA page feed details.
        /// </summary>
        private class DSAFeedDetails
        {
            public long feedId { get; set; }
            public long urlAttributeId { get; set; }
            public long labelAttributeId { get; set; }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            AddDynamicPageFeed codeExample = new AddDynamicPageFeed();
            Console.WriteLine(codeExample.Description);
            try
            {
                long campaignId = long.Parse("INSERT_CAMPAIGN_ID_HERE");
                long adGroupId = long.Parse("INSERT_ADGROUP_ID_HERE");
                codeExample.Run(new AdWordsUser(), campaignId, adGroupId);
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
                return "This code example adds a page feed to specify precisely which URLs to " +
                    "use with your Dynamic Search Ads campaign. To create a Dynamic Search Ads " +
                    "campaign, run AddDynamicSearchAdsCampaign.cs. To get campaigns, " +
                    "run GetCampaigns.cs.";
            }
        }

        /// <summary>
        /// Runs the code example.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="campaignId">The DSA campaign ID.</param>
        /// <param name="adGroupId">The DSA ad group ID.</param>
        public void Run(AdWordsUser user, long campaignId, long adGroupId)
        {
            string dsaPageUrlLabel = "discounts";

            // Get the page feed details. This code example creates a new feed, but you can
            // fetch and re-use an existing feed.
            DSAFeedDetails feedDetails = CreateFeed(user);
            CreateFeedMapping(user, feedDetails);
            CreateFeedItems(user, feedDetails, dsaPageUrlLabel);

            // Associate the page feed with the campaign.
            UpdateCampaignDsaSetting(user, campaignId, feedDetails.feedId);

            // Optional: Target web pages matching the feed's label in the ad group.
            AddDsaTargeting(user, adGroupId, dsaPageUrlLabel);

            Console.WriteLine("Dynamic page feed setup is complete for campaign ID '{0}'.",
                campaignId);
        }

        /// <summary>
        /// Creates the feed for DSA page URLs.
        /// </summary>
        /// <param name="user">The AdWords User.</param>
        /// <returns>The feed details.</returns>
        private static DSAFeedDetails CreateFeed(AdWordsUser user)
        {
            using (FeedService feedService =
                (FeedService) user.GetService(AdWordsService.v201809.FeedService))
            {
                // Create attributes.
                FeedAttribute urlAttribute = new FeedAttribute
                {
                    type = FeedAttributeType.URL_LIST,
                    name = "Page URL"
                };

                FeedAttribute labelAttribute = new FeedAttribute
                {
                    type = FeedAttributeType.STRING_LIST,
                    name = "Label"
                };

                // Create the feed.
                Feed sitelinksFeed = new Feed
                {
                    name = "DSA Feed " + ExampleUtilities.GetRandomString(),
                    attributes = new FeedAttribute[]
                    {
                        urlAttribute,
                        labelAttribute
                    },
                    origin = FeedOrigin.USER
                };

                // Create operation.
                FeedOperation operation = new FeedOperation
                {
                    operand = sitelinksFeed,
                    @operator = Operator.ADD
                };

                try
                {
                    // Add the feed.
                    FeedReturnValue result = feedService.mutate(new FeedOperation[]
                    {
                        operation
                    });

                    Feed savedFeed = result.value[0];
                    return new DSAFeedDetails
                    {
                        feedId = savedFeed.id,
                        urlAttributeId = savedFeed.attributes[0].id,
                        labelAttributeId = savedFeed.attributes[1].id,
                    };
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException("Failed to create DSA feed.", e);
                }
            }
        }

        /// <summary>
        /// Creates the feed mapping for DSA page feeds.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="feedDetails">The feed details.</param>
        private static void CreateFeedMapping(AdWordsUser user, DSAFeedDetails feedDetails)
        {
            using (FeedMappingService feedMappingService =
                (FeedMappingService) user.GetService(AdWordsService.v201809.FeedMappingService))
            {
                // Map the FeedAttributeIds to the fieldId constants.
                AttributeFieldMapping urlFieldMapping = new AttributeFieldMapping
                {
                    feedAttributeId = feedDetails.urlAttributeId,
                    fieldId = DSA_PAGE_URLS_FIELD_ID
                };

                AttributeFieldMapping labelFieldMapping = new AttributeFieldMapping
                {
                    feedAttributeId = feedDetails.labelAttributeId,
                    fieldId = DSA_LABEL_FIELD_ID
                };

                // Create the FieldMapping and operation.
                FeedMapping feedMapping = new FeedMapping
                {
                    criterionType = DSA_PAGE_FEED_CRITERION_TYPE,
                    feedId = feedDetails.feedId,
                    attributeFieldMappings = new AttributeFieldMapping[]
                    {
                        urlFieldMapping,
                        labelFieldMapping
                    }
                };

                FeedMappingOperation operation = new FeedMappingOperation
                {
                    operand = feedMapping,
                    @operator = Operator.ADD
                };

                try
                {
                    // Add the field mapping.
                    feedMappingService.mutate(new FeedMappingOperation[]
                    {
                        operation
                    });
                    return;
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException("Failed to create feed mapping.", e);
                }
            }
        }

        /// <summary>
        /// Creates the page URLs in the DSA page feed.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="feedDetails">The feed details.</param>
        /// <param name="labelName">The pagefeed url label.</param>
        private static void CreateFeedItems(AdWordsUser user, DSAFeedDetails feedDetails,
            string labelName)
        {
            using (FeedItemService feedItemService =
                (FeedItemService) user.GetService(AdWordsService.v201809.FeedItemService))
            {
                FeedItemOperation[] operations = new FeedItemOperation[]
                {
                    CreateDsaUrlAddOperation(feedDetails,
                        "http://www.example.com/discounts/rental-cars", labelName),
                    CreateDsaUrlAddOperation(feedDetails,
                        "http://www.example.com/discounts/hotel-deals", labelName),
                    CreateDsaUrlAddOperation(feedDetails,
                        "http://www.example.com/discounts/flight-deals", labelName),
                };
                feedItemService.mutate(operations);
            }
        }

        /// <summary>
        /// Creates the DSA URL add operation.
        /// </summary>
        /// <param name="details">The page feed details.</param>
        /// <param name="url">The DSA page feed URL.</param>
        /// <param name="label">DSA page feed label.</param>
        /// <returns>The DSA URL add operation.</returns>
        private static FeedItemOperation CreateDsaUrlAddOperation(DSAFeedDetails details,
            string url, string label)
        {
            // Create the FeedItemAttributeValues for our text values.
            FeedItemAttributeValue urlAttributeValue = new FeedItemAttributeValue
            {
                feedAttributeId = details.urlAttributeId,

                // See https://support.google.com/adwords/answer/7166527 for page feed URL
                // recommendations and rules.
                stringValues = new string[]
                {
                    url
                }
            };

            FeedItemAttributeValue labelAttributeValue = new FeedItemAttributeValue
            {
                feedAttributeId = details.labelAttributeId,
                stringValues = new string[]
                {
                    label
                }
            };

            // Create the feed item and operation.
            FeedItem item = new FeedItem
            {
                feedId = details.feedId,

                attributeValues = new FeedItemAttributeValue[]
                {
                    urlAttributeValue,
                    labelAttributeValue
                }
            };

            FeedItemOperation operation = new FeedItemOperation
            {
                operand = item,
                @operator = Operator.ADD
            };

            return operation;
        }

        /// <summary>
        /// Updates the campaign DSA setting to add DSA pagefeeds.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="campaignId">The Campaign ID.</param>
        /// <param name="feedId">The page feed ID.</param>
        private static void UpdateCampaignDsaSetting(AdWordsUser user, long campaignId, long feedId)
        {
            using (CampaignService campaignService =
                (CampaignService) user.GetService(AdWordsService.v201809.CampaignService))
            {
                Selector selector = new Selector()
                {
                    fields = new string[]
                    {
                        Campaign.Fields.Id,
                        Campaign.Fields.Settings
                    },
                    predicates = new Predicate[]
                    {
                        Predicate.Equals(Campaign.Fields.Id, campaignId)
                    },
                    paging = Paging.Default
                };

                CampaignPage page = campaignService.get(selector);

                if (page == null || page.entries == null || page.entries.Length == 0)
                {
                    throw new System.ApplicationException(
                        string.Format("Failed to retrieve campaign with ID = {0}.", campaignId));
                }

                Campaign campaign = page.entries[0];

                if (campaign.settings == null)
                {
                    throw new System.ApplicationException("This is not a DSA campaign.");
                }

                DynamicSearchAdsSetting dsaSetting = null;
                Setting[] campaignSettings = campaign.settings;

                for (int i = 0; i < campaign.settings.Length; i++)
                {
                    Setting setting = campaignSettings[i];
                    if (setting is DynamicSearchAdsSetting)
                    {
                        dsaSetting = (DynamicSearchAdsSetting) setting;
                        break;
                    }
                }

                if (dsaSetting == null)
                {
                    throw new System.ApplicationException("This is not a DSA campaign.");
                }

                // Use a page feed to specify precisely which URLs to use with your
                // Dynamic Search Ads.
                dsaSetting.pageFeed = new PageFeed()
                {
                    feedIds = new long[]
                    {
                        feedId
                    },
                };

                // Optional: Specify whether only the supplied URLs should be used with your
                // Dynamic Search Ads.
                dsaSetting.useSuppliedUrlsOnly = true;

                Campaign campaignToUpdate = new Campaign
                {
                    id = campaignId,
                    settings = campaignSettings
                };

                CampaignOperation operation = new CampaignOperation
                {
                    operand = campaignToUpdate,
                    @operator = Operator.SET
                };

                try
                {
                    CampaignReturnValue retval = campaignService.mutate(new CampaignOperation[]
                    {
                        operation
                    });
                    Campaign updatedCampaign = retval.value[0];
                    Console.WriteLine(
                        "DSA page feed for campaign ID '{0}' was updated with feed ID '{1}'.",
                        updatedCampaign.id, feedId);
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException("Failed to set page feed for campaign.",
                        e);
                }
            }

        }

        /// <summary>
        /// Set custom targeting for the page feed URLs based on a list of labels.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="adGroupId">Ad group ID.</param>
        /// <param name="labelName">The label name.</param>
        /// <returns>The newly created webpage criterion.</returns>
        private static BiddableAdGroupCriterion AddDsaTargeting(AdWordsUser user, long adGroupId,
            string labelName)
        {
            using (AdGroupCriterionService adGroupCriterionService =
                (AdGroupCriterionService) user.GetService(AdWordsService.v201809
                    .AdGroupCriterionService))
            {
                // Create a webpage criterion.
                Webpage webpage = new Webpage();

                WebpageParameter parameter = new WebpageParameter
                {
                    criterionName = "Test criterion"
                };
                webpage.parameter = parameter;

                // Add a condition for label=specified_label_name.
                WebpageCondition condition = new WebpageCondition
                {
                    operand = WebpageConditionOperand.CUSTOM_LABEL,
                    argument = labelName
                };
                parameter.conditions = new WebpageCondition[]
                {
                    condition
                };

                BiddableAdGroupCriterion criterion = new BiddableAdGroupCriterion
                {
                    adGroupId = adGroupId,
                    criterion = webpage
                };

                // Set a custom bid for this criterion.
                BiddingStrategyConfiguration biddingStrategyConfiguration =
                    new BiddingStrategyConfiguration
                    {
                        bids = new Bids[]
                        {
                            new CpcBid()
                            {
                                bid = new Money()
                                {
                                    microAmount = 1500000
                                }
                            }
                        }
                    };

                criterion.biddingStrategyConfiguration = biddingStrategyConfiguration;

                AdGroupCriterionOperation operation = new AdGroupCriterionOperation
                {
                    operand = criterion,
                    @operator = Operator.ADD
                };

                try
                {
                    AdGroupCriterionReturnValue retval = adGroupCriterionService.mutate(
                        new AdGroupCriterionOperation[]
                        {
                            operation
                        });
                    BiddableAdGroupCriterion newCriterion =
                        (BiddableAdGroupCriterion) retval.value[0];

                    Console.WriteLine(
                        "Web page criterion with ID = {0} and status = {1} was created.",
                        newCriterion.criterion.id, newCriterion.userStatus);
                    return newCriterion;
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException(
                        "Failed to create webpage criterion for " + "custom page feed label.", e);
                }
            }
        }

    }
}
