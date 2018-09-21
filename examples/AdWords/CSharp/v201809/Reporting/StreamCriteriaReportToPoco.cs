// Copyright 2018 Google LLC
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
using Google.Api.Ads.AdWords.Util.Reports.v201809;
using Google.Api.Ads.AdWords.v201809;
using Google.Api.Ads.Common.Util.Reports;

using System;
using System.IO.Compression;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201809
{
    /// <summary>
    /// The class that holds the data of one row of the report.
    /// </summary>
    public class CriteriaReportRow
    {
        /// <summary>
        /// The Keyword ID column.
        /// </summary>
        [ReportColumn("keywordID")]
        public long KeywordID { get; set; }

        /// <summary>
        /// The impressions column.
        /// </summary>
        [ReportColumn("impressions")]
        public long Impressions { get; set; }

        /// <summary>
        /// The network column.
        /// </summary>
        [ReportColumn("network")]
        public string NetworkType { get; set; }

        /// <summary>
        /// Returns a string that represents the current report row.
        /// </summary>
        override public string ToString()
        {
            return "Id: " + KeywordID + " Impressions: " + Impressions + " NetworkType: " +
                NetworkType;
        }
    }

    /// <summary>
    /// This code example streams the results of an ad hoc report, and
    /// returns the data in the report as objects of a given type.
    /// </summary>
    public class StreamCriteriaReportToPoco : ExampleBase
    {
        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            StreamCriteriaReportToPoco codeExample = new StreamCriteriaReportToPoco();
            Console.WriteLine(codeExample.Description);
            try
            {
                codeExample.Run(new AdWordsUser());
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception occurred while running this code example. {0}",
                    ExampleUtilities.FormatException(e));
            }

            Console.ReadLine();
        }

        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example streams the results of an ad hoc report, and " +
                    "returns the data in the report as objects of a given type.";
            }
        }

        /// <summary>
        /// Runs the code example.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        public void Run(AdWordsUser user)
        {
            // Create the query.
            ReportQuery query = new ReportQueryBuilder()
                .Select("Id", "AdNetworkType1", "Impressions")
                .From(ReportDefinitionReportType.CRITERIA_PERFORMANCE_REPORT)
                .Where("Status").In("ENABLED", "PAUSED")
                .During(ReportDefinitionDateRangeType.LAST_7_DAYS)
                .Build();

            ReportUtilities reportUtilities = new ReportUtilities(user, "v201809", query,
                DownloadFormat.GZIPPED_XML.ToString());

            try
            {
                using (ReportResponse response = reportUtilities.GetResponse())
                {
                    using (GZipStream gzipStream =
                        new GZipStream(response.Stream, CompressionMode.Decompress))
                    {
                        // Deserialize the report into a list of CriteriaReportRow.
                        // You can also deserialize the list into your own POCOs as follows.
                        // 1. Annotate your class properties with ReportRow annotation.
                        //
                        //  public class MyCriteriaReportRow {
                        //
                        //    [ReportColumn]
                        //    public long KeywordID { get; set; }
                        //
                        //    [ReportColumn]
                        //    public long Impressions { get; set; }
                        //  }
                        //
                        // 2. Deserialize into your own report rows.
                        //
                        // var report = new AwReport<MyCriteriaReportRow>(
                        //                new AwXmlTextReader(gzipStream), "Example");
                        using (var report =
                            new AwReport<CriteriaReportRow>(new AwXmlTextReader(gzipStream),
                                "Example"))
                        {
                            // Print the contents of each row object.
                            foreach (var record in report.Rows)
                            {
                                Console.WriteLine(record);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new System.ApplicationException("Failed to download and parse report.", e);
            }
        }
    }
}
