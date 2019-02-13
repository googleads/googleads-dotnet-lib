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
    /// This code example gets the current network that you can make requests
    /// against.
    /// </summary>
    public class GetCurrentNetwork : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example gets the current network that you can make requests " +
                    "against.";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            GetCurrentNetwork codeExample = new GetCurrentNetwork();
            Console.WriteLine(codeExample.Description);
            codeExample.Run(new AdManagerUser());
        }

        /// <summary>
        /// Run the code example.
        /// </summary>
        public void Run(AdManagerUser user)
        {
            using (NetworkService networkService = user.GetService<NetworkService>())
            {
                try
                {
                    // Get the current network.
                    Network network = networkService.getCurrentNetwork();

                    Console.WriteLine(
                        "Current network has network code \"{0}\" and display name \"{1}\".",
                        network.networkCode, network.displayName);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to get current network. Exception says \"{0}\"",
                        e.Message);
                }
            }
        }
    }
}
