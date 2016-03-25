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
using System.Linq;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201603 {
  /// <summary>
  /// This code example adds a text ad that uses advanced features of upgraded
  /// URLs.
  /// </summary>
  public class AddTextAdWithUpgradedUrls : ExampleBase {

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      AddTextAdWithUpgradedUrls codeExample = new AddTextAdWithUpgradedUrls();
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
        return "This code example adds a text ad that uses advanced features of upgraded URLs.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="adGroupId">ID of the ad group to which ad is added.
    /// </param>
    public void Run(AdWordsUser user, long adGroupId) {
      // Get the AdGroupAdService.
      AdGroupAdService service =
          (AdGroupAdService) user.GetService(AdWordsService.v201603.AdGroupAdService);

      // Create the text ad.
      TextAd textAd = new TextAd();
      textAd.headline = "Luxury Cruise to Mars";
      textAd.description1 = "Visit the Red Planet in style.";
      textAd.description2 = "Low-gravity fun for everyone!";
      textAd.displayUrl = "www.example.com";

      // Specify a tracking URL for 3rd party tracking provider. You may
      // specify one at customer, campaign, ad group, ad, criterion or
      // feed item levels.
      textAd.trackingUrlTemplate =
          "http://tracker.example.com/?season={_season}&promocode={_promocode}&u={lpurl}";

      // Since your tracking URL has two custom parameters, provide their
      // values too. This can be provided at campaign, ad group, ad, criterion
      // or feed item levels.
      CustomParameter seasonParameter = new CustomParameter();
      seasonParameter.key = "season";
      seasonParameter.value = "christmas";

      CustomParameter promoCodeParameter = new CustomParameter();
      promoCodeParameter.key = "promocode";
      promoCodeParameter.value = "NYC123";

      textAd.urlCustomParameters = new CustomParameters();
      textAd.urlCustomParameters.parameters =
          new CustomParameter[] { seasonParameter, promoCodeParameter };

      // Specify a list of final URLs. This field cannot be set if URL field is
      // set. This may be specified at ad, criterion and feed item levels.
      textAd.finalUrls = new string[] {
        "http://www.example.com/cruise/space/",
        "http://www.example.com/locations/mars/"
      };

      // Specify a list of final mobile URLs. This field cannot be set if URL
      // field is set, or finalUrls is unset. This may be specified at ad,
      // criterion and feed item levels.
      textAd.finalMobileUrls = new string[] {
        "http://mobile.example.com/cruise/space/",
        "http://mobile.example.com/locations/mars/"
      };

      AdGroupAd textAdGroupAd = new AdGroupAd();
      textAdGroupAd.adGroupId = adGroupId;
      textAdGroupAd.ad = textAd;

      // Optional: Set the status.
      textAdGroupAd.status = AdGroupAdStatus.PAUSED;

      // Create the operation.
      AdGroupAdOperation operation = new AdGroupAdOperation();
      operation.@operator = Operator.ADD;
      operation.operand = textAdGroupAd;

      AdGroupAdReturnValue retVal = null;

      try {
        // Create the ads.
        retVal = service.mutate(new AdGroupAdOperation[] { operation });

        // Display the results.
        if (retVal != null && retVal.value != null) {
          AdGroupAd newAdGroupAd = retVal.value[0];
          Console.WriteLine("New text ad with ID = {0} and display URL = \"{1}\" was " +
              "created.", newAdGroupAd.ad.id, newAdGroupAd.ad.displayUrl);
          Console.WriteLine("Upgraded URL properties:");
          TextAd newTextAd = (TextAd) newAdGroupAd.ad;

          Console.WriteLine("  Final URLs: {0}", string.Join(", ", newTextAd.finalUrls));
          Console.WriteLine("  Final Mobile URLs: {0}",
              string.Join(", ", newTextAd.finalMobileUrls));
          Console.WriteLine("  Tracking URL template: {0}", newTextAd.trackingUrlTemplate);
          Console.WriteLine("  Final App URLs: {0}",
              string.Join(", ", newTextAd.finalAppUrls.Select(finalAppUrl =>
                  finalAppUrl.url).ToArray()));

          List<string> parameters = new List<string>();
          foreach (CustomParameter customParam in newTextAd.urlCustomParameters.parameters) {
            parameters.Add(string.Format("{0}={1}", customParam.key, customParam.value));
          }
          Console.WriteLine("  Custom parameters: {0}", string.Join(", ", parameters.ToArray()));
        } else {
          Console.WriteLine("No text ads were created.");
        }
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to create text ad.", e);
      }
    }
  }
}
