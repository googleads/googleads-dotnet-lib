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

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201603 {
  /// <summary>
  /// This code example adds demographic target criteria to an ad group. To get
  /// ad groups, run AddAdGroup.cs.
  /// </summary>
  public class AddAdGroupDemographicCriteria : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      AddAdGroupDemographicCriteria codeExample = new AddAdGroupDemographicCriteria();
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
        return "This code example adds demographic target criteria to an ad group. To get ad " +
            "groups, run AddAdGroup.cs.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="adGroupId">Id of the ad group to which criteria are
    /// added.</param>
    public void Run(AdWordsUser user, long adGroupId) {
      // Get the AdGroupCriterionService.
      AdGroupCriterionService adGroupCriterionService =
          (AdGroupCriterionService) user.GetService(AdWordsService.v201603.AdGroupCriterionService);

      // Create biddable ad group criterion for gender
      Gender genderTarget = new Gender();
      // Criterion Id for male. The IDs can be found here
      // https://developers.google.com/adwords/api/docs/appendix/genders
      genderTarget.id = 10;

      BiddableAdGroupCriterion genderBiddableAdGroupCriterion = new BiddableAdGroupCriterion();
      genderBiddableAdGroupCriterion.adGroupId = adGroupId;
      genderBiddableAdGroupCriterion.criterion = genderTarget;

      // Create negative ad group criterion for age range
      AgeRange ageRangeNegative = new AgeRange();
      // Criterion Id for age 18 to 24. The IDs can be found here
      // https://developers.google.com/adwords/api/docs/appendix/ages

      ageRangeNegative.id = 503001;
      NegativeAdGroupCriterion ageRangeNegativeAdGroupCriterion = new NegativeAdGroupCriterion();
      ageRangeNegativeAdGroupCriterion.adGroupId = adGroupId;
      ageRangeNegativeAdGroupCriterion.criterion = ageRangeNegative;

      // Create operations.
      AdGroupCriterionOperation genderBiddableAdGroupCriterionOperation =
          new AdGroupCriterionOperation();
      genderBiddableAdGroupCriterionOperation.operand = genderBiddableAdGroupCriterion;
      genderBiddableAdGroupCriterionOperation.@operator = Operator.ADD;

      AdGroupCriterionOperation ageRangeNegativeAdGroupCriterionOperation =
          new AdGroupCriterionOperation();
      ageRangeNegativeAdGroupCriterionOperation.operand = ageRangeNegativeAdGroupCriterion;
      ageRangeNegativeAdGroupCriterionOperation.@operator = Operator.ADD;

      AdGroupCriterionOperation[] operations = new AdGroupCriterionOperation[] {
          genderBiddableAdGroupCriterionOperation, ageRangeNegativeAdGroupCriterionOperation};

      try {
        // Add ad group criteria.
        AdGroupCriterionReturnValue result = adGroupCriterionService.mutate(operations);

        // Display ad group criteria.
        if (result != null && result.value != null) {
          foreach (AdGroupCriterion adGroupCriterionResult in result.value) {
            Console.WriteLine("Ad group criterion with ad group id \"{0}\", criterion id " +
                "\"{1}\", and type \"{2}\" was added.", adGroupCriterionResult.adGroupId,
                adGroupCriterionResult.criterion.id,
                adGroupCriterionResult.criterion.CriterionType);
          }
        } else {
          Console.WriteLine("No ad group criteria were added.");
        }
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to create ad group criteria.", e);
      }
    }
  }
}
