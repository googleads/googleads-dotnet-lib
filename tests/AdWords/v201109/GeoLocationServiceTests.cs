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
using Google.Api.Ads.AdWords.v201109;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.AdWords.Tests.v201109 {
  /// <summary>
  /// UnitTests for <see cref="GeoLocationService"/> class.
  /// </summary>
  [TestFixture]
  class GeoLocationServiceTests : BaseTests {
    /// <summary>
    /// GeoLocationService object to be used in this test.
    /// </summary>
    private GeoLocationService geoLocationService;

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public GeoLocationServiceTests() : base() {
    }

    /// <summary>
    /// Initialize the test case.
    /// </summary>
    [SetUp]
    public void Init() {
      geoLocationService = (GeoLocationService)user.GetService(
          AdWordsService.v201109.GeoLocationService);
    }

    /// <summary>
    /// Test whether we can fetch geo location information for the given
    /// address.
    /// </summary>
    [Test]
    public void TestGetGeoLocationInfo() {
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

      // Get geo locations.
      GeoLocation[] geoLocations = geoLocationService.get(selector);

      Assert.NotNull(geoLocations);
      Assert.AreEqual(geoLocations.Length, 3);
      Assert.NotNull(geoLocations[0]);
      Assert.False(geoLocations[0] is InvalidGeoLocation);
      Assert.NotNull(geoLocations[1]);
      Assert.False(geoLocations[1] is InvalidGeoLocation);
      Assert.NotNull(geoLocations[2]);
      Assert.False(geoLocations[2] is InvalidGeoLocation);
    }
  }
}
