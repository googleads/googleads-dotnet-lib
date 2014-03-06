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
using Google.Api.Ads.Dfp.Util.v201403;
using Google.Api.Ads.Dfp.v201403;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.Dfp.Examples.v201403 {
  /// <summary>
  /// This code example approves and overbooks all eligible draft and pending
  /// orders. To determine which orders exist, run GetAllOrders.cs.
  ///
  /// Tags: OrderService.getOrdersByStatement, OrderService.performOrderAction
  /// </summary>
  class ApproveOrder : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example approves and overbooks all draft orders. To determine which " +
            "orders exist, run GetAllOrders.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new ApproveOrder();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the OrderService.
      OrderService orderService =
          (OrderService) user.GetService(DfpService.v201403.OrderService);

      // Create Statement text to select all draft orders.
      string statementText = "WHERE status IN (:status1, :status2) and endDateTime >= :today " +
          "AND isArchived = FALSE LIMIT 500";
      Statement statement = new StatementBuilder("").
          AddValue("status1", OrderStatus.DRAFT.ToString()).
          AddValue("status2", OrderStatus.PENDING_APPROVAL.ToString()).
          AddValue("today", System.DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")).ToStatement();

      // Set defaults for page and offset.
      OrderPage page = new OrderPage();
      int i = 0;
      int offset = 0;
      List<string> orderIds = new List<string>();

      try {
        do {
          // Create a Statement to page through draft orders.
          statement.query = string.Format("{0} OFFSET {1}", statementText, offset);
          // Get orders by Statement.
          page = orderService.getOrdersByStatement(statement);

          if (page.results != null && page.results.Length > 0) {
            foreach (Order order in page.results) {
              Console.WriteLine("{0}) Order with ID = '{1}', name = '{2}', and status ='{3}' " +
                  "will be approved.", i, order.id, order.name, order.status);
              orderIds.Add(order.id.ToString());
              i++;
            }
          }

          offset += 500;
        } while (offset < page.totalResultSetSize);

        Console.WriteLine("Number of orders to be approved: {0}", orderIds.Count);

        if (orderIds.Count > 0) {
          // Create action Statement.
          statement = new StatementBuilder(
              string.Format("WHERE id IN ({0})", string.Join(",", orderIds.ToArray()))).
              ToStatement();

          // Create action.
          ApproveAndOverbookOrders action = new ApproveAndOverbookOrders();

          // Perform action.
          UpdateResult result = orderService.performOrderAction(action, statement);

          // Display results.
          if (result != null && result.numChanges > 0) {
            Console.WriteLine("Number of orders approved: {0}", result.numChanges);
          } else {
            Console.WriteLine("No orders were approved.");
          }
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to approve orders. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
