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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.Common.Lib;

using System;
using System.Web.UI;

namespace Google.Api.Ads.AdWords.Examples.CSharp.OAuth
{
    /// <summary>
    /// Login and callback page for handling OAuth2 authentication.
    /// </summary>
    public partial class OAuthLogin : Page
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing
        /// the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Create an AdWordsAppConfig object with the default settings in
            // App.config.
            AdWordsAppConfig config = new AdWordsAppConfig();
            if (config.OAuth2Mode == OAuth2Flow.APPLICATION &&
                string.IsNullOrEmpty(config.OAuth2RefreshToken))
            {
                DoAuth2Configuration(config);
            }
        }

        private void DoAuth2Configuration(AdWordsAppConfig config)
        {
            // Since we use this page for OAuth callback also, we set the callback
            // url as the current page. For a non-web application, this will be null.
            config.OAuth2RedirectUri = Request.Url.GetLeftPart(UriPartial.Path);

            // Create an OAuth2 object for handling OAuth2 flow.
            OAuth2ProviderForApplications oAuth = new OAuth2ProviderForApplications(config);

            if (Request.Params["state"] == null)
            {
                // This is the first time this page is being loaded.
                // Set the state variable to any value that helps you recognize
                // when this url will be called by the OAuth2 server.
                oAuth.State = "callback";

                // Create an authorization url and redirect the user to that page.
                Response.Redirect(oAuth.GetAuthorizationUrl());
            }
            else if (Request.Params["state"] == "callback")
            {
                // This page was loaded because OAuth server did a callback.
                // Retrieve the authorization code from the url and use it to fetch
                // the access token. This call will also fetch the refresh token if
                // your mode is offline.
                oAuth.FetchAccessAndRefreshTokens(Request.Params["code"]);

                // Save the OAuth2 provider for future use. If you wish to save only
                // the values and restore the object later, then save
                // oAuth.RefreshToken, oAuth.AccessToken, oAuth.UpdatedOn and
                // oAuth.ExpiresIn.
                //
                // You can later restore the values as
                // AdWordsUser user = new AdWordsUser();
                // user.Config.OAuth2Mode = OAuth2Flow.APPLICATION;
                // OAuth2ProviderForApplications oAuth =
                //     (user.OAuthProvider as OAuth2ProviderForApplications);
                // oAuth.RefreshToken = xxx;
                // oAuth.AccessToken = xxx;
                // oAuth.UpdatedOn = xxx;
                // oAuth.ExpiresIn = xxx;
                //
                // Note that only oAuth.RefreshToken is mandatory. If you leave
                // oAuth.AccessToken as empty, or if oAuth.UpdatedOn + oAuth.ExpiresIn
                // is in the past, the access token will be refreshed by the library.
                // You can listen to this event as
                //
                // oAuth.OnOAuthTokensObtained += delegate(AdsOAuthProvider provider) {
                //    OAuth2ProviderForApplications oAuth =
                //        (provider as OAuth2ProviderForApplications);
                //    // Save oAuth.RefreshToken, oAuth.AccessToken, oAuth.UpdatedOn and
                //    // oAuth.ExpiresIn.
                //};
                Session["OAuthProvider"] = oAuth;
                // Redirect the user to the main page.
                Response.Redirect("Default.aspx");
            }
            else
            {
                throw new Exception("Unknown state for OAuth callback.");
            }
        }
    }
}
