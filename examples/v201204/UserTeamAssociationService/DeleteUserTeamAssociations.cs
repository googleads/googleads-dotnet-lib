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
using Google.Api.Ads.Dfp.Util.v201204;
using Google.Api.Ads.Dfp.v201204;

using System;

namespace Google.Api.Ads.Dfp.Examples.v201204 {
  /// <summary>
  /// This code example removes the user from all its teams. To determine which
  /// users exist, run GetAllUsers.cs.
  ///
  /// Tags:  UserTeamAssociationService.performUserTeamAssociationAction
  /// </summary>
  class DeleteUserTeamAssociation : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example removes the user from all its teams. To determine which " +
            "users exist, run GetAllUsers.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new DeleteUserTeamAssociation();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="dfpUser">The DFP user object running the code example.</param>
    public override void Run(DfpUser dfpUser) {
      // Get the UserTeamAssociationService.
      UserTeamAssociationService userTeamAssociationService = (UserTeamAssociationService)
          dfpUser.GetService(DfpService.v201204.UserTeamAssociationService);

      // Set the user to remove from its teams.
      long userId = long.Parse(_T("INSERT_USER_ID_HERE"));

      // Create filter text to select user team associations by the user ID.
      String statementText = "WHERE userId = :userId LIMIT 500";
      Statement filterStatement = new StatementBuilder("").AddValue("userId", userId).
          ToStatement();

      // Set defaults for page and offset.
      UserTeamAssociationPage page = new UserTeamAssociationPage();
      int offset = 0;

      try {
        do {
          // Create a statement to page through user team associations.
          filterStatement.query = statementText + " OFFSET " + offset;

          // Get user team associations by statement.
          page = userTeamAssociationService.getUserTeamAssociationsByStatement(filterStatement);

          if (page.results != null) {
            int i = page.startIndex;
            foreach (UserTeamAssociation userTeamAssociation in page.results) {
              Console.WriteLine("{0}) User team association between user with ID \"{1}\" and " +
                  "team with ID \"{2}\" will be deleted.", i, userTeamAssociation.userId,
                  userTeamAssociation.teamId);
              i++;
            }
          }

          offset += 500;
        } while (offset < page.totalResultSetSize);

        Console.WriteLine("Number of teams that the user will be removed from: "
            + page.totalResultSetSize);

        if (page.totalResultSetSize > 0) {
          // Modify statement for action.
          filterStatement.query = "WHERE userId = :userId";

          // Create action.
          DeleteUserTeamAssociations action = new DeleteUserTeamAssociations();

          // Perform action.
          UpdateResult result =
              userTeamAssociationService.performUserTeamAssociationAction(action, filterStatement);

          // Display results.
          if (result != null && result.numChanges > 0) {
            Console.WriteLine("Number of teams that the user was removed from: "
                + result.numChanges);
          } else {
            Console.WriteLine("No user team associations were deleted.");
          }
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to delete user team associations. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
