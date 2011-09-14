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
using Google.Api.Ads.Dfa.v1_15;

using System;
using System.Collections.Generic;
using System.Text;
using Google.Api.Ads.Common.Util;

namespace Google.Api.Ads.Dfa.Examples.CSharp.v1_15 {
  /// <summary>
  /// This code example authenticates using your DFA user name and password,
  /// and displays the user profile token, DFA account name and id. If you are
  /// setting username and password in your application's app.config and
  /// creating a DfaUser using the default constructor, then you don't need to
  /// obtain an authToken; the DfaUser does it for you at runtime based on your
  /// app.config settings. If you wish to set authToken in your app.config or
  /// create DfaUser using custom configuration, then you can run this example
  /// to obtain an authToken. See GetAdTypesNoConfig.cs to see how to construct
  /// a DfaUser with custom configuration.
  ///
  /// Tags: login.authenticate
  /// </summary>
  class Authenticate : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example authenticates using your DFA user name and password, and" +
            " displays the user profile token, DFA account name and id. If you are setting" +
            " username and password in your application's app.config and creating a DfaUser" +
            " using the default constructor, then you don't need to obtain an authToken; the" +
            " DfaUser does it for you at runtime based on your app.config settings. If you wish" +
            " to set authToken in your app.config or create DfaUser using custom configuration," +
            " then you can run this example to obtain an authToken. See GetAdTypesNoConfig.cs" +
            " to see how to construct a DfaUser with custom configuration.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new Authenticate();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new DfaUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The Dfa user object running the code example.
    /// </param>
    public override void Run(DfaUser user) {
      // Create LoginRemoteService instance.
      LoginRemoteService service = (LoginRemoteService) user.GetService(
          DfaService.v1_15.LoginRemoteService);

      // Provide user name and password.
      String username = _T("INSERT_USER_NAME_HERE");
      String password = _T("INSERT_PASSWORD_HERE");

      try {
        // Authenticate.
        UserProfile userProfile = service.authenticate(username, password);

        // Display user profile token, DFA account name and id.
        Console.WriteLine("User profile token is \"{0}\", DFA account name is \"{1}\", and " +
            "DFA account id is \"{2}\".", userProfile.token, userProfile.networkName,
            userProfile.networkId);
      } catch (Exception ex) {
        Console.WriteLine("Failed to authenticate user. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
