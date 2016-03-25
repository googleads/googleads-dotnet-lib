// Copyright 2016, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.Util.Reports;
using Google.Api.Ads.AdWords.v201603;
using Google.Api.Ads.Common.Util.Reports;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201603 {
  /// <summary>
  /// This code example gets and downloads a criteria Ad Hoc report from an AWQL
  /// query. See https://developers.google.com/adwords/api/docs/guides/awql for
  /// AWQL documentation.
  /// </summary>
  public class DownloadCriteriaReportWithAwql : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      DownloadCriteriaReportWithAwql codeExample = new DownloadCriteriaReportWithAwql();
      Console.WriteLine(codeExample.Description);
      try {
        string fileName = "INSERT_FILE_NAME_HERE";
        codeExample.Run(new AdWordsUser(), fileName);
      } catch (Exception e) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(e));
      }
    }

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets and downloads a criteria Ad Hoc report from an AWQL " +
            "query. See https://developers.google.com/adwords/api/docs/guides/awql for AWQL " +
            "documentation.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="fileName">The file to which the report is downloaded.
    /// </param>
    public void Run(AdWordsUser user, string fileName) {
      string query = "SELECT CampaignId, AdGroupId, Id, Criteria, CriteriaType, Impressions, " +
          "Clicks, Cost FROM CRITERIA_PERFORMANCE_REPORT WHERE Status IN [ENABLED, PAUSED] " +
          "DURING LAST_7_DAYS";

      string filePath = ExampleUtilities.GetHomeDir() + Path.DirectorySeparatorChar + fileName;

      try {
        ReportUtilities utilities = new ReportUtilities(user, "v201603", query,
            DownloadFormat.GZIPPED_CSV.ToString());
        using (ReportResponse response = utilities.GetResponse()) {
          response.Save(filePath);
        }
        Console.WriteLine("Report was downloaded to '{0}'.", filePath);
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to download report.", e);
      }
    }
  }
}
