// Copyright 2010, Google Inc. All Rights Reserved.
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

using com.google.api.adwords.lib;
using com.google.api.adwords.v201008;

using System;
using System.Collections.Generic;
using System.Text;

namespace com.google.api.adwords.examples.v201008 {
  /// <summary>
  /// This code example illustrates how to retrieve the account hierarchy under
  /// an account.
  ///
  /// Tags: ServicedAccountService.get
  /// </summary>
  class GetAccountHierarchy : SampleBase {
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
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the ServicedAccountService.
      ServicedAccountService servicedAccountService =
          (ServicedAccountService) user.GetService(AdWordsService.v201008.
              ServicedAccountService);

      ServicedAccountSelector selector = new ServicedAccountSelector();
      selector.serviceTypes = new ServiceType[] {ServiceType.UI_AND_API,
          ServiceType.API_ONLY};
      selector.enablePaging = false;
      selector.enablePagingSpecified = true;

      try {
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
            Console.WriteLine("There is a {0} link of type {1} from " +
                "{2:###-###-####} to {3:###-###-####}", link.typeOfLink,
                link.serviceType, link.managerId.id, link.clientId.id);
          }
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to retrieve accounts. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
