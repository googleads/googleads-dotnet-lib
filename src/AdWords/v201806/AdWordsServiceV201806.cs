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
        /// All the services available in v201806.
        /// </summary>
        public class v201806
        {
            #region Campaign Management.

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201806/AdGroupAdService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature AdGroupAdService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201806/AdGroupBidModifierService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature AdGroupBidModifierService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201806/AdGroupCriterionService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature AdGroupCriterionService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201806/AdGroupFeedService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature AdGroupFeedService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201806/AdGroupService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature AdGroupService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201806/AdParamService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature AdParamService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201806/AssetService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature AssetService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201806/AdService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature AdService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201806/BatchJobService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature BatchJobService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201806/BiddingStrategyService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature BiddingStrategyService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201806/BudgetService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature BudgetService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201806/CampaignCriterionService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature CampaignCriterionService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201806/CampaignFeedService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature CampaignFeedService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201806/CampaignBidModifierService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature CampaignBidModifierService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201806/CampaignGroupService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature CampaignGroupService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201806/CampaignGroupPerformanceTargetService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature CampaignGroupPerformanceTargetService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201806/CampaignService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature CampaignService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/cm/v201806/CampaignSharedSetService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature CampaignSharedSetService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201806/ConstantDataService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ConstantDataService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201806/ConversionTrackerService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ConversionTrackerService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201806/CustomerFeedService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature CustomerFeedService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201806/CustomerNegativeCriterionService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature CustomerNegativeCriterionService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201806/DataService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature DataService;

            /// <summary>
            /// Factory type for v201806 services.
            /// </summary>
            public static readonly Type factoryType = typeof(AdWordsServiceFactory);

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201806/FeedItemService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature FeedItemService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201806/FeedMappingService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature FeedMappingService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201806/FeedService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature FeedService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201806/FeedItemTargetService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature FeedItemTargetService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/cm/v201806/SharedSetService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature LabelService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/cm/v201806/AccountLabelService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature AccountLabelService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201806/LocationCriterionService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature LocationCriterionService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201806/MediaService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature MediaService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201806/OfflineConversionFeedService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature OfflineConversionFeedService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201806/OfflineConversionFeedService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature OfflineCallConversionFeedService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201806/ReportDefinitionService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ReportDefinitionService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/cm/v201806/SharedCriterionService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature SharedCriterionService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/cm/v201806/SharedCriterionService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature SharedSetService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/cm/v201806/DraftService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature DraftService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/cm/v201806/DraftAsyncErrorService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature DraftAsyncErrorService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201806/OfflineConversionAdjustmentFeedService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature OfflineConversionAdjustmentFeedService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/cm/v201806/TrialService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature TrialService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/cm/v201806/TrialAsyncErrorService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature TrialAsyncErrorService;

            #endregion

            #region Billing.

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201806/BudgetOrderService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature BudgetOrderService;

            #endregion

            #region Remarketing.

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201806/AdwordsUserListService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature AdwordsUserListService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201806/OfflineDataUploadService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature OfflineDataUploadService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201806/CustomAffinityService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature CustomAffinityService;

            #endregion

            #region Optimization

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201806/TargetingIdeaService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature TargetingIdeaService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201806/TrafficEstimatorService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature TrafficEstimatorService;

            #endregion

            #region Account Management.

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201806/CustomerService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature CustomerService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201806/ManagedCustomerService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ManagedCustomerService;

            #endregion

            #region Change history.

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201806/CustomerSyncService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature CustomerSyncService;

            #endregion

            #region Extension setting

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201806/AdCustomizerFeedService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature AdCustomizerFeedService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201806/AdGroupExtensionSettingService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature AdGroupExtensionSettingService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201806/CampaignExtensionSettingService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature CampaignExtensionSettingService;

            /// <summary>
            /// See <a href="https://developers.google.com/adwords/api/docs/reference/v201806/CustomerExtensionSettingService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature CustomerExtensionSettingService;

            #endregion

            /// <summary>
            /// Static constructor to initialize the service constants.
            /// </summary>
            static v201806()
            {
                #region Campaign Management.

                AdGroupAdService =
                    AdWordsService.MakeServiceSignature("v201806", "cm", "AdGroupAdService");
                AdGroupBidModifierService =
                    AdWordsService.MakeServiceSignature("v201806", "cm",
                        "AdGroupBidModifierService");
                AdGroupCriterionService =
                    AdWordsService.MakeServiceSignature("v201806", "cm", "AdGroupCriterionService");
                AdGroupFeedService =
                    AdWordsService.MakeServiceSignature("v201806", "cm", "AdGroupFeedService");
                AdGroupService =
                    AdWordsService.MakeServiceSignature("v201806", "cm", "AdGroupService");
                AssetService = AdWordsService.MakeServiceSignature("v201806", "cm", "AssetService");
                AdService = AdWordsService.MakeServiceSignature("v201806", "cm", "AdService");
                AdParamService =
                    AdWordsService.MakeServiceSignature("v201806", "cm", "AdParamService");
                BatchJobService =
                    AdWordsService.MakeServiceSignature("v201806", "cm", "BatchJobService");
                BiddingStrategyService =
                    AdWordsService.MakeServiceSignature("v201806", "cm", "BiddingStrategyService");
                BudgetService =
                    AdWordsService.MakeServiceSignature("v201806", "cm", "BudgetService");
                CampaignCriterionService =
                    AdWordsService.MakeServiceSignature("v201806", "cm",
                        "CampaignCriterionService");
                CampaignFeedService =
                    AdWordsService.MakeServiceSignature("v201806", "cm", "CampaignFeedService");
                CampaignService =
                    AdWordsService.MakeServiceSignature("v201806", "cm", "CampaignService");
                CampaignBidModifierService =
                    AdWordsService.MakeServiceSignature("v201806", "cm",
                        "CampaignBidModifierService");
                CampaignGroupService =
                    AdWordsService.MakeServiceSignature("v201806", "cm", "CampaignGroupService");
                CampaignGroupPerformanceTargetService =
                    AdWordsService.MakeServiceSignature("v201806", "cm",
                        "CampaignGroupPerformanceTargetService");
                CampaignSharedSetService =
                    AdWordsService.MakeServiceSignature("v201806", "cm",
                        "CampaignSharedSetService");
                ConstantDataService =
                    AdWordsService.MakeServiceSignature("v201806", "cm", "ConstantDataService");
                ConversionTrackerService =
                    AdWordsService.MakeServiceSignature("v201806", "cm",
                        "ConversionTrackerService");
                CustomerNegativeCriterionService =
                    AdWordsService.MakeServiceSignature("v201806", "cm",
                        "CustomerNegativeCriterionService");
                CustomerFeedService =
                    AdWordsService.MakeServiceSignature("v201806", "cm", "CustomerFeedService");
                DataService = AdWordsService.MakeServiceSignature("v201806", "cm", "DataService");
                FeedItemService =
                    AdWordsService.MakeServiceSignature("v201806", "cm", "FeedItemService");
                FeedMappingService =
                    AdWordsService.MakeServiceSignature("v201806", "cm", "FeedMappingService");
                FeedService = AdWordsService.MakeServiceSignature("v201806", "cm", "FeedService");
                FeedItemTargetService =
                    AdWordsService.MakeServiceSignature("v201806", "cm", "FeedItemTargetService");
                LabelService = AdWordsService.MakeServiceSignature("v201806", "cm", "LabelService");
                LocationCriterionService =
                    AdWordsService.MakeServiceSignature("v201806", "cm",
                        "LocationCriterionService");
                MediaService = AdWordsService.MakeServiceSignature("v201806", "cm", "MediaService");
                OfflineConversionAdjustmentFeedService =
                    AdWordsService.MakeServiceSignature("v201806", "cm",
                        "OfflineConversionAdjustmentFeedService");
                OfflineConversionFeedService =
                    AdWordsService.MakeServiceSignature("v201806", "cm",
                        "OfflineConversionFeedService");
                OfflineCallConversionFeedService =
                    AdWordsService.MakeServiceSignature("v201806", "cm",
                        "OfflineCallConversionFeedService");
                ReportDefinitionService =
                    AdWordsService.MakeServiceSignature("v201806", "cm", "ReportDefinitionService");
                SharedCriterionService =
                    AdWordsService.MakeServiceSignature("v201806", "cm", "SharedCriterionService");
                SharedSetService =
                    AdWordsService.MakeServiceSignature("v201806", "cm", "SharedSetService");

                DraftService = AdWordsService.MakeServiceSignature("v201806", "cm", "DraftService");
                DraftAsyncErrorService =
                    AdWordsService.MakeServiceSignature("v201806", "cm", "DraftAsyncErrorService");
                TrialService = AdWordsService.MakeServiceSignature("v201806", "cm", "TrialService");
                TrialAsyncErrorService =
                    AdWordsService.MakeServiceSignature("v201806", "cm", "TrialAsyncErrorService");

                #endregion

                #region Blling.

                BudgetOrderService =
                    AdWordsService.MakeServiceSignature("v201806", "billing", "BudgetOrderService");

                #endregion

                #region Remarketing.

                AdwordsUserListService =
                    AdWordsService.MakeServiceSignature("v201806", "rm", "AdwordsUserListService");

                OfflineDataUploadService =
                    AdWordsService.MakeServiceSignature("v201806", "rm",
                        "OfflineDataUploadService");

                CustomAffinityService =
                    AdWordsService.MakeServiceSignature("v201806", "rm", "CustomAffinityService");

                #endregion

                #region Optimization.

                TargetingIdeaService =
                    AdWordsService.MakeServiceSignature("v201806", "o", "TargetingIdeaService");
                TrafficEstimatorService =
                    AdWordsService.MakeServiceSignature("v201806", "o", "TrafficEstimatorService");

                #endregion

                #region Change History.

                CustomerSyncService =
                    AdWordsService.MakeServiceSignature("v201806", "ch", "CustomerSyncService");

                #endregion

                #region Account Management.

                AccountLabelService =
                    AdWordsService.MakeServiceSignature("v201806", "mcm", "AccountLabelService");

                CustomerService =
                    AdWordsService.MakeServiceSignature("v201806", "mcm", "CustomerService");
                ManagedCustomerService =
                    AdWordsService.MakeServiceSignature("v201806", "mcm", "ManagedCustomerService");

                #endregion

                #region Extension setting

                AdCustomizerFeedService =
                    AdWordsService.MakeServiceSignature("v201806", "cm", "AdCustomizerFeedService");
                AdGroupExtensionSettingService =
                    AdWordsService.MakeServiceSignature("v201806", "cm",
                        "AdGroupExtensionSettingService");

                CampaignExtensionSettingService =
                    AdWordsService.MakeServiceSignature("v201806", "cm",
                        "CampaignExtensionSettingService");
                CustomerExtensionSettingService =
                    AdWordsService.MakeServiceSignature("v201806", "cm",
                        "CustomerExtensionSettingService");

                #endregion
            }
        }
    }
}
