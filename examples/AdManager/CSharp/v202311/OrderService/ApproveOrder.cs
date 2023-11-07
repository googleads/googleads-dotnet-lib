// Copyright 2019 Google LLC
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

using Google.Api.Ads.AdManager.Lib;
using Google.Api.Ads.AdManager.Util.v202311;
using Google.Api.Ads.AdManager.v202311;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v202311
{
    /// <summary>
    /// This code example approves an order and all line items belonging to that order. To determine
    /// which orders exist, run GetAllOrders.cs.
    /// </summary>
    public class ApproveOrder : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example approves an order and all line items belonging to that" +
                    " order. To determine which orders exist, run GetAllOrders.cs.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            ApproveOrder codeExample = new ApproveOrder();
            Console.WriteLine(codeExample.Description);
            codeExample.Run(new AdManagerUser());
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (OrderService orderService = user.GetService<OrderService>())
            {
                // Set the ID of the order.
                long orderId = long.Parse(_T("INSERT_ORDER_ID_HERE"));

                // Create statement to select the order.
                StatementBuilder statementBuilder = new StatementBuilder()
                    .Where("id = :id")
                    .OrderBy("id ASC")
                    .Limit(1)
                    .AddValue("id", orderId);

                // Set default for page.
                OrderPage page = new OrderPage();
                List<string> orderIds = new List<string>();
                int i = 0;

                try
                {
                    do
                    {
                        // Get orders by statement.
                        page = orderService.getOrdersByStatement(statementBuilder.ToStatement());

                        if (page.results != null)
                        {
                            foreach (Order order in page.results)
                            {
                                Console.WriteLine(
                                    "{0}) Order with ID = '{1}', name = '{2}', and status ='{3}' " +
                                    "will be approved.", i, order.id, order.name, order.status);
                                orderIds.Add(order.id.ToString());
                                i++;
                            }
                        }

                        statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
                    } while (statementBuilder.GetOffset() < page.totalResultSetSize);

                    Console.WriteLine("Number of orders to be approved: {0}", orderIds.Count);

                    if (orderIds.Count > 0)
                    {
                        // Modify statement for action.
                        statementBuilder.RemoveLimitAndOffset();

                        // Create action.
                        ApproveAndOverbookOrders action = new ApproveAndOverbookOrders();

                        // Perform action.
                        UpdateResult result =
                            orderService.performOrderAction(action, statementBuilder.ToStatement());

                        // Display results.
                        if (result != null && result.numChanges > 0)
                        {
                            Console.WriteLine("Number of orders approved: {0}", result.numChanges);
                        }
                        else
                        {
                            Console.WriteLine("No orders were approved.");
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to approve orders. Exception says \"{0}\"",
                        e.Message);
                }
            }
        }
    }
}
