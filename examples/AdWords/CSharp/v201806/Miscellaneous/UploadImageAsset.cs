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
using Google.Api.Ads.Common.Util;

using System;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201806
{
    /// <summary>
    /// This code example uploads an image asset. To get images, run GetAllImageAssets.cs.
    /// </summary>
    public class UploadImageAsset : ExampleBase
    {
        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            UploadImageAsset codeExample = new UploadImageAsset();
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
                return "This code example uploads an image asset. To get images, run " +
                    "GetAllImageAssets.cs.";
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
                // Create the image asset.
                ImageAsset imageAsset = new ImageAsset()
                {
                    // Optional: Provide a unique friendly name to identify your asset. If you
                    // specify the assetName field, then both the asset name and the image being
                    // uploaded should be unique, and should not match another ACTIVE asset in this
                    // customer account.
                    // assetName = "Jupiter Trip " + ExampleUtilities.GetRandomString(),
                    imageData =
                        MediaUtilities.GetAssetDataFromUrl("https://goo.gl/3b9Wfh", user.Config),
                };

                // Create the operation.
                AssetOperation operation = new AssetOperation()
                {
                    @operator = Operator.ADD,
                    operand = imageAsset
                };

                try
                {
                    // Create the asset.
                    AssetReturnValue result = assetService.mutate(new AssetOperation[]
                    {
                        operation
                    });

                    // Display the results.
                    if (result != null && result.value != null && result.value.Length > 0)
                    {
                        Asset newAsset = result.value[0];

                        Console.WriteLine("Image asset with id = '{0}' and name = {1} was created.",
                            newAsset.assetId, newAsset.assetName);
                    }
                    else
                    {
                        Console.WriteLine("No image asset was created.");
                    }
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException("Failed to create image asset.", e);
                }
            }
        }
    }
}
