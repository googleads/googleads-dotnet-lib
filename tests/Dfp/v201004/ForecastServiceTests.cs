// Copyright 2010, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.Dfp.Util.v201004;
using Google.Api.Ads.Dfp.v201004;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Threading;

namespace Google.Api.Ads.Dfp.Tests.v201004 {
  /// <summary>
  /// UnitTests for <see cref="ForecastService"/> class.
  /// </summary>
  [TestFixture]
  public class ForecastServiceTests : BaseTests {
    /// <summary>
    /// ForecastService object to be used in this test.
    /// </summary>
    private ForecastService forecastService;

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
    /// Order id for running tests.
    /// </summary>
    private long orderId;

    /// <summary>
    /// Ad unit id for running tests.
    /// </summary>
    private string adUnitId;

    /// <summary>
    /// Line item id for running tests.
    /// </summary>
    private long lineItemId;

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public ForecastServiceTests() : base() {
    }

    /// <summary>
    /// Initialize the test case.
    /// </summary>
    [SetUp]
    public void Init() {
      TestUtils utils = new TestUtils();
      forecastService = (ForecastService)user.GetService(DfpService.v201004.ForecastService);
      advertiserId = utils.CreateCompany(user, CompanyType.ADVERTISER).id;
      salespersonId = utils.GetSalesperson(user).id;
      traffickerId = utils.GetTrafficker(user).id;

      orderId = utils.CreateOrder(user, advertiserId, salespersonId, traffickerId).id;
      adUnitId = utils.CreateAdUnit(user).id;
      lineItemId = utils.CreateLineItem(user, orderId, adUnitId).id;
    }

    /// <summary>
    /// Test whether we can get a forecast for given line item.
    /// </summary>
    [Test]
    public void TestGetForecast() {
      TestUtils utils = new TestUtils();

      LineItem lineItem = new LineItem();
      lineItem.name = string.Format("Line item #{0}", utils.GetTimeStamp());

      lineItem.orderId = orderId;

      lineItem.targeting = new Targeting();
      lineItem.targeting.inventoryTargeting = new InventoryTargeting();
      lineItem.targeting.inventoryTargeting.targetedAdUnitIds = new string[] {adUnitId};

      Size size1 = new Size();
      size1.width = 300;
      size1.height = 250;

      Size size2 = new Size();
      size2.width = 120;
      size2.height = 600;

      lineItem.creativeSizes = new Size[] {size1, size2};
      lineItem.lineItemType = LineItemType.STANDARD;

      // Set start date time and end date time.
      lineItem.startDateTime = DateTimeUtilities.FromString("20100901 00:00:00");
      lineItem.endDateTime = DateTimeUtilities.FromString("20100930 00:00:00");

      lineItem.costType = CostType.CPM;
      lineItem.costPerUnit = new Money();
      lineItem.costPerUnit.currencyCode = "USD";
      lineItem.costPerUnit.microAmount = 2000000;

      lineItem.creativeRotationType = CreativeRotationType.EVEN;
      lineItem.discountType = LineItemDiscountType.PERCENTAGE;

      lineItem.unitsBought = 500000;
      lineItem.unitType = UnitType.IMPRESSIONS;

      Forecast forecast = null;
      Assert.DoesNotThrow(delegate() {
        forecast = forecastService.getForecast(lineItem);
      });
      Assert.NotNull(forecast);
      Assert.AreEqual(forecast.orderId, orderId);
      Assert.AreEqual(forecast.unitType, lineItem.unitType);
    }

    /// <summary>
    /// Test whether we can get a forecast for existing line item.
    /// </summary>
    [Test]
    public void TestGetForecastById() {
      Forecast forecast = null;
      Assert.DoesNotThrow(delegate() {
        forecast = forecastService.getForecastById(lineItemId);
      });
      Assert.NotNull(forecast);
      Assert.AreEqual(forecast.orderId, orderId);
    }
  }
}
