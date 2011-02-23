// Copyright 2011, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.Dfp.Util.v201101;
using Google.Api.Ads.Dfp.v201101;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Threading;

namespace Google.Api.Ads.Dfp.Tests.v201101 {
  /// <summary>
  /// UnitTests for <see cref="LineItemService"/> class.
  /// </summary>
  [TestFixture]
  public class LineItemServiceTests : BaseTests {
    /// <summary>
    /// UnitTests for <see cref="LineItemService"/> class.
    /// </summary>
    private LineItemService lineItemService;

    /// <summary>
    /// Advertiser company id for running tests.
    /// </summary>
    private long advertiserId;

    /// <summary>
    /// Salesperson user id for running tests.
    /// </summary>
    private long salespersonId;

    /// <summary>
    /// Trafficker user id for running tests.
    /// </summary>
    private long traffickerId;

    /// <summary>
    /// Ad unit id for running tests.
    /// </summary>
    private string adUnitId;

    /// <summary>
    /// Order id for running tests.
    /// </summary>
    private long orderId;

    /// <summary>
    /// Line item 1 for running tests.
    /// </summary>
    private LineItem lineItem1;

    /// <summary>
    /// Line item 2 for running tests.
    /// </summary>
    private LineItem lineItem2;

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public LineItemServiceTests() : base() {
    }

    /// <summary>
    /// Initialize the test case.
    /// </summary>
    [SetUp]
    public void Init() {
      TestUtils utils = new TestUtils();
      lineItemService = (LineItemService)user.GetService(DfpService.v201101.LineItemService);

      advertiserId = utils.CreateCompany(user, CompanyType.ADVERTISER).id;
      salespersonId = utils.GetSalesperson(user).id;
      traffickerId = utils.GetTrafficker(user).id;
      orderId = utils.CreateOrder(user, advertiserId, salespersonId, traffickerId).id;
      adUnitId = utils.CreateAdUnit(user).id;
      lineItem1 = utils.CreateLineItem(user, orderId, adUnitId);
      lineItem2 = utils.CreateLineItem(user, orderId, adUnitId);
    }

    /// <summary>
    /// Test whether we can create a line item creative association.
    /// </summary>
    [Test]
    public void TestCreateLineItem() {
      string[] targetAdUnitIds = new string[] {adUnitId};

      LineItem lineItem = new LineItem();
      lineItem.name = string.Format("Line item #{0}", new TestUtils().GetTimeStamp());
      lineItem.orderId = orderId;

      Targeting targeting = new Targeting();
      targeting.inventoryTargeting = new InventoryTargeting();
      targeting.inventoryTargeting.targetedAdUnitIds = targetAdUnitIds;
      lineItem.targeting = targeting;
      lineItem.lineItemType = LineItemType.STANDARD;

      // Set the creative rotation type to even.
      lineItem.creativeRotationType = CreativeRotationType.EVEN;

      // Set the size of creatives that can be associated with this line item.
      Size size = new Size();
      size.width = 300;
      size.height = 250;

      lineItem.creativeSizes = new Size[] {size};

      // Set the length of the line item to run.
      lineItem.startDateTime = DateTimeUtilities.FromDateTime(System.DateTime.Today.AddDays(1));
      lineItem.startDateTime.timeZoneID = "America/New_York";

      lineItem.endDateTime = DateTimeUtilities.FromDateTime(System.DateTime.Today.AddMonths(1));
      lineItem.endDateTime.timeZoneID = "America/New_York";

      // Set the cost per unit to $2.
      lineItem.costType = CostType.CPM;
      lineItem.costPerUnit = new Money();
      lineItem.costPerUnit.currencyCode = "USD";
      lineItem.costPerUnit.microAmount = 2000000L;

      // Set the number of units bought to 500,000 so that the budget is
      // $1,000.
      lineItem.unitsBought = 500000L;
      lineItem.unitType = UnitType.IMPRESSIONS;

      LineItem localLineItem = null;

      Assert.DoesNotThrow(delegate() {
        localLineItem = lineItemService.createLineItem(lineItem);
      });

      Assert.NotNull(localLineItem);
      Assert.AreEqual(localLineItem.name, lineItem.name);
      Assert.AreEqual(localLineItem.orderId, lineItem.orderId);
    }

