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
using Google.Api.Ads.AdManager.v202311;

using System;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v202311
{
    /// <summary>
    /// This code example creates custom field options for a drop-down custom
    /// field. Once created, custom field options can be found under the options
    /// fields of the drop-down custom field and they cannot be deleted. To
    /// determine which custom fields exist, run GetAllCustomFields.cs.
    /// </summary>
    public class CreateCustomFieldOptions : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example creates custom field options for a drop-down custom " +
                    "field. Once created, custom field options can be found under the options " +
                    "fields of the drop-down custom field and they cannot be deleted. " +
                    "To determine which custom fields exist, run GetAllCustomFields.cs.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            CreateCustomFieldOptions codeExample = new CreateCustomFieldOptions();
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
                // Set the ID of the drop-down custom field to create options for.
                long customFieldId = long.Parse(_T("INSERT_DROP_DOWN_CUSTOM_FIELD_ID_HERE"));

                // Create custom field options.
                CustomFieldOption customFieldOption1 = new CustomFieldOption();
                customFieldOption1.displayName = "Approved";
                customFieldOption1.customFieldId = customFieldId;

                CustomFieldOption customFieldOption2 = new CustomFieldOption();
                customFieldOption2.displayName = "Unapproved";
                customFieldOption2.customFieldId = customFieldId;

                try
                {
                    // Add custom field options.
                    CustomFieldOption[] customFieldOptions =
                        customFieldService.createCustomFieldOptions(new CustomFieldOption[]
                        {
                            customFieldOption1,
                            customFieldOption2
                        });

                    // Display results.
                    if (customFieldOptions != null)
                    {
                        foreach (CustomFieldOption customFieldOption in customFieldOptions)
                        {
                            Console.WriteLine(
                                "Custom field option with ID \"{0}\" and name \"{1}\" was " +
                                "created.", customFieldOption.id, customFieldOption.displayName);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No custom field options created.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(
                        "Failed to create custom field options. Exception says \"{0}\"", e.Message);
                }
            }
        }
    }
}
