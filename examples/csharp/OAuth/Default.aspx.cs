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

using Google.Api.Ads.Common.OAuth.Lib;
using Google.Api.Ads.Dfa.Lib;
using Google.Api.Ads.Dfa.v1_19;

using System;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;

namespace Google.Api.Ads.Dfa.Examples.CSharp.OAuth {
  /// <summary>
  /// Code-behind class for Default.aspx.
  /// </summary>
  public partial class Default : System.Web.UI.Page {
    /// <summary>
    /// The user for creating services and making DFA API calls.
    /// </summary>
    DfaUser user;

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing
    /// the event data.</param>
    void Page_Load(object sender, EventArgs e) {
      user = new DfaUser();
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
      //
      // OAuth 2
      // ==========
      //
      // OAuth2Provider oAuth2 = new OAuth2Provider(user.Config);
      // oAuth2.AccessToken = myAccessToken;
      // oAuth2.RefreshToken = myRefreshToken;
      DfaAppConfig config = user.Config as DfaAppConfig;
      if (config.AuthorizationMethod == DfaAuthorizationMethod.OAuth2) {
        Response.Redirect("OAuthLogin.aspx");
      } else {
        throw new Exception("Authorization mode is not OAuth2.");
      }
    }

    /// <summary>
    /// Handles the Click event of the btnAdTypes control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing
    /// the event data.</param>
    protected void OnGetAdTypesButtonClick(object sender, EventArgs e) {
      ConfigureUserForOAuth();

      try {
        // Set the username. This is required for the LoginService to work
        // correctly when authenticating using OAuth2.
        (user.Config as DfaAppConfig).UserName = txtUserName.Text;

        // Create AdRemoteService instance.
        AdRemoteService service = (AdRemoteService) user.GetService(
            DfaService.v1_19.AdRemoteService);

        // Get ad types.
        AdType[] adTypes = service.getAdTypes();

        // Display ad type and its id.
        foreach (AdType result in adTypes) {
          Console.WriteLine("Ad type with name \"{0}\" and id \"{1}\" was found.", result.name,
              result.id);
        }

        // Display ad types.
        if (adTypes != null && adTypes.Length > 0) {
          DataTable dataTable = new DataTable();
          dataTable.Columns.AddRange(new DataColumn[] {
              new DataColumn("Serial No.", typeof(int)),
              new DataColumn("Id", typeof(long)),
              new DataColumn("Ad Type", typeof(string)),
          });
          for (int i = 0; i < adTypes.Length; i++) {
            AdType result = adTypes[i];
            DataRow dataRow = dataTable.NewRow();
            dataRow.ItemArray = new object[] {i + 1, result.id, result.name};
            dataTable.Rows.Add(dataRow);
          }

          AdTypeGrid.DataSource = dataTable;
          AdTypeGrid.DataBind();
        } else {
          Response.Write("No ad types were found.");
        }
      } catch (Exception ex) {
        Response.Write(string.Format("Failed to get ad types. Exception says \"{0}\"",
            ex.Message));
      }
    }

    /// <summary>
    /// Configures the DFA user for OAuth.
    /// </summary>
    private void ConfigureUserForOAuth() {
      if ((user.Config as DfaAppConfig).AuthorizationMethod ==
          DfaAuthorizationMethod.OAuth2) {
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
    /// Handles the RowDataBound event of the AdTypeGrid control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The
    /// <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance
    /// containing the event data.</param>
    protected void AdTypeGrid_RowDataBound(object sender, GridViewRowEventArgs e) {
      if (e.Row.DataItemIndex >= 0) {
        e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
        e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
        e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
      }
    }
  }
}
