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
    /// All the services available in v201004.
    /// </summary>
    public class v201004 {
      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/latest/CreativeService.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature CreativeService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/latest/PlacementService.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature PlacementService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/latest/LineItemService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature LineItemService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/latest/UserService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature UserService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/latest/OrderService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature OrderService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/latest/LineItemCreativeAssociationService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature LineItemCreativeAssociationService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/latest/InventoryService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature InventoryService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/latest/CompanyService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CompanyService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/latest/ReportService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ReportService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfp/docs/reference/latest/ForecastService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ForecastService;

      /// See <a href="http://code.google.com/apis/dfp/docs/reference/latest/NetworkService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature NetworkService;

      /// <summary>
      /// Factory type for v201004 services.
      /// </summary>
      public static readonly Type factoryType = typeof(DfpServiceFactory);

      /// <summary>
      /// Static constructor to initialize the service constants.
      /// </summary>
      static v201004() {
        CreativeService = DfpService.MakeServiceSignature("v201004", "CreativeService");
        PlacementService = DfpService.MakeServiceSignature("v201004", "PlacementService");
        LineItemService = DfpService.MakeServiceSignature("v201004", "LineItemService");
        UserService = DfpService.MakeServiceSignature("v201004", "UserService");
        OrderService = DfpService.MakeServiceSignature("v201004", "OrderService");
        LineItemCreativeAssociationService =
            DfpService.MakeServiceSignature("v201004", "LineItemCreativeAssociationService");
        InventoryService = DfpService.MakeServiceSignature("v201004", "InventoryService");
        CompanyService = DfpService.MakeServiceSignature("v201004", "CompanyService");
        ReportService = DfpService.MakeServiceSignature("v201004", "ReportService");
        ForecastService = DfpService.MakeServiceSignature("v201004", "ForecastService");
        NetworkService = DfpService.MakeServiceSignature("v201004", "NetworkService");
      }
    }
  }
}
