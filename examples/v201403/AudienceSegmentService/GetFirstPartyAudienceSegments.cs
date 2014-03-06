// Copyright 2014, Google Inc. All Rights Reserved.
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

// Author: api.anash@gmail.com (Anash P. Oommen)

using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.v201403;

using System;
using Google.Api.Ads.Dfp.Util.v201403;

namespace Google.Api.Ads.Dfp.Examples.v201403 {
  /// <summary>
  /// This code example gets all first party audience segments. To create first
  /// party audience segments, run CreateAudienceSegments.cs.
  ///
  /// Tags: AudienceSegmentService.getAudienceSegmentsByStatement
  /// </summary>
  class GetFirstPartyAudienceSegments : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets all first party audience segments. To create first " +
            "party audience segments, run CreateAudienceSegments.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetFirstPartyAudienceSegments();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the AudienceSegmentService.
      AudienceSegmentService audienceSegmentService =
          (AudienceSegmentService) user.GetService(DfpService.v201403.AudienceSegmentService);

      // Create a statement to only select first party audience segments.
      string statementText = "where type = :type order by id ASC LIMIT 500";
      Statement statement = new StatementBuilder(statementText).
          AddValue("type", "FIRST_PARTY").ToStatement();
      // Set defaults for page and Statement.
      AudienceSegmentPage page = new AudienceSegmentPage();
      int offset = 0;

      try {
        do {
          // Create a Statement to get all first party audience segments.
          statement.query = statementText + " OFFSET " + offset;

          // Get audience segment by Statement.
          page = audienceSegmentService.getAudienceSegmentsByStatement(statement);

          // Display results.
          if (page.results != null && page.results.Length > 0) {
            int i = page.startIndex;
            foreach (AudienceSegment segment in page.results) {
              Console.WriteLine("{0}) 'Audience segment with id \"{1}\" and name \"{2}\" of " +
                  "size {3} was found.", i, segment.id, segment.name, segment.size);
              i++;
            }
          }

          offset += 500;
        } while (offset < page.totalResultSetSize);
        Console.WriteLine("Number of results found: {0}", page.totalResultSetSize);
      } catch (Exception ex) {
        Console.WriteLine("Failed to get audience segment. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
