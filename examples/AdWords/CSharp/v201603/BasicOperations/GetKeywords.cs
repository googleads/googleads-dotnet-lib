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

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201603 {

  /// <summary>
  /// This code example gets all keywords in an ad group. To add keywords, run
  /// AddKeywords.cs.
  /// </summary>
  public class GetKeywords : ExampleBase {

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      GetKeywords codeExample = new GetKeywords();
      Console.WriteLine(codeExample.Description);
      try {
        long adGroupId = long.Parse("INSERT_ADGROUP_ID_HERE");
        codeExample.Run(new AdWordsUser(), adGroupId);
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
        return "This code example gets all keywords in an ad group. To add keywords, run " +
            "AddKeywords.cs.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="adGroupId">ID of the ad group from which keywords are
    /// retrieved.</param>
    public void Run(AdWordsUser user, long adGroupId) {
      // Get the AdGroupCriterionService.
      AdGroupCriterionService adGroupCriterionService =
          (AdGroupCriterionService) user.GetService(
              AdWordsService.v201603.AdGroupCriterionService);

      // Create a selector.
      Selector selector = new Selector() {
        fields = new string[] {
          Keyword.Fields.Id, Keyword.Fields.KeywordMatchType,
          Keyword.Fields.KeywordText, Keyword.Fields.CriteriaType
        },
        predicates = new Predicate[] {
          // Select only keywords.
          Predicate.In(Keyword.Fields.CriteriaType, new string[] {"KEYWORD"}),

          // Restrict search to an ad group.
          Predicate.Equals(AdGroupCriterion.Fields.AdGroupId, adGroupId),
        },
        ordering = new OrderBy[] {OrderBy.Asc(Keyword.Fields.KeywordText)},
        paging = Paging.Default
      };

      AdGroupCriterionPage page = new AdGroupCriterionPage();

      try {
        do {
          // Get the keywords.
          page = adGroupCriterionService.get(selector);

          // Display the results.
          if (page != null && page.entries != null) {
            int i = selector.paging.startIndex;

            foreach (AdGroupCriterion adGroupCriterion in page.entries) {
              Keyword keyword = (Keyword) adGroupCriterion.criterion;

              Console.WriteLine("{0}) Keyword with text '{1}', match type '{2}', criteria " +
                  "type '{3}', and ID {4} was found.", i + 1, keyword.text, keyword.matchType,
                  keyword.type, keyword.id);
              i++;
            }
          }
          selector.paging.IncreaseOffset();
        } while (selector.paging.startIndex < page.totalNumEntries);
        Console.WriteLine("Number of keywords found: {0}", page.totalNumEntries);
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to retrieve keywords.", e);
      }
    }
  }
}
