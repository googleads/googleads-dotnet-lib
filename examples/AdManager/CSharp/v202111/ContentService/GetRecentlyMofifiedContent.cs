// Copyright 2020 Google LLC
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
using Google.Api.Ads.AdManager.Util.v202111;
using Google.Api.Ads.AdManager.v202111;

using System;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v202111
{
    /// <summary>
    /// This example gets recently modified content.
    /// </summary>
    public class GetRecentlyModifiedContent : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get { return "This example gets recently modified content."; }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            GetRecentlyModifiedContent codeExample = new GetRecentlyModifiedContent();
            Console.WriteLine(codeExample.Description);
            try
            {
                codeExample.Run(new AdManagerUser());
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to get content. Exception says \"{0}\"", e.Message);
            }
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (ContentService contentService = user.GetService<ContentService>())
            {
                // Create a statement to get recently modified content based on lastModifiedDateTime.
                // Changes to content buddle associations will update the lastModifiedDateTime, but
                // CMS metadata changes may not change the lastModifiedDateTime.
                int pageSize = StatementBuilder.SUGGESTED_PAGE_LIMIT;
                StatementBuilder statementBuilder = new StatementBuilder()
                  .Where("lastModifiedDateTime > :lastModifiedDateTime")
                  .OrderBy("id ASC")
                  .Limit(pageSize)
                  .AddValue("lastModifiedDateTime",
                        DateTimeUtilities.FromDateTime(System.DateTime.Now.AddDays(-1),
                            "America/New_York"));

                // Retrieve a small amount of content at a time, paging through until all
                // content have been retrieved.
                int totalResultSetSize = 0;
                do
                {
                    ContentPage page =
                        contentService.getContentByStatement(statementBuilder.ToStatement());

                    // Print out some information for each content.
                    if (page.results != null)
                    {
                        totalResultSetSize = page.totalResultSetSize;
                        int i = page.startIndex;
                        foreach (Content content in page.results)
                        {
                            var bundleIds = content.contentBundleIds ?? new long[0];
                            Console.WriteLine(
                                "{0}) Content with ID {1} and name \"{2}\" belonging" +
                                " to bundle IDs [{3}] was found.", i++,
                                content.id, content.name, string.Join(", ", bundleIds));
                        }
                    }

                    statementBuilder.IncreaseOffsetBy(pageSize);
                } while (statementBuilder.GetOffset() < totalResultSetSize);

                Console.WriteLine("Number of results found: {0}", totalResultSetSize);
            }
        }
    }
}
