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

// Author: api.anash@gmail.com (Anash P. Oommen)

using Google.Api.Ads.Common.Util;
using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.v201505;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Threading;

namespace Google.Api.Ads.Dfp.Tests.v201505 {
  /// <summary>
  /// UnitTests for <see cref="InventoryServiceTests"/> class.
  /// </summary>
  [TestFixture]
  public class InventoryServiceTests : BaseTests {
    /// <summary>
    /// UnitTests for <see cref="InventoryService"/> class.
    /// </summary>
    private InventoryService inventoryService;

    /// <summary>
    /// The ad unit 1 for running tests.
    /// </summary>
    private AdUnit adUnit1;

    /// <summary>
    /// The ad unit 2 for running tests.
    /// </summary>
    private AdUnit adUnit2;

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public InventoryServiceTests() : base() {
    }

    /// <summary>
    /// Initialize the test case.
    /// </summary>
    [SetUp]
    public void Init() {
      TestUtils utils = new TestUtils();
      inventoryService = (InventoryService) user.GetService(DfpService.v201505.InventoryService);
      adUnit1 = utils.CreateAdUnit(user);
      adUnit2 = utils.CreateAdUnit(user);
    }

    /// <summary>
    /// Test whether we can create a list of ad units.
    /// </summary>
    [Test]
    public void TestCreateAdUnits() {
      // Create ad unit 1.
      AdUnit localAdUnit1 = new AdUnit();
      localAdUnit1.name = string.Format("Ad_Unit_{0}", new TestUtils().GetTimeStamp());
      localAdUnit1.parentId = adUnit1.id;

      Size size1 = new Size();
      size1.width = 300;
      size1.height = 250;

      AdUnitSize adUnitSize1 = new AdUnitSize();
      adUnitSize1.size = size1;
      adUnitSize1.environmentType = EnvironmentType.BROWSER;

      localAdUnit1.adUnitSizes = new AdUnitSize[] {adUnitSize1};

      // Create ad unit 2.
      AdUnit localAdUnit2 = new AdUnit();
      localAdUnit2.name = string.Format("Ad_Unit_{0}", new TestUtils().GetTimeStamp());
      localAdUnit2.parentId = adUnit1.id;

      Size size2 = new Size();
      size2.width = 300;
      size2.height = 250;

      AdUnitSize adUnitSize2 = new AdUnitSize();
      adUnitSize2.size = size2;
      adUnitSize2.environmentType = EnvironmentType.BROWSER;

      localAdUnit2.adUnitSizes = new AdUnitSize[] {adUnitSize2};

      AdUnit[] newAdUnits = null;

      Assert.DoesNotThrow(delegate() {
        newAdUnits = inventoryService.createAdUnits(new AdUnit[] {localAdUnit1, localAdUnit2});
      });

      Assert.NotNull(newAdUnits);
      Assert.AreEqual(newAdUnits.Length, 2);

      Assert.AreEqual(newAdUnits[0].name, localAdUnit1.name);
      Assert.AreEqual(newAdUnits[0].parentId, localAdUnit1.parentId);
      Assert.AreEqual(newAdUnits[0].parentId, adUnit1.id);
      Assert.AreEqual(newAdUnits[0].status, localAdUnit1.status);
      Assert.AreEqual(newAdUnits[0].targetWindow, localAdUnit1.targetWindow);

      Assert.AreEqual(newAdUnits[1].name, localAdUnit2.name);
      Assert.AreEqual(newAdUnits[1].parentId, localAdUnit2.parentId);
      Assert.AreEqual(newAdUnits[1].parentId, adUnit1.id);
      Assert.AreEqual(newAdUnits[1].status, localAdUnit2.status);
      Assert.AreEqual(newAdUnits[1].targetWindow, localAdUnit2.targetWindow);
    }

