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
using Google.Api.Ads.Dfa.v1_17;

using System;
using System.Collections.Generic;
using System.Text;
using Google.Api.Ads.Common.Util;

namespace Google.Api.Ads.Dfa.Examples.CSharp.v1_17 {
  /// <summary>
  /// This code example fetches information about all reports generated from
  /// the same query, including their status (pending, running, complete, etc.)
  /// and URLs where they can be downloaded if completed. There is currently no
  /// way to get a query ID through the DFA API; you must use the website
  /// interface or the Java DART API instead.
  ///
  /// Tags: report.getReportsByCriteria
  /// </summary>
  class GetReports : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example fetches information about all reports generated from the " +
            "same query, including their status (pending, running, complete, etc.) and URLs " +
            "where they can be downloaded if completed. There is currently no way to get a " +
            "query ID through the DFA API; you must use the website interface or the Java DART " +
            "API instead.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetReports();
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
          DfaService.v1_17.ReportRemoteService);

      long queryId = long.Parse(_T("INSERT_QUERY_ID_HERE"));

      // Create report search criteria object.
      ReportSearchCriteria reportSearchCriteria = new ReportSearchCriteria();
      reportSearchCriteria.queryId = queryId;

      try {
        // Get report information.
        ReportInfoRecordSet reportInfoRecordSet =
            service.getReportsByCriteria(reportSearchCriteria);

        // Display information on reports.
        if (reportInfoRecordSet.records != null && reportInfoRecordSet.records.Length > 0) {
          foreach (ReportInfo report in reportInfoRecordSet.records) {
            Console.WriteLine("Report with ID '{0}', status of '{1}' and URL of '{2}' was found.",
                report.reportId, report.status, report.url);
          }
        } else {
          Console.WriteLine("No reports found for your query ID.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to retrieve all reports. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
