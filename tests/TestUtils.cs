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
using com.google.api.adwords.v200909;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace com.google.api.adwords.tests {
  /// <summary>
  /// A utility class to assist the testing of v200909 services.
  /// </summary>
  class TestUtils {
    /// <summary>
    /// Creates a test campaign for running further tests.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <returns>The campaign id.</returns>
    public long CreateCampaign(AdWordsUser user, bool cpc) {
      CampaignService campaignService =
          (CampaignService) user.GetService(AdWordsService.v200909.CampaignService);
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
    /// Creates a test adgroup for running further tests.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="campaignId">The campaign id for which the adgroup is created.</param>
    /// <returns>The adgroup id.</returns>
    public long CreateAdGroup(AdWordsUser user, long campaignId) {
      AdGroupService adGroupService =
          (AdGroupService) user.GetService(AdWordsService.v200909.AdGroupService);
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
    /// <param name="adGroupId">The AdGroup id for which the ad is created.</param>
    /// <returns>The text ad id.</returns>
    public long CreateTextAd(AdWordsUser user, long adGroupId) {
      long adId = 0;
      AdGroupAdService adGroupAdService =
          (AdGroupAdService) user.GetService(AdWordsService.v200909.AdGroupAdService);
      AdGroupAdOperation adGroupAdOperation = new AdGroupAdOperation();
      adGroupAdOperation.operatorSpecified = true;
      adGroupAdOperation.@operator = Operator.ADD;
      adGroupAdOperation.operand = new AdGroupAd();
      adGroupAdOperation.operand.adGroupIdSpecified = true;
      adGroupAdOperation.operand.adGroupId = adGroupId;
      TextAd ad = new TextAd();
      ad.url = "http://www.example.com";
      ad.displayUrl = "example.com";
      ad.description1 = "Visit the Red Planet in style.";
      ad.description2 = "Low-gravity fun for everyone!";
      ad.headline = "Luxury Cruise to Mars";
      adGroupAdOperation.operand.ad = ad;

      AdGroupAdReturnValue retVal =
          adGroupAdService.mutate(new AdGroupAdOperation[] {adGroupAdOperation});
      if (retVal != null && retVal.value != null && retVal.value.Length > 0) {
        adId = retVal.value[0].ad.id;
      }
      return adId;
    }

    /// <summary>
    /// Creates a keyword for running further tests.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="adGroupId">The AdGroup id for which the keyword is created.</param>
    /// <returns>The keyword id.</returns>
    public long CreateTestKeyword(AdWordsUser user, long adGroupId) {
      long keywordId = 0;
      AdGroupCriterionService adGroupCriterionService =
         (AdGroupCriterionService) user.GetService(AdWordsService.v200909.AdGroupCriterionService);

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

    public long CreateCampaignNegativeKeyword(AdWordsUser user, long campaignId) {
      long keywordId = 0;
      CampaignCriterionService service = (CampaignCriterionService) user.GetService(
          AdWordsService.v200909.CampaignCriterionService);

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

    public byte[] GetSandboxImage() {
      string imageUrl = "https://sandbox.google.com/sandboximages/image.jpg";

      WebRequest request = HttpWebRequest.Create(imageUrl);
      WebResponse response = request.GetResponse();

      Stream responseStream = response.GetResponseStream();

      MemoryStream memStream = new MemoryStream();
      byte[] strmBuffer = new byte[4096];

      int bytesRead = responseStream.Read(strmBuffer, 0, 4096);
      while (bytesRead != 0) {
        memStream.Write(strmBuffer, 0, bytesRead);
        bytesRead = responseStream.Read(strmBuffer, 0, 4096);
      }
      responseStream.Close();

      return memStream.ToArray();

    }

    public long CreateCampaignAdExtension(AdWordsUser user, long campaignId) {
      long campaignAdExtensionId = 0;

      CampaignAdExtensionService campaignExtensionService =
          (CampaignAdExtensionService) user.GetService(AdWordsService.v200909.
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

    public GeoLocation GetLocationForAddress(AdWordsUser user, Address address) {
      GeoLocationService geoService =
          (GeoLocationService) user.GetService(AdWordsService.v200909.GeoLocationService);

      GeoLocationSelector selector = new GeoLocationSelector();
      selector.addresses = new Address[] {address};
      return geoService.get(selector)[0];
    }
  }
}
