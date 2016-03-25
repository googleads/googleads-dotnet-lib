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
  /// This code example illustrates how to retrieve all the ad groups for a
  /// campaign. To create an ad group, run AddAdGroup.cs.
  /// </summary>
  public class GetAdGroups : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      GetAdGroups codeExample = new GetAdGroups();
      Console.WriteLine(codeExample.Description);
      try {
        long campaignId = long.Parse("INSERT_CAMPAIGN_ID_HERE");
        codeExample.Run(new AdWordsUser(), campaignId);
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
        return "This code example illustrates how to retrieve all the ad groups for a " +
            "campaign. To create an ad group, run AddAdGroup.cs.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="campaignId">Id of the campaign for which ad groups are
    /// retrieved.</param>
    public void Run(AdWordsUser user, long campaignId) {
      // Get the AdGroupService.
      AdGroupService adGroupService =
          (AdGroupService) user.GetService(AdWordsService.v201603.AdGroupService);

      // Create the selector.
      Selector selector = new Selector() {
        fields = new string[] { AdGroup.Fields.Id, AdGroup.Fields.Name },
        predicates = new Predicate[] {
          Predicate.Equals(AdGroup.Fields.CampaignId, campaignId)
        },
        paging = Paging.Default,
        ordering = new OrderBy[] {OrderBy.Asc(AdGroup.Fields.Name)}
      };

      AdGroupPage page = new AdGroupPage();

      try {
        do {
          // Get the ad groups.
          page = adGroupService.get(selector);

          // Display the results.
          if (page != null && page.entries != null) {
            int i = selector.paging.startIndex;
            foreach (AdGroup adGroup in page.entries) {
              Console.WriteLine("{0}) Ad group name is '{1}' and id is {2}.", i + 1, adGroup.name,
                  adGroup.id);
              i++;
            }
          }
          // Note: You can also use selector.paging.IncrementOffsetBy(customPageSize)
          selector.paging.IncreaseOffset();
        } while (selector.paging.startIndex < page.totalNumEntries);
        Console.WriteLine("Number of ad groups found: {0}", page.totalNumEntries);
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to retrieve ad groups.", e);
      }
    }
  }
}
