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
  ///  This code example updates user team associations by setting the
  ///  overridden access type to read only for all teams that the user belongs
  ///  to. To determine which users exists, run GetAllUsers.cs.
  ///
  /// Tags: UserTeamAssociationService.getUserTeamAssociationsByStatement
  /// Tags: UserTeamAssociationService.updateUserTeamAssociations
  /// </summary>
  class UpdateUserTeamAssociations : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example updates user team associations by setting the overridden " +
            "access type to read only for all teams that the user belongs to. To determine " +
            "which users exists, run GetAllUsers.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new UpdateUserTeamAssociations();
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
              DfpService.v201204.UserTeamAssociationService);

      // Set the user to set to read only access within its teams.
      long userId = long.Parse(_T("INSERT_USER_ID_HERE"));

      // Create filter text to select user team associations by the user ID.
      String statementText = "WHERE userId = :userId LIMIT 500";
      Statement filterStatement = new StatementBuilder(statementText).
          AddValue("userId", userId).ToStatement();

      try {
        // Get user team associations by statement.
        UserTeamAssociationPage page =
            userTeamAssociationService.getUserTeamAssociationsByStatement(filterStatement);

        if (page.results != null) {
          UserTeamAssociation[] userTeamAssociations = page.results;

          // Update each local user team association to read only access.
          foreach (UserTeamAssociation userTeamAssociation in userTeamAssociations) {
            userTeamAssociation.overriddenTeamAccessType = TeamAccessType.READ_ONLY;
          }

          // Update the user team associations on the server.
          userTeamAssociations =
              userTeamAssociationService.updateUserTeamAssociations(userTeamAssociations);

          if (userTeamAssociations != null) {
            foreach (UserTeamAssociation userTeamAssociation in userTeamAssociations) {
              Console.WriteLine("User team association between user with ID \"{0}\" and team " +
                  "with ID \"{1}\" was updated to access type \"{2}\".", userTeamAssociation.userId,
                  userTeamAssociation.teamId, userTeamAssociation.overriddenTeamAccessType);
            }
          } else {
            Console.WriteLine("No user team associations updated.");
          }
        } else {
          Console.WriteLine("No user team associations found to update.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to update user team associations. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
