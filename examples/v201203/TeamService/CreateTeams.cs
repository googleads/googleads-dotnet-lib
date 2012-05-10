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
using Google.Api.Ads.Dfp.v201203;

using System;
using Google.Api.Ads.Dfp.Util.v201203;
using System.Collections.Generic;

namespace Google.Api.Ads.Dfp.Examples.v201203 {
  /// <summary>
  /// This code example creates new teams with the logged in user added to each
  /// team. To determine which teams exist, run GetAllTeams.cs.
  ///
  /// Tags: TeamService.createTeams, UserService.getCurrentUser
  /// </summary>
  class CreateTeams : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example creates new teams with the logged in user added to each team. " +
            "To determine which teams exist, run GetAllTeams.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new CreateTeams();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the TeamService.
      TeamService teamService = (TeamService) user.GetService(DfpService.v201203.TeamService);

      // Get the UserService.
      UserService userService = (UserService) user.GetService(DfpService.v201203.UserService);

      try {
        // Get the current user's ID.
        long userId = userService.getCurrentUser().id;

        // Create an array to store local team objects.
        Team[] teams = new Team[5];

        for (int i = 0; i < 5; i++) {
          Team team = new Team();
          team.name = "Team #" + i;
          team.userIds = new long[] {userId};
          team.hasAllCompanies = true;
          team.hasAllInventory = true;
          teams[i] = team;
        }

        // Create the teams on the server.
        teams = teamService.createTeams(teams);

        if (teams != null && teams.Length > 0) {
          foreach (Team newTeam in teams) {
            Console.WriteLine("A team with ID \"{0}\", and name \"{1}\" was created.",
                newTeam.id, newTeam.name);
          }
        } else {
          Console.WriteLine("No teams created.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to create teams. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
