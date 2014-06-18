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

using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.v201403;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Threading;
using System.Xml.Serialization;

namespace Google.Api.Ads.Dfp.Tests.v201403 {
  /// <summary>
  /// UnitTests for <see cref="OrderService"/> class.
  /// </summary>
  [TestFixture]
  public class OrderServiceTests : BaseTests {
    /// <summary>
    /// UnitTests for <see cref="OrderService"/> class.
    /// </summary>
    private OrderService orderService;

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
    /// Default public constructor.
    /// </summary>
    public OrderServiceTests() : base() {
    }

    /// <summary>
    /// Initialize the test case.
    /// </summary>
    [SetUp]
    public void Init() {
      TestUtils utils = new TestUtils();
      orderService = (OrderService) user.GetService(DfpService.v201403.OrderService);

      advertiserId = utils.CreateCompany(user, CompanyType.ADVERTISER).id;
      salespersonId = utils.GetSalesperson(user).id;
      traffickerId = utils.GetTrafficker(user).id;

      orderId = utils.CreateOrder(user, advertiserId, salespersonId, traffickerId).id;
    }

    /// <summary>
    /// Test whether we can create a list of orders.
    /// </summary>
    [Test]
    public void TestCreateOrders() {
      Order order1 = new Order();
      order1.name = string.Format("Order #{0}", new TestUtils().GetTimeStamp());
      order1.advertiserId = advertiserId;
      order1.traffickerId = traffickerId;
      order1.currencyCode = "USD";

      Order order2 = new Order();
      order2.name = string.Format("Order #{0}", new TestUtils().GetTimeStamp());
      order2.advertiserId = advertiserId;
      order2.traffickerId = traffickerId;
      order2.currencyCode = "USD";

      Order[] newOrders = null;

      Assert.DoesNotThrow(delegate() {
        newOrders = orderService.createOrders(new Order[] {order1, order2});
      });

      Statement statement = new Statement();
      statement.query = string.Format("WHERE advertiserId = '{0}' LIMIT 500", advertiserId);

      OrderPage page = orderService.getOrdersByStatement(statement);
      Assert.NotNull(newOrders);
      Assert.AreEqual(newOrders.Length, 2);

      Assert.AreEqual(order1.name, newOrders[0].name);
      Assert.AreEqual(order1.advertiserId, newOrders[0].advertiserId);
      Assert.AreEqual(order1.traffickerId, newOrders[0].traffickerId);
      Assert.AreEqual(order1.currencyCode, newOrders[0].currencyCode);

      Assert.AreEqual(order2.name, newOrders[1].name);
      Assert.AreEqual(order2.advertiserId, newOrders[1].advertiserId);
      Assert.AreEqual(order2.traffickerId, newOrders[1].traffickerId);
      Assert.AreEqual(order2.currencyCode, newOrders[1].currencyCode);
    }

    /// <summary>
    /// Test whether we can perform an order action.
    /// </summary>
    [Test]
    public void TestPerformOrderAction() {
      Statement statement = new Statement();
      statement.query = string.Format("WHERE status IN ('{0}', '{1}')",
          OrderStatus.DRAFT, OrderStatus.PENDING_APPROVAL);

      ApproveOrders action = new ApproveOrders();

      UpdateResult result = null;

      Assert.DoesNotThrow(delegate() {
        result = orderService.performOrderAction(action, statement);
      });

      Assert.NotNull(result);
      Assert.GreaterOrEqual(result.numChanges, 0);
    }

    /// <summary>
    /// Test whether we can update a list of orders.
    /// </summary>
    [Test]
    public void TestUpdateOrders() {
      Statement statement = new Statement();
      statement.query = String.Format("WHERE id = {0} LIMIT 1", orderId);
      Order order = orderService.getOrdersByStatement(statement).results[0];
      order.notes = "Spoke to advertiser. All is well.";

      Order[] newOrders = null;

      Assert.DoesNotThrow(delegate() {
        newOrders = orderService.updateOrders(new Order[] {order});
      });

      Assert.NotNull(newOrders);
      Assert.AreEqual(newOrders.Length, 1);

      Assert.AreEqual(order.id, newOrders[0].id);
      Assert.AreEqual(order.name, newOrders[0].name);
      Assert.AreEqual(order.notes, newOrders[0].notes);
      Assert.AreEqual(order.advertiserId, newOrders[0].advertiserId);
      Assert.AreEqual(order.traffickerId, newOrders[0].traffickerId);
      Assert.AreEqual(order.currencyCode, newOrders[0].currencyCode);
    } 
  }


}
