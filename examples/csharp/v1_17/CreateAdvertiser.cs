// Copyright 2011, Google Inc. All Rights Reserved.
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

// Author: api.anash@gmail.com (Anash P. Oommen)

using Google.Api.Ads.Dfa.Lib;
using Google.Api.Ads.Dfa.v1_17;

using System;
using System.Collections.Generic;
using System.Text;
using Google.Api.Ads.Common.Util;

namespace Google.Api.Ads.Dfa.Examples.CSharp.v1_17 {
  /// <summary>
  /// This code example creates an advertiser in a given DFA network. To get
  /// the network id, run Authenticate.cs.
  ///
  /// Tags: advertiser.saveAdvertiser
  /// </summary>
  class CreateAdvertiser : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example creates an advertiser in a given DFA network. To get the " +
            "network id, run Authenticate.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new CreateAdvertiser();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfaUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The Dfa user object running the code example.
    /// </param>
    public override void Run(DfaUser user) {
      // Create AdvertiserRemoteService instance.
      AdvertiserRemoteService service = (AdvertiserRemoteService) user.GetService(
          DfaService.v1_17.AdvertiserRemoteService);

      long networkId = long.Parse(_T("INSERT_NETWORK_ID_HERE"));
      string advertiserName = _T("INSERT_ADVERTISER_NAME_HERE");

      // Create advertiser structure.
      Advertiser advertiser = new Advertiser();
      advertiser.name = advertiserName;
      advertiser.networkId = networkId;
      advertiser.id = 0;
      advertiser.advertiserGroupId = 0;
      advertiser.approved = true;

      try {
        // Create advertiser.
        AdvertiserSaveResult result = service.saveAdvertiser(advertiser);
        if (result != null) {
          // Display advertiser id.
          Console.WriteLine("Advertiser with id \"{0}\" was created.", result.id);
        } else {
          Console.WriteLine("Could not create advertiser for the specified network id.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to create advertiser. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
