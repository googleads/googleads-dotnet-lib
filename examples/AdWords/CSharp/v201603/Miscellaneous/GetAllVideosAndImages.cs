// Copyright 2016, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201603;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201603 {
  /// <summary>
  /// This code example gets all videos and images. To upload video, see
  /// http://adwords.google.com/support/aw/bin/answer.py?hl=en&amp;answer=39454.
  /// To upload image, run UploadImage.cs.
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
      } catch (Exception e) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(e));
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
          AdWordsService.v201603.MediaService);

      // Create a selector.
      Selector selector = new Selector() {
        fields = new string[] {
          Media.Fields.MediaId, Dimensions.Fields.Width,
          Dimensions.Fields.Height,  Media.Fields.MimeType
        },
        predicates = new Predicate[] {
          Predicate.In(Media.Fields.Type, new string[] {
            MediaMediaType.VIDEO.ToString(),
            MediaMediaType.IMAGE.ToString()
          })
        },
        paging = Paging.Default
      };
      MediaPage page = new MediaPage();

      try {
        do {
          page = mediaService.get(selector);

          if (page != null && page.entries != null) {
            int i = selector.paging.startIndex;

            foreach (Media media in page.entries) {
              if (media is Video) {
                Video video = (Video) media;
                Console.WriteLine("{0}) Video with id '{1}' and name '{2}' was found.",
                    i + 1, video.mediaId, video.name);
              } else if (media is Image) {
                Image image = (Image) media;
                Dictionary<MediaSize, Dimensions> dimensions =
                    CreateMediaDimensionMap(image.dimensions);
                Console.WriteLine("{0}) Image with id '{1}', dimensions '{2}x{3}', and MIME " +
                    "type '{4}' was found.", i + 1, image.mediaId,
                    dimensions[MediaSize.FULL].width, dimensions[MediaSize.FULL].height,
                    image.mimeType);
              }
              i++;
            }
          }
          selector.paging.IncreaseOffset();
        } while (selector.paging.startIndex < page.totalNumEntries);
        Console.WriteLine("Number of images and videos found: {0}", page.totalNumEntries);
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to get images and videos.", e);
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
