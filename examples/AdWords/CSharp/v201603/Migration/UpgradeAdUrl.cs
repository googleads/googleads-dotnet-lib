// Copyright 2016, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201603;

using System;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201603 {

  /// <summary>
  /// This code example upgrades an ad to use upgraded URLs.
  /// </summary>
  public class UpgradeAdUrl : ExampleBase {

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example upgrades an ad to use upgraded URLs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      UpgradeAdUrl codeExample = new UpgradeAdUrl();
      Console.WriteLine(codeExample.Description);
      try {
        long adGroupId = long.Parse("INSERT_ADGROUP_ID_HERE");
        long adId = long.Parse("INSERT_AD_ID_HERE");
        codeExample.Run(new AdWordsUser(), adGroupId, adId);
      } catch (Exception e) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(e));
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="adGroupId">ID of the ad group that contains the ad.</param>
    /// <param name="adId">ID of the ad to be upgraded.</param>
    public void Run(AdWordsUser user, long adGroupId, long adId) {
      // Get the AdGroupAdService.
      AdGroupAdService adGroupAdService = (AdGroupAdService)
          user.GetService(AdWordsService.v201603.AdGroupAdService);

      try {
        // Retrieve the Ad.
        AdGroupAd adGroupAd = GetAdGroupAd(adGroupAdService, adGroupId, adId);

        if (adGroupAd == null) {
          Console.WriteLine("Ad not found.");
          return;
        }

        // Copy the destination url to the final url.
        AdUrlUpgrade upgradeUrl = new AdUrlUpgrade();
        upgradeUrl.adId = adGroupAd.ad.id;
        upgradeUrl.finalUrl = adGroupAd.ad.url;

        // Upgrade the ad.
        Ad[] upgradedAds = adGroupAdService.upgradeUrl(new AdUrlUpgrade[] { upgradeUrl });

        // Display the results.
        if (upgradedAds != null && upgradedAds.Length > 0) {
          foreach (Ad upgradedAd in upgradedAds) {
            Console.WriteLine("Ad with id = {0} and destination url = {1} was upgraded.",
                upgradedAd.id, upgradedAd.finalUrls[0]);
          }
        } else {
          Console.WriteLine("No ads were upgraded.");
        }
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to upgrade ads.", e);
      }
    }

    /// <summary>
    /// Gets the ad group ad by ID.
    /// </summary>
    /// <param name="adGroupAdService">The AdGroupAdService instance.</param>
    /// <param name="adGroupId">ID of the ad group.</param>
    /// <param name="adId">ID of the ad to be retrieved.</param>
    /// <returns>The AdGroupAd if the item could be retrieved, null otherwise.
    /// </returns>
    private AdGroupAd GetAdGroupAd(AdGroupAdService adGroupAdService, long adGroupId, long adId) {
      // Create a selector.
      Selector selector = new Selector() {
        fields = new string[] { Ad.Fields.Id, Ad.Fields.Url },
        predicates = new Predicate[] {
          // Restrict the fetch to only the selected ad group ID and ad ID.
          Predicate.Equals(AdGroupAd.Fields.AdGroupId, adGroupId),
          Predicate.Equals(Ad.Fields.Id, adId)
        }
      };

      // Get the ad.
      AdGroupAdPage page = adGroupAdService.get(selector);

      if (page != null && page.entries != null && page.entries.Length > 0) {
        return page.entries[0];
      } else {
        return null;
      }
    }
  }
}
