// Copyright 2015, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.Common.Util;
using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.v201602;

using System;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201602 {
  /// <summary>
  /// This code example creates custom fields. To determine which custom fields
  /// exist, run GetAllCustomFields.cs.
  /// </summary>
  class CreateCustomFields : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example creates custom fields. To determine which custom fields exist" +
            ", run GetAllCustomFields.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new CreateCustomFields();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the CustomFieldService.
      CustomFieldService customFieldService = (CustomFieldService) user.GetService(
          DfpService.v201602.CustomFieldService);

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

      try {
        // Add custom fields.
        CustomField[] customFields =
            customFieldService.createCustomFields(new CustomField[] {customField1, customField2});

        // Display results.
        if (customFields != null) {
          foreach (CustomField customField in customFields) {
            Console.WriteLine("Custom field with ID \"{0}\" and name \"{1}\" was created.",
                customField.id, customField.name);
          }
        } else {
          Console.WriteLine("No custom fields created.");
        }
      } catch (Exception e) {
        Console.WriteLine("Failed to create custom fields. Exception says \"{0}\"", e.Message);
      }
    }
  }
}
