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
using System.Collections.Generic;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201602 {
  /// <summary>
  /// This code example creates a new line item to serve to video content. To
  /// determine which line items exist, run GetAllLineItems.cs. To determine
  /// which orders exist, run GetAllOrders.cs. To create a video ad unit, run
  /// CreateVideoAdUnit.cs. To determine which content metadata key hierarchies
  /// exist, run GetAllContentMetadataKeyHierarchies.cs.
  /// </summary>
  class CreateVideoLineItem : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example creates a new line item to serve to video content. To determine" +
            " which line items exist, run GetAllLineItems.cs. To determine which orders exist, " +
            "run GetAllOrders.cs. To create a video ad unit, run CreateVideoAdUnit.cs. To " +
            "determine which content metadata key hierarchies exist, run " +
            "GetAllContentMetadataKeyHierarchies.cs";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new CreateVideoLineItem();
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
          (LineItemService) user.GetService(DfpService.v201602.LineItemService);

      // Set the order that all created line items will belong to and the
      // video ad unit ID to target.
      long orderId = long.Parse(_T("INSERT_ORDER_ID_HERE"));
      string targetedVideoAdUnitId = _T("INSERT_TARGETED_VIDEO_AD_UNIT_ID_HERE");

      // Set the custom targeting value ID representing the metadata
      // on the content to target. This would typically be from a key representing
      // a "genre" and a value representing something like "comedy". The value must
      // be from a key in a content metadata key hierarchy.
      long contentCustomTargetingValueId =
          long.Parse(_T("INSERT_CONTENT_CUSTOM_TARGETING_VALUE_ID_HERE"));

      // Create content targeting.
      ContentMetadataKeyHierarchyTargeting contentMetadataTargeting =
          new ContentMetadataKeyHierarchyTargeting();
      contentMetadataTargeting.customTargetingValueIds =
          new long[] {contentCustomTargetingValueId};

      ContentTargeting contentTargeting = new ContentTargeting();
      contentTargeting.targetedContentMetadata =
          new ContentMetadataKeyHierarchyTargeting[] {contentMetadataTargeting};

      // Create inventory targeting.
      InventoryTargeting inventoryTargeting = new InventoryTargeting();
      AdUnitTargeting adUnitTargeting = new AdUnitTargeting();
      adUnitTargeting.adUnitId = targetedVideoAdUnitId;
      adUnitTargeting.includeDescendants = true;
      inventoryTargeting.targetedAdUnits = new AdUnitTargeting[] {adUnitTargeting};

      // Create video position targeting.
      VideoPosition videoPosition = new VideoPosition();
      videoPosition.positionType = VideoPositionType.PREROLL;
      VideoPositionTarget videoPositionTarget = new VideoPositionTarget();
      videoPositionTarget.videoPosition = videoPosition;
      VideoPositionTargeting videoPositionTargeting = new VideoPositionTargeting();
      videoPositionTargeting.targetedPositions = new VideoPositionTarget[] {videoPositionTarget};

      // Create targeting.
      Targeting targeting = new Targeting();
      targeting.contentTargeting = contentTargeting;
      targeting.inventoryTargeting = inventoryTargeting;
      targeting.videoPositionTargeting = videoPositionTargeting;

      // Create local line item object.
      LineItem lineItem = new LineItem();
      lineItem.name = "Video line item - " + this.GetTimeStamp();
      lineItem.orderId = orderId;
      lineItem.targeting = targeting;
      lineItem.lineItemType = LineItemType.SPONSORSHIP;
      lineItem.allowOverbook = true;

      // Set the environment type to video.
      lineItem.environmentType = EnvironmentType.VIDEO_PLAYER;

      // Set the creative rotation type to optimized.
      lineItem.creativeRotationType = CreativeRotationType.OPTIMIZED;

      // Create the master creative placeholder.
      CreativePlaceholder creativeMasterPlaceholder = new CreativePlaceholder();
      Size size1 = new Size();
      size1.width = 400;
      size1.height = 300;
      size1.isAspectRatio = false;
      creativeMasterPlaceholder.size = size1;

      // Create companion creative placeholders.
      CreativePlaceholder companionCreativePlaceholder1 = new CreativePlaceholder();
      Size size2 = new Size();
      size2.width = 300;
      size2.height = 250;
      size2.isAspectRatio = false;
      companionCreativePlaceholder1.size = size2;

      CreativePlaceholder companionCreativePlaceholder2 = new CreativePlaceholder();
      Size size3 = new Size();
      size3.width = 728;
      size3.height = 90;
      size3.isAspectRatio = false;
      companionCreativePlaceholder2.size = size3;

      // Set companion creative placeholders.
      creativeMasterPlaceholder.companions = new CreativePlaceholder[] {
          companionCreativePlaceholder1, companionCreativePlaceholder2};

      // Set the size of creatives that can be associated with this line item.
      lineItem.creativePlaceholders = new CreativePlaceholder[] {creativeMasterPlaceholder};

      // Set delivery of video companions to optional.
      lineItem.companionDeliveryOption = CompanionDeliveryOption.OPTIONAL;

      // Set the line item to run for one month.
      lineItem.startDateTimeType = StartDateTimeType.IMMEDIATELY;
      lineItem.endDateTime =
          DateTimeUtilities.FromDateTime(System.DateTime.Now.AddMonths(1), "America/New_York");

      // Set the cost per day to $1.
      lineItem.costType = CostType.CPD;
      Money money = new Money();
      money.currencyCode = "USD";
      money.microAmount = 1000000L;
      lineItem.costPerUnit = money;

      // Set the percentage to be 100%.
      Goal goal = new Goal();
      goal.goalType = GoalType.DAILY;
      goal.unitType = UnitType.IMPRESSIONS;
      goal.units = 100;
      lineItem.primaryGoal = goal;

      try {
        // Create the line item on the server.
        LineItem[] createdLineItems = lineItemService.createLineItems(new LineItem[] {lineItem});

        foreach (LineItem createdLineItem in createdLineItems) {
          Console.WriteLine("A line item with ID \"{0}\", belonging to order ID \"{1}\", and " +
              "named \"{2}\" was created.", createdLineItem.id, createdLineItem.orderId,
              createdLineItem.name);
        }
      } catch (Exception e) {
        Console.WriteLine("Failed to create line items. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}
