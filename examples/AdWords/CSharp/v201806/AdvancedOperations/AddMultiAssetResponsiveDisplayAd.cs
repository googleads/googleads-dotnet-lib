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
    /// This code example adds a responsive display ad (MultiAssetResponsiveDisplayAd)
    /// to an ad group. Image assets are uploaded using AssetService. To get ad groups,
    /// run GetAdGroups.cs.
    /// </summary>
    public class AddMultiAssetResponsiveDisplayAd : ExampleBase
    {
        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            AddMultiAssetResponsiveDisplayAd codeExample = new AddMultiAssetResponsiveDisplayAd();
            Console.WriteLine(codeExample.Description);
            try
            {
                long adGroupId = long.Parse("INSERT_ADGROUP_ID_HERE");
                codeExample.Run(new AdWordsUser(), adGroupId);
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
                return "This code example adds a responsive display ad " +
                    "(MultiAssetResponsiveDisplayAd) to an ad group. Image assets are uploaded " +
                    "using AssetService. To get ad groups, run GetAdGroups.cs.";
            }
        }

        /// <summary>
        /// Runs the code example.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="adGroupId">Id of the ad group to which ads are added.
        /// </param>
        public void Run(AdWordsUser user, long adGroupId)
        {
            using (AdGroupAdService adGroupAdService =
                (AdGroupAdService) user.GetService(AdWordsService.v201806.AdGroupAdService))
            {
                try
                {
                    // Create the ad.
                    MultiAssetResponsiveDisplayAd ad = new MultiAssetResponsiveDisplayAd()
                    {
                        headlines = new AssetLink[]
                        {
                            new AssetLink()
                            {
                                // Text assets can be specified directly in the asset field when
                                // creating the ad.
                                asset = new TextAsset()
                                {
                                    assetText = "Travel to Mars",
                                },
                            },
                            new AssetLink()
                            {
                                asset = new TextAsset()
                                {
                                    assetText = "Travel to Jupiter",
                                },
                            },
                            new AssetLink()
                            {
                                asset = new TextAsset()
                                {
                                    assetText = "Travel to Pluto",
                                },
                            },
                        },
                        descriptions = new AssetLink[]
                        {
                            new AssetLink()
                            {
                                asset = new TextAsset()
                                {
                                    assetText = "Visit the planet in a luxury spaceship.",
                                },
                            },
                            new AssetLink()
                            {
                                asset = new TextAsset()
                                {
                                    assetText = "See the planet in style.",
                                },
                            },
                        },
                        businessName = "Galactic Luxury Cruises",
                        longHeadline = new AssetLink()
                        {
                            asset = new TextAsset()
                            {
                                assetText = "Visit the planet in a luxury spaceship.",
                            },
                        },

                        // This ad format does not allow the creation of an image asset by setting
                        // the asset.imageData field. An image asset must first be created using the
                        // AssetService, and asset.assetId must be populated when creating the ad.
                        marketingImages = new AssetLink[]
                        {
                            new AssetLink()
                            {
                                asset = new ImageAsset()
                                {
                                    assetId = UploadImageAsset(user, "https://goo.gl/3b9Wfh")
                                },
                            }
                        },
                        squareMarketingImages = new AssetLink[]
                        {
                            new AssetLink()
                            {
                                asset = new ImageAsset()
                                {
                                    assetId = UploadImageAsset(user, "https://goo.gl/mtt54n")
                                },
                            }
                        },
                        finalUrls = new string[]
                        {
                            "http://www.example.com"
                        },

                        // Optional: set call to action text.
                        callToActionText = "Shop Now",

                        // Set color settings using hexadecimal values. Set allowFlexibleColor to
                        // false if you want your ads to render by always using your colors
                        // strictly.
                        mainColor = "#0000ff",
                        accentColor = "#ffff00",
                        allowFlexibleColor = false,

                        // Set the format setting that the ad will be served in.
                        formatSetting = DisplayAdFormatSetting.NON_NATIVE,

                        // Optional: Set dynamic display ad settings, composed of landscape logo
                        // image, promotion text, and price prefix.
                        dynamicSettingsPricePrefix = "as low as",
                        dynamicSettingsPromoText = "Free shipping!",
                        logoImages = new AssetLink[]
                        {
                            new AssetLink()
                            {
                                asset = new ImageAsset()
                                {
                                    assetId = UploadImageAsset(user, "https://goo.gl/mtt54n")
                                },
                            }
                        }
                    };

                    // Create the ad group ad.
                    AdGroupAd adGroupAd = new AdGroupAd()
                    {
                        ad = ad,
                        adGroupId = adGroupId
                    };

                    // Create the operation.
                    AdGroupAdOperation operation = new AdGroupAdOperation()
                    {
                        operand = adGroupAd,
                        @operator = Operator.ADD
                    };

                    // Make the mutate request.
                    AdGroupAdReturnValue result = adGroupAdService.mutate(new AdGroupAdOperation[]
                    {
                        operation
                    });

                    // Display results.
                    if (result != null && result.value != null)
                    {
                        foreach (AdGroupAd newAdGroupAd in result.value)
                        {
                            MultiAssetResponsiveDisplayAd newAd =
                                newAdGroupAd.ad as MultiAssetResponsiveDisplayAd;
                            Console.WriteLine(
                                "Responsive display ad with ID '{0}' and long headline '{1}'" +
                                " was added.", newAd.id,
                                (newAd.longHeadline.asset as TextAsset).assetText);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No responsive display ads were created.");
                    }
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException("Failed to create responsive display ad.",
                        e);
                }
            }
        }

        /// <summary>
        /// Uploads the image from the specified <paramref name="url"/>.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="url">The image URL.</param>
        /// <returns>ID of the uploaded image.</returns>
        private static long UploadImageAsset(AdWordsUser user, string url)
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
                    // assetName = "Image asset " + ExampleUtilities.GetRandomString(),
                    imageData = MediaUtilities.GetAssetDataFromUrl(url, user.Config),
                };

                // Create the operation.
                AssetOperation operation = new AssetOperation()
                {
                    @operator = Operator.ADD,
                    operand = imageAsset
                };

                // Create the asset and return the ID.
                return assetService.mutate(new AssetOperation[]
                {
                    operation
                }).value[0].assetId;
            }
        }
    }
}
