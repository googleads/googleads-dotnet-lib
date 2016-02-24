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
using Google.Api.Ads.Dfp.Util.v201602;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201602 {
  /// <summary>
  /// This code example gets all teams ordered by name. To create teams,
  /// run CreateTeams.cs.
  /// </summary>
  class GetTeamsByStatement : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets all teams ordered by name. To create teams, run " +
            "CreateTeams.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetTeamsByStatement();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the TeamService.
      TeamService teamService = (TeamService) user.GetService(DfpService.v201602.TeamService);

      // Create a statement to order teams by name.
      StatementBuilder statementBuilder = new StatementBuilder()
          .OrderBy("name ASC")
          .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT);

      // Set default for page.
      TeamPage page = new TeamPage();

      try {
        do {
          // Get teams by statement.
          page = teamService.getTeamsByStatement(statementBuilder.ToStatement());

          // Display results.
          if (page.results != null) {
            int i = page.startIndex;
            foreach (Team team in page.results) {
              Console.WriteLine("{0}) Team with ID \"{1}\" and name \"{2}\" was found.",
                  i, team.id, team.name);
              i++;
            }
          }

          statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
        } while(statementBuilder.GetOffset() < page.totalResultSetSize);
      Console.WriteLine("Number of results found: " + page.totalResultSetSize);
      } catch (Exception e) {
        Console.WriteLine("Failed to get teams by statement. Exception says \"{0}\"", e.Message);
      }
    }
  }
}
