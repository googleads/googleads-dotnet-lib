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
    /// This code example deactivates all LICAs for a given line item. To determine
    /// which LICAs exist, run GetAllLicas.cs.
    /// </summary>
    public class DeactivateLicas : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return
                    "This code example deactivates all LICAs for a given line item. To determine " +
                    "which LICAs exist, run GetAllLicas.cs.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            DeactivateLicas codeExample = new DeactivateLicas();
            Console.WriteLine(codeExample.Description);
            codeExample.Run(new AdManagerUser());
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (LineItemCreativeAssociationService licaService =
                user.GetService<LineItemCreativeAssociationService>())
            {
                // Set the line item to get LICAs by.
                long lineItemId = long.Parse(_T("INSERT_LINE_ITEM_ID_HERE"));

                // Create a statement to page through active LICAs.
                StatementBuilder statementBuilder = new StatementBuilder()
                    .Where("lineItemId = :lineItemId").OrderBy("lineItemId ASC, creativeId ASC")
                    .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT)
                    .AddValue("lineItemId", lineItemId);

                // Set default for page.
                LineItemCreativeAssociationPage page = new LineItemCreativeAssociationPage();
                List<string> creativeIds = new List<string>();

                try
                {
                    do
                    {
                        // Get LICAs by statement.
                        page = licaService.getLineItemCreativeAssociationsByStatement(
                            statementBuilder.ToStatement());

                        if (page.results != null)
                        {
                            int i = page.startIndex;
                            foreach (LineItemCreativeAssociation lica in page.results)
                            {
                                Console.WriteLine(
                                    "{0}) LICA with line item ID = '{1}', creative ID ='{2}' and " +
                                    "status ='{3}' will be deactivated.", i, lica.lineItemId,
                                    lica.creativeId, lica.status);
                                i++;
                                creativeIds.Add(lica.creativeId.ToString());
                            }
                        }

                        statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
                    } while (statementBuilder.GetOffset() < page.totalResultSetSize);

                    Console.WriteLine("Number of LICAs to be deactivated: {0}", creativeIds.Count);

                    if (creativeIds.Count > 0)
                    {
                        // Modify statement for action.
                        statementBuilder.RemoveLimitAndOffset();

                        // Create action.
                        DeactivateLineItemCreativeAssociations action =
                            new DeactivateLineItemCreativeAssociations();

                        // Perform action.
                        UpdateResult result =
                            licaService.performLineItemCreativeAssociationAction(action,
                                statementBuilder.ToStatement());

                        // Display results.
                        if (result != null && result.numChanges > 0)
                        {
                            Console.WriteLine("Number of LICAs deactivated: {0}",
                                result.numChanges);
                        }
                        else
                        {
                            Console.WriteLine("No LICAs were deactivated.");
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to deactivate LICAs. Exception says \"{0}\"",
                        e.Message);
                }
            }
        }
    }
}
