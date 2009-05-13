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
  /// Gets all ad groups from a specific campaign.
  /// </summary>
  class GetAdGroupIdsDemo : SampleBase {
    /// <summary>
    /// Returns a description about the sample code.
    /// </summary>
    public override string Description {
      get {
        return "Gets all ad groups from a specific campaign.";
      }
    }

    /// <summary>
    /// Run the sample code.
    /// </summary>
    /// <param name="user">The AdWords user object running the sample.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the service.
      AdGroupService adGroupService =
          (AdGroupService) user.GetService(ApiServices.v13.AdGroupService);

      // Get all adGroups.
      int campaignId = (int)InputUtils.AcceptLong("Enter campaign id : ");
      AdGroup[] adGroups = adGroupService.getAllAdGroups(campaignId);

      if (adGroups != null) {
        for (int i = 0; i < adGroups.Length; i++) {
          Console.WriteLine("----- AdGroup Info -----\nAd Group Id: {0}\nName: {1}" +
              "\nStatus: {2}\n------------------------",
              adGroups[i].id, adGroups[i].name, adGroups[i].status);
        }
      } else {
        Console.WriteLine("There are no adgroups in this campaign.");
      }
    }
  }
}
