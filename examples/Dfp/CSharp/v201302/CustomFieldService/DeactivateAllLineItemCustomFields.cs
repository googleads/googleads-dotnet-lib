// Copyright 2013, Google Inc. All Rights Reserved.
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

// Author: api.anash@gmail.com (Anash P. Oommen)

using Google.Api.Ads.Common.Util;
using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.v201302;

using System;
using Google.Api.Ads.Dfp.Util.v201302;
using System.Collections.Generic;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201302 {
  /// <summary>
  /// This code example deactivates all active line item custom fields. To
  /// determine which custom fields exist, run GetAllCustomFields.cs.
  ///
  /// Tags: CustomFieldService.getCustomFieldsByStatement
  /// Tags: CustomFieldService.performCustomFieldAction
  /// </summary>
  class DeactivateAllLineItemCustomFields : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example deactivates all active line item custom fields. To determine " +
            "which custom fields exist, run GetAllCustomFields.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new DeactivateAllLineItemCustomFields();
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
          DfpService.v201302.CustomFieldService);

      // Create statement to select only active custom fields that apply to
      // line items.
      String statementText = "WHERE entityType = :entityType and isActive = :isActive LIMIT 500";
      Statement filterStatement = new StatementBuilder(statementText)
              .AddValue("entityType", CustomFieldEntityType.LINE_ITEM.ToString())
              .AddValue("isActive", true)
              .ToStatement();

      // Set defaults for page and offset.
      CustomFieldPage page = new CustomFieldPage();
      int offset = 0;
      int i = 0;
      List<string> customFieldIds = new List<string>();

      try {
        do {
          // Create a statement to page through custom fields.
          filterStatement.query = statementText + " OFFSET " + offset;

          // Get custom fields by statement.
          page = customFieldService.getCustomFieldsByStatement(filterStatement);

          if (page.results != null) {
            foreach (CustomField customField in page.results) {
              Console.WriteLine("{0}) Custom field with ID \"{1}\" and name \"{2}\" will be " +
                  "deactivated.", i, customField.id, customField.name);
              customFieldIds.Add(customField.id.ToString());
              i++;
            }
          }
          offset += 500;
        } while (offset < page.totalResultSetSize);

        Console.WriteLine("Number of custom fields to be deactivated: " + customFieldIds.Count);

        if (customFieldIds.Count > 0) {
          // Modify statement for action.
          filterStatement.query = "WHERE id IN (" + string.Join(", ", customFieldIds.ToArray()) +
              ")";

          // Create action.
          DeactivateCustomFields action = new DeactivateCustomFields();

          // Perform action.
          UpdateResult result = customFieldService.performCustomFieldAction(
              action, filterStatement);

          // Display results.
          if (result != null && result.numChanges > 0) {
            Console.WriteLine("Number of custom fields deactivated: " + result.numChanges);
          } else {
            Console.WriteLine("No custom fields were deactivated.");
          }
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to deactivate custom fields. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
