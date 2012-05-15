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
using Google.Api.Ads.AdWords.v201109_1;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201109_1 {
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
      ExampleBase codeExample = new AddLocationExtension();
      Console.WriteLine(codeExample.Description);
      try {
        codeExample.Run(new AdWordsUser(), codeExample.GetParameters(), Console.Out);
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
    /// Gets the list of parameter names required to run this code example.
    /// </summary>
    /// <returns>
    /// A list of parameter names for this code example.
    /// </returns>
    public override string[] GetParameterNames() {
      return new string[] {"CAMPAIGN_ID"};
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
      // Get the CampaignAdExtensionService.
      CampaignAdExtensionService campaignExtensionService =
          (CampaignAdExtensionService) user.GetService(AdWordsService.v201109_1.
          CampaignAdExtensionService);

      long campaignId = long.Parse(parameters["CAMPAIGN_ID"]);

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
          (GeoLocationService) user.GetService(AdWordsService.v201109_1.GeoLocationService);

      // Create the selector.
      GeoLocationSelector selector = new GeoLocationSelector();
      selector.addresses = new Address[] {address1, address2};

      // Retrieve the locations.
      GeoLocation[] locations = geoService.get(selector);

      List<CampaignAdExtensionOperation> operations = new List<CampaignAdExtensionOperation>();

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
        locationExtension.phoneNumber = "(650) 253-0000";

        CampaignAdExtension extension = new CampaignAdExtension();
        extension.campaignId = campaignId;
        extension.status = CampaignAdExtensionStatus.ACTIVE;
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
            writer.WriteLine("Created a location extension with id = \"{0}\" and " +
                "status = \"{1}\"", campaignExtension.adExtension.id, campaignExtension.status);
          }
        } else {
          writer.WriteLine("No location extensions were created.");
        }
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to add location extension.", ex);
      }
    }
  }
}
