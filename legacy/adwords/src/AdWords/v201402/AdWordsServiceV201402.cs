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

using Google.Api.Ads.Common.Lib;

using System;

namespace Google.Api.Ads.AdWords.Lib {

  /// <summary>
  /// Lists all the services available through this library.
  /// </summary>
  public partial class AdWordsService : AdsService {

    /// <summary>
    /// All the services available in v201402.
    /// </summary>
    public class v201402 {

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201402/AdGroupAdService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupAdService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201402/AdGroupBidModifierService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupBidModifierService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201402/AdGroupCriterionService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupCriterionService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201402/AdGroupFeedService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupFeedService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201402/AdGroupService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201402/AdParamService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdParamService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201402/AdwordsUserListService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdwordsUserListService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201402/AlertService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AlertService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201402/BiddingStrategyService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature BiddingStrategyService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201402/BudgetOrderService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature BudgetOrderService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201402/BudgetService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature BudgetService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201402/CampaignAdExtensionService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignAdExtensionService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201402/CampaignCriterionService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignCriterionService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201402/CampaignFeedService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignFeedService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201402/CampaignService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201402/ConstantDataService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ConstantDataService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201402/ConversionTrackerService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ConversionTrackerService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201402/CustomerService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CustomerService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201402/CustomerSyncService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CustomerSyncService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201402/CustomerFeedService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CustomerFeedService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201402/DataService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature DataService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201402/ExperimentService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ExperimentService;

      /// <summary>
      /// Factory type for v201402 services.
      /// </summary>
      public static readonly Type factoryType = typeof(AdWordsServiceFactory);

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201402/FeedItemService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature FeedItemService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201402/FeedMappingService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature FeedMappingService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201402/FeedService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature FeedService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201402/GeoLocationService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature GeoLocationService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201402/LocationCriterionService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature LocationCriterionService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201402/ManagedCustomerService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ManagedCustomerService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201402/MediaService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature MediaService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201402/MutateJobService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature MutateJobService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201402/OfflineConversionFeedService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature OfflineConversionFeedService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201402/ReportDefinitionService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ReportDefinitionService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201402/TargetingIdeaService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature TargetingIdeaService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201402/TrafficEstimatorService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature TrafficEstimatorService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/video/v201402/VideoAdService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature VideoAdService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/video/v201402/VideoCampaignCriterionService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature VideoCampaignCriterionService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/video/v201402/VideoCampaignService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature VideoCampaignService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/video/v201402/VideoService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature VideoService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/video/v201402/VideoTargetingGroupCriterionService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature VideoTargetingGroupCriterionService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/video/v201402/VideoTargetingGroupService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature VideoTargetingGroupService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/express/v201402/ExpressBusinessService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ExpressBusinessService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/express/v201402/PromotionService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature PromotionService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/express/v201402/ProductServiceService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ProductServiceService;

