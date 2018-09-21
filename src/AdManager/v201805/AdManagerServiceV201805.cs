// Copyright 2018, Google Inc. All Rights Reserved.
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
        /// All the services available in v201805.
        /// </summary>
        public class v201805
        {
            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/ActivityGroupService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ActivityGroupService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/ActivityService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ActivityService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/AdExclusionRuleService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature AdExclusionRuleService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/AdRuleService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature AdRuleService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/BaseRateService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature BaseRateService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/AudienceSegmentService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature AudienceSegmentService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/CdnConfigurationService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature CdnConfigurationService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/ContactService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ContactService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/CompanyService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature CompanyService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/ContentBundleService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ContentBundleService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/ContentService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ContentService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/ContentMetadataKeyHierarchyService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ContentMetadataKeyHierarchyService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/CreativeService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature CreativeService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/CreativeSetService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature CreativeSetService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/CreativeTemplateService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature CreativeTemplateService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/CreativeWrapperService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature CreativeWrapperService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/CustomFieldService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature CustomFieldService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/CustomTargetingService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature CustomTargetingService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/DaiAuthenticationKeyService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature DaiAuthenticationKeyService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/ExchangeRateService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ExchangeRateService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/ForecastService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ForecastService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/InventoryService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature InventoryService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/LabelService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature LabelService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/LineItemTemplateService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature LineItemTemplateService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/LineItemService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature LineItemService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/LineItemCreativeAssociationService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature LineItemCreativeAssociationService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/LiveStreamEventService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature LiveStreamEventService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/MobileApplicationService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature MobileApplicationService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/NativeStyleService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature NativeStyleService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/NetworkService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature NetworkService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/OrderService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature OrderService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/PackageService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature PackageService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/ProductPackageItemService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ProductPackageItemService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/ProductPackageService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ProductPackageService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/PlacementService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature PlacementService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/PremiumRateService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature PremiumRateService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/ProductService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature ProductService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/ProductTemplateService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature ProductTemplateService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/ProposalService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature ProposalService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/ProposalLineItemService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature ProposalLineItemService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/PublisherQueryLanguageService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature PublisherQueryLanguageService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/RateCardService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature RateCardService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/ReconciliationLineItemReportService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ReconciliationLineItemReportService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/ReconciliationOrderReportService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ReconciliationOrderReportService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/ReconciliationReportService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ReconciliationReportService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/ReconciliationReportRowService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ReconciliationReportRowService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/ReportService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ReportService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/SuggestedAdUnitService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature SuggestedAdUnitService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/TeamService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature TeamService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/UserService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature UserService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/UserTeamAssociationService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature UserTeamAssociationService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201805/WorkflowRequestService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature WorkflowRequestService;

            /// <summary>
            /// Factory type for v201805 services.
            /// </summary>
            public static readonly Type factoryType = typeof(AdManagerServiceFactory);

            /// <summary>
            /// Static constructor to initialize the service constants.
            /// </summary>
            static v201805()
            {
                ActivityGroupService =
                    AdManagerService.MakeServiceSignature("v201805", "ActivityGroupService");
                ActivityService =
                    AdManagerService.MakeServiceSignature("v201805", "ActivityService");
                AdExclusionRuleService =
                    AdManagerService.MakeServiceSignature("v201805", "AdExclusionRuleService");
                AdRuleService = AdManagerService.MakeServiceSignature("v201805", "AdRuleService");
                BaseRateService =
                    AdManagerService.MakeServiceSignature("v201805", "BaseRateService");
                ContactService = AdManagerService.MakeServiceSignature("v201805", "ContactService");
                AudienceSegmentService =
                    AdManagerService.MakeServiceSignature("v201805", "AudienceSegmentService");
                CdnConfigurationService =
                    AdManagerService.MakeServiceSignature("v201805", "CdnConfigurationService");
                CompanyService = AdManagerService.MakeServiceSignature("v201805", "CompanyService");
                ContentBundleService =
                    AdManagerService.MakeServiceSignature("v201805", "ContentBundleService");
                ContentService = AdManagerService.MakeServiceSignature("v201805", "ContentService");
                ContentMetadataKeyHierarchyService =
                    AdManagerService.MakeServiceSignature("v201805",
                        "ContentMetadataKeyHierarchyService");
                CreativeService =
                    AdManagerService.MakeServiceSignature("v201805", "CreativeService");
                CreativeSetService =
                    AdManagerService.MakeServiceSignature("v201805", "CreativeSetService");
                CreativeTemplateService =
                    AdManagerService.MakeServiceSignature("v201805", "CreativeTemplateService");
                CreativeWrapperService =
                    AdManagerService.MakeServiceSignature("v201805", "CreativeWrapperService");
                CustomTargetingService =
                    AdManagerService.MakeServiceSignature("v201805", "CustomTargetingService");
                CustomFieldService =
                    AdManagerService.MakeServiceSignature("v201805", "CustomFieldService");
                DaiAuthenticationKeyService =
                    AdManagerService.MakeServiceSignature("v201805", "DaiAuthenticationKeyService");
                ExchangeRateService =
                    AdManagerService.MakeServiceSignature("v201805", "ExchangeRateService");
                ForecastService =
                    AdManagerService.MakeServiceSignature("v201805", "ForecastService");
                InventoryService =
                    AdManagerService.MakeServiceSignature("v201805", "InventoryService");
                LabelService = AdManagerService.MakeServiceSignature("v201805", "LabelService");
                LineItemTemplateService =
                    AdManagerService.MakeServiceSignature("v201805", "LineItemTemplateService");
                LineItemService =
                    AdManagerService.MakeServiceSignature("v201805", "LineItemService");
                LineItemCreativeAssociationService =
                    AdManagerService.MakeServiceSignature("v201805",
                        "LineItemCreativeAssociationService");
                LiveStreamEventService =
                    AdManagerService.MakeServiceSignature("v201805", "LiveStreamEventService");
                MobileApplicationService =
                    AdManagerService.MakeServiceSignature("v201805", "MobileApplicationService");
                NativeStyleService =
                    AdManagerService.MakeServiceSignature("v201805", "NativeStyleService");
                NetworkService = AdManagerService.MakeServiceSignature("v201805", "NetworkService");
                OrderService = AdManagerService.MakeServiceSignature("v201805", "OrderService");
                PackageService = AdManagerService.MakeServiceSignature("v201805", "PackageService");
                ProductPackageService =
                    AdManagerService.MakeServiceSignature("v201805", "ProductPackageService");
                ProductPackageItemService =
                    AdManagerService.MakeServiceSignature("v201805", "ProductPackageItemService");
                PlacementService =
                    AdManagerService.MakeServiceSignature("v201805", "PlacementService");
                PremiumRateService =
                    AdManagerService.MakeServiceSignature("v201805", "PremiumRateService");
                ProductService = AdManagerService.MakeServiceSignature("v201805", "ProductService");
                ProductTemplateService =
                    AdManagerService.MakeServiceSignature("v201805", "ProductTemplateService");
                ProposalService =
                    AdManagerService.MakeServiceSignature("v201805", "ProposalService");
                ProposalLineItemService =
                    AdManagerService.MakeServiceSignature("v201805", "ProposalLineItemService");
                PublisherQueryLanguageService =
                    AdManagerService.MakeServiceSignature("v201805",
                        "PublisherQueryLanguageService");
                RateCardService =
                    AdManagerService.MakeServiceSignature("v201805", "RateCardService");
                ReconciliationLineItemReportService =
                    AdManagerService.MakeServiceSignature("v201805",
                        "ReconciliationLineItemReportService");
                ReconciliationOrderReportService =
                    AdManagerService.MakeServiceSignature("v201805",
                        "ReconciliationOrderReportService");
                ReconciliationReportService =
                    AdManagerService.MakeServiceSignature("v201805", "ReconciliationReportService");
                ReconciliationReportRowService =
                    AdManagerService.MakeServiceSignature("v201805",
                        "ReconciliationReportRowService");
                ReportService = AdManagerService.MakeServiceSignature("v201805", "ReportService");
                SuggestedAdUnitService =
                    AdManagerService.MakeServiceSignature("v201805", "SuggestedAdUnitService");
                TeamService = AdManagerService.MakeServiceSignature("v201805", "TeamService");
                UserService = AdManagerService.MakeServiceSignature("v201805", "UserService");
                UserTeamAssociationService =
                    AdManagerService.MakeServiceSignature("v201805", "UserTeamAssociationService");
                WorkflowRequestService =
                    AdManagerService.MakeServiceSignature("v201805", "WorkflowRequestService");
            }
        }
    }
}
