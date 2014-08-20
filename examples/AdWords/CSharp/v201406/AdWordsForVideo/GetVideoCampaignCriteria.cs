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
  /// This code example demonstrates how to retrieve all campaign-level
  /// criteria in a campaign.
  ///
  /// Tags: VideoCampaignCriterionService.get
  /// </summary>
  /// <remarks>AdWords for Video API is a Beta feature.</remarks>
  public class GetVideoCampaignCriteria : ExampleBase {

    /// <summary>
    /// Maximum number of results to fetch.
    /// </summary>
    private const int PAGE_SIZE = 100;

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example illustrates how to retrieve all non-deleted video " +
            "campaigns for an account.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      GetVideoCampaignCriteria codeExample = new GetVideoCampaignCriteria();
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
      // Get the VideoCampaignCriterionService.
      VideoCampaignCriterionService videoCampaignCriterionService =
          (VideoCampaignCriterionService) user.GetService(
              AdWordsService.v201406.VideoCampaignCriterionService);

      int offset = 0;

      VideoCampaignCriterionPage page = new VideoCampaignCriterionPage();

      try {
        // Create selector.
        VideoCampaignCriterionSelector selector = new VideoCampaignCriterionSelector();
        selector.campaignIds = new long[] { campaignId };

        selector.paging = new Paging();

        do {
          selector.paging.startIndex = offset;
          selector.paging.numberResults = PAGE_SIZE;

          page = videoCampaignCriterionService.get(selector);

          // Display the results.
          if (page != null && page.entries != null) {
            int i = offset;
            foreach (VideoCampaignCriterion videoCampaignCriterion in page.entries) {
              String negative =
                  (videoCampaignCriterion is NegativeVideoCampaignCriterion) ? " (negative)"
                      : "";
              Console.WriteLine("{0}) Video {1} criterion ID {2} of type '{3}'", (i + 1),
                negative,
                  videoCampaignCriterion.criterion.id,
                  videoCampaignCriterion.criterion.BaseCriterionType);
              i++;
            }
          } else {
            Console.WriteLine("No video campaign criteria were found.");
          }
          offset += PAGE_SIZE;
        } while (offset < page.totalNumEntries);
        Console.WriteLine("Number of video campaign criteria found: {0}", page.totalNumEntries);
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to get video campaign criteria.", ex);
      }
    }
  }
}