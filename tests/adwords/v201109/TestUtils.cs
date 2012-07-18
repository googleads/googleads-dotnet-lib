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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.Common.Util;
using Google.Api.Ads.AdWords.v201109;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Google.Api.Ads.AdWords.Tests.v201109 {
  /// <summary>
  /// A utility class to assist the testing of v201109 services.
  /// </summary>
  class TestUtils {
    /// <summary>
    /// Creates a test campaign for running further tests.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="biddingStrategy">The bidding strategy to be used.</param>
    /// <returns>The campaign id.</returns>
    public long CreateCampaign(AdWordsUser user, BiddingStrategy biddingStrategy) {
      CampaignService campaignService =
          (CampaignService) user.GetService(AdWordsService.v201109.CampaignService);

      CampaignOperation campaignOperation = new CampaignOperation();
      campaignOperation.@operator = Operator.ADD;
      campaignOperation.operand = new Campaign();
      campaignOperation.operand.name =
          string.Format("Campaign {0}", DateTime.Now.ToString("yyyy-M-d H:m:s.ffffff"));
      campaignOperation.operand.status = CampaignStatus.PAUSED;
      campaignOperation.operand.biddingStrategy = biddingStrategy;
      campaignOperation.operand.budget = new Budget();
      campaignOperation.operand.budget.period = BudgetBudgetPeriod.DAILY;
      campaignOperation.operand.budget.amount = new Money();
      campaignOperation.operand.budget.amount.microAmount = 100000000;
      campaignOperation.operand.budget.deliveryMethod = BudgetBudgetDeliveryMethod.STANDARD;

      CampaignReturnValue retVal =
          campaignService.mutate(new CampaignOperation[] {campaignOperation});
      return retVal.value[0].id;
    }

    /// <summary>
    /// Creates a test adgroup for running further tests.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="campaignId">The campaign id for which the adgroup is created.</param>
    /// <returns>The adgroup id.</returns>
    public long CreateAdGroup(AdWordsUser user, long campaignId) {
      return CreateAdGroup(user, campaignId, false);
    }

    /// <summary>
    /// Creates a test adgroup for running further tests.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="campaignId">The campaign id for which the adgroup is created.</param>
    /// <param name="isCpmBid">True, if a ManualCPM bid is to be used.</param>
    /// <returns>The adgroup id.</returns>
    public long CreateAdGroup(AdWordsUser user, long campaignId, bool isCpmBid) {
      AdGroupService adGroupService =
          (AdGroupService) user.GetService(AdWordsService.v201109.AdGroupService);

      AdGroupOperation adGroupOperation = new AdGroupOperation();
      adGroupOperation.@operator = Operator.ADD;
      adGroupOperation.operand = new AdGroup();
      adGroupOperation.operand.campaignId = campaignId;
      adGroupOperation.operand.name =
          string.Format("AdGroup {0}", DateTime.Now.ToString("yyyy-M-d H:m:s.ffffff"));
      adGroupOperation.operand.status = AdGroupStatus.ENABLED;

      if (isCpmBid) {
        ManualCPMAdGroupBids bids = new ManualCPMAdGroupBids();
        bids.maxCpm = new Bid();
        bids.maxCpm.amount = new Money();
        bids.maxCpm.amount.microAmount = 1000000;
        adGroupOperation.operand.bids = bids;
      } else {
        ManualCPCAdGroupBids bids = new ManualCPCAdGroupBids();
        bids.keywordMaxCpc = new Bid();
        bids.keywordMaxCpc.amount = new Money();
        bids.keywordMaxCpc.amount.microAmount = 1000000;
        adGroupOperation.operand.bids = bids;
      }
      AdGroupReturnValue retVal = adGroupService.mutate(new AdGroupOperation[] {adGroupOperation});
      return retVal.value[0].id;
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
      AdGroupAdService adGroupAdService =
          (AdGroupAdService) user.GetService(AdWordsService.v201109.AdGroupAdService);
      AdGroupAdOperation adGroupAdOperation = new AdGroupAdOperation();
      adGroupAdOperation.@operator = Operator.ADD;
      adGroupAdOperation.operand = new AdGroupAd();
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
      return retVal.value[0].ad.id;
    }

    /// <summary>
    /// Creates a test ThirdPartyRedirectAd for running further tests.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="adGroupId">The adgroup id for which the ad is created.
    /// </param>
    /// <param name="hasAdParam">True, if an ad param placeholder should be
    /// added.</param>
    /// <returns>The text ad id.</returns>
    public long CreateThirdPartyRedirectAd(AdWordsUser user, long adGroupId) {
      AdGroupAdService adGroupAdService =
          (AdGroupAdService) user.GetService(AdWordsService.v201109.AdGroupAdService);
      AdGroupAdOperation adGroupAdOperation = new AdGroupAdOperation();
      adGroupAdOperation.@operator = Operator.ADD;
      adGroupAdOperation.operand = new AdGroupAd();
      adGroupAdOperation.operand.adGroupId = adGroupId;

      // Create the third party redirect ad.
      ThirdPartyRedirectAd redirectAd = new ThirdPartyRedirectAd();
      redirectAd.name = string.Format("Example third party ad #{0}", this.GetTimeStamp());
      redirectAd.url = "http://www.example.com";

      redirectAd.dimensions = new Dimensions();
      redirectAd.dimensions.height = 250;
      redirectAd.dimensions.width = 300;

      // This field normally contains the javascript ad tag.
      redirectAd.snippet = "<img src=\"https://sandbox.google.com/sandboximages/image.jpg\"/>";
      redirectAd.impressionBeaconUrl = "http://www.examples.com/beacon";
      redirectAd.certifiedVendorFormatId = 119;
      redirectAd.isCookieTargeted = false;
      redirectAd.isUserInterestTargeted = false;
      redirectAd.isTagged = false;

      adGroupAdOperation.operand.ad = redirectAd;

      AdGroupAdReturnValue retVal =
          adGroupAdService.mutate(new AdGroupAdOperation[] {adGroupAdOperation});
      return retVal.value[0].ad.id;
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
          (AdParamService) user.GetService(AdWordsService.v201109.AdParamService);

      // Prepare for setting ad parameters.
      AdParam adParam = new AdParam();
      adParam.adGroupId = adGroupId;
      adParam.criterionId = criterionId;
      adParam.paramIndex = 1;
      adParam.insertionText = "$100";

      AdParamOperation adParamOperation = new AdParamOperation();
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
    /// <param name="adGroupId">The adgroup id for which the keyword is
    /// created.</param>
    /// <returns>The keyword id.</returns>
    public long CreateKeyword(AdWordsUser user, long adGroupId) {
      AdGroupCriterionService adGroupCriterionService =
         (AdGroupCriterionService) user.GetService(AdWordsService.v201109.AdGroupCriterionService);

      AdGroupCriterionOperation operation = new AdGroupCriterionOperation();
      operation.@operator = Operator.ADD;
      operation.operand = new BiddableAdGroupCriterion();
      operation.operand.adGroupId = adGroupId;

      Keyword keyword = new Keyword();
      keyword.matchType = KeywordMatchType.BROAD;
      keyword.text = "mars cruise";

      operation.operand.criterion = keyword;
      AdGroupCriterionReturnValue retVal =
          adGroupCriterionService.mutate(new AdGroupCriterionOperation[] {operation});
      return retVal.value[0].criterion.id;
    }

    /// <summary>
    /// Creates the placement.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="adGroupId">The adgroup id for which the placement is
    /// created.</param>
    /// <returns>The placement id.</returns>
    public long CreatePlacement(AdWordsUser user, long adGroupId) {
      AdGroupCriterionService adGroupCriterionService =
          (AdGroupCriterionService) user.GetService(AdWordsService.v201109.AdGroupCriterionService);

      Placement placement = new Placement();
      placement.url = "http://mars.google.com";

      AdGroupCriterion placementCriterion = new BiddableAdGroupCriterion();
      placementCriterion.adGroupId = adGroupId;
      placementCriterion.criterion = placement;

      AdGroupCriterionOperation placementOperation = new AdGroupCriterionOperation();
      placementOperation.@operator = Operator.ADD;
      placementOperation.operand = placementCriterion;

      AdGroupCriterionReturnValue retVal = adGroupCriterionService.mutate(
          new AdGroupCriterionOperation[] {placementOperation});

      return retVal.value[0].criterion.id;
    }

    /// <summary>
    /// Creates a campaign ad extension for running further tests.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="campaignId">The campaign id for which extension is
    /// created.</param>
    /// <returns>The campaign ad extension id.</returns>
    public long CreateLocationExtension(AdWordsUser user, long campaignId) {
      CampaignAdExtensionService campaignExtensionService =
          (CampaignAdExtensionService) user.GetService(AdWordsService.v201109.
               CampaignAdExtensionService);

      CampaignAdExtensionOperation operation = new CampaignAdExtensionOperation();
      operation.@operator = Operator.ADD;

      CampaignAdExtension extension = new CampaignAdExtension();
      extension.campaignId = campaignId;
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
      locationExtension.source = LocationExtensionSource.ADWORDS_FRONTEND;

      extension.adExtension = locationExtension;
      operation.operand = extension;
      CampaignAdExtensionReturnValue retVal =
          campaignExtensionService.mutate(new CampaignAdExtensionOperation[] {operation});
      return retVal.value[0].adExtension.id;
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
    public long CreateLocationExtensionOverride(AdWordsUser user, long campaignAdExtensionId,
        long adId, GeoLocation location) {
      AdExtensionOverrideService adExtensionOverrideService =
          (AdExtensionOverrideService)user.GetService(
              AdWordsService.v201109.AdExtensionOverrideService);

      AdExtensionOverrideOperation operation = new AdExtensionOverrideOperation();
      operation.@operator = Operator.ADD;

      operation.operand = new AdExtensionOverride();
      operation.operand.adId = adId;

      LocationExtension locationExtension = new LocationExtension();
      locationExtension.id = campaignAdExtensionId;

      // Note: Do not populate an address directly. Instead, use
      // GeoLocationService to obtain the location of an address,
      // and use the address as per the location it returns.
      locationExtension.address = location.address;
      locationExtension.geoPoint = location.geoPoint;
      locationExtension.encodedLocation = location.encodedLocation;
      locationExtension.source = LocationExtensionSource.ADWORDS_FRONTEND;

      // Optional: Apply this override within 20 kms.
      operation.operand.overrideInfo = new OverrideInfo();
      operation.operand.overrideInfo.Item = new LocationOverrideInfo();
      operation.operand.overrideInfo.Item.radius = 20;
      operation.operand.overrideInfo.Item.radiusUnits = LocationOverrideInfoRadiusUnits.KILOMETERS;

      operation.operand.adExtension = locationExtension;

      AdExtensionOverrideReturnValue retVal = adExtensionOverrideService.mutate(
          new AdExtensionOverrideOperation[] {operation});
      return retVal.value[0].adExtension.id;
    }

    /// <summary>
    /// Adds site links to a campaign.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="campaignId">The campaign id.</param>
    /// <returns>The sitelinks campaign adextension id.</returns>
    public long AddSiteLinks(AdWordsUser user, long campaignId) {
      // Get the CampaignAdExtensionService.
      CampaignAdExtensionService campaignExtensionService =
          (CampaignAdExtensionService) user.GetService(AdWordsService.v201109.
          CampaignAdExtensionService);

      // Create the sitelinks.
      SitelinksExtension siteLinkExtension = new SitelinksExtension();

      Sitelink siteLink1 = new Sitelink();
      siteLink1.displayText = "Music";
      siteLink1.destinationUrl = "http://www.example.com/music";

      Sitelink siteLink2 = new Sitelink();
      siteLink2.displayText = "DVDs";
      siteLink2.destinationUrl = "http://www.example.com/dvds";

      Sitelink siteLink3 = new Sitelink();
      siteLink3.displayText = "New albums";
      siteLink3.destinationUrl = "http://www.example.com/albums/new";

      siteLinkExtension.sitelinks = new Sitelink[] {siteLink1, siteLink2, siteLink3};

      CampaignAdExtension campaignAdExtension = new CampaignAdExtension();
      campaignAdExtension.adExtension = siteLinkExtension;
      campaignAdExtension.campaignId = campaignId;

      // Create the operation.
      CampaignAdExtensionOperation operation = new CampaignAdExtensionOperation();
      operation.@operator = Operator.ADD;
      operation.operand = campaignAdExtension;

      // Create the sitelinks.
      CampaignAdExtensionReturnValue retVal =
          campaignExtensionService.mutate(new CampaignAdExtensionOperation[] {operation});

      return retVal.value[0].adExtension.id;
    }

    /// <summary>
    /// Adds an experiment.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="campaignId">The campaign id.</param>
    /// <param name="adGroupId">The ad group id.</param>
    /// <param name="criterionId">The criterion id.</param>
    /// <returns>The experiment id.</returns>
    public long AddExperiment(AdWordsUser user, long campaignId, long adGroupId, long criterionId) {
      // Get the ExperimentService.
      ExperimentService experimentService =
          (ExperimentService) user.GetService(AdWordsService.v201109.ExperimentService);

      // Get the AdGroupService.
      AdGroupService adGroupService =
          (AdGroupService) user.GetService(AdWordsService.v201109.AdGroupService);

      // Get the AdGroupCriterionService.
      AdGroupCriterionService adGroupCriterionService =
          (AdGroupCriterionService) user.GetService(AdWordsService.v201109.AdGroupCriterionService);

      // Create the experiment.
      Experiment experiment = new Experiment();
      experiment.campaignId = campaignId;
      experiment.name = "Interplanetary Cruise #" + GetTimeStamp();
      experiment.queryPercentage = 10;
      experiment.startDateTime = DateTime.Now.ToString("yyyyMMdd HHmmss");

      // Create the operation.
      ExperimentOperation experimentOperation = new ExperimentOperation();
      experimentOperation.@operator = Operator.ADD;
      experimentOperation.operand = experiment;

      // Add the experiment.
      ExperimentReturnValue experimentRetVal = experimentService.mutate(
          new ExperimentOperation[] {experimentOperation});

      return experimentRetVal.value[0].id;
    }

    /// <summary>
    /// Adds the campaign targeting criteria to a campaign.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="campaignId">The campaign id.</param>
    /// <returns>The campaign criteria id.</returns>
    public long AddCampaignTargetingCriteria(AdWordsUser user, long campaignId) {
      // Get the CampaignCriterionService.
      CampaignCriterionService campaignCriterionService =
          (CampaignCriterionService) user.GetService(
              AdWordsService.v201109.CampaignCriterionService);

      // Create language criteria.
      // See http://code.google.com/apis/adwords/docs/appendix/languagecodes.html
      // for a detailed list of language codes.
      Language language1 = new Language();
      language1.id = 1002; // French
      CampaignCriterion languageCriterion1 = new CampaignCriterion();
      languageCriterion1.campaignId = campaignId;
      languageCriterion1.criterion = language1;

      CampaignCriterion[] criteria = new CampaignCriterion[] {languageCriterion1};

      List<CampaignCriterionOperation> operations = new List<CampaignCriterionOperation>();

      foreach (CampaignCriterion criterion in criteria) {
        CampaignCriterionOperation operation = new CampaignCriterionOperation();
        operation.@operator = Operator.ADD;
        operation.operand = criterion;
        operations.Add(operation);
      }

      CampaignCriterionReturnValue retVal = campaignCriterionService.mutate(operations.ToArray());
      return retVal.value[0].criterion.id;
    }

    /// <summary>
    /// Returns an image which can be used for creating image ads.
    /// </summary>
    /// <returns>The image data, as an array of bytes.</returns>
    public byte[] GetSandboxImage() {
      return MediaUtilities.GetAssetDataFromUrl("http://goo.gl/HJM3L");
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
          (GeoLocationService) user.GetService(AdWordsService.v201109.GeoLocationService);

      GeoLocationSelector selector = new GeoLocationSelector();
      selector.addresses = new Address[] {address};
      return geoService.get(selector)[0];
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