    /// <summary>
    /// Test whether we can fetch a list of existing ad units that match given
    /// statement.
    /// </summary>
    [Test]
    public void TestGetAdUnitsByStatement() {
      Statement statement = new Statement();
      statement.query = string.Format("WHERE id = '{0}' LIMIT 1", adUnit1.id);

      AdUnitPage page = null;

      Assert.DoesNotThrow(delegate() {
        page = inventoryService.getAdUnitsByStatement(statement);
      });

      Assert.NotNull(page);
      Assert.NotNull(page.results);
      Assert.AreEqual(page.totalResultSetSize, 1);
      Assert.NotNull(page.results[0]);

      Assert.AreEqual(page.results[0].name, adUnit1.name);
      Assert.AreEqual(page.results[0].parentId, adUnit1.parentId);
      Assert.AreEqual(page.results[0].id, adUnit1.id);
      Assert.AreEqual(page.results[0].status, adUnit1.status);
      Assert.AreEqual(page.results[0].targetWindow, adUnit1.targetWindow);
    }

    /// <summary>
    /// Test whether we can deactivate an ad unit.
    /// </summary>
    [Test]
    public void TestPerformAdUnitAction() {
      Statement statement = new Statement();
      statement.query = string.Format("WHERE id = '{0}' LIMIT 1", adUnit1.id);

      UpdateResult result = null;
      Assert.DoesNotThrow(delegate() {
        result = inventoryService.performAdUnitAction(new DeactivateAdUnits(), statement);
      });

      Assert.NotNull(result);
      Assert.AreEqual(result.numChanges, 1);
    }

    /// <summary>
    /// Test whether we can update a list of an ad units.
    /// </summary>
    [Test]
    public void TestUpdateAdUnits() {
      List<AdUnitSize> adUnitSizes = null;
      Size size = null;

      // Modify ad unit 1.
      adUnitSizes = new List<AdUnitSize>(adUnit1.adUnitSizes);
      size = new Size();
      size.width = 728;
      size.height = 90;

      // Create ad unit size.
      AdUnitSize adUnitSize = new AdUnitSize();
      adUnitSize.size = size;
      adUnitSize.environmentType = EnvironmentType.BROWSER;

      adUnitSizes.Add(adUnitSize);
      adUnit1.adUnitSizes = adUnitSizes.ToArray();

      // Modify ad unit 2.
      adUnitSizes = new List<AdUnitSize>(adUnit2.adUnitSizes);
      size = new Size();
      size.width = 728;
      size.height = 90;

      // Create ad unit size.
      adUnitSize = new AdUnitSize();
      adUnitSize.size = size;
      adUnitSize.environmentType = EnvironmentType.BROWSER;

      adUnitSizes.Add(adUnitSize);
      adUnit2.adUnitSizes = adUnitSizes.ToArray();

      AdUnit[] newAdUnits = null;
      Assert.DoesNotThrow(delegate() {
        newAdUnits = inventoryService.updateAdUnits(new AdUnit[] {adUnit1, adUnit2});
      });

      Assert.NotNull(newAdUnits);
      Assert.AreEqual(newAdUnits.Length, 2);

      Assert.AreEqual(newAdUnits[0].name, adUnit1.name);
      Assert.AreEqual(newAdUnits[0].parentId, adUnit1.parentId);
      Assert.AreEqual(newAdUnits[0].id, adUnit1.id);
      Assert.AreEqual(newAdUnits[0].status, adUnit1.status);
      Assert.AreEqual(newAdUnits[0].targetWindow, adUnit1.targetWindow);
      Assert.AreEqual(newAdUnits[0].adUnitSizes.Length, adUnit1.adUnitSizes.Length);

      Assert.AreEqual(newAdUnits[1].name, adUnit2.name);
      Assert.AreEqual(newAdUnits[1].parentId, adUnit2.parentId);
      Assert.AreEqual(newAdUnits[1].id, adUnit2.id);
      Assert.AreEqual(newAdUnits[1].status, adUnit2.status);
      Assert.AreEqual(newAdUnits[1].targetWindow, adUnit2.targetWindow);
      Assert.AreEqual(newAdUnits[1].adUnitSizes.Length, adUnit2.adUnitSizes.Length);
    }
  }
}
