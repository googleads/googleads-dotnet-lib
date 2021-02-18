// Copyright 2020 Google LLC
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

using Google.Api.Ads.Common.Util;
using Google.Api.Ads.AdManager.Lib;
using Google.Api.Ads.AdManager.Util.v202102;
using Google.Api.Ads.AdManager.v202102;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v202102
{
    /// <summary>
    /// This example gets Multiple Customer Management earning information for the prior month.
    /// </summary>
    public class GetMcmEarnings : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This example gets Multiple Customer Management earning information for the "
                + "prior month.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            GetMcmEarnings codeExample = new GetMcmEarnings();
            Console.WriteLine(codeExample.Description);
            codeExample.Run(new AdManagerUser());
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (PublisherQueryLanguageService pqlService =
                user.GetService<PublisherQueryLanguageService>())
            {

                // First day of last month.
                System.DateTime lastMonth = System.DateTime.Now
                    .AddDays(1 - System.DateTime.Now.Day)
                    .AddMonths(-1);

                // Create statement to select MCM earnings for the prior month.
                StatementBuilder statementBuilder = new StatementBuilder()
                    .Select("Month, ChildName, ChildNetworkCode, TotalEarningsCurrencyCode,"
                        + " TotalEarningsMicros, ParentPaymentCurrencyCode, ParentPaymentMicros,"
                        + " ChildPaymentCurrencyCode, ChildPaymentMicros, DeductionsMicros")
                    .From("Mcm_Earnings")
                    .Where("Month = :month")
                    .OrderBy("ChildNetworkCode")
                    .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT)
                    .AddValue("month",
                        DateTimeUtilities.FromDateTime(lastMonth, "America/New_York").date);

                int resultSetSize = 0;
                List<Row> allRows = new List<Row>();
                ResultSet resultSet;

                try
                {
                    do
                    {
                        // Get earnings information.
                        resultSet = pqlService.select(statementBuilder.ToStatement());

                        // Collect all data from each page.
                        allRows.AddRange(resultSet.rows);

                        // Display results.
                        Console.WriteLine(PqlUtilities.ResultSetToString(resultSet));

                        statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
                        resultSetSize = resultSet.rows == null ? 0 : resultSet.rows.Length;
                    } while (resultSetSize == StatementBuilder.SUGGESTED_PAGE_LIMIT);

                    Console.WriteLine("Number of results found: " + allRows.Count);

                    // Optionally, save all rows to a CSV.
                    // Get a string array representation of the data rows.
                    resultSet.rows = allRows.ToArray();
                    List<String[]> rows = PqlUtilities.ResultSetToStringArrayList(resultSet);

                    // Write the contents to a csv file.
                    CsvFile file = new CsvFile();
                    file.Headers.AddRange(rows[0]);
                    file.Records.AddRange(rows.GetRange(1, rows.Count - 1).ToArray());
                    file.Write("Earnings_Report_" + this.GetTimeStamp() + ".csv");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to get MCM earnings. Exception says \"{0}\"",
                        e.Message);
                }
            }
        }
    }
}
