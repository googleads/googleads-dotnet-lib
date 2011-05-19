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
using Google.Api.Ads.AdWords.Util.Reports;
using Google.Api.Ads.AdWords.v201008;
using Google.Api.Ads.Common.Lib;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201008 {
  /// <summary>
  /// This code example gets and downloads a report from a report definition.
  /// To get a report definition, run AddKeywordsPerformanceReportDefinition.cs.
  /// Currently, there is only production support for report download.
  /// </summary>
  class DownloadReport : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets and downloads a report from a report definition. " +
            "To get a report definition, run AddKeywordsPerformanceReportDefinition.cs. " +
            "Currently, there is only production support for report download.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new DownloadReport();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      long reportDefinitionId = long.Parse(_T("INSERT_REPORT_DEFINITION_ID_HERE"));
      string fileName = _T("INSERT_OUTPUT_FILE_NAME_HERE");
      string path = GetHomeDir() + '\\' + fileName;

      try {
        // If you know that your report is small enough to fit in memory, then
        // you can instead use
        // ClientReport report = new ReportUtilities().GetClientReport(
        //     new AdWordsAppConfig(), reportDefinitionId);
        //
        // // Binary report file (e.g. zip format)
        // byte[] reportBytes = report.Contents;
        //
        // // Text report file (e.g. xml format)
        // string reportText = report.Text;
        new ReportUtilities(user).DownloadClientReport(reportDefinitionId, path);
        Console.WriteLine("Report with definition id '{0}' was downloaded to '{1}'.",
            reportDefinitionId, path);
      } catch (Exception ex) {
        Console.WriteLine("Failed to download report. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
