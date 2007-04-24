//
// Copyright (C) 2007 Google Inc.
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
	// Gets web site suggestions by topics.
	class SiteSuggestionServiceDemo
	{
		public static void run()
		{
			// Create a user (reads headers from App.config file).
			AdWordsUser user = new AdWordsUser();
			user.useSandbox();	// use sandbox

			// Get the service.
			SiteSuggestionService service = 
				(SiteSuggestionService) user.getService(
					"SiteSuggestionService");

			String[] topics = {"soap", "xml", "virtual machine"};

			LanguageGeoTargeting targeting = new LanguageGeoTargeting();
			targeting.languages = new String[] {"en_US", "es"};
			targeting.countries = new String[] {"US", "ES"};

			// Get site suggestions.
			SiteSuggestion[] sites = 
				service.getSitesByTopics(topics, targeting);

			if (sites != null)
			{
				Console.WriteLine(
					"{0, -16}{1, -15}{2, -15}{3, -12}{4, -14}",
					"acceptsImageAds",
					"acceptsTextAds",
					"acceptsVidoAds",
					"pageViews",
					"url");
				for (int i = 0; i < sites.Length; i ++)
				{
					SiteSuggestion site = sites[i];
					Console.WriteLine(
						"{0, -16}{1, -15}{2, -15}{3, -12}{4, -14}",
						site.acceptsImageAds,
						site.acceptsTextAds,
						site.acceptsVideoAds,
						site.pageViews,
						site.url);
				}
			}

			Console.ReadLine();
		}
	}
}