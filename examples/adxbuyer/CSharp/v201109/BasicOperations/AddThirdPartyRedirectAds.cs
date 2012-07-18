// Copyright 2012, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdWords.v201109;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201109 {
  /// <summary>
  /// This code example adds third party redirect ads to an ad group. To get
  /// ad groups, run GetAdGroups.cs.
  ///
  /// Tags: AdGroupAdService.mutate
  /// </summary>
  public class AddThirdPartyRedirectAds : ExampleBase {
    /// <summary>
    /// Number of items being added / updated in this code example.
    /// </summary>
    const int NUM_ITEMS = 5;

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      AddThirdPartyRedirectAds codeExample = new AddThirdPartyRedirectAds();
      Console.WriteLine(codeExample.Description);
      try {
        long adGroupId = long.Parse("INSERT_ADGROUP_ID_HERE");
        codeExample.Run(new AdWordsUser(), adGroupId);
      } catch (Exception ex) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(ex));
      }
    }

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example adds third party redirect ads to an ad group. To get ad " +
            "groups, run GetAdGroups.cs.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="adGroupId">Id of the ad group to which ads are added.
    /// </param>
    public void Run(AdWordsUser user, long adGroupId) {
      // Get the AdGroupAdService.
      AdGroupAdService service =
          (AdGroupAdService) user.GetService(AdWordsService.v201109.AdGroupAdService);

      List<AdGroupAdOperation> operations = new List<AdGroupAdOperation>();

      for (int i = 0; i < NUM_ITEMS; i++) {
        // Create the text ad.
        // Create the third party redirect ad.
        ThirdPartyRedirectAd redirectAd = new ThirdPartyRedirectAd();
        redirectAd.name = string.Format("Example third party ad #{0}",
            ExampleUtilities.GetRandomString());
        redirectAd.url = "http://www.example.com";

        redirectAd.dimensions = new Dimensions();
        redirectAd.dimensions.height = 250;
        redirectAd.dimensions.width = 300;

        // This field normally contains the javascript ad tag.
        redirectAd.snippet = "<img src=\"https://sandbox.google.com/sandboximages/image.jpg\"/>";
        redirectAd.impressionBeaconUrl = "http://www.examples.com/beacon1";
        redirectAd.certifiedVendorFormatId = 119;
        redirectAd.isCookieTargeted = false;
        redirectAd.isUserInterestTargeted = false;
        redirectAd.isTagged = false;

        // Set the ad group id.
        AdGroupAd adGroupAd = new AdGroupAd();
        adGroupAd.adGroupId = adGroupId;
        adGroupAd.ad = redirectAd;

        // Create the operation.
        AdGroupAdOperation operation = new AdGroupAdOperation();
        operation.@operator = Operator.ADD;
        operation.operand = adGroupAd;

        operations.Add(operation);
      }

      AdGroupAdReturnValue retVal = null;

      try {
        // Create the ads
        retVal = service.mutate(operations.ToArray());
        if (retVal != null && retVal.value != null) {
          // If you are adding multiple type of Ads, then you may need to check
          // for
          //
          // if (adGroupAd.ad is ThirdPartyRedirectAd) { ... }
          //
          // to identify the ad type.
          foreach (AdGroupAd newAdGroupAd in retVal.value) {
            Console.WriteLine("New third party redirect ad with url = \"{0}\" and id = {1}" +
                " was created.", ((ThirdPartyRedirectAd) newAdGroupAd.ad).url,
                newAdGroupAd.ad.id);
          }
        } else {
          Console.WriteLine("No third party redirect ads were created.");
        }
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to create third party redirect ads.", ex);
      }
    }
  }
}
