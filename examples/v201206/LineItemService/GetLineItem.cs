// Copyright 2012, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.Dfp.v201206;

using System;

namespace Google.Api.Ads.Dfp.Examples.v201206 {
  /// <summary>
  /// This code example gets a line item by its ID. To determine which line
  /// items exist, run GetAllLineItems.cs.
  ///
  /// Tags: LineItemService.getLineItem
  /// </summary>
  class GetLineItem : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets a line item by its ID. To determine which line items " +
            "exist, run GetAllLineItems.cs";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetLineItem();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the LineItemService.
      LineItemService lineItemService =
          (LineItemService) user.GetService(DfpService.v201206.LineItemService);

      // Set the ID of the line item to get.
      long lineItemId = long.Parse(_T("INSERT_LINE_ITEM_ID_HERE"));

      try {
        // Get the line item.
        LineItem lineItem = lineItemService.getLineItem(lineItemId);

        if (lineItem != null) {
          Console.WriteLine("Line item with ID ='{0}', belonging to order ID ='{1}', and named " +
              "'{2}' was found.", lineItem.id, lineItem.orderId, lineItem.name);
        } else {
          Console.WriteLine("No line item found for this ID.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to get line item by ID. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
