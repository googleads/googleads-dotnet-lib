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

// Author: thagikura@gmail.com (Takeshi Hagikura)

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
    /// All the services available in v201306.
    /// </summary>
    public class v201306 {
      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201306/AdExtensionOverrideService.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature AdExtensionOverrideService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201306/AdGroupAdService.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupAdService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201306/AdGroupCriterionService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupCriterionService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201306/AdGroupFeedService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupFeedService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201306/AdGroupService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201306/AdGroupBidModifierService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupBidModifierService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201306/AdParamService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdParamService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201306/AdwordsUserListService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdwordsUserListService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201306/AlertService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AlertService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201306/BiddingStrategyService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature BiddingStrategyService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201306/BudgetService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature BudgetService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201306/BudgetOrderService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature BudgetOrderService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201306/CampaignAdExtensionService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignAdExtensionService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201306/CampaignCriterionService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignCriterionService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201306/CampaignFeedService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignFeedService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201306/CampaignService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201306/CampaignSharedSetService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignSharedSetService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201306/ConstantDataService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ConstantDataService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201306/ConversionTrackerService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ConversionTrackerService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201306/CustomerService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CustomerService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201306/CustomerSyncService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CustomerSyncService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201306/DataService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature DataService;
      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201306/ExperimentService.html">
      /// this page</a> for details.
      /// </summary>

      public static readonly ServiceSignature ExperimentService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201306/FeedService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature FeedService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201306/FeedItemService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature FeedItemService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201306/FeedMappingService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature FeedMappingService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201306/GeoLocationService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature GeoLocationService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201306/LocationCriterionService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature LocationCriterionService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201306/ManagedCustomerService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ManagedCustomerService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201306/MediaService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature MediaService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201306/MutateJobService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature MutateJobService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201306/ReportDefinitionService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ReportDefinitionService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201306/SharedCriterionService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature SharedCriterionService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201306/SharedSetService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature SharedSetService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201306/TargetingIdeaService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature TargetingIdeaService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201306/TrafficEstimatorService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature TrafficEstimatorService;

      /// <summary>
      /// Factory type for v201306 services.
      /// </summary>
      public static readonly Type factoryType = typeof(AdWordsServiceFactory);

      /// <summary>
      /// Static constructor to initialize the service constants.
      /// </summary>
      static v201306() {
        AdExtensionOverrideService =
            AdWordsService.MakeServiceSignature("v201306", "cm", "AdExtensionOverrideService");
        AdGroupAdService =
            AdWordsService.MakeServiceSignature("v201306", "cm", "AdGroupAdService");
        AdGroupCriterionService =
            AdWordsService.MakeServiceSignature("v201306", "cm", "AdGroupCriterionService");
        AdGroupService =
            AdWordsService.MakeServiceSignature("v201306", "cm", "AdGroupService");
        AdGroupBidModifierService =
            AdWordsService.MakeServiceSignature("v201306", "cm", "AdGroupBidModifierService");
        AdGroupFeedService =
            AdWordsService.MakeServiceSignature("v201306", "cm", "AdGroupFeedService");
        AdParamService =
            AdWordsService.MakeServiceSignature("v201306", "cm", "AdParamService");
        AdwordsUserListService =
            AdWordsService.MakeServiceSignature("v201306", "rm", "AdWordsUserListService");
        AlertService =
            AdWordsService.MakeServiceSignature("v201306", "mcm", "AlertService");
        BiddingStrategyService =
            AdWordsService.MakeServiceSignature("v201306", "cm", "BiddingStrategyService");
        BudgetService =
            AdWordsService.MakeServiceSignature("v201306", "cm", "BudgetService");
        BudgetOrderService =
            AdWordsService.MakeServiceSignature("v201306", "billing", "BudgetOrderService");
        CampaignAdExtensionService =
            AdWordsService.MakeServiceSignature("v201306", "cm", "CampaignAdExtensionService");
        CampaignCriterionService =
            AdWordsService.MakeServiceSignature("v201306", "cm", "CampaignCriterionService");
        CampaignService =
            AdWordsService.MakeServiceSignature("v201306", "cm", "CampaignService");
        CampaignFeedService =
            AdWordsService.MakeServiceSignature("v201306", "cm", "CampaignFeedService");
        CampaignSharedSetService =
            AdWordsService.MakeServiceSignature("v201306", "cm", "CampaignSharedSetService");
        ConstantDataService =
            AdWordsService.MakeServiceSignature("v201306", "cm", "ConstantDataService");
        ConversionTrackerService =
            AdWordsService.MakeServiceSignature("v201306", "cm", "ConversionTrackerService");
        CustomerService =
            AdWordsService.MakeServiceSignature("v201306", "mcm", "CustomerService");
        CustomerSyncService =
            AdWordsService.MakeServiceSignature("v201306", "ch", "CustomerSyncService");
        DataService =
            AdWordsService.MakeServiceSignature( "v201306", "cm", "DataService");
        ExperimentService =
            AdWordsService.MakeServiceSignature("v201306", "cm", "ExperimentService");
        FeedService =
            AdWordsService.MakeServiceSignature("v201306", "cm", "FeedService");
        FeedItemService =
            AdWordsService.MakeServiceSignature("v201306", "cm", "FeedItemService");
        FeedMappingService =
            AdWordsService.MakeServiceSignature("v201306", "cm", "FeedMappingService");
        GeoLocationService =
            AdWordsService.MakeServiceSignature("v201306", "cm", "GeoLocationService");
        LocationCriterionService =
            AdWordsService.MakeServiceSignature("v201306", "cm", "LocationCriterionService");
        ManagedCustomerService =
            AdWordsService.MakeServiceSignature("v201306", "mcm", "ManagedCustomerService");
        MediaService =
            AdWordsService.MakeServiceSignature("v201306", "cm", "MediaService");
        MutateJobService =
            AdWordsService.MakeServiceSignature("v201306", "cm", "MutateJobService");
        ReportDefinitionService =
            AdWordsService.MakeServiceSignature("v201306", "cm", "ReportDefinitionService");
        SharedCriterionService =
            AdWordsService.MakeServiceSignature("v201306", "cm", "SharedCriterionService");
        SharedSetService =
            AdWordsService.MakeServiceSignature("v201306", "cm", "SharedSetService");
        TargetingIdeaService =
            AdWordsService.MakeServiceSignature("v201306", "o", "TargetingIdeaService");
        TrafficEstimatorService =
            AdWordsService.MakeServiceSignature("v201306", "o", "TrafficEstimatorService");
      }
    }
  }
}
