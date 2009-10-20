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

// Author: api.anash@gmail.com (Anash P. Oommen)

using com.google.api.adwords.lib;
using com.google.api.adwords.v200909;

using System;
using System.Collections.Generic;
using System.Text;

namespace com.google.api.adwords.samples.v200909 {
  /// <summary>
  /// This sample retrieves the total API usage for a given date range.
  /// </summary>
  class GetApiUsage : SampleBase {
    /// <summary>
    /// Returns a description about the sample code.
    /// </summary>
    public override string Description {
      get {
        return "This sample retrieves the total API usage for a given date range.";
      }
    }

    /// <summary>
    /// Run the sample code.
    /// </summary>
    /// <param name="user">The AdWords user object running the sample.
    /// </param>
    public override void Run(AdWordsUser user) {
      InfoService infoService = (InfoService) user.GetService(AdWordsService.v200909.InfoService);

      // Since we are requesting the total API usage, clear out
      // the clientEmail field.
      infoService.RequestHeader.clientEmail = null;
      InfoSelector selector = new InfoSelector();
      selector.apiUsageTypeSpecified = true;
      selector.apiUsageType = ApiUsageType.UNIT_COUNT;

      // Request for API usage from 1st to 30th Sep 2009.
      DateTime startDate = new DateTime(2009, 9, 1).ToUniversalTime();
      DateTime endDate = new DateTime(2009, 9, 30).ToUniversalTime();

      selector.dateRange = new DateRange();
      selector.dateRange.min = startDate.ToString("yyyyMMdd");
      selector.dateRange.max = endDate.ToString("yyyyMMdd");

      try {
        ApiUsageInfo usageInfo = infoService.get(selector);
        Console.WriteLine("The total Api usage between '{0}' and '{1}' is {2} units.",
            startDate.ToString("dd MMM yyyy"), endDate.ToString("dd MMM yyyy"), usageInfo.cost);
      } catch (Exception ex) {
        Console.WriteLine("Failed to retrieve total Api usage in the given date range. " +
            "Exception says \"{0}\"", ex.Message);
      }

    }
  }
}
