// Copyright 2013, Google Inc. All Rights Reserved.
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

// Author: api.anash@gmail.com (Anash P. Oommen)

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201306;
using Google.Api.Ads.Common.Util;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201306 {
  /// <summary>
  /// This code example migrates legacy sitelinks to upgraded sitelinks for a
  /// given list of campaigns. The campaigns must be upgraded to enhanced
  /// campaigns before you can run this example. To upgrade a campaign to
  /// enhanced, run CampaignManagement/SetCampaignEnhanced.cs. To get all
  /// campaigns, run BasicOperations/GetCampaigns.cs.
  ///
  /// Tags: CampaignAdExtensionService.get, CampaignAdExtensionService.mutate
  /// Tags: FeedService.mutate, FeedItemService.mutate
  /// Tags: FeedMappingService.mutate, CampaignFeedService.mutate
  /// </summary>
  public class UpgradeLegacySitelinks : ExampleBase {
    /// <summary>
    /// Data structure to hold details about a Sitelink feed.
    /// </summary>
    private class SiteLinksFeed {
      /// <summary>
      /// Feed id.
      /// </summary>
      long siteLinksFeedId;

      /// <summary>
      /// Attribute id for sitelink text.
      /// </summary>
      long linkTextFeedAttributeId;

      /// <summary>
      /// Attribute id for sitelink url.
      /// </summary>
      long linkUrlFeedAttributeId;

      /// <summary>
      /// Gets or sets the attribute id for sitelink text.
      /// </summary>
      public long LinkTextFeedAttributeId {
        get {
          return linkTextFeedAttributeId;
        }
        set {
          linkTextFeedAttributeId = value;
        }
      }

      /// <summary>
      /// Gets or sets the site links feed id.
      /// </summary>
      public long SiteLinksFeedId {
        get {
          return siteLinksFeedId;
        }
        set {
          siteLinksFeedId = value;
        }
      }

      /// <summary>
      /// Gets or sets the attribute id for sitelink url.
      /// </summary>
      public long LinkUrlFeedAttributeId {
        get {
          return linkUrlFeedAttributeId;
        }
        set {
          linkUrlFeedAttributeId = value;
        }
      }
    }

    // See https://developers.google.com/adwords/api/docs/appendix/placeholders
    // for a list of all the placeholder types and fields.
    private const int PLACEHOLDER_SITELINKS = 1;

    // See https://developers.google.com/adwords/api/docs/appendix/placeholders
    // for a list of all the placeholder types and fields.
    private const int PLACEHOLDER_FIELD_SITELINK_LINK_TEXT = 1;
    private const int PLACEHOLDER_FIELD_SITELINK_URL = 2;

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      UpgradeLegacySitelinks codeExample = new UpgradeLegacySitelinks();
      Console.WriteLine(codeExample.Description);
      try {
        long campaignId = long.Parse("INSERT_CAMPAIGN_ID_HERE");
        codeExample.Run(new AdWordsUser(), new long[] {campaignId});
      } catch (Exception ex) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(ex));
      }
    }

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example migrates legacy sitelinks to upgraded sitelinks for a given " +
            "list of campaigns. The campaigns must be upgraded to enhanced campaigns before " +
            "you can run this example. To upgrade a campaign to enhanced, run " +
            "CampaignManagement/SetCampaignEnhanced.cs. To get all campaigns, run " +
            "BasicOperations/GetCampaigns.cs.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="campaignIds">Ids of the campaign for which sitelinks are
    /// upgraded.</param>
    public void Run(AdWordsUser user, long[] campaignIds) {
      // Get the CampaignAdExtensionService.
      CampaignAdExtensionService campaignAdExtensionService =
          (CampaignAdExtensionService) user.GetService(
              AdWordsService.v201306.CampaignAdExtensionService);
      // Get the FeedMappingService.
      FeedMappingService feedMappingService = (FeedMappingService) user.GetService(
          AdWordsService.v201306.FeedMappingService);
      // Get the FeedService.
      FeedService feedService = (FeedService) user.GetService(AdWordsService.v201306.FeedService);
      // Get the FeedItemService.
      FeedItemService feedItemService = (FeedItemService) user.GetService(
          AdWordsService.v201306.FeedItemService);
      // Get the CampaignFeedService.
      CampaignFeedService campaignFeedService = (CampaignFeedService) user.GetService(
          AdWordsService.v201306.CampaignFeedService);

      // Try to retrieve an existing feed that has been mapped for use with
      // sitelinks. if multiple such feeds exist, the first matching feed is
      // retrieved. You could modify this code example to retrieve all the feeds
      // and pick the appropriate feed based on user input.
      SiteLinksFeed siteLinksFeed = getExistingFeed(feedMappingService);

      if (siteLinksFeed == null) {
        // Create a feed for storing sitelinks.
        siteLinksFeed = createSiteLinksFeed(feedService);

        // Map the feed for using with sitelinks.
        createSiteLinksFeedMapping(feedMappingService, siteLinksFeed);
      }

      foreach (long campaignId in campaignIds) {
        // Get legacy sitelinks for the campaign.
        CampaignAdExtension extension =
            getLegacySitelinksForCampaign(campaignAdExtensionService, campaignId);
        if (extension != null) {
          // Get the sitelinks.
          Sitelink[] legacySitelinks =
              ((SitelinksExtension) extension.adExtension).sitelinks;

          // Add the sitelinks to the feed.
          List<long> siteLinkFeedItemIds =
              createSiteLinkFeedItems(feedItemService, siteLinksFeed, legacySitelinks);

          // Associate feeditems to the campaign.
          associateSitelinkFeedItemsWithCampaign(
              campaignFeedService, siteLinksFeed, siteLinkFeedItemIds, campaignId);

          // Once the upgraded sitelinks are added to a campaign, the legacy
          // sitelinks will stop serving. You can delete the legacy sitelinks
          // once you have verified that the migration went fine. In case the
          // migration didn't succeed, you can roll back the migration by deleting
          // the CampaignFeed you created in the previous step.
          deleteLegacySitelinks(campaignAdExtensionService, extension);
        }
      }
    }

    /// <summary>
    /// Retrieve an existing feed that is mapped to hold sitelinks. The first
    /// active sitelinks feed is retrieved by this method.
    /// </summary>
    /// <param name="feedMappingService">The feed mapping service.</param>
    /// <returns>A SiteLinksFeed if a feed is found, or null otherwise.</returns>
    private static SiteLinksFeed getExistingFeed(FeedMappingService feedMappingService) {
      Selector selector = new Selector();
      selector.fields = new string[] {"FeedId", "FeedMappingId", "PlaceholderType", "Status",
        "AttributeFieldMappings"};

      Predicate placeHolderPredicate = new Predicate();
      placeHolderPredicate.field = "PlaceholderType";
      placeHolderPredicate.@operator = PredicateOperator.EQUALS;
      placeHolderPredicate.values = new string[] {PLACEHOLDER_SITELINKS.ToString()};

      Predicate statusPredicate = new Predicate();
      statusPredicate.field = "Status";
      statusPredicate.@operator = PredicateOperator.EQUALS;
      statusPredicate.values = new string[] {"ACTIVE"};

      selector.predicates = new Predicate[] {placeHolderPredicate, statusPredicate};

      FeedMappingPage page = feedMappingService.get(selector);

      if (page != null && page.entries != null && page.entries.Length > 0) {
        foreach (FeedMapping feedMapping in page.entries) {
          long? feedId = feedMapping.feedId;
          long? textAttributeId = null;
          long? urlAttributeId = null;
          foreach (AttributeFieldMapping attributeMapping in feedMapping.attributeFieldMappings) {
            if (attributeMapping.fieldId == PLACEHOLDER_FIELD_SITELINK_LINK_TEXT) {
              textAttributeId = attributeMapping.feedAttributeId;
            } else if (attributeMapping.fieldId == PLACEHOLDER_FIELD_SITELINK_URL) {
              urlAttributeId = attributeMapping.feedAttributeId;
            }
          }

          if (feedId != null && textAttributeId != null && urlAttributeId != null) {
            SiteLinksFeed siteLinksFeed = new SiteLinksFeed();
            siteLinksFeed.SiteLinksFeedId = feedId.Value;
            siteLinksFeed.LinkTextFeedAttributeId = textAttributeId.Value;
            siteLinksFeed.LinkUrlFeedAttributeId = urlAttributeId.Value;
            return siteLinksFeed;
          }
        }
      }
      return null;
    }

    /// <summary>
    /// Create a feed for holding upgraded sitelinks.
    /// </summary>
    /// <param name="feedService">The feed service.</param>
    /// <returns>A SiteLinksFeed for holding the sitelinks.</returns>
    private static SiteLinksFeed createSiteLinksFeed(FeedService feedService) {
      SiteLinksFeed siteLinksData = new SiteLinksFeed();

      // Create attributes.
      FeedAttribute textAttribute = new FeedAttribute();
      textAttribute.type = FeedAttributeType.STRING;
      textAttribute.name = "Link Text";
      FeedAttribute urlAttribute = new FeedAttribute();
      urlAttribute.type = FeedAttributeType.URL;
      urlAttribute.name = "Link URL";

      // Create the feed.
      Feed siteLinksFeed = new Feed();
      siteLinksFeed.name = "Feed For Sitelinks";
      siteLinksFeed.attributes = new FeedAttribute[] {textAttribute, urlAttribute};
      siteLinksFeed.origin = FeedOrigin.USER;

      // Create operation.
      FeedOperation operation = new FeedOperation();
      operation.operand = siteLinksFeed;
      operation.@operator = Operator.ADD;

      // Add the feed.
      FeedReturnValue result = feedService.mutate(new FeedOperation[] {operation});

      Feed savedFeed = result.value[0];
      siteLinksData.SiteLinksFeedId = savedFeed.id;
      FeedAttribute[] savedAttributes = savedFeed.attributes;
      siteLinksData.LinkTextFeedAttributeId = savedAttributes[0].id;
      siteLinksData.LinkUrlFeedAttributeId = savedAttributes[1].id;
      return siteLinksData;
    }

    /// <summary>
    /// Map the feed for use with Sitelinks.
    /// </summary>
    /// <param name="feedMappingService">The feed mapping service.</param>
    /// <param name="siteLinksFeed">The feed for holding sitelinks.</param>
    private static void createSiteLinksFeedMapping(FeedMappingService feedMappingService,
        SiteLinksFeed siteLinksFeed) {
      // Map the FeedAttributeIds to the fieldId constants.
      AttributeFieldMapping linkTextFieldMapping = new AttributeFieldMapping();
      linkTextFieldMapping.feedAttributeId = siteLinksFeed.LinkTextFeedAttributeId;
      linkTextFieldMapping.fieldId = PLACEHOLDER_FIELD_SITELINK_LINK_TEXT;
      AttributeFieldMapping linkUrlFieldMapping = new AttributeFieldMapping();
      linkUrlFieldMapping.feedAttributeId = siteLinksFeed.LinkUrlFeedAttributeId;
      linkUrlFieldMapping.fieldId = PLACEHOLDER_FIELD_SITELINK_URL;

      // Create the FieldMapping and operation.
      FeedMapping feedMapping = new FeedMapping();
      feedMapping.placeholderType = PLACEHOLDER_SITELINKS;
      feedMapping.feedId = siteLinksFeed.SiteLinksFeedId;
      feedMapping.attributeFieldMappings =
          new AttributeFieldMapping[] {linkTextFieldMapping, linkUrlFieldMapping};
      FeedMappingOperation operation = new FeedMappingOperation();
      operation.operand = feedMapping;
      operation.@operator = Operator.ADD;

      // Save the field mapping.
      feedMappingService.mutate(new FeedMappingOperation[] {operation});
    }

    /// <summary>
    /// Gets the legacy sitelinks for campaign.
    /// </summary>
    /// <param name="campaignExtensionService">The campaign extension service.</param>
    /// <param name="campaignId">The campaign id.</param>
    /// <returns>The CampaignAdExtension that contains the legacy sitelinks, or
    /// null if there are no legacy sitelinks in this campaign.</returns>
    private static CampaignAdExtension getLegacySitelinksForCampaign(
        CampaignAdExtensionService campaignExtensionService, long campaignId) {
      // Create the selector.
      Selector selector = new Selector();
      selector.fields = new string[] {"CampaignId", "AdExtensionId", "Status", "DisplayText",
        "DestinationUrl"};

      // Filter the results for specified campaign id.
      Predicate campaignPredicate = new Predicate();
      campaignPredicate.@operator = PredicateOperator.EQUALS;
      campaignPredicate.field = "CampaignId";
      campaignPredicate.values = new string[] {campaignId.ToString()};

      // Filter the results for active campaign ad extensions. You may add
      // additional filtering conditions here as required.
      Predicate statusPredicate = new Predicate();
      statusPredicate.@operator = PredicateOperator.EQUALS;
      statusPredicate.field = "Status";
      statusPredicate.values = new string[] {CampaignAdExtensionStatus.ACTIVE.ToString()};

      // Filter for sitelinks ad extension type.
      Predicate typePredicate = new Predicate();
      typePredicate.@operator = PredicateOperator.EQUALS;
      typePredicate.field = "AdExtensionType";
      typePredicate.values = new string[] {"SITELINKS_EXTENSION"};

      selector.predicates = new Predicate[] {campaignPredicate, statusPredicate, typePredicate};

      CampaignAdExtensionPage page = campaignExtensionService.get(selector);
      if (page.entries != null && page.entries.Length > 0) {
        return page.entries[0];
      } else {
        return null;
      }
    }

    /// <summary>
    /// Add legacy sitelinks to the sitelinks feed.
    /// </summary>
    /// <param name="feedItemService">The feed item service.</param>
    /// <param name="siteLinksFeed">The feed for adding sitelinks.</param>
    /// <param name="sitelinks">The list of legacy sitelinks to be added to the
    /// feed.</param>
    /// <returns>The list of feeditems that were added to the feed.</returns>
    private static List<long> createSiteLinkFeedItems(FeedItemService feedItemService,
        SiteLinksFeed siteLinksFeed, Sitelink[] sitelinks) {
      List<long> siteLinkFeedItemIds = new List<long>();

      // Create operation for adding each legacy sitelink to the sitelinks feed.
      List<FeedItemOperation> feedItemOperations = new List<FeedItemOperation>();

      foreach (Sitelink sitelink in sitelinks) {
        FeedItemOperation operation = newSiteLinkFeedItemAddOperation(
            siteLinksFeed, sitelink.displayText, sitelink.destinationUrl);
        feedItemOperations.Add(operation);
      }

      FeedItemReturnValue result = feedItemService.mutate(feedItemOperations.ToArray());

      // Retrieve the feed item ids.
      foreach (FeedItem item in result.value) {
        siteLinkFeedItemIds.Add(item.feedItemId);
      }
      return siteLinkFeedItemIds;
    }

    /// <summary>
    /// Creates a new operation for adding a feed item.
    /// </summary>
    /// <param name="siteLinksFeed">The site links feed.</param>
    /// <param name="text">The sitelinks text.</param>
    /// <param name="url">The sitelinks URL.</param>
    /// <returns>A FeedItemOperation for adding the feed item.</returns>
    private static FeedItemOperation newSiteLinkFeedItemAddOperation(
        SiteLinksFeed siteLinksFeed, string text, string url) {
      // Create the FeedItemAttributeValues for our text values.
      FeedItemAttributeValue linkTextAttributeValue = new FeedItemAttributeValue();
      linkTextAttributeValue.feedAttributeId = siteLinksFeed.LinkTextFeedAttributeId;
      linkTextAttributeValue.stringValue = text;
      FeedItemAttributeValue linkUrlAttributeValue = new FeedItemAttributeValue();
      linkUrlAttributeValue.feedAttributeId = siteLinksFeed.LinkUrlFeedAttributeId;
      linkUrlAttributeValue.stringValue = url;

      // Create the feed item and operation.
      FeedItem item = new FeedItem();
      item.feedId = siteLinksFeed.SiteLinksFeedId;
      item.attributeValues =
          new FeedItemAttributeValue[] {linkTextAttributeValue, linkUrlAttributeValue};
      FeedItemOperation operation = new FeedItemOperation();
      operation.operand = item;
      operation.@operator = Operator.ADD;
      return operation;
    }

    /// <summary>
    /// Delete legacy sitelinks from a campaign.
    /// </summary>
    /// <param name="campaignExtensionService">The campaign extension service.
    /// </param>
    /// <param name="extensionToDelete">The CampaignAdExtension that holds
    /// legacy sitelinks.</param>
    private static void deleteLegacySitelinks(CampaignAdExtensionService campaignExtensionService,
        CampaignAdExtension extensionToDelete) {
      CampaignAdExtensionOperation operation = new CampaignAdExtensionOperation();
      operation.@operator = Operator.REMOVE;
      operation.operand = extensionToDelete;

      campaignExtensionService.mutate(new CampaignAdExtensionOperation[] {operation});
    }

    /// <summary>
    /// Associates the sitelink feed items with a campaign.
    /// </summary>
    /// <param name="campaignFeedService">The campaign feed service.</param>
    /// <param name="siteLinksFeed">The feed for holding the sitelinks.</param>
    /// <param name="siteLinkFeedItemIds">The list of feed item ids to be
    /// associated with a campaign as sitelinks.</param>
    /// <param name="campaignId">The campaign id to which upgraded sitelinks are
    /// added.</param>
    private static void associateSitelinkFeedItemsWithCampaign(
        CampaignFeedService campaignFeedService, SiteLinksFeed siteLinksFeed,
        List<long> siteLinkFeedItemIds, long campaignId) {
      // Create a custom matching function that matches the given feed items to
      // the campaign.
      RequestContextOperand requestContextOperand = new RequestContextOperand();
      requestContextOperand.contextType = RequestContextOperandContextType.FEED_ITEM_ID;

      Function function = new Function();
      function.lhsOperand = new FunctionArgumentOperand[] {requestContextOperand};
      function.@operator = FunctionOperator.IN;

      List<FunctionArgumentOperand> operands = new List<FunctionArgumentOperand>();
      foreach (long feedItemId in siteLinkFeedItemIds) {
        ConstantOperand constantOperand = new ConstantOperand();
        constantOperand.longValue = feedItemId;
        constantOperand.type = ConstantOperandConstantType.LONG;
        operands.Add(constantOperand);
      }
      function.rhsOperand = operands.ToArray();

      // Create upgraded sitelinks for the campaign. Use the sitelinks feed we
      // created, and restrict feed items by matching function.
      CampaignFeed campaignFeed = new CampaignFeed();
      campaignFeed.feedId = siteLinksFeed.SiteLinksFeedId;
      campaignFeed.campaignId = campaignId;
      campaignFeed.matchingFunction = function;
      campaignFeed.placeholderTypes = new int[] {PLACEHOLDER_SITELINKS};

      CampaignFeedOperation operation = new CampaignFeedOperation();
      operation.operand = campaignFeed;
      operation.@operator = Operator.ADD;
      campaignFeedService.mutate(new CampaignFeedOperation[] {operation});
    }
  }
}
