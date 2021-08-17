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
using Google.Api.Ads.AdManager.v202108;

using System;
using System.Linq;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v202108
{
    /// <summary>
    /// This code example creates new sites.
    /// </summary>
    public class CreateSites : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get { return "This code example creates new sites."; }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            CreateSites codeExample = new CreateSites();
            Console.WriteLine(codeExample.Description);

            string childNetworkCode = _T("INSERT_CHILD_NETWORK_CODE_HERE");
            string url = _T("INSERT_URL_HERE");

            codeExample.Run(new AdManagerUser(), childNetworkCode, url);
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user, string childNetworkCode, string url)
        {
            using (NetworkService networkService = user.GetService<NetworkService>())
            using (SiteService siteService = user.GetService<SiteService>())
            {
                Site site = new Site() {
                    childNetworkCode = childNetworkCode,
                    url = url
                };

                try
                {
                    Network currentNetwork = networkService.getCurrentNetwork();
                    // Validate that the site can be created.
                    bool validChildNetwork = currentNetwork.childPublishers
                        .Where(c => c.childNetworkCode == childNetworkCode)
                        .Where(c => c.approvedDelegationType == DelegationType.MANAGE_INVENTORY)
                        .Any();

                    if(!validChildNetwork)
                    {
                      throw new ArgumentException(string.Format("Child network {0} has not " +
                          "approved the current network ({}) to manage its inventory. Could not " +
                          "create site.", childNetworkCode, currentNetwork.networkCode));
                    }

                    // Create the sites on the server.
                    Site[] sites = siteService.createSites(new Site[] { site });

                    foreach (Site createdSite in sites)
                    {
                        Console.WriteLine("A site with ID \"{0}\" and URL \"{1}\" was created.",
                            createdSite.id, createdSite.url);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to create sites. Exception says \"{0}\"",
                        e.Message);
                }
            }
        }
    }
}
