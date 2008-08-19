//
// Copyright (C) 2008 Google Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

using com.google.api.adwords.lib;
using com.google.api.adwords.v12;

using System;
using System.Text;

namespace com.google.api.adwords.examples {
  // Gets all ads from specific ad group.
  class GetAdGroupIdsDemo {
    public static void run() {
      // Create a user (reads headers from App.config file).
      AdWordsUser user = new AdWordsUser();
      user.useSandbox();  // use sandbox

      // Get the service.
      AdGroupService adGroupService =
        (AdGroupService) user.getService("AdGroupService");

      // Get all adGroups.
      int campaignId = 12345;
      AdGroup[] adGroups = adGroupService.getAllAdGroups(campaignId);

      for (int i = 0; i < adGroups.Length; i++) {
        Console.WriteLine(
            "----- AdGroup Info -----"
            + "\nAd Group Id: {0}"
            + "\nName: {1}"
            + "\nStatus: {2}"
            + "\n------------------------",
            adGroups[i].id, adGroups[i].name, adGroups[i].status);
      }

      Console.ReadLine();
    }
  }
}
