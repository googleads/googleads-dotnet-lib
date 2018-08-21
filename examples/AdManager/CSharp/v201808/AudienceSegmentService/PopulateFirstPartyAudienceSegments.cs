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
    /// This code example populates a specific rule base first party audience
    /// segment. To determine which audience segments exist, run
    /// GetAllAudienceSegments.cs.
    /// </summary>
    public class PopulateFirstPartyAudienceSegments : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example populates a specific rule base first party audience " +
                    "segment. To determine which audience segments exist, " +
                    "run GetAllAudienceSegments.cs.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            PopulateFirstPartyAudienceSegments codeExample =
                new PopulateFirstPartyAudienceSegments();
            Console.WriteLine(codeExample.Description);
            codeExample.Run(new AdManagerUser());
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (AudienceSegmentService audienceSegmentService =
                user.GetService<AudienceSegmentService>())
            {
                long audienceSegmentId = long.Parse(_T("INSERT_AUDIENCE_SEGMENT_ID_HERE"));

                // Create a statement to only select a specified first party audience
                // segment.
                StatementBuilder statementBuilder = new StatementBuilder()
                    .Where("id = :audienceSegmentId").OrderBy("id ASC").Limit(1)
                    .AddValue("audienceSegmentId", audienceSegmentId);

                try
                {
                    // Get audience segment by statement.
                    AudienceSegment audienceSegment = audienceSegmentService
                        .getAudienceSegmentsByStatement(statementBuilder.ToStatement()).results[0];

                    Console.WriteLine(
                        "Audience segment with id \"{0}\" and name \"{1}\" " + "will be populated",
                        audienceSegment.id, audienceSegment.name);
                    // Create action.
                    PopulateAudienceSegments action = new PopulateAudienceSegments();

                    // Perform action.
                    UpdateResult result =
                        audienceSegmentService.performAudienceSegmentAction(action,
                            statementBuilder.ToStatement());

                    if (result != null && result.numChanges > 0)
                    {
                        Console.WriteLine("Number of audience segments populated: {0}",
                            result.numChanges);
                    }
                    else
                    {
                        Console.WriteLine("No audience segments were populated.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to populate audience segment. Exception says \"{0}\"",
                        e.Message);
                }
            }
        }
    }
}
