// Copyright 2016, Google Inc. All Rights Reserved.
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
using System.Windows.Forms;

namespace Google.Api.Ads.Common.Utilities.OAuthTokenGenerator {

  /// <summary>
  /// A dialog to show the OAuth2 results.
  /// </summary>
  public partial class ResultDialog : Form {

    public ResultDialog(string configText) {
      InitializeComponent();
      txtConfig.Text = configText.Replace("\n", "\r\n");
    }

    /// <summary>
    /// Handles the Click event of the btnOk control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the
    /// event data.</param>
    private void btnOk_Click(object sender, EventArgs e) {
      this.Close();
    }
  }
}
