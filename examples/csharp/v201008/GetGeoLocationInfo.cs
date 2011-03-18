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
using Google.Api.Ads.AdWords.v201008;

using System;
using System.IO;
using System.Net;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201008 {
  /// <summary>
  /// This code example gets geo location information for addresses.
  ///
  /// Tags: GeoLocationService.get
  /// </summary>
  class GetGeoLocationInfo : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets geo location information for addresses.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetGeoLocationInfo();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the GeoLocationService.
      GeoLocationService geoLocationService =
          (GeoLocationService) user.GetService(AdWordsService.v201008.GeoLocationService);

      Address address1 = new Address();
      address1.streetAddress = "1600 Amphitheatre Parkway";
      address1.cityName = "Mountain View";
      address1.provinceCode = "CA";
      address1.postalCode = "94043";
      address1.countryCode = "US";

      Address address2 = new Address();
      address2.streetAddress = "76 Ninth Avenue";
      address2.cityName = "New York";
      address2.provinceCode = "NY";
      address2.postalCode = "10011";
      address2.countryCode = "US";

      Address address3 = new Address();
      address3.streetAddress = "五四大街1号, Beijing东城区";
      address3.countryCode = "CN";

      // Create selector.
      GeoLocationSelector selector = new GeoLocationSelector();
      selector.addresses = new Address[] {address1, address2, address3};

      try {
        // Get geo locations.
        GeoLocation[] geoLocations = geoLocationService.get(selector);

        if (geoLocations != null) {
          // Display geo locations.
          foreach (GeoLocation geoLocation in geoLocations) {
            if (!(geoLocation is InvalidGeoLocation)) {
              Console.WriteLine("Address {0} has latitude {1} and longitude {2}.",
                  geoLocation.address.streetAddress, geoLocation.geoPoint.latitudeInMicroDegrees,
                  geoLocation.geoPoint.longitudeInMicroDegrees);
            } else {
              Console.WriteLine("Invalid geo location returned.\n");
            }
          }
        } else {
          Console.WriteLine("No geo locations were returned.\n");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to retrieve geo location(s). Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
