// Copyright 2015, Google Inc. All Rights Reserved.
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

// Author: Anash P. Oommen

using Google.Api.Ads.Common.Util;
using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.v201502;

using System;
using System.Text;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201502 {
  /// <summary>
  /// This code example downloads a completed report and prints out its
  /// contents. To download a completed report to a file, run DownloadReport.cs.
  /// To run a report, run RunDeliveryReport.cs.
  ///
  /// Tags: ReportService.getReportDownloadUrlWithOptions
  /// </summary>
  class DisplayReport : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example downloads a completed report and prints out its contents. " +
            "To download a completed report to a file, run DownloadReport.cs. To run a report, " +
            "run RunDeliveryReport.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new DisplayReport();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      ReportService reportService = (ReportService) user.GetService(
          DfpService.v201502.ReportService);

      // Set the id of the completed report.
      long reportJobId = long.Parse(_T("INSERT_REPORT_JOB_ID_HERE"));
      ExportFormat exportFormat = (ExportFormat) Enum.Parse(typeof(ExportFormat),
          _T("INSERT_EXPORT_FORMAT_HERE"));

      // Set the format of the report (e.g., CSV_DUMP) and download without
      // compression so we can print it.
      ReportDownloadOptions reportDownloadOptions = new ReportDownloadOptions();
      reportDownloadOptions.exportFormat = exportFormat;
      reportDownloadOptions.useGzipCompression = false;

      try {
        // Download report data.
        string downloadUrl = reportService.getReportDownloadUrlWithOptions(reportJobId,
            reportDownloadOptions);
        byte[] rawReport = MediaUtilities.GetAssetDataFromUrl(downloadUrl);
        string reportContents = Encoding.UTF8.GetString(rawReport);

        // Display results.
        Console.WriteLine("Data for report job with id '{0}\':\n{1}", reportJobId, reportContents);
      } catch (Exception ex) {
        Console.WriteLine("Failed to download report. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
