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

using Google.Api.Ads.Common.Util;
using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.v201004;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Threading;

namespace Google.Api.Ads.Dfp.Tests.v201004 {
  /// <summary>
  /// UnitTests for <see cref="LineItemCreativeAssociationServiceTests"/> class.
  /// </summary>
  [TestFixture]
  public class LineItemCreativeAssociationServiceTests : BaseTests {
    /// <summary>
    /// UnitTests for <see cref="LineItemCreativeAssociationService"/> class.
    /// </summary>
    private LineItemCreativeAssociationService licaService;

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
    /// Creative id 1 for running tests.
    /// </summary>
    private long creativeId1;

    /// <summary>
    /// Creative id 2 for running tests.
    /// </summary>
    private long creativeId2;

    /// <summary>
    /// Creative id 3 for running tests.
    /// </summary>
    private long creativeId3;

    /// <summary>
    /// Creative id 4 for running tests.
    /// </summary>
    private long creativeId4;

    /// <summary>
    /// Line item id 1 for running tests.
    /// </summary>
    private long lineItemId1;

    /// <summary>
    /// Line item id 2 for running tests.
    /// </summary>
    private long lineItemId2;

    /// <summary>
    /// Line item id 3 for running tests.
    /// </summary>
    private long lineItemId3;

    /// <summary>
    /// Line item id 4 for running tests.
    /// </summary>
    private long lineItemId4;

    /// <summary>
    /// Line item creative association 1 for running tests.
    /// </summary>
    private LineItemCreativeAssociation lica1;

    /// <summary>
    /// Line item creative association 2 for running tests.
    /// </summary>
    private LineItemCreativeAssociation lica2;

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public LineItemCreativeAssociationServiceTests() : base() {
    }

    /// <summary>
    /// Initialize the test case.
    /// </summary>
    [SetUp]
    public void Init() {
      TestUtils utils = new TestUtils();
      licaService = (LineItemCreativeAssociationService) user.GetService(
          DfpService.v201004.LineItemCreativeAssociationService);
      advertiserId = utils.CreateCompany(user, CompanyType.ADVERTISER).id;
      salespersonId = utils.GetSalesperson(user).id;
      traffickerId = utils.GetTrafficker(user).id;

      orderId = utils.CreateOrder(user, advertiserId, salespersonId, traffickerId).id;
      adUnitId = utils.CreateAdUnit(user).id;

      lineItemId1 = utils.CreateLineItem(user, orderId, adUnitId).id;
      lineItemId2 = utils.CreateLineItem(user, orderId, adUnitId).id;
      lineItemId3 = utils.CreateLineItem(user, orderId, adUnitId).id;
      lineItemId4 = utils.CreateLineItem(user, orderId, adUnitId).id;

      creativeId1 = utils.CreateCreative(user, advertiserId).id;
      creativeId2 = utils.CreateCreative(user, advertiserId).id;
      creativeId3 = utils.CreateCreative(user, advertiserId).id;
      creativeId4 = utils.CreateCreative(user, advertiserId).id;

      lica1 = utils.CreateLica(user, lineItemId3, creativeId3);
      lica2 = utils.CreateLica(user, lineItemId4, creativeId4);
    }

    /// <summary>
    /// Test whether we can create a line item creative association.
    /// </summary>
    [Test]
    public void TestCreateLineItemCreativeAssociation() {
      LineItemCreativeAssociation lica = new LineItemCreativeAssociation();
      lica.creativeId = creativeId1;
      lica.lineItemId = lineItemId1;

      LineItemCreativeAssociation newLica = null;

      Assert.DoesNotThrow(delegate() {
        newLica = licaService.createLineItemCreativeAssociation(lica);
      });

      Assert.NotNull(newLica);
      Assert.AreEqual(newLica.creativeId, lica.creativeId);
      Assert.AreEqual(newLica.lineItemId, lica.lineItemId);
    }

    /// <summary>
    /// Test whether we can create a list of line item creative associations.
    /// </summary>
    [Test]
    public void TestCreateLineItemCreativeAssociations() {
      LineItemCreativeAssociation localLica1 = new LineItemCreativeAssociation();
      localLica1.creativeId = creativeId1;
      localLica1.lineItemId = lineItemId1;

      LineItemCreativeAssociation localLica2 = new LineItemCreativeAssociation();
      localLica2.creativeId = creativeId2;
      localLica2.lineItemId = lineItemId2;

      LineItemCreativeAssociation[] newLicas = null;

      Assert.DoesNotThrow(delegate() {
        newLicas = licaService.createLineItemCreativeAssociations(
            new LineItemCreativeAssociation[] {localLica1, localLica2});
      });

      Assert.NotNull(newLicas);
      Assert.AreEqual(newLicas.Length, 2);

      Assert.NotNull(newLicas[0]);
      Assert.AreEqual(newLicas[0].creativeId, localLica1.creativeId);
      Assert.AreEqual(newLicas[0].lineItemId, localLica1.lineItemId);

      Assert.NotNull(newLicas[1]);
      Assert.AreEqual(newLicas[1].creativeId, localLica2.creativeId);
      Assert.AreEqual(newLicas[1].lineItemId, localLica2.lineItemId);
    }

