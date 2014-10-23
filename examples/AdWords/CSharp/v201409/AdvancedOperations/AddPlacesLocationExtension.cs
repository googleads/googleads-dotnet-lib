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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201409;
using Google.Api.Ads.Common.Lib;

using System;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201409 {

  /// <summary>
  /// This code example adds a feed that syncs feed items from a Google
  /// Places account and associates the feed with a customer.
  ///
  /// Tags: CustomerFeedService.mutate, FeedItemService.mutate
  /// Tags:  FeedMappingService.mutate, FeedService.mutate
  /// </summary>
  public class AddPlacesLocationExtension : ExampleBase {

    /// <summary>
    /// The placeholder type for location extensions. See the Placeholder
    /// reference page for a list of all the placeholder types and fields.
    ///
    /// https://developers.google.com/adwords/api/docs/appendix/placeholders
    /// </summary>
    private const int PLACEHOLDER_LOCATION = 7;

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example adds a feed that syncs feed items from a Google Places " +
            "account and associates the feed with a customer.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      AddPlacesLocationExtension codeExample = new AddPlacesLocationExtension();
      Console.WriteLine(codeExample.Description);
      try {
        string placesEmailAddress = "INSERT_PLACES_EMAIL_ADDRESS_HERE";
        // To obtain an access token for your Places account, run the
        // OAuthTokenGenerator utility and capture the Credential's
        // access token.
        string placesAccessToken = "INSERT_PLACES_ACCESS_TOKEN_HERE";
        codeExample.Run(new AdWordsUser(), placesEmailAddress, placesAccessToken);
      } catch (Exception ex) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(ex));
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="placesEmailAddress">The email address for Google Places
    /// account.</param>
    /// <param name="placesAccessToken">The OAuth2 access token for Google
    /// Places account.</param>
    public void Run(AdWordsUser user, string placesEmailAddress, string placesAccessToken) {
      FeedService feedService = (FeedService) user.GetService(AdWordsService.v201409.FeedService);

      CustomerFeedService customerFeedService = (CustomerFeedService) user.GetService(
          AdWordsService.v201409.CustomerFeedService);

      // Create a feed that will sync to the Google Places account specified
      // by placesEmailAddress. Do not add FeedAttributes to this object,
      // as AdWords will add them automatically because this will be a
      // system generated feed.
      Feed placesFeed = new Feed();
      placesFeed.name = String.Format("Places feed #{0}", ExampleUtilities.GetRandomString());

      PlacesLocationFeedData feedData = new PlacesLocationFeedData();
      feedData.emailAddress = placesEmailAddress;
      OAuthInfo oAuthInfo = new OAuthInfo();
      oAuthInfo.httpMethod = "GET";
      oAuthInfo.httpRequestUrl = "https://www.google.com/local/add";
      oAuthInfo.httpAuthorizationHeader = string.Format("Bearer {0}", placesAccessToken);
      feedData.oAuthInfo = oAuthInfo;

      placesFeed.systemFeedGenerationData = feedData;

      // Since this feed's feed items will be managed by AdWords,
      // you must set its origin to ADWORDS.
      placesFeed.origin = FeedOrigin.ADWORDS;

      // Create an operation to add the feed.
      FeedOperation feedOperation = new FeedOperation();
      feedOperation.operand = placesFeed;
      feedOperation.@operator = Operator.ADD;

      try {
        // Add the feed. Since it is a system generated feed, AdWords will
        // automatically:
        // 1. Set up the FeedAttributes on the feed.
        // 2. Set up a FeedMapping that associates the FeedAttributes of the
        //    feed with the placeholder fields of the LOCATION placeholder
        //    type.
        FeedReturnValue addFeedResult = feedService.mutate(new FeedOperation[] { feedOperation });
        Feed addedFeed = addFeedResult.value[0];
        Console.WriteLine("Added places feed with ID {0}", addedFeed.id);

        // Add a CustomerFeed that associates the feed with this customer for
        // the LOCATION placeholder type.
        CustomerFeed customerFeed = new CustomerFeed();
        customerFeed.feedId = addedFeed.id;
        customerFeed.placeholderTypes = new int[] { PLACEHOLDER_LOCATION };

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

        // After the completion of the Feed ADD operation above the added feed
        // will not be available for usage in a CustomerFeed until the sync
        // between the AdWords and Places accounts completes.  The loop below
        // will retry adding the CustomerFeed up to ten times with an
        // exponential back-off policy.
        CustomerFeed addedCustomerFeed = null;

        AdWordsAppConfig config = new AdWordsAppConfig();
        config.RetryCount = 10;

        ErrorHandler errorHandler = new ErrorHandler(config);
        do {
          try {
            CustomerFeedReturnValue customerFeedResult =
                customerFeedService.mutate(new CustomerFeedOperation[] { customerFeedOperation });
            addedCustomerFeed = customerFeedResult.value[0];

            Console.WriteLine("Added CustomerFeed for feed ID {0} and placeholder type {1}",
                addedCustomerFeed.feedId, addedCustomerFeed.placeholderTypes[0]);
            break;
          } catch (AdWordsApiException e) {
            ApiException apiException = (ApiException) e.ApiException;
            foreach (ApiError error in apiException.errors) {
              if (error is CustomerFeedError) {
                if ((error as CustomerFeedError).reason ==
                    CustomerFeedErrorReason.MISSING_FEEDMAPPING_FOR_PLACEHOLDER_TYPE) {
                  errorHandler.DoExponentialBackoff();
                  errorHandler.IncrementRetriedAttempts();
                } else {
                  throw;
                }
              }
            }

          }
        } while (errorHandler.HaveMoreRetryAttemptsLeft());

        // OPTIONAL: Create a CampaignFeed to specify which FeedItems to use at
        // the Campaign level.  This will be similar to the CampaignFeed in the
        // AddSiteLinks example, except you can also filter based on the
        // business name and category of each FeedItem by using a
        // FeedAttributeOperand in your matching function.

        // OPTIONAL: Create an AdGroupFeed for even more fine grained control
        // over which feed items are used at the AdGroup level.
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to create customer feed.", ex);
      }
    }
  }
}
