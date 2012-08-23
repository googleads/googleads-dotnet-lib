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

using OAuth.Net.Common;

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Google.Api.Ads.Dfa.Examples.CSharp.OAuth {
  /// <summary>
  /// Login and callback page for handling OAuth1.0a authentication.
  /// </summary>
  public partial class OAuthLogin : Page {
    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing
    /// the event data.</param>
    protected void Page_Load(object sender, EventArgs e) {
      // Create an DfaAppConfig object with the default settings in
      // App.config.
      DfaAppConfig config = new DfaAppConfig();
      if (config.AuthorizationMethod == DfaAuthorizationMethod.OAuth2) {
        DoAuth2Configuration(config);
      } else {
      }
    }

    private void DoAuth2Configuration(DfaAppConfig config) {
      // You could specify scope in your App.config, but if you care only about
      // DFA, the following is a simpler way of setting the scope.
      config.OAuth2Scope = DfaService.GetOAuthScope(config);

      // Since we use this page for OAuth callback also, we set the callback
      // url as the current page. For a non-web application, this will be null.
      config.OAuth2RedirectUri = Request.Url.GetLeftPart(UriPartial.Path);

      // Create an OAuth2 object for handling OAuth2 flow.
      OAuth2Provider oAuth = new OAuth2Provider(config);

      if (Request.Params["state"] == null) {
        // This is the first time this page is being loaded.
        // Set the state variable to any value that helps you recognize
        // when this url will be called by the OAuth2 server.
        oAuth.State = "callback";

        // Create an authorization url and redirect the user to that page.
        Response.Redirect(oAuth.GetAuthorizationUrl());
      } else if (Request.Params["state"] == "callback") {
        // This page was loaded because OAuth server did a callback.
        // Retrieve the authorization code from the url and use it to fetch
        // the access token. This call will also fetch the refresh token if
        // your mode is offline.
        oAuth.FetchAccessAndRefreshTokens(Request.Params["code"]);

        // Save the access and refresh tokens for future use.
        Session["AccessToken"] = oAuth.AccessToken;
        Session["RefreshToken"] = oAuth.RefreshToken;

        // Redirect the user to the main page.
        Response.Redirect("Default.aspx");
      } else {
        throw new Exception("Unknown state for OAuth callback.");
      }
    }
  }
}
