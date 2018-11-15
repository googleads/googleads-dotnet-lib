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
using Google.Api.Ads.AdManager.Util.v201811;
using Google.Api.Ads.AdManager.v201811;

using System;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v201811
{
    /// <summary>
    /// This code example updates image creatives. To create an image creative, run
    /// CreateCreatives.cs.
    /// </summary>
    public class UpdateCreatives : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return
                    "This code example updates image creatives. To create an image creative, run " +
                    "CreateCreatives.cs.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            UpdateCreatives codeExample = new UpdateCreatives();
            Console.WriteLine(codeExample.Description);
            codeExample.Run(new AdManagerUser());
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (CreativeService creativeService = user.GetService<CreativeService>())
            {
                // Set the ID of the creative to update.
                long creativeId = long.Parse(_T("INSERT_CREATIVE_ID_HERE"));

                // Create a statement to get all image creatives.
                Statement statement = new StatementBuilder().Where("id = :id").OrderBy("id ASC")
                    .Limit(1)
                    .AddValue("id", creativeId).ToStatement();

                try
                {
                    // Get creatives by statement.
                    CreativePage page = creativeService.getCreativesByStatement(statement);

                    Creative creative = page.results[0];

                    // Update local creative object by changing its destination URL.
                    if (creative is ImageCreative)
                    {
                        ImageCreative imageCreative = (ImageCreative) creative;
                        imageCreative.destinationUrl = "http://news.google.com";
                    }

                    // Update the creatives on the server.
                    Creative[] creatives = creativeService.updateCreatives(new Creative[]
                    {
                        creative
                    });

                    foreach (Creative updatedCreative in creatives)
                    {
                        if (creative is ImageCreative)
                        {
                            ImageCreative imageCreative = (ImageCreative) updatedCreative;
                            Console.WriteLine(
                                "An image creative with ID = '{0}' and destination URL ='{1}' " +
                                "was updated.", imageCreative.id, imageCreative.destinationUrl);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to update creatives. Exception says \"{0}\"",
                        e.Message);
                }
            }
        }
    }
}
