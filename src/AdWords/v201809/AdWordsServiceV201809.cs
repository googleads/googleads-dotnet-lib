// Copyright 2018 Google LLC
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

namespace Google.Api.Ads.AdWords.Lib
{
    /// <summary>
    /// Lists all the services available through this library.
    /// </summary>
    public partial class AdWordsService : AdsService
    {
        /// <summary>
        /// All the services available in v201809.
        /// </summary>
        public class v201809
        {
            #region Campaign Management.

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201809/AdGroupAdService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature AdGroupAdService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201809/AdGroupBidModifierService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature AdGroupBidModifierService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201809/AdGroupCriterionService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature AdGroupCriterionService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201809/AdGroupFeedService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature AdGroupFeedService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201809/AdGroupService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature AdGroupService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201809/AdParamService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature AdParamService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201809/AssetService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature AssetService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201809/AdService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature AdService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201809/BatchJobService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature BatchJobService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201809/BiddingStrategyService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature BiddingStrategyService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201809/BudgetService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature BudgetService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201809/CampaignCriterionService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature CampaignCriterionService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201809/CampaignFeedService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature CampaignFeedService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201809/CampaignBidModifierService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature CampaignBidModifierService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201809/CampaignGroupService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature CampaignGroupService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201809/CampaignGroupPerformanceTargetService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature CampaignGroupPerformanceTargetService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201809/CampaignService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature CampaignService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/cm/v201809/CampaignSharedSetService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature CampaignSharedSetService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201809/ConstantDataService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ConstantDataService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201809/ConversionTrackerService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ConversionTrackerService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201809/CustomerFeedService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature CustomerFeedService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201809/CustomerNegativeCriterionService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature CustomerNegativeCriterionService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201809/DataService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature DataService;

            /// <summary>
            /// Factory type for v201809 services.
            /// </summary>
            public static readonly Type factoryType = typeof(AdWordsServiceFactory);

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201809/FeedItemService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature FeedItemService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201809/FeedMappingService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature FeedMappingService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201809/FeedService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature FeedService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201809/FeedItemTargetService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature FeedItemTargetService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/cm/v201809/SharedSetService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature LabelService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/cm/v201809/AccountLabelService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature AccountLabelService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201809/LocationCriterionService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature LocationCriterionService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201809/MediaService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature MediaService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201809/OfflineConversionFeedService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature OfflineConversionFeedService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201809/OfflineConversionFeedService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature OfflineCallConversionFeedService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201809/ReportDefinitionService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ReportDefinitionService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/cm/v201809/SharedCriterionService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature SharedCriterionService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/cm/v201809/SharedCriterionService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature SharedSetService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/cm/v201809/DraftService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature DraftService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/cm/v201809/DraftAsyncErrorService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature DraftAsyncErrorService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201809/OfflineConversionAdjustmentFeedService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature OfflineConversionAdjustmentFeedService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/cm/v201809/TrialService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature TrialService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/cm/v201809/TrialAsyncErrorService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature TrialAsyncErrorService;

            #endregion Campaign Management.

            #region Billing.

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201809/BudgetOrderService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature BudgetOrderService;

            #endregion Billing.

            #region Remarketing.

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201809/AdwordsUserListService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature AdwordsUserListService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201809/OfflineDataUploadService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature OfflineDataUploadService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201809/CustomAffinityService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature CustomAffinityService;

            #endregion Remarketing.

            #region Optimization

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201809/TargetingIdeaService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature TargetingIdeaService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201809/TrafficEstimatorService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature TrafficEstimatorService;

            #endregion Optimization

            #region Account Management.

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201809/CustomerService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature CustomerService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201809/ManagedCustomerService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ManagedCustomerService;

            #endregion Account Management.

            #region Change history.

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201809/CustomerSyncService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature CustomerSyncService;

            #endregion Change history.

            #region Extension setting

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201809/AdCustomizerFeedService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature AdCustomizerFeedService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201809/AdGroupExtensionSettingService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature AdGroupExtensionSettingService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201809/CampaignExtensionSettingService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature CampaignExtensionSettingService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201809/CustomerExtensionSettingService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature CustomerExtensionSettingService;

            #endregion Extension setting

