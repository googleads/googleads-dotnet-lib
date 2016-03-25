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

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201603 {

  /// <summary>
  /// This code example uploads an HTML5 zip file.
  /// </summary>
  public class UploadMediaBundle : ExampleBase {

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      UploadMediaBundle codeExample = new UploadMediaBundle();
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
        return "This example uploads an HTML5 zip file.";
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

      try {
        // Create HTML5 media.
        byte[] html5Zip = MediaUtilities.GetAssetDataFromUrl("https://goo.gl/9Y7qI2");
        // Create a media bundle containing the zip file with all the HTML5 components.
        Media[] mediaBundle = new Media[] {
        new MediaBundle() {
          data = html5Zip,
          type = MediaMediaType.MEDIA_BUNDLE
        }};

        // Upload HTML5 zip.
        mediaBundle = mediaService.upload(mediaBundle);

        // Display HTML5 zip.
        if (mediaBundle != null && mediaBundle.Length > 0) {
          Media newBundle = mediaBundle[0];
          Dictionary<MediaSize, Dimensions> dimensions =
              CreateMediaDimensionMap(newBundle.dimensions);
          Console.WriteLine("HTML5 media with id \"{0}\", dimensions \"{1}x{2}\", and MIME type " +
              "\"{3}\" was uploaded.", newBundle.mediaId, dimensions[MediaSize.FULL].width,
              dimensions[MediaSize.FULL].height, newBundle.mimeType
          );
        } else {
          Console.WriteLine("No HTML5 zip was uploaded.");
        }
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to upload HTML5 zip file.", e);
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