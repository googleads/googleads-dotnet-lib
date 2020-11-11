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
using Google.Api.Ads.AdManager.Util.v202011;
using Google.Api.Ads.AdManager.v202011;

using System;
using System.Collections;
using System.Collections.Generic;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v202011
{
    /// <summary>
    /// This example activates all CMS metadata values for the given key.
    /// </summary>
    public class ActivateCmsMetadataValues : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get { return "This example activates all CMS metadata values for the given key."; }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            ActivateCmsMetadataValues codeExample = new ActivateCmsMetadataValues();
            Console.WriteLine(codeExample.Description);

            long cmsMetadataKeyId = long.Parse(_T("INSERT_CMS_KEY_ID_HERE"));

            try
            {
                codeExample.Run(new AdManagerUser(), cmsMetadataKeyId);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to activate CMS metadata values. Exception says \"{0}\"",
                    e.Message);
            }
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user, long cmsMetadataKeyId)
        {
            using (CmsMetadataService cmsMetadataService = user.GetService<CmsMetadataService>())
            {
                // Create a statement to select CMS metadata values.
                int pageSize = StatementBuilder.SUGGESTED_PAGE_LIMIT;
                StatementBuilder statementBuilder = new StatementBuilder()
                    .Where("cmsKeyId = :cmsMetadataKeyId and status = :status")
                    .OrderBy("id ASC")
                    .Limit(pageSize)
                    .AddValue("cmsMetadataKeyId", cmsMetadataKeyId)
                    .AddValue("status", CmsMetadataValueStatus.INACTIVE.ToString());

                // Retrieve a small amount of CMS metadata values at a time, paging through until
                // all CMS metadata values have been retrieved.
                int totalResultSetSize = 0;

                do
                {
                    CmsMetadataValuePage page = cmsMetadataService.getCmsMetadataValuesByStatement(
                        statementBuilder.ToStatement());

                    if (page.results != null)
                    {
                        totalResultSetSize = page.totalResultSetSize;
                        int i = page.startIndex;
                        foreach (CmsMetadataValue cmsMetadataValue in page.results)
                        {
                            Console.WriteLine(
                                "{0}) CMS metadata value with ID {1} will be activated",
                                i++, cmsMetadataValue.cmsMetadataValueId);
                        }
                    }

                    statementBuilder.IncreaseOffsetBy(pageSize);
                } while (statementBuilder.GetOffset() < totalResultSetSize);

                Console.WriteLine("Number of CMS metadata values to be activated: {0}",
                        totalResultSetSize);

                if (totalResultSetSize > 0)
                {
                    // Modify statement.
                    statementBuilder.RemoveLimitAndOffset();

                    // Create action.
                    Google.Api.Ads.AdManager.v202011.ActivateCmsMetadataValues action =
                        new Google.Api.Ads.AdManager.v202011.ActivateCmsMetadataValues();

                    // Perform action.
                    UpdateResult result = cmsMetadataService.performCmsMetadataValueAction(action,
                            statementBuilder.ToStatement());

                    // Display results.
                    if (result != null && result.numChanges > 0)
                    {
                        Console.WriteLine("Number of CMS metadata values activated: {0}",
                            result.numChanges);
                    }
                    else
                    {
                        Console.WriteLine("No CMS metadata values were activated.");
                    }
                }
            }
        }
    }
}
