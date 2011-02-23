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
using Google.Api.Ads.Dfp.Util.v201004;
using Google.Api.Ads.Dfp.v201004;

using System;

namespace Google.Api.Ads.Dfp.Examples.v201004 {
  /// <summary>
  /// This code example gets all orders for a given advertiser. The Statement
  /// retrieves up to the maximum page size limit of 500. To create orders, run
  /// CreateOrders.cs. To determine which companies are advertisers,
  /// run GetCompaniesByStatement.cs.
  /// </summary>
  class GetOrdersByStatement : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets all orders for a given advertiser. The Statement " +
            "retrieves up to the maximum page size limit of 500. To create orders, run " +
            "CreateOrders.cs. To determine which companies are advertisers,run " +
            "GetCompaniesByStatement.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetOrdersByStatement();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the OrderService.
      OrderService orderService = (OrderService) user.GetService(DfpService.v201004.OrderService);

      // Set the name of the advertiser (company) to get orders for.
      String advertiserId = _T("INSERT_ADVERTISER_COMPANY_ID_HERE");

      // Create a Statement to only select orders for a given advertiser.
      Statement statement = new StatementBuilder("WHERE advertiserId = :advertiserId LIMIT 500").
          AddParam("advertiserId", advertiserId).ToStatement();

      try {
        // Get orders by Statement.
        OrderPage page = orderService.getOrdersByStatement(statement);

        if (page.results != null && page.results.Length > 0) {
          int i = page.startIndex;
          foreach (Order order in page.results) {
            Console.WriteLine("{0}) Order with ID = '{1}', name = '{2}', and advertiser " +
                "ID = '{3}' was found.", i, order.id, order.name, order.advertiserId);
            i++;
          }
        }
        Console.WriteLine("Number of results found: " + page.totalResultSetSize);
      } catch (Exception ex) {
        Console.WriteLine("Failed to get orders by Statement. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
