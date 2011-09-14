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
using Google.Api.Ads.Dfa.v1_15;

using System;
using System.Collections.Generic;
using System.Text;
using Google.Api.Ads.Common.Util;

namespace Google.Api.Ads.Dfa.Examples.CSharp.v1_15 {
  /// <summary>
  /// This code example creates a campaign in a given advertiser. To create an
  /// advertiser, run CreateAdvertiser.cs.
  ///
  /// Tags: campaign.saveCampaign
  /// </summary>
  class CreateCampaign : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example creates a campaign in a given advertiser. To create an " +
            "advertiser, run CreateAdvertiser.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new CreateCampaign();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfaUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The Dfa user object running the code example.
    /// </param>
    public override void Run(DfaUser user) {
      // Create CampaignRemoteService instance.
      CampaignRemoteService service = (CampaignRemoteService) user.GetService(
          DfaService.v1_15.CampaignRemoteService);

      long advertiserId = long.Parse(_T("INSERT_ADVERTISER_ID_HERE"));
      string campaignName = _T("INSERT_CAMPAIGN_NAME_HERE");
      string url = _T("INSERT_LANDING_PAGE_URL_HERE");
      string landingPageName = _T("INSERT_LANDING_PAGE_NAME_HERE");

      // Create campaign structure.
      Campaign campaign = new Campaign();
      campaign.advertiserId = advertiserId;
      campaign.id = 0;
      campaign.name = campaignName;

      campaign.startDate = DateTime.Now;
      campaign.endDate = DateTime.Now.AddMonths(1);

      // Create & set default landing page.
      LandingPage defaultLandingPage = new LandingPage();
      defaultLandingPage.id = 0;
      defaultLandingPage.name = landingPageName;
      defaultLandingPage.url = url;

      try {
        campaign.defaultLandingPageId = service.saveLandingPage(defaultLandingPage).id;

        // Create campaign.
        CampaignSaveResult result = service.saveCampaign(campaign);

        // Display new campaign id.
        Console.WriteLine("Campaign with id \"{0}\" was created.", result.id);
      } catch (Exception ex) {
        Console.WriteLine("Failed to create campaign. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
