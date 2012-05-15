// Copyright 2012, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdWords.v201109_1;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201109_1 {
  /// <summary>
  /// This code example adds a keyword to an ad group. To get ad groups, run
  /// GetAdGroups.cs.
  ///
  /// Tags: AdGroupCriterionService.mutate
  /// </summary>
  public class AddKeywords : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      ExampleBase codeExample = new AddKeywords();
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
        return "This code example adds a keyword to an ad group. To get ad groups, run " +
            "GetAdGroups.cs.";
      }
    }

    /// <summary>
    /// Gets the list of parameter names required to run this code example.
    /// </summary>
    /// <returns>
    /// A list of parameter names for this code example.
    /// </returns>
    public override string[] GetParameterNames() {
      return new string[] {"ADGROUP_ID"};
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
          (AdGroupCriterionService) user.GetService(AdWordsService.v201109_1.AdGroupCriterionService);

      long adGroupId = long.Parse(parameters["ADGROUP_ID"]);

      // Create the keyword.
      Keyword keyword1 = new Keyword();
      keyword1.text = "mars cruise";
      keyword1.matchType = KeywordMatchType.BROAD;

      // Create the biddable ad group criterion.
      BiddableAdGroupCriterion keywordCriterion1 = new BiddableAdGroupCriterion();
      keywordCriterion1.adGroupId = adGroupId;
      keywordCriterion1.criterion = keyword1;

      // Optional: Set the user status.
      keywordCriterion1.userStatus = UserStatus.PAUSED;

      // Optional: Set the keyword destination url.
      keywordCriterion1.destinationUrl = "http://example.com/mars/cruise";

      // Create the keyword.
      Keyword keyword2 = new Keyword();
      keyword2.text = "mars chocolates";
      keyword2.matchType = KeywordMatchType.EXACT;

      // Create the biddable ad group criterion.
      NegativeAdGroupCriterion keywordCriterion2 = new NegativeAdGroupCriterion();
      keywordCriterion2.adGroupId = adGroupId;
      keywordCriterion2.criterion = keyword2;

      // Create the operations.
      AdGroupCriterionOperation keywordOperation1 = new AdGroupCriterionOperation();
      keywordOperation1.@operator = Operator.ADD;
      keywordOperation1.operand = keywordCriterion1;

      AdGroupCriterionOperation keywordOperation2 = new AdGroupCriterionOperation();
      keywordOperation2.@operator = Operator.ADD;
      keywordOperation2.operand = keywordCriterion2;

      try {
        // Create the keywords.
        AdGroupCriterionReturnValue retVal = adGroupCriterionService.mutate(
            new AdGroupCriterionOperation[] {keywordOperation1, keywordOperation2});

        // Display the results.
        if (retVal != null && retVal.value != null) {
          foreach (AdGroupCriterion adGroupCriterion in retVal.value) {
            // If you are adding multiple type of criteria, then you may need to
            // check for
            //
            // if (adGroupCriterion is Keyword) { ... }
            //
            // to identify the criterion type.
            writer.WriteLine("Keyword with ad group id = '{0}', keyword id = '{1}', text = " +
                "'{2}' and match type = '{3}' was created.", adGroupCriterion.adGroupId,
                adGroupCriterion.criterion.id, (adGroupCriterion.criterion as Keyword).text,
                (adGroupCriterion.criterion as Keyword).matchType);
          }
        } else {
          writer.WriteLine("No keywords were added.");
        }
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to create keywords.", ex);
      }
    }
  }
}
