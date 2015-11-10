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
using System.Text;
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
    /// Initializes a new instance of the <see cref="MainForm"/> class.
    /// </summary>
    public MainForm() {
      InitializeComponent();

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
        MessageBox.Show("Client ID cannot be empty.");
        return;
      }

      string clientSecret = txtClientSecret.Text.Trim();
      if (string.IsNullOrEmpty(clientSecret)) {
        MessageBox.Show("Client Secret cannot be empty.");
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
        MessageBox.Show("You should select at least one scope.");
        return;
      }

      LoginForm loginForm = new LoginForm(clientId, clientSecret, string.Join(" ", allScopes));
      loginForm.ShowDialog();
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
  }
}
