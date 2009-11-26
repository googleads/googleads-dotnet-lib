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
using com.google.api.adwords.v200909;

using System;

namespace com.google.api.adwords.samples.v200909 {
  /// <summary>
  /// This code sample shows how to retrieve the list of all active campaigns
  /// in an account.
  /// </summary>
  class GetAllActiveCampaigns : SampleBase {
    /// <summary>
    /// Returns a description about the sample code.
    /// </summary>
    public override string Description {
      get {
        return "This code sample shows how to retrieve the list of all active campaigns " +
            "in an account.";
      }
    }

    /// <summary>
    /// Run the sample code.
    /// </summary>
    /// <param name="user">The AdWords user object running the sample.
    /// </param>
    public override void Run(AdWordsUser user) {
      CampaignService campaignService =
          (CampaignService) user.GetService(AdWordsService.v200909.CampaignService);
      CampaignSelector selector = new CampaignSelector();
      selector.campaignStatuses = new CampaignStatus[] {CampaignStatus.ACTIVE};

      try {
        CampaignPage campaignPage = campaignService.get(selector);

        if (campaignPage != null && campaignPage.entries != null) {
          foreach (Campaign campaign in campaignPage.entries) {
            Console.WriteLine("Campaign id is #{0} and name is #{1}", campaign.id, campaign.name);
          }
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to retrieve all active campaigns. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}

