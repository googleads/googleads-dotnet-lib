// Copyright 2013, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.Dfa.Lib;
using Google.Api.Ads.Dfa.v1_20;

using System;
using System.Collections.Generic;
using System.Text;
using Google.Api.Ads.Common.Util;

namespace Google.Api.Ads.Dfa.Examples.CSharp.v1_20 {
  /// <summary>
  /// This code example creates a new activity group for a given spotlight
  /// configuration. To get spotLight tag configuration, run GetAdvertisers.cs.
  /// To get activity types, run GetActivityTypes.cs.
  /// </summary>
  class CreateSpotlightActivityGroup : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example creates a new activity group for a given spotlight " +
            "configuration. To get spotLight tag configuration, run GetAdvertisers.cs. To get " +
            "activity types, run GetActivityTypes.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new CreateSpotlightActivityGroup();
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
          DfaService.v1_20.SpotlightRemoteService);

      long spotlightConfigurationId = long.Parse(_T("INSERT_SPOTLIGHT_CONFIGURATION_ID_HERE"));
      int activityType = int.Parse(_T("INSERT_ACTIVITY_TYPE_HERE"));
      string groupName = _T("INSERT_GROUP_NAME_HERE");

      // Set spotlight activity group structure.
      SpotlightActivityGroup spotlightActivityGroup = new SpotlightActivityGroup();
      spotlightActivityGroup.id = 0;
      spotlightActivityGroup.spotlightConfigurationId = spotlightConfigurationId;
      spotlightActivityGroup.groupType = activityType;
      spotlightActivityGroup.name = groupName;

      try {
        // Create the activity group.
        SpotlightActivityGroupSaveResult result = service.saveSpotlightActivityGroup(
            spotlightActivityGroup);

        // Display activity group id.
        if (result != null) {
          Console.WriteLine("Activity group with id \"{0}\" was created.", result.id);
        }
      } catch (Exception e) {
        Console.WriteLine("Failed to create spotlight activity group. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}
