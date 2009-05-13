// Copyright 2009, Google Inc. All Rights Reserved.
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

using System;
using System.Text;

using com.google.api.adwords.lib;
using com.google.api.adwords.v13;

namespace com.google.api.adwords.samples.v13 {
  /// <summary>
  /// Gets all ads from a specific ad group.
  /// </summary>
  class GetAdIdsDemo : SampleBase {
    /// <summary>
    /// Returns a description about the sample code.
    /// </summary>
    public override string Description {
      get {
        return "Gets all ads from a specific ad group.";
      }
    }

    /// <summary>
    /// Run the sample code.
    /// </summary>
    /// <param name="user">The AdWords user object running the sample.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the service.
      AdService service = (AdService) user.GetService(ApiServices.v13.AdService);

      // Get all ads.
      long[] adGroupIds = {InputUtils.AcceptLong("Enter AdGroup ID: ")};
      Ad[] ads = service.getAllAds(adGroupIds);

      if (ads != null) {
        for (int i = 0; i < ads.Length; i++) {
          Console.WriteLine("----- Ad Info -----\nAd Group Id: {0}\nId: {1}\nType: {2}" +
              "\nStatus: {3}\n------------------------",
              ads[i].adGroupId, ads[i].id, ads[i].adType, ads[i].status);
        }
      } else {
        Console.WriteLine("There are no Ads in the given AdGroup(s).");
      }
    }
  }
}
