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
  /// This code example creates a test network. You do not need to have a DFP
  /// account to run this example, but you do need to have a new Google account
  /// (created at http://www.google.com/accounts/newaccount) that is not
  /// associated with any other DFP networks (including old sandbox networks).
  /// Once this network is created, you can supply the network code in your
  /// settings to make calls to other services.
  ///
  /// Please see the following URL for more information:
  /// https://developers.google.com/doubleclick-publishers/docs/signup
  /// </summary>
  class MakeTestNetwork : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example creates a test network. You do not need to have a DFP" +
            " account to run this example, but you do need to have a new Google account" +
            " (created at http://www.google.com/accounts/newaccount) that is not associated " +
            "with any other DFP networks (including old sandbox networks). Once this network " +
            "is created, you can supply the network code in your settings to make calls to " +
            "other services.\n\n Please see the following URL for more information:" +
            " https://developers.google.com/doubleclick-publishers/docs/signup";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new MakeTestNetwork();
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
      // Set the networkCode field to null.
      networkService.RequestHeader.networkCode = null;

      try {
        Network network = networkService.makeTestNetwork();

        Console.WriteLine("Test network with network code \"{0}\" and display name \"{1}\" " +
            "created.\nYou may now sign in at http://www.google.com/dfp/main?networkCode={0}",
            network.networkCode, network.displayName);
      } catch (Exception e) {
        Console.WriteLine("Failed to make test network. Exception says \"{0}\"",
            e.Message);
      }
    }
  }
}
