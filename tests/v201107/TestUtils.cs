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
using Google.Api.Ads.Dfp.Util.v201107;
using Google.Api.Ads.Dfp.v201107;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Configuration;
using System.Collections;

namespace Google.Api.Ads.Dfp.Tests.v201107 {
  /// <summary>
  /// A utility class to assist the testing of v201004 services.
  /// </summary>
  public class TestUtils {
    public User GetCurrentUser(DfpUser user) {
      return GetUserByEmail(user, new DfpAppConfig().Email);
    }

    public User GetTrafficker(DfpUser user) {
      return GetUserByEmail(user, "dfp.api.trafficker@gmail.com");
    }

    public User GetSalesperson(DfpUser user) {
      return GetUserByEmail(user, "dfp.api.salesperson@gmail.com");
    }

    public User GetUserByEmail(DfpUser user, string email) {
      UserService userService = (UserService) user.GetService(DfpService.v201107.UserService);

      // Create a Statement to get all users sorted by name.
      Statement statement = new Statement();
      statement.query = string.Format("where email = '{0}' LIMIT 1", email);

      UserPage page = userService.getUsersByStatement(statement);

      if (page.results != null && page.results.Length > 0) {
        return page.results[0];
      } else {
        return null;
      }
    }

    public Role GetRole(DfpUser user, string roleName) {
      UserService userService = (UserService)user.GetService(DfpService.v201107.UserService);

      // Get all roles.
      Role[] roles = userService.getAllRoles();
      foreach (Role role in roles) {
        if (role.name == roleName) {
          return role;
        }
      }
      return null;
    }

    /// <summary>
    /// Create a test company for running further tests.
    /// </summary>
    /// <returns>A test company for running further tests.</returns>
    public Company CreateCompany(DfpUser user, CompanyType companyType) {
      CompanyService companyService = (CompanyService) user.GetService(
          DfpService.v201107.CompanyService);
      Company company = new Company();
      company.name = string.Format("Company #{0}", GetTimeStamp());
      company.type = companyType;

      return companyService.createCompany(company);
    }

    public Order CreateOrder(DfpUser user, long advertiserId, long salespersonId,
        long traffickerId) {
      // Get the OrderService.
      OrderService orderService = (OrderService) user.GetService(DfpService.v201107.OrderService);

      Order order = new Order();
      order.name = string.Format("Order #{0}", GetTimeStamp());
      order.advertiserId = advertiserId;
      order.salespersonId = salespersonId;
      order.traffickerId = traffickerId;

      return orderService.createOrder(order);
    }

    public LineItem CreateLineItem(DfpUser user, long orderId, string adUnitId) {
      LineItemService lineItemService =
          (LineItemService) user.GetService(DfpService.v201107.LineItemService);

      string[] targetAdUnitIds = new string[] {adUnitId};

      LineItem lineItem = new LineItem();
      lineItem.name = string.Format("Line item #{0}", GetTimeStamp());
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

      return lineItemService.createLineItem(lineItem);
    }

    /// <summary>
    /// Create a test company for running further tests.
    /// </summary>
    /// <returns>A creative for running further tests.</returns>
    public Creative CreateCreative(DfpUser user, long advertiserId) {
      CreativeService creativeService = (CreativeService)user.GetService(
          DfpService.v201107.CreativeService);
      ImageCreative imageCreative = new ImageCreative();
      imageCreative.name = string.Format("Image creative #{0}", GetTimeStamp());
      imageCreative.advertiserId = advertiserId;
      imageCreative.destinationUrl = "http://www.google.com";
      imageCreative.imageName = "image.jpg";
      imageCreative.imageByteArray = MediaUtilities.GetAssetDataFromUrl(
          "http://www.google.com/intl/en/adwords/select/images/samples/inline.jpg");

      Size imageSize = new Size();
      imageSize.width = 300;
      imageSize.height = 250;

      imageCreative.size = imageSize;

      return creativeService.createCreative(imageCreative);
    }

    public LineItemCreativeAssociation CreateLica(DfpUser user, long lineItemId, long creativeId) {
      LineItemCreativeAssociationService licaService =
          (LineItemCreativeAssociationService)user.GetService(
              DfpService.v201107.LineItemCreativeAssociationService);

      LineItemCreativeAssociation lica = new LineItemCreativeAssociation();
      lica.creativeId = creativeId;
      lica.lineItemId = lineItemId;

      return licaService.createLineItemCreativeAssociation(lica);
    }

    public AdUnit CreateAdUnit(DfpUser user) {
      InventoryService inventoryService =
          (InventoryService) user.GetService(DfpService.v201107.InventoryService);

      AdUnit adUnit = new AdUnit();
      adUnit.name = string.Format("Ad_Unit_{0}", GetTimeStamp());
      adUnit.parentId = FindRootAdUnit(user).id;

      // Set the size of possible creatives that can match this ad unit.
      Size size = new Size();
      size.width = 300;
      size.height = 250;

      adUnit.sizes = new Size[] {size};
      return inventoryService.createAdUnit(adUnit);
    }

    public AdUnit FindRootAdUnit(DfpUser user) {
      // Get InventoryService.
      InventoryService inventoryService =
          (InventoryService)user.GetService(DfpService.v201107.InventoryService);

      // Create a Statement to only select the root ad unit.
      Statement statement = new Statement();
      statement.query = "WHERE parentId IS NULL LIMIT 500";

      // Get ad units by Statement.
      AdUnitPage page = inventoryService.getAdUnitsByStatement(statement);
      return page.results[0];
    }

    public Placement CreatePlacement(DfpUser user, string[] targetedAdUnitIds) {
      // Get InventoryService.
      PlacementService placementService =
          (PlacementService) user.GetService(DfpService.v201107.PlacementService);

      Placement placement = new Placement();
      placement.name = string.Format("Test placement #{0}", this.GetTimeStamp());
      placement.description = "Test placement";
      placement.targetedAdUnitIds = targetedAdUnitIds;

      return placementService.createPlacement(placement);
    }

    public ReportJob CreateReport(DfpUser user) {
      // Get ReportService.
      ReportService reportService =
          (ReportService) user.GetService(DfpService.v201107.ReportService);

      ReportJob reportJob = new ReportJob();
      reportJob.reportQuery = new ReportQuery();
      reportJob.reportQuery.dimensions = new Dimension[] {Dimension.ORDER};
      reportJob.reportQuery.columns = new Column[] {Column.AD_SERVER_IMPRESSIONS,
          Column.AD_SERVER_CLICKS, Column.AD_SERVER_CTR, Column.AD_SERVER_REVENUE,
          Column.AD_SERVER_AVERAGE_ECPM};
      reportJob.reportQuery.dateRangeType = DateRangeType.LAST_MONTH;

      ReportJob newReportJob = null;

      return reportService.runReportJob(reportJob);
    }

    /// <summary>
    /// Gets the current timestamp as a string.
    /// </summary>
    /// <returns>The current timestamp as a string.</returns>
    public string GetTimeStamp() {
      Thread.Sleep(500);
      return (System.DateTime.UtcNow - new System.DateTime(1970, 1, 1)).Ticks.
          ToString();
    }
  }
}
