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
using Google.Api.Ads.AdWords.v201101;

using System;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201101 {
  /// <summary>
  /// This code example shows how to retrieve all Ad Extensions in a Campaign.
  /// To create a Campaign Ad Extension, run AddCampaignAdExtensionOverride.cs.
  ///
  /// Tags: CampaignAdExtensionService.get
  /// </summary>
  class GetAllCampaignAdExtensions : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example shows how to retrieve all Ad Extensions in a campaign. " +
            "To create a Campaign Ad Extension, run AddCampaignAdExtensionOverride.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetAllCampaignAdExtensions();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the CampaignAdExtensionService.
      CampaignAdExtensionService campaignExtensionService =
          (CampaignAdExtensionService) user.GetService(AdWordsService.v201101.
          CampaignAdExtensionService);

      long campaignId = long.Parse(_T("INSERT_CAMPAIGN_ID_HERE"));

      // Create a selector.
      Selector selector = new Selector();
      selector.fields = new string[] {"AdExtensionId", "Status"};

      // Create the filtering conditions.
      Predicate campaignPredicate = new Predicate();
      campaignPredicate.@operator = PredicateOperator.EQUALS;
      campaignPredicate.field = "CampaignId";
      campaignPredicate.values = new string[] {campaignId.ToString()};

      selector.predicates = new Predicate[] {campaignPredicate};

      // Specify the sort order.
      OrderBy orderBy = new OrderBy();
      orderBy.field = "AdExtensionId";
      orderBy.sortOrder = SortOrder.ASCENDING;
      selector.ordering = new OrderBy[] {orderBy};

      try {
        CampaignAdExtensionPage page = campaignExtensionService.get(selector);
        if (page != null && page.entries != null) {
          Console.WriteLine("Retrieved {0} out of {1} entries.", page.entries.Length,
              page.totalNumEntries);
          foreach (CampaignAdExtension campaignExtension in page.entries) {
            Console.WriteLine("Campaign ad extension id is \"{0}\" and status is  \"{1}\"",
                campaignExtension.adExtension.id, campaignExtension.status);
          }
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to retrieve campaign ad extensions. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
