// Copyright 2013, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.Dfp.v201308;

using System;
using Google.Api.Ads.Dfp.Util.v201308;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201308 {
  /// <summary>
  /// This code example updates a first party audience segment's member
  /// expiration days. To determine which first party audience segments exist,
  /// run GetFirstPartyAudienceSegments.cs.
  ///
  /// Tags: AudienceSegmentService.getAudienceSegmentsByStatement
  /// Tags: AudienceSegmentService.updateAudienceSegments
  /// </summary>
  class UpdateAudienceSegments : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example updates a first party audience segment's member expiration " +
            "days. To determine which first party audience segments exist, " +
            "run GetFirstPartyAudienceSegments.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new UpdateAudienceSegments();
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
          (AudienceSegmentService) user.GetService(DfpService.v201308.AudienceSegmentService);

      string audienceSegmentId = _T("INSERT_AUDIENCE_SEGMENT_ID_HERE");

      // Create a statement to only select a specified first party audience
      // segment.
      string statementText = "where id = :audienceSegmentId order by id ASC " +
          "LIMIT 1";
      Statement statement = new StatementBuilder(statementText)
          .AddValue("audienceSegmentId", audienceSegmentId)
          .ToStatement();

      try {
        // Get the audience segment.
        RuleBasedFirstPartyAudienceSegment audienceSegment =
            (RuleBasedFirstPartyAudienceSegment) audienceSegmentService
                .getAudienceSegmentsByStatement(statement).results[0];

        // Update the member expiration days.
        audienceSegment.membershipExpirationDays = 180;

        // Update the audience segment on the server.
        AudienceSegment[] audienceSegments = audienceSegmentService.updateAudienceSegments(
            new FirstPartyAudienceSegment[] {audienceSegment});

        foreach (AudienceSegment updatedAudienceSegment in audienceSegments) {
          Console.WriteLine(
              "Audience segment with ID \"{0}\" and name \"{1}\" was updated.\n",
              updatedAudienceSegment.id, updatedAudienceSegment.name);
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to update audience segment. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
