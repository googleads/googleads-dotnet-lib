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
using Google.Api.Ads.Dfa.v1_13;

using System;
using System.Collections.Generic;
using System.Text;
using Google.Api.Ads.Common.Util;

namespace Google.Api.Ads.Dfa.Examples.v1_13 {
  /// <summary>
  /// This code example displays user filter criteria type names and ids.
  ///
  /// Tags: user.getAvailableUserFilterCriteriaTypes
  /// </summary>
  class GetUserFilterTypes : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example displays user filter criteria type names and ids.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetUserFilterTypes();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfaUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The Dfa user object running the code example.
    /// </param>
    public override void Run(DfaUser user) {
      // Create UserRemoteService instance.
      UserRemoteService service = (UserRemoteService) user.GetService(
          DfaService.v1_12.UserRemoteService);

      try {
        // Get user filter criteria types.
        UserFilterCriteriaType[] userFilterCriteriaTypes =
            service.getAvailableUserFilterCriteriaTypes();

        // Display user filter criteria types.
        if (userFilterCriteriaTypes != null) {
          foreach (UserFilterCriteriaType userFilterCriteriaType in userFilterCriteriaTypes) {
            Console.WriteLine("User Filter Criteria type with name \"{0}\" and id \"{1}\" " +
                "was found.", userFilterCriteriaType.name, userFilterCriteriaType.id);
          }
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to retrieve user filter and criteria types. Exception " +
            "says \"{0}\"", ex.Message);
      }
    }
  }
}
