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
using Google.Api.Ads.AdWords.Util;
using Google.Api.Ads.AdWords.v201603;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201603 {
  /// <summary>
  /// This code example demonstrates how to handle partial failures.
  /// </summary>
  public class HandlePartialFailures : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      HandlePartialFailures codeExample = new HandlePartialFailures();
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
        return "This code example demonstrates how to handle partial failures.";
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
          (AdGroupCriterionService) user.GetService(AdWordsService.v201603.AdGroupCriterionService);

      // Set partial failure mode for the service.
      adGroupCriterionService.RequestHeader.partialFailure = true;

      List<AdGroupCriterionOperation> operations = new List<AdGroupCriterionOperation>();

      // Create the keywords.
      string[] keywords = new String[] {"mars cruise", "inv@lid cruise", "venus cruise",
          "b(a)d keyword cruise"};

      foreach (String keywordText in keywords) {
        Keyword keyword = new Keyword();
        keyword.text = keywordText;
        keyword.matchType = KeywordMatchType.BROAD;

        // Create biddable ad group criterion.
        BiddableAdGroupCriterion keywordBiddableAdGroupCriterion = new BiddableAdGroupCriterion();
        keywordBiddableAdGroupCriterion.adGroupId = adGroupId;
        keywordBiddableAdGroupCriterion.criterion = keyword;

        // Create the operation.
        AdGroupCriterionOperation keywordAdGroupCriterionOperation =
            new AdGroupCriterionOperation();
        keywordAdGroupCriterionOperation.operand = keywordBiddableAdGroupCriterion;
        keywordAdGroupCriterionOperation.@operator = Operator.ADD;
        operations.Add(keywordAdGroupCriterionOperation);
      }

      try {
        // Create the keywords.
        AdGroupCriterionReturnValue result = adGroupCriterionService.mutate(operations.ToArray());

        // Display the results.
        if (result != null && result.value != null) {
          foreach (AdGroupCriterion adGroupCriterionResult in result.value) {
            if (adGroupCriterionResult.criterion != null) {
              Console.WriteLine("Keyword with ad group id '{0}', criterion id '{1}', and " +
                  "text '{2}' was added.\n", adGroupCriterionResult.adGroupId,
                  adGroupCriterionResult.criterion.id,
                  ((Keyword) adGroupCriterionResult.criterion).text);
            }
          }
        } else {
          Console.WriteLine("No keywords were added.");
        }

        // Display the partial failure errors.
        if (result != null && result.partialFailureErrors != null) {
          foreach (ApiError apiError in result.partialFailureErrors) {
            int operationIndex = ErrorUtilities.GetOperationIndex(apiError.fieldPath);
            if (operationIndex != -1) {
              AdGroupCriterion adGroupCriterion = operations[operationIndex].operand;
              Console.WriteLine("Keyword with ad group id '{0}' and text '{1}' "
                  + "triggered a failure for the following reason: '{2}'.\n",
                  adGroupCriterion.adGroupId, ((Keyword) adGroupCriterion.criterion).text,
                  apiError.errorString);
            } else {
              Console.WriteLine("A failure for the following reason: '{0}' has occurred.\n",
                  apiError.errorString);
            }
          }
        }
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to add keywords in partial failure mode.",
            e);
      }
    }
  }
}

