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
      inventoryService = (InventoryService) user.GetService(DfpService.v201004.InventoryService);
      adUnit1 = utils.CreateAdUnit(user);
      adUnit2 = utils.CreateAdUnit(user);
    }

    /// <summary>
    /// Test whether we can create an ad unit.
    /// </summary>
    [Test]
    public void TestCreateAdUnit() {
      AdUnit localAdUnit = new AdUnit();
      localAdUnit.name = string.Format("Ad_Unit_{0}", new TestUtils().GetTimeStamp());
      localAdUnit.parentId = adUnit1.id;

      Size size = new Size();
      size.width = 300;
      size.height = 250;

      localAdUnit.sizes = new Size[] {size};

      AdUnit newAdUnit = null;

      Assert.DoesNotThrow(delegate() {
        newAdUnit = inventoryService.createAdUnit(localAdUnit);
      });

      Assert.NotNull(newAdUnit);
      Assert.AreEqual(newAdUnit.name, localAdUnit.name);
      Assert.AreEqual(newAdUnit.parentId, localAdUnit.parentId);
      Assert.AreEqual(newAdUnit.parentId, adUnit1.id);
      Assert.AreEqual(newAdUnit.status, localAdUnit.status);
      Assert.AreEqual(newAdUnit.targetWindow, localAdUnit.targetWindow);
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

      localAdUnit1.sizes = new Size[] {size1};

      // Create ad unit 2.
      AdUnit localAdUnit2 = new AdUnit();
      localAdUnit2.name = string.Format("Ad_Unit_{0}", new TestUtils().GetTimeStamp());
      localAdUnit2.parentId = adUnit1.id;

      Size size2 = new Size();
      size2.width = 300;
      size2.height = 250;

      localAdUnit2.sizes = new Size[] {size2};

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
    /// Test whether we can update an ad unit.
    /// </summary>
    [Test]
    public void TestUpdateAdUnit() {
      List<Size> sizes = new List<Size>(adUnit1.sizes);

      Size size = new Size();
      size.width = 728;
      size.height = 90;

      sizes.Add(size);
      adUnit1.sizes = sizes.ToArray();

      AdUnit newAdUnit = null;
      Assert.DoesNotThrow(delegate() {
        newAdUnit = inventoryService.updateAdUnit(adUnit1);
      });

      Assert.NotNull(newAdUnit);

      Assert.AreEqual(newAdUnit.name, adUnit1.name);
      Assert.AreEqual(newAdUnit.parentId, adUnit1.parentId);
      Assert.AreEqual(newAdUnit.id, adUnit1.id);
      Assert.AreEqual(newAdUnit.status, adUnit1.status);
      Assert.AreEqual(newAdUnit.targetWindow, adUnit1.targetWindow);
      Assert.AreEqual(newAdUnit.sizes.Length, adUnit1.sizes.Length);
    }

    /// <summary>
    /// Test whether we can update a list of an ad units.
    /// </summary>
    [Test]
    public void TestUpdateAdUnits() {
      List<Size> sizes = null;
      Size size = null;

      // Modify ad unit 1.
      sizes = new List<Size>(adUnit1.sizes);
      size = new Size();
      size.width = 728;
      size.height = 90;
      sizes.Add(size);
      adUnit1.sizes = sizes.ToArray();

      // Modify ad unit 2.
      sizes = new List<Size>(adUnit2.sizes);
      size = new Size();
      size.width = 728;
      size.height = 90;
      sizes.Add(size);
      adUnit2.sizes = sizes.ToArray();

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
      Assert.AreEqual(newAdUnits[0].sizes.Length, adUnit1.sizes.Length);

      Assert.AreEqual(newAdUnits[1].name, adUnit2.name);
      Assert.AreEqual(newAdUnits[1].parentId, adUnit2.parentId);
      Assert.AreEqual(newAdUnits[1].id, adUnit2.id);
      Assert.AreEqual(newAdUnits[1].status, adUnit2.status);
      Assert.AreEqual(newAdUnits[1].targetWindow, adUnit2.targetWindow);
      Assert.AreEqual(newAdUnits[1].sizes.Length, adUnit2.sizes.Length);
    }
  }
}
