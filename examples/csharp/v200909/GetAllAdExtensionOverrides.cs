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
  /// This code example illustrates how to retrieve all the ad extension
  /// overrides for an existing campaign. To create an ad extension override
  /// run AddAdExtensionOverride.cs.
  ///
  /// Tags: AdExtensionOverrideService.get
  /// </summary>
  class GetAllAdExtensionOverrides : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example illustrates how to retrieve all the ad extension overrides" +
            " for an existing campaign. To create an ad extension override run" +
            " AddAdExtensionOverride.cs.";
      }
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the AdExtensionOverrideService.
      AdExtensionOverrideService adExtensionOverrideService =
          (AdExtensionOverrideService) user.GetService(AdWordsService.v200909.
              AdExtensionOverrideService);

      long campaignId = long.Parse(_T("INSERT_CAMPAIGN_ID_HERE"));

      AdExtensionOverrideSelector selector = new AdExtensionOverrideSelector();
      selector.campaignIds = new long[] {campaignId};

      try {
        AdExtensionOverridePage result = adExtensionOverrideService.get(selector);

        if (result != null && result.entries != null) {
          Console.WriteLine("Campaign id '{0}' has {1} ad extension override(s).",
              campaignId, result.entries.Length);
          foreach(AdExtensionOverride adExtension in result.entries) {
            Console.WriteLine("  Ad extension override has id = '{0}' and is for ad id = '{1}'.",
                adExtension.adId, adExtension.adExtension.id);
          }
        } else {
          Console.WriteLine("No ad extension overrides found for campaign id = '{0}'.", campaignId);
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to retrieve ad extension override(s). Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
