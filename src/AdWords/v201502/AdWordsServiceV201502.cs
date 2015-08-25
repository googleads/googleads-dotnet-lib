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

using Google.Api.Ads.Common.Lib;

using System;

namespace Google.Api.Ads.AdWords.Lib {

  /// <summary>
  /// Lists all the services available through this library.
  /// </summary>
  public partial class AdWordsService : AdsService {

    /// <summary>
    /// All the services available in v201502.
    /// </summary>
    public class v201502 {

      #region Campaign Management.

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201502/AdGroupAdService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupAdService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201502/AdGroupBidModifierService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupBidModifierService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201502/AdGroupCriterionService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupCriterionService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201502/AdGroupFeedService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupFeedService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201502/AdGroupService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201502/AdParamService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdParamService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201502/BiddingStrategyService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature BiddingStrategyService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201502/BudgetService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature BudgetService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201502/CampaignAdExtensionService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignAdExtensionService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201502/CampaignCriterionService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignCriterionService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201502/CampaignFeedService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignFeedService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201502/CampaignService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/cm/v201502/CampaignSharedSetService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignSharedSetService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201502/ConstantDataService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ConstantDataService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201502/ConversionTrackerService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ConversionTrackerService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201502/CustomerFeedService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CustomerFeedService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201502/DataService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature DataService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201502/ExperimentService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ExperimentService;

      /// <summary>
      /// Factory type for v201502 services.
      /// </summary>
      public static readonly Type factoryType = typeof(AdWordsServiceFactory);

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201502/FeedItemService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature FeedItemService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201502/FeedMappingService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature FeedMappingService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201502/FeedService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature FeedService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201502/GeoLocationService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature GeoLocationService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/cm/v201502/SharedSetService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature LabelService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/cm/v201502/AccountLabelService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AccountLabelService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201502/LocationCriterionService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature LocationCriterionService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201502/MediaService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature MediaService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201502/MutateJobService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature MutateJobService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201502/OfflineConversionFeedService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature OfflineConversionFeedService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201502/ReportDefinitionService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ReportDefinitionService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/cm/v201502/SharedCriterionService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature SharedCriterionService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/cm/v201502/SharedCriterionService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature SharedSetService;

      #endregion

      #region Billing.

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201502/BudgetOrderService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature BudgetOrderService;

      #endregion

      #region Remarketing.
      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201502/AdwordsUserListService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdwordsUserListService;

      #endregion

      #region Optimization

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201502/TargetingIdeaService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature TargetingIdeaService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201502/TrafficEstimatorService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature TrafficEstimatorService;

      #endregion

      #region AdWords Express.

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/express/v201502/BudgetSuggestionService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature BudgetSuggestionService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/express/v201502/ExpressBusinessService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ExpressBusinessService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/express/v201502/PromotionService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature PromotionService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/express/v201502/ProductServiceService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ProductServiceService;

      #endregion

      #region Account Management.

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201502/CustomerService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CustomerService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201502/ManagedCustomerService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ManagedCustomerService;

      #endregion

      #region Change history.

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201502/CustomerSyncService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CustomerSyncService;

      #endregion

      #region Extension setting

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201502/AdCustomizerFeedService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdCustomizerFeedService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201502/AdGroupExtensionSettingService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupExtensionSettingService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201502/CampaignExtensionSettingService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignExtensionSettingService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201502/CustomerExtensionSettingService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CustomerExtensionSettingService;

      #endregion

