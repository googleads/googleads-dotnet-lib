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

using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.Util.v201204;
using Google.Api.Ads.Dfp.v201204;

using System;
using System.Collections.Generic;

namespace Google.Api.Ads.Dfp.Examples.v201204 {
  /// <summary>
  /// This code example creates a new line item to serve to video content. To
  /// determine which line items exist, run GetAllLineItems.cs. To determine
  /// which orders exist, run GetAllOrders.cs. To create a video ad unit, run
  /// CreateVideoAdUnit.cs. To create criteria for categories, run
  /// CreateCustomTargetingKeysAndValues.cs.
  ///
  /// Tags: LineItemService.createLineItem
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
            "create criteria for categories, run CreateCustomTargetingKeysAndValues.cs.";
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
          (LineItemService) user.GetService(DfpService.v201204.LineItemService);

      // Set the order that all created line items will belong to and the
      // video ad unit ID to target.
      long orderId = long.Parse(_T("INSERT_ORDER_ID_HERE"));
      string targetedVideoAdUnitId = _T("INSERT_TARGETED_VIDEO_AD_UNIT_ID_HERE");

      // Set the custom targeting key ID and value ID representing the metadata
      // on the content to target. This would typically be a key representing
      // a "genre" and a value representing something like "comedy".
      long contentCustomTargetingKeyId =
          long.Parse(_T("INSERT_CONTENT_CUSTOM_TARGETING_KEY_ID_HERE"));
      long contentCustomTargetingValueId =
          long.Parse(_T("INSERT_CONTENT_CUSTOM_TARGETING_VALUE_ID_HERE"));

      // Create custom criteria for the content metadata targeting.
      CustomCriteria contentCustomCriteria = new CustomCriteria();
      contentCustomCriteria.keyId = contentCustomTargetingKeyId;
      contentCustomCriteria.valueIds = new long[] {contentCustomTargetingValueId};
      contentCustomCriteria.@operator = CustomCriteriaComparisonOperator.IS;

      // Create custom criteria set.
      CustomCriteriaSet customCriteriaSet = new CustomCriteriaSet();
      customCriteriaSet.children = new CustomCriteriaNode[] {contentCustomCriteria};

      // Create inventory targeting.
      InventoryTargeting inventoryTargeting = new InventoryTargeting();
      AdUnitTargeting adUnitTargeting = new AdUnitTargeting();
      adUnitTargeting.adUnitId = targetedVideoAdUnitId;
      adUnitTargeting.includeDescendants = true;
      inventoryTargeting.targetedAdUnits = new AdUnitTargeting[] {adUnitTargeting};

      // Create video position targeting.
      VideoPositionTargeting videoPositionTargeting = new VideoPositionTargeting();
      videoPositionTargeting.targetedVideoPositions =
          new VideoPositionTargetingType[] {VideoPositionTargetingType.PREROLL};

      // Create targeting.
      Targeting targeting = new Targeting();
      targeting.customTargeting = customCriteriaSet;
      targeting.inventoryTargeting = inventoryTargeting;
      targeting.videoPositionTargeting = videoPositionTargeting;

      // Create local line item object.
      LineItem lineItem = new LineItem();
      lineItem.name = "Video line item";
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

      // Set the length of the line item to run.
      lineItem.startDateTimeType = StartDateTimeType.IMMEDIATELY;
      lineItem.endDateTime = DateTimeUtilities.FromString("20120901 00:00:00");

      // Set the cost per day to $1.
      lineItem.costType = CostType.CPD;
      Money money = new Money();
      money.currencyCode = "USD";
      money.microAmount = 1000000L;
      lineItem.costPerUnit = money;

      // Set the percentage to be 100%.
      lineItem.unitsBought = 100;

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
