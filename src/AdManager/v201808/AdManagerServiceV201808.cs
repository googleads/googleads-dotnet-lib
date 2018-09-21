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
        /// All the services available in v201808.
        /// </summary>
        public class v201808
        {
            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/ActivityGroupService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ActivityGroupService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/ActivityService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ActivityService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/AdExclusionRuleService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature AdExclusionRuleService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/AdRuleService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature AdRuleService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/BaseRateService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature BaseRateService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/AudienceSegmentService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature AudienceSegmentService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/CdnConfigurationService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature CdnConfigurationService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/ContactService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ContactService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/CompanyService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature CompanyService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/ContentBundleService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ContentBundleService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/ContentService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ContentService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/ContentMetadataKeyHierarchyService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ContentMetadataKeyHierarchyService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/CreativeService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature CreativeService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/CreativeSetService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature CreativeSetService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/CreativeTemplateService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature CreativeTemplateService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/CreativeWrapperService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature CreativeWrapperService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/CustomFieldService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature CustomFieldService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/CustomTargetingService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature CustomTargetingService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/DaiAuthenticationKeyService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature DaiAuthenticationKeyService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/ExchangeRateService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ExchangeRateService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/ForecastService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ForecastService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/InventoryService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature InventoryService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/LabelService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature LabelService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/LineItemTemplateService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature LineItemTemplateService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/LineItemService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature LineItemService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/LineItemCreativeAssociationService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature LineItemCreativeAssociationService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/LiveStreamEventService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature LiveStreamEventService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/MobileApplicationService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature MobileApplicationService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/NativeStyleService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature NativeStyleService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/NetworkService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature NetworkService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/OrderService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature OrderService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/PackageService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature PackageService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/ProductPackageItemService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ProductPackageItemService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/ProductPackageService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ProductPackageService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/PlacementService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature PlacementService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/PremiumRateService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature PremiumRateService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/ProductService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature ProductService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/ProductTemplateService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature ProductTemplateService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/ProposalService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature ProposalService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/ProposalLineItemService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature ProposalLineItemService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/PublisherQueryLanguageService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature PublisherQueryLanguageService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/RateCardService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature RateCardService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/ReconciliationLineItemReportService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ReconciliationLineItemReportService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/ReconciliationOrderReportService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ReconciliationOrderReportService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/ReconciliationReportService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ReconciliationReportService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/ReconciliationReportRowService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ReconciliationReportRowService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/ReportService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ReportService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/SuggestedAdUnitService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature SuggestedAdUnitService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/TeamService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature TeamService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/UserService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature UserService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/UserTeamAssociationService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature UserTeamAssociationService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201808/WorkflowRequestService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature WorkflowRequestService;

            /// <summary>
            /// Factory type for v201808 services.
            /// </summary>
            public static readonly Type factoryType = typeof(AdManagerServiceFactory);

            /// <summary>
            /// Static constructor to initialize the service constants.
            /// </summary>
            static v201808()
            {
                ActivityGroupService =
                    AdManagerService.MakeServiceSignature("v201808", "ActivityGroupService");
                ActivityService =
                    AdManagerService.MakeServiceSignature("v201808", "ActivityService");
                AdExclusionRuleService =
                    AdManagerService.MakeServiceSignature("v201808", "AdExclusionRuleService");
                AdRuleService = AdManagerService.MakeServiceSignature("v201808", "AdRuleService");
                BaseRateService =
                    AdManagerService.MakeServiceSignature("v201808", "BaseRateService");
                ContactService = AdManagerService.MakeServiceSignature("v201808", "ContactService");
                AudienceSegmentService =
                    AdManagerService.MakeServiceSignature("v201808", "AudienceSegmentService");
                CdnConfigurationService =
                    AdManagerService.MakeServiceSignature("v201808", "CdnConfigurationService");
                CompanyService = AdManagerService.MakeServiceSignature("v201808", "CompanyService");
                ContentBundleService =
                    AdManagerService.MakeServiceSignature("v201808", "ContentBundleService");
                ContentService = AdManagerService.MakeServiceSignature("v201808", "ContentService");
                ContentMetadataKeyHierarchyService =
                    AdManagerService.MakeServiceSignature("v201808",
                        "ContentMetadataKeyHierarchyService");
                CreativeService =
                    AdManagerService.MakeServiceSignature("v201808", "CreativeService");
                CreativeSetService =
                    AdManagerService.MakeServiceSignature("v201808", "CreativeSetService");
                CreativeTemplateService =
                    AdManagerService.MakeServiceSignature("v201808", "CreativeTemplateService");
                CreativeWrapperService =
                    AdManagerService.MakeServiceSignature("v201808", "CreativeWrapperService");
                CustomTargetingService =
                    AdManagerService.MakeServiceSignature("v201808", "CustomTargetingService");
                CustomFieldService =
                    AdManagerService.MakeServiceSignature("v201808", "CustomFieldService");
                DaiAuthenticationKeyService =
                    AdManagerService.MakeServiceSignature("v201808", "DaiAuthenticationKeyService");
                ExchangeRateService =
                    AdManagerService.MakeServiceSignature("v201808", "ExchangeRateService");
                ForecastService =
                    AdManagerService.MakeServiceSignature("v201808", "ForecastService");
                InventoryService =
                    AdManagerService.MakeServiceSignature("v201808", "InventoryService");
                LabelService = AdManagerService.MakeServiceSignature("v201808", "LabelService");
                LineItemTemplateService =
                    AdManagerService.MakeServiceSignature("v201808", "LineItemTemplateService");
                LineItemService =
                    AdManagerService.MakeServiceSignature("v201808", "LineItemService");
                LineItemCreativeAssociationService =
                    AdManagerService.MakeServiceSignature("v201808",
                        "LineItemCreativeAssociationService");
                LiveStreamEventService =
                    AdManagerService.MakeServiceSignature("v201808", "LiveStreamEventService");
                MobileApplicationService =
                    AdManagerService.MakeServiceSignature("v201808", "MobileApplicationService");
                NativeStyleService =
                    AdManagerService.MakeServiceSignature("v201808", "NativeStyleService");
                NetworkService = AdManagerService.MakeServiceSignature("v201808", "NetworkService");
                OrderService = AdManagerService.MakeServiceSignature("v201808", "OrderService");
                PackageService = AdManagerService.MakeServiceSignature("v201808", "PackageService");
                ProductPackageService =
                    AdManagerService.MakeServiceSignature("v201808", "ProductPackageService");
                ProductPackageItemService =
                    AdManagerService.MakeServiceSignature("v201808", "ProductPackageItemService");
                PlacementService =
                    AdManagerService.MakeServiceSignature("v201808", "PlacementService");
                PremiumRateService =
                    AdManagerService.MakeServiceSignature("v201808", "PremiumRateService");
                ProductService = AdManagerService.MakeServiceSignature("v201808", "ProductService");
                ProductTemplateService =
                    AdManagerService.MakeServiceSignature("v201808", "ProductTemplateService");
                ProposalService =
                    AdManagerService.MakeServiceSignature("v201808", "ProposalService");
                ProposalLineItemService =
                    AdManagerService.MakeServiceSignature("v201808", "ProposalLineItemService");
                PublisherQueryLanguageService =
                    AdManagerService.MakeServiceSignature("v201808",
                        "PublisherQueryLanguageService");
                RateCardService =
                    AdManagerService.MakeServiceSignature("v201808", "RateCardService");
                ReconciliationLineItemReportService =
                    AdManagerService.MakeServiceSignature("v201808",
                        "ReconciliationLineItemReportService");
                ReconciliationOrderReportService =
                    AdManagerService.MakeServiceSignature("v201808",
                        "ReconciliationOrderReportService");
                ReconciliationReportService =
                    AdManagerService.MakeServiceSignature("v201808", "ReconciliationReportService");
                ReconciliationReportRowService =
                    AdManagerService.MakeServiceSignature("v201808",
                        "ReconciliationReportRowService");
                ReportService = AdManagerService.MakeServiceSignature("v201808", "ReportService");
                SuggestedAdUnitService =
                    AdManagerService.MakeServiceSignature("v201808", "SuggestedAdUnitService");
                TeamService = AdManagerService.MakeServiceSignature("v201808", "TeamService");
                UserService = AdManagerService.MakeServiceSignature("v201808", "UserService");
                UserTeamAssociationService =
                    AdManagerService.MakeServiceSignature("v201808", "UserTeamAssociationService");
                WorkflowRequestService =
                    AdManagerService.MakeServiceSignature("v201808", "WorkflowRequestService");
            }
        }
    }
}
