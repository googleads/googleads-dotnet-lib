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
  /// This code example illustrates how to create a video ad.
  ///
  /// Tags: VideoAdService.mutate
  /// </summary>
  /// <remarks>AdWords for Video API is a Beta feature.</remarks>
  public class AddVideoAd : ExampleBase {

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example illustrates how to create a video ad.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      AddVideoAd codeExample = new AddVideoAd();
      Console.WriteLine(codeExample.Description);
      try {
        long campaignId = long.Parse("INSERT_CAMPAIGN_ID_HERE");
        string videoId = "INSERT_VIDEO_ID_HERE";
        codeExample.Run(new AdWordsUser(), campaignId, videoId);
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
    /// <param name="videoId">The Youtube video ID.</param>
    public void Run(AdWordsUser user, long campaignId, string videoId) {
      // Get the VideoAdService.
      VideoAdService videoAdService = (VideoAdService) user.GetService(
          AdWordsService.v201406.VideoAdService);

      VideoAd videoAd = new VideoAd();
      videoAd.campaignId = campaignId;
      videoAd.name = "My first video ad";
      videoAd.status = VideoAdStatus.PAUSED;

      videoAd.headline = "Best headline ever";
      videoAd.destinationUrl = "http://www.example.com";
      videoAd.displayUrl = "www.example.com";
      videoAd.description1 = "My favorite line 1";
      videoAd.description2 = "My favorite line 2";

      videoAd.videoId = videoId;

      VideoAdDisplayFormat_VideoAdStatusMapEntry statusByFormat =
          new VideoAdDisplayFormat_VideoAdStatusMapEntry();
      statusByFormat.key = VideoAdDisplayFormat.TRUE_VIEW_IN_STREAM;
      statusByFormat.value = VideoAdStatus.ENABLED;

      videoAd.statusByFormat = new VideoAdDisplayFormat_VideoAdStatusMapEntry[] {
          statusByFormat
      };

      try {
        VideoAdOperation operation = new VideoAdOperation();
        operation.operand = videoAd;
        operation.@operator = Operator.ADD;

        VideoAdReturnValue retval = videoAdService.mutate(new VideoAdOperation[] { operation });
        if (retval != null && retval.value != null && retval.value.Length > 0) {
          VideoAd newVideoAd = retval.value[0];
          Console.WriteLine("Ad with id = {0} was added to campaign id {1}.", newVideoAd.id,
              newVideoAd.campaignId);
        } else {
          Console.WriteLine("No video ads were added.");
        }
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to add video ad.", ex);
      }
    }
  }
}