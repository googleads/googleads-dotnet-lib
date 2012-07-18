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

namespace Google.Api.Ads.AdWords.Lib {
  /// <summary>
  /// Lists all the services available through this library.
  /// </summary>
  public partial class AdWordsService : AdsService {
    /// <summary>
    /// All the services available in v201206.
    /// </summary>
    public class v201206 {
      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201206/AdExtensionOverrideService.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature AdExtensionOverrideService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201206/AdGroupAdService.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupAdService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201206/AdGroupCriterionService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupCriterionService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201206/AdGroupService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201206/AdParamService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdParamService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201206/AlertService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AlertService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201206/BudgetOrderService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature BudgetOrderService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201206/BulkMutateJobService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature BulkMutateJobService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201206/CampaignAdExtensionService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignAdExtensionService;
      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201206/CampaignCriterionService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignCriterionService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201206/CampaignService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201206/ConstantDataService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ConstantDataService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201206/ConversionTrackerService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ConversionTrackerService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201206/CustomerService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CustomerService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201206/CustomerSyncService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CustomerSyncService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201206/DataService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature DataService;
      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201206/ExperimentService.html">
      /// this page</a> for details.
      /// </summary>

      public static readonly ServiceSignature ExperimentService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201206/GeoLocationService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature GeoLocationService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201206/InfoService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature InfoService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201206/LocationCriterionService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature LocationCriterionService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201206/ManagedCustomerService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ManagedCustomerService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201206/MediaService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature MediaService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201206/MutateJobService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature MutateJobService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201206/ReportDefinitionService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ReportDefinitionService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201206/TargetingIdeaService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature TargetingIdeaService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201206/TrafficEstimatorService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature TrafficEstimatorService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201206/UserListService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature UserListService;

      /// <summary>
      /// Factory type for v201206 services.
      /// </summary>
      public static readonly Type factoryType = typeof(AdWordsServiceFactory);

      /// <summary>
      /// Static constructor to initialize the service constants.
      /// </summary>
      static v201206() {
        AdExtensionOverrideService =
            AdWordsService.MakeServiceSignature("v201206", "cm", "AdExtensionOverrideService");
        AdGroupAdService =
            AdWordsService.MakeServiceSignature("v201206", "cm", "AdGroupAdService");
        AdGroupCriterionService =
            AdWordsService.MakeServiceSignature("v201206", "cm", "AdGroupCriterionService");
        AdGroupService =
            AdWordsService.MakeServiceSignature("v201206", "cm", "AdGroupService");
        AdParamService =
            AdWordsService.MakeServiceSignature("v201206", "cm", "AdParamService");
        AlertService =
            AdWordsService.MakeServiceSignature("v201206", "mcm", "AlertService");
        BudgetOrderService =
            AdWordsService.MakeServiceSignature("v201206", "billing", "BudgetOrderService");
        BulkMutateJobService =
            AdWordsService.MakeServiceSignature("v201206", "job", "BulkMutateJobService");
        CampaignAdExtensionService =
            AdWordsService.MakeServiceSignature("v201206", "cm", "CampaignAdExtensionService");
        CampaignCriterionService =
            AdWordsService.MakeServiceSignature("v201206", "cm", "CampaignCriterionService");
        CampaignService =
            AdWordsService.MakeServiceSignature("v201206", "cm", "CampaignService");
        ConstantDataService =
            AdWordsService.MakeServiceSignature("v201206", "cm", "ConstantDataService");
        ConversionTrackerService =
            AdWordsService.MakeServiceSignature("v201206", "cm", "ConversionTrackerService");
        CustomerService =
            AdWordsService.MakeServiceSignature("v201206", "mcm", "CustomerService");
        CustomerSyncService =
            AdWordsService.MakeServiceSignature("v201206", "ch", "CustomerSyncService");
        DataService =
            AdWordsService.MakeServiceSignature( "v201206", "cm", "DataService");
        ExperimentService =
            AdWordsService.MakeServiceSignature("v201206", "cm", "ExperimentService");
        GeoLocationService =
            AdWordsService.MakeServiceSignature("v201206", "cm", "GeoLocationService");
        InfoService =
            AdWordsService.MakeServiceSignature("v201206", "info", "InfoService");
        LocationCriterionService =
            AdWordsService.MakeServiceSignature("v201206", "cm", "LocationCriterionService");
        ManagedCustomerService =
            AdWordsService.MakeServiceSignature("v201206", "mcm", "ManagedCustomerService");
        MediaService =
            AdWordsService.MakeServiceSignature("v201206", "cm", "MediaService");
        MutateJobService =
            AdWordsService.MakeServiceSignature("v201206", "cm", "MutateJobService");
        ReportDefinitionService =
            AdWordsService.MakeServiceSignature("v201206", "cm", "ReportDefinitionService");
        TargetingIdeaService =
            AdWordsService.MakeServiceSignature("v201206", "o", "TargetingIdeaService");
        TrafficEstimatorService =
            AdWordsService.MakeServiceSignature("v201206", "o", "TrafficEstimatorService");
        UserListService =
            AdWordsService.MakeServiceSignature("v201206", "cm", "UserListService");
      }
    }
  }
}
