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
using Google.Api.Ads.Dfp.Util.v201208;
using Google.Api.Ads.Dfp.v201208;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.Dfp.Examples.v201208 {
  /// <summary>
  /// This code example updates the delivery rate of all line items up to the
  /// first 500. To determine which line items exist, run GetAllLineItems.cs.
  ///
  /// Tags: LineItemService.getLineItemsByStatement
  /// Tags: LineItemService.updateLineItems
  /// </summary>
  class UpdateLineItems : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example updates the delivery rate of all line items up to the first " +
            "500. To determine which line items exist, run GetAllLineItems.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new UpdateLineItems();
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
          (LineItemService) user.GetService(DfpService.v201208.LineItemService);

      // Set the ID of the order to get line items from.
      long orderId = long.Parse(_T("INSERT_ORDER_ID_HERE"));

      // Create a Statement to get line items with even delivery rates.
      Statement statement = new StatementBuilder("WHERE deliveryRateType = :deliveryRateType and " +
          "orderId = :orderId LIMIT 500").AddValue("deliveryRateType",
              DeliveryRateType.EVENLY.ToString()).AddValue("orderId", orderId).ToStatement();

      try {
        // Get line items by Statement.
        LineItemPage page = lineItemService.getLineItemsByStatement(statement);

        if (page.results != null && page.results.Length > 0) {
          LineItem[] lineItems = page.results;
          List<LineItem> lineItemsToUpdate = new List<LineItem>();

          // Update each local line item object by changing its delivery rate.
          foreach (LineItem lineItem in lineItems) {
            // Archived line items cannot be updated.
            if (!lineItem.isArchived) {
              lineItem.deliveryRateType = DeliveryRateType.AS_FAST_AS_POSSIBLE;
              lineItemsToUpdate.Add(lineItem);
            }
          }

          // Update the line items on the server.
          lineItems = lineItemService.updateLineItems(lineItemsToUpdate.ToArray());

          if (lineItems != null) {
            foreach (LineItem lineItem in lineItems) {
              Console.WriteLine("A line item with ID = '{0}', belonging to order ID = '{1}', " +
                  "named '{2}', and having delivery rate = '{3}' was updated.",
                  lineItem.id, lineItem.orderId, lineItem.name, lineItem.deliveryRateType);
            }
          } else {
            Console.WriteLine("No line items updated.");
          }
        } else {
          Console.WriteLine("No line items found to update.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to update line items. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
