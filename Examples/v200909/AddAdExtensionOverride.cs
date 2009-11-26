// Copyright 2009, Google Inc. All Rights Reserved.
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

using com.google.api.adwords.lib;
using com.google.api.adwords.v200909;

using System;
using System.Collections.Generic;
using System.Text;

namespace com.google.api.adwords.samples.v200909 {
  /// <summary>
  /// This sample shows how to override an existing AdExtension.
  /// </summary>
  class AddAdExtensionOverride : SampleBase {
    /// <summary>
    /// Returns a description about the sample code.
    /// </summary>
    public override string Description {
      get {
        return "This sample shows how to override an existing AdExtension.";
      }
    }

    /// <summary>
    /// Run the sample code.
    /// </summary>
    /// <param name="user">The AdWords user object running the sample.</param>
    public override void Run(AdWordsUser user) {
      AdExtensionOverrideService adExtensionOverrideService =
          (AdExtensionOverrideService) user.GetService(AdWordsService.v200909.
              AdExtensionOverrideService);

      AdExtensionOverrideOperation operation = new AdExtensionOverrideOperation();
      operation.operatorSpecified = true;
      operation.@operator = Operator.ADD;

      operation.operand = new AdExtensionOverride();
      operation.operand.adIdSpecified = true;
      operation.operand.adId = long.Parse(_T("INSERT_AD_ID_HERE"));

      Address address = new Address();
      address.streetAddress = "1600 Amphitheatre Pkwy, Mountain View";
      address.countryCode = "US";

      GeoLocation location = GetLocationForAddress(user, address);

      LocationExtension locationExtension = new LocationExtension();

      locationExtension.idSpecified = true;
      locationExtension.id = long.Parse(_T("INSERT_LOCATION_EXTENSION_ID_HERE"));

      // Note: Do not populate an address directly. Instead, use
      // GeoLocationService to obtain the location of an address,
      // and use the address as per the location it returns.
      locationExtension.address = location.address;
      locationExtension.geoPoint = location.geoPoint;
      locationExtension.encodedLocation = location.encodedLocation;
      locationExtension.sourceSpecified = true;
      locationExtension.source = LocationExtensionSource.ADWORDS_FRONTEND;

      // Optional: Apply this override within 20 kms.
      operation.operand.overrideInfo = new OverrideInfo();
      operation.operand.overrideInfo.Item = new LocationOverrideInfo();
      operation.operand.overrideInfo.Item.radiusSpecified = true;
      operation.operand.overrideInfo.Item.radius = 20;
      operation.operand.overrideInfo.Item.radiusUnitsSpecified = true;
      operation.operand.overrideInfo.Item.radiusUnits = LocationOverrideInfoRadiusUnits.KILOMETERS;

      operation.operand.adExtension = locationExtension;

      try {
        AdExtensionOverrideReturnValue retval =
            adExtensionOverrideService.mutate(new AdExtensionOverrideOperation[] {operation});

        if (retval != null && retval.value != null && retval.value.Length > 0) {
          AdExtensionOverride adExtensionOverride = retval.value[0];
          Console.WriteLine("Overrode Ad Extension with id = \"{0}\" in Ad id = \"{1}\"",
              adExtensionOverride.adExtension.id, adExtensionOverride.adId);
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to override AdExtension. Exception says \"{0}\"", ex.Message);
      }
    }

    private GeoLocation GetLocationForAddress(AdWordsUser user, Address address) {
      GeoLocationService geoService =
          (GeoLocationService) user.GetService(AdWordsService.v200909.GeoLocationService);

      GeoLocationSelector selector = new GeoLocationSelector();
      selector.addresses = new Address[] {address};
      return geoService.get(selector)[0];
    }
  }
}
