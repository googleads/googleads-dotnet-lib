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
using System.Text;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201603 {

  /// <summary>
  /// This code example retrieves keywords that are related to a given keyword.
  /// </summary>
  public class GetKeywordIdeas : ExampleBase {

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      GetKeywordIdeas codeExample = new GetKeywordIdeas();
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
        return "This code example retrieves keywords that are related to a given keyword.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    public void Run(AdWordsUser user) {
      // Get the TargetingIdeaService.
      TargetingIdeaService targetingIdeaService =
          (TargetingIdeaService) user.GetService(AdWordsService.v201603.TargetingIdeaService);

      // Create selector.
      TargetingIdeaSelector selector = new TargetingIdeaSelector();
      selector.requestType = RequestType.IDEAS;
      selector.ideaType = IdeaType.KEYWORD;
      selector.requestedAttributeTypes = new AttributeType[] {
        AttributeType.KEYWORD_TEXT,
        AttributeType.SEARCH_VOLUME,
        AttributeType.CATEGORY_PRODUCTS_AND_SERVICES};

      // Create the search parameters.
      string keywordText = "mars cruise";

      // Create related to query search parameter.
      RelatedToQuerySearchParameter relatedToQuerySearchParameter =
          new RelatedToQuerySearchParameter();
      relatedToQuerySearchParameter.queries = new String[] { keywordText };

      // Add a language search parameter (optional).
      // The ID can be found in the documentation:
      //   https://developers.google.com/adwords/api/docs/appendix/languagecodes
      LanguageSearchParameter languageParameter = new LanguageSearchParameter();
      Language english = new Language();
      english.id = 1000;
      languageParameter.languages = new Language[] { english };

      // Add network search parameter (optional).
      NetworkSetting networkSetting = new NetworkSetting();
      networkSetting.targetGoogleSearch = true;
      networkSetting.targetSearchNetwork = false;
      networkSetting.targetContentNetwork = false;
      networkSetting.targetPartnerSearchNetwork = false;

      NetworkSearchParameter networkSearchParameter = new NetworkSearchParameter();
      networkSearchParameter.networkSetting = networkSetting;

      // Set the search parameters.
      selector.searchParameters = new SearchParameter[] {
          relatedToQuerySearchParameter, languageParameter, networkSearchParameter
      };

      // Set selector paging (required for targeting idea service).
      Paging paging = Paging.Default;

      TargetingIdeaPage page = new TargetingIdeaPage();

      try {
        int i = 0;
        do {
          // Get related keywords.
          page = targetingIdeaService.get(selector);

          // Display related keywords.
          if (page.entries != null && page.entries.Length > 0) {
            foreach (TargetingIdea targetingIdea in page.entries) {
              string keyword = null;
              string categories = null;
              long averageMonthlySearches = 0;

              foreach (Type_AttributeMapEntry entry in targetingIdea.data) {
                if (entry.key == AttributeType.KEYWORD_TEXT) {
                  keyword = (entry.value as StringAttribute).value;
                }
                if (entry.key == AttributeType.CATEGORY_PRODUCTS_AND_SERVICES) {
                  IntegerSetAttribute categorySet = entry.value as IntegerSetAttribute;
                  StringBuilder builder = new StringBuilder();
                  if (categorySet.value != null) {
                    foreach (int value in categorySet.value) {
                      builder.AppendFormat("{0}, ", value);
                    }
                    categories = builder.ToString().Trim(new char[] { ',', ' ' });
                  }
                }
                if (entry.key == AttributeType.SEARCH_VOLUME) {
                  averageMonthlySearches = (entry.value as LongAttribute).value;
                }
              }
              Console.WriteLine("Keyword with text '{0}', and average monthly search volume " +
                  "'{1}' was found with categories: {2}", keyword, averageMonthlySearches,
                  categories);
              i++;
            }
          }
          selector.paging.IncreaseOffset();
        } while (selector.paging.startIndex < page.totalNumEntries);
        Console.WriteLine("Number of related keywords found: {0}", page.totalNumEntries);
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to retrieve related keywords.", e);
      }
    }
  }
}
