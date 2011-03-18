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
using Google.Api.Ads.AdWords.v200909;

using System;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v200909 {
  /// <summary>
  /// This code example illustrates how to override a campaign ad extension.
  /// To create an ad, run AddAds.cs. To create a campaign ad extension, run
  /// AddCampaignAdExtension.cs.
  ///
  /// Tags: GeoLocationService.get, AdExtensionOverrideService.mutate
  /// </summary>
  class AddAdExtensionOverride : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example illustrates how to override a campaign ad extension." +
            " To create an ad, run AddAds.cs. To create a campaign ad extension, run" +
            " AddCampaignAdExtension.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new AddAdExtensionOverride();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser());
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

      long adId = long.Parse(_T("INSERT_AD_ID_HERE"));
      long campaignAdExtensionId = long.Parse(_T("INSERT_CAMPAIGN_AD_EXTENSION_ID_HERE"));

      Address address = new Address();
      address.streetAddress = "1600 Amphitheatre Parkway";
      address.cityName = "Mountain View";
      address.provinceCode = "CA";
      address.postalCode = "94043";
      address.countryCode = "US";

      GeoLocationService geoService =
          (GeoLocationService) user.GetService(AdWordsService.v200909.GeoLocationService);

      GeoLocationSelector selector = new GeoLocationSelector();
      selector.addresses = new Address[] {address};
      GeoLocation location = geoService.get(selector)[0];

      LocationExtension extension = new LocationExtension();
      extension.id = campaignAdExtensionId;
      extension.address = location.address;
      extension.geoPoint = location.geoPoint;
      extension.encodedLocation = location.encodedLocation;
      extension.source = LocationExtensionSource.ADWORDS_FRONTEND;
      extension.phoneNumber = "1-800-555-5556";

      AdExtensionOverride adOverride = new AdExtensionOverride();
      adOverride.adExtension = extension;
      adOverride.adId = adId;


      AdExtensionOverrideOperation operation = new AdExtensionOverrideOperation();
      operation.@operator = Operator.ADD;
      operation.operand = adOverride;

      try {
        AdExtensionOverrideReturnValue retVal = adExtensionOverrideService.mutate(
            new AdExtensionOverrideOperation[] {operation});

        if (retVal != null && retVal.value != null && retVal.value.Length > 0) {
          AdExtensionOverride adExtensionOverride = retVal.value[0];
          Console.WriteLine("Overrode Ad Extension with id = \"{0}\" in Ad id = \"{1}\"",
              adExtensionOverride.adExtension.id, adExtensionOverride.adId);
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to override AdExtension. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
