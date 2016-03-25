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

using Google.Api.Ads.Common.Lib;

using System;

namespace Google.Api.Ads.AdWords.Lib {

  /// <summary>
  /// Lists all the services available through this library.
  /// </summary>
  public partial class AdWordsService : AdsService {

    /// <summary>
    /// All the services available in v201603.
    /// </summary>
    public class v201603 {

      #region Campaign Management.

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201603/AdGroupAdService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupAdService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201603/AdGroupBidModifierService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupBidModifierService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201603/AdGroupCriterionService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupCriterionService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201603/AdGroupFeedService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupFeedService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201603/AdGroupService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201603/AdParamService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdParamService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201603/BatchJobService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature BatchJobService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201603/BiddingStrategyService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature BiddingStrategyService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201603/BudgetService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature BudgetService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201603/CampaignAdExtensionService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignAdExtensionService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201603/CampaignCriterionService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignCriterionService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201603/CampaignFeedService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignFeedService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201603/CampaignService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/cm/v201603/CampaignSharedSetService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignSharedSetService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201603/ConstantDataService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ConstantDataService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201603/ConversionTrackerService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ConversionTrackerService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201603/CustomerFeedService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CustomerFeedService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201603/DataService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature DataService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201603/ExperimentService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ExperimentService;

      /// <summary>
      /// Factory type for v201603 services.
      /// </summary>
      public static readonly Type factoryType = typeof(AdWordsServiceFactory);

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201603/FeedItemService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature FeedItemService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201603/FeedMappingService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature FeedMappingService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201603/FeedService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature FeedService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/cm/v201603/SharedSetService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature LabelService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/cm/v201603/AccountLabelService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AccountLabelService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201603/LocationCriterionService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature LocationCriterionService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201603/MediaService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature MediaService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201603/OfflineConversionFeedService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature OfflineConversionFeedService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201603/ReportDefinitionService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ReportDefinitionService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/cm/v201603/SharedCriterionService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature SharedCriterionService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/cm/v201603/SharedCriterionService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature SharedSetService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/cm/v201603/DraftService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature DraftService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/cm/v201603/DraftAsyncErrorService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature DraftAsyncErrorService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/cm/v201603/TrialService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature TrialService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/cm/v201603/TrialAsyncErrorService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature TrialAsyncErrorService;

      #endregion

      #region Billing.

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201603/BudgetOrderService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature BudgetOrderService;

      #endregion

      #region Remarketing.
      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201603/AdwordsUserListService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdwordsUserListService;

      #endregion

      #region Optimization

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201603/TargetingIdeaService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature TargetingIdeaService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201603/TrafficEstimatorService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature TrafficEstimatorService;

      #endregion

      #region AdWords Express.

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/express/v201603/BudgetSuggestionService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature BudgetSuggestionService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/express/v201603/ExpressBusinessService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ExpressBusinessService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/express/v201603/PromotionService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature PromotionService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/express/v201603/ProductServiceService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ProductServiceService;

      #endregion

      #region Account Management.

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201603/CustomerService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CustomerService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201603/ManagedCustomerService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ManagedCustomerService;

      #endregion

      #region Change history.

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201603/CustomerSyncService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CustomerSyncService;

      #endregion

      #region Extension setting

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201603/AdCustomizerFeedService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdCustomizerFeedService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201603/AdGroupExtensionSettingService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupExtensionSettingService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201603/CampaignExtensionSettingService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignExtensionSettingService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201603/CustomerExtensionSettingService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CustomerExtensionSettingService;

      #endregion

