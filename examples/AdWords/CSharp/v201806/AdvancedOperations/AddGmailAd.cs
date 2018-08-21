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
    /// This code example adds a Gmail ad to a given ad group. The ad group's
    /// campaign needs to have an AdvertisingChannelType of DISPLAY and
    /// AdvertisingChannelSubType of DISPLAY_GMAIL_AD.
    /// To get ad groups, run GetAdGroups.cs.
    /// </summary>
    public class AddGmailAd : ExampleBase
    {
        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            AddGmailAd codeExample = new AddGmailAd();
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
                return "This code example adds a Gmail ad to a given ad group. The ad group's " +
                    "campaign needs to have an AdvertisingChannelType of DISPLAY and " +
                    "AdvertisingChannelSubType of DISPLAY_GMAIL_AD. To get ad groups, " +
                    "run GetAdGroups.cs.";
            }
        }

        /// <summary>
        /// Runs the code example.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="adGroupId">Id of the adgroup to which ads are added.</param>
        public void Run(AdWordsUser user, long adGroupId)
        {
            using (AdGroupAdService adGroupAdService =
                (AdGroupAdService) user.GetService(AdWordsService.v201806.AdGroupAdService))
            {
                // This ad format does not allow the creation of an image using the
                // Image.data field. An image must first be created using the
                // MediaService, and Image.mediaId must be populated when creating the
                // ad.
                Image logoImage = new Image
                {
                    mediaId = UploadImage(user, "https://goo.gl/mtt54n").mediaId
                };

                Image marketingImage = new Image
                {
                    mediaId = UploadImage(user, "https://goo.gl/3b9Wfh").mediaId
                };

                GmailTeaser teaser = new GmailTeaser
                {
                    headline = "Dream",
                    description = "Create your own adventure",
                    businessName = "Interplanetary Ships",
                    logoImage = logoImage
                };

                // Creates a Gmail ad.
                GmailAd gmailAd = new GmailAd
                {
                    teaser = teaser,
                    marketingImage = marketingImage,
                    marketingImageHeadline = "Travel",
                    marketingImageDescription = "Take to the skies!",
                    finalUrls = new string[]
                    {
                        "http://www.example.com/"
                    }
                };

                // Creates ad group ad for the Gmail ad.
                AdGroupAd adGroupAd = new AdGroupAd
                {
                    adGroupId = adGroupId,
                    ad = gmailAd,
                    // Optional: Set additional settings.
                    status = AdGroupAdStatus.PAUSED
                };

                // Creates ad group ad operation and add it to the list.
                AdGroupAdOperation operation = new AdGroupAdOperation
                {
                    operand = adGroupAd,
                    @operator = Operator.ADD
                };

                try
                {
                    // Adds a responsive display ad on the server.
                    AdGroupAdReturnValue result = adGroupAdService.mutate(new AdGroupAdOperation[]
                    {
                        operation
                    });

                    if (result == null || result.value == null || result.value.Length == 0)
                    {
                        Console.WriteLine("No Gmail ads were added.");
                        return;
                    }

                    // Prints out some information for each created Gmail ad.
                    foreach (AdGroupAd newAdGroupAd in result.value)
                    {
                        Console.WriteLine("A Gmail ad with ID {0} and headline '{1}' was added.",
                            newAdGroupAd.ad.id, (newAdGroupAd.ad as GmailAd).teaser.headline);
                    }
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException("Failed to add Gmail ads.", e);
                }
            }
        }

        /// <summary>
        /// Uploads an image to the server.
        /// </summary>
        /// <param name="user">The AdWords user.</param>
        /// <param name="url">The URL of image to upload.</param>
        /// <returns>The created image.</returns>
        private static Media UploadImage(AdWordsUser user, string url)
        {
            using (MediaService mediaService =
                (MediaService) user.GetService(AdWordsService.v201806.MediaService))
            {
                Image image = new Image
                {
                    data = MediaUtilities.GetAssetDataFromUrl(url, user.Config),
                    type = MediaMediaType.IMAGE
                };
                return mediaService.upload(new Media[]
                {
                    image
                })[0];
            }
        }
    }
}
