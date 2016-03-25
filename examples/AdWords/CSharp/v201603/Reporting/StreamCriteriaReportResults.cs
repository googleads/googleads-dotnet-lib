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
using System.IO.Compression;
using System.Xml;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201603 {
  /// <summary>
  /// This code example streams the results of an ad hoc report, collecting
  /// total impressions by network from each line. This demonstrates how you
  /// can extract data from a large report without holding the entire result
  /// set in memory or using files.
  /// </summary>
  public class StreamCriteriaReportResults : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      StreamCriteriaReportResults codeExample = new StreamCriteriaReportResults();
      Console.WriteLine(codeExample.Description);
      try {
        codeExample.Run(new AdWordsUser());
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
        return "This code example streams the results of an ad hoc report, collecting total " +
            "impressions by network from each line. This demonstrates how you can extract " +
            "data from a large report without holding the entire result set in memory or " +
            "using files.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    public void Run(AdWordsUser user) {
      // Create the query.
      String query = "SELECT Id, AdNetworkType1, Impressions FROM CRITERIA_PERFORMANCE_REPORT " +
          "WHERE Status IN [ENABLED, PAUSED] DURING LAST_7_DAYS";

      ReportUtilities reportUtilities = new ReportUtilities(user, "v201603", query,
          DownloadFormat.GZIPPED_XML.ToString());

      Dictionary<string, long> impressionsByAdNetworkType1 = new Dictionary<string, long>();

      try {
        using (ReportResponse response = reportUtilities.GetResponse()) {
          using (GZipStream gzipStream = new GZipStream(response.Stream,
              CompressionMode.Decompress)) {
            using (XmlTextReader reader = new XmlTextReader(gzipStream)) {
              while (reader.Read()) {
                switch (reader.NodeType) {
                  case XmlNodeType.Element: // The node is an Element.
                    if (reader.Name == "row") {
                      ParseRow(impressionsByAdNetworkType1, reader);
                    }
                    break;
                }
              }
            }
          }
        }

        Console.WriteLine("Network, Impressions");
        foreach (string network in impressionsByAdNetworkType1.Keys) {
          Console.WriteLine("{0}, {1}", network, impressionsByAdNetworkType1[network]);
        }
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to download report.", e);
      }
    }

    /// <summary>
    /// Parses a report row.
    /// </summary>
    /// <param name="impressionsByAdNetworkType1">The map that keeps track of
    /// the impressions grouped by by ad network type1.</param>
    /// <param name="reader">The XML reader that parses the report.</param>
    private static void ParseRow(Dictionary<string, long> impressionsByAdNetworkType1,
        XmlTextReader reader) {
      string network = null;
      long impressions = 0;

      while (reader.MoveToNextAttribute()) {
        switch (reader.Name) {
          case "network":
            network = reader.Value;
            break;

          case "impressions":
            impressions = long.Parse(reader.Value);
            break;
        }
      }

      if (network != null) {
        if (!impressionsByAdNetworkType1.ContainsKey(network)) {
          impressionsByAdNetworkType1[network] = 0;
        }
        impressionsByAdNetworkType1[network] += impressions;
      }
    }
  }
}
