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
using Google.Api.Ads.Dfp.v201204;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Threading;

namespace Google.Api.Ads.Dfp.Tests.v201204 {
  /// <summary>
  /// UnitTests for <see cref="PlacementService"/> class.
  /// </summary>
  [TestFixture]
  public class PlacementServiceTests : BaseTests {
    /// <summary>
    /// UnitTests for <see cref="PlacementService"/> class.
    /// </summary>
    private PlacementService placementService;

    /// <summary>
    /// The ad unit 1 for running tests.
    /// </summary>
    private AdUnit adUnit1;

    /// <summary>
    /// The ad unit 2 for running tests.
    /// </summary>
    private AdUnit adUnit2;

    /// <summary>
    /// The placement for running tests.
    /// </summary>
    private Placement placement;

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public PlacementServiceTests() : base() {
    }

    /// <summary>
    /// Initialize the test case.
    /// </summary>
    [SetUp]
    public void Init() {
      TestUtils utils = new TestUtils();
      placementService = (PlacementService) user.GetService(DfpService.v201204.PlacementService);
      adUnit1 = utils.CreateAdUnit(user);
      adUnit2 = utils.CreateAdUnit(user);
      placement = utils.CreatePlacement(user, new string[] {adUnit1.id, adUnit2.id});
    }

    /// <summary>
    /// Test whether we can create a placement.
    /// </summary>
    [Test]
    public void TestCreatePlacement() {
      Placement placement = new Placement();
      placement.name = string.Format("Medium Square AdUnit Placement #{0}",
          new TestUtils().GetTimeStamp());
      placement.description = "Contains ad units that can hold creatives of size 300x250";
      placement.targetedAdUnitIds = new string[] {adUnit1.id, adUnit2.id};

      Placement newPlacement = null;

      Assert.DoesNotThrow(delegate() {
        newPlacement = placementService.createPlacement(placement);
      });

      Assert.NotNull(newPlacement);
      Assert.AreEqual(newPlacement.name, placement.name);
      Assert.AreEqual(newPlacement.description, placement.description);
      Assert.Contains(adUnit1.id, newPlacement.targetedAdUnitIds);
      Assert.Contains(adUnit2.id, newPlacement.targetedAdUnitIds);
    }

    /// <summary>
    /// Test whether we can create a list of placements items.
    /// </summary>
    [Test]
    public void TestCreatePlacements() {
      TestUtils utils = new TestUtils();
      Placement placement1 = new Placement();
      placement1.name = string.Format("Medium Square AdUnit Placement #{0}", utils.GetTimeStamp());
      placement1.description = "Contains ad units that can hold creatives of size 300x250";
      placement1.targetedAdUnitIds = new string[] {adUnit1.id, adUnit2.id};

      Placement placement2 = new Placement();
      placement2.name = string.Format("Medium Square AdUnit Placement #{0}", utils.GetTimeStamp());
      placement2.description = "Contains ad units that can hold creatives of size 300x250";
      placement2.targetedAdUnitIds = new string[] {adUnit1.id, adUnit2.id};

      Placement[] newPlacements = null;

      Assert.DoesNotThrow(delegate() {
        newPlacements = placementService.createPlacements(new Placement[] {placement1, placement2});
      });

      Assert.NotNull(newPlacements);
      Assert.AreEqual(newPlacements.Length, 2);

      Assert.NotNull(newPlacements[0]);
      Assert.AreEqual(newPlacements[0].name, placement1.name);
      Assert.AreEqual(newPlacements[0].description, placement1.description);
      Assert.Contains(adUnit1.id, newPlacements[0].targetedAdUnitIds);
      Assert.Contains(adUnit2.id, newPlacements[0].targetedAdUnitIds);

      Assert.NotNull(newPlacements[1]);
      Assert.AreEqual(newPlacements[1].name, placement2.name);
      Assert.AreEqual(newPlacements[1].description, placement2.description);
      Assert.Contains(adUnit1.id, newPlacements[1].targetedAdUnitIds);
      Assert.Contains(adUnit2.id, newPlacements[1].targetedAdUnitIds);
    }

