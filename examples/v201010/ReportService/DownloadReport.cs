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

using Google.Api.Ads.Common.Util;
using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.v201010;

using System;
using System.Text;

namespace Google.Api.Ads.Dfp.Examples.v201010 {
  /// <summary>
  /// This code example downloads a completed report. To run a report, run
  /// RunDeliveryReport.cs.
  /// </summary>
  class DownloadReport : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example downloads a completed report. To run a report, run " +
            "RunDeliveryReport.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new DownloadReport();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      ReportService reportService = (ReportService) user.GetService(
          DfpService.v201010.ReportService);

      // Set the id of the completed report.
      long reportJobId = long.Parse(_T("INSERT_REPORT_JOB_ID_HERE"));

      try {
        // Download report data.
        string url = reportService.getReportDownloadURL(reportJobId, ExportFormat.CSV);
        byte[] gzipReport = MediaUtilities.GetAssetDataFromUrl(url);
        string reportContents = Encoding.UTF8.GetString(MediaUtilities.DeflateGZipData(gzipReport));
        // Display results.
        Console.WriteLine("Data for report job with id '{0}\':\n{1}", reportJobId, reportContents);
      } catch (Exception ex) {
        Console.WriteLine("Failed to download report. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
