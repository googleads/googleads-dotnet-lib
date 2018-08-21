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
    /// This code example creates a click-to-download ad, also known as an
    /// app promotion ad to a given ad group. To list ad groups, run
    /// GetAdGroups.cs.
    /// </summary>
    public class AddClickToDownloadAd : ExampleBase
    {
        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            AddClickToDownloadAd codeExample = new AddClickToDownloadAd();
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
                return "This code example creates a click-to-download ad, also known as an app " +
                    "promotion ad to a given ad group. To list ad groups, run GetAdGroups.cs.";
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
                // Create the template ad.
                TemplateAd clickToDownloadAppAd = new TemplateAd
                {
                    name = "Ad for demo game",
                    templateId = 353,
                    finalUrls = new string[]
                    {
                        "http://play.google.com/store/apps/details?id=com.example.demogame"
                    },
                    displayUrl = "play.google.com"
                };

                // Create the template elements for the ad. You can refer to
                // https://developers.google.com/adwords/api/docs/appendix/templateads
                // for the list of avaliable template fields.
                TemplateElementField headline = new TemplateElementField
                {
                    name = "headline",
                    fieldText = "Enjoy your drive in Mars",
                    type = TemplateElementFieldType.TEXT
                };

                TemplateElementField description1 = new TemplateElementField
                {
                    name = "description1",
                    fieldText = "Realistic physics simulation",
                    type = TemplateElementFieldType.TEXT
                };

                TemplateElementField description2 = new TemplateElementField
                {
                    name = "description2",
                    fieldText = "Race against players online",
                    type = TemplateElementFieldType.TEXT
                };

                TemplateElementField appId = new TemplateElementField
                {
                    name = "appId",
                    fieldText = "com.example.demogame",
                    type = TemplateElementFieldType.TEXT
                };

                TemplateElementField appStore = new TemplateElementField
                {
                    name = "appStore",
                    fieldText = "2",
                    type = TemplateElementFieldType.ENUM
                };

                // Optionally specify a landscape image. The image needs to be in a BASE64
                // encoded form. Here we download a demo image and encode it for this ad.
                byte[] imageData =
                    MediaUtilities.GetAssetDataFromUrl("https://goo.gl/9JmyKk", user.Config);
                Image image = new Image
                {
                    data = imageData
                };
                TemplateElementField landscapeImage = new TemplateElementField
                {
                    name = "landscapeImage",
                    fieldMedia = image,
                    type = TemplateElementFieldType.IMAGE
                };

                TemplateElement adData = new TemplateElement
                {
                    uniqueName = "adData",
                    fields = new TemplateElementField[]
                    {
                        headline,
                        description1,
                        description2,
                        appId,
                        appStore,
                        landscapeImage
                    }
                };

                clickToDownloadAppAd.templateElements = new TemplateElement[]
                {
                    adData
                };

                // Create the adgroupad.
                AdGroupAd clickToDownloadAppAdGroupAd = new AdGroupAd
                {
                    adGroupId = adGroupId,
                    ad = clickToDownloadAppAd,

                    // Optional: Set the status.
                    status = AdGroupAdStatus.PAUSED
                };

                // Create the operation.
                AdGroupAdOperation operation = new AdGroupAdOperation
                {
                    @operator = Operator.ADD,
                    operand = clickToDownloadAppAdGroupAd
                };

                try
                {
                    // Create the ads.
                    AdGroupAdReturnValue retval = adGroupAdService.mutate(new AdGroupAdOperation[]
                    {
                        operation
                    });

                    // Display the results.
                    if (retval != null && retval.value != null)
                    {
                        foreach (AdGroupAd adGroupAd in retval.value)
                        {
                            Console.WriteLine(
                                "New click-to-download ad with id = \"{0}\" and url = \"{1}\" " +
                                "was created.", adGroupAd.ad.id, adGroupAd.ad.finalUrls[0]);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No click-to-download ads were created.");
                    }
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException("Failed to create click-to-download ad.",
                        e);
                }
            }
        }
    }
}
