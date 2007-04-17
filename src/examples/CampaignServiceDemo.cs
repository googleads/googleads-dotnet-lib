//
// Copyright (C) 2006 Google Inc.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//      http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

using System;
using System.Text;

using com.google.api.adwords.lib;
using com.google.api.adwords.v9;

namespace com.google.api.adwords.examples
{
	// Creates new campaign, ad group, text ad, and some keywords.
	class CampaignServiceDemo
	{
		public static void run()
		{
			// Create a user (reads headers from App.config file).
			AdWordsUser user = new AdWordsUser();
			user.useSandbox();	// use sandbox

			// Get the services.
			CampaignService campaignService = 
				(CampaignService) user.getService("CampaignService");
			AdGroupService adgroupService = 
				(AdGroupService) user.getService("AdGroupService");
			CriterionService criterionService = 
				(CriterionService) user.getService("CriterionService");
			AdService adService = (AdService) user.getService("AdService");

			// Create a new campaign with an ad group.  First create a 
			// campaign, so we can get its id.
			Campaign newCampaign = new Campaign();
			newCampaign.dailyBudget = 1000000;
			newCampaign.dailyBudgetSpecified = true;

			// The campaign name is optional.  An error results if a campaign 
			// of the same name already exists.
			//newCampaign.name = "AdWords API Campaign";

			// Target the campaign at France and Spain.  Only one kind of 
			// geotargeting can be specified.
			GeoTarget newGeoTarget = new GeoTarget();
			String[] countries = {"FR", "ES"};
			newGeoTarget.countries = countries;
			newCampaign.geoTargeting = newGeoTarget;

			// Target the campaign at English, French and Spanish.
			String[] languages =  {"en", "fr", "es"};
			newCampaign.languageTargeting = languages;

			// Set the campaign status to paused, we don't want to start 
			// paying for this test.
			newCampaign.status = CampaignStatus.Paused;

			// Add this campaign.  The campaign object is returned with ids 
			// filled in.
			newCampaign = campaignService.addCampaign(newCampaign);
			int campaignId = newCampaign.id;

			// Create an ad group.
			AdGroup newAdGroup = new AdGroup();
			newAdGroup.name = "dev guide";
			newAdGroup.maxCpc = 50000;
			newAdGroup.maxCpcSpecified = true;

			// Associate this ad group with the newly created campaign.  Send 
			// the request to add the new ad group.
			AdGroup myAdGroup = 
				adgroupService.addAdGroup(campaignId, newAdGroup);
			int adGroupId = myAdGroup.id;

			// Create a text ad.
			//
			// IMPORTANT: create an ad before adding keywords!  Else the 
			// minCpc will have a higher value.
			TextAd newTextAd = new TextAd();
			newTextAd.headline = "AdWords API Dev Guide";
			newTextAd.description1 = "Access your AdWords";
			newTextAd.description2 = "accounts programmatically";
			newTextAd.displayUrl = "blog.chanezon.com";
			newTextAd.destinationUrl = "http://blog.chanezon.com/";
			newTextAd.adGroupId = adGroupId;
			Ad[] myAds = adService.addAds(new Ad[] {newTextAd});
    
			// Add keywords to the newly created ad group.
			Keyword newKeyword1 = new Keyword();
			newKeyword1.adGroupId = adGroupId;
			newKeyword1.text = "AdWords API";
			newKeyword1.type = KeywordType.Broad;
			Keyword newKeyword2 = new Keyword();
			newKeyword2.adGroupId = adGroupId;
			newKeyword2.text = "Adwords developer guide";
			newKeyword2.type = KeywordType.Broad;
			Keyword newKeyword3 = new Keyword();
			newKeyword3.adGroupId = adGroupId;
			newKeyword3.text  = "AdWords reference";
			newKeyword3.type= KeywordType.Broad;

			Criterion[] myKeywords = criterionService.addCriteria(
				new Criterion[] {newKeyword1, newKeyword2, newKeyword3});

			// Update all criteria maxCpc.
			((Keyword) myKeywords[0]).maxCpc = 100000;			
			((Keyword) myKeywords[0]).maxCpcSpecified = true;
			((Keyword) myKeywords[1]).maxCpc = 100000;
			((Keyword) myKeywords[1]).maxCpcSpecified = true;
			((Keyword) myKeywords[2]).maxCpc = 100000;
			((Keyword) myKeywords[2]).maxCpcSpecified = true;
			criterionService.updateCriteria(myKeywords);
    
			// Check criteria maxCpc.
			myKeywords = criterionService.getAllCriteria(adGroupId);

			for (int i = 0; i < myKeywords.Length; i ++) 
			{
				Keyword myKeyword = (Keyword) myKeywords[i];
				Console.WriteLine(
					myKeyword.text + ": maxCpc = " + myKeyword.maxCpc);
			}
    
			// Determine how much quota these operations have consumed.
			Console.WriteLine("---------------------------------------"
							+ "\nTotal Quota unit cost for this run: {0}", 
							user.getUnits());

			Console.ReadLine();
		}
	}
}