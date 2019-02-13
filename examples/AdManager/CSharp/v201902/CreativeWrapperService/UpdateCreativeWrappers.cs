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
using Google.Api.Ads.AdManager.v201902;

using System;

using Google.Api.Ads.AdManager.Util.v201902;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v201902
{
    /// <summary>
    /// This code example updates a creative wrapper to the 'OUTER' wrapping
    /// order. To determine which creative wrappers exist, run
    /// GetAllCreativeWrappers.cs.
    /// </summary>
    public class UpdateCreativeWrappers : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example updates a creative wrapper to the 'OUTER' wrapping " +
                    "order. To determine which creative wrappers exist, " +
                    "run GetAllCreativeWrappers.cs.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            UpdateCreativeWrappers codeExample = new UpdateCreativeWrappers();
            Console.WriteLine(codeExample.Description);
            codeExample.Run(new AdManagerUser());
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (CreativeWrapperService creativeWrapperService =
                user.GetService<CreativeWrapperService>())
            {
                long creativeWrapperId = long.Parse(_T("INSERT_CREATIVE_WRAPPER_ID_HERE"));

                try
                {
                    StatementBuilder statementBuilder = new StatementBuilder()
                        .Where("id = :id")
                        .OrderBy("id ASC")
                        .Limit(1)
                        .AddValue("id", creativeWrapperId);
                    CreativeWrapperPage page =
                        creativeWrapperService.getCreativeWrappersByStatement(
                            statementBuilder.ToStatement());
                    CreativeWrapper wrapper = page.results[0];

                    wrapper.ordering = CreativeWrapperOrdering.OUTER;
                    // Update the creative wrappers on the server.
                    CreativeWrapper[] creativeWrappers =
                        creativeWrapperService.updateCreativeWrappers(new CreativeWrapper[]
                        {
                            wrapper
                        });

                    // Display results.
                    foreach (CreativeWrapper createdCreativeWrapper in creativeWrappers)
                    {
                        Console.WriteLine(
                            "Creative wrapper with ID '{0}' and wrapping order '{1}' was " +
                            "updated.", createdCreativeWrapper.id, createdCreativeWrapper.ordering);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to update creative wrappers. Exception says \"{0}\"",
                        e.Message);
                }
            }
        }
    }
}
