// Copyright 2016, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.Dfp.Util.v201608;
using Google.Api.Ads.Dfp.v201608;

using System;
namespace Google.Api.Ads.Dfp.Examples.CSharp.v201608 {
  /// <summary>
  /// This example gets all line item creative associations for a given line item.
  /// </summary>
  public class GetLicasForLineItem : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This example gets all line item creative associations for a given line item.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main() {
      GetLicasForLineItem codeExample = new GetLicasForLineItem();
      Console.WriteLine(codeExample.Description);

      long lineItemId = long.Parse(_T("INSERT_LINE_ITEM_ID_HERE"));
      codeExample.Run(new DfpUser(), lineItemId);
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public void Run(DfpUser user, long lineItemId) {
      LineItemCreativeAssociationService lineItemCreativeAssociationService =
          (LineItemCreativeAssociationService) user.GetService(
          DfpService.v201608.LineItemCreativeAssociationService);

      // Create a statement to select line item creative associations.
      StatementBuilder statementBuilder = new StatementBuilder()
          .Where("lineItemId = :lineItemId")
          .OrderBy("lineItemId ASC, creativeId ASC")
          .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT)
          .AddValue("lineItemId", lineItemId);

      // Retrieve a small amount of line item creative associations at a time, paging through
      // until all line item creative associations have been retrieved.
      LineItemCreativeAssociationPage page = new LineItemCreativeAssociationPage();
      try {
        do {
          page = lineItemCreativeAssociationService.getLineItemCreativeAssociationsByStatement(
              statementBuilder.ToStatement());

          if (page.results != null) {
            // Print out some information for each line item creative association.
            int i = page.startIndex;
            foreach (LineItemCreativeAssociation lineItemCreativeAssociation in page.results) {
              if (lineItemCreativeAssociation.creativeSetIdSpecified) {
                Console.WriteLine("{0}) Line item creative association with line item ID \"{1}\" "
                    + "and creative set ID \"{2}\" was found.",
                    i++,
                    lineItemCreativeAssociation.lineItemId,
                    lineItemCreativeAssociation.creativeSetId);
              } else {
                Console.WriteLine("{0}) Line item creative association with line item ID \"{1}\" "
                    + "and creative ID \"{2}\" was found.",
                    i++,
                    lineItemCreativeAssociation.lineItemId,
                    lineItemCreativeAssociation.creativeId);
              }
            }
          }

          statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
        } while (statementBuilder.GetOffset() < page.totalResultSetSize);

        Console.WriteLine("Number of results found: {0}", page.totalResultSetSize);
      } catch (Exception e) {
        Console.WriteLine("Failed to get line item creative associations. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}
