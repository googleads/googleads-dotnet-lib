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
    /// This code example creates custom fields. To determine which custom fields
    /// exist, run GetAllCustomFields.cs.
    /// </summary>
    public class CreateCustomFields : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example creates custom fields. To determine which custom " +
                    "fields exist, run GetAllCustomFields.cs.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            CreateCustomFields codeExample = new CreateCustomFields();
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
                // Create custom fields.
                CustomField customField1 = new CustomField();
                customField1.name = "Customer comments #" + GetTimeStamp();
                customField1.entityType = CustomFieldEntityType.LINE_ITEM;
                customField1.dataType = CustomFieldDataType.STRING;
                customField1.visibility = CustomFieldVisibility.FULL;

                CustomField customField2 = new CustomField();
                customField2.name = "Internal approval status #" + GetTimeStamp();
                customField2.entityType = CustomFieldEntityType.LINE_ITEM;
                customField2.dataType = CustomFieldDataType.DROP_DOWN;
                customField2.visibility = CustomFieldVisibility.FULL;

                try
                {
                    // Add custom fields.
                    CustomField[] customFields = customFieldService.createCustomFields(
                        new CustomField[]
                        {
                            customField1,
                            customField2
                        });

                    // Display results.
                    if (customFields != null)
                    {
                        foreach (CustomField customField in customFields)
                        {
                            Console.WriteLine(
                                "Custom field with ID \"{0}\" and name \"{1}\" was created.",
                                customField.id, customField.name);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No custom fields created.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to create custom fields. Exception says \"{0}\"",
                        e.Message);
                }
            }
        }
    }
}
