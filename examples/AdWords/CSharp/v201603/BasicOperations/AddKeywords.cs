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
using System.Web;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201603 {
  /// <summary>
  /// This code example adds keywords to an ad group. To get ad groups, run
  /// GetAdGroups.cs.
  /// </summary>
  public class AddKeywords : ExampleBase {
    /// <summary>
    /// Items being added in this code example.
    /// </summary>
    readonly string[] KEYWORDS = new string[] { "mars cruise", "space hotel" };

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      AddKeywords codeExample = new AddKeywords();
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
        return "This code example adds keywords to an ad group. To get ad groups, run " +
            "GetAdGroups.cs.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="adGroupId">Id of the ad group to which keywords are added.
    /// </param>
    public void Run(AdWordsUser user, long adGroupId) {
      // Get the AdGroupCriterionService.
      AdGroupCriterionService adGroupCriterionService =
          (AdGroupCriterionService) user.GetService(
              AdWordsService.v201603.AdGroupCriterionService);

      List<AdGroupCriterionOperation> operations = new List<AdGroupCriterionOperation>();

      foreach (string keywordText in KEYWORDS) {
        // Create the keyword.
        Keyword keyword = new Keyword();
        keyword.text = keywordText;
        keyword.matchType = KeywordMatchType.BROAD;

        // Create the biddable ad group criterion.
        BiddableAdGroupCriterion keywordCriterion = new BiddableAdGroupCriterion();
        keywordCriterion.adGroupId = adGroupId;
        keywordCriterion.criterion = keyword;

        // Optional: Set the user status.
        keywordCriterion.userStatus = UserStatus.PAUSED;

        // Optional: Set the keyword destination url.
        keywordCriterion.finalUrls = new UrlList() {
          urls = new string[] { "http://example.com/mars/cruise/?kw=" +
              HttpUtility.UrlEncode(keywordText) }
        };

        // Create the operations.
        AdGroupCriterionOperation operation = new AdGroupCriterionOperation();
        operation.@operator = Operator.ADD;
        operation.operand = keywordCriterion;

        operations.Add(operation);
      }
      try {
        // Create the keywords.
        AdGroupCriterionReturnValue retVal = adGroupCriterionService.mutate(operations.ToArray());

        // Display the results.
        if (retVal != null && retVal.value != null) {
          foreach (AdGroupCriterion adGroupCriterion in retVal.value) {
            // If you are adding multiple type of criteria, then you may need to
            // check for
            //
            // if (adGroupCriterion is Keyword) { ... }
            //
            // to identify the criterion type.
            Console.WriteLine("Keyword with ad group id = '{0}', keyword id = '{1}', text = " +
                "'{2}' and match type = '{3}' was created.", adGroupCriterion.adGroupId,
                adGroupCriterion.criterion.id, (adGroupCriterion.criterion as Keyword).text,
                (adGroupCriterion.criterion as Keyword).matchType);
          }
        } else {
          Console.WriteLine("No keywords were added.");
        }
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to create keywords.", e);
      }
    }
  }
}