      /// <summary>
      /// Static constructor to initialize the service constants.
      /// </summary>
      static v201502() {

        #region Campaign Management.

        AdGroupAdService =
            AdWordsService.MakeServiceSignature("v201502", "cm", "AdGroupAdService");
        AdGroupBidModifierService =
            AdWordsService.MakeServiceSignature("v201502", "cm", "AdGroupBidModifierService");
        AdGroupCriterionService =
            AdWordsService.MakeServiceSignature("v201502", "cm", "AdGroupCriterionService");
        AdGroupFeedService =
            AdWordsService.MakeServiceSignature("v201502", "cm", "AdGroupFeedService");
        AdGroupService =
            AdWordsService.MakeServiceSignature("v201502", "cm", "AdGroupService");
        AdParamService =
            AdWordsService.MakeServiceSignature("v201502", "cm", "AdParamService");
        BiddingStrategyService =
            AdWordsService.MakeServiceSignature("v201502", "cm", "BiddingStrategyService");
        BudgetService =
            AdWordsService.MakeServiceSignature("v201502", "cm", "BudgetService");
        CampaignAdExtensionService =
            AdWordsService.MakeServiceSignature("v201502", "cm", "CampaignAdExtensionService");
        CampaignCriterionService =
            AdWordsService.MakeServiceSignature("v201502", "cm", "CampaignCriterionService");
        CampaignFeedService =
            AdWordsService.MakeServiceSignature("v201502", "cm", "CampaignFeedService");
        CampaignService =
            AdWordsService.MakeServiceSignature("v201502", "cm", "CampaignService");
        CampaignSharedSetService =
            AdWordsService.MakeServiceSignature("v201502", "cm", "CampaignSharedSetService");
        ConstantDataService =
            AdWordsService.MakeServiceSignature("v201502", "cm", "ConstantDataService");
        ConversionTrackerService =
            AdWordsService.MakeServiceSignature("v201502", "cm", "ConversionTrackerService");

        CustomerFeedService =
            AdWordsService.MakeServiceSignature("v201502", "cm", "CustomerFeedService");
        DataService =
            AdWordsService.MakeServiceSignature("v201502", "cm", "DataService");
        ExperimentService =
            AdWordsService.MakeServiceSignature("v201502", "cm", "ExperimentService");
        FeedItemService =
            AdWordsService.MakeServiceSignature("v201502", "cm", "FeedItemService");
        FeedMappingService =
            AdWordsService.MakeServiceSignature("v201502", "cm", "FeedMappingService");
        FeedService =
            AdWordsService.MakeServiceSignature("v201502", "cm", "FeedService");
        GeoLocationService =
            AdWordsService.MakeServiceSignature("v201502", "cm", "GeoLocationService");
        LabelService =
            AdWordsService.MakeServiceSignature("v201502", "cm", "LabelService");
        LocationCriterionService =
            AdWordsService.MakeServiceSignature("v201502", "cm", "LocationCriterionService");
        MediaService =
            AdWordsService.MakeServiceSignature("v201502", "cm", "MediaService");
        MutateJobService =
            AdWordsService.MakeServiceSignature("v201502", "cm", "MutateJobService");
        OfflineConversionFeedService =
            AdWordsService.MakeServiceSignature("v201502", "cm", "OfflineConversionFeedService");
        ReportDefinitionService =
            AdWordsService.MakeServiceSignature("v201502", "cm", "ReportDefinitionService");
        SharedCriterionService =
            AdWordsService.MakeServiceSignature("v201502", "cm", "SharedCriterionService");
        SharedSetService =
            AdWordsService.MakeServiceSignature("v201502", "cm", "SharedSetService");

        #endregion

        #region Blling.

        BudgetOrderService =
            AdWordsService.MakeServiceSignature("v201502", "billing", "BudgetOrderService");

        #endregion

        #region Remarketing.

        AdwordsUserListService =
            AdWordsService.MakeServiceSignature("v201502", "rm", "AdwordsUserListService");

        #endregion

        #region Optimization.

        TargetingIdeaService =
            AdWordsService.MakeServiceSignature("v201502", "o", "TargetingIdeaService");
        TrafficEstimatorService =
            AdWordsService.MakeServiceSignature("v201502", "o", "TrafficEstimatorService");

        #endregion

        #region AdWords Express.

        BudgetSuggestionService =
              AdWordsService.MakeServiceSignature("v201502", "express", "BudgetSuggestionService");
        ExpressBusinessService =
            AdWordsService.MakeServiceSignature("v201502", "express",
                "ExpressBusinessService");
        ProductServiceService =
            AdWordsService.MakeServiceSignature("v201502", "express", "ProductServiceService");
        PromotionService =
            AdWordsService.MakeServiceSignature("v201502", "express", "PromotionService");

        #endregion

        #region Change History.

        CustomerSyncService =
            AdWordsService.MakeServiceSignature("v201502", "ch", "CustomerSyncService");

        #endregion

        #region Account Management.

        AccountLabelService =
            AdWordsService.MakeServiceSignature("v201502", "mcm", "AccountLabelService");

        CustomerService =
            AdWordsService.MakeServiceSignature("v201502", "mcm", "CustomerService");
        ManagedCustomerService =
            AdWordsService.MakeServiceSignature("v201502", "mcm", "ManagedCustomerService");

        #endregion

        #region Extension setting

        AdCustomizerFeedService =
            AdWordsService.MakeServiceSignature("v201502", "cm", "AdCustomizerFeedService");
        AdGroupExtensionSettingService =
            AdWordsService.MakeServiceSignature("v201502", "cm", "AdGroupExtensionSettingService");

        CampaignExtensionSettingService =
            AdWordsService.MakeServiceSignature("v201502", "cm",
                "CampaignExtensionSettingService");
        CustomerExtensionSettingService =
            AdWordsService.MakeServiceSignature("v201502", "cm",
                "CustomerExtensionSettingService");

        #endregion
      }
    }
  }
}