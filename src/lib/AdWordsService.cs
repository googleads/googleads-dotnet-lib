// Copyright 2009, Google Inc. All Rights Reserved.
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

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Services.Protocols;

namespace com.google.api.adwords.lib {
  /// <summary>
  /// Lists all the services available through this library.
  /// </summary>
  public partial class AdWordsService {
    /// <summary>
    /// Creates a service creation parameter for defining an AdWords service.
    /// </summary>
    /// <param name="version">Service version, for instance, v200906.</param>
    /// <param name="groupName">Group name, for instance, cm.</param>
    /// <param name="serviceName">Service name, for instance, CampaignService.
    /// </param>
    /// <returns>A service creation parameter defining this service.</returns>
    internal static ServiceSignature MakeServiceSignature(string version, string groupName,
        string serviceName) {
      AdWordsApiServiceSignature signature = new AdWordsApiServiceSignature();
      signature.id = version + "." + serviceName;
      signature.serviceName = serviceName;
      signature.groupName = groupName;
      signature.version = version;

      return signature;
    }

    /// <summary>
    /// Creates a service creation parameter for defining a v13 service.
    /// </summary>
    /// <param name="version">Service version, for instance, v13.</param>
    /// <param name="serviceName">Service name, for instance, CampaignService.
    /// </param>
    /// <returns>A service creation parameter defining this service.</returns>
    protected static ServiceSignature MakeLegacyServiceSignature(string version,
        string serviceName) {
      LegacyAdwordsApiServiceSignature param = new LegacyAdwordsApiServiceSignature();
      param.id = version + "." + serviceName;
      param.serviceName = serviceName;
      param.version = version;

      return param;
    }

    /// <summary>
    /// All the services available in v200906.
    /// </summary>
    public class v200906 {
      /// <summary>
      /// Static constructor to initialize the service constants.
      /// </summary>
      static v200906() {
        AdGroupAdService = AdWordsService.MakeServiceSignature("v200906", "cm", "AdGroupAdService");
        AdGroupCriterionService =
            AdWordsService.MakeServiceSignature("v200906", "cm", "AdGroupCriterionService");
        AdGroupService =
            AdWordsService.MakeServiceSignature("v200906", "cm", "AdGroupService");
        CampaignCriterionService =
            AdWordsService.MakeServiceSignature("v200906", "cm", "CampaignCriterionService");
        CampaignService =
            AdWordsService.MakeServiceSignature("v200906", "cm", "CampaignService");
        CampaignTargetService =
            AdWordsService.MakeServiceSignature("v200906", "cm", "CampaignTargetService");
      }
      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/v2009/docs/reference/AdGroupAdService.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupAdService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/v2009/docs/reference/AdGroupCriterionService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupCriterionService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/v2009/docs/reference/AdGroupService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/v2009/docs/reference/CampaignCriterionService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignCriterionService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/v2009/docs/reference/CampaignService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/v2009/docs/reference/CampaignTargetService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignTargetService;
    }

    /// <summary>
    /// All the services available in v200909.
    /// </summary>
    public class v200909 {
      /// <summary>
      /// Static constructor to initialize the service constants.
      /// </summary>
      static v200909() {
        AdExtensionOverrideService =
            AdWordsService.MakeServiceSignature("v200909", "cm", "AdExtensionOverrideService");
        AdGroupAdService =
            AdWordsService.MakeServiceSignature("v200909", "cm", "AdGroupAdService");
        AdGroupCriterionService =
            AdWordsService.MakeServiceSignature("v200909", "cm", "AdGroupCriterionService");
        AdGroupService =
            AdWordsService.MakeServiceSignature("v200909", "cm", "AdGroupService");
        BulkMutateJobService =
            AdWordsService.MakeServiceSignature("v200909", "job", "BulkMutateJobService");
        CampaignAdExtensionService =
            AdWordsService.MakeServiceSignature("v200909", "cm", "CampaignAdExtensionService");
        CampaignCriterionService =
            AdWordsService.MakeServiceSignature("v200909", "cm", "CampaignCriterionService");
        CampaignService =
            AdWordsService.MakeServiceSignature("v200909", "cm", "CampaignService");
        CampaignTargetService =
            AdWordsService.MakeServiceSignature("v200909", "cm", "CampaignTargetService");
        GeoLocationService =
            AdWordsService.MakeServiceSignature("v200909", "cm", "GeoLocationService");
        AdParamService =
            AdWordsService.MakeServiceSignature("v200909", "cm", "AdParamService");
        InfoService =
            AdWordsService.MakeServiceSignature("v200909", "info", "InfoService");
        TargetingIdeaService =
            AdWordsService.MakeServiceSignature("v200909", "o", "TargetingIdeaService");
      }

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/v2009/docs/reference/AdExtensionOverrideService.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature AdExtensionOverrideService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/v2009/docs/reference/AdGroupAdService.html">
      /// this page </a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupAdService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/v2009/docs/reference/AdGroupCriterionService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupCriterionService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/v2009/docs/reference/AdGroupService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/v2009/docs/reference/BulkMutateJobService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature BulkMutateJobService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/v2009/docs/reference/CampaignAdExtensionService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignAdExtensionService;
      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/v2009/docs/reference/CampaignCriterionService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignCriterionService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/v2009/docs/reference/CampaignService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/v2009/docs/reference/CampaignTargetService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignTargetService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/v2009/docs/reference/GeoLocationService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature GeoLocationService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/v2009/docs/reference/AdParamService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdParamService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/v2009/docs/reference/InfoService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature InfoService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/v2009/docs/reference/TargetingIdeaService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature TargetingIdeaService;
    }

