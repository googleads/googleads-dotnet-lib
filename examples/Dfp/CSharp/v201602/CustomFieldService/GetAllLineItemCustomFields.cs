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
  /// This code example gets all custom fields that apply to line items. To
  /// create custom fields, run CreateCustomFields.cs.
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
          DfpService.v201602.CustomFieldService);

      // Create statement to select only custom fields that apply to line items.
      StatementBuilder statementBuilder = new StatementBuilder()
          .Where("entityType = :entityType")
          .OrderBy("id ASC")
          .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT)
          .AddValue("entityType", CustomFieldEntityType.LINE_ITEM.ToString());

      // Set default for page.
      CustomFieldPage page = new CustomFieldPage();
      int i = 0;

      try {
        do {
          // Get custom fields by statement.
          page = customFieldService.getCustomFieldsByStatement(statementBuilder.ToStatement());

          if (page.results != null) {
            foreach (CustomField customField in page.results) {
              Console.WriteLine("{0}) Custom field with ID \"{1}\" and name \"{2}\" was found.", i,
                  customField.id, customField.name);
              i++;
            }
          }
          statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
        } while (statementBuilder.GetOffset() < page.totalResultSetSize);
        Console.WriteLine("Number of results found: {0}", page.totalResultSetSize);
      } catch (Exception e) {
        Console.WriteLine("Failed to get all line item custom fields. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}