    /// <summary>
    /// Test whether we can fetch an existing line item creative association.
    /// </summary>
    [Test]
    public void TestGetLineItemCreativeAssociation() {
      LineItemCreativeAssociation newLica = null;

      Assert.DoesNotThrow(delegate() {
        newLica = licaService.getLineItemCreativeAssociation(lineItemId3, creativeId3);
      });

      Assert.NotNull(newLica);
      Assert.AreEqual(newLica.creativeId, lica1.creativeId);
      Assert.AreEqual(newLica.lineItemId, lica1.lineItemId);
    }

    /// <summary>
    /// Test whether we can fetch a list of existing line item creative
    /// associations that match given statement.
    /// </summary>
    [Test]
    public void TestGetLineItemCreativeAssociationsByStatement() {
      Statement statement = new Statement();
      statement.query = string.Format("WHERE lineItemId = '{0}' LIMIT 500", lineItemId3);

      LineItemCreativeAssociationPage page = null;
      Assert.DoesNotThrow(delegate() {
        page = licaService.getLineItemCreativeAssociationsByStatement(statement);
      });
      Assert.NotNull(page);
      Assert.NotNull(page.results);
      Assert.AreEqual(page.totalResultSetSize, 1);

      Assert.NotNull(page.results[0]);
      Assert.AreEqual(page.results[0].creativeId, lica1.creativeId);
      Assert.AreEqual(page.results[0].lineItemId, lica1.lineItemId);
    }

    /// <summary>
    /// Test whether we can deactivate a line item create association.
    /// </summary>
    [Test]
    public void TestPerformLineItemCreativeAssociationAction() {
      Statement statement = new Statement();
      statement.query = string.Format("WHERE lineItemId = '{0}' LIMIT 1", lineItemId3);

      DeactivateLineItemCreativeAssociations action = new DeactivateLineItemCreativeAssociations();

      UpdateResult result = null;
      Assert.DoesNotThrow(delegate() {
        result = licaService.performLineItemCreativeAssociationAction(action, statement);
      });

      Assert.NotNull(result);
      Assert.AreEqual(result.numChanges, 1);
    }

    /// <summary>
    /// Test whether we can update a line item creative association.
    /// </summary>
    [Test]
    public void TestUpdateLineItemCreativeAssociation() {
      lica1.destinationUrl = "http://news.google.com";

      LineItemCreativeAssociation newLica = null;
      Assert.DoesNotThrow(delegate() {
        newLica = licaService.updateLineItemCreativeAssociation(lica1);
      });

      Assert.NotNull(newLica);
      Assert.AreEqual(newLica.creativeId, lica1.creativeId);
      Assert.AreEqual(newLica.lineItemId, lica1.lineItemId);
      Assert.AreEqual(newLica.destinationUrl, lica1.destinationUrl);
    }

    /// <summary>
    /// Test whether we can update a list of line item creative associations.
    /// </summary>
    [Test]
    public void TestUpdateLineItemCreativeAssociations() {
      lica1.destinationUrl = "http://news.google.com";
      lica2.destinationUrl = "http://news.google.com";

      LineItemCreativeAssociation[] newLicas = null;
      Assert.DoesNotThrow(delegate() {
        newLicas = licaService.updateLineItemCreativeAssociations(
            new LineItemCreativeAssociation[] {lica1, lica2});
      });

      Assert.NotNull(newLicas);
      Assert.AreEqual(newLicas.Length, 2);

      Assert.NotNull(newLicas[0]);
      Assert.AreEqual(newLicas[0].creativeId, lica1.creativeId);
      Assert.AreEqual(newLicas[0].lineItemId, lica1.lineItemId);

      Assert.NotNull(newLicas[1]);
      Assert.AreEqual(newLicas[1].creativeId, lica2.creativeId);
      Assert.AreEqual(newLicas[1].lineItemId, lica2.lineItemId);
    }
  }
}
