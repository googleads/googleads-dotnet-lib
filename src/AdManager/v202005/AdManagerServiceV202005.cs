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
        /// All the services available in v202005.
        /// </summary>
        public class v202005
        {
            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v202005/ActivityGroupService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ActivityGroupService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v202005/ActivityService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ActivityService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v202005/AdExclusionRuleService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature AdExclusionRuleService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v202005/AdjustmentRuleService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature AdjustmentService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v202005/AdRuleService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature AdRuleService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v202005/AudienceSegmentService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature AudienceSegmentService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v202005/CdnConfigurationService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature CdnConfigurationService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v202005/CmsMetadataService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature CmsMetadataService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v202005/ContactService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ContactService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v202005/CompanyService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature CompanyService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v202005/ContentBundleService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ContentBundleService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v202005/ContentService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ContentService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v202005/CreativeService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature CreativeService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v202005/CreativeReviewService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature CreativeReviewService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v202005/CreativeSetService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature CreativeSetService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v202005/CreativeTemplateService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature CreativeTemplateService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v202005/CreativeWrapperService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature CreativeWrapperService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v202005/CustomFieldService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature CustomFieldService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v202005/CustomTargetingService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature CustomTargetingService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v202005/DaiAuthenticationKeyService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature DaiAuthenticationKeyService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v202005/ForecastService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ForecastService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v202005/InventoryService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature InventoryService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v202005/LabelService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature LabelService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v202005/LineItemTemplateService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature LineItemTemplateService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v202005/LineItemService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature LineItemService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v202005/LineItemCreativeAssociationService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature LineItemCreativeAssociationService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v202005/LiveStreamEventService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature LiveStreamEventService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v202005/MobileApplicationService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature MobileApplicationService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v202005/NativeStyleService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature NativeStyleService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v202005/NetworkService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature NetworkService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v202005/OrderService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature OrderService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v202005/PlacementService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature PlacementService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v202005/ProposalService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature ProposalService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v202005/ProposalLineItemService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature ProposalLineItemService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v202005/PublisherQueryLanguageService">
            /// this page </a> for details.
            /// </summary>
            public static readonly ServiceSignature PublisherQueryLanguageService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v202005/ReportService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature ReportService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v202005/StreamActivityMonitorService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature StreamActivityMonitorService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v202005/SuggestedAdUnitService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature SuggestedAdUnitService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v202005/TargetingPresetService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature TargetingPresetService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v202005/TeamService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature TeamService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v202005/UserService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature UserService;

            /// <summary>
            /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v202005/UserTeamAssociationService">
            /// this page</a> for details.
            /// </summary>
            public static readonly ServiceSignature UserTeamAssociationService;

            /// <summary>
            /// Factory type for v202005 services.
            /// </summary>
            public static readonly Type factoryType = typeof(AdManagerServiceFactory);

            /// <summary>
            /// Static constructor to initialize the service constants.
            /// </summary>
            static v202005()
            {
                ActivityGroupService =
                    AdManagerService.MakeServiceSignature("v202005", "ActivityGroupService");
                ActivityService =
                    AdManagerService.MakeServiceSignature("v202005", "ActivityService");
                AdExclusionRuleService =
                    AdManagerService.MakeServiceSignature("v202005", "AdExclusionRuleService");
                AdjustmentService =
                    AdManagerService.MakeServiceSignature("v202005", "AdjustmentService");
                AdRuleService = AdManagerService.MakeServiceSignature("v202005", "AdRuleService");
                AudienceSegmentService =
                    AdManagerService.MakeServiceSignature("v202005", "AudienceSegmentService");
                CdnConfigurationService =
                    AdManagerService.MakeServiceSignature("v202005", "CdnConfigurationService");
                CmsMetadataService =
                    AdManagerService.MakeServiceSignature("v202005", "CmsMetadataService");
                CompanyService = AdManagerService.MakeServiceSignature("v202005", "CompanyService");
                ContactService = AdManagerService.MakeServiceSignature("v202005", "ContactService");
                ContentBundleService =
                    AdManagerService.MakeServiceSignature("v202005", "ContentBundleService");
                ContentService = AdManagerService.MakeServiceSignature("v202005", "ContentService");
                CreativeService =
                    AdManagerService.MakeServiceSignature("v202005", "CreativeService");
                CreativeReviewService =
                    AdManagerService.MakeServiceSignature("v202005", "CreativeReviewService");
                CreativeSetService =
                    AdManagerService.MakeServiceSignature("v202005", "CreativeSetService");
                CreativeTemplateService =
                    AdManagerService.MakeServiceSignature("v202005", "CreativeTemplateService");
                CreativeWrapperService =
                    AdManagerService.MakeServiceSignature("v202005", "CreativeWrapperService");
                CustomTargetingService =
                    AdManagerService.MakeServiceSignature("v202005", "CustomTargetingService");
                CustomFieldService =
                    AdManagerService.MakeServiceSignature("v202005", "CustomFieldService");
                DaiAuthenticationKeyService =
                    AdManagerService.MakeServiceSignature("v202005", "DaiAuthenticationKeyService");
                ForecastService =
                    AdManagerService.MakeServiceSignature("v202005", "ForecastService");
                InventoryService =
                    AdManagerService.MakeServiceSignature("v202005", "InventoryService");
                LabelService = AdManagerService.MakeServiceSignature("v202005", "LabelService");
                LineItemTemplateService =
                    AdManagerService.MakeServiceSignature("v202005", "LineItemTemplateService");
                LineItemService =
                    AdManagerService.MakeServiceSignature("v202005", "LineItemService");
                LineItemCreativeAssociationService =
                    AdManagerService.MakeServiceSignature("v202005",
                        "LineItemCreativeAssociationService");
                LiveStreamEventService =
                    AdManagerService.MakeServiceSignature("v202005", "LiveStreamEventService");
                MobileApplicationService =
                    AdManagerService.MakeServiceSignature("v202005", "MobileApplicationService");
                NativeStyleService =
                    AdManagerService.MakeServiceSignature("v202005", "NativeStyleService");
                NetworkService = AdManagerService.MakeServiceSignature("v202005", "NetworkService");
                OrderService = AdManagerService.MakeServiceSignature("v202005", "OrderService");
                PlacementService =
                    AdManagerService.MakeServiceSignature("v202005", "PlacementService");
                ProposalService =
                    AdManagerService.MakeServiceSignature("v202005", "ProposalService");
                ProposalLineItemService =
                    AdManagerService.MakeServiceSignature("v202005", "ProposalLineItemService");
                PublisherQueryLanguageService =
                    AdManagerService.MakeServiceSignature("v202005",
                        "PublisherQueryLanguageService");
                ReportService = AdManagerService.MakeServiceSignature("v202005", "ReportService");
                StreamActivityMonitorService =
                    AdManagerService.MakeServiceSignature("v202005", "StreamActivityMonitorService");
                SuggestedAdUnitService =
                    AdManagerService.MakeServiceSignature("v202005", "SuggestedAdUnitService");
                TargetingPresetService = AdManagerService.MakeServiceSignature("v202005", "TargetingPresetService");
                TeamService = AdManagerService.MakeServiceSignature("v202005", "TeamService");
                UserService = AdManagerService.MakeServiceSignature("v202005", "UserService");
                UserTeamAssociationService =
                    AdManagerService.MakeServiceSignature("v202005", "UserTeamAssociationService");
            }
        }
    }
}
