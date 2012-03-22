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
using Google.Api.Ads.AdWords.v13;
using Google.Api.Ads.AdWords.v201109;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;


namespace Google.Api.Ads.AdWords.Tests {
  /// <summary>
  /// Base class for all test suites.
  /// </summary>
  public class BaseTests {
    /// <summary>
    /// The AdWords user to be used for tests.
    /// </summary>
    protected AdWordsUser user = new AdWordsUser();

    /// <summary>
    /// Default public constructor.
    /// </summary>
    public BaseTests() {
      (user.Config as AdWordsAppConfig).ClientCustomerId = null;
      (user.Config as AdWordsAppConfig).ClientEmail = null;
      CampaignService campaignService = (CampaignService) user.GetService(
          AdWordsService.v201109.CampaignService);
      ServicedAccountService servicedAccountService =
          (ServicedAccountService) user.GetService(AdWordsService.v201109.
              ServicedAccountService);

      // Create a selector.
      Selector selector = new Selector();
      selector.fields = new string[] {"Id", "Name", "Status"};

      // Initialize the sandbox by making a call to CampaignService.get.
      CampaignPage page = campaignService.get(selector);

      // Get the client customer ids.
      ServicedAccountSelector serviceSelector = new ServicedAccountSelector();
      serviceSelector.enablePaging = false;

      ServicedAccountGraph graph = servicedAccountService.get(serviceSelector);

      if (graph != null && graph.accounts != null) {
        for (int i = 0; i < graph.accounts.Length; i++) {
          if (graph.accounts[i].canManageClients == false) {
            (user.Config as AdWordsAppConfig).ClientCustomerId =
                graph.accounts[i].customerId.ToString();
            break;
          }
        }
      }
      Assert.NotNull((user.Config as AdWordsAppConfig).ClientCustomerId);
      Assert.Null((user.Config as AdWordsAppConfig).ClientEmail);
    }
  }
}
