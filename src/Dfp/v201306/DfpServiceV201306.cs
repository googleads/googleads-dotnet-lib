// Copyright 2013, Google Inc. All Rights Reserved.
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

// Author: api.anash@gmail.com (Anash P. Oommen)

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
    /// All the services available in v201306.
    /// </summary>
    public class v201306 {
      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201306/ActivityGroupService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ActivityGroupService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201306/ActivityService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ActivityService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201306/AdRuleService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdRuleService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201306/BaseRateService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature BaseRateService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201306/ContactService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ContactService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201306/AudienceSegmentService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AudienceSegmentService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201306/CompanyService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CompanyService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201306/ContentService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ContentService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201306/CreativeService.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature CreativeService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201306/CreativeSetService.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature CreativeSetService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201306/CreativeTemplateService.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature CreativeTemplateService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201306/CreativeWrapperService.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature CreativeWrapperService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201306/CustomFieldService.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature CustomFieldService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201306/CustomTargetingService.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature CustomTargetingService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201306/ForecastService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ForecastService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201306/InventoryService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature InventoryService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201306/LabelService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature LabelService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201306/LineItemTemplateService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature LineItemTemplateService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201306/LineItemService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature LineItemService;

      /// <summary>
      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201306/LineItemCreativeAssociationService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature LineItemCreativeAssociationService;

      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201306/NetworkService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature NetworkService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201306/OrderService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature OrderService;

      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201306/PlacementService.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature PlacementService;

      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201306/ProductService.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature ProductService;

      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201306/ProductTemplateService.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature ProductTemplateService;

      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201306/ProposalService.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature ProposalService;

      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201306/ProposalLineItemService.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature ProposalLineItemService;


      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201306/PublisherQueryLanguageService.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature PublisherQueryLanguageService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201306/RateCardService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature RateCardService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201306/RateCardCustomizationService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature RateCardCustomizationService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201306/ReconciliationOrderReportService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ReconciliationOrderReportService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201306/ReconciliationReportService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ReconciliationReportService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201306/ReconciliationReportRowService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ReconciliationReportRowService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201306/ReportService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ReportService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201306/SuggestedAdUnitService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature SuggestedAdUnitService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201306/TeamService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature TeamService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201306/UserService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature UserService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201306/UserTeamAssociationService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature UserTeamAssociationService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201306/WorkflowActionService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature WorkflowActionService;

      /// <summary>
      /// Factory type for v201306 services.
      /// </summary>
      public static readonly Type factoryType = typeof(DfpServiceFactory);

      /// <summary>
      /// Static constructor to initialize the service constants.
      /// </summary>
      static v201306() {
        ActivityGroupService = DfpService.MakeServiceSignature("v201306", "ActivityGroupService");
        ActivityService = DfpService.MakeServiceSignature("v201306", "ActivityService");
        AdRuleService = DfpService.MakeServiceSignature("v201306", "AdRuleService");
        BaseRateService = DfpService.MakeServiceSignature("v201306", "BaseRateService");
        ContactService = DfpService.MakeServiceSignature("v201306", "ContactService");
        AudienceSegmentService = DfpService.MakeServiceSignature("v201306",
            "AudienceSegmentService");
        CompanyService = DfpService.MakeServiceSignature("v201306", "CompanyService");
        ContentService = DfpService.MakeServiceSignature("v201306", "ContentService");
        CreativeService = DfpService.MakeServiceSignature("v201306", "CreativeService");
        CreativeSetService = DfpService.MakeServiceSignature("v201306", "CreativeSetService");
        CreativeTemplateService = DfpService.MakeServiceSignature("v201306",
            "CreativeTemplateService");
        CreativeWrapperService = DfpService.MakeServiceSignature("v201306",
            "CreativeWrapperService");
        CustomTargetingService = DfpService.MakeServiceSignature("v201306",
            "CustomTargetingService");
        CustomFieldService = DfpService.MakeServiceSignature("v201306",
            "CustomFieldService");
        ForecastService = DfpService.MakeServiceSignature("v201306", "ForecastService");
        InventoryService = DfpService.MakeServiceSignature("v201306", "InventoryService");
        LabelService = DfpService.MakeServiceSignature("v201306", "LabelService");
        LineItemTemplateService = DfpService.MakeServiceSignature("v201306",
            "LineItemTemplateService");
        LineItemService = DfpService.MakeServiceSignature("v201306", "LineItemService");
        LineItemCreativeAssociationService =
            DfpService.MakeServiceSignature("v201306", "LineItemCreativeAssociationService");
        NetworkService = DfpService.MakeServiceSignature("v201306", "NetworkService");
        OrderService = DfpService.MakeServiceSignature("v201306", "OrderService");
        PlacementService = DfpService.MakeServiceSignature("v201306", "PlacementService");
        ProductService = DfpService.MakeServiceSignature("v201306", "ProductService");
        ProductTemplateService = DfpService.MakeServiceSignature("v201306",
            "ProductTemplateService");
        ProposalService = DfpService.MakeServiceSignature("v201306", "ProposalService");
        ProposalLineItemService = DfpService.MakeServiceSignature("v201306",
            "ProposalLineItemService");
        PublisherQueryLanguageService = DfpService.MakeServiceSignature("v201306",
            "PublisherQueryLanguageService");
        RateCardService = DfpService.MakeServiceSignature("v201306", "RateCardService");
        RateCardCustomizationService = DfpService.MakeServiceSignature("v201306",
            "RateCardCustomizationService");
        ReconciliationOrderReportService = DfpService.MakeServiceSignature("v201306",
            "ReconciliationOrderReportService");
        ReconciliationReportService = DfpService.MakeServiceSignature("v201306",
            "ReconciliationReportService");
        ReconciliationReportRowService = DfpService.MakeServiceSignature("v201306",
            "ReconciliationReportRowService");
        ReportService = DfpService.MakeServiceSignature("v201306", "ReportService");
        SuggestedAdUnitService = DfpService.MakeServiceSignature("v201306",
            "SuggestedAdUnitService");
        TeamService = DfpService.MakeServiceSignature("v201306", "TeamService");
        UserService = DfpService.MakeServiceSignature("v201306", "UserService");
        UserTeamAssociationService = DfpService.MakeServiceSignature("v201306",
            "UserTeamAssociationService");
        WorkflowActionService = DfpService.MakeServiceSignature("v201306",
            "WorkflowActionService");
      }
    }
  }
}
