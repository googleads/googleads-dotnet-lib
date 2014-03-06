// Copyright 2013, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.Util.v201306;
using Google.Api.Ads.Dfp.v201306;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.Dfp.Examples.v201306 {
  /// <summary>
  /// This code example creates a new line item to serve to the mobile platform.
  /// Mobile features needs to be enabled in your account to use mobile
  /// targeting. To determine which line items exist, run GetAllLineItems.cs. To
  /// determine which orders exist, run GetAllOrders.cs. To determine
  /// which placements exist, run GetAllPlacements.cs.
  ///
  /// Tags: LineItemService.getLineItemsByStatement
  /// Tags: LineItemService.performLineItemAction
  /// </summary>
  class CreateMobileLineItem : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example creates a new line item to serve to the mobile platform. Mobile" +
            " features needs to be enabled in your account to use mobile targeting. To determine " +
            "which line items exist, run GetAllLineItems.cs. To determine which orders exist, " +
            "run GetAllOrders.cs. To determine which placements exist, run GetAllPlacements.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new CreateMobileLineItem();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the LineItemService.
      LineItemService lineItemService =
          (LineItemService) user.GetService(DfpService.v201306.LineItemService);

      // Set the order that all created line items will belong to and the
      // placement containing ad units with a mobile target platform.
      long orderId = long.Parse(_T("INSERT_ORDER_ID_HERE"));
      long[] targetedPlacementIds = new long[] {long.Parse(_T("INSERT_MOBILE_PLACEMENT_ID_HERE"))};

      // Create inventory targeting.
      InventoryTargeting inventoryTargeting = new InventoryTargeting();
      inventoryTargeting.targetedPlacementIds = targetedPlacementIds;

      // Create technology targeting.
      TechnologyTargeting technologyTargeting = new TechnologyTargeting();

      // Create device manufacturer targeting.
      DeviceManufacturerTargeting deviceManufacturerTargeting = new DeviceManufacturerTargeting();
      deviceManufacturerTargeting.isTargeted = true;

      // Target the Google device manufacturer (40100).
      Technology deviceManufacturerTechnology = new Technology();
      deviceManufacturerTechnology.id = 40100L;
      deviceManufacturerTargeting.deviceManufacturers =
          new Technology[] {deviceManufacturerTechnology};
      technologyTargeting.deviceManufacturerTargeting = deviceManufacturerTargeting;

      // Create mobile device targeting.
      MobileDeviceTargeting mobileDeviceTargeting = new MobileDeviceTargeting();

      // Exclude the Nexus One device (604046).
      Technology mobileDeviceTechnology = new Technology();
      mobileDeviceTechnology.id = 604046L;
      mobileDeviceTargeting.targetedMobileDevices = new Technology[] {mobileDeviceTechnology};
      technologyTargeting.mobileDeviceTargeting = mobileDeviceTargeting;

      // Create mobile device submodel targeting.
      MobileDeviceSubmodelTargeting mobileDeviceSubmodelTargeting =
          new MobileDeviceSubmodelTargeting();

      // Target the iPhone 4 device submodel (640003).
      Technology mobileDeviceSubmodelTechnology = new Technology();
      mobileDeviceSubmodelTechnology.id = 640003L;
      mobileDeviceSubmodelTargeting.targetedMobileDeviceSubmodels =
          new Technology[] {mobileDeviceSubmodelTechnology};
      technologyTargeting.mobileDeviceSubmodelTargeting = mobileDeviceSubmodelTargeting;

      // Create targeting.
      Targeting targeting = new Targeting();
      targeting.inventoryTargeting = inventoryTargeting;
      targeting.technologyTargeting = technologyTargeting;

      // Create local line item object.
      LineItem lineItem = new LineItem();
      lineItem.name = "Mobile line item";
      lineItem.orderId = orderId;
      lineItem.targeting = targeting;
      lineItem.lineItemType = LineItemType.STANDARD;
      lineItem.allowOverbook = true;

      // Set the target platform to mobile.
      lineItem.targetPlatform = TargetPlatform.MOBILE;

      // Set the creative rotation type to even.
      lineItem.creativeRotationType = CreativeRotationType.EVEN;

      // Create the creative placeholder.
      CreativePlaceholder creativePlaceholder = new CreativePlaceholder();
      Size size = new Size();
      size.width = 300;
      size.height = 250;
      size.isAspectRatio = false;
      creativePlaceholder.size = size;

      // Set the size of creatives that can be associated with this line item.
      lineItem.creativePlaceholders = new CreativePlaceholder[] {creativePlaceholder};

      // Set the length of the line item to run.
      lineItem.startDateTimeType = StartDateTimeType.IMMEDIATELY;
      lineItem.endDateTime = DateTimeUtilities.FromString("20120901 00:00:00");

      // Set the cost per unit to $2.
      lineItem.costType = CostType.CPM;
      Money money = new Money();
      money.currencyCode = "USD";
      money.microAmount = 2000000L;
      lineItem.costPerUnit = money;

      // Set the number of units bought to 500,000 so that the budget is
      // $1,000.
      lineItem.unitsBought = 500000L;
      lineItem.unitType = UnitType.IMPRESSIONS;

      try {
        // Create the line item on the server.
        lineItem = lineItemService.createLineItem(lineItem);

        if (lineItem != null) {
          Console.WriteLine("A line item with ID \"{0}\", belonging to order ID \"{1}\", and " +
              "named \"{2}\" was created.", lineItem.id, lineItem.orderId, lineItem.name);
        } else {
          Console.WriteLine("No line item created.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to create line items. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
