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
  /// This code example creates content metadata key hierachries. To determine which content
  /// metadata key hierachries exist, run GetAllContentMetadataKeyHierarchies.cs
  /// </summary>
  class CreateContentMetadataKeyHierarchies : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example creates content metadata key hierachries. To determine " +
            "which content metadata key hierachries exist, run " +
            "GetAllContentMetadataKeyHierarchies.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new CreateContentMetadataKeyHierarchies();
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

      // Set the IDs of the custom targeting keys for the hierarchy.
      long customTargetingKeyId1 = long.Parse(_T("INSERT_LEVEL_ONE_CUSTOM_TARGETING_KEY_ID_HERE"));
      long customTargetingKeyId2 = long.Parse(_T("INSERT_LEVEL_TWO_CUSTOM_TARGETING_KEY_ID_HERE"));

      List<ContentMetadataKeyHierarchyLevel> hierarchyLevels =
          new List<ContentMetadataKeyHierarchyLevel>();

      ContentMetadataKeyHierarchyLevel hierarchyLevel1 = new ContentMetadataKeyHierarchyLevel();
      hierarchyLevel1.customTargetingKeyId = customTargetingKeyId1;
      hierarchyLevel1.hierarchyLevel = 1;
      hierarchyLevels.Add(hierarchyLevel1);

      ContentMetadataKeyHierarchyLevel hierarchyLevel2 = new ContentMetadataKeyHierarchyLevel();
      hierarchyLevel2.customTargetingKeyId = customTargetingKeyId2;
      hierarchyLevel2.hierarchyLevel = 2;
      hierarchyLevels.Add(hierarchyLevel2);

      ContentMetadataKeyHierarchy contentMetadataKeyHierarchy = new ContentMetadataKeyHierarchy();
      contentMetadataKeyHierarchy.name = "Content hierarchy #" + new Random().Next(int.MaxValue);
      contentMetadataKeyHierarchy.hierarchyLevels = hierarchyLevels.ToArray();

      try {
        // Create the content metadata key hierarchy on the server.
        ContentMetadataKeyHierarchy[] contentMetadataKeyHierarchies =
            contentMetadataKeyHierarchyService.createContentMetadataKeyHierarchies(
            new ContentMetadataKeyHierarchy[] {contentMetadataKeyHierarchy});

        foreach (ContentMetadataKeyHierarchy createdContentMetadataKeyHierarchy in
            contentMetadataKeyHierarchies) {
          Console.WriteLine("A content metadata key hierarchy with ID \"{0}\", name " +
              "\"{1}\" and {2} levels was created.", createdContentMetadataKeyHierarchy.id,
              createdContentMetadataKeyHierarchy.name,
              createdContentMetadataKeyHierarchy.hierarchyLevels.Length);
        }
      } catch (Exception e) {
        Console.WriteLine("Failed to create content metadata key hierarchies. Exception says " +
            "\"{0}\"", e.Message);
      }
    }
  }
}
