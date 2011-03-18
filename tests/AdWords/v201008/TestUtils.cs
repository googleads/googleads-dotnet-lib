// Copyright 2010, Google Inc. All Rights Reserved.
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

using com.google.api.adwords.lib;
using com.google.api.adwords.lib.util;
using com.google.api.adwords.v201008;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace com.google.api.adwords.tests.v201008 {
  /// <summary>
  /// A utility class to assist the testing of v201008 services.
  /// </summary>
  class TestUtils {
    /// <summary>
    /// Creates a test campaign for running further tests.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <returns>The campaign id.</returns>
    public long CreateCampaign(AdWordsUser user, bool cpc) {
      CampaignService campaignService =
          (CampaignService) user.GetService(AdWordsService.v201008.CampaignService);
      long campaignId = 0;

      CampaignOperation campaignOperation = new CampaignOperation();
      campaignOperation.operatorSpecified = true;
      campaignOperation.@operator = Operator.ADD;
      campaignOperation.operand = new Campaign();
      campaignOperation.operand.name =
          string.Format("Campaign {0}", DateTime.Now.ToString("yyyy-M-d H:m:s.ffffff"));
      campaignOperation.operand.statusSpecified = true;
      campaignOperation.operand.status = CampaignStatus.PAUSED;
      if (cpc == true) {
        campaignOperation.operand.biddingStrategy = new ManualCPC();
      } else {
        campaignOperation.operand.biddingStrategy = new ManualCPM();
      }
      campaignOperation.operand.budget = new Budget();
      campaignOperation.operand.budget.period = BudgetBudgetPeriod.DAILY;
      campaignOperation.operand.budget.periodSpecified = true;
      campaignOperation.operand.budget.amount = new Money();
      campaignOperation.operand.budget.amount.microAmountSpecified = true;
      campaignOperation.operand.budget.amount.microAmount = 100000000;
      campaignOperation.operand.budget.deliveryMethodSpecified = true;
      campaignOperation.operand.budget.deliveryMethod = BudgetBudgetDeliveryMethod.STANDARD;

      CampaignReturnValue retVal =
          campaignService.mutate(new CampaignOperation[] {campaignOperation});
      if (retVal != null && retVal.value != null && retVal.value.Length > 0) {
        campaignId = retVal.value[0].id;
      }
      return campaignId;
    }

    /// <summary>
    /// Sets test targets for a campaign for running further tests.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="campaignId">The campaign id for which the targets are set.
    /// </param>
    /// <returns>The list of campaign targets.</returns>
    public TargetList[] SetCampaignTargets(AdWordsUser user, long campaignId) {
      // Get the CampaignTargetService.
      CampaignTargetService campaignTargetService =
          (CampaignTargetService)user.GetService(AdWordsService.v201008.CampaignTargetService);

      // Create language targets.
      LanguageTargetList langTargetList = new LanguageTargetList();
      langTargetList.campaignIdSpecified = true;
      langTargetList.campaignId = campaignId;

      LanguageTarget langTarget1 = new LanguageTarget();
      langTarget1.languageCode = "fr";

      LanguageTarget langTarget2 = new LanguageTarget();
      langTarget2.languageCode = "ja";

      langTargetList.targets = new LanguageTarget[] { langTarget1, langTarget2 };

      // Create language target set operation.
      CampaignTargetOperation langTargetOperation = new CampaignTargetOperation();
      langTargetOperation.operatorSpecified = true;
      langTargetOperation.@operator = Operator.SET;
      langTargetOperation.operand = langTargetList;

      // Create geo targets.
      GeoTargetList geoTargetList = new GeoTargetList();
      geoTargetList.campaignIdSpecified = true;
      geoTargetList.campaignId = campaignId;

      CountryTarget geoTarget1 = new CountryTarget();
      geoTarget1.countryCode = "US";

      CountryTarget geoTarget2 = new CountryTarget();
      geoTarget2.countryCode = "JP";

      // Create geo target set operation.
      CampaignTargetOperation geoTargetOperation = new CampaignTargetOperation();
      geoTargetOperation.operatorSpecified = true;
      geoTargetOperation.@operator = Operator.SET;
      geoTargetOperation.operand = geoTargetList;

      // Create network targets.
      NetworkTargetList networkTargetList = new NetworkTargetList();
      networkTargetList.campaignIdSpecified = true;
      networkTargetList.campaignId = campaignId;

      // Specifying GOOGLE_SEARCH is necessary if you want to target SEARCH_NETWORK.
      NetworkTarget networkTarget1 = new NetworkTarget();
      networkTarget1.networkCoverageTypeSpecified = true;
      networkTarget1.networkCoverageType = NetworkCoverageType.GOOGLE_SEARCH;

      NetworkTarget networkTarget2 = new NetworkTarget();
      networkTarget2.networkCoverageTypeSpecified = true;
      networkTarget2.networkCoverageType = NetworkCoverageType.SEARCH_NETWORK;

      networkTargetList.targets = new NetworkTarget[] { networkTarget1, networkTarget2 };

      // Create network target set operation.
      CampaignTargetOperation networkTargetOperation = new CampaignTargetOperation();
      networkTargetOperation.operatorSpecified = true;
      networkTargetOperation.@operator = Operator.SET;
      networkTargetOperation.operand = networkTargetList;

      // Set campaign targets.
      CampaignTargetReturnValue result =
          campaignTargetService.mutate(new CampaignTargetOperation[] {
              geoTargetOperation, langTargetOperation,networkTargetOperation});

      if (result != null && result.value != null) {
        return result.value;
      } else {
        return new TargetList[] { };
      }
    }

    /// <summary>
    /// Creates a test adgroup for running further tests.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="campaignId">The campaign id for which the adgroup is created.</param>
    /// <returns>The adgroup id.</returns>
    public long CreateAdGroup(AdWordsUser user, long campaignId) {
      AdGroupService adGroupService =
          (AdGroupService) user.GetService(AdWordsService.v201008.AdGroupService);
      long adGroupId = 0;

      AdGroupOperation adGroupOperation = new AdGroupOperation();
      adGroupOperation.operatorSpecified = true;
      adGroupOperation.@operator = Operator.ADD;
      adGroupOperation.operand = new AdGroup();
      adGroupOperation.operand.campaignIdSpecified = true;
      adGroupOperation.operand.campaignId = campaignId;
      adGroupOperation.operand.name =
          string.Format("AdGroup {0}", DateTime.Now.ToString("yyyy-M-d H:m:s.ffffff"));
      adGroupOperation.operand.status = AdGroupStatus.ENABLED;

      ManualCPCAdGroupBids bids = new ManualCPCAdGroupBids();
      bids.keywordMaxCpc = new Bid();
      bids.keywordMaxCpc.amount = new Money();
      bids.keywordMaxCpc.amount.microAmountSpecified = true;
      bids.keywordMaxCpc.amount.microAmount = 1000000;
      adGroupOperation.operand.bids = bids;

      AdGroupReturnValue retVal = adGroupService.mutate(new AdGroupOperation[] {adGroupOperation});
      if (retVal != null && retVal.value != null && retVal.value.Length > 0) {
        adGroupId = retVal.value[0].id;
      }
      return adGroupId;
    }

    /// <summary>
    /// Creates a test textad for running further tests.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="adGroupId">The adgroup id for which the ad is created.
    /// </param>
    /// <param name="hasAdParam">True, if an ad param placeholder should be
    /// added.</param>
    /// <returns>The text ad id.</returns>
    public long CreateTextAd(AdWordsUser user, long adGroupId, bool hasAdParam) {
      long adId = 0;
      AdGroupAdService adGroupAdService =
          (AdGroupAdService) user.GetService(AdWordsService.v201008.AdGroupAdService);
      AdGroupAdOperation adGroupAdOperation = new AdGroupAdOperation();
      adGroupAdOperation.operatorSpecified = true;
      adGroupAdOperation.@operator = Operator.ADD;
      adGroupAdOperation.operand = new AdGroupAd();
      adGroupAdOperation.operand.adGroupIdSpecified = true;
      adGroupAdOperation.operand.adGroupId = adGroupId;
      TextAd ad = new TextAd();

      ad.headline = "Luxury Cruise to Mars";
      ad.description1 = "Visit the Red Planet in style.";
      if (hasAdParam) {
        ad.description2 = "Low-gravity fun for {param1:cheap}!";
      } else {
        ad.description2 = "Low-gravity fun for everyone!";
      }
      ad.displayUrl = "example.com";
      ad.url = "http://www.example.com";

      adGroupAdOperation.operand.ad = ad;

      AdGroupAdReturnValue retVal =
          adGroupAdService.mutate(new AdGroupAdOperation[] {adGroupAdOperation});
      if (retVal != null && retVal.value != null && retVal.value.Length > 0) {
        adId = retVal.value[0].ad.id;
      }
      return adId;
    }

    /// <summary>
    /// Sets an adparam for running further tests.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="adGroupId">The adgroup id to which criterionId belongs.
    /// </param>
    /// <param name="criterionId">The criterion id to which adparam is set.
    /// </param>
    public void SetAdParam(AdWordsUser user, long adGroupId, long criterionId) {
      AdParamService adParamService =
          (AdParamService) user.GetService(AdWordsService.v201008.AdParamService);

      // Prepare for setting ad parameters.
      AdParam adParam = new AdParam();
      adParam.adGroupIdSpecified = true;
      adParam.adGroupId = adGroupId;
      adParam.criterionIdSpecified = true;
      adParam.criterionId = criterionId;
      adParam.paramIndex = 1;
      adParam.paramIndexSpecified = true;
      adParam.insertionText = "$100";

      AdParamOperation adParamOperation = new AdParamOperation();
      adParamOperation.operatorSpecified = true;
      adParamOperation.@operator = Operator.SET;
      adParamOperation.operand = adParam;

      // Set ad parameters.
      AdParam[] newAdParams = adParamService.mutate(new AdParamOperation[] {adParamOperation});
      return;
    }

    /// <summary>
    /// Creates a keyword for running further tests.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="adGroupId">The adgroup id for which the keyword is created.</param>
    /// <returns>The keyword id.</returns>
    public long CreateKeyword(AdWordsUser user, long adGroupId) {
      long keywordId = 0;
      AdGroupCriterionService adGroupCriterionService =
         (AdGroupCriterionService) user.GetService(AdWordsService.v201008.AdGroupCriterionService);

      AdGroupCriterionOperation operation = new AdGroupCriterionOperation();
      operation.@operator = Operator.ADD;
      operation.operatorSpecified = true;
      operation.operand = new BiddableAdGroupCriterion();
      operation.operand.adGroupId = adGroupId;
      operation.operand.adGroupIdSpecified = true;

      Keyword keyword = new Keyword();
      keyword.matchType = KeywordMatchType.BROAD;
      keyword.matchTypeSpecified = true;
      keyword.text = "mars cruise";

      operation.operand.criterion = keyword;
      AdGroupCriterionReturnValue retVal =
          adGroupCriterionService.mutate(new AdGroupCriterionOperation[] {operation});
      if (retVal != null && retVal.value != null && retVal.value.Length > 0) {
        keywordId = retVal.value[0].criterion.id;
      }
      return keywordId;
    }

    /// <summary>
    /// Creates a campaign negative keyword for running further tests.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="campaignId">The campaign id for which the keyword is
    /// created.</param>
    /// <returns>The keyword id.</returns>
    public long CreateCampaignNegativeKeyword(AdWordsUser user, long campaignId) {
      long keywordId = 0;
      CampaignCriterionService service = (CampaignCriterionService) user.GetService(
          AdWordsService.v201008.CampaignCriterionService);

      NegativeCampaignCriterion criterion = new NegativeCampaignCriterion();

      criterion.campaignId = campaignId;
      criterion.campaignIdSpecified = true;

      Keyword keyword = new Keyword();
      keyword.matchType = KeywordMatchType.BROAD;
      keyword.matchTypeSpecified = true;
      keyword.text = "mars cruise";

      criterion.criterion = keyword;

      CampaignCriterionOperation campaignCriterionOperation = new CampaignCriterionOperation();
      campaignCriterionOperation.operatorSpecified = true;
      campaignCriterionOperation.@operator = Operator.ADD;
      campaignCriterionOperation.operand = criterion;

      CampaignCriterionReturnValue results =
          service.mutate(new CampaignCriterionOperation[] {campaignCriterionOperation});
      if (results != null && results.value != null && results.value.Length > 0) {
        keywordId = (results.value[0].criterion as Keyword).id;
      }
      return keywordId;
    }

    /// <summary>
    /// Creates a campaign ad extension for running further tests.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="campaignId">The campaign id for which extension is
    /// created.</param>
    /// <returns>The campaign ad extension id.</returns>
    public long CreateCampaignAdExtension(AdWordsUser user, long campaignId) {
      long campaignAdExtensionId = 0;

      CampaignAdExtensionService campaignExtensionService =
          (CampaignAdExtensionService) user.GetService(AdWordsService.v201008.
               CampaignAdExtensionService);

      CampaignAdExtensionOperation operation = new CampaignAdExtensionOperation();
      operation.operatorSpecified = true;
      operation.@operator = Operator.ADD;

      CampaignAdExtension extension = new CampaignAdExtension();
      extension.campaignIdSpecified = true;
      extension.campaignId = campaignId;
      extension.statusSpecified = true;
      extension.status = CampaignAdExtensionStatus.ACTIVE;

      Address address = new Address();
      address.streetAddress = "1600 Amphitheatre Pkwy, Mountain View";
      address.countryCode = "US";

      GeoLocation location = GetLocationForAddress(user, address);

      LocationExtension locationExtension = new LocationExtension();

      // Note: Do not populate an address directly. Instead, use
      // GeoLocationService to obtain the location of an address,
      // and use the address as per the location it returns.
      locationExtension.address = location.address;
      locationExtension.geoPoint = location.geoPoint;
      locationExtension.encodedLocation = location.encodedLocation;
      locationExtension.sourceSpecified = true;
      locationExtension.source = LocationExtensionSource.ADWORDS_FRONTEND;

      extension.adExtension = locationExtension;
      operation.operand = extension;
      CampaignAdExtensionReturnValue retval =
          campaignExtensionService.mutate(new CampaignAdExtensionOperation[] {operation});
      if (retval != null && retval.value != null && retval.value.Length > 0) {
        CampaignAdExtension campaignExtension = retval.value[0];
        campaignAdExtensionId = campaignExtension.adExtension.id;
      }
      return campaignAdExtensionId;
    }

    /// <summary>
    /// Creates an ad extension override for running further tests.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="campaignAdExtensionId">The campaign ad extension id to be
    /// overridden.</param>
    /// <param name="adId">The ad for which the ad extension should be
    /// overridden.</param>
    /// <param name="location">The overridden address.</param>
    /// <returns>The override id.</returns>
    public long CreateAdExtensionOverride(AdWordsUser user, long campaignAdExtensionId, long adId,
        GeoLocation location) {
      AdExtensionOverrideService adExtensionOverrideService = 
          (AdExtensionOverrideService)user.GetService(
              AdWordsService.v201008.AdExtensionOverrideService);

      AdExtensionOverrideOperation operation = new AdExtensionOverrideOperation();
      operation.operatorSpecified = true;
      operation.@operator = Operator.ADD;

      operation.operand = new AdExtensionOverride();
      operation.operand.adIdSpecified = true;
      operation.operand.adId = adId;

      LocationExtension locationExtension = new LocationExtension();

      locationExtension.idSpecified = true;
      locationExtension.id = campaignAdExtensionId;

      // Note: Do not populate an address directly. Instead, use
      // GeoLocationService to obtain the location of an address,
      // and use the address as per the location it returns.
      locationExtension.address = location.address;
      locationExtension.geoPoint = location.geoPoint;
      locationExtension.encodedLocation = location.encodedLocation;
      locationExtension.sourceSpecified = true;
      locationExtension.source = LocationExtensionSource.ADWORDS_FRONTEND;

      // Optional: Apply this override within 20 kms.
      operation.operand.overrideInfo = new OverrideInfo();
      operation.operand.overrideInfo.Item = new LocationOverrideInfo();
      operation.operand.overrideInfo.Item.radiusSpecified = true;
      operation.operand.overrideInfo.Item.radius = 20;
      operation.operand.overrideInfo.Item.radiusUnitsSpecified = true;
      operation.operand.overrideInfo.Item.radiusUnits = LocationOverrideInfoRadiusUnits.KILOMETERS;

      operation.operand.adExtension = locationExtension;

      AdExtensionOverrideReturnValue retval = adExtensionOverrideService.mutate(
          new AdExtensionOverrideOperation[] { operation });
      return retval.value[0].adExtension.id;
    }
    
    /// <summary>
    /// Returns an image which can be used for creating image ads.
    /// </summary>
    /// <returns>The image data, as an array of bytes.</returns>
    public byte[] GetSandboxImage() {
      return MediaUtilities.GetAssetDataFromUrl(
          "https://sandbox.google.com/sandboximages/image.jpg");
    }

    /// <summary>
    /// Gets the geo location for a given address.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="address">The address for which geolocation should be
    /// fetched.</param>
    /// <returns>Geo location for the address.</returns>
    public GeoLocation GetLocationForAddress(AdWordsUser user, Address address) {
      GeoLocationService geoService =
          (GeoLocationService) user.GetService(AdWordsService.v201008.GeoLocationService);

      GeoLocationSelector selector = new GeoLocationSelector();
      selector.addresses = new Address[] {address};
      return geoService.get(selector)[0];
    }

    /// <summary>
    /// Creates a keyword performance report for running further tests.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="adGroupId">The adgroup id for which the keyword
    /// performance report is generated.</param>
    /// <returns>The keyword id.</returns>
    public long CreateKeywordPerformanceReport(AdWordsUser user, long adGroupId) {
      long reportId = 0;

      // Create ad group predicate.
      Predicate adGroupPredicate = new Predicate();
      adGroupPredicate.field = "AdGroupId";
      adGroupPredicate.@operator = PredicateOperator.EQUALS;
      adGroupPredicate.operatorSpecified = true;
      adGroupPredicate.values = new string[] {adGroupId.ToString()};

      // Create selector.
      Selector selector = new Selector();
      selector.fields = new string[] {"AdGroupId", "Id", "KeywordText", "KeywordMatchType",
      "Impressions", "Clicks", "Cost"};
      selector.predicates = new Predicate[] { adGroupPredicate };
      selector.dateRange = new DateRange();
      selector.dateRange.min = "20100101";
      selector.dateRange.max = DateTime.Today.ToString("yyyyMMdd");

      // Create report definition.
      ReportDefinition reportDefinition = new ReportDefinition();
      reportDefinition.reportName = "Keywords performance report #" +
          new TestUtils().GetTimeStamp();
      reportDefinition.dateRangeType = ReportDefinitionDateRangeType.CUSTOM_DATE;
      reportDefinition.dateRangeTypeSpecified = true;
      reportDefinition.reportType = ReportDefinitionReportType.KEYWORDS_PERFORMANCE_REPORT;
      reportDefinition.reportTypeSpecified = true;
      reportDefinition.downloadFormat = DownloadFormat.XML;
      reportDefinition.downloadFormatSpecified = true;
      reportDefinition.selector = selector;

      // Create operations.
      ReportDefinitionOperation operation = new ReportDefinitionOperation();
      operation.operand = reportDefinition;
      operation.@operator = Operator.ADD;
      operation.operatorSpecified = true;

      ReportDefinitionService reportDefinitionService = (ReportDefinitionService) user.GetService(
          AdWordsService.v201008.ReportDefinitionService);
      ReportDefinition[] result = reportDefinitionService.mutate(
          new ReportDefinitionOperation[] {operation});
      if (result != null && result.Length > 0 && result[0] != null) {
        reportId = result[0].id;
      }
      return reportId;
    }

    /// <summary>
    /// Gets the current timestamp.
    /// </summary>
    /// <returns>The timestamp as a string.</returns>
    public string GetTimeStamp() {
      return (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds.ToString();
    }
  }
}
