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
using Google.Api.Ads.Dfp.v201306;

using System;
using Google.Api.Ads.Dfp.Util.v201306;
using System.Collections.Generic;

namespace Google.Api.Ads.Dfp.Examples.v201306 {
  /// <summary>
  /// This code example gets all custom fields that apply to line items. To
  /// create custom fields, run CreateCustomFields.cs.
  ///
  /// Tags: CustomFieldService.getCustomFieldsByStatement
  /// </summary>
  class GetAllLineItemCustomFields : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets all custom fields that apply to line items. To create " +
            "custom fields, run CreateCustomFields.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetAllLineItemCustomFields();
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
          DfpService.v201306.CustomFieldService);

      // Create statement to select only custom fields that apply to line items.
      String statementText = "WHERE entityType = :entityType LIMIT 500";
      Statement filterStatement = new StatementBuilder(statementText)
          .AddValue("entityType", CustomFieldEntityType.LINE_ITEM.ToString())
          .ToStatement();

      // Set defaults for page and offset.
      CustomFieldPage page = new CustomFieldPage();
      int offset = 0;
      int i = 0;

      try {
        do {
          // Create a statement to page through custom fields.
          filterStatement.query = statementText + " OFFSET " + offset;

          // Get custom fields by statement.
          page = customFieldService.getCustomFieldsByStatement(filterStatement);

          if (page.results != null) {
            foreach (CustomField customField in page.results) {
              Console.WriteLine("{0}) Custom field with ID \"{1}\" and name \"{2}\" was found.", i,
                  customField.id, customField.name);
              i++;
            }
          }
          offset += 500;
        } while (offset < page.totalResultSetSize);
        Console.WriteLine("Number of results found: {0}", page.totalResultSetSize);
      } catch (Exception ex) {
        Console.WriteLine("Failed to get all line item custom fields. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
