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

namespace Google.Api.Ads.AdManager.Examples.CSharp.v202311
{
    /// <summary>
    /// This code example gets all child ad units of the effective root ad unit. To create an ad
    /// unit, run CreateAdUnits.cs.
    /// </summary>
    public class GetAdUnitsByStatement : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return
                    "This code example gets all child ad units of the effective root ad unit. To " +
                    "create an ad unit, run CreateAdUnits.cs.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            GetAdUnitsByStatement codeExample = new GetAdUnitsByStatement();
            Console.WriteLine(codeExample.Description);
            codeExample.Run(new AdManagerUser());
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (InventoryService inventoryService = user.GetService<InventoryService>())
                using (NetworkService networkService = user.GetService<NetworkService>())
                {
                    // Get the effective root ad unit ID of the network.
                    string effectiveRootAdUnitId =
                        networkService.getCurrentNetwork().effectiveRootAdUnitId;

                    // Create a statement to select the children of the effective root ad
                    // unit.
                    StatementBuilder statementBuilder = new StatementBuilder()
                        .Where("parentId = :parentId").OrderBy("id ASC")
                        .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT)
                        .AddValue("parentId", effectiveRootAdUnitId);

                    // Set default for page.
                    AdUnitPage page = new AdUnitPage();

                    try
                    {
                        do
                        {
                            // Get ad units by statement.
                            page = inventoryService.getAdUnitsByStatement(statementBuilder
                                .ToStatement());

                            if (page.results != null)
                            {
                                int i = page.startIndex;
                                foreach (AdUnit adUnit in page.results)
                                {
                                    Console.WriteLine(
                                        "{0}) Ad unit with ID = '{1}', name = '{2}' and " +
                                        "status = '{3}' was found.", i, adUnit.id, adUnit.name,
                                        adUnit.status);
                                    i++;
                                }
                            }

                            statementBuilder.IncreaseOffsetBy(StatementBuilder
                                .SUGGESTED_PAGE_LIMIT);
                        } while (statementBuilder.GetOffset() < page.totalResultSetSize);

                        Console.WriteLine("Number of results found: {0}", page.totalResultSetSize);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Failed to get ad unit. Exception says \"{0}\"",
                            e.Message);
                    }
                }
        }
    }
}
