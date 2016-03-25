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
using Google.Api.Ads.Common.Util;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201603 {
  /// <summary>
  /// This code example adds text ads to a given ad group. To list ad groups,
  /// run GetAdGroups.cs. To learn how to handle policy violations and add
  /// exemption requests, see HandlePolicyViolationError.cs. This code example
  /// uses only the final URL field when creating the Ad. To see more options,
  /// see AddTextAdWithUpgradedUrls.cs.
  /// </summary>
  public class AddTextAds : ExampleBase {
    /// <summary>
    /// Number of items being added / updated in this code example.
    /// </summary>
    const int NUM_ITEMS = 5;

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      AddTextAds codeExample = new AddTextAds();
      Console.WriteLine(codeExample.Description);
      try {
        long adGroupId = long.Parse("INSERT_ADGROUP_ID_HERE");
        codeExample.Run(new AdWordsUser(), adGroupId);
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
        return "This code example adds text ads to a given ad group. To list ad groups, run " +
            "GetAdGroups.cs. To learn how to handle policy violations and add exemption " +
            "requests, see HandlePolicyViolationError.cs. This code example uses only the " +
            "final URL field when creating the Ad. To see more options, see " +
            "AddTextAdWithUpgradedUrls.cs.";
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
          (AdGroupAdService) user.GetService(AdWordsService.v201603.AdGroupAdService);

      List<AdGroupAdOperation> operations = new List<AdGroupAdOperation>();

      for (int i = 0; i < NUM_ITEMS; i++) {
        // Create the text ad.
        TextAd textAd = new TextAd();
        textAd.headline = "Luxury Cruise to Mars";
        textAd.description1 = "Visit the Red Planet in style.";
        textAd.description2 = "Low-gravity fun for everyone!";
        textAd.displayUrl = "www.example.com";
        textAd.finalUrls = new string[] { "http://www.example.com/" + i };

        AdGroupAd textAdGroupAd = new AdGroupAd();
        textAdGroupAd.adGroupId = adGroupId;
        textAdGroupAd.ad = textAd;

        // Optional: Set the status.
        textAdGroupAd.status = AdGroupAdStatus.PAUSED;

        // Create the operation.
        AdGroupAdOperation operation = new AdGroupAdOperation();
        operation.@operator = Operator.ADD;
        operation.operand = textAdGroupAd;

        operations.Add(operation);
      }

      AdGroupAdReturnValue retVal = null;

      try {
        // Create the ads.
        retVal = service.mutate(operations.ToArray());

        // Display the results.
        if (retVal != null && retVal.value != null) {
          // If you are adding multiple type of Ads, then you may need to check
          // for
          //
          // if (adGroupAd.ad is TextAd) { ... }
          //
          // to identify the ad type.
          foreach (AdGroupAd adGroupAd in retVal.value) {
            Console.WriteLine("New text ad with id = \"{0}\" and displayUrl = \"{1}\" was created.",
                adGroupAd.ad.id, adGroupAd.ad.displayUrl);
          }
        } else {
          Console.WriteLine("No text ads were created.");
        }
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to create text ad.", e);
      }
    }
  }
}
