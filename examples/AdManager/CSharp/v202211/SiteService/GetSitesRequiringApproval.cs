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
using Google.Api.Ads.AdManager.Util.v202211;
using Google.Api.Ads.AdManager.v202211;

using System;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v202211
{
    /// <summary>
    /// This example gets sites under Multiple Customer Management requiring review.
    /// </summary>
    public class GetSitesRequiringApproval : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get { return "This example gets sites under Multiple Customer Management requiring " +
                "review."; }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            GetSitesRequiringApproval codeExample = new GetSitesRequiringApproval();
            Console.WriteLine(codeExample.Description);
            try
            {
                codeExample.Run(new AdManagerUser());
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to get sites. Exception says \"{0}\"", e.Message);
            }
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (SiteService siteService = user.GetService<SiteService>())
            {
                // Create a statement to select sites requiring approval.
                int pageSize = StatementBuilder.SUGGESTED_PAGE_LIMIT;
                StatementBuilder statementBuilder = new StatementBuilder()
                    .Where("approvalStatus = :approvalStatus")
                    .OrderBy("id ASC")
                    .Limit(pageSize)
                    .AddValue("approvalStatus", ApprovalStatus.REQUIRES_REVIEW.ToString());

                // Retrieve a small amount of sites at a time, paging through until all
                // sites have been retrieved.
                int totalResultSetSize = 0;
                do
                {
                    SitePage page =
                        siteService.getSitesByStatement(statementBuilder.ToStatement());

                    // Print out some information for each site.
                    if (page.results != null)
                    {
                        totalResultSetSize = page.totalResultSetSize;
                        int i = page.startIndex;
                        foreach (Site site in page.results)
                        {
                            Console.WriteLine(
                                "{0}) Site with ID {1} and URL \"{2}\" was found.", i++,
                                site.id, site.url);
                        }
                    }

                    statementBuilder.IncreaseOffsetBy(pageSize);
                } while (statementBuilder.GetOffset() < totalResultSetSize);

                Console.WriteLine("Number of results found: {0}", totalResultSetSize);
            }
        }
    }
}
