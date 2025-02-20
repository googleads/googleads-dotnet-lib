// Copyright 2022 Google LLC
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
using Google.Api.Ads.AdManager.v202502;

using System;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v202502
{
    /// <summary>
    /// This code example creates a new video creative for a given advertiser.
    /// </summary>
    public class CreateVideoCreative : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example creates a new video creative for a given advertiser.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            CreateVideoCreative codeExample = new CreateVideoCreative();
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
                // Set the ID of the advertiser (company) that all creatives will be
                // assigned to.
                long advertiserId = long.Parse(_T("INSERT_ADVERTISER_COMPANY_ID_HERE"));

                // Create a video creative.
                VideoCreative videoCreative = new VideoCreative() {
                    name = string.Format("Video creative #{0}", new Random().Next(int.MaxValue)),
                    advertiserId = advertiserId,
                    destinationUrl = "https://www.google.com",
                    videoSourceUrl =
                        "https://storage.googleapis.com/interactive-media-ads/media/android.mp4",
                    duration = 115000,
                    size = new Size() {
                        width = 640,
                        height = 360
                    }
                };

                try
                {
                    // Create the video creative on the server.
                    Creative[] videoCreatives =
                        creativeService.createCreatives(new VideoCreative[] { videoCreative });

                    if (videoCreatives != null)
                    {
                        foreach (Creative creative in videoCreatives)
                        {
                            // Use "is" operator to determine what type of creative was
                            // returned.
                            if (creative is VideoCreative)
                            {
                                Console.WriteLine(
                                    "A video creative with ID ='{0}' and name ='{1}' " +
                                    "was created and can be previewed at: {2}",
                                    creative.id,
                                    creative.name,
                                    ((VideoCreative) creative).vastPreviewUrl);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("No creatives created.");
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
