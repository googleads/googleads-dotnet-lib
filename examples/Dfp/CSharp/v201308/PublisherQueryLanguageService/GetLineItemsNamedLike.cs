// Copyright 2013, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.Common.Util;
using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.Util.v201308;
using Google.Api.Ads.Dfp.v201308;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201308 {
  /// <summary>
  /// This code example gets all line items which have a name beginning with
  /// "line item". This code example may take a while to run.
  ///
  /// Tags: PublisherQueryLanguageService.select
  /// </summary>
  class GetLineItemsNamedLike : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets all line items which have a name beginning with " +
            "'line item'. This code example may take a while to run.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetLineItemsNamedLike();
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
              DfpService.v201308.PublisherQueryLanguageService);

      int pageSize = 500;

      // Create statement to select all line items.
      String selectStatement = "SELECT Id, Name, Status FROM Line_Item where Name LIKE " +
            "'line item%' order by Id ASC LIMIT " + pageSize;
      int offset = 0;
      int resultSetSize = 0;
      List<Row> allRows = new List<Row>();
      ResultSet resultSet;

      try {
        do {
          StatementBuilder statementBuilder =
              new StatementBuilder(selectStatement + " OFFSET " + offset);

          // Get line items like 'line item%'.
          resultSet = pqlService.select(statementBuilder.ToStatement());

          // Collect all line items from each page.
          allRows.AddRange(resultSet.rows);

          // Display results.
          Console.WriteLine(PqlUtilities.ResultSetToString(resultSet));

          offset += pageSize;
          resultSetSize = resultSet.rows == null ? 0 : resultSet.rows.Length;
        } while (resultSetSize == pageSize);

        Console.WriteLine("Number of results found: " + allRows.Count);

        // Optionally, save all rows to a CSV.
        // Get a string array representation of the data rows.
        resultSet.rows = allRows.ToArray();
        List<String[]> rows = PqlUtilities.ResultSetToStringArrayList(resultSet);

        // Write the contents to a csv file.
        CsvFile file = new CsvFile();
        file.Headers.AddRange(rows[0]);
        file.Records.AddRange(rows.GetRange(1, rows.Count - 1).ToArray());
        file.Write("line_items_named_like_" + GetTimeStamp() + ".csv");
      } catch (Exception ex) {
        Console.WriteLine("Failed to get line items. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
