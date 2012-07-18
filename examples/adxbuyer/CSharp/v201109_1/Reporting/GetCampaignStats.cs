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

// Author: api.anash@gmail.com (Anash P. Oommen)

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201109_1;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201109_1 {
  /// <summary>
  /// This code example gets various statistics for campaigns that received at
  /// least one impression during the last week. To get campaigns, run
  /// GetCampaigns.cs.
  ///
  /// Tags: CampaignService.get
  /// </summary>
  public class GetCampaignStats : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      GetCampaignStats codeExample = new GetCampaignStats();
      Console.WriteLine(codeExample.Description);
      try {
        codeExample.Run(new AdWordsUser());
      } catch (Exception ex) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(ex));
      }
    }

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets various statistics for campaigns that received at least " +
            "one impression during the last week. To get campaigns, run GetCampaigns.cs.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    public void Run(AdWordsUser user) {
      // Get the CampaignService.
      CampaignService campaignService =
          (CampaignService) user.GetService(AdWordsService.v201109_1.CampaignService);

      // Create the selector.
      Selector selector = new Selector();
      selector.fields = new string[] {"Id", "Name", "Status", "Impressions", "Cost", "Ctr"};

      // Create the predicates.
      Predicate predicate = new Predicate();
      predicate.field = "Impressions";
      predicate.@operator = PredicateOperator.GREATER_THAN;
      predicate.values = new string[] {"0"};
      selector.predicates = new Predicate[] {predicate};

      // Set the date range.
      DateRange dateRange = new DateRange();
      dateRange.min = DateTime.Now.AddDays(-8).ToString("yyyyMMdd");
      dateRange.max = DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
      selector.dateRange = dateRange;

      // Set the selector paging.
      selector.paging = new Paging();

      int offset = 0;
      int pageSize = 500;

      CampaignPage page = new CampaignPage();

      try {
        do {
          selector.paging.startIndex = offset;
          selector.paging.numberResults = pageSize;

          // Get the campaigns.
          page = campaignService.get(selector);

          // Display the results.
          if (page != null && page.entries != null) {
            int i = offset;
            foreach (Campaign campaign in page.entries) {
              Console.WriteLine("{0}) Campaign with id = '{1}', name = '{2}' had the following " +
                  "stats last week", i + 1, campaign.id, campaign.name);
              Console.WriteLine("  Clicks: {0}, Impressions: {1}, Cost: {2} micros. Ctr: {3}",
                  campaign.campaignStats.clicks, campaign.campaignStats.impressions,
                  campaign.campaignStats.cost,campaign.campaignStats.ctr);
              i++;
            }
          }
          offset += pageSize;
        } while (offset < page.totalNumEntries);
        Console.WriteLine("Number of matching campaigns found: {0}", page.totalNumEntries);
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to retrieve matching campaigns", ex);
      }
    }
  }
}
