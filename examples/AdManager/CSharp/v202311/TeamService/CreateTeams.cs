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

namespace Google.Api.Ads.AdManager.Examples.CSharp.v202311
{
    /// <summary>
    /// This code example creates new teams. To determine which teams exist, run
    /// GetAllTeams.cs.
    /// </summary>
    public class CreateTeams : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example creates new teams. To determine which teams exist, run " +
                    "GetAllTeams.cs.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            CreateTeams codeExample = new CreateTeams();
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
                try
                {
                    // Create an array to store local team objects.
                    Team[] teams = new Team[5];

                    for (int i = 0; i < 5; i++)
                    {
                        Team team = new Team();
                        team.name = "Team #" + i;
                        team.hasAllCompanies = false;
                        team.hasAllInventory = false;
                        teams[i] = team;
                    }

                    // Create the teams on the server.
                    teams = teamService.createTeams(teams);

                    if (teams != null && teams.Length > 0)
                    {
                        foreach (Team newTeam in teams)
                        {
                            Console.WriteLine(
                                "A team with ID \"{0}\", and name \"{1}\" was created.", newTeam.id,
                                newTeam.name);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No teams created.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to create teams. Exception says \"{0}\"", e.Message);
                }
            }
        }
    }
}
