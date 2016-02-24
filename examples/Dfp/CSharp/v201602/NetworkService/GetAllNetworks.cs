// Copyright 2015, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.Dfp.Lib;
using Google.Api.Ads.Dfp.v201602;

using System;

namespace Google.Api.Ads.Dfp.Examples.CSharp.v201602 {
  /// <summary>
  /// This code example gets all networks that you have access to with the
  /// current login credentials. A networkCode should be left out for this
  /// request.
  /// </summary>
  class GetAllNetworks : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets all networks that you have access to with the current " +
            "login credentials. A networkCode should be left out for this request.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetAllNetworks();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfpUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The DFP user object running the code example.</param>
    public override void Run(DfpUser user) {
      // Get the NetworkService.
      NetworkService networkService =
          (NetworkService) user.GetService(DfpService.v201602.NetworkService);
      // Set the networkCode field to null to retrieve all networks you have
      // access to.
      networkService.RequestHeader.networkCode = null;

      try {
        // Get all networks that you have access to with the current login
        // credentials.
        Network[] networks = networkService.getAllNetworks();

        int i = 0;
        foreach (Network network in networks) {
         Console.WriteLine("{0} ) Network with network code \"{1}\" and display name \"{2}\" " +
            "was found.", i + 1, network.networkCode, network.displayName);
          i++;
        }
        Console.WriteLine("Number of networks found: {0}", i);
      } catch (Exception e) {
        Console.WriteLine("Failed to get all networks. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}
