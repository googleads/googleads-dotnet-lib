// Copyright 2015, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdWords.v201502;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201502 {
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
          (AdGroupAdService) user.GetService(AdWordsService.v201502.AdGroupAdService);

      // Create standard third party redirect ad.
      ThirdPartyRedirectAd standardAd = new ThirdPartyRedirectAd();
      standardAd.name = String.Format("Example third party ad #{0}",
          ExampleUtilities.GetRandomString());
      standardAd.finalUrls = new string[] {"http://www.example.com"};

      standardAd.dimensions = new Dimensions();
      standardAd.dimensions.height = 250;
      standardAd.dimensions.width = 300;

      standardAd.snippet = "<img src=\"http://goo.gl/HJM3L\"/>";

      // DoubleClick Rich Media Expandable format ID.
      standardAd.certifiedVendorFormatId = 232;
      standardAd.isCookieTargeted = false;
      standardAd.isUserInterestTargeted = false;
      standardAd.isTagged = false;
      standardAd.richMediaAdType = RichMediaAdRichMediaAdType.STANDARD;

      // Expandable Ad properties.
      standardAd.expandingDirections = new ThirdPartyRedirectAdExpandingDirection[] {
          ThirdPartyRedirectAdExpandingDirection.EXPANDING_UP,
          ThirdPartyRedirectAdExpandingDirection.EXPANDING_DOWN
      };

      standardAd.adAttributes = new RichMediaAdAdAttribute[] {
          RichMediaAdAdAttribute.ROLL_OVER_TO_EXPAND};

      // Create in-stream third party redirect ad.
      ThirdPartyRedirectAd inStreamAd = new ThirdPartyRedirectAd();
      inStreamAd.name = String.Format("Example third party ad #{0}",
          ExampleUtilities.GetRandomString());
      inStreamAd.finalUrls = new string[] {"http://www.example.com"};
      // Set the duration to 15 secs.
      inStreamAd.adDuration = 15000;
      inStreamAd.sourceUrl = "http://ad.doubleclick.net/pfadx/N270.126913.6102203221521/B3876671.21;dcadv=2215309;sz=0x0;ord=%5btimestamp%5d;dcmt=text/xml";
      inStreamAd.certifiedVendorFormatId = 303;
      inStreamAd.richMediaAdType = RichMediaAdRichMediaAdType.IN_STREAM_VIDEO;

      List<AdGroupAdOperation> operations = new List<AdGroupAdOperation>();

      foreach (ThirdPartyRedirectAd redirectAd in new
          ThirdPartyRedirectAd[] {standardAd, inStreamAd}) {
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
                " was created.", ((ThirdPartyRedirectAd) newAdGroupAd.ad).finalUrls[0],
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
