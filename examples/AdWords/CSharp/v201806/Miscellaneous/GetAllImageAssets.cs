// Copyright 2018 Google LLC
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
using Google.Api.Ads.AdWords.v201806;

using System;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201806
{
    /// <summary>
    /// This code example gets all image assets. To upload an image asset, run UploadImageAsset.cs.
    /// </summary>
    public class GetAllImageAssets : ExampleBase
    {
        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            GetAllImageAssets codeExample = new GetAllImageAssets();
            Console.WriteLine(codeExample.Description);
            try
            {
                codeExample.Run(new AdWordsUser());
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception occurred while running this code example. {0}",
                    ExampleUtilities.FormatException(e));
            }
        }

        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example gets all image assets. To upload an image asset, run " +
                    "UploadImageAsset.cs.";
            }
        }

        /// <summary>
        /// Runs the code example.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        public void Run(AdWordsUser user)
        {
            using (AssetService assetService =
                (AssetService) user.GetService(AdWordsService.v201806.AssetService))
            {
                // Create the selector.
                Selector selector = new Selector()
                {
                    fields = new string[]
                    {
                        Asset.Fields.AssetName,
                        Asset.Fields.AssetStatus,
                        ImageAsset.Fields.ImageFileSize,
                        ImageDimensionInfo.Fields.ImageWidth,
                        ImageDimensionInfo.Fields.ImageHeight,
                        ImageDimensionInfo.Fields.ImageFullSizeUrl
                    },
                    predicates = new Predicate[]
                    {
                        // Filter for image assets only.
                        Predicate.Equals(Asset.Fields.AssetSubtype, AssetType.IMAGE.ToString())
                    },
                    paging = Paging.Default
                };

                AssetPage page = new AssetPage();

                try
                {
                    do
                    {
                        // Get the image assets.
                        page = assetService.get(selector);

                        // Display the results.
                        if (page != null && page.entries != null)
                        {
                            int i = selector.paging.startIndex;
                            foreach (ImageAsset imageAsset in page.entries)
                            {
                                Console.WriteLine(
                                    "{0}) Image asset with id = '{1}', name = '{2}' and " +
                                    "status = '{3}' was found.", i + 1, imageAsset.assetId,
                                    imageAsset.assetName, imageAsset.assetStatus);
                                Console.WriteLine("  Size is {0}x{1} and asset URL is {2}.",
                                    imageAsset.fullSizeInfo.imageWidth,
                                    imageAsset.fullSizeInfo.imageHeight,
                                    imageAsset.fullSizeInfo.imageUrl);
                                i++;
                            }
                        }

                        selector.paging.IncreaseOffset();
                    } while (selector.paging.startIndex < page.totalNumEntries);

                    Console.WriteLine("Number of image assets found: {0}", page.totalNumEntries);
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException("Failed to retrieve image assets.", e);
                }
            }
        }
    }
}
