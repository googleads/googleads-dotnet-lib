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
  /// This code example retrieves keywords that are related to a given keyword.
  ///
  /// Tags: TargetingIdeaService.get
  /// </summary>
  class GetKeywordIdeas : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      ExampleBase codeExample = new GetKeywordIdeas();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser(), codeExample.GetParameters(), Console.Out);
    }

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example retrieves keywords that are related to a given keyword.";
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
      // Get the TargetingIdeaService.
      TargetingIdeaService targetingIdeaService =
          (TargetingIdeaService) user.GetService(AdWordsService.v201109.TargetingIdeaService);

      string keywordText = "mars cruise";

      // Create seed keyword.
      Keyword keyword = new Keyword();
      keyword.text = keywordText;
      keyword.matchType = KeywordMatchType.BROAD;

      // Create the selector.
      TargetingIdeaSelector selector = new TargetingIdeaSelector();
      selector.requestType = RequestType.IDEAS;
      selector.ideaType = IdeaType.KEYWORD;
      selector.requestedAttributeTypes = new AttributeType[] {AttributeType.CRITERION,
          AttributeType.AVERAGE_TARGETED_MONTHLY_SEARCHES};

      // Create related to keyword search parameter.
      RelatedToKeywordSearchParameter relatedToKeywordSearchParameter =
          new RelatedToKeywordSearchParameter();
      relatedToKeywordSearchParameter.keywords = new Keyword[] {keyword};

      // Create keyword match type search parameter to ensure unique results.
      KeywordMatchTypeSearchParameter keywordMatchTypeSearchParameter =
          new KeywordMatchTypeSearchParameter();
      keywordMatchTypeSearchParameter.keywordMatchTypes = new KeywordMatchType[] {
          KeywordMatchType.BROAD};

      selector.searchParameters =
          new SearchParameter[] {relatedToKeywordSearchParameter, keywordMatchTypeSearchParameter};

      // Set selector paging.
      selector.paging = new Paging();

      int offset = 0;
      int pageSize = 500;

      TargetingIdeaPage page = new TargetingIdeaPage();

      try {
        do {
          selector.paging.startIndex = offset;
          selector.paging.numberResults = pageSize;

          // Get related keywords.
          page = targetingIdeaService.get(selector);

          // Display the results.
          if (page != null && page.entries != null) {
            int i = offset;
            foreach (TargetingIdea idea in page.entries) {
              foreach (Type_AttributeMapEntry entry in idea.data) {
                if (entry.key == AttributeType.CRITERION) {
                  CriterionAttribute kwdAttribute = entry.value as CriterionAttribute;
                  Keyword relatedKeyword = (Keyword) kwdAttribute.value;
                  writer.WriteLine("{0}) Related keyword with text = '{0}' and match type = " +
                      "'{1}' was found.", relatedKeyword.text, relatedKeyword.matchType);
                }
              }
            }
            i++;
          }
          offset += pageSize;
        } while (offset < page.totalNumEntries);
        writer.WriteLine("Number of related keywords found: {0}", page.totalNumEntries);
      } catch (Exception ex) {
        writer.WriteLine("Failed to retrieve related keywords. Exception says \"{0}\"",
            ex.Message);
      }
    }
  }
}
