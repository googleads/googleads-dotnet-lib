// Copyright 2015, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdWords.v201502;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201502 {
  /// <summary>
  /// This code example gets all videos and images. To upload video, see
  /// http://adwords.google.com/support/aw/bin/answer.py?hl=en&amp;answer=39454.
  /// To upload image, run UploadImage.cs.
  ///
  /// Tags: MediaService.get
  /// </summary>
  public class GetAllVideosAndImages : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      GetAllVideosAndImages codeExample = new GetAllVideosAndImages();
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
        return "This code example gets all videos and images. To upload video, see " +
            "http://adwords.google.com/support/aw/bin/answer.py?hl=en&amp;answer=39454. To " +
            "upload image, run UploadImage.cs.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    public void Run(AdWordsUser user) {
      // Get the MediaService.
      MediaService mediaService = (MediaService) user.GetService(
          AdWordsService.v201502.MediaService);

      // Create a selector.
      Selector selector = new Selector();
      selector.fields = new string[] {"MediaId", "Width", "Height", "MimeType"};

      // Set the filter.
      Predicate predicate = new Predicate();
      predicate.@operator = PredicateOperator.IN;
      predicate.field = "Type";
      predicate.values = new string[] {MediaMediaType.VIDEO.ToString(),
          MediaMediaType.IMAGE.ToString()};

      selector.predicates = new Predicate[] {predicate};

      // Set selector paging.
      selector.paging = new Paging();

      int offset = 0;
      int pageSize = 500;

      MediaPage page = new MediaPage();

      try {
        do {
          selector.paging.startIndex = offset;
          selector.paging.numberResults = pageSize;

          page = mediaService.get(selector);

          if (page != null && page.entries != null) {
            int i = offset;

            foreach (Media media in page.entries) {
              if (media is Video) {
                Video video = (Video) media;
                Console.WriteLine("{0}) Video with id \"{1}\" and name \"{2}\" was found.",
                    i, video.mediaId, video.name);
              } else if (media is Image) {
                Image image = (Image) media;
                Dictionary<MediaSize, Dimensions> dimensions =
                    CreateMediaDimensionMap(image.dimensions);
                Console.WriteLine("{0}) Image with id '{1}', dimensions '{2}x{3}', and MIME type " +
                    "'{4}' was found.", i, image.mediaId, dimensions[MediaSize.FULL].width,
                    dimensions[MediaSize.FULL].height, image.mimeType);
              }
              i++;
            }
          }
          offset += pageSize;
        } while (offset < page.totalNumEntries);
        Console.WriteLine("Number of images and videos found: {0}", page.totalNumEntries);
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to get images and videos.", ex);
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
