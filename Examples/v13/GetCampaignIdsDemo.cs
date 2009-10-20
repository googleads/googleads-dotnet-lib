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

// Author: api.anash@gmail.com (Anash P. Oommen)

using com.google.api.adwords.lib;
using com.google.api.adwords.v13;

using System;
using System.Text;

namespace com.google.api.adwords.samples.v13 {
  /// <summary>
  /// Gets all campaigns.
  /// </summary>
  class GetCampaignIdsDemo : SampleBase {
    /// <summary>
    /// Returns a description about the sample code.
    /// </summary>
    public override string Description {
      get {
        return "Gets all campaigns from a client account.";
      }
    }

    /// <summary>
    /// Run the sample code.
    /// </summary>
    /// <param name="user">The AdWords user object running the sample.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the service.
      CampaignService campaignService =
          (CampaignService) user.GetService(AdWordsService.v13.CampaignService);

      // Get all campaigns.
      Campaign[] myCampaigns = campaignService.getAllAdWordsCampaigns(1);

      if (myCampaigns != null) {
        for (int i = 0; i < myCampaigns.Length; i++) {
          Console.WriteLine("Name: {0,-30} id: {1,-10} status: {2}",
              myCampaigns[i].name, myCampaigns[i].id, myCampaigns[i].status);
        }
      } else {
        Console.WriteLine("There are no campaigns in this account.");
      }
    }
  }
}
