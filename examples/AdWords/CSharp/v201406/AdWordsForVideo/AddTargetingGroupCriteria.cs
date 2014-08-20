// Copyright 2014, Google Inc. All Rights Reserved.
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
using Google.Api.Ads.AdWords.v201406;

using System;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201406 {

  /// <summary>
  /// This code example illustrates how to create targeting group criteria.
  ///
  /// Tags: VideoTargetingGroupCriterionService.mutate
  /// </summary>
  /// <remarks>AdWords for Video API is a Beta feature.</remarks>
  public class AddTargetingGroupCriteria : ExampleBase {

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example illustrates how to create targeting group criteria.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      AddTargetingGroupCriteria codeExample = new AddTargetingGroupCriteria();
      Console.WriteLine(codeExample.Description);
      try {
        long campaignId = long.Parse("INSERT_CAMPAIGN_ID_HERE");
        long targetingGroupId = long.Parse("INSERT_TARGETING_GROUP_ID_HERE");
        codeExample.Run(new AdWordsUser(), campaignId, targetingGroupId);
      } catch (Exception ex) {
        Console.WriteLine("An exception occurred while running this code example. {0}",
            ExampleUtilities.FormatException(ex));
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="campaignId">The campaign ID.</param>
    /// <param name="targetingGroupId">The targeting group ID.</param>
    public void Run(AdWordsUser user, long campaignId, long targetingGroupId) {
      // Get the VideoTargetingGroupCriterionService.
      VideoTargetingGroupCriterionService videoTargetingGroupCriterionService =
          (VideoTargetingGroupCriterionService) user.GetService(
              AdWordsService.v201406.VideoTargetingGroupCriterionService);

      // Create age criteria.
      TargetingGroupCriterion ageCriterion = new TargetingGroupCriterion();
      ageCriterion.campaignId = campaignId;
      ageCriterion.targetingGroupId = targetingGroupId;
      AudienceAge age = new AudienceAge();
      age.ageRange = AudienceAgeAgeRange.AGE_RANGE_18_24;
      ageCriterion.criterion = age;

      TargetingGroupCriterionOperation addAgeCriterionOperation =
          new TargetingGroupCriterionOperation();
      addAgeCriterionOperation.operand = ageCriterion;
      addAgeCriterionOperation.@operator = Operator.ADD;

      // Create negative age criteria.
      NegativeTargetingGroupCriterion negativeAgeCriterion = new NegativeTargetingGroupCriterion();
      negativeAgeCriterion.campaignId = campaignId;
      negativeAgeCriterion.targetingGroupId = targetingGroupId;
      AudienceAge negativeAge = new AudienceAge();
      negativeAge.ageRange = AudienceAgeAgeRange.AGE_RANGE_65_UP;
      negativeAgeCriterion.criterion = negativeAge;

      TargetingGroupCriterionOperation addNegativeAgeCriterionOperation =
          new TargetingGroupCriterionOperation();
      addNegativeAgeCriterionOperation.operand = negativeAgeCriterion;
      addNegativeAgeCriterionOperation.@operator = Operator.ADD;

      // Create gender criteria.
      TargetingGroupCriterion genderCriterion = new TargetingGroupCriterion();
      AudienceGender gender = new AudienceGender();
      gender.genderType = AudienceGenderGenderType.GENDER_MALE;
      genderCriterion.campaignId = campaignId;
      genderCriterion.targetingGroupId = targetingGroupId;
      genderCriterion.criterion = gender;

      TargetingGroupCriterionOperation addGenderCriterionOperation =
          new TargetingGroupCriterionOperation();
      addGenderCriterionOperation.operand = genderCriterion;
      addGenderCriterionOperation.@operator = Operator.ADD;

      // Create topic criteria.
      // See https://developers.google.com/adwords/api/docs/appendix/verticals
      // for valid ids.
      TargetingGroupCriterion topicCriterion = new TargetingGroupCriterion();
      Topic topic = new Topic();
      // Autos & Vehicles > Vehicle Brands > Buick
      topic.verticalId = 1060L;

      topicCriterion.campaignId = campaignId;
      topicCriterion.targetingGroupId = targetingGroupId;
      topicCriterion.criterion = topic;

      TargetingGroupCriterionOperation addTopicCriterionOperation =
          new TargetingGroupCriterionOperation();
      addTopicCriterionOperation.operand = topicCriterion;
      addTopicCriterionOperation.@operator = Operator.ADD;

      // Add audience interest criteria.
      // See https://developers.google.com/adwords/api/docs/appendix/verticals
      // for valid ids.
      TargetingGroupCriterion interestCriterion = new TargetingGroupCriterion();
      AudienceInterest interest = new AudienceInterest();
      // Computers & Electronics > Computer Hardware > Laptops & Notebooks > Tablet PCs
      interest.interestId = 1277L;

      interestCriterion.campaignId = campaignId;
      interestCriterion.targetingGroupId = targetingGroupId;
      interestCriterion.criterion = topic;

      TargetingGroupCriterionOperation addUserInterestCriteriaOperation =
          new TargetingGroupCriterionOperation();
      addUserInterestCriteriaOperation.operand = interestCriterion;
      addUserInterestCriteriaOperation.@operator = Operator.ADD;

      try {
        TargetingGroupCriterionReturnValue retval = videoTargetingGroupCriterionService.mutate(
            new TargetingGroupCriterionOperation[] {
                addAgeCriterionOperation,
                addGenderCriterionOperation,
                addNegativeAgeCriterionOperation,
                addTopicCriterionOperation,
                addUserInterestCriteriaOperation
            });
        if (retval != null && retval.value != null) {
          foreach (TargetingGroupCriterion newCriterion in retval.value) {
            Console.WriteLine("Added targeting group criteria with id = {0} and type = {1} " +
              "to campaign id {2}.",
              newCriterion.criterion.id, newCriterion.criterion.BaseCriterionType,
              newCriterion.campaignId);
          }
        } else {
          Console.WriteLine("No new targeting group criteria were added.");
        }
      } catch (Exception ex) {
        throw new System.ApplicationException("Failed to add targeting group criteria.", ex);
      }
    }
  }
}