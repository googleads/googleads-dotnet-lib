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

using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.Util.v201602;
using Google.Api.Ads.Dfp.v201602;

using System;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201602 {
  /// <summary>
  /// This code example updates the destination URL of a LICA. To determine which LICAs exist,
  /// run GetAllLicas.cs.
  /// </summary>
  class UpdateLicas : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example updates the destination URL of a LICAs. To determine which " +
            "LICAs exist, run GetAllLicas.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new UpdateLicas();
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
          user.GetService(DfpService.v201602.LineItemCreativeAssociationService);

      // Set the line item to get LICAs by.
      long lineItemId = long.Parse(_T("INSERT_LINE_ITEM_ID_HERE"));

      // Set the creative to get LICAs by.
      long creativeId = long.Parse(_T("INSERT_CREATIVE_ID_HERE"));

      // Create a statement to get all LICAs.
      StatementBuilder statementBuilder = new StatementBuilder()
          .Where("lineItemId = :lineItemId AND creativeId = :creativeId")
          .OrderBy("lineItemId ASC, creativeId ASC")
          .Limit(1)
          .AddValue("lineItemId", lineItemId)
          .AddValue("creativeId", creativeId);

      try {
        // Get LICAs by statement.
        LineItemCreativeAssociationPage page =
            licaService.getLineItemCreativeAssociationsByStatement(statementBuilder.ToStatement());

        LineItemCreativeAssociation lica = page.results[0];

        // Update the LICA object by changing its destination URL.
        lica.destinationUrl = "http://news.google.com";

        // Update the LICA on the server.
        LineItemCreativeAssociation[] licas = licaService.updateLineItemCreativeAssociations(
            new LineItemCreativeAssociation[] {lica});

        if (licas != null) {
          foreach (LineItemCreativeAssociation updatedLica in licas) {
            Console.WriteLine("LICA with line item ID = '{0}, creative ID ='{1}' and " +
                "destination URL '{2}' was updated.", updatedLica.lineItemId,
                updatedLica.creativeId, updatedLica.destinationUrl);
          }
        } else {
          Console.WriteLine("No LICAs updated.");
        }
      } catch (Exception e) {
        Console.WriteLine("Failed to update LICAs. Exception says \"{0}\"", e.Message);
      }
    }
  }
}
