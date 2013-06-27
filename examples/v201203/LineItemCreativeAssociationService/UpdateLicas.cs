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
using Google.Api.Ads.Dfp.v201203;

using System;

namespace Google.Api.Ads.Dfp.Examples.v201203 {
  /// <summary>
  /// This code example updates the destination URL of all LICAs up to the first
  /// 500. To determine which LICAs exist, run GetAllLicas.cs.
  ///
  /// Tags: LineItemCreativeAssociationService.getLineItemCreativeAssociationsByStatement
  /// Tags: LineItemCreativeAssociationService.updateLineItemCreativeAssociations
  /// </summary>
  class UpdateLicas : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example updates the destination URL of all LICAs up to the first " +
            "500. To determine which LICAs exist, run GetAllLicas.cs.";
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
          user.GetService(DfpService.v201203.LineItemCreativeAssociationService);

      // Create a Statement to get all LICAs.
      Statement statement = new Statement();
      statement.query = "LIMIT 500";

      try {
        // Get LICAs by Statement.
        LineItemCreativeAssociationPage page =
            licaService.getLineItemCreativeAssociationsByStatement(statement);

        if (page.results != null && page.results.Length > 0) {
          LineItemCreativeAssociation[] licas = page.results;

          // Update each local LICA object by changing its destination URL.
          foreach (LineItemCreativeAssociation lica in licas) {
            lica.destinationUrl = "http://news.google.com";
          }

          // Update the LICAs on the server.
          licas = licaService.updateLineItemCreativeAssociations(licas);

          if (licas != null) {
            foreach (LineItemCreativeAssociation lica in licas) {
              Console.WriteLine("LICA with line item ID = '{0}, creative ID ='{1}' and " +
                  "destination URL '{2}' was updated.", lica.lineItemId, lica.creativeId,
                  lica.destinationUrl);
            }
          } else {
            Console.WriteLine("No LICAs updated.");
          }
        } else {
          Console.WriteLine("No LICAs found to update.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to update LICAs. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
