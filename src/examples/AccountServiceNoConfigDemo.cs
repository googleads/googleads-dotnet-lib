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
using System.Collections;
using System.Text;

using com.google.api.adwords.lib;
using com.google.api.adwords.v10;

namespace com.google.api.adwords.examples
{
	// Displays some of the account's info
	class AccountServiceNoConfigDemo
	{
		public static void run()
		{
			// Creates a user.
			Hashtable headers = new Hashtable();
			headers.Add("email", "api.sgrinberg@gmail.com");
			headers.Add("useragent", "C# Client Library");
			headers.Add("password", "secret");
			headers.Add("clientEmail", "client_1+api.sgrinberg@gmail.com");
			headers.Add("applicationToken", "api.sgrinberg@gmail.com++USD");
			headers.Add("developerToken", "api.sgrinberg@gmail.com++USD");
			
			AdWordsUser user = new AdWordsUser(headers);
			user.useSandbox();	// use sandbox

			// Get the service.
			AccountService service = 
				(AccountService) user.getService("AccountService");

			// Gets account's info.
			AccountInfo acctInfo = service.getAccountInfo();

			Console.WriteLine("----- Account Info -----"
							+ "\nCustomer Id: " 
							+ acctInfo.customerId
							+ "\nDescriptive Name: " 
							+ acctInfo.descriptiveName);
			if (null != acctInfo.billingAddress) 
			{
				Console.WriteLine("Billing information"
								+ "\n   Company Name: " 
								+ acctInfo.billingAddress.companyName
								+ "\n   Address Line 1: " 
								+ acctInfo.billingAddress.addressLine1
								+ "\n   Address Line 2: " 
								+ acctInfo.billingAddress.addressLine2
								+ "\n   City: " 
								+ acctInfo.billingAddress.city
								+ "\n   State: " 
								+ acctInfo.billingAddress.state
								+ "\n   Postal Code: " 
								+ acctInfo.billingAddress.postalCode
								+ "\n   Country Code: " 
								+ acctInfo.billingAddress.countryCode);
			}
			Console.WriteLine("Time Zone ID: " + acctInfo.timeZoneId
							+ "\n------------------------");

			Console.ReadLine();
		}
	}
}