// Copyright 2012, Google Inc. All Rights Reserved.
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
    /// All the services available in v201208.
    /// </summary>
    public class v201208 {
      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201208/AudienceSegmentService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AudienceSegmentService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201208/CompanyService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CompanyService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201208/ContentService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ContentService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201208/CreativeService.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature CreativeService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201208/CreativeSetService.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature CreativeSetService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201208/CreativeTemplateService.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature CreativeTemplateService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201208/CustomFieldService.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature CustomFieldService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201208/CustomTargetingService.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature CustomTargetingService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201208/ForecastService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ForecastService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201208/InventoryService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature InventoryService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201208/LabelService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature LabelService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201208/LineItemService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature LineItemService;

      /// <summary>
      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201208/LineItemCreativeAssociationService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature LineItemCreativeAssociationService;

      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201208/NetworkService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature NetworkService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201208/OrderService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature OrderService;

      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201208/PlacementService.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature PlacementService;

      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201208/PublisherQueryLanguageService.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature PublisherQueryLanguageService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201208/ReportService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ReportService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201208/SuggestedAdUnitService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature SuggestedAdUnitService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201208/TeamService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature TeamService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201208/ThirdPartySlotService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ThirdPartySlotService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201208/UserService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature UserService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201208/UserTeamAssociationService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature UserTeamAssociationService;

      /// <summary>
      /// Factory type for v201208 services.
      /// </summary>
      public static readonly Type factoryType = typeof(DfpServiceFactory);

      /// <summary>
      /// Static constructor to initialize the service constants.
      /// </summary>
      static v201208() {
        AudienceSegmentService = DfpService.MakeServiceSignature("v201208",
            "AudienceSegmentService");
        CompanyService = DfpService.MakeServiceSignature("v201208", "CompanyService");
        ContentService = DfpService.MakeServiceSignature("v201208", "ContentService");
        CreativeService = DfpService.MakeServiceSignature("v201208", "CreativeService");
        CreativeSetService = DfpService.MakeServiceSignature("v201208", "CreativeSetService");
        CreativeTemplateService = DfpService.MakeServiceSignature("v201208",
            "CreativeTemplateService");
        CustomTargetingService = DfpService.MakeServiceSignature("v201208",
            "CustomTargetingService");
        CustomFieldService = DfpService.MakeServiceSignature("v201208",
            "CustomFieldService");
        ForecastService = DfpService.MakeServiceSignature("v201208", "ForecastService");
        InventoryService = DfpService.MakeServiceSignature("v201208", "InventoryService");
        LabelService = DfpService.MakeServiceSignature("v201208", "LabelService");
        LineItemService = DfpService.MakeServiceSignature("v201208", "LineItemService");
        LineItemCreativeAssociationService =
            DfpService.MakeServiceSignature("v201208", "LineItemCreativeAssociationService");
        NetworkService = DfpService.MakeServiceSignature("v201208", "NetworkService");
        OrderService = DfpService.MakeServiceSignature("v201208", "OrderService");
        PlacementService = DfpService.MakeServiceSignature("v201208", "PlacementService");
        PublisherQueryLanguageService = DfpService.MakeServiceSignature("v201208",
            "PublisherQueryLanguageService");
        ReportService = DfpService.MakeServiceSignature("v201208", "ReportService");
        SuggestedAdUnitService = DfpService.MakeServiceSignature("v201208",
            "SuggestedAdUnitService");
        TeamService = DfpService.MakeServiceSignature("v201208", "TeamService");
        ThirdPartySlotService = DfpService.MakeServiceSignature("v201208",
            "ThirdPartySlotService");
        UserService = DfpService.MakeServiceSignature("v201208", "UserService");
        UserTeamAssociationService = DfpService.MakeServiceSignature("v201208",
            "UserTeamAssociationService");
      }
    }
  }
}
