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
using Google.Api.Ads.Dfa.v1_11;

using System;
using System.Collections.Generic;
using System.Text;
using Google.Api.Ads.Common.Util;

namespace Google.Api.Ads.Dfa.Examples.v1_11 {
  /// <summary>
  /// This code example gets the size id for a given width and height.
  /// </summary>
  class GetSize : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example gets the size id for a given width and height.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetSize();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfaUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The Dfa user object running the code example.
    /// </param>
    public override void Run(DfaUser user) {
      // Create SizeRemoteService instance.
      SizeRemoteService service = (SizeRemoteService) user.GetService(
          DfaService.v1_11.SizeRemoteService);

      int width = int.Parse(_T("INSERT_WIDTH_HERE"));
      int height = int.Parse(_T("INSERT_HEIGHT_HERE"));

      try {
        // Get size.
        Size size = service.getSize(width, height);

        // Display size id.
        Console.WriteLine("Size id for \"{0}\" width and \"{1}\" height is \"{2}\".", size.width,
            size.height, size.id);
      } catch (Exception ex) {
        Console.WriteLine("Failed to retrieve size. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
