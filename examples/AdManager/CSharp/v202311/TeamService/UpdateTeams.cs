// Copyright 2019 Google LLC
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

using Google.Api.Ads.AdManager.Lib;
using Google.Api.Ads.AdManager.v202311;

using System;

using Google.Api.Ads.AdManager.Util.v202311;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v202311
{
    /// <summary>
    /// This code example updates a team by adding an ad unit to it. To
    /// determine which teams exist, run GetAllTeams.cs. To determine which ad
    /// units exist, run GetAllAdUnits.cs.
    /// </summary>
    public class UpdateTeams : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example updates a team by adding an ad unit to it. " +
                    "To determine which teams exist, run GetAllTeams.cs. " +
                    "To determine which ad units exist, run GetAllAdUnits.cs.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            UpdateTeams codeExample = new UpdateTeams();
            Console.WriteLine(codeExample.Description);
            codeExample.Run(new AdManagerUser());
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (TeamService teamService = user.GetService<TeamService>())
            {
                // Set the ID of the team to update.
                long teamId = long.Parse(_T("INSERT_TEAM_ID_HERE"));

                // Set the ID of the ad unit to add to the team.
                String adUnitId = _T("INSERT_AD_UNIT_ID_HERE");

                // Create a statement to select the team.
                StatementBuilder statementBuilder = new StatementBuilder()
                    .Where("id = :id")
                    .OrderBy("id ASC")
                    .Limit(1)
                    .AddValue("id", teamId);

                try
                {
                    // Get the teams by statement.
                    TeamPage page = teamService.getTeamsByStatement(statementBuilder.ToStatement());

                    Team team = page.results[0];
                    team.description = team.description + " - UPDATED";

                    // Update the teams on the server.
                    Team[] teams = teamService.updateTeams(new Team[]
                    {
                        team
                    });

                    if (teams != null)
                    {
                        foreach (Team updatedTeam in teams)
                        {
                            Console.WriteLine(
                                "A team with ID \"{0}\" and name \"{1}\" was updated.",
                                updatedTeam.id, updatedTeam.name);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No teams updated.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to update teams. Exception says \"{0}\"", e.Message);
                }
            }
        }
    }
}
