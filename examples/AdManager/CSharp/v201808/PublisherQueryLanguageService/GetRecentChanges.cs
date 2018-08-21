// Copyright 2017, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdManager.Util.v201808;
using Google.Api.Ads.AdManager.v201808;

using DateTime = Google.Api.Ads.AdManager.v201808.DateTime;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v201808
{
    /// <summary>
    /// This example gets recent changes in your network using the Change_History table. 
    ///
    /// A full list of available tables can be found at
    /// https://developers.google.com/doubleclick-publishers/docs/reference/v201808/PublisherQueryLanguageService
    /// </summary>
    public class GetRecentChanges : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This example gets recent changes in your network using the " +
                    "Change_History table. A full list of available tables can be found at " +
                    "https://developers.google.com/doubleclick-publishers/docs/reference/v201808/" +
                    "PublisherQueryLanguageService";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            GetRecentChanges codeExample = new GetRecentChanges();
            Console.WriteLine(codeExample.Description);
            try
            {
                codeExample.Run(new AdManagerUser());
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to get recent changes. Exception says \"{0}\"",
                    e.Message);
            }
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (PublisherQueryLanguageService pqlService =
                user.GetService<PublisherQueryLanguageService>())
            {
                // Create statement to select recent changes. Change_History only supports ordering
                // by descending ChangeDateTime. Offset is not supported. To page, use the change ID
                // of the earliest change as a pagination token. A date time range is required when
                // querying this table.
                System.DateTime endDateTime = System.DateTime.Now;
                System.DateTime startDateTime = endDateTime.AddDays(-1);

                StatementBuilder statementBuilder = new StatementBuilder()
                    .Select("Id, ChangeDateTime, EntityId, EntityType, Operation, UserId")
                    .From("Change_History")
                    .Where("ChangeDateTime < :endDateTime AND ChangeDateTime > :startDateTime")
                    .OrderBy("ChangeDateTime DESC")
                    .AddValue("startDateTime",
                        DateTimeUtilities.FromDateTime(startDateTime, "America/New_York"))
                    .AddValue("endDateTime",
                        DateTimeUtilities.FromDateTime(endDateTime, "America/New_York"))
                    .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT);

                int resultSetSize = 0;
                List<Row> allRows = new List<Row>();
                ResultSet resultSet;

                do
                {
                    resultSet = pqlService.select(statementBuilder.ToStatement());

                    if (resultSet.rows != null && resultSet.rows.Length > 0)
                    {
                        // Get the earliest change ID in the result set.
                        Row lastRow = resultSet.rows[resultSet.rows.Length - 1];
                        string lastId = (string) PqlUtilities.GetValue(lastRow.values[0]);

                        // Collect all changes from each page.
                        allRows.AddRange(resultSet.rows);

                        // Display results.
                        Console.WriteLine(PqlUtilities.ResultSetToString(resultSet));

                        // Use the earliest change ID in the result set to page.
                        statementBuilder
                            .Where("Id < :id AND ChangeDateTime < :endDateTime AND " +
                                "ChangeDateTime > :startDateTime").AddValue("id", lastId);
                    }

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
                file.Write("recent_changes_" + this.GetTimeStamp() + ".csv");
            }
        }
    }
}
