// Copyright 2012, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdWords.v201206;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201206 {
  /// <summary>
  /// This code example retrieves the unit usage for a client account for Jan
  /// 2013. This code example will not work for date ranges greater than Mar 1
  /// 2013 due to recent billing changes. See 
  /// http://googleadsdeveloper.blogspot.in/2013/01/new-simplified-adwords-api-pricing.html
  /// for details.
  ///
  /// Tags: InfoService.get
  /// </summary>
  public class GetClientUnitUsage : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      GetClientUnitUsage codeExample = new GetClientUnitUsage();
      Console.WriteLine(codeExample.Description);
      try {
        codeExample.Run(new AdWordsUser());
      } catch (Exception ex) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(ex));
      }
    }

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example retrieves the unit usage for a client account for Jan 2013. " +
            "This code example will not work for date ranges greater than Mar 1 2013 due to " +
            "recent billing changes. See " +
            "http://googleadsdeveloper.blogspot.in/2013/01/new-simplified-adwords-api-pricing.html " +
            "for details.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    public void Run(AdWordsUser user) {
      // Get the InfoService.
      InfoService infoService = (InfoService) user.GetService(AdWordsService.v201206.InfoService);

      // Ensure the clientCustomerId is not set, so that requests are made to
      // the MCC.
      infoService.RequestHeader.clientCustomerId = null;

      // Create the selector.
      InfoSelector selector = new InfoSelector();
      selector.clientCustomerIds = new long[] {GetCustomerId(user)};
      selector.includeSubAccounts = true;
      selector.apiUsageType = ApiUsageType.UNIT_COUNT_FOR_CLIENTS;

      // Create date range for retrieving unit usage. We will use a fixed date
      // range in the past, since InfoService is going away due to billing
      // changes.
      DateRange dateRange = new DateRange();
      dateRange.min = "20130101";
      dateRange.max = "20130131"; 
      selector.dateRange = dateRange;

      try {
        // Get the client's unit usage.
        ApiUsageInfo info = infoService.get(selector);

        // Display the results.
        if (info != null && info.apiUsageRecords != null) {
          foreach (ApiUsageRecord record in info.apiUsageRecords) {
            Console.WriteLine("API Usage for customer ID '{0:###-###-####}' is {1} units.",
                record.clientCustomerId, record.cost);
          }
        } else {
          Console.WriteLine("No API usage records were found for client.");
        }
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to get unit usage for client.", ex);
      }
    }

    /// <summary>
    /// Gets the customer ID.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <returns>The customer ID.</returns>
    private long GetCustomerId(AdWordsUser user) {
      return long.Parse((user.Config as AdWordsAppConfig).ClientCustomerId.Replace("-", ""));
    }
  }
}
