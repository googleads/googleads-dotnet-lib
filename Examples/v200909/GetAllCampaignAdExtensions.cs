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
using System.Collections.Generic;
using System.Text;

namespace com.google.api.adwords.samples.v200909 {
  /// <summary>
  /// This sample shows how to retrieve all Ad Extensions in a Campaign. To
  /// create a Campaign Ad Extension, you can use the
  /// AddCampaignAdExtensionOverride sample.
  /// </summary>
  class GetAllCampaignAdExtensions : SampleBase {
    /// <summary>
    /// Returns a description about the sample code.
    /// </summary>
    public override string Description {
      get {
        return "This sample shows how to retrieve all Ad Extensions in a Campaign.";
      }
    }

    /// <summary>
    /// Run the sample code.
    /// </summary>
    /// <param name="user">The AdWords user object running the sample.</param>
    public override void Run(AdWordsUser user) {
      // Get the CampaignAdExtensionService.
      CampaignAdExtensionService campaignExtensionService =
          (CampaignAdExtensionService) user.GetService(AdWordsService.v200909.
          CampaignAdExtensionService);

      long campaignId = long.Parse(_T("INSERT_CAMPAIGN_ID_HERE"));

      CampaignAdExtensionSelector selector = new CampaignAdExtensionSelector();
      selector.campaignIds = new long[] {campaignId};
      selector.paging = new Paging();
      selector.paging.numberResultsSpecified = true;
      selector.paging.numberResults = 10;

      try {
        CampaignAdExtensionPage page = campaignExtensionService.get(selector);
        if (page != null && page.entries != null) {
          Console.WriteLine("Retrieved {0} out of {1} entries.", page.entries.Length,
              page.totalNumEntries);
          foreach (CampaignAdExtension campaignExtension in page.entries) {
            Console.WriteLine("Campaign ad extension id is \"{0}\" and status is  \"{1}\"",
                campaignExtension.adExtension.id, campaignExtension.status);
          }
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to retrieve campaign ad extensions. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
