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
    /// All the services available in v201406.
    /// </summary>
    public class v201406 {

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201406/AdGroupAdService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupAdService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201406/AdGroupBidModifierService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupBidModifierService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201406/AdGroupCriterionService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupCriterionService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201406/AdGroupFeedService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupFeedService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201406/AdGroupService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201406/AdParamService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdParamService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201406/AdwordsUserListService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdwordsUserListService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201406/AlertService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AlertService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201406/BiddingStrategyService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature BiddingStrategyService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201406/BudgetOrderService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature BudgetOrderService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201406/BudgetService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature BudgetService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201406/CampaignAdExtensionService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignAdExtensionService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201406/CampaignCriterionService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignCriterionService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201406/CampaignFeedService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignFeedService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201406/CampaignService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201406/ConstantDataService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ConstantDataService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201406/ConversionTrackerService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ConversionTrackerService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201406/CustomerService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CustomerService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201406/CustomerSyncService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CustomerSyncService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201406/CustomerFeedService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CustomerFeedService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201406/DataService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature DataService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201406/ExperimentService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ExperimentService;

      /// <summary>
      /// Factory type for v201406 services.
      /// </summary>
      public static readonly Type factoryType = typeof(AdWordsServiceFactory);

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201406/FeedItemService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature FeedItemService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201406/FeedMappingService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature FeedMappingService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201406/FeedService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature FeedService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201406/GeoLocationService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature GeoLocationService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201406/LocationCriterionService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature LocationCriterionService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201406/ManagedCustomerService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ManagedCustomerService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201406/MediaService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature MediaService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201406/MutateJobService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature MutateJobService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201406/OfflineConversionFeedService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature OfflineConversionFeedService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201406/ReportDefinitionService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ReportDefinitionService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201406/TargetingIdeaService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature TargetingIdeaService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201406/TrafficEstimatorService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature TrafficEstimatorService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/express/v201406/ExpressBusinessService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ExpressBusinessService;
      
      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/express/v201406/BudgetSuggestionService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature BudgetSuggestionService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/express/v201406/PromotionService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature PromotionService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/express/v201406/ProductServiceService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ProductServiceService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/cm/v201406/CampaignSharedSetService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignSharedSetService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/cm/v201406/SharedCriterionService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature SharedCriterionService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/cm/v201406/SharedCriterionService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature SharedSetService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/cm/v201406/SharedSetService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature LabelService;

      /// <summary>
      /// Static constructor to initialize the service constants.
      /// </summary>
      static v201406() {
        OfflineConversionFeedService =
            AdWordsService.MakeServiceSignature("v201406", "cm", "OfflineConversionFeedService");
        AdGroupAdService =
            AdWordsService.MakeServiceSignature("v201406", "cm", "AdGroupAdService");
        AdGroupCriterionService =
            AdWordsService.MakeServiceSignature("v201406", "cm", "AdGroupCriterionService");
        AdGroupService =
            AdWordsService.MakeServiceSignature("v201406", "cm", "AdGroupService");
        AdGroupBidModifierService =
            AdWordsService.MakeServiceSignature("v201406", "cm", "AdGroupBidModifierService");
        AdGroupFeedService =
            AdWordsService.MakeServiceSignature("v201406", "cm", "AdGroupFeedService");
        AdParamService =
            AdWordsService.MakeServiceSignature("v201406", "cm", "AdParamService");
        AdwordsUserListService =
            AdWordsService.MakeServiceSignature("v201406", "rm", "AdwordsUserListService");
        AlertService =
            AdWordsService.MakeServiceSignature("v201406", "mcm", "AlertService");
        BiddingStrategyService =
            AdWordsService.MakeServiceSignature("v201406", "cm", "BiddingStrategyService");
        BudgetService =
            AdWordsService.MakeServiceSignature("v201406", "cm", "BudgetService");
        BudgetOrderService =
            AdWordsService.MakeServiceSignature("v201406", "billing", "BudgetOrderService");
        CampaignAdExtensionService =
            AdWordsService.MakeServiceSignature("v201406", "cm", "CampaignAdExtensionService");
        CampaignCriterionService =
            AdWordsService.MakeServiceSignature("v201406", "cm", "CampaignCriterionService");
        CampaignService =
            AdWordsService.MakeServiceSignature("v201406", "cm", "CampaignService");
        CampaignFeedService =
            AdWordsService.MakeServiceSignature("v201406", "cm", "CampaignFeedService");
        ConstantDataService =
            AdWordsService.MakeServiceSignature("v201406", "cm", "ConstantDataService");
        ConversionTrackerService =
            AdWordsService.MakeServiceSignature("v201406", "cm", "ConversionTrackerService");
        CustomerService =
            AdWordsService.MakeServiceSignature("v201406", "mcm", "CustomerService");
        CustomerSyncService =
            AdWordsService.MakeServiceSignature("v201406", "ch", "CustomerSyncService");
        CustomerFeedService =
            AdWordsService.MakeServiceSignature("v201406", "cm", "CustomerFeedService");
        DataService =
            AdWordsService.MakeServiceSignature("v201406", "cm", "DataService");
        ExperimentService =
            AdWordsService.MakeServiceSignature("v201406", "cm", "ExperimentService");
        FeedService =
            AdWordsService.MakeServiceSignature("v201406", "cm", "FeedService");
        FeedItemService =
            AdWordsService.MakeServiceSignature("v201406", "cm", "FeedItemService");
        FeedMappingService =
            AdWordsService.MakeServiceSignature("v201406", "cm", "FeedMappingService");
        GeoLocationService =
            AdWordsService.MakeServiceSignature("v201406", "cm", "GeoLocationService");
        LocationCriterionService =
            AdWordsService.MakeServiceSignature("v201406", "cm", "LocationCriterionService");
        ManagedCustomerService =
            AdWordsService.MakeServiceSignature("v201406", "mcm", "ManagedCustomerService");
        MediaService =
            AdWordsService.MakeServiceSignature("v201406", "cm", "MediaService");
        MutateJobService =
            AdWordsService.MakeServiceSignature("v201406", "cm", "MutateJobService");
        ReportDefinitionService =
            AdWordsService.MakeServiceSignature("v201406", "cm", "ReportDefinitionService");
        TargetingIdeaService =
            AdWordsService.MakeServiceSignature("v201406", "o", "TargetingIdeaService");
        TrafficEstimatorService =
            AdWordsService.MakeServiceSignature("v201406", "o", "TrafficEstimatorService");

        ExpressBusinessService =
            AdWordsService.MakeServiceSignature("v201406", "express",
                "ExpressBusinessService");
        BudgetSuggestionService =
              AdWordsService.MakeServiceSignature("v201406", "express", "BudgetSuggestionService");
        PromotionService =
            AdWordsService.MakeServiceSignature("v201406", "express", "PromotionService");
        ProductServiceService =
            AdWordsService.MakeServiceSignature("v201406", "express", "ProductServiceService");

        SharedCriterionService =
            AdWordsService.MakeServiceSignature("v201406", "cm", "SharedCriterionService");
        CampaignSharedSetService =
            AdWordsService.MakeServiceSignature("v201406", "cm", "CampaignSharedSetService");
        SharedSetService =
            AdWordsService.MakeServiceSignature("v201406", "cm", "SharedSetService");
        LabelService =
            AdWordsService.MakeServiceSignature("v201406", "cm", "LabelService");
      }
    }
  }
}