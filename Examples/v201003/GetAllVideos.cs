// Copyright 2010, Google Inc. All Rights Reserved.
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

using com.google.api.adwords.lib;
using com.google.api.adwords.v201003;

using System;
using System.Collections.Generic;
using System.Text;

namespace com.google.api.adwords.examples.v201003 {
  /// <summary>
  /// This code example gets all videos. To upload video, see
  /// http://adwords.google.com/support/aw/bin/answer.py?hl=en&answer=39454.
  ///
  /// Tags: MediaService.get
  /// </summary>
  class GetAllVideos : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets all videos. To upload video, see " +
            "http://adwords.google.com/support/aw/bin/answer.py?hl=en&answer=39454.";
      }
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the MediaService.
      MediaService mediaService = (MediaService) user.GetService(
          AdWordsService.v201003.MediaService);

      // Create selector.
      MediaSelector selector = new MediaSelector();
      selector.mediaType = MediaMediaType.VIDEO;
      selector.mediaTypeSpecified = true;

      try {
        // Get all images.
        MediaPage page = mediaService.get(selector);

        // Display images.
        if (page != null && page.media != null && page.media.Length > 0) {
          foreach (Video video in page.media) {
            Console.WriteLine("Video with id \"{0}\" and name \"{1}\" was found.",
                video.mediaId, video.name);
          }
        } else {
          Console.WriteLine("No images were found.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to get all images. Exception says \"{0}\"", ex.Message);
      }
    }

    /// <summary>
    /// Converts an array of Media_Size_DimensionsMapEntry into a dictionary.
    /// </summary>
    /// <param name="dimensions">The array of Media_Size_DimensionsMapEntry to be
    /// converted into a dictionary.</param>
    /// <returns>A dictionary with key as MediaSize, and value as Dimensions.
    /// </returns>
    private Dictionary<MediaSize, Dimensions> CreateMediaDimensionMap(
        Media_Size_DimensionsMapEntry[] dimensions) {
      Dictionary<MediaSize, Dimensions> mediaMap = new Dictionary<MediaSize, Dimensions>();
      foreach (Media_Size_DimensionsMapEntry dimension in dimensions) {
        mediaMap.Add(dimension.key, dimension.value);
      }
      return mediaMap;
    }
  }
}
