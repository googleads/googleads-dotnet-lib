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
using Google.Api.Ads.AdWords.Util.Reports;
using Google.Api.Ads.AdWords.v201809;
using Google.Api.Ads.Common.Lib;
using Google.Api.Ads.Common.Util.Reports;

using System;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;

namespace Google.Api.Ads.AdWords.Examples.CSharp.OAuth
{
    /// <summary>
    /// Code-behind class for Default.aspx.
    /// </summary>
    public partial class Default : System.Web.UI.Page
    {
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
        void Page_Load(object sender, EventArgs e)
        {
            user = new AdWordsUser();
        }

        /// <summary>
        /// Handles the Click event of the btnAuthorize control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing
        /// the event data.</param>
        protected void OnAuthorizeButtonClick(object sender, EventArgs e)
        {
            // This code example shows how to run an AdWords API web application
            // while incorporating the OAuth2 web application flow into your
            // application. If your application uses a single AdWords manager account
            // login to make calls to all your accounts, you shouldn't use this code
            // example. Instead, you should run OAuthTokenGenerator.exe to generate a
            // refresh token and use that configuration in your website's Web.config.
            AdWordsAppConfig config = user.Config as AdWordsAppConfig;
            if (user.Config.OAuth2Mode == OAuth2Flow.APPLICATION &&
                string.IsNullOrEmpty(config.OAuth2RefreshToken))
            {
                Response.Redirect("OAuthLogin.aspx");
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
            ReportDefinition definition = new ReportDefinition()
            {
                reportName = "Last 7 days CRITERIA_PERFORMANCE_REPORT",
                reportType = ReportDefinitionReportType.CRITERIA_PERFORMANCE_REPORT,
                downloadFormat = DownloadFormat.GZIPPED_CSV,
                dateRangeType = ReportDefinitionDateRangeType.LAST_7_DAYS,

                selector = new Selector()
                {
                    fields = new string[]
                    {
                        "CampaignId",
                        "AdGroupId",
                        "Id",
                        "CriteriaType",
                        "Criteria",
                        "FinalUrls",
                        "Clicks",
                        "Impressions",
                        "Cost"
                    },
                    predicates = new Predicate[]
                    {
                        Predicate.In("Status", new string[]
                        {
                            "ACTIVE",
                            "PAUSED"
                        })
                    }
                }
            };

            // Optional: Include zero impression rows.
            AdWordsAppConfig config = (AdWordsAppConfig) user.Config;
            config.IncludeZeroImpressions = true;

            string filePath = Path.GetTempFileName();

            try
            {
                ReportUtilities utilities = new ReportUtilities(user, "v201601", definition);
                using (ReportResponse response = utilities.GetResponse())
                {
                    response.Save(filePath);
                }
            }
            catch (Exception e)
            {
                throw new System.ApplicationException("Failed to download report.", e);
            }

            Response.AddHeader("content-disposition", "attachment;filename=report.gzip");
            Response.WriteFile(filePath);
            Response.End();
        }

        /// <summary>
        /// Handles the Click event of the btnGetCampaigns control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">The <see cref="System.EventArgs"/> instance containing
        /// the event data.</param>
        protected void OnGetCampaignsButtonClick(object sender, EventArgs eventArgs)
        {
            ConfigureUserForOAuth();

            // Now proceed to make your API calls as usual.
            // Create a selector.
            Selector selector = new Selector()
            {
                fields = new string[]
                {
                    Campaign.Fields.Id,
                    Campaign.Fields.Name,
                    Campaign.Fields.Status
                },
                ordering = new OrderBy[]
                {
                    OrderBy.Asc(Campaign.Fields.Name)
                }
            };

            (user.Config as AdWordsAppConfig).ClientCustomerId = txtCustomerId.Text;

            try
            {
                CampaignService service =
                    (CampaignService) user.GetService(AdWordsService.v201809.CampaignService);

                CampaignPage page = service.get(selector);

                // Display campaigns.
                if (page != null && page.entries != null && page.entries.Length > 0)
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Columns.AddRange(new DataColumn[]
                    {
                        new DataColumn("Serial No.", typeof(int)),
                        new DataColumn("Campaign Id", typeof(long)),
                        new DataColumn("Campaign Name", typeof(string)),
                        new DataColumn("Status", typeof(string))
                    });
                    for (int i = 0; i < page.entries.Length; i++)
                    {
                        Campaign campaign = page.entries[i];
                        DataRow dataRow = dataTable.NewRow();
                        dataRow.ItemArray = new object[]
                        {
                            i + 1,
                            campaign.id,
                            campaign.name,
                            campaign.status.ToString()
                        };
                        dataTable.Rows.Add(dataRow);
                    }

                    CampaignGrid.DataSource = dataTable;
                    CampaignGrid.DataBind();
                }
                else
                {
                    Response.Write("No campaigns were found.");
                }
            }
            catch (Exception e)
            {
                Response.Write(string.Format("Failed to get campaigns. Exception says \"{0}\"",
                    e.Message));
            }
        }

        /// <summary>
        /// Configures the AdWords user for OAuth.
        /// </summary>
        private void ConfigureUserForOAuth()
        {
            AdWordsAppConfig config = (user.Config as AdWordsAppConfig);
            if (config.OAuth2Mode == OAuth2Flow.APPLICATION &&
                string.IsNullOrEmpty(config.OAuth2RefreshToken))
            {
                user.OAuthProvider = (OAuth2ProviderForApplications) Session["OAuthProvider"];
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
        /// Handles the RowDataBound event of the CampaignGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The
        /// <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance
        /// containing the event data.</param>
        protected void CampaignGrid_RowDataBound(object sender, GridViewRowEventArgs e)
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
