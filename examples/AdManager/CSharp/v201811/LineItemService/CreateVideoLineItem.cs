// Copyright 2018, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.AdManager.Lib;
using Google.Api.Ads.AdManager.Util.v201811;
using Google.Api.Ads.AdManager.v201811;

using System;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v201811
{
    /// <summary>
    /// This code example creates a new line item to serve to video content. To
    /// determine which line items exist, run GetAllLineItems.cs. To determine
    /// which orders exist, run GetAllOrders.cs. To create a video ad unit, run
    /// CreateVideoAdUnit.cs. To determine which content metadata key hierarchies
    /// exist, run GetAllContentMetadataKeyHierarchies.cs.
    /// </summary>
    public class CreateVideoLineItem : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example creates a new line item to serve to video content. " +
                    "To determine which line items exist, run GetAllLineItems.cs. To determine " +
                    "which orders exist, run GetAllOrders.cs. To create a video ad unit, " +
                    "run CreateVideoAdUnit.cs. To determine which content metadata key " +
                    "hierarchies exist, run GetAllContentMetadataKeyHierarchies.cs";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            CreateVideoLineItem codeExample = new CreateVideoLineItem();
            Console.WriteLine(codeExample.Description);
            codeExample.Run(new AdManagerUser());
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (LineItemService lineItemService = user.GetService<LineItemService>())
            {
                // Set the order that all created line items will belong to and the
                // video ad unit ID to target.
                long orderId = long.Parse(_T("INSERT_ORDER_ID_HERE"));
                string targetedVideoAdUnitId = _T("INSERT_TARGETED_VIDEO_AD_UNIT_ID_HERE");

                // Set the content bundle to target 
                long contentBundleId =
                    long.Parse(_T("INSERT_CONTENT_BUNDLE_ID_HERE"));

                // Create content targeting.
                ContentTargeting contentTargeting = new ContentTargeting()
                {
                    targetedVideoContentBundleIds = new long[] { contentBundleId }
                };

                // Create inventory targeting.
                InventoryTargeting inventoryTargeting = new InventoryTargeting()
                {
                    targetedAdUnits = new AdUnitTargeting[] {
                      new AdUnitTargeting() {
                        adUnitId = targetedVideoAdUnitId,
                        includeDescendants = true
                      }
                  }
                };

                // Create video position targeting.
                VideoPositionTargeting videoPositionTargeting = new VideoPositionTargeting()
                {
                    targetedPositions = new VideoPositionTarget[] {
                        new VideoPositionTarget() {
                            videoPosition = new VideoPosition() {
                                positionType = VideoPositionType.PREROLL
                            }
                        }
                    }
                };

                // Target only video platforms
                RequestPlatformTargeting requestPlatformTargeting = new RequestPlatformTargeting()
                {
                    targetedRequestPlatforms = new RequestPlatform[] {
                        RequestPlatform.VIDEO_PLAYER
                    }
                };

                // Create targeting.
                Targeting targeting = new Targeting()
                {
                    contentTargeting = contentTargeting,
                    inventoryTargeting = inventoryTargeting,
                    videoPositionTargeting = videoPositionTargeting,
                    requestPlatformTargeting = requestPlatformTargeting
                };

                // Create local line item object.
                LineItem lineItem = new LineItem()
                {
                    name = "Video line item - " + this.GetTimeStamp(),
                    orderId = orderId,
                    targeting = targeting,
                    lineItemType = LineItemType.SPONSORSHIP,
                    allowOverbook = true,
                    environmentType = EnvironmentType.VIDEO_PLAYER,
                    creativeRotationType = CreativeRotationType.OPTIMIZED
                };


                // Create the master creative placeholder.
                CreativePlaceholder creativeMasterPlaceholder = new CreativePlaceholder()
                {
                    size = new Size()
                    {
                        width = 400,
                        height = 300,
                        isAspectRatio = false
                    }
                };

                // Create companion creative placeholders.
                CreativePlaceholder companionCreativePlaceholder1 = new CreativePlaceholder()
                {
                    size = new Size()
                    {
                        width = 300,
                        height = 250,
                        isAspectRatio = false
                    }
                };

                CreativePlaceholder companionCreativePlaceholder2 = new CreativePlaceholder()
                {
                    size = new Size()
                    {
                        width = 728,
                        height = 90,
                        isAspectRatio = false
                    }
                };

                // Set companion creative placeholders.
                creativeMasterPlaceholder.companions = new CreativePlaceholder[]
                {
                    companionCreativePlaceholder1,
                    companionCreativePlaceholder2
                };

                // Set the size of creatives that can be associated with this line item.
                lineItem.creativePlaceholders = new CreativePlaceholder[]
                {
                    creativeMasterPlaceholder
                };

                // Set delivery of video companions to optional.
                lineItem.companionDeliveryOption = CompanionDeliveryOption.OPTIONAL;

                // Set the line item to run for one month.
                lineItem.startDateTimeType = StartDateTimeType.IMMEDIATELY;
                lineItem.endDateTime =
                    DateTimeUtilities.FromDateTime(System.DateTime.Now.AddMonths(1),
                        "America/New_York");

                // Set the cost per day to $1.
                lineItem.costType = CostType.CPD;
                lineItem.costPerUnit = new Money()
                {
                    currencyCode = "USD",
                    microAmount = 1000000L
                };

                // Set the percentage to be 100%.
                lineItem.primaryGoal = new Goal()
                {
                    goalType = GoalType.DAILY,
                    unitType = UnitType.IMPRESSIONS,
                    units = 100
                };

                try
                {
                    // Create the line item on the server.
                    LineItem[] createdLineItems = lineItemService.createLineItems(new LineItem[]
                    {
                        lineItem
                    });

                    foreach (LineItem createdLineItem in createdLineItems)
                    {
                        Console.WriteLine(
                            "A line item with ID \"{0}\", belonging to order ID \"{1}\", and " +
                            "named \"{2}\" was created.", createdLineItem.id,
                            createdLineItem.orderId, createdLineItem.name);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to create line items. Exception says \"{0}\"",
                        e.Message);
                }
            }
        }
    }
}
