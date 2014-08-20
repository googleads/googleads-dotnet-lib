// Copyright 2014, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdWords.v201406;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201406 {
  /// <summary>
  /// This code example shows how to add a location extension to an existing
  /// campaign. To create a campaign, run AddCampaign.cs.
  ///
  /// Tags: GeoLocationService.get, CampaignAdExtensionService.mutate
  /// </summary>
  public class AddLocationExtension : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      AddLocationExtension codeExample = new AddLocationExtension();
      Console.WriteLine(codeExample.Description);
      try {
        long campaignId = long.Parse("INSERT_CAMPAIGN_ID_HERE");
        codeExample.Run(new AdWordsUser(), campaignId);
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
        return "This code example shows how to add a location extension to an existing " +
            "campaign. To create a campaign, run AddCampaign.cs.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="campaignId">Id of the campaign to which location
    /// extensions are added.</param>
    public void Run(AdWordsUser user, long campaignId) {
      // Get the CampaignAdExtensionService.
      CampaignAdExtensionService campaignExtensionService =
          (CampaignAdExtensionService) user.GetService(AdWordsService.v201406.
          CampaignAdExtensionService);

      // Add location 1: 1600 Amphitheatre Pkwy, Mountain View, US.
      Address address1 = new Address();
      address1.streetAddress = "1600 Amphitheatre Parkway";
      address1.cityName = "Mountain View";
      address1.provinceCode = "CA";
      address1.postalCode = "94043";
      address1.countryCode = "US";

      // Add location 2: 38 avenue de l'Opéra, 75002 Paris, FR.
      Address address2 = new Address();
      address2.streetAddress = "38 avenue de l'Opéra";
      address2.cityName = "Paris";
      address2.postalCode = "75002";
      address2.countryCode = "FR";

      // Get the GeoLocationService.
      GeoLocationService geoService =
          (GeoLocationService) user.GetService(AdWordsService.v201406.GeoLocationService);

      // Create the selector.
      GeoLocationSelector selector = new GeoLocationSelector();
      selector.addresses = new Address[] {address1, address2};

      // Retrieve the locations.
      GeoLocation[] locations = geoService.get(selector);

      List<CampaignAdExtensionOperation> operations = new List<CampaignAdExtensionOperation>();

      // Phone numbers for US and FR offices.
      string[] phoneNumbers = new string[] {"(650) 253-0000", "(0)1 42 68 53 00"};
      int index = 0;

      // Create a location extension for each geo location returned by the
      // server.
      foreach (GeoLocation location in locations) {
        LocationExtension locationExtension = new LocationExtension();
        locationExtension.address = location.address;
        locationExtension.geoPoint = location.geoPoint;
        locationExtension.encodedLocation = location.encodedLocation;
        locationExtension.source = LocationExtensionSource.ADWORDS_FRONTEND;

        // Optional: Set the company name.
        locationExtension.companyName = "ACME Inc.";

        // Optional: Set the phone number.
        locationExtension.phoneNumber = phoneNumbers[index];
        index++;

        CampaignAdExtension extension = new CampaignAdExtension();
        extension.campaignId = campaignId;
        extension.status = CampaignAdExtensionStatus.ENABLED;
        extension.adExtension = locationExtension;

        CampaignAdExtensionOperation operation = new CampaignAdExtensionOperation();
        operation.@operator = Operator.ADD;
        operation.operand = extension;

        operations.Add(operation);
      }

      try {
        CampaignAdExtensionReturnValue retVal =
            campaignExtensionService.mutate(operations.ToArray());

        // Display the results.
        if (retVal != null && retVal.value != null && retVal.value.Length > 0) {
          foreach (CampaignAdExtension campaignExtension in retVal.value) {
            Console.WriteLine("Created a location extension with id = \"{0}\" and " +
                "status = \"{1}\"", campaignExtension.adExtension.id, campaignExtension.status);
          }
        } else {
          Console.WriteLine("No location extensions were created.");
        }
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to add location extension.", ex);
      }
    }
  }
}
