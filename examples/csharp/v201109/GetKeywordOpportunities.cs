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
using System.Text;
using Attribute = Google.Api.Ads.AdWords.v201109.Attribute;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201109 {
  /// <summary>
  /// This code example reads all the keyword opportunities for the account.
  ///
  /// Tags: BulkOpportunityService.get
  /// </summary>
  class GetKeywordOpportunities : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example reads all the keyword opportunities for the account.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetKeywordOpportunities();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the BulkOpportunityService.
      BulkOpportunityService bulkOpportunityService =
          (BulkOpportunityService)user.GetService(AdWordsService.v201109.
              BulkOpportunityService);

      // Create selector.
      BulkOpportunitySelector selector = new BulkOpportunitySelector();
      selector.requestedAttributeTypes = new OpportunityAttributeType[] {
        OpportunityAttributeType.ADGROUP_ID, OpportunityAttributeType.AVERAGE_MONTHLY_SEARCHES,
        OpportunityAttributeType.CAMPAIGN_ID, OpportunityAttributeType.IDEA_TYPE,
        OpportunityAttributeType.KEYWORD
      };
      selector.ideaTypes = new OpportunityIdeaType[] {OpportunityIdeaType.KEYWORD};

      // Set selector paging.
      Paging paging = new Paging();
      paging.startIndex = 0;
      paging.numberResults = 10;

      selector.paging = paging;

      try {
        // Get keyword opportunities.
        BulkOpportunityPage page = bulkOpportunityService.get(selector);

        // Display keyword opportunities.
        if (page != null && page.entries != null && page.entries.Length > 0) {
          foreach (Opportunity opportunity in page.entries) {
            foreach (OpportunityIdea idea in opportunity.opportunityIdeas) {
              Dictionary<OpportunityAttributeType, Attribute> data =
                  new Dictionary<OpportunityAttributeType, Attribute>();
              foreach (OpportunityAttribute_AttributeMapEntry dataItem in idea.data) {
                data[dataItem.key] = dataItem.value;
              }

              long campaignId =
                  (data[OpportunityAttributeType.CAMPAIGN_ID] as LongAttribute).value;
              long adGroupId = (data[OpportunityAttributeType.CAMPAIGN_ID] as LongAttribute).value;
              Console.WriteLine("Opportunities found for campaign id '{0}' and ad group id '{1}':",
                  campaignId, adGroupId);
              Keyword keyword =
                  (data[OpportunityAttributeType.KEYWORD] as KeywordAttribute).value;
              long averageMonthlySearches =
                  (data[OpportunityAttributeType.AVERAGE_MONTHLY_SEARCHES] as IntegerAttribute).
                      value;
              Console.WriteLine("\tKeyword opportunity with text '{0}', match type '{1}', and " +
                  "average monthly search volume '{2}' was found.", keyword.text,
                  keyword.matchType, averageMonthlySearches);
            }
          }
        } else {
           Console.WriteLine("No keyword opportunities were found.");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to get keyword opportunities. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
