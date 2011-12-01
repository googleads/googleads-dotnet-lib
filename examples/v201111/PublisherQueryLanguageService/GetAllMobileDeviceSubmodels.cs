// Copyright 2011, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.Dfp.v201111;

using System;
using System.Collections.Generic;
using Google.Api.Ads.Dfp.Util.v201111;

namespace Google.Api.Ads.Dfp.Examples.v201111 {
  /// <summary>
  /// This code example gets all mobile device submodels.
  ///
  /// Tags: PublisherQueryLanguageService.select
  /// </summary>
  class GetAllMobileDeviceSubmodels : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets all mobile device submodels.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetAllMobileDeviceSubmodels();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the PublisherQueryLanguageService.
      PublisherQueryLanguageService pqlService =
          (PublisherQueryLanguageService) user.GetService(
              DfpService.v201111.PublisherQueryLanguageService);

      // Create statement to select all mobile device submodels.
      StatementBuilder statementBuilder =
          new StatementBuilder("SELECT * FROM Mobile_Device_Submodel");

      try {
        // Get all mobile device submodels.
        ResultSet resultSet = pqlService.select(statementBuilder.ToStatement());

        // Display results.
        Console.WriteLine(PqlUtilities.ResultSetToString(resultSet));
      } catch (Exception ex) {
        Console.WriteLine("Failed to get all mobile device submodels. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
