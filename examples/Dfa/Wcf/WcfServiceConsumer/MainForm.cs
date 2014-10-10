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

using System;
using System.Text;
using System.Windows.Forms;

using Google.Api.Ads.Dfa.Examples.Wcf.WcfServiceReference;

namespace Google.Api.Ads.Dfa.Examples.Wcf {
  /// <summary>
  /// Main form for this application.
  /// </summary>
  public partial class MainForm : Form {
    /// <summary>
    /// Initializes a new instance of the <see cref="MainForm"/> class.
    /// </summary>
    public MainForm() {
      InitializeComponent();
    }

    /// <summary>
    /// Handles the Click event of the btnGetAdTypes control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/>
    /// instance containing the event data.</param>
    private void btnGetAdTypes_Click(object sender, EventArgs e) {
      IWcfService service = new WcfServiceClient();
      AdType[] adTypes = service.GetAdTypes();

      StringBuilder builder = new StringBuilder();
      foreach (AdType adType in adTypes) {
        builder.AppendFormat("Ad type name is '{0}' and id is {1}.\n", adType.nameField,
          adType.idField);
      }
      MessageBox.Show(builder.ToString());
    }
  }
}
