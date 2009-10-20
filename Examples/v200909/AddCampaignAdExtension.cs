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
  /// This sample shows how to add an Ad Extension to an existing campaign. To
  /// create a campaign, you can use the AddCampaign sample.
  /// </summary>
  class AddCampaignAdExtension : SampleBase {
    /// <summary>
    /// Returns a description about the sample code.
    /// </summary>
    public override string Description {
      get {
        return "This sample shows how to add an Ad Extension to an existing campaign.";
      }
    }

    /// <summary>
    /// Run the sample code.
    /// </summary>
    /// <param name="user">The AdWords user object running the sample.</param>
    public override void Run(AdWordsUser user) {
      CampaignAdExtensionService campaignExtensionService =
          (CampaignAdExtensionService) user.GetService(AdWordsService.v200909.
          CampaignAdExtensionService);

      CampaignAdExtensionOperation operation = new CampaignAdExtensionOperation();
      operation.operatorSpecified = true;
      operation.@operator = Operator.ADD;

      CampaignAdExtension extension = new CampaignAdExtension();
      extension.campaignIdSpecified = true;
      extension.campaignId = long.Parse(_T("INSERT_CAMPAIGN_ID_HERE"));
      extension.statusSpecified = true;
      extension.status = CampaignAdExtensionStatus.ACTIVE;

      Address address = new Address();
      address.streetAddress = "1600 Amphitheatre Pkwy, Mountain View";
      address.countryCode = "US";

      GeoLocation location = GetLocationForAddress(user, address);

      LocationExtension locationExtension = new LocationExtension();

      // Note: Do not populate an address directly. Instead, use
      // GeoLocationService to obtain the location of an address,
      // and use the address as per the location it returns.
      locationExtension.address = location.address;
      locationExtension.geoPoint = location.geoPoint;
      locationExtension.encodedLocation = location.encodedLocation;
      locationExtension.sourceSpecified = true;
      locationExtension.source = LocationExtensionSource.ADWORDS_FRONTEND;

      extension.adExtension = locationExtension;
      operation.operand = extension;

      try {
        CampaignAdExtensionReturnValue retval =
            campaignExtensionService.mutate(new CampaignAdExtensionOperation[] {operation});
        if (retval != null && retval.value != null && retval.value.Length > 0) {
          CampaignAdExtension campaignExtension = retval.value[0];
          Console.WriteLine("Created a campaign ad extension with id = \"{0}\" and " +
              "status = \"{1}\"", campaignExtension.adExtension.id, campaignExtension.status);
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to obtain location of the given address. " +
            "Exception says \"{0}\"", ex.Message);
      }
    }

    private GeoLocation GetLocationForAddress(AdWordsUser user, Address address) {
      GeoLocationService geoService =
          (GeoLocationService) user.GetService(AdWordsService.v200909.GeoLocationService);

      return geoService.get(new Address[] {address})[0];
    }
  }
}
