// Copyright 2014, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.Dfp.v201403;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.Dfp.Examples.v201403 {
  /// <summary>
  /// This code example updates the notes of each order up to the first 500.
  /// To determine which orders exist, run GetAllOrders.cs.
  ///
  /// Tags: OrderService.getOrdersByStatement, OrderService.updateOrders
  /// </summary>
  class UpdateOrders : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example updates the notes of each order up to the first 500. " +
            "To determine which orders exist, run GetAllOrders.cs.";
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
      OrderService orderService = (OrderService) user.GetService(DfpService.v201403.OrderService);

      // Create a Statement to get all orders.
      Statement statement = new Statement();
      statement.query = "LIMIT 500";

      try {
        // Get orders by Statement.
        OrderPage page = orderService.getOrdersByStatement(statement);

        if (page.results != null && page.results.Length > 0) {
          Order[] orders = page.results;
          List<Order> ordersToUpdate = new List<Order>();

          // Update each local order object by changing its notes.
          foreach (Order order in orders) {
            if (!order.isArchived) {
              order.notes = "Spoke to advertiser. All is well.";
              ordersToUpdate.Add(order);
            }
          }

          // Update the orders on the server.
          orders = orderService.updateOrders(ordersToUpdate.ToArray());

          if (orders != null) {
            foreach (Order order in orders) {
              Console.WriteLine("Order with ID = '{0}', name = '{1}', advertiser ID = '{2}', " +
                  "and notes = '{3}' was updated.", order.id, order.name, order.advertiserId,
                  order.notes);
            }
          } else {
            Console.WriteLine("No orders updated.");
          }
        } else {
          Console.WriteLine("No orders found to update.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to update orders. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
