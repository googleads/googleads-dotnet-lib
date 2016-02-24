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
using Google.Api.Ads.Dfp.Util.v201602;
using System.Collections.Generic;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201602 {
  /// <summary>
  /// This code example updates custom field descriptions. To determine which
  /// custom fields exist, run GetAllCustomFields.cs.
  /// </summary>
  class UpdateCustomFields : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example updates custom field descriptions. To determine which custom " +
            "fields exist, run GetAllCustomFields.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new UpdateCustomFields();
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

      // Set the ID of the custom field to update.
      long customFieldId = long.Parse(_T("INSERT_CUSTOM_FIELD_ID_HERE"));

      try {
        // Get the custom field.
        StatementBuilder statementBuilder = new StatementBuilder()
            .Where("id = :id")
            .OrderBy("id ASC")
            .Limit(1)
            .AddValue("id", customFieldId);

        CustomFieldPage page = customFieldService.getCustomFieldsByStatement(
            statementBuilder.ToStatement());
        CustomField customField = page.results[0];

        customField.description =
            (customField.description == null ? "" : customField.description + " Updated");

        // Update the custom field on the server.
        CustomField[] customFields =
            customFieldService.updateCustomFields(new CustomField[] {customField});

        // Display results
        foreach (CustomField updatedCustomField in customFields) {
          Console.WriteLine("Custom field with ID \"{0}\", name \"{1}\", and description " +
              "\"{2}\" was updated.", updatedCustomField.id, updatedCustomField.name,
              updatedCustomField.description);
        }
      } catch (Exception e) {
        Console.WriteLine("Failed to update custom fields. Exception says \"{0}\"", e.Message);
      }
    }
  }
}
