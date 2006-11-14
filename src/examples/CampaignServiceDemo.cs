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
using com.google.api.adwords.v7;
using com.google.api.adwords.lib;


namespace com.google.api.adwords.examples
{
	/**
	 * Displays some of the fields in the Account's Info. 
	 */
	class CampaignServiceDemo
	{
		public static void run()
		{
			//create a user (reads headers from app.config file)
			AdWordsUser user = new AdWordsUser();
			// get the services
			CampaignServiceService campaignService = (CampaignServiceService)user.getService("CampaignServiceService");
			AdGroupServiceService adgroupService = (AdGroupServiceService)user.getService("AdGroupServiceService");
			CriterionServiceService criterionService = (CriterionServiceService)user.getService("CriterionServiceService");
			CreativeServiceService creativeService = (CreativeServiceService)user.getService("CreativeServiceService");

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
			//set the campaign status to paused
			//we don't want to start paying for this test
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

			// Create a creative.
			// IMPORTANT: create the creative before adding keywords!
			//else the minCpc will have a higher value
			Creative creative1 = new Creative();
			creative1.headline = "AdWords API Dev Guide";
			creative1.description1 = "Access your AdWords";
			creative1.description2 = "accounts programmatically";
			creative1.displayUrl = "blog.chanezon.com";
			creative1.destinationUrl = "http://blog.chanezon.com/";
			creative1.adGroupId = adgroup_id;
			creative1 = creativeService.addCreative(creative1);
    
			// Add keywords to the AdGroup.
			Keyword keyword1 = new Keyword();
			keyword1.adGroupId = adgroup_id;
			keyword1.text = "AdWords API";
			keyword1.type = KeywordType.Broad;
			Keyword keyword2 = new Keyword();
			keyword2.adGroupId = adgroup_id;
			keyword2.text = "Adwords developer guide";
			keyword2.type = KeywordType.Broad;
			Keyword keyword3 = new Keyword();
			keyword3.adGroupId = adgroup_id;
			keyword3.text  = "AdWords reference";
			keyword3.type= KeywordType.Broad;

			Criterion[] newKeywords= {keyword1, keyword2,keyword3};

			newKeywords = criterionService.addCriteria(newKeywords);

			//update all criteria maxCpc
			((Keyword)newKeywords[0]).maxCpc = 100000;			
			((Keyword)newKeywords[0]).maxCpcSpecified = true;
			((Keyword)newKeywords[1]).maxCpc = 100000;
			((Keyword)newKeywords[1]).maxCpcSpecified = true;
			((Keyword)newKeywords[2]).maxCpc = 100000;
			((Keyword)newKeywords[2]).maxCpcSpecified = true;
			criterionService.updateCriteria(newKeywords);
    
			//check criteria maxCpc
			Criterion[] updatedKw = criterionService.getAllCriteria(adgroup_id);
			Keyword kw;
			for (int i = 0; i < updatedKw.Length; i++) 
			{
				kw = (Keyword)updatedKw[i];
				Console.WriteLine(kw.text + " maxCpc = " + kw.maxCpc);
			}
    
			//determining how much quota all these operations have consumed
			Console.WriteLine("---------------------------------------");
			Console.WriteLine("Total Quota unit cost for this run: {0}", user.getUnits());

			Console.ReadLine();
		}
	}
}