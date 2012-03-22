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
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      ExampleBase codeExample = new AddThirdPartyRedirectAds();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser(), codeExample.GetParameters(), Console.Out);
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
    /// Gets the list of parameter names required to run this code example.
    /// </summary>
    /// <returns>
    /// A list of parameter names for this code example.
    /// </returns>
    public override string[] GetParameterNames() {
      return new string[] {"ADGROUP_ID"};
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="parameters">The parameters for running the code
    /// example.</param>
    /// <param name="writer">The stream writer to which script output should be
    /// written.</param>
    public override void Run(AdWordsUser user, Dictionary<string, string> parameters,
        TextWriter writer) {
      // Get the AdGroupAdService.
      AdGroupAdService service =
          (AdGroupAdService) user.GetService(AdWordsService.v201109.AdGroupAdService);

      long adGroupId = long.Parse(parameters["ADGROUP_ID"]);

      // Create the third party redirect ad.
      ThirdPartyRedirectAd redirectAd1 = new ThirdPartyRedirectAd();
      redirectAd1.name = string.Format("Example third party ad #{0}",
          ExampleUtilities.GetTimeStamp());
      redirectAd1.url = "http://www.example.com";

      redirectAd1.dimensions = new Dimensions();
      redirectAd1.dimensions.height = 250;
      redirectAd1.dimensions.width = 300;

      // This field normally contains the javascript ad tag.
      redirectAd1.snippet = "<img src=\"https://sandbox.google.com/sandboximages/image.jpg\"/>";
      redirectAd1.impressionBeaconUrl = "http://www.examples.com/beacon1";
      redirectAd1.certifiedVendorFormatId = 119;
      redirectAd1.isCookieTargeted = false;
      redirectAd1.isUserInterestTargeted = false;
      redirectAd1.isTagged = false;

      // Set the ad group id.
      AdGroupAd adGroupAd1 = new AdGroupAd();
      adGroupAd1.adGroupId = adGroupId;
      adGroupAd1.ad = redirectAd1;

      // Create the operation.
      AdGroupAdOperation adGroupAdOperation1 = new AdGroupAdOperation();
      adGroupAdOperation1.@operator = Operator.ADD;
      adGroupAdOperation1.operand = adGroupAd1;

      // Create the third party redirect ad.
      ThirdPartyRedirectAd redirectAd2 = new ThirdPartyRedirectAd();
      redirectAd2.name = string.Format("Example third party ad #{0}",
          ExampleUtilities.GetTimeStamp());
      redirectAd2.url = "http://www.example.com";

      redirectAd2.dimensions = new Dimensions();
      redirectAd2.dimensions.height = 250;
      redirectAd2.dimensions.width = 300;

      // This field normally contains the javascript ad tag.
      redirectAd2.snippet = "<img src=\"https://sandbox.google.com/sandboximages/image.jpg\"/>";
      redirectAd2.impressionBeaconUrl = "http://www.examples.com/beacon2";
      redirectAd2.certifiedVendorFormatId = 119;
      redirectAd2.isCookieTargeted = false;
      redirectAd2.isUserInterestTargeted = false;
      redirectAd2.isTagged = false;

      // Set the ad group id.
      AdGroupAd adGroupAd2 = new AdGroupAd();
      adGroupAd2.adGroupId = adGroupId;
      adGroupAd2.ad = redirectAd2;

      // Create the operation.
      AdGroupAdOperation adGroupAdOperation2 = new AdGroupAdOperation();
      adGroupAdOperation2.@operator = Operator.ADD;
      adGroupAdOperation2.operand = adGroupAd2;

      try {
        // Create the ads
        AdGroupAdReturnValue result =
            service.mutate(new AdGroupAdOperation[] {adGroupAdOperation1, adGroupAdOperation2});
        if (result != null && result.value != null && result.value.Length > 0) {
          foreach (AdGroupAd newAdGroupAd in result.value) {
            writer.WriteLine("New third party redirect ad with url = \"{0}\" and id = {1}" +
                " was created.", ((ThirdPartyRedirectAd) newAdGroupAd.ad).url,
                newAdGroupAd.ad.id);
          }
        } else {
          writer.WriteLine("No third party redirect ads were created.");
        }
      } catch (Exception ex) {
        writer.WriteLine("Failed to create third party redirect ads. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