      /// <summary>
      /// Static constructor to initialize the service constants.
      /// </summary>
      static v201603() {

        #region Campaign Management.

        AdGroupAdService =
            AdWordsService.MakeServiceSignature("v201603", "cm", "AdGroupAdService");
        AdGroupBidModifierService =
            AdWordsService.MakeServiceSignature("v201603", "cm", "AdGroupBidModifierService");
        AdGroupCriterionService =
            AdWordsService.MakeServiceSignature("v201603", "cm", "AdGroupCriterionService");
        AdGroupFeedService =
            AdWordsService.MakeServiceSignature("v201603", "cm", "AdGroupFeedService");
        AdGroupService =
            AdWordsService.MakeServiceSignature("v201603", "cm", "AdGroupService");
        AdParamService =
            AdWordsService.MakeServiceSignature("v201603", "cm", "AdParamService");
        BatchJobService =
            AdWordsService.MakeServiceSignature("v201603", "cm", "BatchJobService");
        BiddingStrategyService =
            AdWordsService.MakeServiceSignature("v201603", "cm", "BiddingStrategyService");
        BudgetService =
            AdWordsService.MakeServiceSignature("v201603", "cm", "BudgetService");
        CampaignAdExtensionService =
            AdWordsService.MakeServiceSignature("v201603", "cm", "CampaignAdExtensionService");
        CampaignCriterionService =
            AdWordsService.MakeServiceSignature("v201603", "cm", "CampaignCriterionService");
        CampaignFeedService =
            AdWordsService.MakeServiceSignature("v201603", "cm", "CampaignFeedService");
        CampaignService =
            AdWordsService.MakeServiceSignature("v201603", "cm", "CampaignService");
        CampaignSharedSetService =
            AdWordsService.MakeServiceSignature("v201603", "cm", "CampaignSharedSetService");
        ConstantDataService =
            AdWordsService.MakeServiceSignature("v201603", "cm", "ConstantDataService");
        ConversionTrackerService =
            AdWordsService.MakeServiceSignature("v201603", "cm", "ConversionTrackerService");

        CustomerFeedService =
            AdWordsService.MakeServiceSignature("v201603", "cm", "CustomerFeedService");
        DataService =
            AdWordsService.MakeServiceSignature("v201603", "cm", "DataService");
        ExperimentService =
            AdWordsService.MakeServiceSignature("v201603", "cm", "ExperimentService");
        FeedItemService =
            AdWordsService.MakeServiceSignature("v201603", "cm", "FeedItemService");
        FeedMappingService =
            AdWordsService.MakeServiceSignature("v201603", "cm", "FeedMappingService");
        FeedService =
            AdWordsService.MakeServiceSignature("v201603", "cm", "FeedService");
        LabelService =
            AdWordsService.MakeServiceSignature("v201603", "cm", "LabelService");
        LocationCriterionService =
            AdWordsService.MakeServiceSignature("v201603", "cm", "LocationCriterionService");
        MediaService =
            AdWordsService.MakeServiceSignature("v201603", "cm", "MediaService");
        OfflineConversionFeedService =
            AdWordsService.MakeServiceSignature("v201603", "cm", "OfflineConversionFeedService");
        ReportDefinitionService =
            AdWordsService.MakeServiceSignature("v201603", "cm", "ReportDefinitionService");
        SharedCriterionService =
            AdWordsService.MakeServiceSignature("v201603", "cm", "SharedCriterionService");
        SharedSetService =
            AdWordsService.MakeServiceSignature("v201603", "cm", "SharedSetService");

        DraftService =
            AdWordsService.MakeServiceSignature("v201603", "cm", "DraftService");
        DraftAsyncErrorService =
            AdWordsService.MakeServiceSignature("v201603", "cm", "DraftAsyncErrorService");
        TrialService =
            AdWordsService.MakeServiceSignature("v201603", "cm", "TrialService");
        TrialAsyncErrorService =
            AdWordsService.MakeServiceSignature("v201603", "cm", "TrialAsyncErrorService");

        #endregion

        #region Blling.

        BudgetOrderService =
            AdWordsService.MakeServiceSignature("v201603", "billing", "BudgetOrderService");

        #endregion

        #region Remarketing.

        AdwordsUserListService =
            AdWordsService.MakeServiceSignature("v201603", "rm", "AdwordsUserListService");

        #endregion

        #region Optimization.

        TargetingIdeaService =
            AdWordsService.MakeServiceSignature("v201603", "o", "TargetingIdeaService");
        TrafficEstimatorService =
            AdWordsService.MakeServiceSignature("v201603", "o", "TrafficEstimatorService");

        #endregion

        #region AdWords Express.

        BudgetSuggestionService =
              AdWordsService.MakeServiceSignature("v201603", "express", "BudgetSuggestionService");
        ExpressBusinessService =
            AdWordsService.MakeServiceSignature("v201603", "express",
                "ExpressBusinessService");
        ProductServiceService =
            AdWordsService.MakeServiceSignature("v201603", "express", "ProductServiceService");
        PromotionService =
            AdWordsService.MakeServiceSignature("v201603", "express", "PromotionService");

        #endregion

        #region Change History.

        CustomerSyncService =
            AdWordsService.MakeServiceSignature("v201603", "ch", "CustomerSyncService");

        #endregion

        #region Account Management.

        AccountLabelService =
            AdWordsService.MakeServiceSignature("v201603", "mcm", "AccountLabelService");

        CustomerService =
            AdWordsService.MakeServiceSignature("v201603", "mcm", "CustomerService");
        ManagedCustomerService =
            AdWordsService.MakeServiceSignature("v201603", "mcm", "ManagedCustomerService");

        #endregion

        #region Extension setting

        AdCustomizerFeedService =
            AdWordsService.MakeServiceSignature("v201603", "cm", "AdCustomizerFeedService");
        AdGroupExtensionSettingService =
            AdWordsService.MakeServiceSignature("v201603", "cm", "AdGroupExtensionSettingService");

        CampaignExtensionSettingService =
            AdWordsService.MakeServiceSignature("v201603", "cm",
                "CampaignExtensionSettingService");
        CustomerExtensionSettingService =
            AdWordsService.MakeServiceSignature("v201603", "cm",
                "CustomerExtensionSettingService");

        #endregion
      }
    }
  }
}
