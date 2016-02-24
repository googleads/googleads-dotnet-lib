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

using System;
using System.Collections.Generic;
using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.Util.v201602;
using Google.Api.Ads.Dfp.v201602;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201602 {

  /// <summary>
  /// This example updates a content metadata key hierarchy by adding a hierarchy level. To
  /// determine which content metadata key hierarchies exist, run
  /// GetAllContentMetadataKeyHierarchies.cs.
  /// </summary>
  class UpdateContentMetadataKeyHierarchies : SampleBase {

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This example updates a content metadata key hierarchy by adding a hierarchy " +
          "level. To determine which content metadata key hierarchies exist, run " +
          "GetAllContentMetadataKeyHierarchies.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new UpdateContentMetadataKeyHierarchies();
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

      // Set the ID of the content metadata key hierarchy to update.
      long contentMetadataKeyHierarchyId =
          long.Parse(_T("INSERT_CONTENT_METADATA_KEY_HIERARCHY_ID_HERE"));

      // Set the ID of the custom targeting key to be added as a hierarchy level
      long customTargetingKeyId = long.Parse(_T("INSERT_CUSTOM_TARGETING_KEY_ID_HERE"));

      // Create a statement to get content metadata key hierarchies.
      StatementBuilder statementBuilder = new StatementBuilder()
          .Where("WHERE id = :id")
          .OrderBy("id ASC")
          .Limit(1)
          .AddValue("id", contentMetadataKeyHierarchyId);

      try {
        ContentMetadataKeyHierarchyPage page = contentMetadataKeyHierarchyService
            .getContentMetadataKeyHierarchiesByStatement(statementBuilder.ToStatement());

        ContentMetadataKeyHierarchy contentMetadataKeyHierarchy = page.results[0];

        // Update the content metadata key hierarchy by adding a hierarchy level.
        ContentMetadataKeyHierarchyLevel[] hierarchyLevels = contentMetadataKeyHierarchy
            .hierarchyLevels;

        ContentMetadataKeyHierarchyLevel hierarchyLevel = new ContentMetadataKeyHierarchyLevel();
        hierarchyLevel.customTargetingKeyId = customTargetingKeyId;
        hierarchyLevel.hierarchyLevel = hierarchyLevels.Length + 1;

        List<ContentMetadataKeyHierarchyLevel> updatedHieratchyLevels =
           new List<ContentMetadataKeyHierarchyLevel>();
        updatedHieratchyLevels.AddRange(hierarchyLevels);
        updatedHieratchyLevels.Add(hierarchyLevel);

        contentMetadataKeyHierarchy.hierarchyLevels = updatedHieratchyLevels.ToArray();

        // Update the content hierarchy on the server.
        ContentMetadataKeyHierarchy[] contentMetadataKeyHierarchies =
            contentMetadataKeyHierarchyService.updateContentMetadataKeyHierarchies(
            new ContentMetadataKeyHierarchy[] {contentMetadataKeyHierarchy});

        foreach (ContentMetadataKeyHierarchy updatedContentMetadataKeyHierarchy in
            contentMetadataKeyHierarchies) {
          Console.WriteLine("Content metadata key hierarchy with ID \"{0}\", name " +
              "\"{1}\" was updated.", updatedContentMetadataKeyHierarchy.id,
              updatedContentMetadataKeyHierarchy.name);
        }
      } catch (Exception e) {
        Console.WriteLine("Failed to update content metadata key hierarchies. Exception " +
            "says \"{0}\"", e.Message);
      }
    }
  }
}
