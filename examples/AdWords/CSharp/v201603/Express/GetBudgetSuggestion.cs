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

using System;
using System.Collections.Generic;
using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201603;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201603 {

  /// <summary>
  /// This code example shows how to retrieve an AdWords Express budget
  /// suggestion.
  /// </summary>
  public class GetBudgetSuggestion : ExampleBase {

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example shows how to retrieve an AdWords Express budget suggestion.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      GetBudgetSuggestion codeExample = new GetBudgetSuggestion();
      Console.WriteLine(codeExample.Description);
      try {
        codeExample.Run(new AdWordsUser());
      } catch (Exception e) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(e));
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    public void Run(AdWordsUser user) {
      // Get the BudgetSuggestionService.
      BudgetSuggestionService budgetSuggestionService = (BudgetSuggestionService)
          user.GetService(AdWordsService.v201603.BudgetSuggestionService);

      BudgetSuggestionSelector selector = new BudgetSuggestionSelector();

      List<Criterion> criteria = new List<Criterion>();

      // Criterion - Travel Agency product/service. See GetProductServices.cs for an example
      // of how to get valid product/service settings.
      ProductService productService = new ProductService();
      productService.text = "Travel Agency";
      productService.locale = "en_US";
      criteria.Add(productService);

      // Criterion - English language.
      // The ID can be found in the documentation:
      // https://developers.google.com/adwords/api/docs/appendix/languagecodes
      Language language = new Language();
      language.id = 1000L;
      criteria.Add(language);

      // Criterion - Mountain View, California location.
      // The ID can be found in the documentation:
      // https://developers.google.com/adwords/api/docs/appendix/geotargeting
      // https://developers.google.com/adwords/api/docs/appendix/cities-DMAregions
      Location location = new Location();
      location.id = 1014044L;
      criteria.Add(location);

      selector.criteria = criteria.ToArray();

      try {
        BudgetSuggestion budgetSuggestion = budgetSuggestionService.get(selector);

        Console.WriteLine("Budget suggestion for criteria is:\n" +
            "  SuggestedBudget={0}\n" +
            "  Min/MaxBudget={1}/{2}\n" +
            "  Min/MaxCpc={3}/{4}\n" +
            "  CPM={5}\n" +
            "  CPC={6}\n" +
            "  Impressions={7}\n",
            budgetSuggestion.suggestedBudget.microAmount,
            budgetSuggestion.minBudget.microAmount, budgetSuggestion.maxBudget.microAmount,
            budgetSuggestion.minCpc.microAmount, budgetSuggestion.maxCpc.microAmount,
            budgetSuggestion.cpm.microAmount,
            budgetSuggestion.cpc.microAmount,
            budgetSuggestion.impressions);
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to get budget suggestion.", e);
      }
    }
  }
}