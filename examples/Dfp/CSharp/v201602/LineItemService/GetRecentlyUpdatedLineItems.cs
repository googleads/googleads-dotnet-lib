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
using Google.Api.Ads.Dfp.v201602;
using Google.Api.Ads.Dfp.Util.v201602;

using System;

using DateTime = Google.Api.Ads.Dfp.v201602.DateTime;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201602 {
  /// <summary>
  /// This code example shows how to get recently updated line items. To create
  /// line items, run CreateLineItems.cs.
  /// </summary>
  class GetRecentlyUpdatedLineItems : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example shows how to get recently updated line items. To create " +
            "line items, run CreateLineItems.cs";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetRecentlyUpdatedLineItems();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the OrderService.
      LineItemService lineItemService = (LineItemService) user.GetService(
          DfpService.v201602.LineItemService);

      long orderId = long.Parse(_T("INSERT_ORDER_ID_HERE"));

      // Create statement to only select line items for the given order that
      // have been modified in the last 3 days.
      DateTime threeDaysAgo =
        DateTimeUtilities.FromDateTime(System.DateTime.Now.AddDays(-3), "America/New_York");
      StatementBuilder statementBuilder = new StatementBuilder()
          .Where("lastModifiedDateTime >= :lastModifiedDateTime AND orderId = :orderId")
          .OrderBy("id ASC")
          .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT)
          .AddValue("lastModifiedDateTime", threeDaysAgo)
          .AddValue("orderId", orderId);

      // Set default for page.
      LineItemPage page = new LineItemPage();

      try {
        do {
          // Get line items by statement.
          page = lineItemService.getLineItemsByStatement(statementBuilder.ToStatement());

          // Display results.
          if (page != null && page.results != null) {
            foreach (LineItem lineItem in page.results) {
              Console.WriteLine("Line item with id \"{0}\", belonging to order id \"{1}\" and " +
                  "named \"{2}\" was found.", lineItem.id, lineItem.orderId, lineItem.name);
            }
          }
          statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
        } while(statementBuilder.GetOffset() < page.totalResultSetSize);

        Console.WriteLine("Number of results found: {1}.", page.totalResultSetSize);
      } catch (Exception e) {
        Console.WriteLine("Failed to get line items. Exception says \"{0}\"", e.Message);
      }
    }
  }
}
