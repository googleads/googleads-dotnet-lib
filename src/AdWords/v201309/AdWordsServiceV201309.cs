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
    /// All the services available in v201309.
    /// </summary>
    public class v201309 {
      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201309/OfflineConversionFeedService.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature OfflineConversionFeedService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201309/AdGroupAdService.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupAdService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201309/AdGroupCriterionService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupCriterionService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201309/AdGroupFeedService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupFeedService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201309/AdGroupService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201309/AdGroupBidModifierService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupBidModifierService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201309/AdParamService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdParamService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201309/AdwordsUserListService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdwordsUserListService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201309/AlertService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AlertService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201309/BiddingStrategyService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature BiddingStrategyService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201309/BudgetService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature BudgetService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201309/BudgetOrderService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature BudgetOrderService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201309/CampaignAdExtensionService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignAdExtensionService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201309/CampaignCriterionService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignCriterionService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201309/CampaignFeedService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignFeedService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201309/CampaignService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201309/CampaignSharedSetService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignSharedSetService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201309/ConstantDataService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ConstantDataService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201309/ConversionTrackerService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ConversionTrackerService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201309/CustomerService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CustomerService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201309/CustomerSyncService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CustomerSyncService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201309/DataService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature DataService;
      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201309/ExperimentService.html">
      /// this page</a> for details.
      /// </summary>

      public static readonly ServiceSignature ExperimentService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201309/FeedService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature FeedService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201309/FeedItemService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature FeedItemService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201309/FeedMappingService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature FeedMappingService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201309/GeoLocationService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature GeoLocationService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201309/LocationCriterionService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature LocationCriterionService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201309/ManagedCustomerService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ManagedCustomerService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201309/MediaService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature MediaService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201309/MutateJobService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature MutateJobService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201309/ReportDefinitionService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ReportDefinitionService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201309/SharedCriterionService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature SharedCriterionService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201309/SharedSetService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature SharedSetService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201309/TargetingIdeaService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature TargetingIdeaService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/reference/v201309/TrafficEstimatorService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature TrafficEstimatorService;

      /// <summary>
      /// Factory type for v201309 services.
      /// </summary>
      public static readonly Type factoryType = typeof(AdWordsServiceFactory);

      /// <summary>
      /// Static constructor to initialize the service constants.
      /// </summary>
      static v201309() {
        OfflineConversionFeedService =
            AdWordsService.MakeServiceSignature("v201309", "cm", "OfflineConversionFeedService");
        AdGroupAdService =
            AdWordsService.MakeServiceSignature("v201309", "cm", "AdGroupAdService");
        AdGroupCriterionService =
            AdWordsService.MakeServiceSignature("v201309", "cm", "AdGroupCriterionService");
        AdGroupService =
            AdWordsService.MakeServiceSignature("v201309", "cm", "AdGroupService");
        AdGroupBidModifierService =
            AdWordsService.MakeServiceSignature("v201309", "cm", "AdGroupBidModifierService");
        AdGroupFeedService =
            AdWordsService.MakeServiceSignature("v201309", "cm", "AdGroupFeedService");
        AdParamService =
            AdWordsService.MakeServiceSignature("v201309", "cm", "AdParamService");
        AdwordsUserListService =
            AdWordsService.MakeServiceSignature("v201309", "rm", "AdWordsUserListService");
        AlertService =
            AdWordsService.MakeServiceSignature("v201309", "mcm", "AlertService");
        BiddingStrategyService =
            AdWordsService.MakeServiceSignature("v201309", "cm", "BiddingStrategyService");
        BudgetService =
            AdWordsService.MakeServiceSignature("v201309", "cm", "BudgetService");
        BudgetOrderService =
            AdWordsService.MakeServiceSignature("v201309", "billing", "BudgetOrderService");
        CampaignAdExtensionService =
            AdWordsService.MakeServiceSignature("v201309", "cm", "CampaignAdExtensionService");
        CampaignCriterionService =
            AdWordsService.MakeServiceSignature("v201309", "cm", "CampaignCriterionService");
        CampaignService =
            AdWordsService.MakeServiceSignature("v201309", "cm", "CampaignService");
        CampaignFeedService =
            AdWordsService.MakeServiceSignature("v201309", "cm", "CampaignFeedService");
        CampaignSharedSetService =
            AdWordsService.MakeServiceSignature("v201309", "cm", "CampaignSharedSetService");
        ConstantDataService =
            AdWordsService.MakeServiceSignature("v201309", "cm", "ConstantDataService");
        ConversionTrackerService =
            AdWordsService.MakeServiceSignature("v201309", "cm", "ConversionTrackerService");
        CustomerService =
            AdWordsService.MakeServiceSignature("v201309", "mcm", "CustomerService");
        CustomerSyncService =
            AdWordsService.MakeServiceSignature("v201309", "ch", "CustomerSyncService");
        DataService =
            AdWordsService.MakeServiceSignature( "v201309", "cm", "DataService");
        ExperimentService =
            AdWordsService.MakeServiceSignature("v201309", "cm", "ExperimentService");
        FeedService =
            AdWordsService.MakeServiceSignature("v201309", "cm", "FeedService");
        FeedItemService =
            AdWordsService.MakeServiceSignature("v201309", "cm", "FeedItemService");
        FeedMappingService =
            AdWordsService.MakeServiceSignature("v201309", "cm", "FeedMappingService");
        GeoLocationService =
            AdWordsService.MakeServiceSignature("v201309", "cm", "GeoLocationService");
        LocationCriterionService =
            AdWordsService.MakeServiceSignature("v201309", "cm", "LocationCriterionService");
        ManagedCustomerService =
            AdWordsService.MakeServiceSignature("v201309", "mcm", "ManagedCustomerService");
        MediaService =
            AdWordsService.MakeServiceSignature("v201309", "cm", "MediaService");
        MutateJobService =
            AdWordsService.MakeServiceSignature("v201309", "cm", "MutateJobService");
        ReportDefinitionService =
            AdWordsService.MakeServiceSignature("v201309", "cm", "ReportDefinitionService");
        SharedCriterionService =
            AdWordsService.MakeServiceSignature("v201309", "cm", "SharedCriterionService");
        SharedSetService =
            AdWordsService.MakeServiceSignature("v201309", "cm", "SharedSetService");
        TargetingIdeaService =
            AdWordsService.MakeServiceSignature("v201309", "o", "TargetingIdeaService");
        TrafficEstimatorService =
            AdWordsService.MakeServiceSignature("v201309", "o", "TrafficEstimatorService");
      }
    }
  }
}
