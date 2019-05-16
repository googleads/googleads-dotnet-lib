// Copyright 2019, Google Inc. All Rights Reserved.
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
using System.Collections.Generic;
using System.Reflection;

namespace Google.Api.Ads.AdManager.Lib
{
    /// <summary>
    /// Lists all the services available through this library.
    /// </summary>
    public partial class AdManagerService : AdsService
    {
        /// <summary>
        /// All the services available in v201905.
        /// </summary>
        public class v201905
        {
            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/ActivityGroupService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ActivityGroupService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/ActivityService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ActivityService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/AdExclusionRuleService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature AdExclusionRuleService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/AdjustmentRuleService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature AdjustmentService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/AdRuleService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature AdRuleService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/BaseRateService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature BaseRateService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/AudienceSegmentService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature AudienceSegmentService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/CdnConfigurationService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature CdnConfigurationService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/CmsMetadataService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature CmsMetadataService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/ContactService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ContactService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/CompanyService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature CompanyService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/ContentBundleService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ContentBundleService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/ContentService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ContentService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/CreativeService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature CreativeService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/CreativeSetService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature CreativeSetService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/CreativeTemplateService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature CreativeTemplateService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/CreativeWrapperService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature CreativeWrapperService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/CustomFieldService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature CustomFieldService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/CustomTargetingService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature CustomTargetingService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/DaiAuthenticationKeyService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature DaiAuthenticationKeyService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/ExchangeRateService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ExchangeRateService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/ForecastService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ForecastService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/InventoryService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature InventoryService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/LabelService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature LabelService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/LineItemTemplateService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature LineItemTemplateService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/LineItemService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature LineItemService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/LineItemCreativeAssociationService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature LineItemCreativeAssociationService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/LiveStreamEventService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature LiveStreamEventService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/MobileApplicationService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature MobileApplicationService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/NativeStyleService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature NativeStyleService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/NetworkService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature NetworkService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/OrderService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature OrderService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/PackageService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature PackageService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/ProductPackageItemService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ProductPackageItemService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/ProductPackageService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ProductPackageService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/PlacementService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature PlacementService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/PremiumRateService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature PremiumRateService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/ProductService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature ProductService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/ProductTemplateService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature ProductTemplateService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/ProposalService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature ProposalService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/ProposalLineItemService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature ProposalLineItemService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/PublisherQueryLanguageService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature PublisherQueryLanguageService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/RateCardService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature RateCardService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/ReconciliationLineItemReportService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ReconciliationLineItemReportService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/ReconciliationOrderReportService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ReconciliationOrderReportService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/ReconciliationReportService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ReconciliationReportService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/ReconciliationReportRowService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ReconciliationReportRowService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/ReportService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ReportService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/SuggestedAdUnitService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature SuggestedAdUnitService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/TargetingPresetService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature TargetingPresetService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/TeamService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature TeamService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/UserService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature UserService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/UserTeamAssociationService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature UserTeamAssociationService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201905/WorkflowRequestService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature WorkflowRequestService;

            /// <summary>
            /// Factory type for v201905 services.
            /// </summary>
            public static readonly Type factoryType = typeof(AdManagerServiceFactory);

