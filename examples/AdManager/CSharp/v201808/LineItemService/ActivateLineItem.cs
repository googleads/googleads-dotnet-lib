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
using Google.Api.Ads.AdManager.Util.v201808;
using Google.Api.Ads.AdManager.v201808;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v201808
{
    /// <summary>
    /// This code example activates all line items for the given order. To be
    /// activated, line items need to be in the approved (needs creatives) state
    /// and have at least one creative associated with them. To approve line
    /// items, approve the order to which they belong by running ApproveOrders.cs.
    /// To create LICAs, run CreateLicas.cs. To determine which line items exist,
    /// run GetAllLineItem.cs.
    /// </summary>
    public class ActivateLineItem : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example activates all line items for the given order. " +
                    "To be activated, line items need to be in the approved (needs creatives) " +
                    "state and have at least one creative associated with them. To approve line " +
                    "items, approve the order to which they belong by running ApproveOrders.cs. " +
                    "To create LICAs, run CreateLicas.cs. To determine which line items exist, " +
                    "run GetAllLineItem.cs.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            ActivateLineItem codeExample = new ActivateLineItem();
            Console.WriteLine(codeExample.Description);
            codeExample.Run(new AdManagerUser());
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (LineItemService lineItemService = user.GetService<LineItemService>())
            {
                // Set the ID of the order to get line items from.
                long orderId = long.Parse(_T("INSERT_ORDER_ID_HERE"));

                // Create statement to select approved line items from a given order.
                StatementBuilder statementBuilder = new StatementBuilder()
                    .Where("orderId = :orderId and status = :status").AddValue("orderId", orderId)
                    .AddValue("status", ComputedStatus.INACTIVE.ToString());

                // Set default for page.
                LineItemPage page = new LineItemPage();
                List<string> lineItemIds = new List<string>();

                try
                {
                    do
                    {
                        // Get line items by statement.
                        page = lineItemService.getLineItemsByStatement(
                            statementBuilder.ToStatement());

                        if (page.results != null)
                        {
                            int i = page.startIndex;
                            foreach (LineItemSummary lineItem in page.results)
                            {
                                // Archived line items cannot be activated.
                                if (!lineItem.isArchived)
                                {
                                    Console.WriteLine(
                                        "{0}) Line item with ID ='{1}', belonging to order " +
                                        "ID ='{2}' and name ='{3}' will be activated.", i,
                                        lineItem.id, lineItem.orderId, lineItem.name);
                                    lineItemIds.Add(lineItem.id.ToString());
                                    i++;
                                }
                            }
                        }

                        statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
                    } while (statementBuilder.GetOffset() < page.totalResultSetSize);


                    Console.WriteLine("Number of line items to be activated: {0}",
                        lineItemIds.Count);

                    if (lineItemIds.Count > 0)
                    {
                        // Modify statement.
                        statementBuilder.RemoveLimitAndOffset();

                        // Create action.
                        ActivateLineItems action = new ActivateLineItems();

                        // Perform action.
                        UpdateResult result =
                            lineItemService.performLineItemAction(action,
                                statementBuilder.ToStatement());

                        // Display results.
                        if (result != null && result.numChanges > 0)
                        {
                            Console.WriteLine("Number of line items activated: {0}",
                                result.numChanges);
                        }
                        else
                        {
                            Console.WriteLine("No line items were activated.");
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to activate line items. Exception says \"{0}\"",
                        e.Message);
                }
            }
        }
    }
}
