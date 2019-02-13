// Copyright 2018, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdManager.v201902;

using System;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v201902
{
    /// <summary>
    /// This code example creates a new creative wrapper. Creative wrappers must
    /// be associated with a LabelType.CREATIVE_WRAPPER label and applied to ad
    /// units by AdUnit.appliedLabels. To determine which creative wrappers exist,
    /// run GetAllCreativeWrappers.cs.
    /// </summary>
    public class CreateCreativeWrappers : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example creates a new creative wrapper. Creative wrappers must " +
                    "be associated with a LabelType.CREATIVE_WRAPPER label and applied to " +
                    "ad units by AdUnit.appliedLabels. To determine which creative wrappers " +
                    "exist, run GetAllCreativeWrappers.cs.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            CreateCreativeWrappers codeExample = new CreateCreativeWrappers();
            Console.WriteLine(codeExample.Description);
            codeExample.Run(new AdManagerUser());
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (CreativeWrapperService creativeWrapperService =
                user.GetService<CreativeWrapperService>())
            {
                long labelId = long.Parse(_T("INSERT_CREATIVE_WRAPPER_LABEL_ID_HERE"));

                // Create creative wrapper objects.
                CreativeWrapper creativeWrapper = new CreativeWrapper();
                creativeWrapper.labelId = labelId;
                creativeWrapper.ordering = CreativeWrapperOrdering.INNER;
                creativeWrapper.htmlHeader = "<b>My creative wrapper header</b>";
                creativeWrapper.htmlFooter = "<b>My creative wrapper footer</b>";

                try
                {
                    // Add creative wrapper.
                    CreativeWrapper[] creativeWrappers =
                        creativeWrapperService.createCreativeWrappers(new CreativeWrapper[]
                        {
                            creativeWrapper
                        });

                    // Display results.
                    foreach (CreativeWrapper wrapper in creativeWrappers)
                    {
                        Console.WriteLine(
                            "Creative wrapper with ID '{0}' applying to label '{1}' was " +
                            "created.", wrapper.id, wrapper.labelId);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to create creative wrappers. Exception says \"{0}\"",
                        e.Message);
                }
            }
        }
    }
}
