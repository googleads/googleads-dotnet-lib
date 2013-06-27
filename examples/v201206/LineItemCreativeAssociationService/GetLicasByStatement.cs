// Copyright 2012, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.Util.v201206;
using Google.Api.Ads.Dfp.v201206;

using System;

namespace Google.Api.Ads.Dfp.Examples.v201206 {
  /// <summary>
  /// This code example gets all line item creative associations for a given
  /// line item ID. The Statement retrieves up to the maximum page size limit of
  /// 500. To create LICAs, run CreateLicas.cs.
  ///
  /// Tags: LineItemCreativeAssociationService.getLineItemCreativeAssociationsByStatement
  /// </summary>
  class GetLicasByStatement : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets all line item creative associations for a given line " +
            "item ID. The Statement retrieves up to the maximum page size limit of 500. To " +
            "create LICAs, run CreateLicas.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetLicasByStatement();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the LineItemCreativeAssociationService.
      LineItemCreativeAssociationService licaService = (LineItemCreativeAssociationService)
          user.GetService(DfpService.v201206.LineItemCreativeAssociationService);

      // Set the line item to get LICAs by.
      long lineItemId = long.Parse(_T("INSERT_LINE_ITEM_ID_HERE"));

      // Create a Statement to only select LICAs for the given lineItem ID.
      Statement statement = new StatementBuilder("WHERE lineItemId = :lineItemId LIMIT 500").
          AddValue("lineItemId", lineItemId).ToStatement();

      try {
        // Get LICAs by Statement.
        LineItemCreativeAssociationPage page =
            licaService.getLineItemCreativeAssociationsByStatement(statement);

        if (page.results != null && page.results.Length > 0) {
          int i = page.startIndex;
          foreach (LineItemCreativeAssociation lica in page.results) {
            Console.WriteLine("{0}) LICA with line item ID = '{1}', creative ID ='{2}' and " +
                "status ='{3}' was found.", i, lica.lineItemId, lica.creativeId,
                lica.status);
            i++;
          }
        }
        Console.WriteLine("Number of results found: {0}", page.totalResultSetSize);
      } catch (Exception ex) {
        Console.WriteLine("Failed to get LICAs. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
