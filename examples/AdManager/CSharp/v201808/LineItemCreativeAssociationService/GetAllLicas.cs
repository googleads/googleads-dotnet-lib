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
    /// This example gets all line item creative associations.
    /// </summary>
    public class GetAllLicas : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get { return "This example gets all line item creative associations."; }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            GetAllLicas codeExample = new GetAllLicas();
            Console.WriteLine(codeExample.Description);
            try
            {
                codeExample.Run(new AdManagerUser());
            }
            catch (Exception e)
            {
                Console.WriteLine(
                    "Failed to get line item creative associations. Exception says \"{0}\"",
                    e.Message);
            }
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (LineItemCreativeAssociationService lineItemCreativeAssociationService =
                user.GetService<LineItemCreativeAssociationService>())
            {
                // Create a statement to select line item creative associations.
                int pageSize = StatementBuilder.SUGGESTED_PAGE_LIMIT;
                StatementBuilder statementBuilder = new StatementBuilder()
                    .OrderBy("lineItemId ASC, creativeId ASC")
                    .Limit(pageSize);

                // Retrieve a small amount of line item creative associations at a time, paging
                // through until all line item creative associations have been retrieved.
                int totalResultSetSize = 0;
                do
                {
                    LineItemCreativeAssociationPage page =
                        lineItemCreativeAssociationService
                            .getLineItemCreativeAssociationsByStatement(
                                statementBuilder.ToStatement());

                    // Print out some information for each line item creative association.
                    if (page.results != null)
                    {
                        totalResultSetSize = page.totalResultSetSize;
                        int i = page.startIndex;
                        foreach (LineItemCreativeAssociation lica in page.results)
                        {
                            if (lica.creativeSetId != 0)
                            {
                                Console.WriteLine(
                                    "{0}) Line item creative association with line item ID {1} " +
                                    "and creative set ID {2} was found.", i++, lica.lineItemId,
                                    lica.creativeSetId);
                            }
                            else
                            {
                                Console.WriteLine(
                                    "{0}) Line item creative association with line item ID {1} " +
                                    "and creative ID {2} was found.", i++, lica.lineItemId,
                                    lica.creativeId);
                            }
                        }
                    }

                    statementBuilder.IncreaseOffsetBy(pageSize);
                } while (statementBuilder.GetOffset() < totalResultSetSize);

                Console.WriteLine("Number of results found: {0}", totalResultSetSize);
            }
        }
    }
}
