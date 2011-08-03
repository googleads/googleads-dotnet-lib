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
using Google.Api.Ads.Dfp.v201103;

using System;

namespace Google.Api.Ads.Dfp.Examples.v201103 {
  /// <summary>
  /// This code example gets a creative by its ID. To determine which creatives
  /// exist, run GetAllCreatives.cs.
  ///
  /// Tags: CreativeService.getCreative
  /// </summary>
  class GetCreative : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets a creative by its ID. To determine which creatives " +
            "exist, run GetAllCreatives.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code sample as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetCreative();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the CreativeService.
      CreativeService creativeService =
          (CreativeService) user.GetService(DfpService.v201103.CreativeService);

      // Set the ID of the creative to get.
      long creativeId = long.Parse(_T("INSERT_CREATIVE_ID_HERE"));

      try {
        // Get the creative.
        Creative creative = creativeService.getCreative(creativeId);

        if (creative != null) {
          Console.WriteLine("Creative with ID ='{0}', name ='{1}' and type ='{2}' " +
              "was found.", creative.id, creative.name, creative.CreativeType);
        } else {
          Console.WriteLine("No creative found for this ID.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to get creative. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
