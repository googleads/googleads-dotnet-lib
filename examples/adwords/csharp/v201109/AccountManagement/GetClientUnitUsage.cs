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
using Google.Api.Ads.AdWords.v201109;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201109 {
  /// <summary>
  /// This code example retrieves the unit usage for a client account for this
  /// month.
  ///
  /// Tags: InfoService.get
  /// </summary>
  class GetClientUnitUsage : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      ExampleBase codeExample = new GetClientUnitUsage();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser(), codeExample.GetParameters(), Console.Out);
    }

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example retrieves the unit usage for a client account for this month.";
      }
    }

    /// <summary>
    /// Gets the list of parameter names required to run this code example.
    /// </summary>
    /// <returns>
    /// A list of parameter names for this code example.
    /// </returns>
    public override string[] GetParameterNames() {
      return new string[] {};
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="parameters">The parameters for running the code
    /// example.</param>
    /// <param name="writer">The stream writer to which script output should be
    /// written.</param>
    public override void Run(AdWordsUser user, Dictionary<string, string> parameters,
        TextWriter writer) {
      // Get the InfoService.
      InfoService infoService = (InfoService) user.GetService(AdWordsService.v201109.InfoService);

      // Ensure the clientCustomerId is not set, so that requests are made to
      // the MCC.
      infoService.RequestHeader.clientCustomerId = null;

      // Create the selector.
      InfoSelector selector = new InfoSelector();
      selector.clientCustomerIds = new long[] {GetCustomerId(user)};
      selector.includeSubAccounts = true;
      selector.apiUsageType = ApiUsageType.UNIT_COUNT_FOR_CLIENTS;

      // Create date range for retrieving unit usage.
      DateRange dateRange = new DateRange();
      dateRange.min = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyyMMdd");
      dateRange.max = DateTime.Now.ToString("yyyyMMdd");
      selector.dateRange = dateRange;

      try {
        // Get the client's unit usage.
        ApiUsageInfo info = infoService.get(selector);

        // Display the results.
        if (info != null && info.apiUsageRecords != null) {
          foreach (ApiUsageRecord record in info.apiUsageRecords) {
            writer.WriteLine("API Usage for customer ID '{0:###-###-####}' is {1} units.",
                record.clientCustomerId, record.cost);
          }
        } else {
          writer.WriteLine("No API usage records were found for client.");
        }
      } catch (Exception ex) {
        writer.WriteLine("Failed to get unit usage for client. Exception says \"{0}\"",
            ex.Message);
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