            /// <summary>
            /// Static constructor to initialize the service constants.
            /// </summary>
            static v201905()
            {
                ActivityGroupService =
                    AdManagerService.MakeServiceSignature("v201905", "ActivityGroupService");
                ActivityService =
                    AdManagerService.MakeServiceSignature("v201905", "ActivityService");
                AdExclusionRuleService =
                    AdManagerService.MakeServiceSignature("v201905", "AdExclusionRuleService");
                AdjustmentService =
                    AdManagerService.MakeServiceSignature("v201905", "AdjustmentService");
                AdRuleService = AdManagerService.MakeServiceSignature("v201905", "AdRuleService");
                BaseRateService =
                    AdManagerService.MakeServiceSignature("v201905", "BaseRateService");
                ContactService = AdManagerService.MakeServiceSignature("v201905", "ContactService");
                AudienceSegmentService =
                    AdManagerService.MakeServiceSignature("v201905", "AudienceSegmentService");
                CdnConfigurationService =
                    AdManagerService.MakeServiceSignature("v201905", "CdnConfigurationService");
                CmsMetadataService =
                    AdManagerService.MakeServiceSignature("v201905", "CmsMetadataService");
                CompanyService = AdManagerService.MakeServiceSignature("v201905", "CompanyService");
                ContentBundleService =
                    AdManagerService.MakeServiceSignature("v201905", "ContentBundleService");
                ContentService = AdManagerService.MakeServiceSignature("v201905", "ContentService");
                CreativeService =
                    AdManagerService.MakeServiceSignature("v201905", "CreativeService");
                CreativeSetService =
                    AdManagerService.MakeServiceSignature("v201905", "CreativeSetService");
                CreativeTemplateService =
                    AdManagerService.MakeServiceSignature("v201905", "CreativeTemplateService");
                CreativeWrapperService =
                    AdManagerService.MakeServiceSignature("v201905", "CreativeWrapperService");
                CustomTargetingService =
                    AdManagerService.MakeServiceSignature("v201905", "CustomTargetingService");
                CustomFieldService =
                    AdManagerService.MakeServiceSignature("v201905", "CustomFieldService");
                DaiAuthenticationKeyService =
                    AdManagerService.MakeServiceSignature("v201905", "DaiAuthenticationKeyService");
                ExchangeRateService =
                    AdManagerService.MakeServiceSignature("v201905", "ExchangeRateService");
                ForecastService =
                    AdManagerService.MakeServiceSignature("v201905", "ForecastService");
                InventoryService =
                    AdManagerService.MakeServiceSignature("v201905", "InventoryService");
                LabelService = AdManagerService.MakeServiceSignature("v201905", "LabelService");
                LineItemTemplateService =
                    AdManagerService.MakeServiceSignature("v201905", "LineItemTemplateService");
                LineItemService =
                    AdManagerService.MakeServiceSignature("v201905", "LineItemService");
                LineItemCreativeAssociationService =
                    AdManagerService.MakeServiceSignature("v201905",
                        "LineItemCreativeAssociationService");
                LiveStreamEventService =
                    AdManagerService.MakeServiceSignature("v201905", "LiveStreamEventService");
                MobileApplicationService =
                    AdManagerService.MakeServiceSignature("v201905", "MobileApplicationService");
                NativeStyleService =
                    AdManagerService.MakeServiceSignature("v201905", "NativeStyleService");
                NetworkService = AdManagerService.MakeServiceSignature("v201905", "NetworkService");
                OrderService = AdManagerService.MakeServiceSignature("v201905", "OrderService");
                PackageService = AdManagerService.MakeServiceSignature("v201905", "PackageService");
                ProductPackageService =
                    AdManagerService.MakeServiceSignature("v201905", "ProductPackageService");
                ProductPackageItemService =
                    AdManagerService.MakeServiceSignature("v201905", "ProductPackageItemService");
                PlacementService =
                    AdManagerService.MakeServiceSignature("v201905", "PlacementService");
                PremiumRateService =
                    AdManagerService.MakeServiceSignature("v201905", "PremiumRateService");
                ProductService = AdManagerService.MakeServiceSignature("v201905", "ProductService");
                ProductTemplateService =
                    AdManagerService.MakeServiceSignature("v201905", "ProductTemplateService");
                ProposalService =
                    AdManagerService.MakeServiceSignature("v201905", "ProposalService");
                ProposalLineItemService =
                    AdManagerService.MakeServiceSignature("v201905", "ProposalLineItemService");
                PublisherQueryLanguageService =
                    AdManagerService.MakeServiceSignature("v201905",
                        "PublisherQueryLanguageService");
                RateCardService =
                    AdManagerService.MakeServiceSignature("v201905", "RateCardService");
                ReconciliationLineItemReportService =
                    AdManagerService.MakeServiceSignature("v201905",
                        "ReconciliationLineItemReportService");
                ReconciliationOrderReportService =
                    AdManagerService.MakeServiceSignature("v201905",
                        "ReconciliationOrderReportService");
                ReconciliationReportService =
                    AdManagerService.MakeServiceSignature("v201905", "ReconciliationReportService");
                ReconciliationReportRowService =
                    AdManagerService.MakeServiceSignature("v201905",
                        "ReconciliationReportRowService");
                ReportService = AdManagerService.MakeServiceSignature("v201905", "ReportService");
                SuggestedAdUnitService =
                    AdManagerService.MakeServiceSignature("v201905", "SuggestedAdUnitService");
                TargetingPresetService = AdManagerService.MakeServiceSignature("v201905", "TargetingPresetService");
                TeamService = AdManagerService.MakeServiceSignature("v201905", "TeamService");
                UserService = AdManagerService.MakeServiceSignature("v201905", "UserService");
                UserTeamAssociationService =
                    AdManagerService.MakeServiceSignature("v201905", "UserTeamAssociationService");
                WorkflowRequestService =
                    AdManagerService.MakeServiceSignature("v201905", "WorkflowRequestService");
            }
        }
    }
}
