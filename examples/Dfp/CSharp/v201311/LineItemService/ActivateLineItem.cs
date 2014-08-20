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

using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.Util.v201311;
using Google.Api.Ads.Dfp.v201311;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201311 {
  /// <summary>
  /// This code example activates all line items for the given order. To be
  /// activated, line items need to be in the approved (needs creatives) state
  /// and have at least one creative associated with them. To approve line
  /// items, approve the order to which they belong by running ApproveOrders.cs.
  /// To create LICAs, run CreateLicas.cs. To determine which line items exist,
  /// run GetAllLineItem.cs.
  ///
  /// Tags: LineItemService.getLineItemsByStatement
  /// Tags: LineItemService.performLineItemAction
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
          (LineItemService) user.GetService(DfpService.v201311.LineItemService);

      // Set the ID of the order to get line items from.
      long orderId = long.Parse(_T("INSERT_ORDER_ID_HERE"));

      // Create Statement text to select approved line items from a given order.
      string statementText = "WHERE orderId = :orderId and status = :status LIMIT 500";
      Statement statement = new StatementBuilder("").AddValue("orderId", orderId).
          AddValue("status", ComputedStatus.NEEDS_CREATIVES.ToString()).ToStatement();

      // Set defaults for page and offset.
      LineItemPage page = new LineItemPage();
      int offset = 0;
      int i = 0;
      List<string> lineItemIds = new List<string>();

      try {
        do {
          // Create a Statement to page through approved line items.
          statement.query = string.Format("{0} OFFSET {1}", statementText, offset);

          // Get line items by Statement.
          page = lineItemService.getLineItemsByStatement(statement);

          if (page.results != null && page.results.Length > 0) {
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

          offset += 500;
        } while (offset < page.totalResultSetSize);

        Console.WriteLine("Number of line items to be activated: {0}", lineItemIds.Count);

        if (lineItemIds.Count > 0) {
          // Create action Statement.
          statement = new StatementBuilder(
              string.Format("WHERE id IN ({0})", string.Join(",", lineItemIds.ToArray()))).
              ToStatement();

          // Create action.
          ActivateLineItems action = new ActivateLineItems();

          // Perform action.
          UpdateResult result = lineItemService.performLineItemAction(action, statement);

          // Display results.
          if (result != null && result.numChanges > 0) {
            Console.WriteLine("Number of line items activated: {0}", result.numChanges);
          } else {
            Console.WriteLine("No line items were activated.");
          }
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to activate line items. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
