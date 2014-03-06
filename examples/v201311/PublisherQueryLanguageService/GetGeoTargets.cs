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
using Google.Api.Ads.Dfp.Util.v201311;
using Google.Api.Ads.Dfp.v201311;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.Dfp.Examples.v201311 {
  /// <summary>
  /// This code example gets geographic criteria from the Geo_Target table,
  /// such as all cities available to target. Other types include 'Country',
  /// 'Region', 'State', 'Postal_Code', and 'DMA_Region' (i.e. Metro).
  /// A full list of available geo target types can be found at
  /// https://developers.google.com/doubleclick-publishers/docs/reference/v201311/PublisherQueryLanguageService
  ///
  /// Tags: PublisherQueryLanguageService.select
  /// </summary>
  class GetGeoTargets : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets geographic criteria from the Geo_Target table, such as " +
            "all cities available to target. Other types include 'Country', 'Region', 'State', " +
            "'Postal_Code', and 'DMA_Region' (i.e. Metro). A full list of available geo target " +
            "types can be found at https://developers.google.com/doubleclick-publishers/docs/reference/v201311/PublisherQueryLanguageService";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetGeoTargets();
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
              DfpService.v201311.PublisherQueryLanguageService);

      string geoType = "City";

      int pageSize = 500;
      // Create statement to select all targetable cities.
      String selectStatement = "SELECT Id, Name, CanonicalParentId, ParentIds, CountryCode from " +
          "Geo_Target where Type = :type and Targetable = true order by CountryCode ASC, " +
          "Name ASC limit " + pageSize;

      Statement statement = new StatementBuilder(selectStatement).AddValue("type", geoType)
          .ToStatement();

      int offset = 0;
      int resultSetSize = 0;
      List<Row> allRows = new List<Row>();
      ResultSet resultSet;

      try {
        do {
          statement.query = selectStatement + " OFFSET " + offset;

          // Get all cities.
          resultSet = pqlService.select(statement);

          // Collect all cities from each page.
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
        file.Write(geoType + "_" + this.GetTimeStamp() + ".csv");
      } catch (Exception ex) {
        Console.WriteLine("Failed to get Geo type = '{0}'. Exception says \"{1}\"",
            geoType, ex.Message);
      }
    }
  }
}
