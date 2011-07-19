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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201101;
using Google.Api.Ads.Common.OAuth.Lib;

using System;
using System.Data;
using System.Web.UI.WebControls;

namespace Google.Api.Ads.AdWords.Examples.CSharp.OAuth {
  /// <summary>
  /// Code-behind class for GetAllCampaigns.aspx.
  /// </summary>
  public partial class GetAllCampaigns : System.Web.UI.Page {
    /// <summary>
    /// The user for creating services and making AdWords API calls.
    /// </summary>
    AdWordsUser user;

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing
    /// the event data.</param>
    void Page_Load(object sender, EventArgs e) {
      user = new AdWordsUser();
      string url = Request.Url.GetLeftPart(UriPartial.Path);
      user.OAuthProvider = new AdsOAuthNetProvider(AdWordsService.GetOAuthScope(
          user.Config as AdWordsAppConfig), url);
    }

    /// <summary>
    /// Handles the Click event of the btnAuthorize control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing
    /// the event data.</param>
    protected void OnAuthorizeButtonClick(object sender, EventArgs e) {
      user.OAuthProvider.GenerateAccessToken();
    }

    /// <summary>
    /// Handles the Click event of the btnGetCampaigns control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing
    /// the event data.</param>
    protected void OnGetCampaignsButtonClick(object sender, EventArgs e) {
      CampaignService service =
          (CampaignService) user.GetService(AdWordsService.v201101.CampaignService);

      // Create a selector.
      Selector selector = new Selector();
      selector.fields = new string[] {"Id", "Name", "Status"};

      OrderBy orderByName = new OrderBy();
      orderByName.field = "Name";
      orderByName.sortOrder = SortOrder.ASCENDING;

      selector.ordering = new OrderBy[] {orderByName};
      CampaignPage page = service.get(selector);

      // Display campaigns.
      if (page != null && page.entries != null && page.entries.Length > 0) {
        DataTable dataTable = new DataTable();
        dataTable.Columns.AddRange(new DataColumn[] {
            new DataColumn("Serial No.", typeof(int)),
            new DataColumn("Campaign Id", typeof(long)),
            new DataColumn("Campaign Name", typeof(string)),
            new DataColumn("Status", typeof(string))
        });
        for (int i = 0; i < page.entries.Length; i++) {
          Campaign campaign = page.entries[i];
          DataRow dataRow = dataTable.NewRow();
          dataRow.ItemArray = new object[] {i + 1, campaign.id, campaign.name,
              campaign.status.ToString()};
          dataTable.Rows.Add(dataRow);
        }
        CampaignGrid.DataSource = dataTable;
        CampaignGrid.DataBind();
      } else {
        Response.Write("No campaigns were found.");
      }
    }


    /// <summary>
    /// Handles the Click event of the btnLogout control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing
    /// the event data.</param>
    protected void OnLogoutButtonClick(object sender, EventArgs e) {
      Session.Clear();
    }

    /// <summary>
    /// Handles the RowDataBound event of the CampaignGrid control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The
    /// <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance
    /// containing the event data.</param>
    protected void CampaignGrid_RowDataBound(object sender, GridViewRowEventArgs e) {
      if (e.Row.DataItemIndex >= 0) {
        e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
        e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
        e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
        e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
      }
    }
  }
}
