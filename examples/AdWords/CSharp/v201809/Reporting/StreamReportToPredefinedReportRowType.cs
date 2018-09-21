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
    /// This code example streams the results of an ad hoc report, and
    /// returns the data in the report as objects of a predefined report row type.
    /// </summary>
    public class StreamReportToPredefinedReportRowType : ExampleBase
    {
        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            StreamReportToPredefinedReportRowType codeExample =
                new StreamReportToPredefinedReportRowType();
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
                    "returns the data in the report as objects of a predefined report row type.";
            }
        }

        /// <summary>
        /// Runs the code example.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        public void Run(AdWordsUser user)
        {
            // Retreiving the raw values of enum-type fields instead of display values
            (user.Config as AdWordsAppConfig).UseRawEnumValues = true;

            // Create the query.
            string query =
                "SELECT AccountCurrencyCode, AccountDescriptiveName FROM FINAL_URL_REPORT " +
                "DURING LAST_7_DAYS";

            ReportUtilities reportUtilities = new ReportUtilities(user, "v201809", query,
                DownloadFormat.GZIPPED_XML.ToString());

            try
            {
                using (ReportResponse response = reportUtilities.GetResponse())
                {
                    using (GZipStream gzipStream =
                        new GZipStream(response.Stream, CompressionMode.Decompress))
                    {
                        // Create the report object using the stream.
                        using (var report =
                            new AwReport<FinalUrlReportReportRow>(new AwXmlTextReader(gzipStream),
                                "Example"))
                        {
                            // Print the contents of each row object.
                            while (report.MoveNext())
                            {
                                Console.WriteLine(report.Current.accountCurrencyCode + " " +
                                    report.Current.accountDescriptiveName);
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
