// Copyright 2010, Google Inc. All Rights Reserved.
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
using System.IO;
using System.Net;

namespace com.google.api.adwords.examples.v200909 {
  /// <summary>
  /// This code example illustrates how to retrieve all the ad groups for a
  /// campaign. To create an ad group, run AddAdGroup.cs.
  /// </summary>
  class GetAllAdGroups : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example illustrates how to retrieve all the ad groups for a" +
            " campaign. To create an ad group, run AddAdGroup.cs.";
      }
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the AdGroupService.
      AdGroupService adGroupService =
          (AdGroupService) user.GetService(AdWordsService.v200909.AdGroupService);

      long campaignId = long.Parse(_T("INSERT_CAMPAIGN_ID_HERE"));

      AdGroupSelector adGroupSelector = new AdGroupSelector();
      adGroupSelector.campaignIdSpecified = true;
      adGroupSelector.campaignId = campaignId;

      try {
        AdGroupPage page = adGroupService.get(adGroupSelector);
        if (page != null && page.entries != null) {
          Console.WriteLine("Campaign #{0} has {1} ad group(s).", campaignId, page.entries.Length);
          foreach (AdGroup adGroup in page.entries) {
            Console.WriteLine("  Ad group name is '{0} and id is {1}.", adGroup.name, adGroup.id);
          }
        } else {
          Console.WriteLine("No ad groups found for campaign #{0}.", campaignId);
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to retrieve ad group(s). Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
