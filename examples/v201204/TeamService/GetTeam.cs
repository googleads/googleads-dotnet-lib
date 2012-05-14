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
using Google.Api.Ads.Dfp.v201204;

using System;
using Google.Api.Ads.Dfp.Util.v201204;
using System.Collections.Generic;

namespace Google.Api.Ads.Dfp.Examples.v201204 {
  /// <summary>
  /// This code example gets a team by its ID. To determine which teams exist,
  /// run GetAllTeams.cs.
  ///
  /// Tags: TeamService.getTeam
  /// </summary>
  class GetTeam : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets a team by its ID. To determine which teams exist, run " +
            "GetAllTeams.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetTeam();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the TeamService.
      TeamService teamService = (TeamService) user.GetService(DfpService.v201204.TeamService);

      // Set the ID of the team to get.
      long teamId = long.Parse(_T("INSERT_TEAM_ID_HERE"));

      try {
        // Get the team.
        Team team = teamService.getTeam(teamId);

        if (team != null) {
          Console.WriteLine("Team with ID \"{0}\"and name \"{1}\" was found.", team.id, team.name);
        } else {
          Console.WriteLine("No team found for this ID.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to get team by ID. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
