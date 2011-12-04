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
using Google.Api.Ads.AdWords.v201109;
using Google.Api.Ads.Common.Lib;

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201109 {
  /// <summary>
  /// This code example gets and downloads an Ad Hoc report from a XML report
  /// definition.
  /// </summary>
  class DownloadAdhocReport : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets and downloads an Ad Hoc report from a XML report " +
            "definition.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new DownloadAdhocReport();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      string fileName = _T("INSERT_OUTPUT_FILE_NAME_HERE");

      ReportDefinition definition = new ReportDefinition();

      definition.reportName = "Last 7 days ADGROUP_PERFORMANCE_REPORT";
      definition.reportType = ReportDefinitionReportType.ADGROUP_PERFORMANCE_REPORT;
      definition.downloadFormat = DownloadFormat.GZIPPED_CSV;
      definition.dateRangeType = ReportDefinitionDateRangeType.LAST_7_DAYS;

      Selector selector = new Selector();
      selector.fields = new string[] {"CampaignId", "Id", "Impressions", "Clicks", "Cost"};

      Predicate predicate = new Predicate();
      predicate.field = "Status";
      predicate.@operator = PredicateOperator.IN;
      predicate.values = new string[] {"ENABLED", "PAUSED"};
      selector.predicates = new Predicate[] {predicate};

      definition.selector = selector;
      definition.includeZeroImpressions = true;

      string path = GetHomeDir() + Path.DirectorySeparatorChar + fileName;

      try {
        // If you know that your report is small enough to fit in memory, then
        // you can instead use
        // ClientReport report = new ReportUtilities(user).GetClientReport(definition);
        //
        // // Get the text report directly if you requested a text format
        // // (e.g. xml)
        // string reportText = report.Text;
        //
        // // Get the binary report if you requested a binary format
        // // (e.g. gzip)
        // byte[] reportBytes = report.Contents;
        //
        // // Deflate a zipped binary report for further processing.
        // string deflatedReportText = Encoding.UTF8.GetString(
        //     MediaUtilities.DeflateGZipData(report.Contents));
        new ReportUtilities(user).DownloadClientReport(definition, path);
        Console.WriteLine("Report was downloaded to '{0}'.", path);
      } catch (Exception ex) {
        Console.WriteLine("Failed to download report. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
