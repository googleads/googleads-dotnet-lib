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
using System.Text;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v202311
{
    /// <summary>
    /// This code example updates a line item to add custom criteria targeting. To
    /// determine which line items exist, run GetAllLineItems.cs. To determine
    /// which custom targeting keys and values exist, run
    /// GetAllCustomTargetingKeysAndValues.cs.
    /// </summary>
    public class TargetCustomCriteria : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example updates a line item to add custom criteria targeting. " +
                    "To determine which line items exist, run GetAllLineItems.cs. To determine " +
                    "which custom targeting keys and values exist, " +
                    "run GetAllCustomTargetingKeysAndValues.cs.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            TargetCustomCriteria codeExample = new TargetCustomCriteria();
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
                long lineItemId = long.Parse(_T("INSERT_LINE_ITEM_ID_HERE"));
                long[] customCriteriaIds1 = new long[]
                {
                    long.Parse(_T("INSERT_CUSTOM_TARGETING_KEY_ID_HERE")),
                    long.Parse(_T("INSERT_CUSTOM_TARGETING_VALUE_ID_HERE"))
                };
                long[] customCriteriaIds2 = new long[]
                {
                    long.Parse(_T("INSERT_CUSTOM_TARGETING_KEY_ID_HERE")),
                    long.Parse(_T("INSERT_CUSTOM_TARGETING_VALUE_ID_HERE"))
                };
                long[] customCriteriaIds3 = new long[]
                {
                    long.Parse(_T("INSERT_CUSTOM_TARGETING_KEY_ID_HERE")),
                    long.Parse(_T("INSERT_CUSTOM_TARGETING_VALUE_ID_HERE"))
                };

                // Create custom criteria.
                CustomCriteria customCriteria1 = new CustomCriteria();
                customCriteria1.keyId = customCriteriaIds1[0];
                customCriteria1.valueIds = new long[]
                {
                    customCriteriaIds1[1]
                };
                customCriteria1.@operator = CustomCriteriaComparisonOperator.IS;

                CustomCriteria customCriteria2 = new CustomCriteria();
                customCriteria2.keyId = customCriteriaIds2[0];
                customCriteria2.valueIds = new long[]
                {
                    customCriteriaIds2[1]
                };
                customCriteria2.@operator = CustomCriteriaComparisonOperator.IS_NOT;

                CustomCriteria customCriteria3 = new CustomCriteria();
                customCriteria3.keyId = customCriteriaIds3[0];
                customCriteria3.valueIds = new long[]
                {
                    customCriteriaIds3[1]
                };
                customCriteria3.@operator = CustomCriteriaComparisonOperator.IS;

                // Create the custom criteria set that will resemble:
                //
                // (customCriteria1.key == customCriteria1.value OR
                //     (customCriteria2.key != customCriteria2.value AND
                //         customCriteria3.key == customCriteria3.value))
                CustomCriteriaSet topCustomCriteriaSet = new CustomCriteriaSet();
                topCustomCriteriaSet.logicalOperator = CustomCriteriaSetLogicalOperator.OR;

                CustomCriteriaSet subCustomCriteriaSet = new CustomCriteriaSet();
                subCustomCriteriaSet.logicalOperator = CustomCriteriaSetLogicalOperator.AND;
                subCustomCriteriaSet.children = new CustomCriteriaNode[]
                {
                    customCriteria2,
                    customCriteria3
                };
                topCustomCriteriaSet.children = new CustomCriteriaNode[]
                {
                    customCriteria1,
                    subCustomCriteriaSet
                };

                try
                {
                    StatementBuilder statementBuilder = new StatementBuilder()
                        .Where("id = :id")
                        .OrderBy("id ASC")
                        .Limit(1)
                        .AddValue("id", lineItemId);
                    // Set the custom criteria targeting on the line item.
                    LineItemPage page =
                        lineItemService.getLineItemsByStatement(statementBuilder.ToStatement());
                    LineItem lineItem = page.results[0];
                    lineItem.targeting.customTargeting = topCustomCriteriaSet;

                    // Update the line items on the server.
                    LineItem[] updatedLineItems = lineItemService.updateLineItems(new LineItem[]
                    {
                        lineItem
                    });

                    foreach (LineItem updatedLineItem in updatedLineItems)
                    {
                        // Display the updated line item.
                        Console.WriteLine(
                            "Line item with ID {0} updated with custom criteria targeting " +
                            "\n{1}\n", updatedLineItem.id,
                            getCustomCriteriaSetString(updatedLineItem.targeting.customTargeting,
                                0));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(
                        "Failed to add custom target criteria. Exception says \"{0}\"", e.Message);
                }
            }
        }

        /// <summary>
        /// Gets a string representation of the custom criteria node. If it has
        /// children, each child will be appended to the string recursively.
        /// </summary>
        /// <param name="root">The root custom criteria node.</param>
        /// <param name="level">The level of the custom criteria tree.</param>
        /// <returns>
        /// A string representation of the custom criteria node and its
        /// children
        /// </returns>
        private static string getCustomCriteriaSetString(CustomCriteriaNode root, int level)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(new String('\t', level));

            if (root is CustomCriteria)
            {
                CustomCriteria customCriteria = (CustomCriteria) root;
                StringBuilder ids = new StringBuilder();
                for (int j = 0; j < customCriteria.valueIds.Length; j++)
                {
                    ids.Append(customCriteria.valueIds[j] + ", ");
                }

                sb.AppendFormat("Custom criteria: operator: [{0}] key: [{1}] values: [{2}]\n",
                    customCriteria.@operator, customCriteria.keyId,
                    ids.ToString().TrimEnd(',', ' '));
                return sb.ToString();
            }
            else if (root is CustomCriteriaSet)
            {
                CustomCriteriaSet customCriteriaSet = (CustomCriteriaSet) root;
                sb.AppendFormat("Custom criteria set: operator: [{0}] children: \n",
                    customCriteriaSet.logicalOperator);
                foreach (CustomCriteriaNode node in customCriteriaSet.children)
                {
                    sb.Append(getCustomCriteriaSetString(node, level + 1));
                }

                return sb.Append("\n").ToString();
            }
            else
            {
                throw new Exception("Unexpected node: " + root.GetType().Name);
            }
        }
    }
}
