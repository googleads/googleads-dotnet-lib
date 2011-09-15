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

namespace Google.Api.Ads.Dfa.Lib {
  /// <summary>
  /// Lists all the services available through this library.
  /// </summary>
  public partial class DfaService : AdsService {
    /// <summary>
    /// All the services available in v1.15.
    /// </summary>
    public class v1_15 {
      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.15/ad/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature AdRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.15/advertiser/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature AdvertiserRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.15/advertisergroup/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature AdvertiserGroupRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.15/campaign/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.15/changelog/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature ChangeLogRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.15/contentcategory/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature ContentCategoryRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.15/creative/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature CreativeRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.15/creativefield/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature CreativeFieldRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.15/creativegroup/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature CreativeGroupRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.15/login/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature LoginRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.15/network/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature NetworkRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.15/placement/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature PlacementRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.15/report/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature ReportRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.15/site/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature SiteRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.15/size/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature SizeRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.15/spotlight/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature SpotlightRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.15/placementstrategy/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature PlacementStrategyRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.15/subnetwork/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature SubnetworkRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.15/user/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature UserRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.15/userrole/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature UserRoleRemoteService;

      /// <summary>
      /// Factory type for v1.15 services.
      /// </summary>
      public static readonly Type factoryType = typeof(DfaServiceFactory);

      /// <summary>
      /// Static constructor to initialize the service constants.
      /// </summary>
      static v1_15() {
        AdRemoteService = DfaService.MakeServiceSignature("v1.15", "AdRemoteService");
        AdvertiserRemoteService = DfaService.MakeServiceSignature("v1.15",
            "AdvertiserRemoteService");
        AdvertiserGroupRemoteService = DfaService.MakeServiceSignature("v1.15",
            "AdvertiserGroupRemoteService");
        CampaignRemoteService = DfaService.MakeServiceSignature("v1.15", "CampaignRemoteService");
        ChangeLogRemoteService = DfaService.MakeServiceSignature("v1.15", "ChangeLogRemoteService");
        ContentCategoryRemoteService = DfaService.MakeServiceSignature("v1.15",
            "ContentCategoryRemoteService");
        CreativeRemoteService = DfaService.MakeServiceSignature("v1.15", "CreativeRemoteService");
        CreativeFieldRemoteService = DfaService.MakeServiceSignature("v1.15",
            "CreativeFieldRemoteService");
        CreativeGroupRemoteService = DfaService.MakeServiceSignature("v1.15",
            "CreativeGroupRemoteService");
        LoginRemoteService = DfaService.MakeServiceSignature("v1.15", "LoginRemoteService");
        NetworkRemoteService = DfaService.MakeServiceSignature("v1.15", "NetworkRemoteService");
        PlacementRemoteService = DfaService.MakeServiceSignature("v1.15", "PlacementRemoteService");
        ReportRemoteService = DfaService.MakeServiceSignature("v1.15", "ReportRemoteService");
        SiteRemoteService = DfaService.MakeServiceSignature("v1.15", "SiteRemoteService");
        SizeRemoteService = DfaService.MakeServiceSignature("v1.15", "SizeRemoteService");
        SpotlightRemoteService = DfaService.MakeServiceSignature("v1.15", "SpotlightRemoteService");
        PlacementStrategyRemoteService = DfaService.MakeServiceSignature("v1.15",
            "PlacementStrategyRemoteService");
        SubnetworkRemoteService = DfaService.MakeServiceSignature("v1.15",
            "SubnetworkRemoteService");
        UserRemoteService = DfaService.MakeServiceSignature("v1.15", "UserRemoteService");
        UserRoleRemoteService = DfaService.MakeServiceSignature("v1.15", "UserRoleRemoteService");
      }
    }
  }
}
