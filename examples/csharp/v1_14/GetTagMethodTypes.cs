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

using Google.Api.Ads.Dfa.Lib;
using Google.Api.Ads.Dfa.v1_14;

using System;
using System.Collections.Generic;
using System.Text;
using Google.Api.Ads.Common.Util;

namespace Google.Api.Ads.Dfa.Examples.CSharp.v1_14 {
  /// <summary>
  /// This code example displays activity method type names and ids.
  ///
  /// Tags: spotlight.getSpotlightTagMethodTypes
  /// </summary>
  class GetTagMethodTypes : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example displays activity method type names and ids.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetTagMethodTypes();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfaUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The Dfa user object running the code example.
    /// </param>
    public override void Run(DfaUser user) {
      // Create SpotlightRemoteService instance.
      SpotlightRemoteService service = (SpotlightRemoteService) user.GetService(
          DfaService.v1_14.SpotlightRemoteService);

      try {
        // Get method types.
        SpotlightTagMethodType[] spotlightTagMethodTypes = service.getSpotlightTagMethodTypes();

        //Display method type names and ids.
        foreach (SpotlightTagMethodType result in spotlightTagMethodTypes) {
          Console.WriteLine("Method type with name \"{0}\" and id \"{1}\" was found.",
              result.name, result.id);
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to retrieve tag method types. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
