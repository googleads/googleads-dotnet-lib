// Copyright 2013, Google Inc. All Rights Reserved.
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

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Google.Api.Ads.Common.Utilities.OAuthTokenGenerator {

  /// <summary>
  /// Main application form.
  /// </summary>
  public partial class MainForm : Form {

    /// <summary>
    /// The dictionary to maintain a map between the API name, and the
    /// corrsponding OAuth2 scope.
    /// </summary>
    private Dictionary<String, String> scopeMap = new Dictionary<String, String>();

    /// <summary>
    /// The Application configuration patch to be displayed to the user.
    /// </summary>
    private const string APP_CONFIG_PATCH = @"
<add key='AuthorizationMethod' value='OAuth2' />
<add key='OAuth2ClientId' value='{0}' />
<add key='OAuth2ClientSecret' value='{1}' />
<add key='OAuth2RefreshToken' value='{2}' />
";

    /// <summary>
    /// The web server that handles the OAuth2 callback.
    /// </summary>
    private readonly LocalWebServer webServer;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainForm"/> class.
    /// </summary>
    public MainForm() {
      InitializeComponent();

      webServer = new LocalWebServer();
      webServer.Start();
      webServer.OnSuccess += delegate(string clientId, string clientSecret, string refreshToken) {
        // Use the Invoke method so that the caller thread doesn't block, and
        // control switches to the UI thread.
        this.Invoke(new MethodInvoker(delegate() {
          string configText = string.Format(APP_CONFIG_PATCH, clientId, clientSecret, refreshToken);
          ResultDialog dialog = new ResultDialog(configText);
          dialog.Owner = this;
          dialog.Show();
        }));
      };

      webServer.OnFailed += delegate(string errorMessage) {
        // Use the Invoke method so that the caller thread doesn't block, and
        // control switches to the UI thread.
        this.Invoke(new MethodInvoker(delegate() {
          MessageBox.Show(this, errorMessage + "\r\n Fix the errors and try again.",
              this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }));
      };

      scopeMap.Add("AdWords API", "https://www.googleapis.com/auth/adwords");
      scopeMap.Add("Doubleclick for Publishers API", "https://www.googleapis.com/auth/dfp");

      foreach (string key in scopeMap.Keys) {
        chkScopes.Items.Add(key);
      }
    }

    /// <summary>
    /// Handles the Click event of the btnOk control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the
    /// event data.</param>
    private void btnOk_Click(object sender, EventArgs e) {
      string clientId = txtClientID.Text.Trim();
      if (string.IsNullOrEmpty(clientId)) {
        MessageBox.Show(this, "Client ID cannot be empty.", this.Text, MessageBoxButtons.OK,
            MessageBoxIcon.Information);
        return;
      }

      string clientSecret = txtClientSecret.Text.Trim();
      if (string.IsNullOrEmpty(clientSecret)) {
        MessageBox.Show(this, "Client Secret cannot be empty.", this.Text,
            MessageBoxButtons.OK, MessageBoxIcon.Information);
        return;
      }

      List<string> allScopes = new List<string>();
      for (int i = 0; i < chkScopes.SelectedItems.Count; i++) {
        allScopes.Add(scopeMap[chkScopes.SelectedItems[i].ToString()]);
      }

      string[] additionalScopes = txtExtraScopes.Text.Split(new char[] { '\r', '\n' },
          StringSplitOptions.RemoveEmptyEntries);

      foreach (string additionalScope in additionalScopes) {
        if (!string.IsNullOrWhiteSpace(additionalScope)) {
          allScopes.Add(additionalScope);
        }
      }

      if (allScopes.Count == 0) {
        MessageBox.Show(this, "You should select at least one scope.", this.Text,
            MessageBoxButtons.OK, MessageBoxIcon.Information);
        return;
      }

      webServer.TriggerAuthFlow(clientId, clientSecret, string.Join(" ", allScopes));
    }

    /// <summary>
    /// Handles the Click event of the btnCancel control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the
    /// event data.</param>
    private void btnCancel_Click(object sender, EventArgs e) {
      this.Close();
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Form.Closing" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" />
    /// that contains the event data.</param>
    protected override void OnClosing(System.ComponentModel.CancelEventArgs e) {
      webServer.Stop();
      base.OnClosing(e);
    }
  }
}
