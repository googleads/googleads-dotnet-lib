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
  /// This code example gets a forecast for a prospective line item. To
  /// determine which placements exist, run GetAllPlacements.cs.
  /// </summary>
  class GetForecast : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets a forecast for a prospective line item. To determine " +
            "which placements exist, run GetAllPlacements.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetForecast();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the ForecastService.
      ForecastService forecastService =
          (ForecastService)user.GetService(DfpService.v201602.ForecastService);

      // Set the placement that the prospective line item will target.
      long[] targetPlacementIds = new long[] {long.Parse(_T("INSERT_PLACEMENT_ID_HERE"))};

      // Set the ID of the advertiser (company) to forecast for. Setting an advertiser
      // will cause the forecast to apply the appropriate unified blocking rules.
      long advertiserId = long.Parse(_T("INSERT_ADVERTISER_ID_HERE"));

      // Create prospective line item.
      LineItem lineItem = new LineItem();

      lineItem.targeting = new Targeting();
      lineItem.targeting.inventoryTargeting = new InventoryTargeting();
      lineItem.targeting.inventoryTargeting.targetedPlacementIds = targetPlacementIds;

      Size size = new Size();
      size.width = 300;
      size.height = 250;

      // Create the creative placeholder.
      CreativePlaceholder creativePlaceholder = new CreativePlaceholder();
      creativePlaceholder.size = size;

      lineItem.creativePlaceholders = new CreativePlaceholder[] {creativePlaceholder};

      lineItem.lineItemType = LineItemType.SPONSORSHIP;
      lineItem.startDateTimeType = StartDateTimeType.IMMEDIATELY;

      // Set the line item to run for one month.
      lineItem.endDateTime =
          DateTimeUtilities.FromDateTime(System.DateTime.Now.AddMonths(1), "America/New_York");

      // Set the cost type to match the unit type.
      lineItem.costType = CostType.CPM;
      Goal goal = new Goal();
      goal.goalType = GoalType.DAILY;
      goal.unitType = UnitType.IMPRESSIONS;
      goal.units = 50L;
      lineItem.primaryGoal = goal;

      try {
        // [START forecasting_3] MOE:strip_line
        // Get availability forecast.
        AvailabilityForecastOptions options = new AvailabilityForecastOptions();
        options.includeContendingLineItems = true;
        options.includeTargetingCriteriaBreakdown = true;
        ProspectiveLineItem prospectiveLineItem = new ProspectiveLineItem();
        prospectiveLineItem.advertiserId = advertiserId;
        prospectiveLineItem.lineItem = lineItem;
        AvailabilityForecast forecast =
            forecastService.getAvailabilityForecast(prospectiveLineItem, options);
        // [END forecasting_3] MOE:strip_line

        // Display results.
        long matched = forecast.matchedUnits;
        double availablePercent = (double) (forecast.availableUnits / (matched * 1.0)) * 100;
        String unitType = forecast.unitType.ToString().ToLower();
        Console.WriteLine("{0} {1} matched.\n{2}%  available.", matched, unitType,
            availablePercent, unitType);

        if (forecast.possibleUnitsSpecified) {
          double possiblePercent = (double) (forecast.possibleUnits / (matched * 1.0)) * 100;
          Console.WriteLine("{0}% {1} possible.\n", possiblePercent, unitType);
        }
        Console.WriteLine("{0} contending line items.", (forecast.contendingLineItems != null) ?
            forecast.contendingLineItems.Length : 0);
      } catch (Exception e) {
        Console.WriteLine("Failed to get forecast. Exception says \"{0}\"", e.Message);
      }
    }
  }
}
