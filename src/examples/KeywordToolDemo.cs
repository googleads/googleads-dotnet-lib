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
using com.google.api.adwords.v6;
using com.google.api.adwords.lib;


namespace com.google.api.adwords.examples
{
	/**
	 * Displays some of the fields in the Account's Info. 
	 */
	class KeywordToolDemo
	{
		public static void run()
		{
			//create a user (reads headers from app.config file)
			AdWordsUser user = new AdWordsUser();
			// get the services
			KeywordToolService s = (KeywordToolService)user.getService("KeywordToolService");
			String site = "http://blog.chanezon.com";

			//get a list of keywords for that url
			SiteKeywordGroups kwg = s.getKeywordsFromSite(site, true, null, null);
			SiteKeyword[] kw = kwg.keywords;
			String[] groups = kwg.groups;
			//print the list
			Console.WriteLine("List of keyword suggestions for url {0}", site);
			Console.WriteLine("Group	groupid	compte  volume text");
			for (int i=0; i < kw.Length; i++) 
			{
				Console.WriteLine(siteKeywordDump(kw[i], groups));
			}
			Console.ReadLine();
		}
 
		public static String SEP = " ";
		  
		public static String siteKeywordDump(SiteKeyword sk, String[] groups) {
			return "" + groups[sk.groupId]
				+ SEP
				+ sk.advertiserCompetitionScale
				+ SEP
				+ sk.searchVolumeScale
				+ SEP
	  			+ sk.text;
		}
	}
}