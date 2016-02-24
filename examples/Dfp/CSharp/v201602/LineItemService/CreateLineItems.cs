// Copyright 2015, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.Util.v201602;
using Google.Api.Ads.Dfp.v201602;

using System;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201602 {
  /// <summary>
  /// This code example creates new line items. To determine which line items
  /// exist, run GetAllLineItems.cs. To determine which orders exist, run
  /// GetAllOrders.cs. To determine which placements exist, run
  /// GetAllPlacements.cs. To determine the IDs for locations, run
  /// GetGeoTargets.cs
  /// </summary>
  class CreateLineItems : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example creates new line items. To determine which line items " +
            "exist, run GetAllLineItems.cs. To determine which orders exist, run GetAllOrders.cs" +
            ". To determine which placements exist, run GetAllPlacements.cs. To determine the " +
            "IDs for locations, run GetGeoTargets.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new CreateLineItems();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code examples.
    /// </summary>
    /// <param name="user">The DFP user object running the code examples.</param>
    public override void Run(DfpUser user) {
      // Get the LineItemService.
      LineItemService lineItemService =
          (LineItemService) user.GetService(DfpService.v201602.LineItemService);

      // Set the order that all created line items will belong to and the
      // placement ID to target.
      long orderId = long.Parse(_T("INSERT_ORDER_ID_HERE"));
      long[] targetPlacementIds = new long[] {long.Parse(_T("INSERT_PLACEMENT_ID_HERE"))};

      // Create inventory targeting.
      InventoryTargeting inventoryTargeting = new InventoryTargeting();
      inventoryTargeting.targetedPlacementIds = targetPlacementIds;

      // Create geographical targeting.
      GeoTargeting geoTargeting = new GeoTargeting();

      // Include the US and Quebec, Canada.
      Location countryLocation = new Location();
      countryLocation.id = 2840L;

      Location regionLocation = new Location();
      regionLocation.id = 20123L;
      geoTargeting.targetedLocations = new Location[] {countryLocation, regionLocation};

      Location postalCodeLocation = new Location();
      postalCodeLocation.id = 9000093;

      // Exclude Chicago and the New York metro area.
      Location cityLocation = new Location();
      cityLocation.id = 1016367L;

      Location metroLocation = new Location();
      metroLocation.id = 200501L;
      geoTargeting.excludedLocations = new Location[] {cityLocation, metroLocation};

      // Exclude domains that are not under the network's control.
      UserDomainTargeting userDomainTargeting = new UserDomainTargeting();
      userDomainTargeting.domains = new String[] {"usa.gov"};
      userDomainTargeting.targeted = false;

      // Create day-part targeting.
      DayPartTargeting dayPartTargeting = new DayPartTargeting();
      dayPartTargeting.timeZone = DeliveryTimeZone.BROWSER;

      // Target only the weekend in the browser's timezone.
      DayPart saturdayDayPart = new DayPart();
      saturdayDayPart.dayOfWeek = Google.Api.Ads.Dfp.v201602.DayOfWeek.SATURDAY;

      saturdayDayPart.startTime = new TimeOfDay();
      saturdayDayPart.startTime.hour = 0;
      saturdayDayPart.startTime.minute = MinuteOfHour.ZERO;

      saturdayDayPart.endTime = new TimeOfDay();
      saturdayDayPart.endTime.hour = 24;
      saturdayDayPart.endTime.minute = MinuteOfHour.ZERO;

      DayPart sundayDayPart = new DayPart();
      sundayDayPart.dayOfWeek = Google.Api.Ads.Dfp.v201602.DayOfWeek.SUNDAY;

      sundayDayPart.startTime = new TimeOfDay();
      sundayDayPart.startTime.hour = 0;
      sundayDayPart.startTime.minute = MinuteOfHour.ZERO;

      sundayDayPart.endTime = new TimeOfDay();
      sundayDayPart.endTime.hour = 24;
      sundayDayPart.endTime.minute = MinuteOfHour.ZERO;

      dayPartTargeting.dayParts = new DayPart[] {saturdayDayPart, sundayDayPart};


      // Create technology targeting.
      TechnologyTargeting technologyTargeting = new TechnologyTargeting();

      // Create browser targeting.
      BrowserTargeting browserTargeting = new BrowserTargeting();
      browserTargeting.isTargeted = true;

      // Target just the Chrome browser.
      Technology browserTechnology = new Technology();
      browserTechnology.id = 500072L;
      browserTargeting.browsers = new Technology[] {browserTechnology};
      technologyTargeting.browserTargeting = browserTargeting;

      // Create an array to store local line item objects.
      LineItem[] lineItems = new LineItem[5];

      for (int i = 0; i < 5; i++) {
        LineItem lineItem = new LineItem();
        lineItem.name = "Line item #" + i;
        lineItem.orderId = orderId;
        lineItem.targeting = new Targeting();

        lineItem.targeting.inventoryTargeting = inventoryTargeting;
        lineItem.targeting.geoTargeting = geoTargeting;
        lineItem.targeting.userDomainTargeting = userDomainTargeting;
        lineItem.targeting.dayPartTargeting = dayPartTargeting;
        lineItem.targeting.technologyTargeting = technologyTargeting;

        lineItem.lineItemType = LineItemType.STANDARD;
        lineItem.allowOverbook = true;

        // Set the creative rotation type to even.
        lineItem.creativeRotationType = CreativeRotationType.EVEN;

        // Set the size of creatives that can be associated with this line item.
        Size size = new Size();
        size.width = 300;
        size.height = 250;
        size.isAspectRatio = false;

        // Create the creative placeholder.
        CreativePlaceholder creativePlaceholder = new CreativePlaceholder();
        creativePlaceholder.size = size;

        lineItem.creativePlaceholders = new CreativePlaceholder[] {creativePlaceholder};

        // Set the line item to run for one month.
        lineItem.startDateTimeType = StartDateTimeType.IMMEDIATELY;
        lineItem.endDateTime =
            DateTimeUtilities.FromDateTime(System.DateTime.Today.AddMonths(1), "America/New_York");

        // Set the cost per unit to $2.
        lineItem.costType = CostType.CPM;
        lineItem.costPerUnit = new Money();
        lineItem.costPerUnit.currencyCode = "USD";
        lineItem.costPerUnit.microAmount = 2000000L;

        // Set the number of units bought to 500,000 so that the budget is
        // $1,000.
        Goal goal = new Goal();
        goal.goalType = GoalType.LIFETIME;
        goal.unitType = UnitType.IMPRESSIONS;
        goal.units = 500000L;
        lineItem.primaryGoal = goal;

        lineItems[i] = lineItem;
      }

      try {
        // Create the line items on the server.
        lineItems = lineItemService.createLineItems(lineItems);

        if (lineItems != null) {
          foreach (LineItem lineItem in lineItems) {
            Console.WriteLine("A line item with ID \"{0}\", belonging to order ID \"{1}\", and" +
                " named \"{2}\" was created.", lineItem.id, lineItem.orderId, lineItem.name);
          }
        } else {
          Console.WriteLine("No line items created.");
        }
      } catch (Exception e) {
        Console.WriteLine("Failed to create line items. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}
