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
using System.Collections.Generic;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201602 {
  /// <summary>
  /// This code example activates all line items for the given order. To be
  /// activated, line items need to be in the approved (needs creatives) state
  /// and have at least one creative associated with them. To approve line
  /// items, approve the order to which they belong by running ApproveOrders.cs.
  /// To create LICAs, run CreateLicas.cs. To determine which line items exist,
  /// run GetAllLineItem.cs.
  /// </summary>
  class ActivateLineItem : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example activates all line items for the given order. To be activated," +
            " line items need to be in the approved (needs creatives) state and have at least one" +
            " creative associated with them. To approve line items, approve the order to which" +
            " they belong by running ApproveOrders.cs. To create LICAs, run CreateLicas.cs." +
            " To determine which line items exist, run GetAllLineItem.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new ActivateLineItem();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the LineItemService.
      LineItemService lineItemService =
          (LineItemService) user.GetService(DfpService.v201602.LineItemService);

      // Set the ID of the order to get line items from.
      long orderId = long.Parse(_T("INSERT_ORDER_ID_HERE"));

      // Create statement to select approved line items from a given order.
      StatementBuilder statementBuilder = new StatementBuilder()
          .Where("orderId = :orderId and status = :status")
          .AddValue("orderId", orderId)
          .AddValue("status", ComputedStatus.INACTIVE.ToString());

      // Set default for page.
      LineItemPage page = new LineItemPage();
      List<string> lineItemIds = new List<string>();

      try {
        do {
          // Get line items by statement.
          page = lineItemService.getLineItemsByStatement(statementBuilder.ToStatement());

          if (page.results != null && page.results.Length > 0) {
            int i = page.startIndex;
            foreach (LineItemSummary lineItem in page.results) {
              // Archived line items cannot be activated.
              if (!lineItem.isArchived) {
                Console.WriteLine("{0}) Line item with ID ='{1}', belonging to order ID ='{2}' " +
                    "and name ='{2}' will be activated.", i, lineItem.id, lineItem.orderId,
                    lineItem.name);
                lineItemIds.Add(lineItem.id.ToString());
                i++;
              }
            }
          }

          statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
        } while (statementBuilder.GetOffset() < page.totalResultSetSize);


        Console.WriteLine("Number of line items to be activated: {0}", lineItemIds.Count);

        if (lineItemIds.Count > 0) {
          // Modify statement.
          statementBuilder.RemoveLimitAndOffset();

          // Create action.
          ActivateLineItems action = new ActivateLineItems();

          // Perform action.
          UpdateResult result = lineItemService.performLineItemAction(action,
              statementBuilder.ToStatement());

          // Display results.
          if (result != null && result.numChanges > 0) {
            Console.WriteLine("Number of line items activated: {0}", result.numChanges);
          } else {
            Console.WriteLine("No line items were activated.");
          }
        }
      } catch (Exception e) {
        Console.WriteLine("Failed to activate line items. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}
