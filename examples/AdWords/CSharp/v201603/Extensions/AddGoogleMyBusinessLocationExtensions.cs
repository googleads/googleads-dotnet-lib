// Copyright 2016, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdWords.v201603;
using Google.Api.Ads.Common.Lib;

using System;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201603 {

  /// <summary>
  /// This code example adds a feed that syncs feed items from a Google
  /// My Business (GMB) account and associates the feed with a customer.
  /// </summary>
  public class AddGoogleMyBusinessLocationExtensions : ExampleBase {

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
        return "This code example adds a feed that syncs feed items from a Google My Business " +
            "(GMB) account and associates the feed with a customer.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      AddGoogleMyBusinessLocationExtensions codeExample =
          new AddGoogleMyBusinessLocationExtensions();
      Console.WriteLine(codeExample.Description);

      AdWordsUser user = new AdWordsUser();

      try {
        // The email address of either an owner or a manager of the GMB account.
        string gmbEmailAddress = "INSERT_GMB_EMAIL_ADDRESS_HERE";

        // Refresh the access token so that there's a valid access token.
        user.OAuthProvider.RefreshAccessToken();

        // If the gmbEmailAddress above is the same user you used to generate
        // your AdWords API refresh token, leave the assignment below unchanged.
        // Otherwise, to obtain an access token for your GMB account, run the
        // OAuth Token generator utility while logged in as the same user as
        // gmbEmailAddress. Copy and paste the AccessToken value into the
        // assignment below.
        string gmbAccessToken = user.OAuthProvider.AccessToken;

        // If the gmbEmailAddress above is for a GMB manager instead of the GMB
        // account owner, then set businessAccountIdentifier to the +Page ID of
        // a location for which the manager has access. See the location
        // extensions guide at
        // https://developers.google.com/adwords/api/docs/guides/feed-services-locations
        // for details.
        String businessAccountIdentifier = null;
        codeExample.Run(user, gmbEmailAddress, gmbAccessToken, businessAccountIdentifier);
      } catch (Exception e) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(e));
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="gmbEmailAddress">The email address for Google My Business
    /// account.</param>
    /// <param name="gmbAccessToken">The OAuth2 access token for Google
    /// My Business account.</param>
    /// <param name="businessAccountIdentifier">The account identifier for
    /// Google My Business account.</param>
    public void Run(AdWordsUser user, string gmbEmailAddress, string gmbAccessToken,
        string businessAccountIdentifier) {
      FeedService feedService = (FeedService) user.GetService(AdWordsService.v201603.FeedService);

      CustomerFeedService customerFeedService = (CustomerFeedService) user.GetService(
          AdWordsService.v201603.CustomerFeedService);

      // Create a feed that will sync to the Google My Business account
      // specified by gmbEmailAddress. Do not add FeedAttributes to this object,
      // as AdWords will add them automatically because this will be a
      // system generated feed.
      Feed gmbFeed = new Feed();
      gmbFeed.name = String.Format("Google My Business feed #{0}",
          ExampleUtilities.GetRandomString());

      PlacesLocationFeedData feedData = new PlacesLocationFeedData();
      feedData.emailAddress = gmbEmailAddress;
      feedData.businessAccountIdentifier = businessAccountIdentifier;

      // Optional: specify labels to filter Google My Business listings. If
      // specified, only listings that have any of the labels set are
      // synchronized into FeedItems.
      feedData.labelFilters = new string[] { "Stores in New York City" };

      OAuthInfo oAuthInfo = new OAuthInfo();
      oAuthInfo.httpMethod = "GET";

      // Permissions for the AdWords API scope will also cover GMB.
      oAuthInfo.httpRequestUrl = user.Config.GetDefaultOAuth2Scope();
      oAuthInfo.httpAuthorizationHeader = string.Format("Bearer {0}", gmbAccessToken);
      feedData.oAuthInfo = oAuthInfo;

      gmbFeed.systemFeedGenerationData = feedData;

      // Since this feed's feed items will be managed by AdWords,
      // you must set its origin to ADWORDS.
      gmbFeed.origin = FeedOrigin.ADWORDS;

      // Create an operation to add the feed.
      FeedOperation feedOperation = new FeedOperation();
      feedOperation.operand = gmbFeed;
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
        Console.WriteLine("Added GMB feed with ID {0}", addedFeed.id);

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
        // between the AdWords and GMB accounts completes.  The loop below
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
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to create customer feed.", e);
      }
    }
  }
}
