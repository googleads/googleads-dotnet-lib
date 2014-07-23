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
  /// This code example shows how to retrieve all keywords for a video
  /// campaign. To get a list with all campaigns, run GetVideoCampaigns.cs.
  ///
  /// Tags: VideoTargetingGroupCriterionService.get
  /// </summary>
  /// <remarks>AdWords for Video API is a Beta feature.</remarks>
  public class GetKeywordCriteria : ExampleBase {

    /// <summary>
    /// Maximum number of results to fetch.
    /// </summary>
    private const int PAGE_SIZE = 100;

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example shows how to retrieve all keywords for a video campaign. " +
            "To get a list with all campaigns, run GetVideoCampaigns.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      GetKeywordCriteria codeExample = new GetKeywordCriteria();
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
      // Get the VideoTargetingGroupCriterionService.
      VideoTargetingGroupCriterionService videoTargetingGroupCriterionService =
          (VideoTargetingGroupCriterionService) user.GetService(
              AdWordsService.v201406.VideoTargetingGroupCriterionService);

      int offset = 0;

      TargetingGroupCriterionPage page = new TargetingGroupCriterionPage();

      try {
        // Create selector.
        TargetingGroupCriterionSelector selector = new TargetingGroupCriterionSelector();

        selector.criteriaDimension = CriteriaDimension.KEYWORD;
        selector.campaignIds = new long[] { campaignId };

        selector.paging = new Paging();

        do {
          selector.paging.startIndex = offset;
          selector.paging.numberResults = PAGE_SIZE;

          // Get all keywords.
          page = videoTargetingGroupCriterionService.get(selector);

          // Display keywords.
          if (page != null && page.entries != null) {
            int i = offset;
            foreach (TargetingGroupCriterion targetingGroupCriterion in page.entries) {
              String negative =
                  (targetingGroupCriterion is NegativeTargetingGroupCriterion) ?
                      " (negative)" : "";
              BaseKeyword keyword = (BaseKeyword) targetingGroupCriterion.criterion;
              Console.WriteLine("{0}) Criterion {1} ID {2}, targeting group ID {3} and " +
                  "text '{4}'\n", i, negative, keyword.id,
                  targetingGroupCriterion.targetingGroupId, keyword.text);
              i++;
            }
          }
          offset += PAGE_SIZE;
        } while (offset < page.totalNumEntries);
        Console.WriteLine("Number of keywords found: {0}", page.totalNumEntries);
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to get keywords.", ex);
      }
    }
  }
}