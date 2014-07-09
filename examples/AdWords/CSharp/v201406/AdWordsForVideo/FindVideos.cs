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
  /// This code example illustrates how to find YouTube videos by a search
  /// string. It retrieve details for the first 100 matching videos.
  ///
  /// Tags: VideoService.search
  /// </summary>
  /// <remarks>AdWords for Video API is a Beta feature.</remarks>
  public class FindVideos : ExampleBase {

    /// <summary>
    /// Maximum number of results to fetch.
    /// </summary>
    private const int PAGE_SIZE = 100;

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example illustrates how to find YouTube videos by a search " +
            "string. It retrieve details for the first 100 matching videos.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      FindVideos codeExample = new FindVideos();
      Console.WriteLine(codeExample.Description);
      try {
        string queryString = "INSERT_QUERY_STRING_HERE";
        codeExample.Run(new AdWordsUser(), queryString);
      } catch (Exception ex) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(ex));
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="queryString">The video search query text.</param>
    public void Run(AdWordsUser user, string queryString) {
      // Get the VideoService.
      VideoService videoService = (VideoService) user.GetService(
          AdWordsService.v201406.VideoService);

      // Create a selector.
      VideoSearchSelector selector = new VideoSearchSelector();
      selector.searchType = VideoSearchSelectorSearchType.VIDEO;
      selector.query = queryString;
      selector.paging = new Paging();
      selector.paging.startIndex = 0;
      selector.paging.numberResults = PAGE_SIZE;

      try {
        // Run the query.
        VideoSearchPage page = videoService.search(selector);

        // Display videos.
        if (page != null && page.totalNumEntries > 0) {
          foreach (YouTubeVideo video in page.entries) {
            Console.WriteLine("YouTube video ID {0} with title {1} found.", video.id, video.title);
          }
          Console.WriteLine("Total number of matching videos: {0}.", page.totalNumEntries);
        } else {
          Console.WriteLine("No videos matching {0} were found.", queryString);
        }
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to search for videos.", ex);
      }
    }
  }
}