    /// <summary>
    /// Test whether we can create a list of line items.
    /// </summary>
    [Test]
    public void TestCreateLineItems() {
      string[] targetAdUnitIds = new string[] {adUnitId};

      LineItem[] lineItems = new LineItem[2];
      for (int i = 0; i < lineItems.Length; i++) {
        LineItem lineItem = new LineItem();
        lineItem.name = string.Format("Line item #{0}", new TestUtils().GetTimeStamp());
        lineItem.orderId = orderId;

        Targeting targeting = new Targeting();
        targeting.inventoryTargeting = new InventoryTargeting();
        targeting.inventoryTargeting.targetedAdUnitIds = targetAdUnitIds;
        lineItem.targeting = targeting;
        lineItem.lineItemType = LineItemType.STANDARD;

        // Set the creative rotation type to even.
        lineItem.creativeRotationType = CreativeRotationType.EVEN;

        // Set the size of creatives that can be associated with this line item.
        Size size = new Size();
        size.width = 300;
        size.height = 250;

        lineItem.creativeSizes = new Size[] {size};

        // Set the length of the line item to run.
        lineItem.startDateTime = DateTimeUtilities.FromDateTime(System.DateTime.Today.AddDays(1));
        lineItem.startDateTime.timeZoneID = "America/New_York";

        lineItem.endDateTime = DateTimeUtilities.FromDateTime(System.DateTime.Today.AddMonths(1));
        lineItem.endDateTime.timeZoneID = "America/New_York";

        // Set the cost per unit to $2.
        lineItem.costType = CostType.CPM;
        lineItem.costPerUnit = new Money();
        lineItem.costPerUnit.currencyCode = "USD";
        lineItem.costPerUnit.microAmount = 2000000L;

        // Set the number of units bought to 500,000 so that the budget is
        // $1,000.
        lineItem.unitsBought = 500000L;
        lineItem.unitType = UnitType.IMPRESSIONS;

        lineItems[i] = lineItem;
      }
      LineItem[] localLineItems = null;

      Assert.DoesNotThrow(delegate() {
        localLineItems = lineItemService.createLineItems(lineItems);
      });

      Assert.NotNull(localLineItems);
      Assert.AreEqual(localLineItems.Length, 2);

      Assert.AreEqual(localLineItems[0].name, lineItems[0].name);
      Assert.AreEqual(localLineItems[0].orderId, lineItems[0].orderId);

      Assert.AreEqual(localLineItems[1].name, lineItems[1].name);
      Assert.AreEqual(localLineItems[1].orderId, lineItems[1].orderId);
    }

    /// <summary>
    /// Test whether we can fetch an existing line item.
    /// </summary>
    [Test]
    public void TestGetLineItem() {
      LineItem localLineItem = null;

      Assert.DoesNotThrow(delegate() {
        localLineItem = lineItemService.getLineItem(lineItem1.id);
      });

      Assert.NotNull(localLineItem);
      Assert.AreEqual(localLineItem.id, lineItem1.id);
      Assert.AreEqual(localLineItem.name, lineItem1.name);
      Assert.AreEqual(localLineItem.orderId, lineItem1.orderId);
    }

    /// <summary>
    /// Test whether we can fetch a list of existing line items that match given
    /// statement.
    /// </summary>
    [Test]
    public void TestGetLineItemsByStatement() {
      Statement statement = new Statement();
      statement.query = string.Format("WHERE id = '{0}' LIMIT 1", lineItem1.id);

      LineItemPage page = null;

      Assert.DoesNotThrow(delegate() {
        page = lineItemService.getLineItemsByStatement(statement);
      });

      Assert.NotNull(page);
      Assert.AreEqual(page.totalResultSetSize, 1);
      Assert.NotNull(page.results);
      Assert.AreEqual(page.results[0].id, lineItem1.id);
      Assert.AreEqual(page.results[0].name, lineItem1.name);
      Assert.AreEqual(page.results[0].orderId, lineItem1.orderId);
    }

    /// <summary>
    /// Test whether we can activate a line item.
    /// </summary>
    [Test]
    public void TestPerformLineItemAction() {
      Statement statement = new Statement();
      statement.query = string.Format("WHERE orderId = '{0}' and status = '{1}'",
          orderId, ComputedStatus.NEEDS_CREATIVES);

      ActivateLineItems action = new ActivateLineItems();
      UpdateResult result = null;

      Assert.DoesNotThrow(delegate() {
        result = lineItemService.performLineItemAction(action, statement);
      });

      Assert.NotNull(result);
    }

    /// <summary>
    /// Test whether we can update a line item.
    /// </summary>
    [Test]
    public void TestUpdateLineItem() {
      lineItem1.deliveryRateType = DeliveryRateType.AS_FAST_AS_POSSIBLE;

      LineItem localLineItem = null;
      Assert.DoesNotThrow(delegate() {
        localLineItem = lineItemService.updateLineItem(lineItem1);
      });

      Assert.NotNull(localLineItem);
      Assert.AreEqual(localLineItem.id, lineItem1.id);
      Assert.AreEqual(localLineItem.name, lineItem1.name);
      Assert.AreEqual(localLineItem.orderId, lineItem1.orderId);
      Assert.AreEqual(localLineItem.deliveryRateType, lineItem1.deliveryRateType);
    }

    /// <summary>
    /// Test whether we can update a list of line items.
    /// </summary>
    [Test]
    public void TestUpdateLineItems() {
      lineItem1.costPerUnit.microAmount = 3500000;
      lineItem2.costPerUnit.microAmount = 3500000;

      LineItem[] localLineItems = null;

      Assert.DoesNotThrow(delegate() {
        localLineItems = lineItemService.updateLineItems(new LineItem[] {lineItem1, lineItem2});
      });

      Assert.NotNull(localLineItems);
      Assert.AreEqual(localLineItems.Length, 2);

      Assert.AreEqual(localLineItems[0].id, lineItem1.id);
      Assert.AreEqual(localLineItems[0].name, lineItem1.name);
      Assert.AreEqual(localLineItems[0].orderId, lineItem1.orderId);
      Assert.AreEqual(localLineItems[0].costPerUnit.microAmount, lineItem1.costPerUnit.microAmount);

      Assert.AreEqual(localLineItems[1].id, lineItem2.id);
      Assert.AreEqual(localLineItems[1].name, lineItem2.name);
      Assert.AreEqual(localLineItems[1].orderId, lineItem2.orderId);
      Assert.AreEqual(localLineItems[1].costPerUnit.microAmount, lineItem2.costPerUnit.microAmount);
    }
  }
}
