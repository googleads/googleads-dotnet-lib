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
  /// This sample shows how to get the Geo location for a given address.
  /// </summary>
  class GetGeoLocation : SampleBase {
    /// <summary>
    /// Returns a description about the sample code.
    /// </summary>
    public override string Description {
      get {
        return "This sample shows how to get the Geo location for a given address.";
      }
    }

    /// <summary>
    /// Run the sample code.
    /// </summary>
    /// <param name="user">The AdWords user object running the sample.</param>
    public override void Run(AdWordsUser user) {
      GeoLocationService geoService =
          (GeoLocationService) user.GetService(AdWordsService.v200909.GeoLocationService);

      // China Art Museum (54 Street, No. 1, Beijing Dongcheng District, CN)
      Address address1 = new Address();
      address1.streetAddress = "五四大街1号, Beijing东城区";
      address1.countryCode = "CN";

      // Google Office, MTV (1600 Amphitheatre Pkwy, Mountain View, US)
      Address address2 = new Address();
      address2.streetAddress = "1600 Amphitheatre Pkwy, Mountain View";
      address2.countryCode = "US";

      try {
        GeoLocationSelector selector = new GeoLocationSelector();
        selector.addresses = new Address[] {address1, address2};
        GeoLocation[] locations = geoService.get(selector);
        if (locations != null) {
          foreach (GeoLocation location in locations) {
            Console.WriteLine("The address {0}, {1} is located at coordinates ({2}, {3})",
                location.address.streetAddress, location.address.countryCode,
                location.geoPoint.latitudeInMicroDegrees,
                location.geoPoint.longitudeInMicroDegrees);
          }
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to obtain location of the given address. " +
            "Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
