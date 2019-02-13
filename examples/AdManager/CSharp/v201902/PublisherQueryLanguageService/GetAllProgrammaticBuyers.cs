// Copyright 2018, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdManager.Util.v201902;
using Google.Api.Ads.AdManager.v201902;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v201902
{
    /// <summary>
    /// This example gets all programmatic buyers in your network using the
    /// Programmatic_Buyer table.
    /// </summary>
    public class GetAllProgrammaticBuyers : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This example gets all programmatic buyers in your network using the " +
                    "Programmatic_Buyer table.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            GetAllProgrammaticBuyers codeExample = new GetAllProgrammaticBuyers();
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
                // Create statement to select all programmatic buyers.
                StatementBuilder statementBuilder = new StatementBuilder()
                    .Select("BuyerAccountId, Name").From("Programmatic_Buyer")
                    .OrderBy("BuyerAccountId ASC")
                    .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT);

                int resultSetSize = 0;
                List<Row> allRows = new List<Row>();
                ResultSet resultSet;

                try
                {
                    do
                    {
                        // Get programmatic buyers.
                        resultSet = pqlService.select(statementBuilder.ToStatement());

                        // Collect all programmatic buyers from each page.
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
                    file.Write("Programmatic_Buyers_" + this.GetTimeStamp() + ".csv");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to get programmatic buyers. Exception says \"{0}\"",
                        e.Message);
                }
            }
        }
    }
}
