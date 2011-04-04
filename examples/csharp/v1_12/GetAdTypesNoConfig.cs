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
using Google.Api.Ads.Dfa.v1_12;

using System;
using System.Collections.Generic;
using System.Text;
using Google.Api.Ads.Common.Util;

namespace Google.Api.Ads.Dfa.Examples.CSharp.v1_12 {
  /// <summary>
  /// This code example retrieves available ad types and displays the name and
  /// id for each type. This also shows how you can set the DfaUser
  /// configuration at runtime instead of reading the settings from app.config.
  /// To get an authToken, run Authenticate.cs.
  ///
  /// </summary>
  class GetAdTypesNoConfig : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example retrieves available ad types and displays the name and id for " +
            "each type. This also shows how you can set the DfaUser configuration at runtime " +
            "instead of reading the settings from app.config.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetAdTypesNoConfig();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfaUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The Dfa user object running the code example.
    /// </param>
    public override void Run(DfaUser user) {
      Dictionary<string, string> headers = new Dictionary<string,string>();

      string userName = _T("INSERT_USERNAME_HERE");
      string password = _T("INSERT_PASSWORD_HERE");
      string applicationName = _T("INSERT_APPLICATION_NAME_HERE");

      headers.Add("userName", userName);
      headers.Add("password", password);
      headers.Add("applicationName", applicationName);

      // If you have already used LoginRemoteService to obtain your token, then
      // you can set the authToken header instead of the password header like:
      //     string authToken = _T("INSERT_AUTH_TOKEN_HERE");
      //     headers.Add("authToken", authToken);

      // Since we are creating a custom DfaUser, we don't use the user passed
      // as a method argument.
      DfaUser user1 = new DfaUser(headers);

      // Create AdRemoteService instance.
      AdRemoteService service = (AdRemoteService) user1.GetService(
          DfaService.v1_12.AdRemoteService);

      try {
        // Get ad types.
        AdType[] adTypes = service.getAdTypes();

        // Display ad type and its id.
        foreach (AdType result in adTypes) {
          Console.WriteLine("Ad type with name \"{0} and id \"{1}\" was found.", result.name,
              result.id);
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to retrieve ad types. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
