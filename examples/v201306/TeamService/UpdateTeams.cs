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
using Google.Api.Ads.Dfp.v201306;

using System;
using Google.Api.Ads.Dfp.Util.v201306;
using System.Collections.Generic;

namespace Google.Api.Ads.Dfp.Examples.v201306 {
  /// <summary>
  /// This code example updates teams by adding an ad unit to the first 5. To
  /// determine which teams exist, run GetAllTeams.cs. To determine which ad
  /// units exist, run GetAllAdUnits.cs.
  ///
  /// Tags: TeamService.getTeamsByStatement, TeamService.updateTeams
  /// </summary>
  class UpdateTeams : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example updates teams by adding an ad unit to the first 5. To " +
            "determine which teams exist, run GetAllTeams.cs. To determine which ad units " +
            "exist, run GetAllAdUnits.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new UpdateTeams();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the TeamService.
      TeamService teamService = (TeamService) user.GetService(DfpService.v201306.TeamService);

      // Set the ID of the ad unit to add to the teams.
      String adUnitId = _T("INSERT_AD_UNIT_ID_HERE");

      // Create a statement to select first 5 teams that aren't built-in.
      Statement filterStatement = new StatementBuilder("WHERE id > 0 LIMIT 5").ToStatement();

      try {
        // Get the teams by statement.
        TeamPage page = teamService.getTeamsByStatement(filterStatement);

        if (page.results != null) {
          Team[] teams = page.results;

          // Update each local team object by adding the ad unit to it.
          foreach (Team team in teams) {
            // Don't add ad unit if the team has all inventory already.
            if (!team.hasAllInventory) {
              List<String> adUnitIds = new List<String>();
              if (team.adUnitIds != null) {
                adUnitIds.AddRange(team.adUnitIds);
              }
              adUnitIds.Add(adUnitId);
              team.adUnitIds = adUnitIds.ToArray();
            }
          }

          // Update the teams on the server.
          teams = teamService.updateTeams(teams);

          if (teams != null) {
            foreach (Team team in teams) {
              Console.WriteLine("A team with ID \"{0}\" and name \"{1}\" was updated.",
                  team.id, team.name);
            }
          } else {
            Console.WriteLine("No teams updated.");
          }
        } else {
          Console.WriteLine("No teams found to update.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to update teams. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
