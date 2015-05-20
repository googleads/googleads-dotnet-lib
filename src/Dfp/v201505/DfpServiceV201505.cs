// Copyright 2015, Google Inc. All Rights Reserved.
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

// Author: Chris Seeley

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
    /// All the services available in v201505.
    /// </summary>
    public class v201505 {
      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201505/ActivityGroupService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ActivityGroupService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201505/ActivityService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ActivityService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201505/AdExclusionRuleService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdExclusionRuleService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201505/AdRuleService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdRuleService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201505/BaseRateService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature BaseRateService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201505/ContactService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ContactService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201505/AudienceSegmentService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AudienceSegmentService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201505/CompanyService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CompanyService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201505/ContentService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ContentService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201505/ContentMetadataKeyHierarchyService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ContentMetadataKeyHierarchyService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201505/CreativeService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature CreativeService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201505/CreativeSetService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature CreativeSetService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201505/CreativeTemplateService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature CreativeTemplateService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201505/CreativeWrapperService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature CreativeWrapperService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201505/CustomFieldService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature CustomFieldService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201505/CustomTargetingService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature CustomTargetingService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201505/ExchangeRateService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ExchangeRateService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201505/ForecastService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ForecastService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201505/InventoryService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature InventoryService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201505/LabelService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature LabelService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201505/LineItemTemplateService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature LineItemTemplateService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201505/LineItemService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature LineItemService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201505/LineItemCreativeAssociationService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature LineItemCreativeAssociationService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201505/LiveStreamEventService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature LiveStreamEventService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201505/NetworkService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature NetworkService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201505/OrderService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature OrderService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201505/PackageService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature PackageService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201505/ProductPackageItemService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ProductPackageItemService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201505/ProductPackageService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ProductPackageService;

      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201505/PlacementService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature PlacementService;

      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201505/PremiumRateService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature PremiumRateService;

      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201505/ProductService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature ProductService;

      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201505/ProductTemplateService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature ProductTemplateService;

      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201505/ProposalService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature ProposalService;

      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201505/ProposalLineItemService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature ProposalLineItemService;

      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201505/PublisherQueryLanguageService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature PublisherQueryLanguageService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201505/RateCardService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature RateCardService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201505/ReconciliationOrderReportService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ReconciliationOrderReportService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201505/ReconciliationReportService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ReconciliationReportService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201505/ReconciliationReportRowService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ReconciliationReportRowService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201505/ReportService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ReportService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201505/SharedAdUnitService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature SharedAdUnitService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201505/SuggestedAdUnitService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature SuggestedAdUnitService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201505/TeamService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature TeamService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201505/UserService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature UserService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201505/UserTeamAssociationService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature UserTeamAssociationService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201505/WorkflowRequestService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature WorkflowRequestService;

      /// <summary>
      /// Factory type for v201505 services.
      /// </summary>
      public static readonly Type factoryType = typeof(DfpServiceFactory);

      /// <summary>
      /// Static constructor to initialize the service constants.
      /// </summary>
      static v201505() {
        ActivityGroupService = DfpService.MakeServiceSignature("v201505", "ActivityGroupService");
        ActivityService = DfpService.MakeServiceSignature("v201505", "ActivityService");
        AdExclusionRuleService = DfpService.MakeServiceSignature("v201505", "AdExclusionRuleService");
        AdRuleService = DfpService.MakeServiceSignature("v201505", "AdRuleService");
        BaseRateService = DfpService.MakeServiceSignature("v201505", "BaseRateService");
        ContactService = DfpService.MakeServiceSignature("v201505", "ContactService");
        AudienceSegmentService = DfpService.MakeServiceSignature("v201505",
            "AudienceSegmentService");
        CompanyService = DfpService.MakeServiceSignature("v201505", "CompanyService");
        ContentService = DfpService.MakeServiceSignature("v201505", "ContentService");
        ContentMetadataKeyHierarchyService = DfpService.MakeServiceSignature("v201505",
            "ContentMetadataKeyHierarchyService");
        CreativeService = DfpService.MakeServiceSignature("v201505", "CreativeService");
        CreativeSetService = DfpService.MakeServiceSignature("v201505", "CreativeSetService");
        CreativeTemplateService = DfpService.MakeServiceSignature("v201505",
            "CreativeTemplateService");
        CreativeWrapperService = DfpService.MakeServiceSignature("v201505",
            "CreativeWrapperService");
        CustomTargetingService = DfpService.MakeServiceSignature("v201505",
            "CustomTargetingService");
        CustomFieldService = DfpService.MakeServiceSignature("v201505",
            "CustomFieldService");
        ExchangeRateService = DfpService.MakeServiceSignature("v201505", "ExchangeRateService");
        ForecastService = DfpService.MakeServiceSignature("v201505", "ForecastService");
        InventoryService = DfpService.MakeServiceSignature("v201505", "InventoryService");
        LabelService = DfpService.MakeServiceSignature("v201505", "LabelService");
        LineItemTemplateService = DfpService.MakeServiceSignature("v201505",
            "LineItemTemplateService");
        LineItemService = DfpService.MakeServiceSignature("v201505", "LineItemService");
        LineItemCreativeAssociationService =
            DfpService.MakeServiceSignature("v201505", "LineItemCreativeAssociationService");
        LiveStreamEventService = DfpService.MakeServiceSignature("v201505",
            "LiveStreamEventService");
        NetworkService = DfpService.MakeServiceSignature("v201505", "NetworkService");
        OrderService = DfpService.MakeServiceSignature("v201505", "OrderService");
        PackageService = DfpService.MakeServiceSignature("v201505", "PackageService");
        ProductPackageService = DfpService.MakeServiceSignature("v201505", "ProductPackageService");
        ProductPackageItemService = DfpService.MakeServiceSignature("v201505", "ProductPackageItemService");
        PlacementService = DfpService.MakeServiceSignature("v201505", "PlacementService");
        PremiumRateService = DfpService.MakeServiceSignature("v201505", "PremiumRateService");
        ProductService = DfpService.MakeServiceSignature("v201505", "ProductService");
        ProductTemplateService = DfpService.MakeServiceSignature("v201505",
            "ProductTemplateService");
        ProposalService = DfpService.MakeServiceSignature("v201505", "ProposalService");
        ProposalLineItemService = DfpService.MakeServiceSignature("v201505",
            "ProposalLineItemService");
        PublisherQueryLanguageService = DfpService.MakeServiceSignature("v201505",
            "PublisherQueryLanguageService");
        RateCardService = DfpService.MakeServiceSignature("v201505", "RateCardService");
        ReconciliationOrderReportService = DfpService.MakeServiceSignature("v201505",
            "ReconciliationOrderReportService");
        ReconciliationReportService = DfpService.MakeServiceSignature("v201505",
            "ReconciliationReportService");
        ReconciliationReportRowService = DfpService.MakeServiceSignature("v201505",
            "ReconciliationReportRowService");
        ReportService = DfpService.MakeServiceSignature("v201505", "ReportService");
        SharedAdUnitService = DfpService.MakeServiceSignature("v201505",
            "SharedAdUnitService");
        SuggestedAdUnitService = DfpService.MakeServiceSignature("v201505",
            "SuggestedAdUnitService");
        TeamService = DfpService.MakeServiceSignature("v201505", "TeamService");
        UserService = DfpService.MakeServiceSignature("v201505", "UserService");
        UserTeamAssociationService = DfpService.MakeServiceSignature("v201505",
            "UserTeamAssociationService");
        WorkflowRequestService = DfpService.MakeServiceSignature("v201505",
            "WorkflowRequestService");
      }
    }
  }
}
