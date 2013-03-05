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
using Google.Api.Ads.AdWords.v201206;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201206 {
  /// <summary>
  /// This code example illustrates how to override a location extension.
  /// To create an ad, run AddTextAds.cs. To create a location extension,
  /// run AddLocationExtension.cs.
  ///
  /// Tags: GeoLocationService.get, AdExtensionOverrideService.mutate
  /// </summary>
  public class AddLocationExtensionOverride : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      AddLocationExtensionOverride codeExample = new AddLocationExtensionOverride();
      Console.WriteLine(codeExample.Description);
      try {
        long adId = long.Parse("INSERT_AD_ID_HERE");
        long locationExtensionId = long.Parse("INSERT_LOCATION_EXTENSION_ID_HERE");
        codeExample.Run(new AdWordsUser(), adId, locationExtensionId);
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
        return "This code example illustrates how to override a location extension. To create " +
            "an ad, run AddTextAds.cs. To create a location extension, run " +
            "AddLocationExtension.cs.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="adId">Id of the ad for which the location extension is
    /// overridden.</param>
    /// <param name="locationExtensionId">Id of the location extension to be
    /// overridden.</param>
    public void Run(AdWordsUser user, long adId, long locationExtensionId) {
      // Get the AdExtensionOverrideService.
      AdExtensionOverrideService adExtensionOverrideService =
          (AdExtensionOverrideService) user.GetService(AdWordsService.v201206.
              AdExtensionOverrideService);

      // Create the address.
      Address address = new Address();
      address.streetAddress = "1600 Amphitheatre Parkway";
      address.cityName = "Mountain View";
      address.provinceCode = "CA";
      address.postalCode = "94043";
      address.countryCode = "US";

      // Get the GeoLocationService.
      GeoLocationService geoService =
          (GeoLocationService) user.GetService(AdWordsService.v201206.GeoLocationService);

      GeoLocationSelector selector = new GeoLocationSelector();
      selector.addresses = new Address[] {address};

      // Get the geo location for the address.
      GeoLocation location = geoService.get(selector)[0];

      // Create the location extension.
      LocationExtension extension = new LocationExtension();
      extension.id = locationExtensionId;
      extension.address = location.address;
      extension.geoPoint = location.geoPoint;
      extension.encodedLocation = location.encodedLocation;
      extension.source = LocationExtensionSource.ADWORDS_FRONTEND;

      // Optional: Set the company name.
      extension.companyName = "ACME Inc.";

      // Optional: Set the phone number.
      extension.phoneNumber = "(650) 253-0000";

      // Optional: Set image and icon media id.
      // extension.imageMediaId = ...;
      // extension.iconMediaId = ...;

      AdExtensionOverride locationExtensionOverride = new AdExtensionOverride();
      locationExtensionOverride.adExtension = extension;
      locationExtensionOverride.adId = adId;

      // Optional: Set the override info.
      OverrideInfo overrideInfo = new OverrideInfo();
      overrideInfo.Item = new LocationOverrideInfo();
      overrideInfo.Item.radius = 5;
      overrideInfo.Item.radiusUnits = LocationOverrideInfoRadiusUnits.MILES;
      locationExtensionOverride.overrideInfo = overrideInfo;

      // Create the operation.
      AdExtensionOverrideOperation operation = new AdExtensionOverrideOperation();
      operation.@operator = Operator.ADD;
      operation.operand = locationExtensionOverride;

      try {
        // Create the location extension override.
        AdExtensionOverrideReturnValue retVal = adExtensionOverrideService.mutate(
            new AdExtensionOverrideOperation[] {operation});

        // Display the results.
        if (retVal != null && retVal.value != null && retVal.value.Length > 0) {
          AdExtensionOverride adExtensionOverride = retVal.value[0];
          Console.WriteLine("Overrode location extension with id = \"{0}\" in Ad id = \"{1}\"",
              adExtensionOverride.adExtension.id, adExtensionOverride.adId);
        } else {
          Console.WriteLine("No location extensions were overridden.");
        }
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to override location extension.", ex);
      }
    }
  }
}
