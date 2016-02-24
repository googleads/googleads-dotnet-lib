// Copyright 2015, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.Util.v201602;
using Google.Api.Ads.Dfp.v201602;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201602 {
  /// <summary>
  /// This code example fetches and creates match table files from the
  /// Line_Item and Ad_Unit tables. This example may take a while to run.
  /// </summary>
  class FetchMatchTables : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example fetches and creates match table files from the Line_Item " +
            "and Ad_Unit tables. This example may take a while to run.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new FetchMatchTables();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the PublisherQueryLanguageService.
      PublisherQueryLanguageService pqlService =
          (PublisherQueryLanguageService) user.GetService(
              DfpService.v201602.PublisherQueryLanguageService);

      try {
        StatementBuilder lineItemStatementBuilder = new StatementBuilder()
            .Select("Id, Name, Status")
            .From("Line_Item")
            .OrderBy("Id ASC")
            .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT);
        string lineItemFilePath = "Line-Item-Matchtable.csv";
        fetchMatchTables(pqlService, lineItemStatementBuilder, lineItemFilePath);

        StatementBuilder adUnitStatementBuilder = new StatementBuilder()
            .Select("Id, Name")
            .From("Ad_Unit")
            .OrderBy("Id ASC")
            .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT);
        string adUnitFilePath = "Ad-Unit-Matchtable.csv";
        fetchMatchTables(pqlService, adUnitStatementBuilder, adUnitFilePath);

        Console.WriteLine("Ad units saved to %s", adUnitFilePath);
        Console.WriteLine("Line items saved to %s\n", lineItemFilePath);
      } catch (Exception e) {
        Console.WriteLine("Failed to get match tables. Exception says \"{0}\"", e.Message);
      }
    }

    /// <summary>
    /// Fetches a match table from a PQL statement and writes it to a file.
    /// </summary>
    /// <param name="pqlService">The PQL service.</param>
    /// <param name="selectStatement">The select statement.</param>
    /// <param name="fileName">Name of the file.</param>
    private static void fetchMatchTables(PublisherQueryLanguageService pqlService,
        StatementBuilder statementBuilder, string fileName) {

      int resultSetSize = 0;
      List<Row> allRows = new List<Row>();
      ResultSet resultSet;

      do {
        resultSet = pqlService.select(statementBuilder.ToStatement());
        allRows.AddRange(resultSet.rows);
        Console.WriteLine(PqlUtilities.ResultSetToString(resultSet));

        statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
        resultSetSize = resultSet.rows == null ? 0 : resultSet.rows.Length;
      } while (resultSetSize == StatementBuilder.SUGGESTED_PAGE_LIMIT);

      resultSet.rows = allRows.ToArray();
      List<String[]> rows = PqlUtilities.ResultSetToStringArrayList(resultSet);

      // Write the contents to a csv file.
      CsvFile file = new CsvFile();
      file.Headers.AddRange(rows[0]);
      file.Records.AddRange(rows.GetRange(1, rows.Count - 1).ToArray());
      file.Write(fileName);
    }
  }
}
