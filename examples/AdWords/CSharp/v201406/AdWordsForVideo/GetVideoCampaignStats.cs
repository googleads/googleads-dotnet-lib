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

// Author: api.anash@gmail.com (Anash P. Oommen)

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201406;

using System;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201406 {

  /// <summary>
  /// This code example illustrates how to retrieve stats for a video campaign.
  ///
  /// Tags: VideoCampaignService.get
  /// </summary>
  /// <remarks>AdWords for Video API is a Beta feature.</remarks>
  public class GetVideoCampaignStats : ExampleBase {

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example illustrates how to retrieve stats for a video campaign.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      GetVideoCampaignStats codeExample = new GetVideoCampaignStats();
      Console.WriteLine(codeExample.Description);
      try {
        long campaignId = long.Parse("INSERT_CAMPAIGN_ID_HERE");
        codeExample.Run(new AdWordsUser(), campaignId);
      } catch (Exception ex) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(ex));
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="campaignId">The campaign ID.</param>
    public void Run(AdWordsUser user, long campaignId) {
      // Get the VideoCampaignService.
      VideoCampaignService videoCampaignService = (VideoCampaignService) user.GetService(
          AdWordsService.v201406.VideoCampaignService);

      // The dates will be interpreted using the account's timezone.
      String minDateTime = DateTime.Now.Year.ToString() + "0101";
      String maxDateTime = DateTime.Now.ToString("yyyyMMdd");

      // Create selector.
      StatsSelector statsSelector = new StatsSelector();
      DateRange dateRange = new DateRange();
      dateRange.min = minDateTime;
      dateRange.max = maxDateTime;

      statsSelector.dateRange = dateRange;
      statsSelector.segmentationDimensions = new SegmentationDimension[] {
          SegmentationDimension.DATE_MONTH };
      statsSelector.metrics = new Metric[] { Metric.VIEWS, Metric.COST, Metric.AVERAGE_CPV };
      statsSelector.summaryTypes = new VideoEntityStatsSummaryType[] {
          VideoEntityStatsSummaryType.ALL };
      statsSelector.segmentedSummaryType = VideoEntityStatsSummaryType.ALL;

      try {
        // Create selector.
        VideoCampaignSelector selector = new VideoCampaignSelector();
        selector.statsSelector = statsSelector;
        selector.ids = new long[] { campaignId };

        selector.paging = new Paging();

        VideoCampaignPage page = videoCampaignService.get(selector);

        if (page != null) {
          if (page.entries != null && page.entries.Length > 0) {
            VideoCampaign videoCampaign = page.entries[0];

            Console.WriteLine("Campaign ID {0}, name '{1}' and status '{2}'", videoCampaign.id,
                videoCampaign.name, videoCampaign.status);
            if (videoCampaign.stats != null) {
              Console.WriteLine("\tCampaign stats:");
              Console.WriteLine("\t\t" + FormatStats(videoCampaign.stats));
            }
            if (videoCampaign.segmentedStats != null) {
              foreach (VideoEntityStats segment in videoCampaign.segmentedStats) {
                Console.WriteLine("\tCampaign segmented stats for month of " +
                    ((DateKey) segment.segmentKey.Item).date);
                Console.WriteLine("\t\t" + FormatStats(segment));
              }
            }
          } else {
            Console.WriteLine("No campaigns were found.");
          }
          if (page.summaryStats != null) {
            foreach (VideoEntityStats summary in page.summaryStats) {
              Console.WriteLine("\tSummary of type " + summary.summaryType);
              Console.WriteLine("\t\t" + FormatStats(summary));
            }
          } else {
            Console.WriteLine("No summary stats found.");
          }
        } else {
          Console.WriteLine("No campaigns were found.");
        }
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to get campaigns.", ex);
      }
    }

    /// <summary>
    /// Formats a money value for display purposes.
    /// </summary>
    /// <param name="m">The money value.</param>
    /// <returns>The formatted money value as text.</returns>
    private static String Dashify(Money m) {
      return m == null ? "--" : m.microAmount.ToString();
    }

    /// <summary>
    /// Formats the stats for display.
    /// </summary>
    /// <param name="stats">The stats.</param>
    /// <returns>Stats as formatted text.</returns>
    private static String FormatStats(VideoEntityStats stats) {
      return string.Format("Views: {0}, Cost: {1}, Avg. CPC: {2}, Avg. CPV: {3}, "
          + "Avg. CPM: {4}, 25%: {5}, 50%: {6}, 75%: {7}, 100%: {8}",
          stats.viewsSpecified? stats.views.ToString() : "--",
          Dashify(stats.cost),
          Dashify(stats.averageCpc),
          Dashify(stats.averageCpv),
          Dashify(stats.averageCpm),
          stats.quartile25PercentsSpecified? stats.quartile25Percents.ToString() : "--",
          stats.quartile50PercentsSpecified? stats.quartile50Percents.ToString() : "--",
          stats.quartile75PercentsSpecified? stats.quartile75Percents.ToString() : "--",
          stats.quartile100PercentsSpecified? stats.quartile100Percents.ToString() : "--"
      );
    }
  }
}