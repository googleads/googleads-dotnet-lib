// Copyright 2019 Google LLC
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

using Google.Api.Ads.Common.Util;
using Google.Api.Ads.AdManager.Lib;
using Google.Api.Ads.AdManager.v202311;

using System;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v202311
{
    /// <summary>
    /// This code example creates a new template creative for a given advertiser.
    /// To determine which companies are advertisers, run
    /// GetCompaniesByStatement.cs. To determine which creatives already exist,
    /// run GetAllCreatives.cs. To determine which creative templates exist, run
    /// GetAllCreativeTemplates.cs.
    /// </summary>
    public class CreateCreativeFromTemplate : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example creates a new template creative for a given " +
                    "advertiser. To determine which companies are advertisers, " +
                    "run GetCompaniesByStatement.cs. To determine which creatives already exist, " +
                    "run GetAllCreatives.cs. To determine which creative templates exist, " +
                    "run GetAllCreativeTemplates.cs.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            CreateCreativeFromTemplate codeExample = new CreateCreativeFromTemplate();
            Console.WriteLine(codeExample.Description);
            codeExample.Run(new AdManagerUser());
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (CreativeService creativeService = user.GetService<CreativeService>())
            {
                // Set the ID of the advertiser (company) that all creative will be
                // assigned to.
                long advertiserId = long.Parse(_T("INSERT_ADVERTISER_COMPANY_ID_HERE"));

                // Use the image banner with optional third party tracking template.
                long creativeTemplateId = 10000680L;

                // Create the local custom creative object.
                TemplateCreative templateCreative = new TemplateCreative();
                templateCreative.name = "Template creative";
                templateCreative.advertiserId = advertiserId;
                templateCreative.creativeTemplateId = creativeTemplateId;

                // Set the creative size.
                Size size = new Size();
                size.width = 300;
                size.height = 250;
                size.isAspectRatio = false;

                templateCreative.size = size;

                // Create the asset variable value.
                AssetCreativeTemplateVariableValue assetVariableValue =
                    new AssetCreativeTemplateVariableValue();
                assetVariableValue.uniqueName = "Imagefile";
                CreativeAsset asset = new CreativeAsset();
                asset.assetByteArray =
                    MediaUtilities.GetAssetDataFromUrl("https://goo.gl/3b9Wfh", user.Config);
                asset.fileName = String.Format("image{0}.jpg", this.GetTimeStamp());
                assetVariableValue.asset = asset;

                // Create the image width variable value.
                LongCreativeTemplateVariableValue imageWidthVariableValue =
                    new LongCreativeTemplateVariableValue();
                imageWidthVariableValue.uniqueName = "Imagewidth";
                imageWidthVariableValue.value = 300;

                // Create the image height variable value.
                LongCreativeTemplateVariableValue imageHeightVariableValue =
                    new LongCreativeTemplateVariableValue();
                imageHeightVariableValue.uniqueName = "Imageheight";
                imageHeightVariableValue.value = 250;

                // Create the URL variable value.
                UrlCreativeTemplateVariableValue urlVariableValue =
                    new UrlCreativeTemplateVariableValue();
                urlVariableValue.uniqueName = "ClickthroughURL";
                urlVariableValue.value = "www.google.com";

                // Create the target window variable value.
                StringCreativeTemplateVariableValue targetWindowVariableValue =
                    new StringCreativeTemplateVariableValue();
                targetWindowVariableValue.uniqueName = "Targetwindow";
                targetWindowVariableValue.value = "_blank";

                templateCreative.creativeTemplateVariableValues =
                    new BaseCreativeTemplateVariableValue[]
                    {
                        assetVariableValue,
                        imageWidthVariableValue,
                        imageHeightVariableValue,
                        urlVariableValue,
                        targetWindowVariableValue
                    };

                try
                {
                    // Create the template creative on the server.
                    Creative[] createdTemplateCreatives = creativeService.createCreatives(
                        new Creative[]
                        {
                            templateCreative
                        });

                    foreach (Creative createdTemplateCreative in createdTemplateCreatives)
                    {
                        Console.WriteLine(
                            "A template creative with ID \"{0}\", name \"{1}\", and type " +
                            "\"{2}\" was created and can be previewed at {3}",
                            createdTemplateCreative.id, createdTemplateCreative.name,
                            createdTemplateCreative.GetType().Name,
                            createdTemplateCreative.previewUrl);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to create creatives. Exception says \"{0}\"",
                        e.Message);
                }
            }
        }
    }
}
