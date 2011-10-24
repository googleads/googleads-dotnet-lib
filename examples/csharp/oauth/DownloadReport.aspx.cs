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
using Google.Api.Ads.AdWords.v201109;
using Google.Api.Ads.Common.OAuth.Lib;

using System;
using System.Data;
using System.Web.UI.WebControls;
using Google.Api.Ads.AdWords.Util.Reports;

namespace Google.Api.Ads.AdWords.Examples.CSharp.OAuth {
  /// <summary>
  /// Code-behind class for DownloadReport.aspx.
  /// </summary>
  public partial class DownloadReport : System.Web.UI.Page {
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
    /// Handles the Click event of the btnDownloadReport control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing
    /// the event data.</param>
    protected void OnDownloadReportButtonClick(object sender, EventArgs e) {
      ReportDefinition definition = new ReportDefinition();

      definition.reportName = "Custom ADGROUP_PERFORMANCE_REPORT";
      definition.reportType = ReportDefinitionReportType.ADGROUP_PERFORMANCE_REPORT;
      definition.downloadFormat = DownloadFormat.CSV;
      definition.dateRangeType = ReportDefinitionDateRangeType.LAST_7_DAYS;

      Selector selector = new Selector();
      selector.fields = new string[] {"CampaignId", "Id", "Impressions", "Clicks", "Cost"};

      Predicate predicate = new Predicate();
      predicate.field = "Status";
      predicate.@operator = PredicateOperator.IN;
      predicate.values = new string[] {"ENABLED", "PAUSED"};
      selector.predicates = new Predicate[] {predicate};

      definition.selector = selector;
      definition.includeZeroImpressions = true;

      (user.Config as AdWordsAppConfig).ClientCustomerId = txtCustomerId.Text;
      try {
        ClientReport report = new ReportUtilities(user).GetClientReport(definition);
        Response.AddHeader("Content-Disposition", "attachment;filename=\"report.csv\"");
        Response.BinaryWrite(report.Contents);
      } catch (Exception ex) {
        Response.Write(string.Format("Failed to download report. Exception says \"{0}\"",
            ex.Message));
      }
      Response.End();
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
  }
}
