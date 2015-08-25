// Copyright 2012, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.Dfa.Lib;
using Google.Api.Ads.Dfa.v1_19;

using System;
using System.Collections.Generic;
using System.Text;
using Google.Api.Ads.Common.Util;

namespace Google.Api.Ads.Dfa.Examples.CSharp.v1_19 {
  /// <summary>
  /// This code example displays available content categories for a given search
  /// string. Results are limited to 10.
  /// </summary>
  class GetContentCategories : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example displays available content categories for a given search " +
            "string. Results are limited to 10.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetContentCategories();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfaUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The Dfa user object running the code example.
    /// </param>
    public override void Run(DfaUser user) {
      // Create CreativeRemoteService instance.
      ContentCategoryRemoteService service = (ContentCategoryRemoteService) user.GetService(
          DfaService.v1_19.ContentCategoryRemoteService);

      String searchString = _T("INSERT_SEARCH_STRING_CRITERIA_HERE");

      // Create content category search criteria structure.
      ContentCategorySearchCriteria searchCriteria = new ContentCategorySearchCriteria();
      searchCriteria.pageSize = 10;
      searchCriteria.searchString = searchString;

      try {
        // Get content category record set.
        ContentCategoryRecordSet contentCategoryRecordSet =
            service.getContentCategories(searchCriteria);

        // Display content category names, ids and descriptions.
        if (contentCategoryRecordSet != null && contentCategoryRecordSet.records != null) {
          foreach (ContentCategory contentCategory in contentCategoryRecordSet.records) {
            Console.WriteLine("Content category with name \"{0}\" and id \"{1}\" was found.",
                contentCategory.name, contentCategory.id);
          }
        } else {
          Console.WriteLine("No content categories found for your search criteria.");
        }
      } catch (Exception e) {
        Console.WriteLine("Failed to get content categories. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}
