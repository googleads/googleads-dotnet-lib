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
using Google.Api.Ads.Common.Util;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201603 {
  /// <summary>
  /// This code example uploads an image. To get images, run GetAllVideosAndImages.cs.
  /// </summary>
  public class UploadImage : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      UploadImage codeExample = new UploadImage();
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
        return "This code example uploads an image. To get images, run GetAllVideosAndImages.cs.";
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

      // Create the image.
      Image image = new Image();
      image.data = MediaUtilities.GetAssetDataFromUrl("http://goo.gl/HJM3L");
      image.type = MediaMediaType.IMAGE;

      try {
        // Upload the image.
        Media[] result = mediaService.upload(new Media[] {image});

        // Display the results.
        if (result != null && result.Length > 0) {
          Media newImage = result[0];
          Dictionary<MediaSize, Dimensions> dimensions =
              CreateMediaDimensionMap(newImage.dimensions);
          Console.WriteLine("Image with id '{0}', dimensions '{1}x{2}', and MIME type '{3}'" +
              " was uploaded.", newImage.mediaId, dimensions[MediaSize.FULL].width,
              dimensions[MediaSize.FULL].height, newImage.mimeType);
        } else {
          Console.WriteLine("No images were uploaded.");
        }
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to upload image.", e);
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
