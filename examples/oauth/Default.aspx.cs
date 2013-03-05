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
using Google.Api.Ads.Dfp.v201208;
using Google.Api.Ads.Common.OAuth.Lib;

using System;
using System.Data;
using System.Web.UI.WebControls;
using System.IO;

namespace Google.Api.Ads.Dfp.Examples.OAuth {
  /// <summary>
  /// Code-behind class for Default.aspx.
  /// </summary>
  public partial class Default : System.Web.UI.Page {
    /// <summary>
    /// The user for creating services and making DFP API calls.
    /// </summary>
    DfpUser user;

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing
    /// the event data.</param>
    void Page_Load(object sender, EventArgs e) {
      user = new DfpUser();
    }

    /// <summary>
    /// Handles the Click event of the btnAuthorize control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing
    /// the event data.</param>
    protected void OnAuthorizeButtonClick(object sender, EventArgs e) {
      // This code example assumes that you don't have a previously authorized
      // access token or refresh token with you. If you have one, you needn't
      // redirect the user through the login page; instead you can initialize
      // the user directly as follows:
      // OAuth 2
      // ==========
      //
      // OAuth2Provider oAuth2 = new OAuth2Provider(user.Config);
      // oAuth2.AccessToken = myAccessToken;
      // oAuth2.RefreshToken = myRefreshToken;
      DfpAppConfig config = user.Config as DfpAppConfig;
      if (config.AuthorizationMethod == DfpAuthorizationMethod.OAuth2) {
        Response.Redirect("OAuthLogin.aspx");
      } else {
        throw new Exception("Authorization mode is not OAuth.");
      }
    }

    /// <summary>
    /// Handles the Click event of the btnGetUsers control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing
    /// the event data.</param>
    protected void OnGetUsersButtonClick(object sender, EventArgs e) {
      PrepareUserForOAuth();

      try {
        // Get the UserService.
        UserService userService = (UserService)user.GetService(DfpService.v201208.UserService);

        // Sets defaults for page and Statement.
        UserPage page = new UserPage();
        Statement statement = new Statement();
        int offset = 0;

        DataTable dataTable = new DataTable();
        dataTable.Columns.AddRange(new DataColumn[] {
            new DataColumn("Serial No.", typeof(int)),
            new DataColumn("User Id", typeof(long)),
            new DataColumn("Email", typeof(string)),
            new DataColumn("Role", typeof(string))
        });
        do {
          // Create a Statement to get all users.
          statement.query = string.Format("LIMIT 500 OFFSET {0}", offset);

          // Get users by Statement.
          page = userService.getUsersByStatement(statement);

          if (page.results != null && page.results.Length > 0) {
            int i = page.startIndex;
            foreach (User usr in page.results) {
              DataRow dataRow = dataTable.NewRow();
              dataRow.ItemArray = new object[] {i + 1, usr.id, usr.email, usr.roleName};
              dataTable.Rows.Add(dataRow);
              i++;
            }
          }
          offset += 500;
        } while (offset < page.totalResultSetSize);
        if (dataTable.Rows.Count > 0) {
          UserGrid.DataSource = dataTable;
          UserGrid.DataBind();
        } else {
          Response.Write("No users were found.");
        }
      } catch (Exception ex) {
        Response.Write(string.Format("Failed to get users. Exception says \"{0}\"",
            ex.Message));
      }
    }

    /// <summary>
    /// Prepares the user for OAuth.
    /// </summary>
    private void PrepareUserForOAuth() {
     if ((user.Config as DfpAppConfig).AuthorizationMethod ==
          DfpAuthorizationMethod.OAuth2) {
        // Create an OAuth2 handler.
        OAuth2Provider oAuth2 = new OAuth2Provider(user.Config);

        // We saved these values to our session in OAuth2Login.aspx, but you could
        // load these values from your local database instead.
        oAuth2.AccessToken = Session["AccessToken"] as string;
        oAuth2.RefreshToken = Session["RefreshToken"] as string;

        // Set the OAuth2 handler for the current user.
        user.OAuthProvider = oAuth2;
      } else {
        throw new Exception("Authorization mode is not OAuth.");
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
    /// Handles the RowDataBound event of the UserGrid control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The
    /// <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance
    /// containing the event data.</param>
    protected void UserGrid_RowDataBound(object sender, GridViewRowEventArgs e) {
      if (e.Row.DataItemIndex >= 0) {
        e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
        e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
        e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
        e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
      }
    }
  }
}
