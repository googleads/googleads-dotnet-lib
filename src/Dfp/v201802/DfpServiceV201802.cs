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

namespace Google.Api.Ads.Dfp.Lib {
  /// <summary>
  /// Lists all the services available through this library.
  /// </summary>
  public partial class DfpService : AdsService {
    /// <summary>
    /// All the services available in v201802.
    /// </summary>
    public class v201802 {
      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/ActivityGroupService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ActivityGroupService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/ActivityService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ActivityService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/AdExclusionRuleService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdExclusionRuleService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/AdRuleService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdRuleService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/BaseRateService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature BaseRateService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/AudienceSegmentService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AudienceSegmentService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/CdnConfigurationService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CdnConfigurationService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/ContactService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ContactService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/CompanyService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CompanyService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/ContentBundleService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ContentBundleService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/ContentService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ContentService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/ContentMetadataKeyHierarchyService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ContentMetadataKeyHierarchyService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/CreativeService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature CreativeService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/CreativeSetService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature CreativeSetService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/CreativeTemplateService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature CreativeTemplateService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/CreativeWrapperService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature CreativeWrapperService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/CustomFieldService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature CustomFieldService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/CustomTargetingService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature CustomTargetingService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/ExchangeRateService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ExchangeRateService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/ForecastService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ForecastService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/InventoryService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature InventoryService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/LabelService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature LabelService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/LineItemTemplateService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature LineItemTemplateService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/LineItemService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature LineItemService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/LineItemCreativeAssociationService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature LineItemCreativeAssociationService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/LiveStreamEventService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature LiveStreamEventService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/MobileApplicationService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature MobileApplicationService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/NativeStyleService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature NativeStyleService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/NetworkService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature NetworkService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/OrderService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature OrderService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/PackageService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature PackageService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/ProductPackageItemService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ProductPackageItemService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/ProductPackageService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ProductPackageService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/PlacementService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature PlacementService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/PremiumRateService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature PremiumRateService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/ProductService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature ProductService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/ProductTemplateService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature ProductTemplateService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/ProposalService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature ProposalService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/ProposalLineItemService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature ProposalLineItemService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/PublisherQueryLanguageService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature PublisherQueryLanguageService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/RateCardService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature RateCardService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/ReconciliationLineItemReportService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ReconciliationLineItemReportService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/ReconciliationOrderReportService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ReconciliationOrderReportService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/ReconciliationReportService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ReconciliationReportService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/ReconciliationReportRowService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ReconciliationReportRowService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/ReportService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ReportService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/SuggestedAdUnitService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature SuggestedAdUnitService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/TeamService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature TeamService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/UserService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature UserService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/UserTeamAssociationService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature UserTeamAssociationService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201802/WorkflowRequestService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature WorkflowRequestService;

      /// <summary>
      /// Factory type for v201802 services.
      /// </summary>
      public static readonly Type factoryType = typeof(DfpServiceFactory);

