// Copyright 2014, Google Inc. All Rights Reserved.
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

using System;
using System.Collections.Generic;
using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201406;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201406 {

  /// <summary>
  /// This code example gets all campaigns with a specific label. To add a
  /// label to campaigns, run AddCampaignLabels.cs.
  ///
  /// Tags: CampaignService.get
  /// </summary>
  public class GetCampaignsByLabel : ExampleBase {

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      GetCampaignsByLabel codeExample = new GetCampaignsByLabel();
      Console.WriteLine(codeExample.Description);
      try {
        long labelId = long.Parse("INSERT_LABEL_ID_HERE");
        codeExample.Run(new AdWordsUser(), labelId);
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
        return "This code example gets all campaigns with a specific label. To add a label " +
            "to campaigns, run AddCampaignLabels.cs.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="labelName">ID of the label.</param>
    public void Run(AdWordsUser user, long labelId) {
      // Get the CampaignService.
      CampaignService campaignService =
          (CampaignService) user.GetService(AdWordsService.v201406.CampaignService);

      // Create the selector.
      Selector selector = new Selector();
      selector.fields = new string[] { "Id", "Name", "Labels" };

      // Labels filtering is performed by ID. You can use CONTAINS_ANY to
      // select campaigns with any of the label IDs, CONTAINS_ALL to select
      // campaigns with all of the label IDs, or CONTAINS_NONE to select
      // campaigns with none of the label IDs.
      Predicate predicate = new Predicate();
      predicate.@operator = PredicateOperator.CONTAINS_ANY;
      predicate.field = "Labels";
      predicate.values = new string[] { labelId.ToString() };
      selector.predicates = new Predicate[] { predicate };

      // Set the selector paging.
      selector.paging = new Paging();

      int offset = 0;
      int pageSize = 500;

      CampaignPage page = new CampaignPage();

      try {
        do {
          selector.paging.startIndex = offset;
          selector.paging.numberResults = pageSize;

          // Get the campaigns.
          page = campaignService.get(selector);

          // Display the results.
          if (page != null && page.entries != null) {
            int i = offset;
            foreach (Campaign campaign in page.entries) {
              List<string> labelNames = new List<string>();
              foreach (Label label in campaign.labels) {
                labelNames.Add(label.name);
              }

              Console.WriteLine("{0}) Campaign with id = '{1}', name = '{2}' and labels = '{3}'" +
                  " was found.", i + 1, campaign.id, campaign.name,
                  string.Join(", ", labelNames.ToArray()));
              i++;
            }
          }
          offset += pageSize;
        } while (offset < page.totalNumEntries);
        Console.WriteLine("Number of campaigns found: {0}", page.totalNumEntries);
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to retrieve campaigns by label", ex);
      }
    }
  }
}
