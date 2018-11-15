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
using Google.Api.Ads.AdManager.v201811;

using System;

using Google.Api.Ads.AdManager.Util.v201811;

using System.Collections.Generic;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v201811
{
    /// <summary>
    /// This code example sets custom field values on a line item. To determine
    /// which custom fields exist, run GetAllCustomFields.cs. To create custom
    /// field options, run CreateCustomFieldOptions.cs. To determine which line
    /// items exist, run GetAllLineItems.cs.
    /// </summary>
    public class SetLineItemCustomFieldValue : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example sets custom field values on a line item. To determine " +
                    "which custom fields exist, run GetAllCustomFields.cs. To create custom " +
                    "field options, run CreateCustomFieldOptions.cs. To determine which line " +
                    "items exist, run GetAllLineItems.cs.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            SetLineItemCustomFieldValue codeExample = new SetLineItemCustomFieldValue();
            Console.WriteLine(codeExample.Description);
            codeExample.Run(new AdManagerUser());
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (CustomFieldService customFieldService = user.GetService<CustomFieldService>())
            {
                // Get the LineItemService.
                LineItemService lineItemService = user.GetService<LineItemService>();

                // Set the IDs of the custom fields, custom field option, and line item.
                long customFieldId = long.Parse(_T("INSERT_STRING_CUSTOM_FIELD_ID_HERE"));
                long customFieldOptionId = long.Parse(_T("INSERT_CUSTOM_FIELD_OPTION_ID_HERE"));
                long lineItemId = long.Parse(_T("INSERT_LINE_ITEM_ID_HERE"));

                try
                {
                    // Get the drop-down custom field id.
                    long dropDownCustomFieldId = customFieldService
                        .getCustomFieldOption(customFieldOptionId).customFieldId;

                    StatementBuilder statementBuilder = new StatementBuilder()
                        .Where("id = :id")
                        .OrderBy("id ASC")
                        .Limit(1)
                        .AddValue("id", lineItemId);

                    // Get the line item.
                    LineItemPage lineItemPage =
                        lineItemService.getLineItemsByStatement(statementBuilder.ToStatement());
                    LineItem lineItem = lineItemPage.results[0];

                    // Create custom field values.
                    List<BaseCustomFieldValue> customFieldValues = new List<BaseCustomFieldValue>();
                    TextValue textValue = new TextValue();
                    textValue.value = "Custom field value";

                    CustomFieldValue customFieldValue = new CustomFieldValue();
                    customFieldValue.customFieldId = customFieldId;
                    customFieldValue.value = textValue;
                    customFieldValues.Add(customFieldValue);

                    DropDownCustomFieldValue dropDownCustomFieldValue =
                        new DropDownCustomFieldValue();
                    dropDownCustomFieldValue.customFieldId = dropDownCustomFieldId;
                    dropDownCustomFieldValue.customFieldOptionId = customFieldOptionId;
                    customFieldValues.Add(dropDownCustomFieldValue);

                    // Only add existing custom field values for different custom fields than
                    // the ones you are setting.
                    if (lineItem.customFieldValues != null)
                    {
                        foreach (BaseCustomFieldValue oldCustomFieldValue in lineItem
                            .customFieldValues)
                        {
                            if (!(oldCustomFieldValue.customFieldId == customFieldId) &&
                                !(oldCustomFieldValue.customFieldId == dropDownCustomFieldId))
                            {
                                customFieldValues.Add(oldCustomFieldValue);
                            }
                        }
                    }

                    lineItem.customFieldValues = customFieldValues.ToArray();

                    // Update the line item on the server.
                    LineItem[] lineItems = lineItemService.updateLineItems(new LineItem[]
                    {
                        lineItem
                    });

                    if (lineItems != null)
                    {
                        foreach (LineItem updatedLineItem in lineItems)
                        {
                            List<String> customFieldValueStrings = new List<String>();
                            foreach (BaseCustomFieldValue baseCustomFieldValue in lineItem
                                .customFieldValues)
                            {
                                if (baseCustomFieldValue is CustomFieldValue &&
                                    ((CustomFieldValue) baseCustomFieldValue).value is TextValue)
                                {
                                    customFieldValueStrings.Add("{ID: '" +
                                        baseCustomFieldValue.customFieldId + "', value: '" +
                                        ((TextValue) ((CustomFieldValue) baseCustomFieldValue)
                                            .value).value + "'}");
                                }
                                else if (baseCustomFieldValue is DropDownCustomFieldValue)
                                {
                                    customFieldValueStrings.Add("{ID: '" +
                                        baseCustomFieldValue.customFieldId +
                                        "', custom field option ID: '" +
                                        ((DropDownCustomFieldValue) baseCustomFieldValue)
                                        .customFieldOptionId + "'}");
                                }
                            }

                            Console.WriteLine(
                                "A line item with ID \"{0}\" set with custom field values " +
                                "\"{1}\".", updatedLineItem.id,
                                string.Join(", ", customFieldValueStrings.ToArray()));
                        }
                    }
                    else
                    {
                        Console.WriteLine("No line items were updated.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to update line items. Exception says \"{0}\"",
                        e.Message);
                }
            }
        }
    }
}
