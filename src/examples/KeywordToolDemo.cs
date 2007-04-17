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
	// Gets keyword variations for specific seed keywords.
	class KeywordToolDemo
	{
		public static void run()
		{
			// Create a user (reads headers from App.config file).
			AdWordsUser user = new AdWordsUser();
			user.useSandbox();	// use sandbox

			// Get the service.
			KeywordToolService service = 
				(KeywordToolService) user.getService("KeywordToolService");
			
			String site = "http://blog.chanezon.com";

			// Get a list of keywords for this site.
			SiteKeywordGroups mySiteKeywordGroups = 
				service.getKeywordsFromSite(site, true, null, null);
			SiteKeyword[] keywords = mySiteKeywordGroups.keywords;
			String[] groups = mySiteKeywordGroups.groups;

			Console.WriteLine(
				"List of keyword suggestions for url: \"{0}\"\n", site);
			Console.WriteLine("{0, -10}{1, -10}{2, -10}{3, -10}{4, -10}",
				"Group",
				"Group Id",
				"Competion",
				"Volume",
				"Text");
			for (int i = 0; i < keywords.Length; i ++)
			{
				SiteKeyword siteKeyword = keywords[i];
				Console.WriteLine("{0, -10}{1, -10}{2, -10}{3, -10}{4, -10}",
					groups[siteKeyword.groupId],
					siteKeyword.groupId,
					siteKeyword.advertiserCompetitionScale,
					siteKeyword.searchVolumeScale,
					siteKeyword.text);
			}
			
			// Get keyword variations.
			SeedKeyword seed = new SeedKeyword();
			seed.text = "flower";
			SeedKeyword[] seeds = {seed};
			bool useSynonyms = true;
			String[] languages = {"en"};
			String[] countries = {"US"};
			KeywordVariations myKeywordVariations = 
				service.getKeywordVariations(
					seeds, useSynonyms, languages, countries);
			KeywordVariation[] myKeywordVariationsDetails = 
				myKeywordVariations.moreSpecific;

			if (null != myKeywordVariationsDetails)
			{
				Console.WriteLine("\n-------------------------------\n"
								+ "\nList of keyword variations for keyword "
								+ "seed \"{0}\"\n", seed.text);
				Console.WriteLine("{0, -30}{1, -10}{2, -20}{3, -10}",
					"advertiserCompetitionScale",
					"language",
					"searchVolumeScale",
					"text");
				for (int i = 0; i < myKeywordVariationsDetails.Length; i ++)
				{
					KeywordVariation keywordDetail = 
						myKeywordVariationsDetails[i];
					Console.WriteLine("{0, -30}{1, -10}{2, -20}{3, -10}",
						keywordDetail.advertiserCompetitionScale,
						keywordDetail.language,
						keywordDetail.searchVolumeScale,
						keywordDetail.text);
				}
			}

			Console.ReadLine();
		}
 	}
}