// Copyright 2012, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdWords.v201109_1;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201109_1 {
  /// <summary>
  /// This code example illustrates how to retrieve the account hierarchy under
  /// an account.
  ///
  /// Tags: ServicedAccountService.get
  /// </summary>
  public class GetAccountHierarchy : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      GetAccountHierarchy codeExample = new GetAccountHierarchy();
      Console.WriteLine(codeExample.Description);
      try {
        codeExample.Run(new AdWordsUser());
      } catch (Exception ex) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(ex));
      }
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
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    public void Run(AdWordsUser user) {
      // Get the ServicedAccountService.
      ServicedAccountService servicedAccountService =
          (ServicedAccountService) user.GetService(AdWordsService.v201109_1.
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
          Console.WriteLine("There are {0} customers under this account hierarchy.",
              graph.accounts.Length);

          for (int i = 0; i < graph.accounts.Length; i++) {
            Console.WriteLine("{0}) Customer id: {1:###-###-####}\nLogin email: " +
                "{2}\nCompany name: {3}\nIsMCC: {4}\n", i + 1, graph.accounts[i].customerId,
                graph.accounts[i].login, graph.accounts[i].companyName,
                graph.accounts[i].canManageClients);
          }

          // Display the links.
          foreach (Link link in graph.links) {
            Console.WriteLine("There is a {0} link from {1:###-###-####} to {2:###-###-####}",
                link.typeOfLink, link.managerId.id, link.clientId.id);
          }
        } else {
          Console.WriteLine("No accounts were retrieved.");
        }
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to retrieve accounts.", ex);
      }
    }
  }
}
