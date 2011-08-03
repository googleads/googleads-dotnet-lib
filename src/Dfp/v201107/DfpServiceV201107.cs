// Copyright 2011, Google Inc. All Rights Reserved.
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
    /// All the services available in v201107.
    /// </summary>
    public class v201107 {
      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201107/CompanyService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CompanyService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201107/CreativeService.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature CreativeService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201107/CustomTargetingService.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature CustomTargetingService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201107/ForecastService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ForecastService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201107/InventoryService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature InventoryService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201107/LabelService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature LabelService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201107/LineItemService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature LineItemService;

      /// <summary>
      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201107/LineItemCreativeAssociationService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature LineItemCreativeAssociationService;

      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201107/NetworkService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature NetworkService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201107/OrderService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature OrderService;

      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201107/PlacementService.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature PlacementService;

      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201107/PublisherQueryLanguageService.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature PublisherQueryLanguageService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201107/ReportService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ReportService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/v201107/UserService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature UserService;

      /// <summary>
      /// Factory type for v201107 services.
      /// </summary>
      public static readonly Type factoryType = typeof(DfpServiceFactory);

      /// <summary>
      /// Static constructor to initialize the service constants.
      /// </summary>
      static v201107() {
        CompanyService = DfpService.MakeServiceSignature("v201107", "CompanyService");
        CreativeService = DfpService.MakeServiceSignature("v201107", "CreativeService");
        CustomTargetingService = DfpService.MakeServiceSignature("v201107",
            "CustomTargetingService");
        ForecastService = DfpService.MakeServiceSignature("v201107", "ForecastService");
        InventoryService = DfpService.MakeServiceSignature("v201107", "InventoryService");
        LabelService = DfpService.MakeServiceSignature("v201107", "LabelService");
        LineItemService = DfpService.MakeServiceSignature("v201107", "LineItemService");
        LineItemCreativeAssociationService =
            DfpService.MakeServiceSignature("v201107", "LineItemCreativeAssociationService");
        NetworkService = DfpService.MakeServiceSignature("v201107", "NetworkService");
        OrderService = DfpService.MakeServiceSignature("v201107", "OrderService");
        PlacementService = DfpService.MakeServiceSignature("v201107", "PlacementService");
        PublisherQueryLanguageService = DfpService.MakeServiceSignature("v201107",
            "PublisherQueryLanguageService");
        ReportService = DfpService.MakeServiceSignature("v201107", "ReportService");
        UserService = DfpService.MakeServiceSignature("v201107", "UserService");
      }
    }
  }
}
