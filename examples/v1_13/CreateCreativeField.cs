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
using Google.Api.Ads.Dfa.v1_13;

using System;
using System.Collections.Generic;
using System.Text;
using Google.Api.Ads.Common.Util;

namespace Google.Api.Ads.Dfa.Examples.v1_13 {
  /// <summary>
  /// This code example creates a creative field associated with a given
  /// advertiser. To get an advertiser id, run GetAdvertisers.cs.
  ///
  /// Tags: creativefield.saveCreativeField
  /// </summary>
  class CreateCreativeField : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example creates a creative field associated with a given advertiser. " +
            "To get an advertiser id, run GetAdvertisers.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new CreateCreativeField();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfaUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The Dfa user object running the code example.
    /// </param>
    public override void Run(DfaUser user) {
      // Create CreativeFieldRemoteService instance.
      CreativeFieldRemoteService service = (CreativeFieldRemoteService) user.GetService(
          DfaService.v1_12.CreativeFieldRemoteService);

      long advertiserId = long.Parse(_T("INSERT_ADVERTISER_ID_HERE"));
      string creativeFieldName = _T("INSERT_CREATIVE_FIELD_NAME_HERE");

      // Create creative field structure.
      CreativeField creativeField = new CreativeField();
      creativeField.id = -1;
      creativeField.name = creativeFieldName;
      creativeField.advertiserId = advertiserId;

      try {
        // Create creative field.
        CreativeFieldSaveResult creativeFieldSaveResult = service.saveCreativeField(creativeField);

        // Display creative field id.
        Console.WriteLine("Creative field with id \"{0}\" was created.",
            creativeFieldSaveResult.id);
      } catch (Exception ex) {
        Console.WriteLine("Failed to add creative field. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