            /// <summary>
            /// Static constructor to initialize the service constants.
            /// </summary>
            static v201809()
            {
                #region Campaign Management.

                AdGroupAdService =
                    AdWordsService.MakeServiceSignature("v201809", "cm", "AdGroupAdService");
                AdGroupBidModifierService =
                    AdWordsService.MakeServiceSignature("v201809", "cm", "AdGroupBidModifierService");
                AdGroupCriterionService =
                    AdWordsService.MakeServiceSignature("v201809", "cm", "AdGroupCriterionService");
                AdGroupFeedService =
                    AdWordsService.MakeServiceSignature("v201809", "cm", "AdGroupFeedService");
                AdGroupService =
                    AdWordsService.MakeServiceSignature("v201809", "cm", "AdGroupService");
                AssetService =
                    AdWordsService.MakeServiceSignature("v201809", "cm", "AssetService");
                AdService =
                    AdWordsService.MakeServiceSignature("v201809", "cm", "AdService");
                AdParamService =
                    AdWordsService.MakeServiceSignature("v201809", "cm", "AdParamService");
                BatchJobService =
                    AdWordsService.MakeServiceSignature("v201809", "cm", "BatchJobService");
                BiddingStrategyService =
                    AdWordsService.MakeServiceSignature("v201809", "cm", "BiddingStrategyService");
                BudgetService =
                    AdWordsService.MakeServiceSignature("v201809", "cm", "BudgetService");
                CampaignCriterionService =
                    AdWordsService.MakeServiceSignature("v201809", "cm", "CampaignCriterionService");
                CampaignFeedService =
                    AdWordsService.MakeServiceSignature("v201809", "cm", "CampaignFeedService");
                CampaignService =
                    AdWordsService.MakeServiceSignature("v201809", "cm", "CampaignService");
                CampaignBidModifierService =
                    AdWordsService.MakeServiceSignature("v201809", "cm", "CampaignBidModifierService");
                CampaignGroupService =
                    AdWordsService.MakeServiceSignature("v201809", "cm", "CampaignGroupService");
                CampaignGroupPerformanceTargetService =
                    AdWordsService.MakeServiceSignature("v201809", "cm",
                        "CampaignGroupPerformanceTargetService");
                CampaignSharedSetService =
                    AdWordsService.MakeServiceSignature("v201809", "cm", "CampaignSharedSetService");
                ConstantDataService =
                    AdWordsService.MakeServiceSignature("v201809", "cm", "ConstantDataService");
                ConversionTrackerService =
                    AdWordsService.MakeServiceSignature("v201809", "cm", "ConversionTrackerService");
                CustomerNegativeCriterionService =
                    AdWordsService.MakeServiceSignature("v201809", "cm",
                        "CustomerNegativeCriterionService");
                CustomerFeedService =
                    AdWordsService.MakeServiceSignature("v201809", "cm", "CustomerFeedService");
                DataService =
                    AdWordsService.MakeServiceSignature("v201809", "cm", "DataService");
                FeedItemService =
                    AdWordsService.MakeServiceSignature("v201809", "cm", "FeedItemService");
                FeedMappingService =
                    AdWordsService.MakeServiceSignature("v201809", "cm", "FeedMappingService");
                FeedService =
                    AdWordsService.MakeServiceSignature("v201809", "cm", "FeedService");
                FeedItemTargetService =
                    AdWordsService.MakeServiceSignature("v201809", "cm", "FeedItemTargetService");
                LabelService =
                    AdWordsService.MakeServiceSignature("v201809", "cm", "LabelService");
                LocationCriterionService =
                    AdWordsService.MakeServiceSignature("v201809", "cm", "LocationCriterionService");
                MediaService =
                    AdWordsService.MakeServiceSignature("v201809", "cm", "MediaService");
                OfflineConversionAdjustmentFeedService =
                    AdWordsService.MakeServiceSignature("v201809", "cm",
                        "OfflineConversionAdjustmentFeedService");
                OfflineConversionFeedService =
                    AdWordsService.MakeServiceSignature("v201809", "cm", "OfflineConversionFeedService");
                OfflineCallConversionFeedService =
                    AdWordsService.MakeServiceSignature("v201809", "cm",
                        "OfflineCallConversionFeedService");
                ReportDefinitionService =
                    AdWordsService.MakeServiceSignature("v201809", "cm", "ReportDefinitionService");
                SharedCriterionService =
                    AdWordsService.MakeServiceSignature("v201809", "cm", "SharedCriterionService");
                SharedSetService =
                    AdWordsService.MakeServiceSignature("v201809", "cm", "SharedSetService");

                DraftService =
                    AdWordsService.MakeServiceSignature("v201809", "cm", "DraftService");
                DraftAsyncErrorService =
                    AdWordsService.MakeServiceSignature("v201809", "cm", "DraftAsyncErrorService");
                TrialService =
                    AdWordsService.MakeServiceSignature("v201809", "cm", "TrialService");
                TrialAsyncErrorService =
                    AdWordsService.MakeServiceSignature("v201809", "cm", "TrialAsyncErrorService");

                #endregion Campaign Management.

                #region Blling.

                BudgetOrderService =
                     AdWordsService.MakeServiceSignature("v201809", "billing", "BudgetOrderService");

                #endregion Blling.

                #region Remarketing.

                AdwordsUserListService =
                    AdWordsService.MakeServiceSignature("v201809", "rm", "AdwordsUserListService");

                OfflineDataUploadService =
                    AdWordsService.MakeServiceSignature("v201809", "rm", "OfflineDataUploadService");

                CustomAffinityService =
                    AdWordsService.MakeServiceSignature("v201809", "rm", "CustomAffinityService");

                #endregion Remarketing.

                #region Optimization.

                TargetingIdeaService =
                    AdWordsService.MakeServiceSignature("v201809", "o", "TargetingIdeaService");
                TrafficEstimatorService =
                    AdWordsService.MakeServiceSignature("v201809", "o", "TrafficEstimatorService");

                #endregion Optimization.

                #region Change History.

                CustomerSyncService =
                    AdWordsService.MakeServiceSignature("v201809", "ch", "CustomerSyncService");

                #endregion Change History.

                #region Account Management.

                AccountLabelService =
                    AdWordsService.MakeServiceSignature("v201809", "mcm", "AccountLabelService");

                CustomerService =
                    AdWordsService.MakeServiceSignature("v201809", "mcm", "CustomerService");
                ManagedCustomerService =
                    AdWordsService.MakeServiceSignature("v201809", "mcm", "ManagedCustomerService");

                #endregion Account Management.

                #region Extension setting

                AdCustomizerFeedService =
                    AdWordsService.MakeServiceSignature("v201809", "cm", "AdCustomizerFeedService");
                AdGroupExtensionSettingService =
                    AdWordsService.MakeServiceSignature("v201809", "cm", "AdGroupExtensionSettingService");

                CampaignExtensionSettingService =
                    AdWordsService.MakeServiceSignature("v201809", "cm",
                        "CampaignExtensionSettingService");
                CustomerExtensionSettingService =
                    AdWordsService.MakeServiceSignature("v201809", "cm",
                        "CustomerExtensionSettingService");

                #endregion Extension setting
            }
        }
    }
}
