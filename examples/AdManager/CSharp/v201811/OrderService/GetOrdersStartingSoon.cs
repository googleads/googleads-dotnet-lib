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
using Google.Api.Ads.AdManager.Util.v201811;
using Google.Api.Ads.AdManager.v201811;

using System;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v201811
{
    /// <summary>
    /// This example gets all orders that are starting soon.
    /// </summary>
    public class GetOrdersStartingSoon : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get { return "This example gets all orders that are starting soon."; }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            GetOrdersStartingSoon codeExample = new GetOrdersStartingSoon();
            Console.WriteLine(codeExample.Description);
            try
            {
                codeExample.Run(new AdManagerUser());
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to get orders. Exception says \"{0}\"", e.Message);
            }
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (OrderService orderService = user.GetService<OrderService>())
            {
                // Create a statement to select orders.
                int pageSize = StatementBuilder.SUGGESTED_PAGE_LIMIT;
                StatementBuilder statementBuilder = new StatementBuilder()
                    .Where("status = :status and startDateTime >= :now and startDateTime <= :soon")
                    .OrderBy("id ASC")
                    .Limit(pageSize)
                    .AddValue("status", OrderStatus.APPROVED.ToString())
                    .AddValue("now",
                        DateTimeUtilities.FromDateTime(System.DateTime.Now, "America/New_York"))
                    .AddValue("soon",
                        DateTimeUtilities.FromDateTime(System.DateTime.Now.AddDays(5),
                            "America/New_York"));

                // Retrieve a small amount of orders at a time, paging through until all
                // orders have been retrieved.
                int totalResultSetSize = 0;
                do
                {
                    OrderPage page =
                        orderService.getOrdersByStatement(statementBuilder.ToStatement());

                    // Print out some information for each order.
                    if (page.results != null)
                    {
                        totalResultSetSize = page.totalResultSetSize;
                        int i = page.startIndex;
                        foreach (Order order in page.results)
                        {
                            Console.WriteLine("{0}) Order with ID {1} and name \"{2}\" was found.",
                                i++, order.id, order.name);
                        }
                    }

                    statementBuilder.IncreaseOffsetBy(pageSize);
                } while (statementBuilder.GetOffset() < totalResultSetSize);

                Console.WriteLine("Number of results found: {0}", totalResultSetSize);
            }
        }
    }
}
