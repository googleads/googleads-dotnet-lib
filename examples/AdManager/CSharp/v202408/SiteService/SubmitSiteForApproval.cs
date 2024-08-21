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
using Google.Api.Ads.AdManager.Util.v202408;
using Google.Api.Ads.AdManager.v202408;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v202408
{
    /// <summary>
    /// This example submits a site for approval.
    /// </summary>
    public class SubmitSiteForApproval : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This example submits a site for approval.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            SubmitSiteForApproval codeExample = new SubmitSiteForApproval();
            Console.WriteLine(codeExample.Description);

            // Set the ID of the site.
            long siteId = long.Parse(_T("INSERT_SITE_ID_HERE"));

            codeExample.Run(new AdManagerUser(), siteId);
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user, long siteId)
        {
            using (SiteService siteService = user.GetService<SiteService>())
            {
                // Create statement to select the site.
                StatementBuilder statementBuilder = new StatementBuilder()
                    .Where("id = :id")
                    .OrderBy("id ASC")
                    .Limit(1)
                    .AddValue("id", siteId);

                // Set default for page.
                SitePage page = new SitePage();
                List<string> siteIds = new List<string>();
                int i = 0;

                try
                {
                    do
                    {
                        // Get sites by statement.
                        page = siteService.getSitesByStatement(
                            statementBuilder.ToStatement());

                        if (page.results != null)
                        {
                            foreach (Site site in page.results)
                            {
                                Console.WriteLine(
                                    "{0}) Site with ID = '{1}', URL = '{2}', " +
                                    "and approval status = '{3}' will be submitted for approval.",
                                    i++, site.id, site.url, site.approvalStatus);
                                siteIds.Add(site.id.ToString());
                            }
                        }

                        statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
                    } while (statementBuilder.GetOffset() < page.totalResultSetSize);

                    Console.WriteLine("Number of sites to be submitted for approval: {0}",
                        siteIds.Count);

                    if (siteIds.Count > 0)
                    {
                        // Modify statement for action.
                        statementBuilder.RemoveLimitAndOffset();

                        // Create action.
                        Google.Api.Ads.AdManager.v202408.SubmitSiteForApproval action =
                            new Google.Api.Ads.AdManager.v202408.SubmitSiteForApproval();

                        // Perform action.
                        UpdateResult result =
                            siteService.performSiteAction(action,
                                statementBuilder.ToStatement());

                        // Display results.
                        if (result != null && result.numChanges > 0)
                        {
                            Console.WriteLine(
                                "Number of sites that were submitted for approval: {0}",
                                result.numChanges);
                        }
                        else
                        {
                            Console.WriteLine("No sites were submitted for approval.");
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(
                        "Failed to send sites to Marketplace. Exception says \"{0}\"",
                        e.Message);
                }
            }
        }
    }
}
