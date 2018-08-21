// Copyright 2018, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.AdManager.Lib;
using Google.Api.Ads.AdManager.Util.v201808;
using Google.Api.Ads.AdManager.v201808;

using System;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v201808
{
    /// <summary>
    /// This example deletes a content metadata key hierarchy. To determine
    /// which content metadata key hierarchies exist, run
    /// GetAllContentMetadataKeyHierarchies.cs.
    /// </summary>
    public class DeleteContentMetadataKeyHierarchies : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This example deletes a content metadata key hierarchy. To determine " +
                    "which content metadata key hierarchies exist, run " +
                    "GetAllContentMetadataKeyHierarchies.cs.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            CreateCustomTargetingKeysAndValues codeExample =
                new CreateCustomTargetingKeysAndValues();
            Console.WriteLine(codeExample.Description);
            codeExample.Run(new AdManagerUser());
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (ContentMetadataKeyHierarchyService contentMetadataKeyHierarchyService =
                user.GetService<ContentMetadataKeyHierarchyService>())
            {
                // Set the ID of the content metadata key hierarchy to delete.
                long contentMetadataKeyHierarchyId =
                    long.Parse(_T("INSERT_CONTENT_METADATA_KEY_HIERARCHY_ID_HERE"));

                // Create a statement to select a content metadata key hierarchy.
                StatementBuilder statementBuilder = new StatementBuilder()
                    .Where("WHERE id = :id")
                    .OrderBy("id ASC")
                    .Limit(1)
                    .AddValue("id", contentMetadataKeyHierarchyId);

                try
                {
                    // Get content metadata key hierarchies by statement.
                    ContentMetadataKeyHierarchyPage page =
                        contentMetadataKeyHierarchyService
                            .getContentMetadataKeyHierarchiesByStatement(statementBuilder
                                .ToStatement());

                    ContentMetadataKeyHierarchy contentMetadataKeyHierarchy = page.results[0];

                    Console.WriteLine(
                        "Content metadata key hierarchy with ID \"{0}\" will be deleted.",
                        contentMetadataKeyHierarchy.id);

                    statementBuilder.RemoveLimitAndOffset();

                    // Create action.
                    Google.Api.Ads.AdManager.v201808.DeleteContentMetadataKeyHierarchies action =
                        new Google.Api.Ads.AdManager.v201808.DeleteContentMetadataKeyHierarchies();

                    // Perform action.
                    UpdateResult result =
                        contentMetadataKeyHierarchyService.performContentMetadataKeyHierarchyAction(
                            action, statementBuilder.ToStatement());

                    Console.WriteLine("Number of content metadata key hierarchies deleted: {0}",
                        result.numChanges);
                }
                catch (Exception e)
                {
                    Console.WriteLine(
                        "Failed to delete content metadata key hierarchies. " +
                        "Exception says \"{0}\"", e.Message);
                }
            }
        }
    }
}