    /// <summary>
    /// Test whether we can create a list of placements items.
    /// </summary>
    [Test]
    public void TestGetPlacement() {
      Placement newPlacement = null;

      Assert.DoesNotThrow(delegate() {
        newPlacement = placementService.getPlacement(placement.id);
      });

      Assert.NotNull(newPlacement);
      Assert.AreEqual(newPlacement.name, placement.name);
      Assert.AreEqual(newPlacement.description, placement.description);
      Assert.Contains(adUnit1.id, newPlacement.targetedAdUnitIds);
      Assert.Contains(adUnit2.id, newPlacement.targetedAdUnitIds);
    }

    /// <summary>
    /// Test whether we can fetch a list of existing placements that match given
    /// statement.
    /// </summary>
    [Test]
    public void TestGetPlacementsByStatement() {
      // Create a Statement to only select active placements.
      Statement statement = new Statement();
      statement.query = string.Format("WHERE id = '{0}'", placement.id);

      PlacementPage page = null;

      Assert.DoesNotThrow(delegate() {
        page = placementService.getPlacementsByStatement(statement);
      });

      Assert.NotNull(page);
      Assert.NotNull(page.results);
      Assert.AreEqual(page.results.Length, 1);

      Assert.AreEqual(page.results[0].id, placement.id);
      Assert.AreEqual(page.results[0].name, placement.name);
      Assert.AreEqual(page.results[0].description, placement.description);
      Assert.Contains(adUnit1.id, page.results[0].targetedAdUnitIds);
      Assert.Contains(adUnit2.id, page.results[0].targetedAdUnitIds);
    }

    /// <summary>
    /// Test whether we can deactivate a placement.
    /// </summary>
    [Test]
    public void TestPerformPlacementAction() {
      Statement statement = new Statement();
      statement.query = string.Format("WHERE status = '{0}'", InventoryStatus.ACTIVE);

      DeactivatePlacements action = new DeactivatePlacements();

      UpdateResult result = null;

      Assert.DoesNotThrow(delegate() {
        result = placementService.performPlacementAction(action, statement);
      });

      Assert.NotNull(result);
      Assert.GreaterOrEqual(result.numChanges, 0);
    }

    /// <summary>
    /// Test whether we can update a placement.
    /// </summary>
    [Test]
    public void TestUpdatePlacement() {
      placement.description += "More description";

      Placement newPlacement = null;

      Assert.DoesNotThrow(delegate() {
        newPlacement = placementService.updatePlacement(placement);
      });

      Assert.NotNull(newPlacement);
      Assert.AreEqual(newPlacement.name, placement.name);
      Assert.AreEqual(newPlacement.description, placement.description);
      Assert.Contains(adUnit1.id, newPlacement.targetedAdUnitIds);
      Assert.Contains(adUnit2.id, newPlacement.targetedAdUnitIds);
    }

    /// <summary>
    /// Test whether we can update a list of placements.
    /// </summary>
    [Test]
    public void TestUpdatePlacements() {
      placement.description += "More description";

      Placement[] newPlacements = null;

      Assert.DoesNotThrow(delegate() {
        newPlacements = placementService.updatePlacements(new Placement[] {placement});
      });

      Assert.NotNull(newPlacements);
      Assert.AreEqual(newPlacements.Length, 1);

      Assert.NotNull(newPlacements[0]);
      Assert.AreEqual(newPlacements[0].name, placement.name);
      Assert.AreEqual(newPlacements[0].description, placement.description);
      Assert.Contains(adUnit1.id, newPlacements[0].targetedAdUnitIds);
      Assert.Contains(adUnit2.id, newPlacements[0].targetedAdUnitIds);
    }
  }
}
