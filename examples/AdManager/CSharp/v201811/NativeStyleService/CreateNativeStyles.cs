// Copyright 2017, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.AdManager.Lib;
using Google.Api.Ads.AdManager.v201811;

using System;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v201811
{
    /// <summary>
    /// This code example creates a new native style.
    /// </summary>
    public class CreateNativeStyles : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get { return "This code example creates a new native style."; }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            CreateNativeStyles codeExample = new CreateNativeStyles();
            Console.WriteLine(codeExample.Description);
            codeExample.Run(new AdManagerUser());
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (NativeStyleService nativeStyleService = user.GetService<NativeStyleService>())
            {
                // This value is typically loaded from a file or other resource.
                string htmlSnippet = @"<div id=""adunit"" style=""overflow: hidden;"">
  <img src=""[%Thirdpartyimpressiontracker%]"" style=""display:none"">
  <div class=""attribution"">Ad</div>
  <div class=""image"">
    <a class=""image-link""
        href=""%%CLICK_URL_UNESC%%[%Thirdpartyclicktracker%]%%DEST_URL%%""
        target=""_top"">
      <img src=""[%Image%]"">
    </a>
  </div>
  <div class=""app-icon""><img src=""[%Appicon%]""/></div>
  <div class=""title"">
    <a class=""title-link""
        href=""%%CLICK_URL_UNESC%%[%Thirdpartyclicktracker%]%%DEST_URL%%""
        target=""_top"">[%Headline%]</a>
  </div>
  <div class=""reviews""></div>
  <div class=""body"">
    <a class=""body-link""
        href=""%%CLICK_URL_UNESC%%[%Thirdpartyclicktracker%]%%DEST_URL%%""
        target=""_top"">[%Body%]</a>
  </div>
  <div class=""price"">[%Price%]</div>
  <div class=""button"">
    <a class=""button-link""
        href=""%%CLICK_URL_UNESC%%[%Thirdpartyclicktracker%]%%DEST_URL%%""
        target=""_top"">[%Calltoaction%]</a>
  </div>
</div>";

                string cssSnippet = @"body {
    background-color: rgba(255, 255, 255, 1);
    font-family: ""Roboto - Regular"", sans-serif;
    font - weight: normal;
      font - size: 12px;
      line - height: 14px;
    }
.attribution {
    background-color: rgba(236, 182, 0, 1);
    color: rgba(255, 255, 255, 1);
    font-size: 13px;
    display: table;
    margin: 4px 8px;
    padding: 0 3px;
    border-radius: 2px;
}
.image {
    text-align: center;
    margin: 8px;
}
.image img,
.image-link {
    width: 100%;
}
.app-icon {
    float: left;
    margin: 0 8px 4px 8px;
    height: 40px;
    width: 40px;
    background-color: transparent;
}
.app-icon img {
  height: 100%;
  width: 100%;
  border-radius: 20%;
}
.title {
    font-weight: bold;
    font-size: 14px;
    line-height: 20px;
    margin: 8px 8px 4px 8px;
}
.title a {
    color: rgba(112, 112, 112, 1);
text-decoration: none;
}
.reviews {
    float: left;
}
.reviews svg {
  fill: rgba(0, 0, 0, 0.7);
}
.body {
    clear: left;
    margin: 8px;
}
.body a {
  color: rgba(110, 110, 110, 1);
  text-decoration: none;
}
.price {
    display: none;
}
.button {
    font-size: 14px;
    font-weight: bold;
    float: right;
    margin: 0px 16px 16px 0px;
    white-space: nowrap;
}
.button a {
  color: #2196F3;
    text-decoration: none;
}
.button svg {
  display: none;
}";

                // Create nativeStyle size.
                Size size = new Size();
                size.width = 300;
                size.height = 250;

                // Create a native style.
                NativeStyle nativeStyle = new NativeStyle();
                nativeStyle.name = string.Format("Native style #{0}", new Random().Next());
                nativeStyle.size = size;
                nativeStyle.htmlSnippet = htmlSnippet;
                nativeStyle.cssSnippet = cssSnippet;

                // This is the creative template ID for the system-defined native app
                // install ad format, which we will create the native style from. Use
                // CreativeTemplateService.getCreativeTemplatesByStatement() and
                // CreativeTemplate.isNativeEligible to get other native ad formats
                // availablein your network.
                nativeStyle.creativeTemplateId = 10004400;

                try
                {
                    // Create the native styles on the server.
                    NativeStyle[] nativeStyles = nativeStyleService.createNativeStyles(
                        new NativeStyle[]
                        {
                            nativeStyle
                        });

                    if (nativeStyles != null)
                    {
                        foreach (NativeStyle createdNativeStyle in nativeStyles)
                        {
                            // Print out some information for each created native style.
                            Console.WriteLine(
                                "A native style with ID ='{0}' and name='{1}' was created.",
                                createdNativeStyle.id, createdNativeStyle.name);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to create native styles. Exception says \"{0}\"",
                        e.Message);
                }
            }
        }
    }
}
