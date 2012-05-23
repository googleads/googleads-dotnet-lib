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
    /// All the services available in v1.16.
    /// </summary>
    [Obsolete()]
    public class v1_16 {
      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.16/ad/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature AdRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.16/advertiser/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature AdvertiserRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.16/advertisergroup/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature AdvertiserGroupRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.16/campaign/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.16/changelog/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature ChangeLogRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.16/contentcategory/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature ContentCategoryRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.16/creative/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature CreativeRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.16/creativefield/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature CreativeFieldRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.16/creativegroup/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature CreativeGroupRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.16/login/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature LoginRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.16/network/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature NetworkRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.16/placement/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature PlacementRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.16/report/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature ReportRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.16/site/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature SiteRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.16/size/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature SizeRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.16/spotlight/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature SpotlightRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.16/placementstrategy/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature PlacementStrategyRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.16/subnetwork/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature SubnetworkRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.16/user/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature UserRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.16/userrole/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature UserRoleRemoteService;

      /// <summary>
      /// Factory type for v1.16 services.
      /// </summary>
      public static readonly Type factoryType = typeof(DfaServiceFactory);

      /// <summary>
      /// Static constructor to initialize the service constants.
      /// </summary>
      static v1_16() {
        AdRemoteService = DfaService.MakeServiceSignature("v1.16", "AdRemoteService");
        AdvertiserRemoteService = DfaService.MakeServiceSignature("v1.16",
            "AdvertiserRemoteService");
        AdvertiserGroupRemoteService = DfaService.MakeServiceSignature("v1.16",
            "AdvertiserGroupRemoteService");
        CampaignRemoteService = DfaService.MakeServiceSignature("v1.16", "CampaignRemoteService");
        ChangeLogRemoteService = DfaService.MakeServiceSignature("v1.16", "ChangeLogRemoteService");
        ContentCategoryRemoteService = DfaService.MakeServiceSignature("v1.16",
            "ContentCategoryRemoteService");
        CreativeRemoteService = DfaService.MakeServiceSignature("v1.16", "CreativeRemoteService");
        CreativeFieldRemoteService = DfaService.MakeServiceSignature("v1.16",
            "CreativeFieldRemoteService");
        CreativeGroupRemoteService = DfaService.MakeServiceSignature("v1.16",
            "CreativeGroupRemoteService");
        LoginRemoteService = DfaService.MakeServiceSignature("v1.16", "LoginRemoteService");
        NetworkRemoteService = DfaService.MakeServiceSignature("v1.16", "NetworkRemoteService");
        PlacementRemoteService = DfaService.MakeServiceSignature("v1.16", "PlacementRemoteService");
        ReportRemoteService = DfaService.MakeServiceSignature("v1.16", "ReportRemoteService");
        SiteRemoteService = DfaService.MakeServiceSignature("v1.16", "SiteRemoteService");
        SizeRemoteService = DfaService.MakeServiceSignature("v1.16", "SizeRemoteService");
        SpotlightRemoteService = DfaService.MakeServiceSignature("v1.16", "SpotlightRemoteService");
        PlacementStrategyRemoteService = DfaService.MakeServiceSignature("v1.16",
            "PlacementStrategyRemoteService");
        SubnetworkRemoteService = DfaService.MakeServiceSignature("v1.16",
            "SubnetworkRemoteService");
        UserRemoteService = DfaService.MakeServiceSignature("v1.16", "UserRemoteService");
        UserRoleRemoteService = DfaService.MakeServiceSignature("v1.16", "UserRoleRemoteService");
      }
    }
  }
}
