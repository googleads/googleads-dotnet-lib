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
  /// This code example updates the note of an order. To determine which orders exist,
  /// run GetAllOrders.cs.
  /// </summary>
  class UpdateOrders : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example updates the note of an order. To determine which orders " +
            "exist, run GetAllOrders.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new UpdateOrders();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the OrderService.
      OrderService orderService = (OrderService) user.GetService(DfpService.v201602.OrderService);

      long orderId = long.Parse(_T("INSERT_ORDER_ID_HERE"));

      // Create a statement to get the order.
      StatementBuilder statementBuilder = new StatementBuilder()
          .Where("id = :id")
          .OrderBy("id ASC")
          .Limit(1)
          .AddValue("id", orderId);

      try {
        // Get orders by statement.
        OrderPage page = orderService.getOrdersByStatement(statementBuilder.ToStatement());

        Order order = page.results[0];

        // Update the order object by changing its note.
        order.notes = "Spoke to advertiser. All is well.";

        // Update the orders on the server.
        Order[] orders = orderService.updateOrders(new Order[] {order});

        if (orders != null) {
          foreach (Order updatedOrder in orders) {
            Console.WriteLine("Order with ID = '{0}', name = '{1}', advertiser ID = '{2}', " +
                "and notes = '{3}' was updated.", updatedOrder.id, updatedOrder.name,
                updatedOrder.advertiserId, updatedOrder.notes);
          }
        } else {
          Console.WriteLine("No orders updated.");
        }
      } catch (Exception e) {
        Console.WriteLine("Failed to update orders. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}
