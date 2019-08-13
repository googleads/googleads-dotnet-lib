// Copyright 2019 Google LLC
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
using Google.Api.Ads.AdManager.Util.v201908;
using Google.Api.Ads.AdManager.v201908;

using System;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v201908
{
    /// <summary>
    /// This example gets all targeting presets.
    /// </summary>
    public class GetAllTargetingPresets : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get { return "This example gets all targeting presets."; }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            GetAllTargetingPresets codeExample = new GetAllTargetingPresets();
            Console.WriteLine(codeExample.Description);
            try
            {
                codeExample.Run(new AdManagerUser());
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to get targeting presets. Exception says \"{0}\"",
                    e.Message);
            }
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (TargetingPresetService targetingPresetService =
                user.GetService<TargetingPresetService>())
            {
                // Create a statement to select targeting presets.
                int pageSize = StatementBuilder.SUGGESTED_PAGE_LIMIT;
                StatementBuilder statementBuilder =
                    new StatementBuilder().OrderBy("id ASC").Limit(pageSize);

                // Retrieve a small amount of targeting presets at a time, paging through until all
                // targeting presets have been retrieved.
                int totalResultSetSize = 0;
                do
                {
                    TargetingPresetPage page =
                        targetingPresetService.getTargetingPresetsByStatement(
                            statementBuilder.ToStatement());

                    // Print out some information for each targetingPreset.
                    if (page.results != null)
                    {
                        totalResultSetSize = page.totalResultSetSize;
                        int i = page.startIndex;
                        foreach (TargetingPreset targetingPreset in page.results)
                        {
                            Console.WriteLine(
                                "{0}) Targeting preset with ID {1} and name \"{2}\" was found.", i++,
                                targetingPreset.id, targetingPreset.name);
                        }
                    }

                    statementBuilder.IncreaseOffsetBy(pageSize);
                } while (statementBuilder.GetOffset() < totalResultSetSize);

                Console.WriteLine("Number of results found: {0}", totalResultSetSize);
            }
        }
    }
}
