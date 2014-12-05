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

namespace Google.Api.Ads.Dfa.Lib {
  /// <summary>
  /// Lists all the services available through this library.
  /// </summary>
  public partial class DfaService : AdsService {
    /// <summary>
    /// All the services available in v1.19.
    /// </summary>
    public class v1_19 {
      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.19/ad/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature AdRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.19/advertiser/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature AdvertiserRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.19/advertisergroup/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature AdvertiserGroupRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.19/campaign/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.19/changelog/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature ChangeLogRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.19/contentcategory/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature ContentCategoryRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.19/creative/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature CreativeRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.19/creativefield/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature CreativeFieldRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.19/creativegroup/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature CreativeGroupRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.19/login/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature LoginRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.19/network/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature NetworkRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.19/placement/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature PlacementRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.19/site/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature SiteRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.19/size/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature SizeRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.19/spotlight/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature SpotlightRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.19/placementstrategy/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature PlacementStrategyRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.19/subnetwork/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature SubnetworkRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.19/user/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature UserRemoteService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/dfa/docs/reference/v1.19/userrole/service.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature UserRoleRemoteService;

      /// <summary>
      /// Factory type for v1.19 services.
      /// </summary>
      public static readonly Type factoryType = typeof(DfaServiceFactory);

      /// <summary>
      /// Static constructor to initialize the service constants.
      /// </summary>
      static v1_19() {
        AdRemoteService = DfaService.MakeServiceSignature("v1.19", "AdRemoteService");
        AdvertiserRemoteService = DfaService.MakeServiceSignature("v1.19",
            "AdvertiserRemoteService");
        AdvertiserGroupRemoteService = DfaService.MakeServiceSignature("v1.19",
            "AdvertiserGroupRemoteService");
        CampaignRemoteService = DfaService.MakeServiceSignature("v1.19", "CampaignRemoteService");
        ChangeLogRemoteService = DfaService.MakeServiceSignature("v1.19", "ChangeLogRemoteService");
        ContentCategoryRemoteService = DfaService.MakeServiceSignature("v1.19",
            "ContentCategoryRemoteService");
        CreativeRemoteService = DfaService.MakeServiceSignature("v1.19", "CreativeRemoteService");
        CreativeFieldRemoteService = DfaService.MakeServiceSignature("v1.19",
            "CreativeFieldRemoteService");
        CreativeGroupRemoteService = DfaService.MakeServiceSignature("v1.19",
            "CreativeGroupRemoteService");
        LoginRemoteService = DfaService.MakeServiceSignature("v1.19", "LoginRemoteService");
        NetworkRemoteService = DfaService.MakeServiceSignature("v1.19", "NetworkRemoteService");
        PlacementRemoteService = DfaService.MakeServiceSignature("v1.19", "PlacementRemoteService");
        SiteRemoteService = DfaService.MakeServiceSignature("v1.19", "SiteRemoteService");
        SizeRemoteService = DfaService.MakeServiceSignature("v1.19", "SizeRemoteService");
        SpotlightRemoteService = DfaService.MakeServiceSignature("v1.19", "SpotlightRemoteService");
        PlacementStrategyRemoteService = DfaService.MakeServiceSignature("v1.19",
            "PlacementStrategyRemoteService");
        SubnetworkRemoteService = DfaService.MakeServiceSignature("v1.19",
            "SubnetworkRemoteService");
        UserRemoteService = DfaService.MakeServiceSignature("v1.19", "UserRemoteService");
        UserRoleRemoteService = DfaService.MakeServiceSignature("v1.19", "UserRoleRemoteService");
      }
    }
  }
}
