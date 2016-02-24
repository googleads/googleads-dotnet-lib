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
using Google.Api.Ads.Dfp.Util.v201602;
using Google.Api.Ads.Dfp.v201602;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201602 {
  /// <summary>
  /// This code example gets all custom fields. To create custom fields, run
  /// CreateCustomFields.cs.
  /// </summary>
  class GetAllCustomFields : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets all custom fields. To create custom fields, run " +
            "CreateCustomFields.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetAllCustomFields();
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

      // Create a statement to get all custom fields.
      StatementBuilder statementBuilder = new StatementBuilder()
          .OrderBy("id ASC")
          .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT);

      // Sets default for page.
      CustomFieldPage page = new CustomFieldPage();

      try {
        do {
          // Get custom fields by statement.
          page = customFieldService.getCustomFieldsByStatement(statementBuilder.ToStatement());

          if (page.results != null) {
            int i = page.startIndex;
            foreach (CustomField customField in page.results) {
              if (customField is DropDownCustomField) {
                List<String> dropDownCustomFieldStrings = new List<String>();
                DropDownCustomField dropDownCustomField = (DropDownCustomField) customField;
                if (dropDownCustomField.options != null) {
                  foreach (CustomFieldOption customFieldOption in dropDownCustomField.options) {
                    dropDownCustomFieldStrings.Add(customFieldOption.displayName);
                  }
                }
                Console.WriteLine("{0}) Drop-down custom field with ID \"{1}\", name \"{2}\", " +
                    "and options {{{3}}} was found.", i, customField.id, customField.name,
                    string.Join(", ", dropDownCustomFieldStrings.ToArray()));
              } else {
                Console.WriteLine("{0}) Custom field with ID \"{1}\" and  name \"{2}\" was found.",
                    i, customField.id, customField.name);
              }
              i++;
            }
          }

          statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
        } while (statementBuilder.GetOffset() < page.totalResultSetSize);

        Console.WriteLine("Number of results found: " + page.totalResultSetSize);
      } catch (Exception e) {
        Console.WriteLine("Failed to get all custom fields. Exception says \"{0}\"", e.Message);
      }
    }
  }
}
