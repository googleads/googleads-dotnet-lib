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
using com.google.api.adwords.v8;
using com.google.api.adwords.lib;


namespace com.google.api.adwords.examples
{
	/**
	 * Gets quota usage details.
	 */
	class InfoServiceDemo
	{
		public static void run()
		{
			//create a user (reads headers from app.config file)
			AdWordsUser user = new AdWordsUser();
			//uses sandbox
			//user.useSandbox();

			InfoService service = (InfoService)user.getService("InfoService");

			// get the quota for this month
			// get the quota for this month
			long usageQuota = service.getUsageQuotaThisMonth();
			Console.WriteLine("Usage quota for this month: " + usageQuota);
			//Console.ReadLine();


			//get the quota used between July 1, 2006 and today
			long unitCount = service.getUnitCount(
				new DateTime(2006, 7, 1, 0, 0, 0),
				DateTime.Today);
			Console.WriteLine("Unit count for the past day month: " 
				+ unitCount);
			//Console.ReadLine();

			//get the operation count used between July 1, 2006 and today
			long operationCount = service.getOperationCount(
				new DateTime(2006, 7, 1, 0, 0, 0),
				DateTime.Today);
			Console.WriteLine("Operation count for the past day month: " 
				+ operationCount);


			long methodUnitCount = service.getUnitCountForMethod(
				"AccountService", 
				"getAccountInfo", 
				new DateTime(2006, 7, 1, 0, 0, 0),
				DateTime.Today);
			Console.WriteLine("Method unit count for AccountService" 
				+ ".getAccountInfo between July 1, 2006 and"
				+ " today: " + methodUnitCount);

			Console.ReadLine();
		}
	}
}