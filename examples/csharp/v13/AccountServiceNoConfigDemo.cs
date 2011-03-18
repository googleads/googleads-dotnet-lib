// Copyright 2011, Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

// Author: api.anash@gmail.com (Anash P. Oommen)

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.Util;
using Google.Api.Ads.AdWords.v13;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web.Services.Protocols;
using System.Reflection;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v13 {
  /// <summary>
  /// This code example displays some of the account's info. It also
  /// demonstrates how to override the settings from App.config.
  /// </summary>
  class AccountServiceNoConfigDemo: SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example displays some of the account's info. It also demonstrates how " +
            "to override the settings from App.config.";
      }
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Declare the headers.
      Dictionary<string, string> headers = new Dictionary<string, string>();

      headers.Add("email", "ENTER_YOUR_EMAIL_HERE");
      headers.Add("password", "ENTER_YOUR_PASSWORD_HERE");
      headers.Add("useragent", "ENTER_YOUR_COMPANY_NAME_HERE");
      headers.Add("developerToken", "ENTER_YOUR_DEVELOPER_TOKEN_HERE");
      headers.Add("applicationToken", "ENTER_YOUR_APPLICATION_TOKEN_HERE");
      headers.Add("clientEmail", "ENTER_YOUR_CLIENT_EMAIL_HERE");

      // Create a custom AdWordsUser.
      user = new AdWordsUser(headers);

      // Get the service.
      AccountService service = (AccountService) user.GetService(AdWordsService.v13.AccountService,
          "https://sandbox.google.com");

      // Gets account's info.
      AccountInfo acctInfo = service.getAccountInfo();

      Console.WriteLine("----- Account Info -----\nCustomer Id: {0}\nDescriptive Name: {1}",
          acctInfo.customerId, acctInfo.descriptiveName);

      if (null != acctInfo.billingAddress) {
        Console.WriteLine("Billing information\n   Company Name: {0}\n   Address Line 1: {1}" +
            "\n   Address Line 2: {2}\n   City: {3}\n   State: {4}\n   Postal Code: {5}" +
            "\n   Country Code: {6}", acctInfo.billingAddress.companyName,
            acctInfo.billingAddress.addressLine1,
            acctInfo.billingAddress.addressLine2, acctInfo.billingAddress.city,
            acctInfo.billingAddress.state, acctInfo.billingAddress.postalCode,
            acctInfo.billingAddress.countryCode);
      }

      Console.WriteLine("Time Zone ID: {0}\n------------------------", acctInfo.timeZoneId);
    }
  }
}
