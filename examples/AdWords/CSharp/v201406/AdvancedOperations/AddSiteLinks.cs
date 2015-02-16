// Copyright 2014, Google Inc. All Rights Reserved.
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

// Author: thagikura@gmail.com (Takeshi Hagikura)

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201406;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201406 {

  class SiteLinksDataHolder {
    long siteLinksFeedId;
    long linkTextFeedAttributeId;
    long linkUrlFeedAttributeId;
    List<long> siteLinkFeedItemIds = new List<long>();

    public long SiteLinksFeedId {
      get {
        return siteLinksFeedId;
      }
      set {
        siteLinksFeedId = value;
      }
    }

    public long LinkTextFeedAttributeId {
      get {
        return linkTextFeedAttributeId;
      }
      set {
        linkTextFeedAttributeId = value;
      }
    }

    public long LinkUrlFeedAttributeId {
      get {
        return linkUrlFeedAttributeId;
      }
      set {
        linkUrlFeedAttributeId = value;
      }
    }

    public List<long> SiteLinkFeedItemIds {
      get {
        return siteLinkFeedItemIds;
      }
      set {
        siteLinkFeedItemIds = value;
      }
    }
  }

  /// <summary>
  /// This code example adds a sitelinks feed and associates it with a campaign.
  /// To create a campaign, run AddCampaign.cs.
  ///
  /// Tags: CampaignFeedService.mutate, FeedService.mutate, FeedItemService.mutate,
  /// Tags: FeedMappingService.mutate
  /// </summary>
  public class AddSiteLinks : ExampleBase {

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      AddSiteLinks codeExample = new AddSiteLinks();
      Console.WriteLine(codeExample.Description);
      try {
        long campaignId = long.Parse("INSERT_CAMPAIGN_ID_HERE");
        string feedName = "INSERT_FEED_NAME_HERE";
        codeExample.Run(new AdWordsUser(), campaignId, feedName);
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
        return "This code example adds sitelinks to a campaign using feed services. To create a " +
            "campaign, run AddCampaign.cs.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="campaignId">Id of the campaign with which sitelinks are associated.
    /// </param>
    /// <param name="feedName">Name of the feed to be created.</param>
    public void Run(AdWordsUser user, long campaignId, string feedName) {
      SiteLinksDataHolder siteLinksData = new SiteLinksDataHolder();
      createSiteLinksFeed(user, siteLinksData, feedName);
      createSiteLinksFeedItems(user, siteLinksData);
      createSiteLinksFeedMapping(user, siteLinksData);
      createSiteLinksCampaignFeed(user, siteLinksData, campaignId);
    }

    private static void createSiteLinksFeed(AdWordsUser user, SiteLinksDataHolder siteLinksData,
        string feedName) {
      // Get the FeedService.
      FeedService feedService = (FeedService) user.GetService(AdWordsService.v201406.FeedService);

      // Create attributes.
      FeedAttribute textAttribute = new FeedAttribute();
      textAttribute.type = FeedAttributeType.STRING;
      textAttribute.name = "Link Text";
      FeedAttribute urlAttribute = new FeedAttribute();
      urlAttribute.type = FeedAttributeType.URL;
      urlAttribute.name = "Link URL";

      // Create the feed.
      Feed siteLinksFeed = new Feed();
      siteLinksFeed.name = feedName;
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
      Console.WriteLine("Feed with name {0} and ID {1} with linkTextAttributeId {2}"
          + " and linkUrlAttributeId {3} was created.", savedFeed.name, savedFeed.id,
          savedAttributes[0].id, savedAttributes[1].id);
    }

    private static void createSiteLinksFeedItems(
        AdWordsUser user, SiteLinksDataHolder siteLinksData) {
      // Get the FeedItemService.
      FeedItemService feedItemService =
        (FeedItemService) user.GetService(AdWordsService.v201406.FeedItemService);

      // Create operations to add FeedItems.
      FeedItemOperation home =
          newSiteLinkFeedItemAddOperation(siteLinksData,
          "Home", "http://www.example.com");
      FeedItemOperation stores =
          newSiteLinkFeedItemAddOperation(siteLinksData,
          "Stores", "http://www.example.com/stores");
      FeedItemOperation onSale =
          newSiteLinkFeedItemAddOperation(siteLinksData,
          "On Sale", "http://www.example.com/sale");
      FeedItemOperation support =
          newSiteLinkFeedItemAddOperation(siteLinksData,
          "Support", "http://www.example.com/support");
      FeedItemOperation products =
          newSiteLinkFeedItemAddOperation(siteLinksData,
          "Products", "http://www.example.com/prods");
      FeedItemOperation aboutUs =
          newSiteLinkFeedItemAddOperation(siteLinksData,
          "About Us", "http://www.example.com/about");

      FeedItemOperation[] operations =
          new FeedItemOperation[] {home, stores, onSale, support, products, aboutUs};

      FeedItemReturnValue result = feedItemService.mutate(operations);
      foreach (FeedItem item in result.value) {
        Console.WriteLine("FeedItem with feedItemId {0} was added.", item.feedItemId);
        siteLinksData.SiteLinkFeedItemIds.Add(item.feedItemId);
      }
    }

    // See the Placeholder reference page for a list of all the placeholder types and fields.
    // https://developers.google.com/adwords/api/docs/appendix/placeholders.html
    private const int PLACEHOLDER_SITELINKS = 1;

    // See the Placeholder reference page for a list of all the placeholder types and fields.
    private const int PLACEHOLDER_FIELD_SITELINK_LINK_TEXT = 1;
    private const int PLACEHOLDER_FIELD_SITELINK_URL = 2;

    private static void createSiteLinksFeedMapping(
        AdWordsUser user, SiteLinksDataHolder siteLinksData) {
      // Get the FeedItemService.
      FeedMappingService feedMappingService =
        (FeedMappingService) user.GetService(AdWordsService.v201406.FeedMappingService);

      // Map the FeedAttributeIds to the fieldId constants.
      AttributeFieldMapping linkTextFieldMapping = new AttributeFieldMapping();
      linkTextFieldMapping.feedAttributeId = siteLinksData.LinkTextFeedAttributeId;
      linkTextFieldMapping.fieldId = PLACEHOLDER_FIELD_SITELINK_LINK_TEXT;
      AttributeFieldMapping linkUrlFieldMapping = new AttributeFieldMapping();
      linkUrlFieldMapping.feedAttributeId = siteLinksData.LinkUrlFeedAttributeId;
      linkUrlFieldMapping.fieldId = PLACEHOLDER_FIELD_SITELINK_URL;

      // Create the FieldMapping and operation.
      FeedMapping feedMapping = new FeedMapping();
      feedMapping.placeholderType = PLACEHOLDER_SITELINKS;
      feedMapping.feedId = siteLinksData.SiteLinksFeedId;
      feedMapping.attributeFieldMappings =
          new AttributeFieldMapping[] {linkTextFieldMapping, linkUrlFieldMapping};
      FeedMappingOperation operation = new FeedMappingOperation();
      operation.operand = feedMapping;
      operation.@operator = Operator.ADD;

      // Save the field mapping.
      FeedMappingReturnValue result =
          feedMappingService.mutate(new FeedMappingOperation[] {operation});
      foreach (FeedMapping savedFeedMapping in result.value) {
        Console.WriteLine(
            "Feed mapping with ID {0} and placeholderType {1} was saved for feed with ID {2}.",
            savedFeedMapping.feedMappingId, savedFeedMapping.placeholderType,
            savedFeedMapping.feedId);
      }
    }

    private static void createSiteLinksCampaignFeed(AdWordsUser user,
      SiteLinksDataHolder siteLinksData, long campaignId) {
      // Get the CampaignFeedService.
      CampaignFeedService campaignFeedService =
        (CampaignFeedService) user.GetService(AdWordsService.v201406.CampaignFeedService);

      // Map the feed item ids to the campaign using an IN operation.
      RequestContextOperand feedItemRequestContextOperand = new RequestContextOperand();
      feedItemRequestContextOperand.contextType = RequestContextOperandContextType.FEED_ITEM_ID;

      List<FunctionArgumentOperand> feedItemOperands = new List<FunctionArgumentOperand>();
      foreach (long feedItemId in siteLinksData.SiteLinkFeedItemIds) {
        ConstantOperand feedItemOperand = new ConstantOperand();
        feedItemOperand.longValue = feedItemId;
        feedItemOperand.type = ConstantOperandConstantType.LONG;
        feedItemOperands.Add(feedItemOperand);
      }

      Function feedItemfunction = new Function();
      feedItemfunction.lhsOperand = new FunctionArgumentOperand[] {feedItemRequestContextOperand};
      feedItemfunction.@operator = FunctionOperator.IN;
      feedItemfunction.rhsOperand = feedItemOperands.ToArray();

      // Optional: to target to a platform, define a function and 'AND' it with
      // the feed item ID link:
      RequestContextOperand platformRequestContextOperand = new RequestContextOperand();
      platformRequestContextOperand.contextType = RequestContextOperandContextType.DEVICE_PLATFORM;

      ConstantOperand platformOperand = new ConstantOperand();
      platformOperand.stringValue = "Mobile";
      platformOperand.type = ConstantOperandConstantType.STRING;

      Function platformFunction = new Function();
      platformFunction.lhsOperand = new FunctionArgumentOperand[] {platformRequestContextOperand};
      platformFunction.@operator = FunctionOperator.EQUALS;
      platformFunction.rhsOperand = new FunctionArgumentOperand[] {platformOperand};

      // Combine the two functions using an AND operation.
      FunctionOperand feedItemFunctionOperand = new FunctionOperand();
      feedItemFunctionOperand.value = feedItemfunction;

      FunctionOperand platformFunctionOperand = new FunctionOperand();
      platformFunctionOperand.value = platformFunction;

      Function combinedFunction = new Function();
      combinedFunction.@operator = FunctionOperator.AND;
      combinedFunction.lhsOperand = new FunctionArgumentOperand[] {
          feedItemFunctionOperand, platformFunctionOperand};

      CampaignFeed campaignFeed = new CampaignFeed();
      campaignFeed.feedId = siteLinksData.SiteLinksFeedId;
      campaignFeed.campaignId = campaignId;
      campaignFeed.matchingFunction = combinedFunction;
      // Specifying placeholder types on the CampaignFeed allows the same feed
      // to be used for different placeholders in different Campaigns.
      campaignFeed.placeholderTypes = new int[] {PLACEHOLDER_SITELINKS};

      CampaignFeedOperation operation = new CampaignFeedOperation();
      operation.operand = campaignFeed;
      operation.@operator = Operator.ADD;
      CampaignFeedReturnValue result =
          campaignFeedService.mutate(new CampaignFeedOperation[] {operation});
      foreach (CampaignFeed savedCampaignFeed in result.value) {
        Console.WriteLine("Campaign with ID {0} was associated with feed with ID {1}",
            savedCampaignFeed.campaignId, savedCampaignFeed.feedId);
      }
    }

    private static FeedItemOperation newSiteLinkFeedItemAddOperation(
        SiteLinksDataHolder siteLinksData, String text, String url) {
      // Create the FeedItemAttributeValues for our text values.
      FeedItemAttributeValue linkTextAttributeValue = new FeedItemAttributeValue();
      linkTextAttributeValue.feedAttributeId = siteLinksData.LinkTextFeedAttributeId;
      linkTextAttributeValue.stringValue = text;
      FeedItemAttributeValue linkUrlAttributeValue = new FeedItemAttributeValue();
      linkUrlAttributeValue.feedAttributeId = siteLinksData.LinkUrlFeedAttributeId;
      linkUrlAttributeValue.stringValue = url;

      // Create the feed item and operation.
      FeedItem item = new FeedItem();
      item.feedId = siteLinksData.SiteLinksFeedId;
      item.attributeValues =
          new FeedItemAttributeValue[] {linkTextAttributeValue, linkUrlAttributeValue};
      FeedItemOperation operation = new FeedItemOperation();
      operation.operand = item;
      operation.@operator = Operator.ADD;
      return operation;
    }
  }
}
