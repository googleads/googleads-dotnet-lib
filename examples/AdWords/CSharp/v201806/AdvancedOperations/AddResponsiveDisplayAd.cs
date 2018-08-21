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
    /// This code example adds an image representing the ad using the MediaService
    /// and then adds a responsive display ad to an ad group. To get ad groups,
    /// run GetAdGroups.cs.
    /// </summary>
    public class AddResponsiveDisplayAd : ExampleBase
    {
        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            AddResponsiveDisplayAd codeExample = new AddResponsiveDisplayAd();
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
                return
                    "This code example adds an image representing the ad using the MediaService " +
                    "and then adds a responsive display ad to an ad group. To get ad groups, " +
                    "run GetAdGroups.cs.";
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
                    // Create a responsive display ad.
                    ResponsiveDisplayAd responsiveDisplayAd = new ResponsiveDisplayAd
                    {
                        // This ad format does not allow the creation of an image using the
                        // Image.data field. An image must first be created using the MediaService,
                        // and Image.mediaId must be populated when creating the ad.
                        marketingImage = new Image()
                        {
                            mediaId = UploadImage(user, "https://goo.gl/3b9Wfh")
                        },
                        shortHeadline = "Travel",
                        longHeadline = "Travel the World",
                        description = "Take to the air!",
                        businessName = "Google",
                        finalUrls = new string[]
                        {
                            "http://www.example.com"
                        },

                        // Optional: Create a square marketing image using MediaService, and set it
                        // to the ad.
                        squareMarketingImage = new Image()
                        {
                            mediaId = UploadImage(user, "https://goo.gl/mtt54n"),
                        },

                        // Optional: set call to action text.
                        callToActionText = "Shop Now",

                        // Optional: Set dynamic display ad settings, composed of landscape logo
                        // image, promotion text, and price prefix.
                        dynamicDisplayAdSettings = CreateDynamicDisplayAdSettings(user)
                    };

                    // Whitelisted accounts only: Set color settings using hexadecimal values.
                    // Set allowFlexibleColor to false if you want your ads to render by always
                    // using your colors strictly.

                    // responsiveDisplayAd.mainColor = "#0000ff";
                    // responsiveDisplayAd.accentColor = "#ffff00";
                    // responsiveDisplayAd.allowFlexibleColor = false;

                    // Whitelisted accounts only: Set the format setting that the ad will be
                    // served in.

                    // responsiveDisplayAd.formatSetting = DisplayAdFormatSetting.NON_NATIVE;

                    // Create ad group ad.
                    AdGroupAd adGroupAd = new AdGroupAd()
                    {
                        adGroupId = adGroupId,
                        ad = responsiveDisplayAd,
                        status = AdGroupAdStatus.PAUSED
                    };

                    // Create operation.
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
                            ResponsiveDisplayAd newAd = newAdGroupAd.ad as ResponsiveDisplayAd;
                            Console.WriteLine(
                                "Responsive display ad with ID '{0}' and short headline '{1}'" +
                                " was added.", newAd.id, newAd.shortHeadline);
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
        /// Creates the dynamic display ad settings.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <returns></returns>
        private static DynamicSettings CreateDynamicDisplayAdSettings(AdWordsUser user)
        {
            long logoImageMediaId = UploadImage(user, "https://goo.gl/dEvQeF");
            Image logo = new Image()
            {
                mediaId = logoImageMediaId
            };

            return new DynamicSettings()
            {
                landscapeLogoImage = logo,
                pricePrefix = "as low as",
                promoText = "Free shipping!"
            };
        }

        /// <summary>
        /// Uploads the image from the specified <paramref name="url"/>.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="url">The image URL.</param>
        /// <returns>ID of the uploaded image.</returns>
        private static long UploadImage(AdWordsUser user, string url)
        {
            using (MediaService mediaService =
                (MediaService) user.GetService(AdWordsService.v201806.MediaService))
            {
                // Create the image.
                Image image = new Image()
                {
                    data = MediaUtilities.GetAssetDataFromUrl(url, user.Config),
                    type = MediaMediaType.IMAGE
                };

                // Upload the image and return the ID.
                return mediaService.upload(new Media[]
                {
                    image
                })[0].mediaId;
            }
        }
    }
}
