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
using Google.Api.Ads.AdManager.v201808;

using System;

namespace Google.Api.Ads.AdManager.Examples.CSharp.v201808
{
    /// <summary>
    /// This code example creates a test network. You do not need to have an Ad Manager
    /// account to run this example, but you do need to have a new Google account
    /// (created at http://www.google.com/accounts/newaccount) that is not
    /// associated with any other Ad Manager networks (including old sandbox networks).
    /// Once this network is created, you can supply the network code in your
    /// settings to make calls to other services.
    ///
    /// Please see the following URL for more information:
    /// https://developers.google.com/ad-manager/docs/signup
    /// </summary>
    public class MakeTestNetwork : SampleBase
    {
        /// <summary>
        /// Returns a description about the code example.
        /// </summary>
        public override string Description
        {
            get
            {
                return "This code example creates a test network. You do not need to have an " +
                    "Ad Manager account to run this example, but you do need to have a new " +
                    "Google account (created at http://www.google.com/accounts/newaccount) that " +
                    "is not associated with any other Ad Manager networks (including old sandbox " +
                    "networks). Once this network is created, you can supply the network code in " +
                    "your settings to make calls to other services.\n\n Please see the following " +
                    "URL for more information: " +
                    "https://developers.google.com/ad-manager/docs/signup";
            }
        }

        /// <summary>
        /// Main method, to run this code example as a standalone application.
        /// </summary>
        public static void Main()
        {
            MakeTestNetwork codeExample = new MakeTestNetwork();
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
                // Set the networkCode field to null.
                networkService.RequestHeader.networkCode = null;

                try
                {
                    Network network = networkService.makeTestNetwork();

                    Console.WriteLine(
                        "Test network with network code \"{0}\" and display name \"{1}\" " +
                        "created.\nYou may now sign in at " +
                        "http://www.google.com/dfp/main?networkCode={0}",
                        network.networkCode, network.displayName);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to make test network. Exception says \"{0}\"",
                        e.Message);
                }
            }
        }
    }
}
