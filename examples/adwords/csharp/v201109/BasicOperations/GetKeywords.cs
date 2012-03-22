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

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201109 {
  /// <summary>
  /// This code example gets all keywords in an account. To add keywords, run
  /// AddKeywords.cs.
  ///
  /// Tags: AdGroupCriterionService.get
  /// </summary>
  public class GetKeywords : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      ExampleBase codeExample = new GetKeywords();
      Console.WriteLine(codeExample.Description);
      try {
        codeExample.Run(new AdWordsUser(), codeExample.GetParameters(), Console.Out);
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
        return "This code example gets all keywords in an account. To add keywords, run " +
            "AddKeywords.cs.";
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
      // Get the AdGroupCriterionService.
      AdGroupCriterionService adGroupCriterionService =
          (AdGroupCriterionService) user.GetService(AdWordsService.v201109.AdGroupCriterionService);

      // Create a selector.
      Selector selector = new Selector();
      selector.fields = new string[] {"Id", "AdGroupId", "KeywordText"};

      // Select only keywords.
      Predicate predicate = new Predicate();
      predicate.field = "CriteriaType";
      predicate.@operator = PredicateOperator.EQUALS;
      predicate.values = new string[] {"KEYWORD"};
      selector.predicates = new Predicate[] {predicate};

      // Set the selector paging.
      selector.paging = new Paging();

      int offset = 0;
      int pageSize = 500;

      AdGroupCriterionPage page = new AdGroupCriterionPage();

      try {
        do {
          selector.paging.startIndex = offset;
          selector.paging.numberResults = pageSize;

          // Get the keywords.
          page = adGroupCriterionService.get(selector);

          // Display the results.
          if (page != null && page.entries != null) {
            int i = offset;

            foreach (AdGroupCriterion adGroupCriterion in page.entries) {
              bool isNegative = (adGroupCriterion is NegativeAdGroupCriterion);

              // If you are retrieving multiple type of criteria, then you may
              // need to check for
              //
              // if (adGroupCriterion is Keyword) { ... }
              //
              // to identify the criterion type.
              Keyword keyword = (Keyword) adGroupCriterion.criterion;
              if (isNegative) {
                writer.WriteLine("{0}) Negative keyword with ad group ID = '{1}', keyword ID " +
                    "= '{2}', and text = '{3}' was found.", i + 1, adGroupCriterion.adGroupId,
                    keyword.id, keyword.text);
              } else {
                writer.WriteLine("{0}) Keyword with ad group ID = '{1}', keyword ID = '{2}', " +
                    "text = '{3}' and matchType = '{4} was found.", i + 1,
                    adGroupCriterion.adGroupId, keyword.id, keyword.text, keyword.matchType);
              }
              i++;
            }
          }
          offset += pageSize;
        } while (offset < page.totalNumEntries);
        writer.WriteLine("Number of keywords found: {0}", page.totalNumEntries);
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to retrieve keywords.", ex);
      }
    }
  }
}
