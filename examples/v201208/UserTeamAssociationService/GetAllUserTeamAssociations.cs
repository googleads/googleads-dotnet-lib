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
  /// This code example gets all user team associations. To create user team
  /// associations, run CreateUserTeamAssociations.cs.
  ///
  /// Tags: UserTeamAssociationService.getUserTeamAssociationsByStatement
  /// </summary>
  class GetAllUserTeamAssociations : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets all user team associations. To create user team " +
            "associations, run CreateUserTeamAssociations.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetAllUserTeamAssociations();
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
          dfpUser.GetService(DfpService.v201208.UserTeamAssociationService);

      // Set defaults for page and filterStatement.
      UserTeamAssociationPage page = new UserTeamAssociationPage();
      Statement filterStatement = new Statement();
      int offset = 0;

      try {
        do {
          // Create a statement to get all user team associations.
          filterStatement.query = "LIMIT 500 OFFSET " + offset;

          // Get user team associations by statement.
          page = userTeamAssociationService.getUserTeamAssociationsByStatement(filterStatement);

          if (page.results != null) {
            int i = page.startIndex;
            foreach (UserTeamAssociation userTeamAssociation in page.results) {
              Console.WriteLine("{0}) User team association between user with ID \"{1}\" and " +
                  "team with ID \"{2}\" was found.", i, userTeamAssociation.userId,
                  userTeamAssociation.teamId);
              i++;
            }
          }

          offset += 500;
        } while (offset < page.totalResultSetSize);

        Console.WriteLine("Number of results found: {0}." + page.totalResultSetSize);
      } catch (Exception ex) {
        Console.WriteLine("Failed to get user team associations. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
