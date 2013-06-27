// Copyright 2012, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.Dfp.Util.v201211;
using Google.Api.Ads.Dfp.v201211;

using System;

namespace Google.Api.Ads.Dfp.Examples.v201211 {
  /// <summary>
  ///  This code example gets all teams that the current user belongs to. The
  ///  statement retrieves up to the maximum page size limit of 500. To create
  ///  teams, run CreateTeams.cs.
  ///
  /// Tags: UserTeamAssociationService.getUserTeamAssociationsByStatement
  /// Tags: UserService.getCurrentUser
  /// </summary>
  class GetUserTeamAssociationsByStatement : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets all teams that the current user belongs to. The " +
            "statement retrieves up to the maximum page size limit of 500. To create teams, " +
            "run CreateTeams.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetUserTeamAssociationsByStatement();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="dfpUser">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the UserTeamAssociationService.
      UserTeamAssociationService userTeamAssociationService =
          (UserTeamAssociationService) user.GetService(
              DfpService.v201211.UserTeamAssociationService);

      // Get the UserService.
      UserService userService = (UserService) user.GetService(DfpService.v201211.UserService);

      try {
        // Get the current user.
        long currentUserId = userService.getCurrentUser().id;

        // Create filter text to select user team associations by the user ID.
        String statementText = "WHERE userId = :userId LIMIT 500";
        Statement filterStatement = new StatementBuilder(statementText).
            AddValue("userId", currentUserId).ToStatement();

        // Get user team associations by statement.
        UserTeamAssociationPage page =
            userTeamAssociationService.getUserTeamAssociationsByStatement(filterStatement);

        // Display results.
        if (page.results != null) {
          int i = page.startIndex;
          foreach (UserTeamAssociation userTeamAssociation in page.results) {
            Console.WriteLine("{0}) User team association between user with ID \"{1}\" and team " +
                "with ID \"{2}\" was found.", i, userTeamAssociation.userId,
                userTeamAssociation.teamId);
            i++;
          }
        }

        Console.WriteLine("Number of results found: " + page.totalResultSetSize);
      } catch (Exception ex) {
        Console.WriteLine("Failed to get user team associations. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
