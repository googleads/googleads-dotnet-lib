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
using Google.Api.Ads.Dfa.v1_14;

using System;
using System.Collections.Generic;
using System.Text;
using Google.Api.Ads.Common.Util;

namespace Google.Api.Ads.Dfa.Examples.CSharp.v1_14 {
  /// <summary>
  /// This code example creates a creative group associated with a given
  /// advertiser. To get an advertiser id, run getAdvertisers.cs. Valid group
  /// numbers are limited to 1 or 2.
  ///
  /// Tags: creativegroup.saveCreativeGroup
  /// </summary>
  class CreateCreativeGroup : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example creates a creative group associated with a given advertiser. " +
            "To get an advertiser id, run getAdvertisers.cs. Valid group numbers are limited to" +
            " 1 or 2.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new CreateCreativeGroup();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfaUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The Dfa user object running the code example.
    /// </param>
    public override void Run(DfaUser user) {
      // Create CreativeGroupRemoteService instance.
      CreativeGroupRemoteService service = (CreativeGroupRemoteService) user.GetService(
          DfaService.v1_14.CreativeGroupRemoteService);

      long advertiserId = long.Parse(_T("INSERT_ADVERTISER_ID_HERE"));
      int groupNumber = int.Parse(_T("INSERT_GROUP_NUMBER_HERE"));
      string creativeGroupName = _T("INSERT_CREATIVE_GROUP_NAME_HERE");

      // Create creative group structure.
      CreativeGroup creativeGroup = new CreativeGroup();
      creativeGroup.id = -1;
      creativeGroup.name = creativeGroupName;
      creativeGroup.groupNumber = groupNumber;
      creativeGroup.advertiserId = advertiserId;

      try {
        // Create creative group.
        CreativeGroupSaveResult creativeGroupSaveResult = service.saveCreativeGroup(creativeGroup);

        // Display new creative group id.
        Console.WriteLine("Creative group with id \"{0}\" was created.",
            creativeGroupSaveResult.id);
      } catch (Exception ex) {
        Console.WriteLine("Failed to create creative group. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
