// Copyright 2011, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.Dfp.v201111;

using System;

namespace Google.Api.Ads.Dfp.Examples.v201111 {
  /// <summary>
  /// This code example gets an order by its ID. To determine which orders
  /// exist, run GetAllOrders.cs.
  ///
  /// Tags: OrderService.getOrder
  /// </summary>
  class GetOrder : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets an order by its ID. To determine which orders " +
            "exist, run GetAllOrders.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      new GetOrder().Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the OrderService.
      OrderService orderService = (OrderService) user.GetService(DfpService.v201111.OrderService);

      // Set the ID of the order to get.
      long orderId = long.Parse(_T("INSERT_ORDER_ID_HERE"));

      try {
        // Get the order.
        Order order = orderService.getOrder(orderId);

        if (order != null) {
          Console.WriteLine("An order with ID = {0}', name = '{1}', and advertiser ID = '{2}' " +
              " was found.", order.id, order.name, order.advertiserId);
        } else {
          Console.WriteLine("No order found for this ID.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to get orders by ID. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
