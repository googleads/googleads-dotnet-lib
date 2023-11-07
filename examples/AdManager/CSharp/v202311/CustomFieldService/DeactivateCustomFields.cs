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
    /// This code example deactivates a custom field. To determine which custom fields exist,
    /// run GetAllCustomFields.cs.
    /// </summary>
    public class DeactivateCustomFields : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example deactivates a custom field. To determine which custom " +
                    "fields exist, run GetAllCustomFields.cs.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            DeactivateCustomFields codeExample = new DeactivateCustomFields();
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
                // Set the ID of the custom field to update.
                int customFieldId = int.Parse(_T("INSERT_CUSTOM_FIELD_ID_HERE"));

                // Create statement to select only active custom fields that apply to
                // line items.
                StatementBuilder statementBuilder = new StatementBuilder()
                    .Where("id = :id")
                    .OrderBy("id ASC")
                    .Limit(1)
                    .AddValue("id", customFieldId);

                // Set default for page.
                CustomFieldPage page = new CustomFieldPage();
                int i = 0;
                List<string> customFieldIds = new List<string>();

                try
                {
                    do
                    {
                        // Get custom fields by statement.
                        page = customFieldService.getCustomFieldsByStatement(
                            statementBuilder.ToStatement());

                        if (page.results != null)
                        {
                            foreach (CustomField customField in page.results)
                            {
                                Console.WriteLine(
                                    "{0}) Custom field with ID \"{1}\" and name \"{2}\" will be " +
                                    "deactivated.", i, customField.id, customField.name);
                                customFieldIds.Add(customField.id.ToString());
                                i++;
                            }
                        }

                        statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
                    } while (statementBuilder.GetOffset() < page.totalResultSetSize);

                    Console.WriteLine("Number of custom fields to be deactivated: " +
                        customFieldIds.Count);

                    if (customFieldIds.Count > 0)
                    {
                        // Remove limit and offset from statement.
                        statementBuilder.RemoveLimitAndOffset();

                        // Create action.
                        Google.Api.Ads.AdManager.v202311.DeactivateCustomFields action =
                            new Google.Api.Ads.AdManager.v202311.DeactivateCustomFields();

                        // Perform action.
                        UpdateResult result =
                            customFieldService.performCustomFieldAction(action,
                                statementBuilder.ToStatement());

                        // Display results.
                        if (result != null && result.numChanges > 0)
                        {
                            Console.WriteLine("Number of custom fields deactivated: " +
                                result.numChanges);
                        }
                        else
                        {
                            Console.WriteLine("No custom fields were deactivated.");
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to deactivate custom fields. Exception says \"{0}\"",
                        e.Message);
                }
            }
        }
    }
}
