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
  /// This code example illustrates how to retrieve all non-deleted video
  /// campaigns for an account.
  ///
  /// Tags: VideoCampaignService.get
  /// </summary>
  /// <remarks>AdWords for Video API is a Beta feature.</remarks>
  public class GetVideoCampaigns : ExampleBase {

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
      GetVideoCampaigns codeExample = new GetVideoCampaigns();
      Console.WriteLine(codeExample.Description);
      try {
        codeExample.Run(new AdWordsUser());
      } catch (Exception ex) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(ex));
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    public void Run(AdWordsUser user) {
      // Get the VideoCampaignService.
      VideoCampaignService videoCampaignService = (VideoCampaignService) user.GetService(
          AdWordsService.v201406.VideoCampaignService);

      int offset = 0;

      VideoCampaignPage page = new VideoCampaignPage();

      try {
        // Create selector.
        VideoCampaignSelector selector = new VideoCampaignSelector();

        selector.paging = new Paging();

        do {
          selector.paging.startIndex = offset;
          selector.paging.numberResults = PAGE_SIZE;

          page = videoCampaignService.get(selector);

          // Display the results.
          if (page != null && page.entries != null) {
            int i = offset;
            foreach (VideoCampaign videoCampaign in page.entries) {
              Console.WriteLine("{0}) Campaign ID {1}, name '{2}' and status '{3}'",
                  (i + 1), videoCampaign.id, videoCampaign.name, videoCampaign.status);
              i++;
            }
          } else {
            Console.WriteLine("No campaigns were found.");
          }
          offset += PAGE_SIZE;
        } while (offset < page.totalNumEntries);
        Console.WriteLine("Number of campaigns found: {0}", page.totalNumEntries);
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to get campaigns.", ex);
      }
    }
  }
}