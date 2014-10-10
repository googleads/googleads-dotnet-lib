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
using Google.Api.Ads.Dfp.v201208;

using System;
using System.Collections.Generic;
using Google.Api.Ads.Dfp.Util.v201208;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201208 {
  /// <summary>
  /// This code example gets all cities available to target. A full list of
  /// available tables can be found at
  /// http://code.google.com/apis/dfp/docs/reference/v201208/PublisherQueryLanguageService.html.
  ///
  /// Tags: PublisherQueryLanguageService.select
  /// </summary>
  class GetAllCities : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets all cities available to target. A full list of available " +
            "tables can be found at " +
            "http://code.google.com/apis/dfp/docs/reference/v201208/" +
            "PublisherQueryLanguageService.html.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetAllCities();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the InventoryService.
      PublisherQueryLanguageService pqlService =
          (PublisherQueryLanguageService) user.GetService(
              DfpService.v201208.PublisherQueryLanguageService);

      // Create statement to select all targetable cities.
      // A limit of 500 is set here. You may want to page through such a large
      // result set. For criteria that do not have a "targetable" property, that
      // predicate may be left off, i.e. just "SELECT * FROM Browser_Groups
      // LIMIT 500"
      StatementBuilder statementBuilder =
          new StatementBuilder("SELECT * FROM City WHERE targetable = true LIMIT 500");

      try {
        // Get all cities.
        ResultSet resultSet = pqlService.select(statementBuilder.ToStatement());

        // Display results.
        Console.WriteLine(PqlUtilities.ResultSetToString(resultSet));
      } catch (Exception ex) {
        Console.WriteLine("Failed to get all cities. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
