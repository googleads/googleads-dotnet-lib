// Copyright 2014, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.Common.Lib;
using Google.Api.Ads.Common.Util.Reports;
using Google.Api.Ads.AdManager.Lib;
using Google.Api.Ads.AdManager.Util.v201902;
using Google.Api.Ads.AdManager.v201902;

using System;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;

namespace Google.Api.Ads.AdManager.Examples.CSharp.OAuth
{
    /// <summary>
    /// Code-behind class for Default.aspx.
    /// </summary>
    public partial class Default : System.Web.UI.Page
    {
        /// <summary>
        /// The user for creating services and making DFP API calls.
        /// </summary>
        AdManagerUser user;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing
        /// the event data.</param>
        void Page_Load(object sender, EventArgs e)
        {
            user = new AdManagerUser();
        }

        /// <summary>
        /// Handles the Click event of the btnAuthorize control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing
        /// the event data.</param>
        protected void OnAuthorizeButtonClick(object sender, EventArgs e)
        {
            // This code example shows how to run an DFP API web application
            // while incorporating the OAuth2 web application flow into your
            // application. If your application uses a single Google login to make calls
            // to all your accounts, you shouldn't use this code example. Instead, you
            // should run Common\Util\OAuth2TokenGenerator.cs to generate a refresh
            // token and set that in user.Config.OAuth2RefreshToken field, or set
            // OAuth2RefreshToken key in your App.config / Web.config.
            AdManagerAppConfig config = user.Config as AdManagerAppConfig;
            if (config.AuthorizationMethod == AdManagerAuthorizationMethod.OAuth2)
            {
                if (user.Config.OAuth2Mode == OAuth2Flow.APPLICATION &&
                    string.IsNullOrEmpty(config.OAuth2RefreshToken))
                {
                    Response.Redirect("OAuthLogin.aspx");
                }
            }
            else
            {
                throw new Exception("Authorization mode is not OAuth.");
            }
        }

        /// <summary>
        /// Handles the Click event of the btnGetUsers control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">The <see cref="System.EventArgs"/> instance containing
        /// the event data.</param>
        protected void OnGetUsersButtonClick(object sender, EventArgs eventArgs)
        {
            ConfigureUserForOAuth();

            try
            {
                // Get the UserService.
                UserService userService = user.GetService<UserService>();

                // Create a Statement to get all users.
                StatementBuilder statementBuilder = new StatementBuilder().OrderBy("id ASC")
                    .Limit(StatementBuilder.SUGGESTED_PAGE_LIMIT);

                // Set default for page.
                UserPage page = new UserPage();

                DataTable dataTable = new DataTable();
                dataTable.Columns.AddRange(new DataColumn[]
                {
                    new DataColumn("Serial No.", typeof(int)),
                    new DataColumn("User Id", typeof(long)),
                    new DataColumn("Email", typeof(string)),
                    new DataColumn("Role", typeof(string))
                });
                do
                {
                    // Get users by Statement.
                    page = userService.getUsersByStatement(statementBuilder.ToStatement());

                    if (page.results != null && page.results.Length > 0)
                    {
                        int i = page.startIndex;
                        foreach (User usr in page.results)
                        {
                            DataRow dataRow = dataTable.NewRow();
                            dataRow.ItemArray = new object[]
                            {
                                i + 1,
                                usr.id,
                                usr.email,
                                usr.roleName
                            };
                            dataTable.Rows.Add(dataRow);
                            i++;
                        }
                    }

                    statementBuilder.IncreaseOffsetBy(StatementBuilder.SUGGESTED_PAGE_LIMIT);
                } while (statementBuilder.GetOffset() < page.totalResultSetSize);

                if (dataTable.Rows.Count > 0)
                {
                    UserGrid.DataSource = dataTable;
                    UserGrid.DataBind();
                }
                else
                {
                    Response.Write("No users were found.");
                }
            }
            catch (Exception e)
            {
                Response.Write(string.Format("Failed to get users. Exception says \"{0}\"",
                    e.Message));
            }
        }

        /// <summary>
        /// Handles the Click event of the btnDownloadReport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">The <see cref="System.EventArgs"/> instance containing
        /// the event data.</param>
        protected void OnDownloadReportButtonClick(object sender, EventArgs eventArgs)
        {
            ConfigureUserForOAuth();
            ReportService reportService = user.GetService<ReportService>();

            ReportQuery reportQuery = new ReportQuery();
            reportQuery.dimensions = new Dimension[]
            {
                Dimension.AD_UNIT_ID,
                Dimension.AD_UNIT_NAME
            };
            reportQuery.columns = new Column[]
            {
                Column.AD_SERVER_IMPRESSIONS,
                Column.AD_SERVER_CLICKS,
                Column.ADSENSE_LINE_ITEM_LEVEL_IMPRESSIONS,
                Column.ADSENSE_LINE_ITEM_LEVEL_CLICKS,
                Column.TOTAL_LINE_ITEM_LEVEL_IMPRESSIONS,
                Column.TOTAL_LINE_ITEM_LEVEL_CPM_AND_CPC_REVENUE
            };

            reportQuery.adUnitView = ReportQueryAdUnitView.HIERARCHICAL;
            reportQuery.dateRangeType = DateRangeType.YESTERDAY;

            // Create report job.
            ReportJob reportJob = new ReportJob();
            reportJob.reportQuery = reportQuery;

            string filePath = Path.GetTempFileName();

            try
            {
                // Run report.
                reportJob = reportService.runReportJob(reportJob);

                ReportUtilities reportUtilities = new ReportUtilities(reportService, reportJob.id);

                // Set download options.
                ReportDownloadOptions options = new ReportDownloadOptions();
                options.exportFormat = ExportFormat.CSV_DUMP;
                options.useGzipCompression = true;
                reportUtilities.reportDownloadOptions = options;

                // Download the report.
                using (ReportResponse reportResponse = reportUtilities.GetResponse())
                {
                    reportResponse.Save(filePath);
                }
            }
            catch (Exception e)
            {
                throw new System.ApplicationException("Failed to download report.", e);
            }

            Response.AddHeader("content-disposition", "attachment;filename=report.csv.gzip");
            Response.WriteFile(filePath);
            Response.End();
        }

        /// <summary>
        /// Configures the DFP user for OAuth.
        /// </summary>
        private void ConfigureUserForOAuth()
        {
            AdManagerAppConfig config = (user.Config as AdManagerAppConfig);
            if (config.AuthorizationMethod == AdManagerAuthorizationMethod.OAuth2)
            {
                if (config.OAuth2Mode == OAuth2Flow.APPLICATION &&
                    string.IsNullOrEmpty(config.OAuth2RefreshToken))
                {
                    user.OAuthProvider = (OAuth2ProviderForApplications) Session["OAuthProvider"];
                }
            }
            else
            {
                throw new Exception("Authorization mode is not OAuth.");
            }
        }

        /// <summary>
        /// Handles the Click event of the btnLogout control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing
        /// the event data.</param>
        protected void OnLogoutButtonClick(object sender, EventArgs e)
        {
            Session.Clear();
        }

        /// <summary>
        /// Handles the RowDataBound event of the UserGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The
        /// <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance
        /// containing the event data.</param>
        protected void UserGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItemIndex >= 0)
            {
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
            }
        }
    }
}
