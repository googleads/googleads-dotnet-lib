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
        /// All the services available in v201811.
        /// </summary>
        public class v201811
        {
            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/ActivityGroupService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ActivityGroupService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/ActivityService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ActivityService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/AdExclusionRuleService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature AdExclusionRuleService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/AdRuleService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature AdRuleService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/BaseRateService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature BaseRateService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/AudienceSegmentService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature AudienceSegmentService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/CdnConfigurationService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature CdnConfigurationService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/ContactService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ContactService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/CompanyService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature CompanyService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/ContentBundleService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ContentBundleService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/ContentService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ContentService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/CreativeService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature CreativeService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/CreativeSetService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature CreativeSetService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/CreativeTemplateService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature CreativeTemplateService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/CreativeWrapperService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature CreativeWrapperService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/CustomFieldService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature CustomFieldService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/CustomTargetingService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature CustomTargetingService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/DaiAuthenticationKeyService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature DaiAuthenticationKeyService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/ExchangeRateService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ExchangeRateService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/ForecastService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ForecastService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/InventoryService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature InventoryService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/LabelService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature LabelService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/LineItemTemplateService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature LineItemTemplateService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/LineItemService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature LineItemService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/LineItemCreativeAssociationService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature LineItemCreativeAssociationService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/LiveStreamEventService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature LiveStreamEventService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/MobileApplicationService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature MobileApplicationService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/NativeStyleService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature NativeStyleService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/NetworkService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature NetworkService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/OrderService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature OrderService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/PackageService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature PackageService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/ProductPackageItemService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ProductPackageItemService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/ProductPackageService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ProductPackageService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/PlacementService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature PlacementService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/PremiumRateService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature PremiumRateService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/ProductService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature ProductService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/ProductTemplateService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature ProductTemplateService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/ProposalService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature ProposalService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/ProposalLineItemService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature ProposalLineItemService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/PublisherQueryLanguageService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature PublisherQueryLanguageService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/RateCardService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature RateCardService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/ReconciliationLineItemReportService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ReconciliationLineItemReportService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/ReconciliationOrderReportService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ReconciliationOrderReportService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/ReconciliationReportService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ReconciliationReportService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/ReconciliationReportRowService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ReconciliationReportRowService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/ReportService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ReportService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/SuggestedAdUnitService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature SuggestedAdUnitService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/TeamService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature TeamService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/UserService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature UserService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/UserTeamAssociationService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature UserTeamAssociationService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201811/WorkflowRequestService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature WorkflowRequestService;

            /// <summary>
            /// Factory type for v201811 services.
            /// </summary>
            public static readonly Type factoryType = typeof(AdManagerServiceFactory);

            /// <summary>
            /// Static constructor to initialize the service constants.
            /// </summary>
            static v201811()
            {
                ActivityGroupService =
                    AdManagerService.MakeServiceSignature("v201811", "ActivityGroupService");
                ActivityService =
                    AdManagerService.MakeServiceSignature("v201811", "ActivityService");
                AdExclusionRuleService =
                    AdManagerService.MakeServiceSignature("v201811", "AdExclusionRuleService");
                AdRuleService = AdManagerService.MakeServiceSignature("v201811", "AdRuleService");
                BaseRateService =
                    AdManagerService.MakeServiceSignature("v201811", "BaseRateService");
                ContactService = AdManagerService.MakeServiceSignature("v201811", "ContactService");
                AudienceSegmentService =
                    AdManagerService.MakeServiceSignature("v201811", "AudienceSegmentService");
                CdnConfigurationService =
                    AdManagerService.MakeServiceSignature("v201811", "CdnConfigurationService");
                CompanyService = AdManagerService.MakeServiceSignature("v201811", "CompanyService");
                ContentBundleService =
                    AdManagerService.MakeServiceSignature("v201811", "ContentBundleService");
                ContentService = AdManagerService.MakeServiceSignature("v201811", "ContentService");
                CreativeService =
                    AdManagerService.MakeServiceSignature("v201811", "CreativeService");
                CreativeSetService =
                    AdManagerService.MakeServiceSignature("v201811", "CreativeSetService");
                CreativeTemplateService =
                    AdManagerService.MakeServiceSignature("v201811", "CreativeTemplateService");
                CreativeWrapperService =
                    AdManagerService.MakeServiceSignature("v201811", "CreativeWrapperService");
                CustomTargetingService =
                    AdManagerService.MakeServiceSignature("v201811", "CustomTargetingService");
                CustomFieldService =
                    AdManagerService.MakeServiceSignature("v201811", "CustomFieldService");
                DaiAuthenticationKeyService =
                    AdManagerService.MakeServiceSignature("v201811", "DaiAuthenticationKeyService");
                ExchangeRateService =
                    AdManagerService.MakeServiceSignature("v201811", "ExchangeRateService");
                ForecastService =
                    AdManagerService.MakeServiceSignature("v201811", "ForecastService");
                InventoryService =
                    AdManagerService.MakeServiceSignature("v201811", "InventoryService");
                LabelService = AdManagerService.MakeServiceSignature("v201811", "LabelService");
                LineItemTemplateService =
                    AdManagerService.MakeServiceSignature("v201811", "LineItemTemplateService");
                LineItemService =
                    AdManagerService.MakeServiceSignature("v201811", "LineItemService");
                LineItemCreativeAssociationService =
                    AdManagerService.MakeServiceSignature("v201811",
                        "LineItemCreativeAssociationService");
                LiveStreamEventService =
                    AdManagerService.MakeServiceSignature("v201811", "LiveStreamEventService");
                MobileApplicationService =
                    AdManagerService.MakeServiceSignature("v201811", "MobileApplicationService");
                NativeStyleService =
                    AdManagerService.MakeServiceSignature("v201811", "NativeStyleService");
                NetworkService = AdManagerService.MakeServiceSignature("v201811", "NetworkService");
                OrderService = AdManagerService.MakeServiceSignature("v201811", "OrderService");
                PackageService = AdManagerService.MakeServiceSignature("v201811", "PackageService");
                ProductPackageService =
                    AdManagerService.MakeServiceSignature("v201811", "ProductPackageService");
                ProductPackageItemService =
                    AdManagerService.MakeServiceSignature("v201811", "ProductPackageItemService");
                PlacementService =
                    AdManagerService.MakeServiceSignature("v201811", "PlacementService");
                PremiumRateService =
                    AdManagerService.MakeServiceSignature("v201811", "PremiumRateService");
                ProductService = AdManagerService.MakeServiceSignature("v201811", "ProductService");
                ProductTemplateService =
                    AdManagerService.MakeServiceSignature("v201811", "ProductTemplateService");
                ProposalService =
                    AdManagerService.MakeServiceSignature("v201811", "ProposalService");
                ProposalLineItemService =
                    AdManagerService.MakeServiceSignature("v201811", "ProposalLineItemService");
                PublisherQueryLanguageService =
                    AdManagerService.MakeServiceSignature("v201811",
                        "PublisherQueryLanguageService");
                RateCardService =
                    AdManagerService.MakeServiceSignature("v201811", "RateCardService");
                ReconciliationLineItemReportService =
                    AdManagerService.MakeServiceSignature("v201811",
                        "ReconciliationLineItemReportService");
                ReconciliationOrderReportService =
                    AdManagerService.MakeServiceSignature("v201811",
                        "ReconciliationOrderReportService");
                ReconciliationReportService =
                    AdManagerService.MakeServiceSignature("v201811", "ReconciliationReportService");
                ReconciliationReportRowService =
                    AdManagerService.MakeServiceSignature("v201811",
                        "ReconciliationReportRowService");
                ReportService = AdManagerService.MakeServiceSignature("v201811", "ReportService");
                SuggestedAdUnitService =
                    AdManagerService.MakeServiceSignature("v201811", "SuggestedAdUnitService");
                TeamService = AdManagerService.MakeServiceSignature("v201811", "TeamService");
                UserService = AdManagerService.MakeServiceSignature("v201811", "UserService");
                UserTeamAssociationService =
                    AdManagerService.MakeServiceSignature("v201811", "UserTeamAssociationService");
                WorkflowRequestService =
                    AdManagerService.MakeServiceSignature("v201811", "WorkflowRequestService");
            }
        }
    }
}
