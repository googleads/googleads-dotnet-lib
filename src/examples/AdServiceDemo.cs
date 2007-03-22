/*
* Copyright (C) 2006 Google Inc.
* 
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
* 
*      http://www.apache.org/licenses/LICENSE-2.0
* 
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/
using System;
using System.Text;
using com.google.api.adwords.v9;
using com.google.api.adwords.lib;

namespace com.google.api.adwords.examples
{
	/**
	 * Tests the new ad update call. 
	 */
	class AdServiceDemo
	{
		public static void run()
		{
			// Create a user (reads headers from app.config file)
			AdWordsUser user = new AdWordsUser();
			// Use sandbox
			user.useSandbox();
			// Get the services
			CampaignService campaignService = (CampaignService)user.getService("CampaignService");
			AdGroupService adgroupService = (AdGroupService)user.getService("AdGroupService");
			AdService adService = (AdService)user.getService("AdService");

			// Create a new campaign with some ad groups.
			// First create the campaign so we can get its id.
			Campaign newCampaign = new Campaign();
			newCampaign.dailyBudget = 1000000;
			newCampaign.dailyBudgetSpecified = true;

			// The campaign name is optional.
			// An error results if a campaign of the same name already exists.
			//newCampaign.name = "AdWords API Campaign";

			// Target the campaign at France and Spain.
			// Only one kind of geotargeting can be specified.
			GeoTarget g_target = new GeoTarget();
			String[] countries = {"FR", "ES"};
			g_target.countries = countries;
			newCampaign.geoTargeting = g_target;

			// Target the campaign at English, French and Spanish.
			String[] languages =  {"en", "fr", "es"};
			newCampaign.languageTargeting = languages;
			// Set the campaign status to paused, we don't want to start paying for this test
			newCampaign.status = CampaignStatus.Paused;

			// Add the new campaign.
			// The campaign object is returned with ids filled in.
			newCampaign = campaignService.addCampaign(newCampaign);
			int campaign_id = newCampaign.id;

			// Create an ad group
			AdGroup myAdGroup = new AdGroup();
			myAdGroup.name = "dev guide";
			myAdGroup.maxCpc = 50000;
			myAdGroup.maxCpcSpecified = true;

			// Associate the ad group with the new campaign.
			// Send the request to add the new AdGroup.
			AdGroup newAdGroup = adgroupService.addAdGroup(campaign_id, myAdGroup);

			int adgroup_id=newAdGroup.id;

			// Create a text ad.
			// IMPORTANT: create the ad before adding keywords! Else the minCpc will 
			// have a higher value
			TextAd ad1 = new TextAd();
			ad1.headline = "AdWords API Dev Guide";
			ad1.description1 = "Access your AdWords";
			ad1.description2 = "accounts programmatically";
			ad1.displayUrl = "blog.chanezon.com";
			ad1.destinationUrl = "http://blog.chanezon.com/";
			ad1.adGroupId = adgroup_id;
			Ad[] ads = adService.addAds(new Ad[] {ad1});
			Console.WriteLine("Before update: " + ad1.headline + " status = " + ad1.status);
			// Update the creative status (only field updatable for now)
			ads[0].status = AdStatus.Disabled;
			ads[0].statusSpecified = true;
			
			adService.updateAds(ads);

			// Check creative status
			Ad[] updatedAds = adService.getAllAds(new int[] {adgroup_id});
			for (int i = 0; i < updatedAds.Length; i++) 
			{
				TextAd ad = (TextAd) updatedAds[i];
				Console.WriteLine("After update: " + ad.headline + " status = " + ad.status);
			}
    
			// Determining how much quota all these operations have consumed
			Console.WriteLine("---------------------------------------");
			Console.WriteLine("Total Quota unit cost for this run: {0}", user.getUnits());

			Console.ReadLine();
		}
	}
}