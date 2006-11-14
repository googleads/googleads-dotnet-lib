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
	class GetCampaignIdsDemo
	{
		public static void run()
		{
			//create a user (reads headers from app.config file)
			AdWordsUser user = new AdWordsUser();
			// get the services
			CampaignServiceService campaignService = (CampaignServiceService)user.getService("CampaignServiceService");

			// Print out all campaign ids
			Campaign[] myCampaigns = campaignService.getAllAdWordsCampaigns(1);

			// Print name and id for each campaign
			for (int i = 0; i < myCampaigns.Length; i++) {
			Console.WriteLine("Name: " + myCampaigns[i].name +
								"    id: " + myCampaigns[i].id +
								"  status: " + myCampaigns[i].status);
			}

			Console.ReadLine();
		}
	}
}