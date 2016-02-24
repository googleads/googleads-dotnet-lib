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

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201602 {
  /// <summary>
  /// This code example updates a creative wrapper to the 'OUTER' wrapping
  /// order. To determine which creative wrappers exist, run
  /// GetAllCreativeWrappers.cs.
  /// </summary>
  class UpdateCreativeWrappers : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example updates a creative wrapper to the 'OUTER' wrapping order. To " +
            "determine which creative wrappers exist, run GetAllCreativeWrappers.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new UpdateCreativeWrappers();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Create the CreativeWrapperService.
      CreativeWrapperService creativeWrapperService = (CreativeWrapperService) user.GetService(
          DfpService.v201602.CreativeWrapperService);

      long creativeWrapperId = long.Parse(_T("INSERT_CREATIVE_WRAPPER_ID_HERE"));

      try {
        StatementBuilder statementBuilder = new StatementBuilder()
            .Where("id = :id")
            .OrderBy("id ASC")
            .Limit(1)
            .AddValue("id", creativeWrapperId);
        CreativeWrapperPage page = creativeWrapperService.getCreativeWrappersByStatement(
            statementBuilder.ToStatement());
        CreativeWrapper wrapper = page.results[0];

        wrapper.ordering = CreativeWrapperOrdering.OUTER;
        // Update the creative wrappers on the server.
        CreativeWrapper[] creativeWrappers = creativeWrapperService.updateCreativeWrappers(
            new CreativeWrapper[] {wrapper});

        // Display results.
        foreach (CreativeWrapper createdCreativeWrapper in creativeWrappers) {
          Console.WriteLine("Creative wrapper with ID '{0}' and wrapping order '{1}' was " +
              "updated.", createdCreativeWrapper.id, createdCreativeWrapper.ordering);
        }
      } catch (Exception e) {
        Console.WriteLine("Failed to update creative wrappers. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}
