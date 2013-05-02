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
using System.Threading;
using System.IO;

namespace Google.Api.Ads.Dfa.Examples.CSharp.v1_20 {
  /// <summary>
  /// This code example mimics the RCRunner file "RCReport" but uses the DFA
  /// API instead of the Java DART API. It shows how to request the generation
  /// of a deferred report, how to check to see when it is done, and how to
  /// download it when it is completed.
  ///
  /// Tags: report.getReport, report.runDeferredReport
  /// </summary>
  class RCReport : SampleBase {
    /// <summary>
    /// Time to wait between checks for report status.
    /// </summary>
    private const int TIME_BETWEEN_CHECKS = 10 * 60 * 1000;

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example mimics the RCRunner file 'RCReport' but uses the DFA API " +
            "instead of the Java DART API. It shows how to request the generation of a " +
            "deferred report, how to check to see when it is done, and how to download it " +
            "when it is completed.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      if (args.Length != 4) {
        Console.WriteLine("This program requires 4 command line arguments:");
        Console.WriteLine("\t1. DFA username\n\t2. DFA password\n" +
            "\t3. Query ID number\n\t4. Output filename.");
        Console.WriteLine("Example usage: RCReport.exe username@dfa password456 " +
            "12345 report.zip");
      }

      RCReport codeExample = new RCReport();
      Console.WriteLine(codeExample.Description);
      DfaUser user = new DfaUser();
      ReportRemoteService reportService = (ReportRemoteService) user.GetService(
          DfaService.v1_20.ReportRemoteService);
      LoginRemoteService loginService = (LoginRemoteService) user.GetService(
          DfaService.v1_20.LoginRemoteService);

      codeExample.ScheduleAndDownloadReport(loginService, reportService, args[0], args[1],
          long.Parse(args[2]), args[3]);
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The Dfa user object running the code example.
    /// </param>
    public override void Run(DfaUser user) {
      string userName = _T("INSERT_USERNAME_HERE");
      string password = _T("INSERT_PASSWORD_HERE");
      long queryId = long.Parse(_T("INSERT_QUERY_ID_HERE"));
      string filePath = _T("INSERT_PATH_TO_SAVE_REPORT_HERE");

      ReportRemoteService reportService = (ReportRemoteService) user.GetService(
          DfaService.v1_20.ReportRemoteService);
      LoginRemoteService loginService = (LoginRemoteService) user.GetService(
          DfaService.v1_20.LoginRemoteService);

      ScheduleAndDownloadReport(loginService, reportService, userName, password, queryId, filePath);
    }

    /// <summary>
    /// Schedules and downloads a report.
    /// </summary>
    /// <param name="loginService">The login service instance.</param>
    /// <param name="reportService">The report service instance.</param>
    /// <param name="userName">The user name to be used for authentication
    /// purposes.</param>
    /// <param name="password">The password to be used for authentication
    /// purposes.</param>
    /// <param name="queryId">The query id to be used for generating reports.
    /// </param>
    /// <param name="filePath">The file path to which the downloaded report
    /// should be saved.</param>
    public void ScheduleAndDownloadReport(LoginRemoteService loginService,
        ReportRemoteService reportService, string userName, string password, long queryId,
        string filePath) {

      // Override the credentials in App.config with the ones the user
      // provided.
      string authToken = loginService.authenticate(userName, password).token;

      reportService.Token.Username = userName;
      reportService.Token.Password = authToken;

      // Create report request and submit it to the server.
      ReportRequest reportRequest = new ReportRequest();
      reportRequest.queryId = queryId;

      try {
        ReportInfo reportInfo = reportService.runDeferredReport(reportRequest);
        long reportId = reportInfo.reportId;
        Console.WriteLine("Report with ID '{0}' has been scheduled.", reportId);

        reportRequest.reportId = reportId;

        while (reportInfo.status.name != "COMPLETE") {
          Console.WriteLine("Still waiting for report with ID '{0}', current status is '{1}'.",
              reportId, reportInfo.status.name);
          Console.WriteLine("Waiting 10 minutes before checking on report status.");
          // Wait 10 minutes.
          Thread.Sleep(TIME_BETWEEN_CHECKS);
          reportInfo = reportService.getReport(reportRequest);
          if (reportInfo.status.name == "ERROR") {
            throw new Exception("Deferred report failed with errors. Run in the UI to " +
                "troubleshoot.");
          }
        }

        FileStream fs = File.OpenWrite(filePath);
        byte[] bytes = MediaUtilities.GetAssetDataFromUrl(reportInfo.url);
        fs.Write(bytes, 0, bytes.Length);
        fs.Close();

        Console.WriteLine("Report successfully downloaded to '{0}'.", filePath);
      } catch (Exception ex) {
        Console.WriteLine("Failed to schedule and download report. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
