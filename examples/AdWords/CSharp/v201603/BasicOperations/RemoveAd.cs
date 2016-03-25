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
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201603 {
  /// <summary>
  /// This code example removes an ad using the 'REMOVE' operator. To list ads,
  /// run GetTextAds.cs.
  /// </summary>
  public class RemoveAd : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      RemoveAd codeExample = new RemoveAd();
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
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example removes an ad using the 'REMOVE' operator. To list ads, " +
            "run GetTextAds.cs.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="adGroupId">Id of the ad group that contains the ad.</param>
    /// <param name="adId">Id of the ad being removed.</param>
    public void Run(AdWordsUser user, long adGroupId, long adId) {
      // Get the AdGroupAdService.
      AdGroupAdService adGroupAdService = (AdGroupAdService) user.GetService(
          AdWordsService.v201603.AdGroupAdService);

      // Since we do not need to update any ad-specific fields, it is enough to
      // create the base type.
      Ad ad = new Ad();
      ad.id = adId;

      // Create the ad group ad.
      AdGroupAd adGroupAd = new AdGroupAd();
      adGroupAd.adGroupId = adGroupId;

      adGroupAd.ad = ad;

      // Create the operation.
      AdGroupAdOperation operation = new AdGroupAdOperation();
      operation.operand = adGroupAd;
      operation.@operator = Operator.REMOVE;

      try {
        // Remove the ad.
        AdGroupAdReturnValue retVal = adGroupAdService.mutate(
            new AdGroupAdOperation[] {operation});

        if (retVal != null && retVal.value != null && retVal.value.Length > 0) {
          AdGroupAd removedAdGroupAd = retVal.value[0];
          Console.WriteLine("Ad with id = \"{0}\" and type = \"{1}\" was removed.",
              removedAdGroupAd.ad.id, removedAdGroupAd.ad.AdType);
        } else {
          Console.WriteLine("No ads were removed.");
        }
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to remove ad.", e);
      }
    }
  }
}
