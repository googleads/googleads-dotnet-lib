// Copyright 2012, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdWords.v201109_1;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201109_1 {
  /// <summary>
  /// This code example gets and downloads a criteria Ad Hoc report from an XML
  /// report definition.
  /// </summary>
  public class DownloadCriteriaReport : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      ExampleBase codeExample = new DownloadCriteriaReport();
      Console.WriteLine(codeExample.Description);
      try {
        codeExample.Run(new AdWordsUser(), codeExample.GetParameters(), Console.Out);
      } catch (Exception ex) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(ex));
      }
    }

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets and downloads a criteria Ad Hoc report from an XML report " +
            "definition.";
      }
    }

    /// <summary>
    /// Gets the list of parameter names required to run this code example.
    /// </summary>
    /// <returns>
    /// A list of parameter names for this code example.
    /// </returns>
    public override string[] GetParameterNames() {
      return new string[] {"OUTPUT_FILE_NAME"};
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
      string fileName = parameters["OUTPUT_FILE_NAME"];

      ReportDefinition definition = new ReportDefinition();

      definition.reportName = "Last 7 days CRITERIA_PERFORMANCE_REPORT";
      definition.reportType = ReportDefinitionReportType.CRITERIA_PERFORMANCE_REPORT;
      definition.downloadFormat = DownloadFormat.GZIPPED_CSV;
      definition.dateRangeType = ReportDefinitionDateRangeType.LAST_7_DAYS;

      // Create selector.
      Selector selector = new Selector();
      selector.fields = new string[] {"CampaignId", "AdGroupId", "Id", "CriteriaType", "Criteria",
          "CriteriaDestinationUrl", "Clicks", "Impressions", "Cost"};

      Predicate predicate = new Predicate();
      predicate.field = "Status";
      predicate.@operator = PredicateOperator.IN;
      predicate.values = new string[] {"ACTIVE", "PAUSED"};
      selector.predicates = new Predicate[] {predicate};

      definition.selector = selector;
      definition.includeZeroImpressions = true;

      string filePath = ExampleUtilities.GetHomeDir() + Path.DirectorySeparatorChar + fileName;

      try {
        // If you know that your report is small enough to fit in memory, then
        // you can instead use
        // ReportUtilities utilities = new ReportUtilities(user);
        // utilities.ReportVersion = "v201109_1";
        // ClientReport report = utilities.GetClientReport(definition);
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
        ReportUtilities utilities = new ReportUtilities(user);
        utilities.ReportVersion = "v201109_1";
        utilities.DownloadClientReport(definition, filePath);
        writer.WriteLine("Report was downloaded to '{0}'.", filePath);
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to download report.", ex);
      }
    }
  }
}
