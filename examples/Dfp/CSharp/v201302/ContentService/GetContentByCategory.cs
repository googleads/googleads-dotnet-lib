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

using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.v201302;

using System;
using Google.Api.Ads.Dfp.Util.v201302;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201302 {
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
          (ContentService) user.GetService(DfpService.v201302.ContentService);

      // Get the NetworkService.
      NetworkService networkService = (NetworkService) user.GetService(
          DfpService.v201302.NetworkService);

      // Get the CustomTargetingService.
      CustomTargetingService customTargetingService = (CustomTargetingService) user.GetService(
          DfpService.v201302.CustomTargetingService);

      try {
        // Get content browse custom targeting key ID.
        long contentBrowseCustomTargetingKeyId =
            networkService.getCurrentNetwork().contentBrowseCustomTargetingKeyId;

        // Create a statement to select the categories matching the name comedy.
        Statement categoryFilterStatement = new StatementBuilder(
            "WHERE customTargetingKeyId = :contentBrowseCustomTargetingKeyId " +
            " and name = :category LIMIT 1")
            .AddValue("contentBrowseCustomTargetingKeyId", contentBrowseCustomTargetingKeyId)
            .AddValue("category", "comedy").ToStatement();

        // Get categories matching the filter statement.
        CustomTargetingValuePage customTargetingValuePage =
            customTargetingService.getCustomTargetingValuesByStatement(categoryFilterStatement);

        if (customTargetingValuePage.results != null) {
          // Get the custom targeting value ID for the comedy category.
          long categoryCustomTargetingValueId = customTargetingValuePage.results[0].id;

          // Set defaults for page and filterStatement.
          ContentPage page = new ContentPage();
          Statement filterStatement = new Statement();
          int offset = 0;

          do {
            // Create a statement to get all active content.
            filterStatement.query = "WHERE status = 'ACTIVE' LIMIT 500 OFFSET " + offset.ToString();

            // Get content by statement.
            page = contentService.getContentByStatementAndCustomTargetingValue(filterStatement,
                categoryCustomTargetingValueId);

            if (page.results != null) {
              int i = page.startIndex;
              foreach (Content content in page.results) {
                Console.WriteLine("{0})  Content with ID \"{1}\", name \"{2}\", and status " +
                    "\"{3}\" was found.", i, content.id, content.name, content.status);
                i++;
              }
            }
            offset += 500;
          } while (offset < page.totalResultSetSize);

          Console.WriteLine("Number of results found: " + page.totalResultSetSize);
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to get content by category. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
