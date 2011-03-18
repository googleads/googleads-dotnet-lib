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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201008;

using System;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201008 {
  /// <summary>
  /// This code example retrieves all ads given an existing ad group. To add
  /// ads to an existing ad group, run AddAds.cs.
  ///
  /// Tags: AdGroupAdService.get
  /// </summary>
  class GetAllAds : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example retrieves all ads given an existing ad group. To add " +
            "ads to an existing ad group, run AddAds.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetAllAds();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the AdGroupAdService.
      AdGroupAdService service =
          (AdGroupAdService) user.GetService(AdWordsService.v201008.AdGroupAdService);

      long adGroupId = long.Parse(_T("INSERT_AD_GROUP_ID_HERE"));

      // Create a selector and set the filters.
      AdGroupAdSelector selector = new AdGroupAdSelector();
      selector.adGroupIds = new long[] {adGroupId};
      // By default disabled ads aren't returned by the selector. To return them
      // include the DISABLED status in the statuses field.
      selector.statuses = new AdGroupAdStatus[] {AdGroupAdStatus.ENABLED, AdGroupAdStatus.PAUSED,
          AdGroupAdStatus.DISABLED};

      try {
        AdGroupAdPage page = service.get(selector);

        if (page != null && page.entries != null) {
          foreach (AdGroupAd tempAdGroupAd in page.entries) {
            Console.WriteLine("Ad id is {0} and status is {1}", tempAdGroupAd.ad.id,
                tempAdGroupAd.status);
          }
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to get Ad(s). Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
