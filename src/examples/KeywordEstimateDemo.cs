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
	 * Estimates given keywords.
	 */
	class KeywordEstimateDemo
	{
		public static void run()
		{
			// Create a user (reads headers from app.config file)
			AdWordsUser user = new AdWordsUser();
			// Use sandbox
			user.useSandbox();
			// Get the services
			TrafficEstimatorService tes = (TrafficEstimatorService)user.getService("TrafficEstimatorService");

			// Set the attributes of the keywords to be estimated
			KeywordRequest myKeyword = new KeywordRequest();
			myKeyword.text = "flowers";
			myKeyword.maxCpc = 50000;
			myKeyword.maxCpcSpecified = true;

			myKeyword.type = KeywordType.Broad;
			myKeyword.typeSpecified = true;

			// Make an array of the keywordrequests
			KeywordRequest[] myKeywordList = {myKeyword};

			// To estimate more keywords, create more KeywordRequest objects
			// and add them to the myKeywordList array.

			// Send the request to the TrafficEstimator service
			KeywordEstimate[] estimates = tes.estimateKeywordList(myKeywordList);

			// Print information from the results.
			KeywordEstimate est = estimates[0];

			Console.WriteLine("Clicks per day between " + est.lowerClicksPerDay + " and " + est.upperClicksPerDay);
			Console.WriteLine("Cost per click between " + est.lowerCpc + " and " + est.upperCpc);
			Console.WriteLine("Average position between " + est.lowerAvgPosition + " and " + est.upperAvgPosition);
			Console.ReadLine();
		}
	}
}