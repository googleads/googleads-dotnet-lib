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
	 * Tests new getAllAds() method.
	 */
	class GetAdIdsDemo
	{
		public static void run()
		{
			// Create a user (reads headers from app.config file)
			AdWordsUser user = new AdWordsUser();
			// Use sandbox
			user.useSandbox();
			// Get an Ad Service client
			AdService service = (AdService)user.getService("AdService");

			// Get all ads
			int[] adGroupIds = {12345};
			Ad[] ads = service.getAllAds(adGroupIds);
			for (int i = 0; i < ads.Length; i ++) 
			{
				Console.WriteLine("----- Ad Info -----");
				Console.WriteLine("Ad Group ID: " + ads[i].adGroupId);
				Console.WriteLine("ID: " + ads[i].id);
				Console.WriteLine("Type: " + ads[i].adType);
				Console.WriteLine("Status: " + ads[i].status);
				Console.WriteLine("------------------------");
			}
			Console.ReadLine();
		}
	}
}