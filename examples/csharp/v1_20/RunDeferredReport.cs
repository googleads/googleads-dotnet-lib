// Copyright 2013, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.Dfa.Lib;
using Google.Api.Ads.Dfa.v1_20;

using System;
using System.Collections.Generic;
using System.Text;
using Google.Api.Ads.Common.Util;

namespace Google.Api.Ads.Dfa.Examples.CSharp.v1_20 {
  /// <summary>
  /// This code example requests the generation of a report. Once the report
  /// has been requested, you can continually get updates on the status of the
  /// report until it is completed. There is currently no way to get a query ID
  /// through the DFA API; you must use the website interface or the Java DART
  /// API instead. To request an update on a report's status, run GetReport.cs.
  ///
  /// Tags: report.runDeferredReport
  /// </summary>
  class RunDeferredReport : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example requests the generation of a report. Once the report has been " +
            "requested, you can continually get updates on the status of the report until it " +
            "is completed. There is currently no way to get a query ID through the DFA API; " +
            "you must use the website interface or the Java DART API instead. To request an " +
            "update on a report's status, run GetReport.cs";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new RunDeferredReport();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfaUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The Dfa user object running the code example.
    /// </param>
    public override void Run(DfaUser user) {
      // Create ReportRemoteService instance.
      ReportRemoteService service = (ReportRemoteService) user.GetService(
          DfaService.v1_20.ReportRemoteService);

      long queryId = long.Parse(_T("INSERT_QUERY_ID_HERE"));

      // Create report request object.
      ReportRequest reportRequest = new ReportRequest();
      reportRequest.queryId = queryId;

      try {
        // Request generation of a report for your query.
        ReportInfo reportInfo = service.runDeferredReport(reportRequest);

        // Display success message.
        Console.WriteLine("Report with ID '{0}' has been scheduled.", reportInfo.reportId);
      } catch (Exception ex) {
        Console.WriteLine("Failed to schedule report. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
