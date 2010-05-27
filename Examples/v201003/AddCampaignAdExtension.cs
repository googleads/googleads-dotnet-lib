// Copyright 2010, Google Inc. All Rights Reserved.
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
using com.google.api.adwords.v201003;

using System;
using System.Collections.Generic;
using System.Text;

namespace com.google.api.adwords.examples.v201003 {
  /// <summary>
  /// This code example shows how to add an Ad Extension to an existing campaign. To
  /// create a campaign, run AddCampaign.cs.
  /// </summary>
  class AddCampaignAdExtension : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example shows how to add an Ad Extension to an existing campaign. To" +
            " create a campaign, run AddCampaign.cs.";
      }
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the CampaignAdExtensionService.
      CampaignAdExtensionService campaignExtensionService =
          (CampaignAdExtensionService) user.GetService(AdWordsService.v201003.
          CampaignAdExtensionService);

      long campaignId = long.Parse(_T("INSERT_CAMPAIGN_ID_HERE"));

      // Add location 1: 1600 Amphitheatre Pkwy, Mountain View, US.
      Address address1 = new Address();
      address1.streetAddress = "1600 Amphitheatre Parkway";
      address1.cityName = "Mountain View";
      address1.provinceCode = "CA";
      address1.postalCode = "94043";
      address1.countryCode = "US";

      // Add location 2: 38 avenue de l'Opéra, 75002 Paris, FR
      Address address2 = new Address();
      address2.streetAddress = "38 avenue de l'Opéra";
      address2.cityName = "Paris";
      address2.postalCode = "75002";
      address2.countryCode = "FR";

      GeoLocationService geoService =
          (GeoLocationService) user.GetService(AdWordsService.v201003.GeoLocationService);

      GeoLocationSelector selector = new GeoLocationSelector();
      selector.addresses = new Address[] {address1, address2};
      GeoLocation[] locations = geoService.get(selector);

      List<CampaignAdExtensionOperation> operations = new List<CampaignAdExtensionOperation>();

      foreach (GeoLocation location in locations) {
        LocationExtension locationExtension = new LocationExtension();
        locationExtension.address = location.address;
        locationExtension.geoPoint = location.geoPoint;
        locationExtension.encodedLocation = location.encodedLocation;
        locationExtension.sourceSpecified = true;
        locationExtension.source = LocationExtensionSource.ADWORDS_FRONTEND;

        CampaignAdExtension extension = new CampaignAdExtension();
        extension.campaignIdSpecified = true;
        extension.campaignId = campaignId;
        extension.statusSpecified = true;
        extension.status = CampaignAdExtensionStatus.ACTIVE;
        extension.adExtension = locationExtension;

        CampaignAdExtensionOperation operation = new CampaignAdExtensionOperation();
        operation.operatorSpecified = true;
        operation.@operator = Operator.ADD;
        operation.operand = extension;

        operations.Add(operation);
      }

      try {
        CampaignAdExtensionReturnValue retval =
            campaignExtensionService.mutate(operations.ToArray());
        if (retval != null && retval.value != null && retval.value.Length > 0) {
          foreach (CampaignAdExtension campaignExtension in retval.value) {
            Console.WriteLine("Created a campaign ad extension with id = \"{0}\" and " +
                "status = \"{1}\"", campaignExtension.adExtension.id, campaignExtension.status);
          }
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to add campaign ad extensions. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
