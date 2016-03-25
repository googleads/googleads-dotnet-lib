// Copyright 2015, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdWords.v201509;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201509 {

  /// <summary>
  /// This code example migrates your feed based sitelinks at campaign level to
  /// use extension settings. To learn more about extensionsettings, see
  /// https://developers.google.com/adwords/api/docs/guides/extension-settings.
  /// To learn more about migrating Feed based extensions to extension
  /// settings, see
  /// https://developers.google.com/adwords/api/docs/guides/migrate-to-extension-settings.
  /// </summary>
  public class MigrateToExtensionSettings : ExampleBase {

    /// <summary>
    /// The placeholder type for sitelinks. See
    /// https://developers.google.com/adwords/api/docs/appendix/placeholders for
    /// the list of all supported placeholder types.
    /// </summary>
    private const int PLACEHOLDER_TYPE_SITELINKS = 1;

    /// <summary>
    /// Holds the placeholder field IDs for sitelinks. See
    /// https://developers.google.com/adwords/api/docs/appendix/placeholders for
    /// the list of all supported placeholder types.
    /// </summary>
    private class SiteLinkFields {
      public const long TEXT = 1;
      public const long URL = 2;
      public const long LINE2 = 3;
      public const long LINE3 = 4;
      public const long FINAL_URLS = 5;
      public const long FINAL_MOBILE_URLS = 6;
      public const long TRACKING_URL_TEMPLATE = 7;
    };

    /// <summary>
    /// A sitelink object read from a feed.
    /// </summary>
    private class SiteLinkFromFeed {

      /// <summary>
      /// Gets or sets the feed ID.
      /// </summary>
      public long FeedId { get; set; }

      /// <summary>
      /// Gets or sets the feed item ID.
      /// </summary>
      public long FeedItemId { get; set; }

      /// <summary>
      /// Gets or sets the sitelink text.
      /// </summary>
      public string Text { get; set; }

      /// <summary>
      /// Gets or sets the sitelink URL.
      /// </summary>
      public string Url { get; set; }

      /// <summary>
      /// Gets or sets the sitelink final URLs.
      /// </summary>
      public string[] FinalUrls { get; set; }

      /// <summary>
      /// Gets or sets the sitelink final Mobile URLs.
      /// </summary>
      public string[] FinalMobileUrls { get; set; }

      /// <summary>
      /// Gets or sets the sitelink tracking URL template.
      /// </summary>
      public string TrackingUrlTemplate { get; set; }

      /// <summary>
      /// Gets or sets the sitelink line2 description.
      /// </summary>
      public string Line2 { get; set; }

      /// <summary>
      /// Gets or sets the sitelink line3 description.
      /// </summary>
      public string Line3 { get; set; }

      /// <summary>
      /// Gets or sets the sitelink scheduling.
      /// </summary>
      public FeedItemSchedule[] Scheduling { get; set; }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      MigrateToExtensionSettings codeExample = new MigrateToExtensionSettings();
      Console.WriteLine(codeExample.Description);
      try {
        codeExample.Run(new AdWordsUser());
      } catch (Exception e) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(e));
      }
    }

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example migrates your feed based sitelinks at campaign level to " +
            "use extension settings. To learn more about extensionsettings, see " +
            "https://developers.google.com/adwords/api/docs/guides/extension-settings. To learn " +
            "more about migrating Feed based extensions to extension settings, see " +
            "https://developers.google.com/adwords/api/docs/guides/migrate-to-extension-settings.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    public void Run(AdWordsUser user) {
      // Get all the feeds from the user account.
      Feed[] feeds = GetFeeds(user);

      foreach (Feed feed in feeds) {
        // Retrieve all the sitelinks from the current feed.
        Dictionary<long, SiteLinkFromFeed> feedItems = GetSiteLinksFromFeed(user, feed.id);

        // Get all the instances where a sitelink from this feed has been added
        // to a campaign.
        CampaignFeed[] campaignFeeds = GetCampaignFeeds(user, feed, PLACEHOLDER_TYPE_SITELINKS);

        if (campaignFeeds != null) {
          HashSet<long> allFeedItemsToDelete = new HashSet<long>();

          foreach (CampaignFeed campaignFeed in campaignFeeds) {
            // Retrieve the sitelinks that have been associated with this
            // campaign.
            List<long> feedItemIds = GetFeedItemsForCampaign(campaignFeed);
            ExtensionSettingPlatform platformRestrictions = GetPlatformRestrictionsForCampaign(
                campaignFeed);

            if (feedItemIds.Count == 0) {
              Console.WriteLine("Migration skipped for campaign feed with campaign ID {0} " +
                  "and feed ID {1} because no mapped feed item IDs were found in the campaign " +
                  "feed's matching function.", campaignFeed.campaignId, campaignFeed.feedId);
            } else {
              // Delete the campaign feed that associates the sitelinks from the
              // feed to the campaign.
              DeleteCampaignFeed(user, campaignFeed);

              // Create extension settings instead of sitelinks.
              CreateExtensionSetting(user, feedItems, campaignFeed.campaignId, feedItemIds,
                  platformRestrictions);

              // Mark the sitelinks from the feed for deletion.
              allFeedItemsToDelete.UnionWith(feedItemIds);
            }
          }
          // Delete all the sitelinks from the feed.
          DeleteOldFeedItems(user, new List<long>(allFeedItemsToDelete), feed.id);
        }
      }
    }

    /// <summary>
    /// Gets the site links from a feed.
    /// </summary>
    /// <param name="user">The user that owns the feed.</param>
    /// <param name="feedId">The feed ID.</param>
    /// <returns>A dictionary of sitelinks from the feed, with key as the feed
    /// item ID, and value as the sitelink.</returns>
    private Dictionary<long, SiteLinkFromFeed> GetSiteLinksFromFeed(AdWordsUser user,
        long feedId) {
      Dictionary<long, SiteLinkFromFeed> siteLinks = new Dictionary<long, SiteLinkFromFeed>();

      // Retrieve all the feed items from the feed.
      FeedItem[] feedItems = GetFeedItems(user, feedId);

      // Retrieve the feed's attribute mapping.
      Dictionary<long, HashSet<long>> feedMappings = GetFeedMapping(user, feedId,
          PLACEHOLDER_TYPE_SITELINKS);

      if (feedItems != null) {
        foreach (FeedItem feedItem in feedItems) {
          SiteLinkFromFeed sitelinkFromFeed = new SiteLinkFromFeed() {
            FeedId = feedItem.feedId,
            FeedItemId = feedItem.feedItemId
          };

          foreach (FeedItemAttributeValue attributeValue in feedItem.attributeValues) {
            // This attribute hasn't been mapped to a field.
            if (!feedMappings.ContainsKey(attributeValue.feedAttributeId)) {
              continue;
            }
            // Get the list of all the fields to which this attribute has been mapped.
            foreach (long fieldId in feedMappings[attributeValue.feedAttributeId]) {
              // Read the appropriate value depending on the ID of the mapped
              // field.
              switch (fieldId) {
                case SiteLinkFields.TEXT:
                  sitelinkFromFeed.Text = attributeValue.stringValue;
                  break;

                case SiteLinkFields.URL:
                  sitelinkFromFeed.Url = attributeValue.stringValue;
                  break;

                case SiteLinkFields.FINAL_URLS:
                  sitelinkFromFeed.FinalUrls = attributeValue.stringValues;
                  break;

                case SiteLinkFields.FINAL_MOBILE_URLS:
                  sitelinkFromFeed.FinalMobileUrls = attributeValue.stringValues;
                  break;

                case SiteLinkFields.TRACKING_URL_TEMPLATE:
                  sitelinkFromFeed.TrackingUrlTemplate = attributeValue.stringValue;
                  break;

                case SiteLinkFields.LINE2:
                  sitelinkFromFeed.Line2 = attributeValue.stringValue;
                  break;

                case SiteLinkFields.LINE3:
                  sitelinkFromFeed.Line3 = attributeValue.stringValue;
                  break;
              }
            }
          }

          sitelinkFromFeed.Scheduling = feedItem.scheduling;

          siteLinks.Add(feedItem.feedItemId, sitelinkFromFeed);
        }
      }
      return siteLinks;
    }

    /// <summary>
    /// Gets the feed mapping for a feed.
    /// </summary>
    /// <param name="user">The user that owns the feed.</param>
    /// <param name="feedId">The feed ID.</param>
    /// <param name="placeHolderType">Type of the place holder for which feed
    /// mappings should be retrieved.</param>
    /// <returns>A dictionary, with key as the feed attribute ID, and value as
    /// the set of all fields which the attribute has a mapping to.</returns>
    private Dictionary<long, HashSet<long>> GetFeedMapping(AdWordsUser user, long feedId,
    long placeHolderType) {
      FeedMappingService feedMappingService = (FeedMappingService) user.GetService(
          AdWordsService.v201509.FeedMappingService);
      FeedMappingPage page = feedMappingService.query(string.Format("SELECT FeedMappingId, " +
          "AttributeFieldMappings where FeedId='{0}' and PlaceholderType={1} and Status='ENABLED'",
          feedId, placeHolderType));

      Dictionary<long, HashSet<long>> attributeMappings = new Dictionary<long, HashSet<long>>();

      if (page.entries != null) {
        // Normally, a feed attribute is mapped only to one field. However,
        // you may map it to more than one field if needed.
        foreach (FeedMapping feedMapping in page.entries) {
          foreach (AttributeFieldMapping attributeMapping in feedMapping.attributeFieldMappings) {
            if (!attributeMappings.ContainsKey(attributeMapping.feedAttributeId)) {
              attributeMappings[attributeMapping.feedAttributeId] = new HashSet<long>();
            }
            attributeMappings[attributeMapping.feedAttributeId].Add(attributeMapping.fieldId);
          }
        }
      }
      return attributeMappings;
    }

    /// <summary>
    /// Gets the feeds.
    /// </summary>
    /// <param name="user">The user for which feeds are retrieved.</param>
    /// <returns>The list of feeds.</returns>
    private Feed[] GetFeeds(AdWordsUser user) {
      FeedService feedService = (FeedService) user.GetService(AdWordsService.v201509.FeedService);
      FeedPage page = feedService.query("SELECT Id, Name, Attributes where " +
          "Origin='USER' and FeedStatus='ENABLED'");
      return page.entries;
    }

    /// <summary>
    /// Gets the feed items in a feed.
    /// </summary>
    /// <param name="user">The user that owns the feed.</param>
    /// <param name="feedId">The feed ID.</param>
    /// <returns>The list of feed items in the feed.</returns>
    private FeedItem[] GetFeedItems(AdWordsUser user, long feedId) {
      FeedItemService feedItemService = (FeedItemService) user.GetService(
          AdWordsService.v201509.FeedItemService);
      FeedItemPage page = feedItemService.query(string.Format("Select FeedItemId, " +
          "AttributeValues, Scheduling  where Status = 'ENABLED' and FeedId = '{0}'", feedId));
      return page.entries;
    }

    /// <summary>
    /// Deletes the old feed items for which extension settings have been
    /// created.
    /// </summary>
    /// <param name="user">The user that owns the feed items.</param>
    /// <param name="feedItemIds">IDs of the feed items to be removed.</param>
    /// <param name="feedId">ID of the feed that holds the feed items.</param>
    private void DeleteOldFeedItems(AdWordsUser user, List<long> feedItemIds, long feedId) {
      if (feedItemIds.Count == 0) {
        return;
      }
      List<FeedItemOperation> operations = new List<FeedItemOperation>();
      foreach (long feedItemId in feedItemIds) {
        FeedItemOperation operation = new FeedItemOperation() {
          @operator = Operator.REMOVE,
          operand = new FeedItem() {
            feedItemId = feedItemId,
            feedId = feedId
          }
        };
        operations.Add(operation);
      }
      FeedItemService feedItemService = (FeedItemService) user.GetService(
          AdWordsService.v201509.FeedItemService);
      feedItemService.mutate(operations.ToArray());
      return;
    }

    /// <summary>
    /// Creates the extension setting fo a list of feed items.
    /// </summary>
    /// <param name="user">The user for which extension settings are created.
    /// </param>
    /// <param name="feedItems">The list of all feed items.</param>
    /// <param name="campaignId">ID of the campaign to which extension settings
    /// are added.</param>
    /// <param name="feedItemIds">IDs of the feed items for which extension
    /// settings should be created.</param>
    /// <param name="platformRestrictions">The platform restrictions for the
    /// extension setting.</param>
    private static void CreateExtensionSetting(AdWordsUser user, Dictionary<long,
        SiteLinkFromFeed> feedItems, long campaignId, List<long> feedItemIds,
        ExtensionSettingPlatform platformRestrictions) {
      CampaignExtensionSetting extensionSetting = new CampaignExtensionSetting() {
        campaignId = campaignId,
        extensionType = FeedType.SITELINK,
        extensionSetting = new ExtensionSetting() {
        }
      };

      List<ExtensionFeedItem> extensionFeedItems = new List<ExtensionFeedItem>();

      foreach (long feedItemId in feedItemIds) {
        SiteLinkFromFeed feedItem = feedItems[feedItemId];
        SitelinkFeedItem newFeedItem = new SitelinkFeedItem() {
          sitelinkText = feedItem.Text,
          sitelinkUrl = feedItem.Url,
          sitelinkFinalUrls = new UrlList() {
            urls = feedItem.FinalUrls
          },
          sitelinkFinalMobileUrls = new UrlList() {
            urls = feedItem.FinalMobileUrls
          },
          sitelinkTrackingUrlTemplate = feedItem.TrackingUrlTemplate,
          sitelinkLine2 = feedItem.Line2,
          sitelinkLine3 = feedItem.Line3,
          scheduling = feedItem.Scheduling
        };

        extensionFeedItems.Add(newFeedItem);
      }
      extensionSetting.extensionSetting.extensions = extensionFeedItems.ToArray();
      extensionSetting.extensionSetting.platformRestrictions = platformRestrictions;
      extensionSetting.extensionType = FeedType.SITELINK;

      CampaignExtensionSettingService campaignExtensionSettingService =
          (CampaignExtensionSettingService) user.GetService(
              AdWordsService.v201509.CampaignExtensionSettingService);
      CampaignExtensionSettingOperation operation = new CampaignExtensionSettingOperation() {
        operand = extensionSetting,
        @operator = Operator.ADD
      };

      campaignExtensionSettingService.mutate(new CampaignExtensionSettingOperation[] { operation });
      return;
    }

    /// <summary>
    /// Deletes a campaign feed.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="campaignFeed">The campaign feed.</param>
    /// <returns></returns>
    private CampaignFeed DeleteCampaignFeed(AdWordsUser user, CampaignFeed campaignFeed) {
      CampaignFeedService campaignFeedService = (CampaignFeedService) user.GetService(
          AdWordsService.v201509.CampaignFeedService);

      CampaignFeedOperation operation = new CampaignFeedOperation() {
        operand = campaignFeed,
        @operator = Operator.REMOVE
      };

      return campaignFeedService.mutate(new CampaignFeedOperation[] { operation }).value[0];
    }

    /// <summary>
    /// Gets the platform restrictions for sitelinks in a campaign.
    /// </summary>
    /// <param name="campaignFeed">The campaign feed.</param>
    /// <returns>The platform restrictions.</returns>
    private ExtensionSettingPlatform GetPlatformRestrictionsForCampaign(
          CampaignFeed campaignFeed) {
      string platformRestrictions = "NONE";

      if (campaignFeed.matchingFunction.@operator == FunctionOperator.AND) {
        foreach (FunctionArgumentOperand argument in campaignFeed.matchingFunction.lhsOperand) {
          // Check if matchingFunction is of the form EQUALS(CONTEXT.DEVICE, 'Mobile').
          if (argument is FunctionOperand) {
            FunctionOperand operand = (argument as FunctionOperand);
            if (operand.value.@operator == FunctionOperator.EQUALS) {
              RequestContextOperand requestContextOperand = operand.value.lhsOperand[0] as
                  RequestContextOperand;
              if (requestContextOperand != null && requestContextOperand.contextType ==
                  RequestContextOperandContextType.DEVICE_PLATFORM) {
                platformRestrictions = (operand.value.rhsOperand[0] as ConstantOperand)
                      .stringValue;
              }
            }
          }
        }
      }

      return (ExtensionSettingPlatform) Enum.Parse(typeof(ExtensionSettingPlatform),
              platformRestrictions, true);
    }

    /// <summary>
    /// Gets the list of feed items that are used by a campaign through a given
    /// campaign feed.
    /// </summary>
    /// <param name="campaignFeed">The campaign feed.</param>
    /// <returns>The list of feed items.</returns>
    private List<long> GetFeedItemsForCampaign(CampaignFeed campaignFeed) {
      List<long> feedItems = new List<long>();

      switch (campaignFeed.matchingFunction.@operator) {
        case FunctionOperator.IN:
          // Check if matchingFunction is of the form IN(FEED_ITEM_ID,{xxx,xxx}).
          // Extract feedItems if applicable.
          feedItems.AddRange(GetFeedItemsFromArgument(campaignFeed.matchingFunction));

          break;

        case FunctionOperator.AND:
          // Check each condition.

          foreach (FunctionArgumentOperand argument in campaignFeed.matchingFunction.lhsOperand) {
            // Check if matchingFunction is of the form IN(FEED_ITEM_ID,{xxx,xxx}).
            // Extract feedItems if applicable.
            if (argument is FunctionOperand) {
              FunctionOperand operand = (argument as FunctionOperand);
              if (operand.value.@operator == FunctionOperator.IN) {
                feedItems.AddRange(GetFeedItemsFromArgument(operand.value));
              }
            }
          }
          break;

        default:
          // There are no other matching functions involving feeditem ids.
          break;
      }

      return feedItems;
    }

    private List<long> GetFeedItemsFromArgument(Function function) {
      List<long> feedItems = new List<long>();
      if (function.lhsOperand.Length == 1) {
        RequestContextOperand requestContextOperand = function.lhsOperand[0] as
            RequestContextOperand;
        if (requestContextOperand != null && requestContextOperand.contextType ==
            RequestContextOperandContextType.FEED_ITEM_ID) {
          foreach (ConstantOperand argument in function.rhsOperand) {
            feedItems.Add(argument.longValue);
          }
        }
      }
      return feedItems;
    }

    /// <summary>
    /// Gets the campaignfeeds that use a particular feed.
    /// </summary>
    /// <param name="user">The user that owns the feed.</param>
    /// <param name="feed">The feed for which campaign feeds should be
    /// retrieved.</param>
    /// <param name="placeholderType">The type of placeholder to restrict
    /// search.</param>
    /// <returns>The list of campaignfeeds.</returns>
    private CampaignFeed[] GetCampaignFeeds(AdWordsUser user, Feed feed, int placeholderType) {
      CampaignFeedService campaignFeedService = (CampaignFeedService) user.GetService(
          AdWordsService.v201509.CampaignFeedService);

      CampaignFeedPage page = campaignFeedService.query(string.Format(
          "SELECT CampaignId, MatchingFunction, PlaceholderTypes where Status='ENABLED' " +
          "and FeedId = '{0}' and PlaceholderTypes CONTAINS_ANY[{1}]", feed.id, placeholderType));
      return page.entries;
    }
  }
}
