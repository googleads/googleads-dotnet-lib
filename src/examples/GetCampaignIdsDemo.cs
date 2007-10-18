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
using com.google.api.adwords.v10;

namespace com.google.api.adwords.examples
{
	// Gets all campaigns.
	class GetCampaignIdsDemo
	{
		public static void run()
		{
			// Create a user (reads headers from App.config file).
			AdWordsUser user = new AdWordsUser();
			user.useSandbox();	// use sandbox

			// Get the service.
			CampaignService campaignService = 
				(CampaignService) user.getService("CampaignService");

			// Get all campaigns.
			Campaign[] myCampaigns = campaignService.getAllAdWordsCampaigns(1);

			for (int i = 0; i < myCampaigns.Length; i ++) 
			{
				Console.WriteLine("Name: " + myCampaigns[i].name
								+ "    id: " + myCampaigns[i].id
								+ "  status: " + myCampaigns[i].status);
			}

			Console.ReadLine();
		}
	}
}