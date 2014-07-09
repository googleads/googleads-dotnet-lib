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

// Author: api.anash@gmail.com (Anash P. Oommen)

using Google.Api.Ads.Dfa.Lib;
using Google.Api.Ads.Dfa.v1_20;

using System;
using System.Collections.Generic;
using System.Text;
using Google.Api.Ads.Common.Util;

namespace Google.Api.Ads.Dfa.Examples.CSharp.v1_20 {
  /// <summary>
  /// This code example creates a spotlight activity in a given activity group.
  /// To create an activity group, run CreateSpotlightActivityGroup.cs. To get
  /// tag methods types, run GetTagMethodTypes.cs. To get activity type ids,
  /// run GetActivityTypes.cs. To get available countries, run GetCountries.cs.
  ///
  /// Tags: spotlight.saveSpotlightActivity
  /// </summary>
  class CreateSpotlightActivity : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example creates a spotlight activity in a given activity group. To " +
            "create an activity group, run CreateSpotlightActivityGroup.cs. To get tag methods " +
            "types, run GetTagMethodTypes.cs. To get activity type ids, run GetActivityTypes.cs" +
            ". To get available countries, run GetCountries.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new CreateSpotlightActivity();
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

      long activityGroupId = long.Parse(_T("INSERT_ACTIVITY_GROUP_ID_HERE"));
      long activityTypeId = long.Parse(_T("INSERT_ACTIVITY_TYPE_ID_HERE"));
      long tagMethodTypeId = long.Parse(_T("INSERT_TAG_METHOD_TYPE_ID_HERE"));
      long countryId = long.Parse(_T("INSERT_COUNTRY_ID_HERE"));
      string url = _T("INSERT_EXPECTED_URL_HERE");
      string activityName = _T("INSERT_ACTIVITY_NAME_HERE");

      // Set spotlight activity structure.
      SpotlightActivity spotActivity = new SpotlightActivity();
      spotActivity.id = 0;
      spotActivity.activityGroupId = activityGroupId;
      spotActivity.activityTypeId = activityTypeId;
      spotActivity.tagMethodTypeId = tagMethodTypeId;
      spotActivity.name = activityName;
      spotActivity.expectedUrl = url;
      spotActivity.countryId = countryId;

      try {
        // Create the spotlight tag activity.
        SpotlightActivitySaveResult result =  service.saveSpotlightActivity(spotActivity);

        // Display new spotlight activity id.
        if (result != null) {
          Console.WriteLine("Spotlight activity with id \"{0}\" was created.", result.id);
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to create spotlight activity. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
