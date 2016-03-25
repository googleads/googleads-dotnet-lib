// Copyright 2016, Google Inc. All Rights Reserved.
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

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201603;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201603 {
  /// <summary>
  /// This code example lists all campaigns. To add a campaign, run
  /// AddCampaign.cs.
  /// </summary>
  public class GetCampaigns : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      GetCampaigns codeExample = new GetCampaigns();
      Console.WriteLine(codeExample.Description);
      try {
        codeExample.Run(new AdWordsUser());
      } catch (Exception e) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(e));
      }
    }

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example lists all campaigns. To add a campaign, run AddCampaign.cs.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    public void Run(AdWordsUser user) {
      // Get the CampaignService.
      CampaignService campaignService =
          (CampaignService) user.GetService(AdWordsService.v201603.CampaignService);

      // Create the selector.
      Selector selector = new Selector() {
        fields = new string[] {
          Campaign.Fields.Id, Campaign.Fields.Name, Campaign.Fields.Status
        },
        paging = Paging.Default
      };

      CampaignPage page = new CampaignPage();

      try {
        do {
          // Get the campaigns.
          page = campaignService.get(selector);

          // Display the results.
          if (page != null && page.entries != null) {
            int i = selector.paging.startIndex;
            foreach (Campaign campaign in page.entries) {
              Console.WriteLine("{0}) Campaign with id = '{1}', name = '{2}' and status = '{3}'" +
                " was found.", i + 1, campaign.id, campaign.name, campaign.status);
              i++;
            }
          }
          selector.paging.IncreaseOffset();
        } while (selector.paging.startIndex < page.totalNumEntries);
        Console.WriteLine("Number of campaigns found: {0}", page.totalNumEntries);
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to retrieve campaigns", e);
      }
    }
  }
}
