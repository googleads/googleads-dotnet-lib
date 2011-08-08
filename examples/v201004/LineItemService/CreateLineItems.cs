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
  /// This code example creates new line items. To determine which line items
  /// exist, run GetAllLineItems.cs. To determine which orders exist, run
  /// GetAllOrders.cs. To determine which placements exist, run
  /// GetAllPlacements.cs.
  ///
  /// Tags: LineItemService.createLineItems
  /// </summary>
  class CreateLineItems : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example creates new line items. To determine which line items " +
            "exist, run GetAllLineItems.cs. To determine which orders exist, run GetAllOrders.cs" +
            ". To determine which placements exist, run GetAllPlacements.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new CreateLineItems();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code examples.
    /// </summary>
    /// <param name="user">The DFP user object running the code examples.</param>
    public override void Run(DfpUser user) {
      // Get the LineItemService.
      LineItemService lineItemService =
          (LineItemService) user.GetService(DfpService.v201004.LineItemService);

      // Set the order that all created line items will belong to and the
      // placement ID to target.
      long orderId = long.Parse(_T("INSERT_ORDER_ID_HERE"));
      long[] targetPlacementIds = new long[] { long.Parse(_T("INSERT_PLACEMENT_ID_HERE")) };

      // Create an array to store local line item objects.
      LineItem[] lineItems = new LineItem[5];

      for (int i = 0; i < 5; i++) {
        LineItem lineItem = new LineItem();
        lineItem.name = "Line item #" + i;
        lineItem.orderId = orderId;
        lineItem.targeting = new Targeting();

        lineItem.targeting.inventoryTargeting = new InventoryTargeting();
        lineItem.targeting.inventoryTargeting.targetedPlacementIds = targetPlacementIds;

        lineItem.lineItemType = LineItemType.STANDARD;
        lineItem.allowOverbook = true;

        // Set the creative rotation type to even.
        lineItem.creativeRotationType = CreativeRotationType.EVEN;

        // Set the size of creatives that can be associated with this line item.
        Size size = new Size();
        size.width = 300;
        size.height = 250;
        size.isAspectRatio = false;

        lineItem.creativeSizes = new Size[] {size};

        // Set the length of the line item to run.
        lineItem.startType = LineItemSummaryStartType.IMMEDIATELY;
        lineItem.endDateTime = DateTimeUtilities.FromDateTime(System.DateTime.Today.AddMonths(1));

        // Set the cost per unit to $2.
        lineItem.costType = CostType.CPM;
        lineItem.costPerUnit = new Money();
        lineItem.costPerUnit.currencyCode = "USD";
        lineItem.costPerUnit.microAmount = 2000000L;

        // Set the number of units bought to 500,000 so that the budget is
        // $1,000.
        lineItem.unitsBought = 500000L;
        lineItem.unitType = UnitType.IMPRESSIONS;

        lineItems[i] = lineItem;
      }

      try {
        // Create the line items on the server.
        lineItems = lineItemService.createLineItems(lineItems);

        if (lineItems != null) {
          foreach (LineItem lineItem in lineItems) {
            Console.WriteLine("A line item with ID \"{0}\", belonging to order ID \"{1}\", and" +
                " named \"{2}\" was created.", lineItem.id, lineItem.orderId, lineItem.name);
          }
        } else {
          Console.WriteLine("No line items created.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to create line items. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