      /// <summary>
      /// Static constructor to initialize the service constants.
      /// </summary>
      static v201402() {
        OfflineConversionFeedService =
            AdWordsService.MakeServiceSignature("v201402", "cm", "OfflineConversionFeedService");
        AdGroupAdService =
            AdWordsService.MakeServiceSignature("v201402", "cm", "AdGroupAdService");
        AdGroupCriterionService =
            AdWordsService.MakeServiceSignature("v201402", "cm", "AdGroupCriterionService");
        AdGroupService =
            AdWordsService.MakeServiceSignature("v201402", "cm", "AdGroupService");
        AdGroupBidModifierService =
            AdWordsService.MakeServiceSignature("v201402", "cm", "AdGroupBidModifierService");
        AdGroupFeedService =
            AdWordsService.MakeServiceSignature("v201402", "cm", "AdGroupFeedService");
        AdParamService =
            AdWordsService.MakeServiceSignature("v201402", "cm", "AdParamService");
        AdwordsUserListService =
            AdWordsService.MakeServiceSignature("v201402", "rm", "AdwordsUserListService");
        AlertService =
            AdWordsService.MakeServiceSignature("v201402", "mcm", "AlertService");
        BiddingStrategyService =
            AdWordsService.MakeServiceSignature("v201402", "cm", "BiddingStrategyService");
        BudgetService =
            AdWordsService.MakeServiceSignature("v201402", "cm", "BudgetService");
        BudgetOrderService =
            AdWordsService.MakeServiceSignature("v201402", "billing", "BudgetOrderService");
        CampaignAdExtensionService =
            AdWordsService.MakeServiceSignature("v201402", "cm", "CampaignAdExtensionService");
        CampaignCriterionService =
            AdWordsService.MakeServiceSignature("v201402", "cm", "CampaignCriterionService");
        CampaignService =
            AdWordsService.MakeServiceSignature("v201402", "cm", "CampaignService");
        CampaignFeedService =
            AdWordsService.MakeServiceSignature("v201402", "cm", "CampaignFeedService");
        ConstantDataService =
            AdWordsService.MakeServiceSignature("v201402", "cm", "ConstantDataService");
        ConversionTrackerService =
            AdWordsService.MakeServiceSignature("v201402", "cm", "ConversionTrackerService");
        CustomerService =
            AdWordsService.MakeServiceSignature("v201402", "mcm", "CustomerService");
        CustomerSyncService =
            AdWordsService.MakeServiceSignature("v201402", "ch", "CustomerSyncService");
        CustomerFeedService =
            AdWordsService.MakeServiceSignature("v201402", "cm", "CustomerFeedService");
        DataService =
            AdWordsService.MakeServiceSignature("v201402", "cm", "DataService");
        ExperimentService =
            AdWordsService.MakeServiceSignature("v201402", "cm", "ExperimentService");
        FeedService =
            AdWordsService.MakeServiceSignature("v201402", "cm", "FeedService");
        FeedItemService =
            AdWordsService.MakeServiceSignature("v201402", "cm", "FeedItemService");
        FeedMappingService =
            AdWordsService.MakeServiceSignature("v201402", "cm", "FeedMappingService");
        GeoLocationService =
            AdWordsService.MakeServiceSignature("v201402", "cm", "GeoLocationService");
        LocationCriterionService =
            AdWordsService.MakeServiceSignature("v201402", "cm", "LocationCriterionService");
        ManagedCustomerService =
            AdWordsService.MakeServiceSignature("v201402", "mcm", "ManagedCustomerService");
        MediaService =
            AdWordsService.MakeServiceSignature("v201402", "cm", "MediaService");
        MutateJobService =
            AdWordsService.MakeServiceSignature("v201402", "cm", "MutateJobService");
        ReportDefinitionService =
            AdWordsService.MakeServiceSignature("v201402", "cm", "ReportDefinitionService");
        TargetingIdeaService =
            AdWordsService.MakeServiceSignature("v201402", "o", "TargetingIdeaService");
        TrafficEstimatorService =
            AdWordsService.MakeServiceSignature("v201402", "o", "TrafficEstimatorService");

        VideoAdService =
            AdWordsService.MakeServiceSignature("v201402", "video", "VideoAdService");
        VideoCampaignCriterionService =
            AdWordsService.MakeServiceSignature("v201402", "video",
                "VideoCampaignCriterionService");
        VideoCampaignService =
            AdWordsService.MakeServiceSignature("v201402", "video", "VideoCampaignService");
        VideoService =
            AdWordsService.MakeServiceSignature("v201402", "video", "VideoService");
        VideoTargetingGroupCriterionService =
            AdWordsService.MakeServiceSignature("v201402", "video",
                "VideoTargetingGroupCriterionService");
        VideoTargetingGroupService =
          AdWordsService.MakeServiceSignature("v201402", "video", "VideoTargetingGroupService");

        ExpressBusinessService =
            AdWordsService.MakeServiceSignature("v201402", "express",
                "ExpressBusinessService");
        PromotionService =
            AdWordsService.MakeServiceSignature("v201402", "express", "PromotionService");
        ProductServiceService =
            AdWordsService.MakeServiceSignature("v201402", "express", "ProductServiceService");
      }
    }
  }
}