      /// <summary>
      /// Static constructor to initialize the service constants.
      /// </summary>
      static v201802() {
        ActivityGroupService = DfpService.MakeServiceSignature("v201802", "ActivityGroupService");
        ActivityService = DfpService.MakeServiceSignature("v201802", "ActivityService");
        AdExclusionRuleService = DfpService.MakeServiceSignature("v201802", "AdExclusionRuleService");
        AdRuleService = DfpService.MakeServiceSignature("v201802", "AdRuleService");
        BaseRateService = DfpService.MakeServiceSignature("v201802", "BaseRateService");
        ContactService = DfpService.MakeServiceSignature("v201802", "ContactService");
        AudienceSegmentService = DfpService.MakeServiceSignature("v201802",
            "AudienceSegmentService");
        CdnConfigurationService = DfpService.MakeServiceSignature("v201802", "CdnConfigurationService");
        CompanyService = DfpService.MakeServiceSignature("v201802", "CompanyService");
        ContentBundleService = DfpService.MakeServiceSignature("v201802", "ContentBundleService");
        ContentService = DfpService.MakeServiceSignature("v201802", "ContentService");
        ContentMetadataKeyHierarchyService = DfpService.MakeServiceSignature("v201802",
            "ContentMetadataKeyHierarchyService");
        CreativeService = DfpService.MakeServiceSignature("v201802", "CreativeService");
        CreativeSetService = DfpService.MakeServiceSignature("v201802", "CreativeSetService");
        CreativeTemplateService = DfpService.MakeServiceSignature("v201802",
            "CreativeTemplateService");
        CreativeWrapperService = DfpService.MakeServiceSignature("v201802",
            "CreativeWrapperService");
        CustomTargetingService = DfpService.MakeServiceSignature("v201802",
            "CustomTargetingService");
        CustomFieldService = DfpService.MakeServiceSignature("v201802",
            "CustomFieldService");
        ExchangeRateService = DfpService.MakeServiceSignature("v201802", "ExchangeRateService");
        ForecastService = DfpService.MakeServiceSignature("v201802", "ForecastService");
        InventoryService = DfpService.MakeServiceSignature("v201802", "InventoryService");
        LabelService = DfpService.MakeServiceSignature("v201802", "LabelService");
        LineItemTemplateService = DfpService.MakeServiceSignature("v201802",
            "LineItemTemplateService");
        LineItemService = DfpService.MakeServiceSignature("v201802", "LineItemService");
        LineItemCreativeAssociationService =
            DfpService.MakeServiceSignature("v201802", "LineItemCreativeAssociationService");
        LiveStreamEventService = DfpService.MakeServiceSignature("v201802",
            "LiveStreamEventService");
        MobileApplicationService = DfpService.MakeServiceSignature("v201802",
            "MobileApplicationService");
        NativeStyleService = DfpService.MakeServiceSignature("v201802", "NativeStyleService");
        NetworkService = DfpService.MakeServiceSignature("v201802", "NetworkService");
        OrderService = DfpService.MakeServiceSignature("v201802", "OrderService");
        PackageService = DfpService.MakeServiceSignature("v201802", "PackageService");
        ProductPackageService = DfpService.MakeServiceSignature("v201802", "ProductPackageService");
        ProductPackageItemService = DfpService.MakeServiceSignature("v201802", "ProductPackageItemService");
        PlacementService = DfpService.MakeServiceSignature("v201802", "PlacementService");
        PremiumRateService = DfpService.MakeServiceSignature("v201802", "PremiumRateService");
        ProductService = DfpService.MakeServiceSignature("v201802", "ProductService");
        ProductTemplateService = DfpService.MakeServiceSignature("v201802",
            "ProductTemplateService");
        ProposalService = DfpService.MakeServiceSignature("v201802", "ProposalService");
        ProposalLineItemService = DfpService.MakeServiceSignature("v201802",
            "ProposalLineItemService");
        PublisherQueryLanguageService = DfpService.MakeServiceSignature("v201802",
            "PublisherQueryLanguageService");
        RateCardService = DfpService.MakeServiceSignature("v201802", "RateCardService");
        ReconciliationLineItemReportService = DfpService.MakeServiceSignature("v201802",
            "ReconciliationLineItemReportService");
        ReconciliationOrderReportService = DfpService.MakeServiceSignature("v201802",
            "ReconciliationOrderReportService");
        ReconciliationReportService = DfpService.MakeServiceSignature("v201802",
            "ReconciliationReportService");
        ReconciliationReportRowService = DfpService.MakeServiceSignature("v201802",
            "ReconciliationReportRowService");
        ReportService = DfpService.MakeServiceSignature("v201802", "ReportService");
        SuggestedAdUnitService = DfpService.MakeServiceSignature("v201802",
            "SuggestedAdUnitService");
        TeamService = DfpService.MakeServiceSignature("v201802", "TeamService");
        UserService = DfpService.MakeServiceSignature("v201802", "UserService");
        UserTeamAssociationService = DfpService.MakeServiceSignature("v201802",
            "UserTeamAssociationService");
        WorkflowRequestService = DfpService.MakeServiceSignature("v201802",
            "WorkflowRequestService");
      }
    }
  }
}
