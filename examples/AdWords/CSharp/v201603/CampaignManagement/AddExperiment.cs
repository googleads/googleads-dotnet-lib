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
  /// This code example creates an experiment using a query percentage of 10,
  /// which defines what fraction of auctions should go to the control split
  /// (90%) vs. the experiment split (10%), then adds experimental bid changes
  /// for criteria and ad groups. To get campaigns, run GetCampaigns.cs.
  /// To get ad groups, run GetAdGroups.cs. To get criteria, run
  /// GetKeywords.cs.
  /// </summary>
  public class AddExperiment : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      AddExperiment codeExample = new AddExperiment();
      Console.WriteLine(codeExample.Description);
      try {
        long campaignId = long.Parse("INSERT_CAMPAIGN_ID_HERE");
        long adGroupId = long.Parse("INSERT_ADGROUP_ID_HERE");
        long criterionId = long.Parse("INSERT_CRITERION_ID_HERE");
        codeExample.Run(new AdWordsUser(), campaignId, adGroupId, criterionId);
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
        return "This code example creates an experiment using a query percentage of 10, which " +
            "defines what fraction of auctions should go to the control split (90%) vs. the " +
            "experiment split (10%), then adds experimental bid changes for criteria and ad " +
            "groups. To get campaigns, run GetCampaigns.cs. To get ad groups, run " +
            "GetAdGroups.cs. To get criteria, run GetKeywords.cs.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="campaignId">Id of the campaign to which experiments are
    /// added.</param>
    /// <param name="adGroupId">Id of the ad group to which experiments are
    /// added.</param>
    /// <param name="criterionId">Id of the criterion for which experiments
    /// are added.</param>
    public void Run(AdWordsUser user, long campaignId, long adGroupId, long criterionId) {
      // Get the ExperimentService.
      ExperimentService experimentService =
          (ExperimentService) user.GetService(AdWordsService.v201603.ExperimentService);

      // Get the AdGroupService.
      AdGroupService adGroupService =
          (AdGroupService) user.GetService(AdWordsService.v201603.AdGroupService);

      // Get the AdGroupCriterionService.
      AdGroupCriterionService adGroupCriterionService =
          (AdGroupCriterionService) user.GetService(AdWordsService.v201603.AdGroupCriterionService);

      // Create the experiment.
      Experiment experiment = new Experiment();
      experiment.campaignId = campaignId;
      experiment.name = "Interplanetary Cruise #" + ExampleUtilities.GetRandomString();
      experiment.queryPercentage = 10;
      experiment.startDateTime = DateTime.Now.AddDays(1).ToString("yyyyMMdd HHmmss");

      // Optional: Set the end date.
      experiment.endDateTime = DateTime.Now.AddDays(30).ToString("yyyyMMdd HHmmss");

      // Optional: Set the status.
      experiment.status = ExperimentStatus.ENABLED;

      // Create the operation.
      ExperimentOperation experimentOperation = new ExperimentOperation();
      experimentOperation.@operator = Operator.ADD;
      experimentOperation.operand = experiment;

      try {
        // Add the experiment.
        ExperimentReturnValue experimentRetVal = experimentService.mutate(
            new ExperimentOperation[] {experimentOperation});

        // Display the results.
        if (experimentRetVal != null && experimentRetVal.value != null && experimentRetVal.value.
            Length > 0) {
          long experimentId = 0;

          Experiment newExperiment = experimentRetVal.value[0];

          Console.WriteLine("Experiment with name = \"{0}\" and id = \"{1}\" was added.\n",
              newExperiment.name, newExperiment.id);
          experimentId = newExperiment.id;

          // Set ad group for the experiment.
          AdGroup adGroup = new AdGroup();
          adGroup.id = adGroupId;

          // Create experiment bid multiplier rule that will modify ad group bid
          // for the experiment.
          ManualCPCAdGroupExperimentBidMultipliers adGroupBidMultiplier =
              new ManualCPCAdGroupExperimentBidMultipliers();
          adGroupBidMultiplier.maxCpcMultiplier = new BidMultiplier();
          adGroupBidMultiplier.maxCpcMultiplier.multiplier = 1.5;

          // Set experiment data to the ad group.
          AdGroupExperimentData adGroupExperimentData = new AdGroupExperimentData();
          adGroupExperimentData.experimentId = experimentId;
          adGroupExperimentData.experimentDeltaStatus = ExperimentDeltaStatus.MODIFIED;
          adGroupExperimentData.experimentBidMultipliers = adGroupBidMultiplier;

          adGroup.experimentData = adGroupExperimentData;

          // Create the operation.
          AdGroupOperation adGroupOperation = new AdGroupOperation();
          adGroupOperation.operand = adGroup;
          adGroupOperation.@operator = Operator.SET;

          // Update the ad group.
          AdGroupReturnValue adGroupRetVal = adGroupService.mutate(new AdGroupOperation[] {
              adGroupOperation});

          // Display the results.
          if (adGroupRetVal != null && adGroupRetVal.value != null &&
              adGroupRetVal.value.Length > 0) {
            AdGroup updatedAdGroup = adGroupRetVal.value[0];
            Console.WriteLine("Ad group with name = \"{0}\", id = \"{1}\" and status = \"{2}\" " +
                "was updated for the experiment.\n", updatedAdGroup.name, updatedAdGroup.id,
                updatedAdGroup.status);
          } else {
            Console.WriteLine("No ad groups were updated.");
          }

          // Set ad group criteria for the experiment.
          Criterion criterion = new Criterion();
          criterion.id = criterionId;

          BiddableAdGroupCriterion adGroupCriterion = new BiddableAdGroupCriterion();
          adGroupCriterion.adGroupId = adGroupId;
          adGroupCriterion.criterion = criterion;

          // Create experiment bid multiplier rule that will modify criterion bid
          // for the experiment.
          ManualCPCAdGroupCriterionExperimentBidMultiplier bidMultiplier =
              new ManualCPCAdGroupCriterionExperimentBidMultiplier();
          bidMultiplier.maxCpcMultiplier = new BidMultiplier();
          bidMultiplier.maxCpcMultiplier.multiplier = 1.5;

          // Set experiment data to the criterion.
          BiddableAdGroupCriterionExperimentData adGroupCriterionExperimentData =
              new BiddableAdGroupCriterionExperimentData();
          adGroupCriterionExperimentData.experimentId = experimentId;
          adGroupCriterionExperimentData.experimentDeltaStatus = ExperimentDeltaStatus.MODIFIED;
          adGroupCriterionExperimentData.experimentBidMultiplier = bidMultiplier;

          adGroupCriterion.experimentData = adGroupCriterionExperimentData;

          // Create the operation.
          AdGroupCriterionOperation adGroupCriterionOperation = new AdGroupCriterionOperation();
          adGroupCriterionOperation.operand = adGroupCriterion;
          adGroupCriterionOperation.@operator = Operator.SET;

          // Update the ad group criteria.
          AdGroupCriterionReturnValue adGroupCriterionRetVal = adGroupCriterionService.mutate(
              new AdGroupCriterionOperation[] {adGroupCriterionOperation});

          // Display the results.
          if (adGroupCriterionRetVal != null && adGroupCriterionRetVal.value != null &&
              adGroupCriterionRetVal.value.Length > 0) {
            AdGroupCriterion updatedAdGroupCriterion = adGroupCriterionRetVal.value[0];
            Console.WriteLine("Ad group criterion with ad group id = \"{0}\", criterion id = "
                + "\"{1}\" and type = \"{2}\" was updated for the experiment.\n",
                updatedAdGroupCriterion.adGroupId, updatedAdGroupCriterion.criterion.id,
                updatedAdGroupCriterion.criterion.CriterionType);
          } else {
            Console.WriteLine("No ad group criteria were updated.");
          }
        } else {
          Console.WriteLine("No experiments were added.");
        }
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to add experiment.", e);
      }
    }
  }
}
