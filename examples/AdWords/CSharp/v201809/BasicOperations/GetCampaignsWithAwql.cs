// Copyright 2018 Google LLC
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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.Util.Reports.v201809;
using Google.Api.Ads.AdWords.v201809;

using System;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201809
{
    /// <summary>
    /// This code example lists all campaigns using an AWQL query. See
    /// https://developers.google.com/adwords/api/docs/guides/awql for AWQL
    /// documentation. To add a campaign, run AddCampaign.cs.
    /// </summary>
    public class GetCampaignsWithAwql : ExampleBase
    {
        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            GetCampaignsWithAwql codeExample = new GetCampaignsWithAwql();
            Console.WriteLine(codeExample.Description);
            try
            {
                codeExample.Run(new AdWordsUser());
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception occurred while running this code example. {0}",
                    ExampleUtilities.FormatException(e));
            }
        }

        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example lists all campaigns using an AWQL query. See " +
                    "https://developers.google.com/adwords/api/docs/guides/awql for AWQL " +
                    "documentation. To add a campaign, run AddCampaign.cs.";
            }
        }

        /// <summary>
        /// Runs the code example.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        public void Run(AdWordsUser user)
        {
            using (CampaignService campaignService =
                (CampaignService) user.GetService(AdWordsService.v201809.CampaignService))
            {
                // Create the query.
                SelectQuery query = new SelectQueryBuilder()
                    .Select(Campaign.Fields.Name, Campaign.Fields.Id, Campaign.Fields.Status)
                    .OrderByAscending(Campaign.Fields.Name)
                    .DefaultLimit()
                    .Build();

                CampaignPage page = new CampaignPage();
                int i = 0;

                try
                {
                    do
                    {
                        // Get the campaigns.
                        page = campaignService.query(query);
                        // Display the results.
                        if (page != null && page.entries != null)
                        {
                            foreach (Campaign campaign in page.entries)
                            {
                                Console.WriteLine(
                                    "{0}) Campaign with id = '{1}', name = '{2}' and status = " +
                                    "'{3}' was found.", i + 1, campaign.id, campaign.name,
                                    campaign.status);
                                i++;
                            }
                        }
                        query.NextPage(page);
                    } while (query.HasNextPage(page));

                    Console.WriteLine("Number of campaigns found: {0}", page.totalNumEntries);
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException("Failed to retrieve campaigns", e);
                }

            }
        }
    }
}
