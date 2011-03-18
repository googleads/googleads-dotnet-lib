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
using Google.Api.Ads.AdWords.v201101;

using System;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201101 {
  /// <summary>
  /// This code example creates an experiment using a query percentage of 10,
  /// which defines what fraction of auctions should go to the control split
  /// (90%) vs. the experiment split (10%), then adds experimental bid changes
  /// for criteria and ad groups. To get campaigns, run GetAllCampaigns.cs.
  /// To get ad groups, run GetAllAdGroups.cs. To get criteria, run
  /// GetAllAdGroupCriteria.cs.
  ///
  /// Tags: ExperimentService.mutate
  /// </summary>
  class AddExperiment : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example creates an experiment using a query percentage of 10, which " +
            "defines what fraction of auctions should go to the control split (90%) vs. the " +
            "experiment split (10%), then adds experimental bid changes for criteria and ad " +
            "groups. To get campaigns, run GetAllCampaigns.cs. To get ad groups, run " +
            "GetAllAdGroups.cs. To get criteria, run GetAllAdGroupCriteria.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new AddExperiment();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="user">The AdWords user object running the code example.
    /// </param>
    public override void Run(AdWordsUser user) {
      // Get the ExperimentService.
      ExperimentService experimentService =
          (ExperimentService) user.GetService(AdWordsService.v201101.ExperimentService);

      // Get the AdGroupService.
      AdGroupService adGroupService =
          (AdGroupService) user.GetService(AdWordsService.v201101.AdGroupService);

      // Get the AdGroupCriterionService.
      AdGroupCriterionService adGroupCriterionService =
          (AdGroupCriterionService) user.GetService(AdWordsService.v201101.AdGroupCriterionService);

      long campaignId = long.Parse(_T("INSERT_CAMPAIGN_ID_HERE"));
      long adGroupId = long.Parse(_T("INSERT_AD_GROUP_ID_HERE"));
      long criterionId = long.Parse(_T("INSERT_CRITERION_ID_HERE"));

      // Create experiment.
      Experiment experiment = new Experiment();
      experiment.campaignId = campaignId;
      experiment.name = "Interplanetary Cruise #" + GetTimeStamp();
      experiment.queryPercentage = 10;
      experiment.startDateTime = DateTime.Now.ToString("yyyyMMdd HHmmss");

      // Create operation.
      ExperimentOperation experimentOperation = new ExperimentOperation();
      experimentOperation.@operator = Operator.ADD;
      experimentOperation.operand = experiment;

      try {
        // Add experiment.
        ExperimentReturnValue experimentRetVal = experimentService.mutate(
            new ExperimentOperation[] { experimentOperation });

        if (experimentRetVal != null && experimentRetVal.value != null && experimentRetVal.value.
            Length > 0) {
          long experimentId = 0;

          foreach (Experiment tempExperiment in experimentRetVal.value) {
            Console.WriteLine("Experiment with name = \"{0}\" and id = \"{1}\" was added.\n",
                tempExperiment.name, tempExperiment.id);
            experimentId = tempExperiment.id;
          }

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

          // Create operation.
          AdGroupOperation adGroupOperation = new AdGroupOperation();
          adGroupOperation.operand = adGroup;
          adGroupOperation.@operator = Operator.SET;

          // Update ad group.
          AdGroupReturnValue adGroupRetVal = adGroupService.mutate(new AdGroupOperation[] {
              adGroupOperation });

          // Display results.
          if (adGroupRetVal != null && adGroupRetVal.value != null) {
            foreach (AdGroup tempAdGroup in adGroupRetVal.value) {
              Console.WriteLine("Ad group with name = \"{0}\", id = \"{1}\" and status = \"{2}\" " +
                  "was updated for the experiment.\n", tempAdGroup.name, tempAdGroup.id,
                  tempAdGroup.status);
            }
          } else {
            Console.WriteLine("No ad groups were updated.\n");
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

          // Create operation.
          AdGroupCriterionOperation adGroupCriterionOperation = new AdGroupCriterionOperation();
          adGroupCriterionOperation.operand = adGroupCriterion;
          adGroupCriterionOperation.@operator = Operator.SET;

          // Update ad group criteria.
          AdGroupCriterionReturnValue adGroupCriterionRetVal = adGroupCriterionService.mutate(
              new AdGroupCriterionOperation[] { adGroupCriterionOperation });

          // Display results.
          if (adGroupCriterionRetVal != null && adGroupCriterionRetVal.value != null) {
            foreach (AdGroupCriterion tempAdGroupCriterion in adGroupCriterionRetVal.value) {
              Console.WriteLine("Ad group criterion with ad group id = \"{0}\", criterion id = "
                  + "\"{1}\" and type = \"{2}\" was updated for the experiment.\n",
                  tempAdGroupCriterion.adGroupId, tempAdGroupCriterion.criterion.id,
                  tempAdGroupCriterion.criterion.CriterionType);
            }
          } else {
            Console.WriteLine("No ad group criteria were updated.\n");
          }
        } else {
          Console.WriteLine("No experiments were added.\n");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to add experiment(s). Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
