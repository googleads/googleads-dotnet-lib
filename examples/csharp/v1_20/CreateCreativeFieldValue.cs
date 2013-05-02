// Copyright 2013, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.Dfa.v1_20;

using System;
using System.Collections.Generic;
using System.Text;
using Google.Api.Ads.Common.Util;

namespace Google.Api.Ads.Dfa.Examples.CSharp.v1_20 {
  /// <summary>
  /// This code example creates a creative field value associated with a given
  /// creative field. To get the creative field id, run GetCreativeFields.cs.
  ///
  /// Tags: creativefield.saveCreativeFieldValue
  /// </summary>
  class CreateCreativeFieldValue : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example creates a creative field value associated with a given " +
            "creative field. To get the creative field id, run GetCreativeFields.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new CreateCreativeFieldValue();
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
          DfaService.v1_20.CreativeFieldRemoteService);

      long creativeFieldId = long.Parse(_T("INSERT_CREATIVE_FIELD_ID_HERE"));
      String creativeFieldValueName = _T("INSERT_CREATIVE_FIELD_VALUE_NAME_HERE");

      // Create creative field value structure.
      CreativeFieldValue creativeFieldValue = new CreativeFieldValue();
      creativeFieldValue.id = -1;
      creativeFieldValue.name = creativeFieldValueName;
      creativeFieldValue.creativeFieldId = creativeFieldId;

      try {
        // Create creative field value.
        CreativeFieldValueSaveResult creativeFieldValueSaveResult =
            service.saveCreativeFieldValue(creativeFieldValue);

        // Display creative field value id.
        Console.WriteLine("Creative field value with id \"{0}\" was created.",
            creativeFieldValueSaveResult.id);
      } catch (Exception ex) {
        Console.WriteLine("Failed to add creative field value. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
