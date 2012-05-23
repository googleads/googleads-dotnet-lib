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

using Google.Api.Ads.Dfa.Lib;
using Google.Api.Ads.Dfa.v1_18;

using System;
using System.Collections.Generic;
using System.Text;
using Google.Api.Ads.Common.Util;

namespace Google.Api.Ads.Dfa.Examples.CSharp.v1_18 {
  /// <summary>
  /// This code example fetches information about a report, including its
  /// status (pending, running, complete, etc.) and a URL where it can be
  /// downloaded if completed. To get a report ID, run RunDeferredReport.cs.
  ///
  /// Tags: report.getReport
  /// </summary>
  class GetReport : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example fetches information about a report, including its status " +
            "(pending, running, complete, etc.) and a URL where it can be downloaded if " +
            "completed. To get a report ID, run RunDeferredReport.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetReport();
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
          DfaService.v1_18.ReportRemoteService);

      long reportId = long.Parse(_T("INSERT_REPORT_ID_HERE"));

      // Create report request object.
      ReportRequest reportRequest = new ReportRequest();
      reportRequest.reportId = reportId;

      try {
        // Get report information.
        ReportInfo reportInfo = service.getReport(reportRequest);
        // Display information on the report.
        Console.WriteLine("Report with ID '{0}', status of '{1}', and URL of '{2}' was found.",
            reportInfo.reportId, reportInfo.status.name, reportInfo.url);
       } catch (Exception ex) {
        Console.WriteLine("Failed to retrieve report. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
