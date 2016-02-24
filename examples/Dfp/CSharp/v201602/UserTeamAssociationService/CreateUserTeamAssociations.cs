// Copyright 2015, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.v201602;

using System;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201602 {
  /// <summary>
  /// This code example adds a user to a team by creating an association
  /// between the two. To determine which teams exist, run GetAllTeams.cs. To
  /// determine which users exist, run GetAllUsers.cs.
  /// </summary>
  class CreateUserTeamAssociations : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example adds a user to a team by creating an association between the " +
            "two. To determine which teams exist, run GetAllTeams.cs. To determine which users " +
            "exist, run GetAllUsers.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new CreateUserTeamAssociations();
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
          dfpUser.GetService(DfpService.v201602.UserTeamAssociationService);

      // Set the users and team to add them to.
      long teamId = long.Parse(_T("INSERT_TEAM_ID_HERE"));
      long[] userIds = new long[] {long.Parse(_T("INSERT_USER_ID_HERE"))};

      // Create an array to store local user team association objects.
      UserTeamAssociation[] userTeamAssociations = new UserTeamAssociation[userIds.Length];

      // For each user, associate it with the given team.
      int i = 0;

      foreach (long userId in userIds) {
        UserTeamAssociation userTeamAssociation = new UserTeamAssociation();
        userTeamAssociation.userId = userId;
        userTeamAssociation.teamId = teamId;
        userTeamAssociations[i++] = userTeamAssociation;
      }

      try {
        // Create the user team associations on the server.
        userTeamAssociations =
            userTeamAssociationService.createUserTeamAssociations(userTeamAssociations);

        if (userTeamAssociations != null) {
          foreach (UserTeamAssociation userTeamAssociation in userTeamAssociations) {
            Console.WriteLine("A user team association between user with ID \"{0}\" and team " +
                "with ID \"{1}\" was created.", userTeamAssociation.userId,
                userTeamAssociation.teamId);
          }
        } else {
          Console.WriteLine("No user team associations created.");
        }
      } catch (Exception e) {
        Console.WriteLine("Failed to create user team associations. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}
