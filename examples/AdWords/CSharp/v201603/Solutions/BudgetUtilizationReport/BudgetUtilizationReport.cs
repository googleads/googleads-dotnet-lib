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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.Util.Reports;
using Google.Api.Ads.AdWords.v201603;
using Google.Api.Ads.Common.Util;
using Google.Api.Ads.Common.Util.Reports;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201603 {

  /// <summary>
  /// This code example shows how to calculate the budget utilization for
  /// various campaigns in your account. This report may then be used to
  /// reallocate campaign budgets or investigate campaign performance issues.
  /// </summary>
  public class BudgetUtilizationReport : ExampleBase {

    /// <summary>
    /// Keeps track of campaign details.
    /// </summary>
    public class LocalCampaign {

      /// <summary>
      /// The campaign stats for a specified period.
      /// </summary>
      private CampaignStat stats = new CampaignStat();

      /// <summary>
      /// Gets or sets the campaign ID.
      /// </summary>
      public long CampaignId {
        get;
        set;
      }

      /// <summary>
      /// Gets or sets the name of the campaign.
      /// </summary>
      public string CampaignName {
        get;
        set;
      }

      /// <summary>
      /// Gets or sets the campaign's daily budget amount in micros.
      /// </summary>
      public long BudgetAmount {
        get;
        set;
      }

      /// <summary>
      /// Gets or sets the campaign stats for a specified period.
      /// </summary>
      /// <value>
      /// The stats.
      /// </value>
      public CampaignStat Stats {
        get {
          return stats;
        }
      }
    }

    /// <summary>
    /// Class to keep track of campaign stats.
    /// </summary>
    public class CampaignStat {

      /// <summary>
      /// The campaign stats for Search network.
      /// </summary>
      private NetworkStat searchStats = new NetworkStat();

      /// <summary>
      /// The campaign stats for Display Network.
      /// </summary>
      private NetworkStat displayStats = new NetworkStat();

      /// <summary>
      /// Gets or sets the campaign stats for Search network.
      /// </summary>
      public NetworkStat SearchStats {
        get {
          return searchStats;
        }
      }

      /// <summary>
      /// Gets or sets the campaign stats for Display network.
      /// </summary>
      public NetworkStat DisplayStats {
        get {
          return displayStats;
        }
      }

      /// <summary>
      /// Gets the total cost.
      /// </summary>
      public long Cost {
        get {
          return SearchStats.Cost + DisplayStats.Cost;
        }
      }

      /// <summary>
      /// Gets the total impressions received by the campaign during a
      /// specified period.
      /// </summary>
      public decimal Impressions {
        get {
          return SearchStats.Impressions + DisplayStats.Impressions;
        }
      }

      /// <summary>
      /// Gets the total clicks received by the campaign during a specified
      /// period.
      /// </summary>
      public decimal Clicks {
        get {
          return SearchStats.Clicks + DisplayStats.Clicks;
        }
      }

      /// <summary>
      /// Gets the cost for incremental clicks that the campaign lost due to
      /// budget restrictions.
      /// </summary>
      public long BudgetLostCost {
        get {
          return SearchStats.BudgetLostCost + DisplayStats.BudgetLostCost;
        }
      }

      /// <summary>
      /// Gets the incremental impressions that the campaign lost due to
      /// budget restrictions.
      /// </summary>
      public long BudgetLostImpressions {
        get {
          return SearchStats.BudgetLostImpressions + DisplayStats.BudgetLostImpressions;
        }
      }

      /// <summary>
      /// Gets the incremental clicks that the campaign lost due to budget
      /// restrictions.
      /// </summary>
      public long BudgetLostClicks {
        get {
          return SearchStats.BudgetLostClicks + DisplayStats.BudgetLostClicks;
        }
      }
    }

    /// <summary>
    /// Keeps track of stats for a particular network.
    /// </summary>
    public class NetworkStat {

      /// <summary>
      /// Gets or sets the impressions.
      /// </summary>
      public decimal Impressions {
        get;
        set;
      }

      /// <summary>
      /// Gets or sets the clicks.
      /// </summary>
      public decimal Clicks {
        get;
        set;
      }

      /// <summary>
      /// Gets or sets the cost.
      /// </summary>
      public long Cost {
        get;
        set;
      }

      /// <summary>
      /// Gets or sets the impression share lost due to budget.
      /// </summary>
      public decimal BudgetLostImpressionShare {
        get;
        set;
      }

      /// <summary>
      /// Gets or sets the average CPC.
      /// </summary>
      public long AverageCpc {
        get;
        set;
      }

      /// <summary>
      /// Gets or sets the impression share.
      /// </summary>
      public decimal ImpressionShare {
        get;
        set;
      }

      /// <summary>
      /// Gets the estimated impressions after adjusting for lost impression
      /// share.
      /// </summary>
      public long EstimatedImpressions {
        get {
          return (ImpressionShare == 0) ? 0 : (long) Math.Round(Impressions / ImpressionShare,
              MidpointRounding.AwayFromZero);
        }
      }

      /// <summary>
      /// Gets the impressions lost due to budget restrictions.
      /// </summary>
      public long BudgetLostImpressions {
        get {
          return (ImpressionShare == 0) ? 0 : (long) Math.Round(BudgetLostImpressionShare *
              EstimatedImpressions, MidpointRounding.AwayFromZero);
        }
      }

      /// <summary>
      /// Gets the number of clicks lost due to budget restrictions.
      /// </summary>
      public long BudgetLostClicks {
        get {
          return (long) Math.Round(ClickThroughRate * BudgetLostImpressions,
              MidpointRounding.AwayFromZero);
        }
      }

      /// <summary>
      /// Gets the cost of the clicks lost due to budget restrictions.
      /// </summary>
      public long BudgetLostCost {
        get {
          return BudgetLostClicks * AverageCpc;
        }
      }

      /// <summary>
      /// Gets the click through rate.
      /// </summary>
      public decimal ClickThroughRate {
        get {
          return (Impressions > 0) ? Clicks / Impressions : 0;
        }
      }
    }

    /// <summary>
    /// The stats columns to be retrieved from the campaign performance report.
    /// </summary>
    private string[] CAMPAIGN_PERFORMANCE_COLUMNS = new string[] {
      "CampaignId",
      "CampaignName",
      "Clicks",
      "Impressions",
      "Cost",
      "SearchBudgetLostImpressionShare",
      "ContentBudgetLostImpressionShare",
      "SearchImpressionShare",
      "ContentImpressionShare",
      "TotalBudget",
      "AdNetworkType1",
      "AverageCpc",
    };

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      BudgetUtilizationReport codeExample = new BudgetUtilizationReport();
      Console.WriteLine(codeExample.Description);
      try {
        string startDate = "INSERT_START_DATE_HERE";
        string endDate = "INSERT_END_DATE_HERE";
        string fileName = "INSERT_FILE_NAME_HERE";

        codeExample.Run(new AdWordsUser(), startDate, endDate, fileName);
      } catch (Exception e) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(e));
      }
    }

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example shows how to calculate the budget utilization for various " +
            "campaigns in your account. This report may then be used to reallocate campaign " +
            "budgets or investigate campaign performance issues.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="startDate">The start date for tracking performance.</param>
    /// <param name="endDate">The end date for tracking performance.</param>
    /// <param name="fileName">The file to which the budget utilization report is saved.</param>
    public void Run(AdWordsUser user, string startDate, string endDate, string fileName) {
      string outputPath = ExampleUtilities.GetHomeDir() + Path.DirectorySeparatorChar + fileName;

      Dictionary<long, LocalCampaign> campaigns = FetchCampaignStats(user, startDate, endDate);
      int numDays = GetDaysBetween(startDate, endDate);

      CsvFile csvFile = new CsvFile();
      csvFile.Headers.AddRange(new string[] {
          "CampaignId", "CampaignName", "DailyBudget", "Clicks", "Impressions", "Cost",
          "Lost Clicks", "Lost Impressions", "Lost Cost", "Difference in Amount"
      });
      foreach (long campaignId in campaigns.Keys) {
        LocalCampaign campaign = campaigns[campaignId];

        List<string> row = new List<string>(new string[] {
          campaign.CampaignId.ToString(),
          campaign.CampaignName,
          campaign.BudgetAmount.ToString(),
          campaign.Stats.Clicks.ToString(),
          campaign.Stats.Impressions.ToString(),
          campaign.Stats.Cost.ToString(),
          campaign.Stats.BudgetLostClicks.ToString(),
          campaign.Stats.BudgetLostImpressions.ToString(),
          campaign.Stats.BudgetLostCost.ToString()
        });

        if (campaign.Stats.BudgetLostCost > 0) {
          // Extra budget needed is the same as BudgetLostCost.
          row.Add(campaign.Stats.BudgetLostCost.ToString());
        } else {
          // Surplus budget is the same as cost - budget.
          row.Add((campaign.Stats.Cost - campaign.BudgetAmount * numDays).ToString());
        }
        csvFile.Records.Add(row.ToArray());
      }
      csvFile.Write(outputPath);
    }

    /// <summary>
    /// Gets the number of days between two given dates.
    /// </summary>
    /// <param name="startDate">The start date.</param>
    /// <param name="endDate">The end date.</param>
    /// <returns>The number of days.</returns>
    private int GetDaysBetween(string startDate, string endDate) {
      DateTime start = DateTime.ParseExact(startDate, "yyyyMMdd", null);
      DateTime end = DateTime.ParseExact(endDate, "yyyyMMdd", null);

      return (int) ((end - start).TotalDays + 1);
    }

    /// <summary>
    /// Fetches the campaign stats.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="startDate">The start date.</param>
    /// <param name="endDate">The end date.</param>
    /// <returns>A dictionary, with key as campaign ID, and value as the
    /// campaign details.</returns>
    public Dictionary<long, LocalCampaign> FetchCampaignStats(AdWordsUser user,
        string startDate, string endDate) {
      Dictionary<long, LocalCampaign> campaigns = new Dictionary<long, LocalCampaign>();

      CsvFile csvFile = DownloadCampaignPerformanceReport(user, startDate, endDate);

      for (int i = 0; i < csvFile.Records.Count; i++) {
        var row = csvFile.Records[i];

        long campaignId = long.Parse(row[0]);
        string campaignName = row[1];
        long clicks = long.Parse(row[2]);
        long impressions = long.Parse(row[3]);
        long cost = long.Parse(row[4]);
        string searchBudgetLostIS = row[5];
        string contentBudgetLostIS = row[6];
        string searchIS = row[7];
        string contentIS = row[8];
        long totalBudget = long.Parse(row[9]);
        string network = row[10];
        string averageCpc = row[11];

        LocalCampaign campaign = null;
        if (!campaigns.TryGetValue(campaignId, out campaign)) {
          campaigns[campaignId] = campaign = new LocalCampaign() {
            BudgetAmount = totalBudget,
            CampaignId = campaignId,
            CampaignName = campaignName
          };
        }
        CampaignStat campaignStat = null;

        campaignStat = campaign.Stats;

        if (network == "Display Network") {
          campaignStat.DisplayStats.Clicks = clicks;
          campaignStat.DisplayStats.Impressions = impressions;
          campaignStat.DisplayStats.Cost = cost;
          campaignStat.DisplayStats.BudgetLostImpressionShare =
              NormalizeImpressionShare(contentBudgetLostIS);
          campaignStat.DisplayStats.AverageCpc = long.Parse(averageCpc);
          campaignStat.DisplayStats.ImpressionShare = NormalizeImpressionShare(contentIS);
        } else {
          campaignStat.SearchStats.Clicks = clicks;
          campaignStat.SearchStats.Impressions = impressions;
          campaignStat.SearchStats.Cost = cost;
          campaignStat.SearchStats.BudgetLostImpressionShare =
              NormalizeImpressionShare(searchBudgetLostIS);
          campaignStat.SearchStats.AverageCpc = long.Parse(averageCpc);
          campaignStat.SearchStats.ImpressionShare = NormalizeImpressionShare(searchIS);
        }
      }
      return campaigns;
    }

    /// <summary>
    /// Normalizes the impression share value.
    /// </summary>
    /// <param name="impressionShare">The impression share value from reports.</param>
    /// <returns>The normalized impression share.</returns>
    private decimal NormalizeImpressionShare(string impressionShare) {
      decimal retval = 0;
      impressionShare = impressionShare.Trim('%', '<', '>', ' ');

      if (impressionShare == "--") {
        retval = 0;
      } else {
        retval = decimal.Parse(impressionShare);
      }
      return retval / 100;
    }

    /// <summary>
    /// Downloads the campaign performance report.
    /// </summary>
    /// <param name="user">The user for which the report is run..</param>
    /// <param name="startDate">The start date in yyyyMMdd format.</param>
    /// <param name="endDate">The end date in yyyyMMdd format.</param>
    /// <returns>The campaign performance report, as a CSV file.</returns>
    private CsvFile DownloadCampaignPerformanceReport(AdWordsUser user, string startDate,
        string endDate) {
      string query = string.Format("Select {0} from CAMPAIGN_PERFORMANCE_REPORT DURING {1}, {2}",
          string.Join(", ", CAMPAIGN_PERFORMANCE_COLUMNS), startDate, endDate);

      AdWordsAppConfig appConfig = user.Config as AdWordsAppConfig;
      appConfig.SkipReportHeader = true;
      appConfig.SkipReportSummary = true;

      ReportUtilities reportUtilities = new ReportUtilities(user, query,
          DownloadFormat.CSV.ToString());

      using (ReportResponse response = reportUtilities.GetResponse()) {
        string reportContents = Encoding.UTF8.GetString(response.Download());
        CsvFile csvFile = new CsvFile();
        csvFile.ReadFromString(reportContents, true);
        return csvFile;
      }
    }
  }
}
