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
  /// This code example updates the delivery rate of a line items.
  /// To determine which line items exist, run GetAllLineItems.cs.
  /// </summary>
  class UpdateLineItems : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example updates the delivery rate of a line item. To determine which " +
          "line items exist, run GetAllLineItems.cs.";
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
          (LineItemService) user.GetService(DfpService.v201602.LineItemService);

      // Set the ID of the line item.
      long lineItemId = long.Parse(_T("INSERT_LINE_ITEM_ID_HERE"));

      // Create a statement to get the line item.
      StatementBuilder statementBuilder = new StatementBuilder()
          .Where("id = :lineItemId")
          .OrderBy("id ASC")
          .Limit(1)
          .AddValue("lineItemId", lineItemId);

      try {
        // Get line items by statement.
        LineItemPage page = lineItemService.getLineItemsByStatement(statementBuilder.ToStatement());

        LineItem lineItem = page.results[0];

        // Update line item object by changing its delivery rate.
       lineItem.deliveryRateType = DeliveryRateType.AS_FAST_AS_POSSIBLE;

        // Update the line item on the server.
        LineItem[] lineItems = lineItemService.updateLineItems(new LineItem[] {lineItem});

        if (lineItems != null) {
          foreach (LineItem updatedLineItem in lineItems) {
            Console.WriteLine("A line item with ID = '{0}', belonging to order ID = '{1}', " +
                "named '{2}', and having delivery rate = '{3}' was updated.",
                updatedLineItem.id, updatedLineItem.orderId, updatedLineItem.name,
                updatedLineItem.deliveryRateType);
          }
        } else {
          Console.WriteLine("No line items updated.");
        }
      } catch (Exception e) {
        Console.WriteLine("Failed to update line items. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}
