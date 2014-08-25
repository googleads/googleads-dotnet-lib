// Copyright 2014, Google Inc. All Rights Reserved.
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

// Author: Chris Seeley (https://github.com/Narwalter)

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
    /// All the services available in v201408.
    /// </summary>
    public class v201408 {
      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201408/ActivityGroupService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ActivityGroupService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201408/ActivityService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ActivityService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201408/AdRuleService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdRuleService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201408/BaseRateService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature BaseRateService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201408/ContactService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ContactService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201408/AudienceSegmentService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AudienceSegmentService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201408/CompanyService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CompanyService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201408/ContentService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ContentService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201408/ContentMetadataKeyHierarchyService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ContentMetadataKeyHierarchyService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201408/CreativeService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature CreativeService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201408/CreativeSetService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature CreativeSetService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201408/CreativeTemplateService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature CreativeTemplateService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201408/CreativeWrapperService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature CreativeWrapperService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201408/CustomFieldService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature CustomFieldService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201408/CustomTargetingService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature CustomTargetingService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201408/ExchangeRateService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ExchangeRateService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201408/ForecastService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ForecastService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201408/InventoryService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature InventoryService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201408/LabelService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature LabelService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201408/LineItemTemplateService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature LineItemTemplateService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201408/LineItemService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature LineItemService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201408/LineItemCreativeAssociationService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature LineItemCreativeAssociationService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201408/LiveStreamEventService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature LiveStreamEventService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201408/NetworkService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature NetworkService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201408/OrderService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature OrderService;

      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201408/PlacementService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature PlacementService;

      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201408/PremiumRateService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature PremiumRateService;

     /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201408/ProductService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature ProductService;

      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201408/ProductTemplateService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature ProductTemplateService;

      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201408/ProposalService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature ProposalService;

      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201408/ProposalLineItemService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature ProposalLineItemService;


      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201408/PublisherQueryLanguageService">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature PublisherQueryLanguageService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201408/RateCardService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature RateCardService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201408/ReconciliationOrderReportService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ReconciliationOrderReportService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201408/ReconciliationReportService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ReconciliationReportService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201408/ReconciliationReportRowService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ReconciliationReportRowService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201408/ReportService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ReportService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201408/SuggestedAdUnitService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature SuggestedAdUnitService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201408/TeamService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature TeamService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201408/UserService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature UserService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201408/UserTeamAssociationService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature UserTeamAssociationService;

      /// <summary>
      /// See <a href="https://developers.google.com/doubleclick-publishers/docs/reference/v201408/WorkflowRequestService">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature WorkflowRequestService;

       /// <summary>
      /// Factory type for v201408 services.
      /// </summary>
      public static readonly Type factoryType = typeof(DfpServiceFactory);

      /// <summary>
      /// Static constructor to initialize the service constants.
      /// </summary>
      static v201408() {
        ActivityGroupService = DfpService.MakeServiceSignature("v201408", "ActivityGroupService");
        ActivityService = DfpService.MakeServiceSignature("v201408", "ActivityService");
        AdRuleService = DfpService.MakeServiceSignature("v201408", "AdRuleService");
        BaseRateService = DfpService.MakeServiceSignature("v201408", "BaseRateService");
        ContactService = DfpService.MakeServiceSignature("v201408", "ContactService");
        AudienceSegmentService = DfpService.MakeServiceSignature("v201408",
            "AudienceSegmentService");
        CompanyService = DfpService.MakeServiceSignature("v201408", "CompanyService");
        ContentService = DfpService.MakeServiceSignature("v201408", "ContentService");
        ContentMetadataKeyHierarchyService = DfpService.MakeServiceSignature("v201408",
            "ContentMetadataKeyHierarchyService");
        CreativeService = DfpService.MakeServiceSignature("v201408", "CreativeService");
        CreativeSetService = DfpService.MakeServiceSignature("v201408", "CreativeSetService");
        CreativeTemplateService = DfpService.MakeServiceSignature("v201408",
            "CreativeTemplateService");
        CreativeWrapperService = DfpService.MakeServiceSignature("v201408",
            "CreativeWrapperService");
        CustomTargetingService = DfpService.MakeServiceSignature("v201408",
            "CustomTargetingService");
        CustomFieldService = DfpService.MakeServiceSignature("v201408",
            "CustomFieldService");
        ExchangeRateService = DfpService.MakeServiceSignature("v201408", "ExchangeRateService");
        ForecastService = DfpService.MakeServiceSignature("v201408", "ForecastService");
        InventoryService = DfpService.MakeServiceSignature("v201408", "InventoryService");
        LabelService = DfpService.MakeServiceSignature("v201408", "LabelService");
        LineItemTemplateService = DfpService.MakeServiceSignature("v201408",
            "LineItemTemplateService");
        LineItemService = DfpService.MakeServiceSignature("v201408", "LineItemService");
        LineItemCreativeAssociationService =
            DfpService.MakeServiceSignature("v201408", "LineItemCreativeAssociationService");
        LiveStreamEventService = DfpService.MakeServiceSignature("v201408",
            "LiveStreamEventService");
        NetworkService = DfpService.MakeServiceSignature("v201408", "NetworkService");
        OrderService = DfpService.MakeServiceSignature("v201408", "OrderService");
        PlacementService = DfpService.MakeServiceSignature("v201408", "PlacementService");
        PremiumRateService = DfpService.MakeServiceSignature("v201408", "PremiumRateService");
        ProductService = DfpService.MakeServiceSignature("v201408", "ProductService");
        ProductTemplateService = DfpService.MakeServiceSignature("v201408",
            "ProductTemplateService");
        ProposalService = DfpService.MakeServiceSignature("v201408", "ProposalService");
        ProposalLineItemService = DfpService.MakeServiceSignature("v201408",
            "ProposalLineItemService");
        PublisherQueryLanguageService = DfpService.MakeServiceSignature("v201408",
            "PublisherQueryLanguageService");
        RateCardService = DfpService.MakeServiceSignature("v201408", "RateCardService");
        ReconciliationOrderReportService = DfpService.MakeServiceSignature("v201408",
            "ReconciliationOrderReportService");
        ReconciliationReportService = DfpService.MakeServiceSignature("v201408",
            "ReconciliationReportService");
        ReconciliationReportRowService = DfpService.MakeServiceSignature("v201408",
            "ReconciliationReportRowService");
        ReportService = DfpService.MakeServiceSignature("v201408", "ReportService");
        SuggestedAdUnitService = DfpService.MakeServiceSignature("v201408",
            "SuggestedAdUnitService");
        TeamService = DfpService.MakeServiceSignature("v201408", "TeamService");
        UserService = DfpService.MakeServiceSignature("v201408", "UserService");
        UserTeamAssociationService = DfpService.MakeServiceSignature("v201408",
            "UserTeamAssociationService");
        WorkflowRequestService = DfpService.MakeServiceSignature("v201408",
            "WorkflowRequestService");
      }
    }
  }
}
