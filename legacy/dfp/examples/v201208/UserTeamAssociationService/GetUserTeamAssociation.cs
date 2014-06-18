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
using Google.Api.Ads.Dfp.Util.v201208;
using Google.Api.Ads.Dfp.v201208;

using System;

namespace Google.Api.Ads.Dfp.Examples.v201208 {
  /// <summary>
  ///  This code example gets a user team association by the user and team ID.
  ///  To determine which teams exist, run GetAllTeams.cs. To determine which
  ///  users exist, run GetAllUsers.cs.
  ///
  /// Tags: UserTeamAssociationService.getUserTeamAssociation
  /// </summary>
  class GetUserTeamAssociation : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets a user team association by the user and team ID. To " +
            "determine which teams exist, run GetAllTeams.cs. To determine which users exist, " +
            "run GetAllUsers.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetUserTeamAssociation();
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
              DfpService.v201208.UserTeamAssociationService);

      // Set the IDs of the user and team to get the association for.
      long userId = long.Parse(_T("INSERT_USER_ID_HERE"));
      long teamId = long.Parse(_T("INSERT_TEAM_ID_HERE"));

      try {
        // Get the user team association.
        UserTeamAssociation userTeamAssociation = userTeamAssociationService.getUserTeamAssociation(
            teamId, userId);

        if (userTeamAssociation != null) {
          Console.WriteLine("User team association between user with ID \"{0}\" and team with " +
              "ID \"{1}\" was found.", userTeamAssociation.userId, userTeamAssociation.teamId);
        } else {
          Console.WriteLine("No user team association found.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to get user team associations. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
