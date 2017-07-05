// Copyright 2017, Google Inc. All Rights Reserved.
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
    /// All the services available in v201705.
    /// </summary>
    public class v201705 {

      #region Campaign Management.

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201705/AdGroupAdService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupAdService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201705/AdGroupBidModifierService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupBidModifierService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201705/AdGroupCriterionService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupCriterionService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201705/AdGroupFeedService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupFeedService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201705/AdGroupService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201705/AdParamService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdParamService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201705/BatchJobService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature BatchJobService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201705/BiddingStrategyService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature BiddingStrategyService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201705/BudgetService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature BudgetService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201705/CampaignCriterionService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignCriterionService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201705/CampaignFeedService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignFeedService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201705/CampaignBidModifierService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignBidModifierService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201705/CampaignGroupService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignGroupService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201705/CampaignGroupPerformanceTargetService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignGroupPerformanceTargetService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201705/CampaignService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/cm/v201705/CampaignSharedSetService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignSharedSetService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201705/ConstantDataService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ConstantDataService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201705/ConversionTrackerService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ConversionTrackerService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201705/CustomerFeedService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CustomerFeedService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201705/DataService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature DataService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201705/ExperimentService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ExperimentService;

      /// <summary>
      /// Factory type for v201705 services.
      /// </summary>
      public static readonly Type factoryType = typeof(AdWordsServiceFactory);

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201705/FeedItemService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature FeedItemService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201705/FeedMappingService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature FeedMappingService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201705/FeedService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature FeedService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/cm/v201705/SharedSetService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature LabelService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/cm/v201705/AccountLabelService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AccountLabelService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201705/LocationCriterionService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature LocationCriterionService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201705/MediaService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature MediaService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201705/OfflineConversionFeedService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature OfflineConversionFeedService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201705/OfflineConversionFeedService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature OfflineCallConversionFeedService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201705/ReportDefinitionService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ReportDefinitionService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/cm/v201705/SharedCriterionService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature SharedCriterionService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/cm/v201705/SharedCriterionService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature SharedSetService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/cm/v201705/DraftService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature DraftService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/cm/v201705/DraftAsyncErrorService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature DraftAsyncErrorService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/cm/v201705/TrialService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature TrialService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/cm/v201705/TrialAsyncErrorService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature TrialAsyncErrorService;

      #endregion

      #region Billing.

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201705/BudgetOrderService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature BudgetOrderService;

      #endregion

      #region Remarketing.

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201705/AdwordsUserListService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdwordsUserListService;

      #endregion

      #region Optimization

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201705/TargetingIdeaService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature TargetingIdeaService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201705/TrafficEstimatorService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature TrafficEstimatorService;

      #endregion

      #region Account Management.

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201705/CustomerService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CustomerService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201705/ManagedCustomerService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ManagedCustomerService;

      #endregion

      #region Change history.

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201705/CustomerSyncService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CustomerSyncService;

      #endregion

      #region Extension setting

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201705/AdCustomizerFeedService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdCustomizerFeedService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201705/AdGroupExtensionSettingService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupExtensionSettingService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201705/CampaignExtensionSettingService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignExtensionSettingService;

      /// <summary>
      /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201705/CustomerExtensionSettingService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CustomerExtensionSettingService;

      #endregion

      /// <summary>
      /// Static constructor to initialize the service constants.
      /// </summary>
      static v201705() {

        #region Campaign Management.

        AdGroupAdService =
            AdWordsService.MakeServiceSignature("v201705", "cm", "AdGroupAdService");
        AdGroupBidModifierService =
            AdWordsService.MakeServiceSignature("v201705", "cm", "AdGroupBidModifierService");
        AdGroupCriterionService =
            AdWordsService.MakeServiceSignature("v201705", "cm", "AdGroupCriterionService");
        AdGroupFeedService =
            AdWordsService.MakeServiceSignature("v201705", "cm", "AdGroupFeedService");
        AdGroupService =
            AdWordsService.MakeServiceSignature("v201705", "cm", "AdGroupService");
        AdParamService =
            AdWordsService.MakeServiceSignature("v201705", "cm", "AdParamService");
        BatchJobService =
            AdWordsService.MakeServiceSignature("v201705", "cm", "BatchJobService");
        BiddingStrategyService =
            AdWordsService.MakeServiceSignature("v201705", "cm", "BiddingStrategyService");
        BudgetService =
            AdWordsService.MakeServiceSignature("v201705", "cm", "BudgetService");
        CampaignCriterionService =
            AdWordsService.MakeServiceSignature("v201705", "cm", "CampaignCriterionService");
        CampaignFeedService =
            AdWordsService.MakeServiceSignature("v201705", "cm", "CampaignFeedService");
        CampaignService =
            AdWordsService.MakeServiceSignature("v201705", "cm", "CampaignService");
        CampaignBidModifierService =
            AdWordsService.MakeServiceSignature("v201705", "cm", "CampaignBidModifierService");
        CampaignGroupService =
            AdWordsService.MakeServiceSignature("v201705", "cm", "CampaignGroupService");
        CampaignGroupPerformanceTargetService =
            AdWordsService.MakeServiceSignature("v201705", "cm",
                "CampaignGroupPerformanceTargetService");
        CampaignSharedSetService =
            AdWordsService.MakeServiceSignature("v201705", "cm", "CampaignSharedSetService");
        ConstantDataService =
            AdWordsService.MakeServiceSignature("v201705", "cm", "ConstantDataService");
        ConversionTrackerService =
            AdWordsService.MakeServiceSignature("v201705", "cm", "ConversionTrackerService");

        CustomerFeedService =
            AdWordsService.MakeServiceSignature("v201705", "cm", "CustomerFeedService");
        DataService =
            AdWordsService.MakeServiceSignature("v201705", "cm", "DataService");
        ExperimentService =
            AdWordsService.MakeServiceSignature("v201705", "cm", "ExperimentService");
        FeedItemService =
            AdWordsService.MakeServiceSignature("v201705", "cm", "FeedItemService");
        FeedMappingService =
            AdWordsService.MakeServiceSignature("v201705", "cm", "FeedMappingService");
        FeedService =
            AdWordsService.MakeServiceSignature("v201705", "cm", "FeedService");
        LabelService =
            AdWordsService.MakeServiceSignature("v201705", "cm", "LabelService");
        LocationCriterionService =
            AdWordsService.MakeServiceSignature("v201705", "cm", "LocationCriterionService");
        MediaService =
            AdWordsService.MakeServiceSignature("v201705", "cm", "MediaService");
        OfflineConversionFeedService =
            AdWordsService.MakeServiceSignature("v201705", "cm", "OfflineConversionFeedService");
        OfflineCallConversionFeedService =
            AdWordsService.MakeServiceSignature("v201705", "cm",
                "OfflineCallConversionFeedService");
        ReportDefinitionService =
            AdWordsService.MakeServiceSignature("v201705", "cm", "ReportDefinitionService");
        SharedCriterionService =
            AdWordsService.MakeServiceSignature("v201705", "cm", "SharedCriterionService");
        SharedSetService =
            AdWordsService.MakeServiceSignature("v201705", "cm", "SharedSetService");

        DraftService =
            AdWordsService.MakeServiceSignature("v201705", "cm", "DraftService");
        DraftAsyncErrorService =
            AdWordsService.MakeServiceSignature("v201705", "cm", "DraftAsyncErrorService");
        TrialService =
            AdWordsService.MakeServiceSignature("v201705", "cm", "TrialService");
        TrialAsyncErrorService =
            AdWordsService.MakeServiceSignature("v201705", "cm", "TrialAsyncErrorService");

        #endregion

        #region Blling.

        BudgetOrderService =
            AdWordsService.MakeServiceSignature("v201705", "billing", "BudgetOrderService");

        #endregion

        #region Remarketing.

        AdwordsUserListService =
            AdWordsService.MakeServiceSignature("v201705", "rm", "AdwordsUserListService");

        #endregion

        #region Optimization.

        TargetingIdeaService =
            AdWordsService.MakeServiceSignature("v201705", "o", "TargetingIdeaService");
        TrafficEstimatorService =
            AdWordsService.MakeServiceSignature("v201705", "o", "TrafficEstimatorService");

        #endregion

        #region Change History.

        CustomerSyncService =
            AdWordsService.MakeServiceSignature("v201705", "ch", "CustomerSyncService");

        #endregion

        #region Account Management.

        AccountLabelService =
            AdWordsService.MakeServiceSignature("v201705", "mcm", "AccountLabelService");

        CustomerService =
            AdWordsService.MakeServiceSignature("v201705", "mcm", "CustomerService");
        ManagedCustomerService =
            AdWordsService.MakeServiceSignature("v201705", "mcm", "ManagedCustomerService");

        #endregion

        #region Extension setting

        AdCustomizerFeedService =
            AdWordsService.MakeServiceSignature("v201705", "cm", "AdCustomizerFeedService");
        AdGroupExtensionSettingService =
            AdWordsService.MakeServiceSignature("v201705", "cm", "AdGroupExtensionSettingService");

        CampaignExtensionSettingService =
            AdWordsService.MakeServiceSignature("v201705", "cm",
                "CampaignExtensionSettingService");
        CustomerExtensionSettingService =
            AdWordsService.MakeServiceSignature("v201705", "cm",
                "CustomerExtensionSettingService");

        #endregion
      }
    }
  }
}
