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
	 * Displays some of the account's info. 
	 */
	class AccountServiceDemo
	{
		public static void run()
		{
			// Create a user (reads headers from app.config file)
			AdWordsUser user = new AdWordsUser();
			// Use sandbox
			user.useSandbox();
			// Get an Account Service client
			AccountService service = (AccountService)user.getService("AccountService");

			// Get the account info
			AccountInfo acctInfo = service.getAccountInfo();
			Console.WriteLine("----- Account Info -----");
			Console.WriteLine("Customer ID: " + acctInfo.customerId);
			Console.WriteLine("Descriptive Name: " + acctInfo.descriptiveName);
			if (null != acctInfo.billingAddress) 
			{
				Console.WriteLine("Billing information");
				Console.WriteLine("   Company Name: " + acctInfo.billingAddress.companyName);
				Console.WriteLine("   Address Line 1: " + acctInfo.billingAddress.addressLine1);
				Console.WriteLine("   Address Line 2: " + acctInfo.billingAddress.addressLine2);
				Console.WriteLine("   City: " + acctInfo.billingAddress.city);
				Console.WriteLine("   State: " + acctInfo.billingAddress.state);
				Console.WriteLine("   Postal Code: " + acctInfo.billingAddress.postalCode);

				Console.WriteLine("   Country Code: " + acctInfo.billingAddress.countryCode);
			}
			Console.WriteLine("Time Zone ID: " + acctInfo.timeZoneId);
			Console.WriteLine("------------------------");
			Console.ReadLine();
		}
	}
}