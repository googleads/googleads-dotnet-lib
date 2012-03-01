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

using Attribute = Google.Api.Ads.AdWords.v201109.Attribute;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201109 {
  /// <summary>
  /// This code example reads all the keyword opportunities for the account.
  ///
  /// Tags: BulkOpportunityService.get
  /// </summary>
  class GetKeywordOpportunities : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      ExampleBase codeExample = new GetKeywordOpportunities();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser(), codeExample.GetParameters(), Console.Out);
    }

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example reads all the keyword opportunities for the account.";
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
      // Get the BulkOpportunityService.
      BulkOpportunityService bulkOpportunityService =
          (BulkOpportunityService) user.GetService(AdWordsService.v201109.
              BulkOpportunityService);

      // Create the selector.
      BulkOpportunitySelector selector = new BulkOpportunitySelector();
      selector.requestedAttributeTypes = new OpportunityAttributeType[] {
        OpportunityAttributeType.ADGROUP_ID, OpportunityAttributeType.AVERAGE_MONTHLY_SEARCHES,
        OpportunityAttributeType.CAMPAIGN_ID, OpportunityAttributeType.IDEA_TYPE,
        OpportunityAttributeType.KEYWORD
      };
      selector.ideaTypes = new OpportunityIdeaType[] {OpportunityIdeaType.KEYWORD};

      // Set selector paging.
      selector.paging = new Paging();

      int offset = 0;
      int pageSize = 500;

      BulkOpportunityPage page = new BulkOpportunityPage();

      try {
        do {
          selector.paging.startIndex = offset;
          selector.paging.numberResults = pageSize;

          // Get keyword opportunities.
          page = bulkOpportunityService.get(selector);

          // Display the results.
          if (page != null && page.entries != null) {
            int i = offset;
            foreach (Opportunity opportunity in page.entries) {
              foreach (OpportunityIdea idea in opportunity.opportunityIdeas) {
                Dictionary<OpportunityAttributeType, Attribute> data =
                    new Dictionary<OpportunityAttributeType, Attribute>();
                foreach (OpportunityAttribute_AttributeMapEntry dataItem in idea.data) {
                  data[dataItem.key] = dataItem.value;
                }

                long campaignId =
                    (data[OpportunityAttributeType.CAMPAIGN_ID] as LongAttribute).value;
                long adGroupId =
                    (data[OpportunityAttributeType.CAMPAIGN_ID] as LongAttribute).value;
                writer.WriteLine("{0}) Opportunities found for campaign id '{1}' and " +
                    "ad group id '{2}':", campaignId, adGroupId);
                Keyword keyword =
                    (data[OpportunityAttributeType.KEYWORD] as KeywordAttribute).value;
                long averageMonthlySearches =
                    (data[OpportunityAttributeType.AVERAGE_MONTHLY_SEARCHES] as IntegerAttribute).
                        value;
                writer.WriteLine("\tKeyword opportunity with text '{0}', match type '{1}', and " +
                    "average monthly search volume '{2}' was found.", keyword.text,
                    keyword.matchType, averageMonthlySearches);
              }
              i++;
            }
          }
          offset += pageSize;
        } while (offset < page.totalNumEntries);
        writer.WriteLine("Number of keyword opportunities found: {0}", page.totalNumEntries);
      } catch (Exception ex) {
        writer.WriteLine("Failed to get keyword opportunities. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
