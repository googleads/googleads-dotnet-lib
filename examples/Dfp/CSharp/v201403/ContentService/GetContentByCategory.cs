// Copyright 2014, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.Util.v201403;
using Google.Api.Ads.Dfp.v201403;

using System;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201403 {
  /// <summary>
  /// This code example gets all active content categorized as "comedy" using
  /// the network's content browse custom targeting key. This feature is only
  /// available to DFP video publishers.
  ///
  /// Tags: NetworkService.getCurrentNetwork
  /// Tags: CustomTargetingService.getCustomTargetingValuesByStatement
  /// Tags: ContentService.getContentByStatementAndCustomTargetingValue
  /// /// </summary>
  class GetContentByCategory : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets all active content categorized as 'comedy' using the " +
            "network's content browse custom targeting key. This feature is only available to " +
            "DFP video publishers.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetContentByCategory();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the ContentService.
      ContentService contentService =
          (ContentService) user.GetService(DfpService.v201403.ContentService);

      // Get the NetworkService.
      NetworkService networkService = (NetworkService) user.GetService(
          DfpService.v201403.NetworkService);

      // Get the CustomTargetingService.
      CustomTargetingService customTargetingService = (CustomTargetingService) user.GetService(
          DfpService.v201403.CustomTargetingService);

      try {
        // Get content browse custom targeting key ID.
        long contentBrowseCustomTargetingKeyId =
            networkService.getCurrentNetwork().contentBrowseCustomTargetingKeyId;

        // Create a statement to select the categories matching the name comedy.
        Statement categoryFilterStatement = new StatementBuilder()
            .Where("customTargetingKeyId = :contentBrowseCustomTargetingKeyId " +
            " and name = :category")
            .OrderBy("id ASC")
            .Limit(1)
            .AddValue("contentBrowseCustomTargetingKeyId", contentBrowseCustomTargetingKeyId)
            .AddValue("category", "comedy")
            .ToStatement();

        // Get categories matching the filter statement.
        CustomTargetingValuePage customTargetingValuePage =
            customTargetingService.getCustomTargetingValuesByStatement(categoryFilterStatement);

        if (customTargetingValuePage.results != null) {
          // Get the custom targeting value ID for the comedy category.
          long categoryCustomTargetingValueId = customTargetingValuePage.results[0].id;

          // Create a statement to get all active content.
          StatementBuilder statementBuilder = new StatementBuilder()
            .Where("status = :status")
            .OrderBy("id ASC")
            .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT)
            .AddValue("status", "ACTIVE");

          // Set defaults for page and filterStatement.
          ContentPage page = new ContentPage();

          do {
            // Get content by statement.
            page = contentService.getContentByStatementAndCustomTargetingValue(
                statementBuilder.ToStatement(), categoryCustomTargetingValueId);

            if (page.results != null) {
              int i = page.startIndex;
              foreach (Content content in page.results) {
                Console.WriteLine("{0})  Content with ID \"{1}\", name \"{2}\", and status " +
                    "\"{3}\" was found.", i, content.id, content.name, content.status);
                i++;
              }
            }
            statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
          } while (statementBuilder.GetOffset() < page.totalResultSetSize);

          Console.WriteLine("Number of results found: " + page.totalResultSetSize);
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to get content by category. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
