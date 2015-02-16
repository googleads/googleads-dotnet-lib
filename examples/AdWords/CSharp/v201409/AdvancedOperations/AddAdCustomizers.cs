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

// Author: api.anash@gmail.com (Anash P. Oommen)

using System;
using System.Collections.Generic;
using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201409;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201409 {

  /// <summary>
  /// This code example adds an ad customizer feed and associates it with the
  /// customer. Then it adds an ad in two different adgroups that uses the
  /// feed to populate dynamic data.
  ///
  /// Tags: CustomerFeedService.mutate, FeedItemService.mutate
  /// Tags: FeedMappingService.mutate
  /// Tags: FeedService.mutate, AdGroupAdService.mutate
  /// </summary>
  public class AddAdCustomizers : ExampleBase {

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      AddAdCustomizers codeExample = new AddAdCustomizers();
      Console.WriteLine(codeExample.Description);
      try {
        long adGroupId1 = long.Parse("INSERT_ADGROUP_ID_HERE");
        long adGroupId2 = long.Parse("INSERT_ADGROUP_ID_HERE");
        string feedName = "INSERT_FEED_NAME_HERE";
        codeExample.Run(new AdWordsUser(), adGroupId1, adGroupId2, feedName);
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
        return "This code example adds an ad customizer feed and associates it with the " +
            "customer. Then it adds an ad in two different adgroups that uses the feed to " +
            "populate dynamic data.";
      }
    }

    // See the Placeholder reference page for a list of all the placeholder
    // types and fields.
    // https://developers.google.com/adwords/api/docs/appendix/placeholders
    private const int PLACEHOLDER_AD_CUSTOMIZER = 10;

    private const int PLACEHOLDER_FIELD_PRICE = 3;
    private const int PLACEHOLDER_FIELD_DATE = 4;
    private const int PLACEHOLDER_FIELD_STRING = 5;

    /// <summary>
    /// A container for metadata related to an ad customizers feed.
    /// </summary>
    private class CustomizersDataHolder {

      /// <summary>
      /// The feed ID.
      /// </summary>
      private long feedId;

      /// <summary>
      /// The name feed attribute ID.
      /// </summary>
      private long nameFeedAttributeId;

      /// <summary>
      /// The price feed attribute ID.
      /// </summary>
      private long priceFeedAttributeId;

      /// <summary>
      /// The date feed attribute ID.
      /// </summary>
      private long dateFeedAttributeId;

      /// <summary>
      /// The feed item IDs.
      /// </summary>
      private List<long> feedItemIds = new List<long>();

      /// <summary>
      /// Gets or sets the feed ID.
      /// </summary>
      public long FeedId {
        get {
          return feedId;
        }
        set {
          feedId = value;
        }
      }

      /// <summary>
      /// Gets or sets the name feed attribute identifier.
      /// </summary>
      public long NameFeedAttributeId {
        get {
          return nameFeedAttributeId;
        }
        set {
          nameFeedAttributeId = value;
        }
      }

      /// <summary>
      /// Gets or sets the price feed attribute ID.
      /// </summary>
      public long PriceFeedAttributeId {
        get {
          return priceFeedAttributeId;
        }
        set {
          priceFeedAttributeId = value;
        }
      }

      /// <summary>
      /// Gets or sets the date feed attribute ID.
      /// </summary>
      /// <value>
      public long DateFeedAttributeId {
        get {
          return dateFeedAttributeId;
        }
        set {
          dateFeedAttributeId = value;
        }
      }

      /// <summary>
      /// Gets or sets the feed item IDs.
      /// </summary>
      public List<long> FeedItemIds {
        get {
          return feedItemIds;
        }
        set {
          feedItemIds = value;
        }
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
    public void Run(AdWordsUser user, long adGroupId1, long adGroupId2, string feedName) {
      // Create a customizer feed. One feed per account can be used for all ads.
      CustomizersDataHolder dataHolder = CreateCustomizerFeed(user, feedName);

      // Create a feed mapping to map the fields with customizer IDs.
      CreateFeedMapping(user, dataHolder);

      // Add feed items containing the values we'd like to place in ads.
      CreateCustomizerFeedItems(user, new long[] { adGroupId1, adGroupId2 }, dataHolder);

      // Create a customer (account-level) feed with a matching function that
      // determines when to use this feed. For this case we use the "IDENTITY"
      // matching function that is always true just to associate this feed with
      // the customer. The targeting is done within the feed items using the
      // campaignTargeting, adGroupTargeting, or keywordTargeting attributes.
      CreateCustomerFeed(user, dataHolder);

      // All set! We can now create ads with customizations.
      CreateAdsWithCustomizations(user, new long[] { adGroupId1, adGroupId2 }, feedName);
    }

    /// <summary>
    /// Creates a new Feed for ad customizers.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="feedName">Name of the feed to be created.</param>
    /// <returns>A new CustomizersDataHolder, populated with the feed ID and
    /// attribute IDs of the new Feed.</returns>
    private static CustomizersDataHolder CreateCustomizerFeed(AdWordsUser user, string feedName) {
      // Get the FeedService.
      FeedService feedService = (FeedService) user.GetService(AdWordsService.v201409.FeedService);

      Feed customizerFeed = new Feed();
      customizerFeed.name = feedName;

      FeedAttribute nameAttribute = new FeedAttribute();
      nameAttribute.name = "Name";
      nameAttribute.type = FeedAttributeType.STRING;

      FeedAttribute priceAttribute = new FeedAttribute();
      priceAttribute.name = "Price";
      priceAttribute.type = FeedAttributeType.STRING;

      FeedAttribute dateAttribute = new FeedAttribute();
      dateAttribute.name = "Date";
      dateAttribute.type = FeedAttributeType.DATE_TIME;

      customizerFeed.attributes = new FeedAttribute[] {
          nameAttribute, priceAttribute, dateAttribute
      };

      FeedOperation feedOperation = new FeedOperation();
      feedOperation.operand = customizerFeed;
      feedOperation.@operator = (Operator.ADD);

      Feed addedFeed = feedService.mutate(new FeedOperation[] { feedOperation }).value[0];

      CustomizersDataHolder dataHolder = new CustomizersDataHolder();
      dataHolder.FeedId = addedFeed.id;
      dataHolder.NameFeedAttributeId = addedFeed.attributes[0].id;
      dataHolder.PriceFeedAttributeId = addedFeed.attributes[1].id;
      dataHolder.DateFeedAttributeId = addedFeed.attributes[2].id;

      Console.WriteLine("Feed with name '{0}' and ID {1} was added with:\n", addedFeed.name,
          dataHolder.FeedId);
      Console.WriteLine("  Name attribute ID {0}\n", dataHolder.NameFeedAttributeId);
      Console.WriteLine("  Price attribute ID {0}\n", dataHolder.PriceFeedAttributeId);
      Console.WriteLine("  Date attribute ID {0}\n", dataHolder.DateFeedAttributeId);

      return dataHolder;
    }

    /// <summary>
    /// Creates a new FeedMapping that indicates how the data holder's feed
    /// should be interpreted in the context of ad customizers.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="dataHolder">The data holder that contains metadata about
    /// the customizer Feed.</param>
    private static void CreateFeedMapping(AdWordsUser user, CustomizersDataHolder dataHolder) {
      // Get the FeedMappingService.
      FeedMappingService feedMappingService = (FeedMappingService) user.GetService(
          AdWordsService.v201409.FeedMappingService);

      FeedMapping feedMapping = new FeedMapping();
      feedMapping.feedId = dataHolder.FeedId;
      feedMapping.placeholderType = PLACEHOLDER_AD_CUSTOMIZER;

      List<AttributeFieldMapping> attributeFieldMappings = new List<AttributeFieldMapping>();
      AttributeFieldMapping attributeFieldMapping;

      attributeFieldMapping = new AttributeFieldMapping();
      attributeFieldMapping.feedAttributeId = dataHolder.NameFeedAttributeId;
      attributeFieldMapping.fieldId = PLACEHOLDER_FIELD_STRING;
      attributeFieldMappings.Add(attributeFieldMapping);

      attributeFieldMapping = new AttributeFieldMapping();
      attributeFieldMapping.feedAttributeId = dataHolder.PriceFeedAttributeId;
      attributeFieldMapping.fieldId = PLACEHOLDER_FIELD_PRICE;
      attributeFieldMappings.Add(attributeFieldMapping);

      attributeFieldMapping = new AttributeFieldMapping();
      attributeFieldMapping.feedAttributeId = dataHolder.DateFeedAttributeId;
      attributeFieldMapping.fieldId = PLACEHOLDER_FIELD_DATE;
      attributeFieldMappings.Add(attributeFieldMapping);

      feedMapping.attributeFieldMappings = attributeFieldMappings.ToArray();

      FeedMappingOperation feedMappingOperation = new FeedMappingOperation();
      feedMappingOperation.operand = feedMapping;
      feedMappingOperation.@operator = Operator.ADD;

      FeedMapping addedFeedMapping =
          feedMappingService.mutate(new FeedMappingOperation[] { feedMappingOperation }).value[0];

      Console.WriteLine("Feed mapping with ID {0} and placeholder type {1} was added for " +
          "feed with ID {2}.",
          addedFeedMapping.feedMappingId, addedFeedMapping.placeholderType,
          addedFeedMapping.feedId);
    }

    /// <summary>
    /// Creates FeedItems with the values to use in ad customizations for each
    /// ad group in <code>adGroupIds</code>.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="adGroupIds">IDs of adgroups to which ad customizations are
    /// made.</param>
    /// <param name="dataHolder">The data holder that contains metadata about
    /// the customizer Feed.</param>
    private static void CreateCustomizerFeedItems(AdWordsUser user, long[] adGroupIds,
        CustomizersDataHolder dataHolder) {
      // Get the FeedItemService.
      FeedItemService feedItemService = (FeedItemService) user.GetService(
          AdWordsService.v201409.FeedItemService);

      List<FeedItemOperation> feedItemOperations = new List<FeedItemOperation>();
      feedItemOperations.Add(CreateFeedItemAddOperation("Mars", "$1234.56", "20140601 000000",
          adGroupIds[0], dataHolder));
      feedItemOperations.Add(CreateFeedItemAddOperation("Venus", "$1450.00", "20140615 120000",
          adGroupIds[1], dataHolder));

      FeedItemReturnValue feedItemReturnValue = feedItemService.mutate(
          feedItemOperations.ToArray());

      foreach (FeedItem addedFeedItem in feedItemReturnValue.value) {
        Console.WriteLine("Added feed item with ID {0}", addedFeedItem.feedItemId);
        dataHolder.FeedItemIds.Add(addedFeedItem.feedItemId);
      }
    }

    /// <summary>
    /// Creates a FeedItemOperation that will create a FeedItem with the
    /// specified values and ad group target when sent to
    /// FeedItemService.mutate.
    /// </summary>
    /// <param name="name">The value for the name attribute of the FeedItem.
    /// </param>
    /// <param name="price">The value for the price attribute of the FeedItem.
    /// </param>
    /// <param name="date">The value for the date attribute of the FeedItem.
    /// </param>
    /// <param name="adGroupId">The ID of the ad group to target with the
    /// FeedItem.</param>
    /// <param name="dataHolder">The data holder that contains metadata about
    /// the customizer Feed.</param>
    /// <returns>A new FeedItemOperation for adding a FeedItem.</returns>
    private static FeedItemOperation CreateFeedItemAddOperation(string name, string price,
        String date, long adGroupId, CustomizersDataHolder dataHolder) {
      FeedItem feedItem = new FeedItem();
      feedItem.feedId = dataHolder.FeedId;
      List<FeedItemAttributeValue> attributeValues = new List<FeedItemAttributeValue>();

      FeedItemAttributeValue nameAttributeValue = new FeedItemAttributeValue();
      nameAttributeValue.feedAttributeId = dataHolder.NameFeedAttributeId;
      nameAttributeValue.stringValue = name;
      attributeValues.Add(nameAttributeValue);

      FeedItemAttributeValue priceAttributeValue = new FeedItemAttributeValue();
      priceAttributeValue.feedAttributeId = dataHolder.PriceFeedAttributeId;
      priceAttributeValue.stringValue = price;
      attributeValues.Add(priceAttributeValue);

      FeedItemAttributeValue dateAttributeValue = new FeedItemAttributeValue();
      dateAttributeValue.feedAttributeId = dataHolder.DateFeedAttributeId;
      dateAttributeValue.stringValue = date;
      attributeValues.Add(dateAttributeValue);

      feedItem.attributeValues = attributeValues.ToArray();

      feedItem.adGroupTargeting = new FeedItemAdGroupTargeting();
      feedItem.adGroupTargeting.TargetingAdGroupId = adGroupId;

      FeedItemOperation feedItemOperation = new FeedItemOperation();
      feedItemOperation.operand = feedItem;
      feedItemOperation.@operator = Operator.ADD;

      return feedItemOperation;
    }

    /// <summary>
    /// Creates a CustomerFeed that will associate the data holder's Feed with
    /// the ad customizer placeholder type.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="dataHolder">The data holder that contains metadata about
    /// the customizer Feed.</param>
    private static void CreateCustomerFeed(AdWordsUser user, CustomizersDataHolder dataHolder) {
      // Get the CustomerFeedService.
      CustomerFeedService customerFeedService = (CustomerFeedService) user.GetService(
          AdWordsService.v201409.CustomerFeedService);

      CustomerFeed customerFeed = new CustomerFeed();
      customerFeed.feedId = dataHolder.FeedId;
      customerFeed.placeholderTypes = new int[] { PLACEHOLDER_AD_CUSTOMIZER };

      // Create a matching function that will always evaluate to true.
      Function customerMatchingFunction = new Function();
      ConstantOperand constOperand = new ConstantOperand();
      constOperand.type = ConstantOperandConstantType.BOOLEAN;
      constOperand.booleanValue = true;
      customerMatchingFunction.lhsOperand = new FunctionArgumentOperand[] { constOperand };
      customerMatchingFunction.@operator = FunctionOperator.IDENTITY;
      customerFeed.matchingFunction = customerMatchingFunction;

      // Create an operation to add the customer feed.
      CustomerFeedOperation customerFeedOperation = new CustomerFeedOperation();
      customerFeedOperation.operand = customerFeed;
      customerFeedOperation.@operator = Operator.ADD;

      CustomerFeed addedCustomerFeed = customerFeedService.mutate(
          new CustomerFeedOperation[] { customerFeedOperation }).value[0];

      Console.WriteLine("Customer feed for feed ID {0} was added.", addedCustomerFeed.feedId);
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
        string feedName) {
      // Get the AdGroupAdService.
      AdGroupAdService adGroupAdService = (AdGroupAdService) user.GetService(
          AdWordsService.v201409.AdGroupAdService);

      TextAd textAd = new TextAd();
      textAd.headline = string.Format("Luxury Cruise to {{={0}.Name}}", feedName);
      textAd.description1 = string.Format("Only {{={0}.Price}}", feedName);
      textAd.description2 = string.Format("Offer ends in {{=countdown({0}.Date)}}!", feedName);
      textAd.finalUrls = new string[] { "http://www.example.com" };
      textAd.displayUrl = "www.example.com";

      // We add the same ad to both ad groups. When they serve, they will show
      // different values, since they match different feed items.
      List<AdGroupAdOperation> adGroupAdOperations = new List<AdGroupAdOperation>();
      foreach (long adGroupId in adGroupIds) {
        AdGroupAd adGroupAd = new AdGroupAd();
        adGroupAd.adGroupId = adGroupId;
        adGroupAd.ad = textAd;

        AdGroupAdOperation adGroupAdOperation = new AdGroupAdOperation();
        adGroupAdOperation.operand = adGroupAd;
        adGroupAdOperation.@operator = Operator.ADD;

        adGroupAdOperations.Add(adGroupAdOperation);
      }

      AdGroupAdReturnValue adGroupAdReturnValue = adGroupAdService.mutate(
          adGroupAdOperations.ToArray());

      foreach (AdGroupAd addedAd in adGroupAdReturnValue.value) {
        Console.WriteLine("Created an ad with ID {0}, type '{1}' and status '{2}'.",
            addedAd.ad.id, addedAd.ad.AdType, addedAd.status);
      }
    }
  }
}
