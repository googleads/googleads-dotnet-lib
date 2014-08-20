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
  /// This code example illustrates how to create a video call to action
  /// overlay.
  ///
  /// Tags: VideoService.mutateCallToAction
  /// </summary>
  /// <remarks>AdWords for Video API is a Beta feature.</remarks>
  public class AddVideoCallToAction : ExampleBase {

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example illustrates how to create a video call to action overlay";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      AddVideoCallToAction codeExample = new AddVideoCallToAction();
      Console.WriteLine(codeExample.Description);
      try {
        string videoId = "INSERT_VIDEO_ID_HERE";
        codeExample.Run(new AdWordsUser(), videoId);
      } catch (Exception ex) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(ex));
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="videoId">The video ID.</param>
    public void Run(AdWordsUser user, string videoId) {
      // Get the VideoService.
      VideoService videoService = (VideoService) user.GetService(
          AdWordsService.v201406.VideoService);

      VideoCallToAction videoCallToAction = new VideoCallToAction();
      videoCallToAction.id = videoId;

      CallToAction callToAction = new CallToAction();

      CallToActionCreative callToActionCreative = new CallToActionCreative();
      callToActionCreative.headline = "Mars cruise";
      callToActionCreative.displayUrl = "www.example.com/mars";
      callToActionCreative.destinationUrl = "www.example.com/mars";

      callToAction.creative = callToActionCreative;
      videoCallToAction.callToAction = callToAction;

      try {
        VideoCallToActionOperation operation = new VideoCallToActionOperation();
        operation.operand = videoCallToAction;

        // If this is a new Call to Action, use Operator.ADD
        // If a Call to Action already exists, use Operator.SET
        operation.@operator = Operator.SET;

        VideoCallToActionOperation[] operations = new VideoCallToActionOperation[] { operation };

        // Add video call to action.
        VideoReturnValue result = videoService.mutateCallToAction(operations);

        if (result != null && result.value != null && result.value.Length > 0) {
          foreach (YouTubeVideo youTubeVideo in result.value) {
            Console.WriteLine("CallToAction overlay was added to video ID {0},  headline {1}.",
                youTubeVideo.id, youTubeVideo.callToAction.creative.headline);
          }
        } else {
          Console.WriteLine("No call to action overlays were added.");
        }
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to add call to action overlay.", ex);
      }
    }
  }
}