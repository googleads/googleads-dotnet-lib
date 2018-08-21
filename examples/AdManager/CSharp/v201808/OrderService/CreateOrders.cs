// Copyright 2018, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdManager.v201808;

using System;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v201808
{
    /// <summary>
    /// This code example creates new orders. To determine which orders exist,
    /// run GetAllOrders.cs.
    /// </summary>
    public class CreateOrders : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example creates new orders. To determine which orders exist, " +
                    "run GetAllOrders.cs.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            CreateOrders codeExample = new CreateOrders();
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
                // Set the advertiser, salesperson, and trafficker to assign to each
                // order.
                long advertiserId = long.Parse(_T("INSERT_ADVERTISER_ID_HERE"));
                long salespersonId = long.Parse(_T("INSERT_SALESPERSON_ID_HERE"));
                long traffickerId = long.Parse(_T("INSERT_TRAFFICKER_ID_HERE"));

                // Create an array to store local order objects.
                Order[] orders = new Order[5];

                for (int i = 0; i < 5; i++)
                {
                    Order order = new Order();
                    order.name = string.Format("Order #{0}", i);
                    order.advertiserId = advertiserId;
                    order.salespersonId = salespersonId;
                    order.traffickerId = traffickerId;

                    orders[i] = order;
                }

                try
                {
                    // Create the orders on the server.
                    orders = orderService.createOrders(orders);

                    if (orders != null)
                    {
                        foreach (Order order in orders)
                        {
                            Console.WriteLine(
                                "An order with ID ='{0}' and named '{1}' was created.", order.id,
                                order.name);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No orders created.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to create orders. Exception says \"{0}\"", e.Message);
                }
            }
        }
    }
}
