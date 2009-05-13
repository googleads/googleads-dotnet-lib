// Copyright 2009, Google Inc. All Rights Reserved.
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

using System;
using System.Text;

using com.google.api.adwords.lib;
using com.google.api.adwords.v13;

namespace com.google.api.adwords.samples.v13 {
  /// <summary>
  /// Gets quota usage information.
  /// </summary>
  class InfoServiceDemo : SampleBase {
    /// <summary>
    /// Returns a description about the sample code.
    /// </summary>
    public override string Description {
      get {
        return "gets quota usage information.";
      }
    }

    /// <summary>
    /// Run the sample code.
    /// </summary>
    /// <param name="user">The AdWords user object running the sample.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the service.
      InfoService service = (InfoService) user.GetService(ApiServices.v13.InfoService);

      // Get the quota for this month.
      long usageQuota = service.getUsageQuotaThisMonth();
      Console.WriteLine("Usage quota for this month: " + usageQuota);

      // Get the quota used between January 1, 2009 and today.
      long unitCount = service.getUnitCount(new DateTime(2009, 1, 1, 0, 0, 0), DateTime.Today);
      Console.WriteLine("Unit count between January 1, 2009 and today: {0}", unitCount);

      // Get the operation count used between January 1, 2009 and today.
      long operationCount =
          service.getOperationCount(new DateTime(2009, 1, 1, 0, 0, 0), DateTime.Today);
      Console.WriteLine("Operation count between January 1, 2009 and today: {0}", operationCount);

      // Get the quota used between January 1, 2009 and today for
      // AccountService.getAccountInfo() call.
      long methodUnitCount = service.getUnitCountForMethod("AccountService", "getAccountInfo",
          new DateTime(2009, 1, 1, 0, 0, 0), DateTime.Today);
      Console.WriteLine("Method unit count for AccountService.getAccountInfo between " +
          "January 1, 2009 and today: {0}", methodUnitCount);
    }
  }
}
