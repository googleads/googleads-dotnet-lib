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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201109;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201109 {
  /// <summary>
  /// This code example illustrates how to retrieve the account hierarchy under
  /// an account.
  ///
  /// Tags: ServicedAccountService.get
  /// </summary>
  class GetAccountHierarchy : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      ExampleBase codeExample = new GetAccountHierarchy();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser(), codeExample.GetParameters(), Console.Out);
    }

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example illustrates how to retrieve the account hierarchy under" +
            " an account.";
      }
    }

    /// <summary>
    /// Gets the list of parameter names required to run this code example.
    /// </summary>
    /// <returns>
    /// A list of parameter names for this code example.
    /// </returns>
    public override string[] GetParameterNames() {
      return new string[] {};
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="parameters">The parameters for running the code
    /// example.</param>
    /// <param name="writer">The stream writer to which script output should be
    /// written.</param>
    public override void Run(AdWordsUser user, Dictionary<string, string> parameters,
        TextWriter writer) {
      // Get the ServicedAccountService.
      ServicedAccountService servicedAccountService =
          (ServicedAccountService) user.GetService(AdWordsService.v201109.
              ServicedAccountService);

      // Create the selector.
      ServicedAccountSelector selector = new ServicedAccountSelector();
      // Disable selector paging to retrive links.
      selector.enablePaging = false;

      try {
        // Retrieve the accounts.
        ServicedAccountGraph graph = servicedAccountService.get(selector);

        if (graph != null && graph.accounts != null) {
          // Display the accounts.
          writer.WriteLine("There are {0} customers under this account hierarchy.",
              graph.accounts.Length);

          for (int i = 0; i < graph.accounts.Length; i++) {
            writer.WriteLine("{0}) Customer id: {1:###-###-####}\nLogin email: " +
                "{2}\nCompany name: {3}\nIsMCC: {4}\n", i + 1, graph.accounts[i].customerId,
                graph.accounts[i].login, graph.accounts[i].companyName,
                graph.accounts[i].canManageClients);
          }

          // Display the links.
          foreach (Link link in graph.links) {
            writer.WriteLine("There is a {0} link from {1:###-###-####} to {2:###-###-####}",
                link.typeOfLink, link.managerId.id, link.clientId.id);
          }
        } else {
          writer.WriteLine("No accounts were retrieved.");
        }
      } catch (Exception ex) {
        writer.WriteLine("Failed to retrieve accounts. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
