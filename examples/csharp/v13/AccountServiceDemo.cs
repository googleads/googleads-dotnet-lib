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
using Google.Api.Ads.AdWords.v13;

using System;
using System.Text;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v13 {
  /// <summary>
  /// This code example displays some of the client account's info.
  /// </summary>
  class AccountServiceDemo : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example displays some of the client account's info.";
      }
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the service.
      AccountService service =
          (AccountService) user.GetService(AdWordsService.v13.AccountService);
      service.clientEmailValue = null;

      // Gets account's info.
      AccountInfo acctInfo = service.getAccountInfo();

      Console.WriteLine("----- Account Info -----\nCustomer Id: {0}\nDescriptive Name: {1}",
          acctInfo.customerId, acctInfo.descriptiveName);
      if (acctInfo.billingAddress != null) {
        Console.WriteLine(
            "Billing information\n   Company Name: {0}\n   Address Line 1: {1}" +
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
