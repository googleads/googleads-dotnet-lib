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
using Google.Api.Ads.AdWords.v201101;

using System;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201101 {
  /// <summary>
  /// This code example adds a cross-client (MCC) report definition. To get
  /// report fields, run GetReportFields.cs. To work correctly this code
  /// example must be run as an MCC account.
  ///
  /// Tags: ReportDefinitionService.mutate
  /// </summary>
  class AddCrossClientReportDefinition : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example adds a cross-client (MCC) report definition. To get report " +
            "fields, run GetReportFields.cs. To work correctly this code example must be run " +
            "as an MCC account.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new AddCrossClientReportDefinition();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
        // Get the ReportDefinitionService.
      ReportDefinitionService reportDefinitionService = (ReportDefinitionService) user.GetService(
          AdWordsService.v201101.ReportDefinitionService);


      // Insert client emails below to limit the report to only certain accounts.
      string[] clientEmails = {};

      // Since we are creating MCC reports, we need to clear clientEmail and
      // clientCustomerId headers.
      reportDefinitionService.RequestHeader.clientEmail = null;
      reportDefinitionService.RequestHeader.clientCustomerId = null;

      // Create selector.
      Selector selector = new Selector();
      selector.fields = new string[] {"ExternalCustomerId", "AccountDescriptiveName",
          "PrimaryUserLogin", "Date", "Id", "Name", "Impressions", "Clicks",
          "Cost"};

      // Create report definition.
      ReportDefinition reportDefinition = new ReportDefinition();
      reportDefinition.reportName = "Cross-client campaign performance report #" + GetTimeStamp();
      reportDefinition.dateRangeType = ReportDefinitionDateRangeType.LAST_7_DAYS;
      reportDefinition.reportType = ReportDefinitionReportType.CAMPAIGN_PERFORMANCE_REPORT;
      reportDefinition.downloadFormat = DownloadFormat.XML;
      reportDefinition.selector = selector;

      reportDefinition.crossClient = true;

      List<ClientSelector> clientSelectors = new List<ClientSelector>();

      foreach (string clientEmail in clientEmails) {
        ClientSelector clientSelector = new ClientSelector();
        clientSelector.login = clientEmail;
        clientSelectors.Add(clientSelector);
      }
      reportDefinition.clientSelectors = clientSelectors.ToArray();

      // Create operations.
      ReportDefinitionOperation operation = new ReportDefinitionOperation();
      operation.operand = reportDefinition;
      operation.@operator = Operator.ADD;

      try {
        // Add report definition.
        ReportDefinition[] result = reportDefinitionService.mutate(new ReportDefinitionOperation[]
            {operation});

        // Display report definitions.
        if (result != null) {
          foreach (ReportDefinition tempReportDefinition in result) {
            Console.WriteLine("Report definition with name '{0}' and id '{1}' was added.\n",
                tempReportDefinition.reportName, tempReportDefinition.id);
          }
        } else {
          Console.WriteLine("No report definitions were added.\n");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to add report definition. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
