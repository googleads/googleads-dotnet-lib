//
// Copyright (C) 2008 Google Inc.
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
  // Gets quota usage information.
  class InfoServiceDemo
  {
    public static void run()
    {
      // Create a user (reads headers from App.config file).
      AdWordsUser user = new AdWordsUser();
      user.useSandbox();  // use sandbox

      // Get the service.
      InfoService service = (InfoService) user.getService("InfoService");

      // Get the quota for this month.
      long usageQuota = service.getUsageQuotaThisMonth();
      Console.WriteLine("Usage quota for this month: " + usageQuota);

      // Get the quota used between January 1, 2007 and today.
      long unitCount = service.getUnitCount(
          new DateTime(2007, 1, 1, 0, 0, 0), DateTime.Today);
      Console.WriteLine("Unit count for the past day month: " + unitCount);

      // Get the operation count used between January 1, 2007 and today.
      long operationCount = service.getOperationCount(
          new DateTime(2007, 1, 1, 0, 0, 0), DateTime.Today);
      Console.WriteLine("Operation count for the past day month: "
              + operationCount);

      // Get the quota used between January 1, 2007 and today for
      // AccountService.getAccountInfo() call.
      long methodUnitCount = service.getUnitCountForMethod(
          "AccountService",
          "getAccountInfo",
          new DateTime(2007, 1, 1, 0, 0, 0),
          DateTime.Today);
      Console.WriteLine(
          "Method unit count for AccountService.getAccountInfo between "
          + "January 1, 2007 and today: {0}", methodUnitCount);

      Console.ReadLine();
    }
  }
}
