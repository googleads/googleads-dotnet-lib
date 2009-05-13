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

using com.google.api.adwords.lib;
using com.google.api.adwords.v13;

using System;
using System.Text;

namespace com.google.api.adwords.samples.v13 {
  /// <summary>
  /// Creates new campaign, ad group, and a text ad.
  /// </summary>
  class AdServiceDemo : SampleBase{
    /// <summary>
    /// Returns a description about the sample code.
    /// </summary>
    public override string Description {
      get {
        return "Creates new campaign, ad group, and a text ad.";
      }
    }

    /// <summary>
    /// Run the sample code.
    /// </summary>
    /// <param name="user">The AdWords user object running the sample.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Reset the units counter before starting a session.
      user.ResetUnits();

      // Get the services.
      CampaignService campaignService =
          (CampaignService) user.GetService(ApiServices.v13.CampaignService);
      AdGroupService adGroupService =
          (AdGroupService) user.GetService(ApiServices.v13.AdGroupService);
      AdService adService = (AdService) user.GetService(ApiServices.v13.AdService);

      // Create a new campaign and an ad group.  First create a
      // campaign, so we can get its id.
      Campaign newCampaign = new Campaign();
      newCampaign.budgetAmount = 1000000L;
      newCampaign.budgetAmountSpecified = true;
      newCampaign.budgetPeriod = BudgetPeriod.Daily;
      newCampaign.budgetPeriodSpecified = true;

      // The campaign name is optional.  An error results if a campaign
      // of the same name already exists.
      // newCampaign.name = "AdWords API Campaign";

      // Target the campaign at France and Spain.  Only one kind of
      // geotargeting can be specified.
      GeoTarget newGeoTarget = new GeoTarget();
      String[] countries = {"FR", "ES"};
      newGeoTarget.countryTargets = new CountryTargets();
      newGeoTarget.countryTargets.countries = countries;
      newCampaign.geoTargeting = newGeoTarget;

      // Target the campaign at English, French and Spanish
      String[] languages =  {"en", "fr", "es"};
      newCampaign.languageTargeting = languages;

      // Set the campaign status to paused, we don't want to start
      // paying for this test.
      newCampaign.status = CampaignStatus.Paused;
      // Add this campaign.  The campaign object is returned with ids
      // filled in.
      newCampaign = campaignService.addCampaign(newCampaign);
      int campaignId = newCampaign.id;
      Console.WriteLine("New campaign created - {0}", campaignId);

      // Create an ad group.
      AdGroup newAdGroup = new AdGroup();
      newAdGroup.name = "dev guide";
      newAdGroup.keywordMaxCpc = 50000;
      newAdGroup.keywordMaxCpcSpecified = true;

      // Associate this ad group with the newly created campaign.  Send
      // the request to add the new ad group.
      AdGroup myAdGroup = adGroupService.addAdGroup(campaignId, newAdGroup);
      long adGroupId = myAdGroup.id;
      Console.WriteLine("New adgroup added to Campaign {0} - {1}", campaignId, adGroupId);

      // Create a text ad.
      //
      // IMPORTANT: create an ad before adding keywords! Else the
      // minCpc will have a higher value.
      TextAd newTextAd = new TextAd();
      newTextAd.headline = "AdWords API Dev Guide";
      newTextAd.description1 = "Access your AdWords";
      newTextAd.description2 = "accounts programmatically";
      newTextAd.displayUrl = "blog.chanezon.com";
      newTextAd.destinationUrl = "http://blog.chanezon.com/";
      newTextAd.adGroupId = adGroupId;
      Ad[] myAds = adService.addAds(new Ad[] {newTextAd});
      Console.WriteLine("Before update: {0} status = {1}", newTextAd.headline, newTextAd.status);

      // Update the creative status, the only field updatable for now.
      myAds[0].status = AdStatus.Disabled;
      myAds[0].statusSpecified = true;

      adService.updateAds(myAds);

      // Check creative status.
      myAds = adService.getAllAds(new long[] {adGroupId});

      for (int i = 0; i < myAds.Length; i++) {
        TextAd myTextAd = (TextAd) myAds[i];
        Console.WriteLine("After update: {0} status = {1}", myTextAd.headline, myTextAd.status);
      }

      // Determine how much quota these operations have consumed.
      Console.WriteLine("---------------------------------------" +
          "\nTotal Quota unit cost for this run: {0}", user.GetUnits());
    }
  }
}
