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

using com.google.api.adwords.lib;
using com.google.api.adwords.v10;

using System;
using System.Text;

namespace com.google.api.adwords.examples
{
  // Displays some of the account's info
  class AccountServiceDemo
  {
    public static void run()
    {
      // Creates a user (reads headers from App.config file).
      AdWordsUser user = new AdWordsUser();
      user.useSandbox();  // use sandbox

      // Get the service.
      AccountService service =
        (AccountService) user.getService("AccountService");

      // Gets account's info.
      AccountInfo acctInfo = service.getAccountInfo();

      Console.WriteLine("----- Account Info -----"
          + "\nCustomer Id: {0}"
          + "\nDescriptive Name: {1}",
          acctInfo.customerId, acctInfo.descriptiveName);
      if (null != acctInfo.billingAddress)
      {
        Console.WriteLine(
            "Billing information"
            + "\n   Company Name: {0}"
            + "\n   Address Line 1: {1}"
            + "\n   Address Line 2: {2}"
            + "\n   City: {3}"
            + "\n   State: {4}"
            + "\n   Postal Code: {5}"
            + "\n   Country Code: {6}", acctInfo.billingAddress.companyName,
            acctInfo.billingAddress.addressLine1,
            acctInfo.billingAddress.addressLine2, acctInfo.billingAddress.city,
            acctInfo.billingAddress.state, acctInfo.billingAddress.postalCode,
            acctInfo.billingAddress.countryCode);
      }
      Console.WriteLine(
          "Time Zone ID: {0}\n------------------------", acctInfo.timeZoneId);

      Console.ReadLine();
    }
  }
}