    /// <summary>
    /// All the services availble in v13.
    /// </summary>
    public class v13 {
      /// <summary>
      /// Static constructor to initialize the service constants.
      /// </summary>
      static v13() {
        AccountService = AdWordsService.MakeLegacyServiceSignature("v13", "AccountService");
        AdGroupService = AdWordsService.MakeLegacyServiceSignature("v13", "AdGroupService");
        AdService = AdWordsService.MakeLegacyServiceSignature("v13", "AdService");
        CampaignService = AdWordsService.MakeLegacyServiceSignature("v13", "CampaignService");
        CriterionService = AdWordsService.MakeLegacyServiceSignature("v13", "CriterionService");
        InfoService = AdWordsService.MakeLegacyServiceSignature("v13", "InfoService");
        KeywordToolService = AdWordsService.MakeLegacyServiceSignature("v13", "KeywordToolService");
        ReportService = AdWordsService.MakeLegacyServiceSignature("v13", "ReportService");
        SiteSuggestionService =
            AdWordsService.MakeLegacyServiceSignature("v13", "SiteSuggestionService");
        TrafficEstimatorService =
            AdWordsService.MakeLegacyServiceSignature("v13", "TrafficEstimatorService");
      }

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/developer/AccountService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AccountService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/developer/AdGroupService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdGroupService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/developer/AdService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature AdService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/developer/CampaignService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CampaignService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/developer/CriterionService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature CriterionService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/developer/InfoService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature InfoService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/developer/KeywordToolService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature KeywordToolService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/developer/ReportService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature ReportService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/developer/SiteSuggestionService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature SiteSuggestionService;

      /// <summary>
      /// See <a href="http://code.google.com/apis/adwords/docs/developer/TrafficEstimatorService.html">
      /// this page</a> for details.
      /// </summary>
      public static readonly ServiceSignature TrafficEstimatorService;
    }

    /// <summary>
    /// Registers all known services against a given user.
    /// </summary>
    /// <param name="user">The user object to which service type registration
    /// should be done.</param>
    internal static void RegisterServices(AdWordsUser user) {
      Type[] enumTypes = {typeof(v13), typeof(v200906), typeof(v200909)};
      ServiceFactory[] factories = {new LegacyAdWordsApiServiceFactory(),
          new AdWordsApiServiceFactory(), new AdWordsApiServiceFactory()};

      for (int i = 0; i < enumTypes.Length; i++) {
        Type enumType = enumTypes[i];
        ServiceFactory serviceFactory = factories[i];

        FieldInfo[] fields = enumType.GetFields();
        foreach (FieldInfo field in fields) {
          ServiceSignature signature = (ServiceSignature) field.GetValue(null);
          user.RegisterService(signature.id, serviceFactory);
        }
      }
    }
  }

  public partial class AdWordsUser {
    /// <summary>
    /// Extended AdWordsUser constructor to support AdWords API services
    /// (Legacy and new).
    /// </summary>
    /// <param name="headers">The headers as a set of key-value pairs.</param>
    public AdWordsUser(Dictionary<string, string> headers) : this() {
      Dictionary<string, SoapHeader> legacyHeader = null;
      RequestHeader requestHeader = null;

      foreach (string id in serviceFactoryMap.Keys) {
        ServiceFactory serviceFactory = serviceFactoryMap[id];
        if (serviceFactory is LegacyAdWordsApiServiceFactory) {
          LegacyAdWordsApiServiceFactory legacyFactory =
                (serviceFactory as LegacyAdWordsApiServiceFactory);
          if (legacyHeader == null) {
            legacyHeader = legacyFactory.MakeSoapHeaders(headers);
          }
          legacyFactory.Headers = legacyHeader;
        } else if (serviceFactory is AdWordsApiServiceFactory) {
          AdWordsApiServiceFactory awapiFactory = (serviceFactory as AdWordsApiServiceFactory);
          if (requestHeader == null) {
            requestHeader = awapiFactory.MakeRequestHeaders(headers);
          }
          awapiFactory.RequestHeader = requestHeader;
        }
      }
    }
  }
}
