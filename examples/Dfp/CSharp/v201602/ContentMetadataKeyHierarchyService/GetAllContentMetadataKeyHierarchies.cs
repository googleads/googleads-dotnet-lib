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

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201602 {
  /// <summary>
  /// This code example gets all content metadata key hierarchies. To create
  /// content metadata key hierarchies, run
  /// CreateContentMetadataKeyHierarchies.cs.
  /// </summary>
  class GetAllContentMetadataKeyHierarchies : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets all content metadata key hierarchies. To create " +
            "content metadata key hierarchies, run CreateContentMetadataKeyHierarchies.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetAllContentMetadataKeyHierarchies();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the ContentMetadataKeyHierarchy service.
      ContentMetadataKeyHierarchyService contentMetadataKeyHierarchyService =
          (ContentMetadataKeyHierarchyService) user.GetService(
           DfpService.v201602.ContentMetadataKeyHierarchyService);

      // Create a statement to get all content metadata key hierarchies
      StatementBuilder statementBuilder = new StatementBuilder()
        .OrderBy("id ASC")
        .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT);

      try {
        int totalResultSetSize = 0;
        do {
          // Get content metadata key hierarchies by statement.
          ContentMetadataKeyHierarchyPage page = contentMetadataKeyHierarchyService
              .getContentMetadataKeyHierarchiesByStatement(statementBuilder.ToStatement());

          if (page.results != null) {
            totalResultSetSize = page.totalResultSetSize;
            int i = page.startIndex;
            foreach (ContentMetadataKeyHierarchy contentMetadataKeyHierarchy in page.results) {
              Console.WriteLine("{0}) Content metadata key hierarchy with ID \"{1}\", " +
                  "and name \"{2}\" was found.", i++, contentMetadataKeyHierarchy.id,
                  contentMetadataKeyHierarchy.name);
            }
          }

          statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
        } while (statementBuilder.GetOffset() < totalResultSetSize);
        Console.WriteLine("Number of results found: {0}", totalResultSetSize);
      } catch (Exception e) {
        Console.WriteLine("Failed to get content metadata key hierarchies. Exception " +
            "says \"{0}\"", e.Message);
      }
    }
  }
}
