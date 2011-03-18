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
using Google.Api.Ads.AdWords.v201008;

using System;
using System.Collections.Generic;
using System.Text;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201008 {
  /// <summary>
  /// This example gets all experiments in a campaign. To add an experiment, run
  /// AddExperiment.cs. To get campaigns, run GetAllCampaigns.cs.
  ///
  /// Tags: ExperimentService.mutate
  /// </summary>
  class GetAllExperiments : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This example gets all experiments in a campaign. To add an experiment, run " +
            "AddExperiment.cs. To get campaigns, run GetAllCampaigns.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetAllExperiments();
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
          (ExperimentService) user.GetService(AdWordsService.v201008.ExperimentService);

      long campaignId = long.Parse(_T("INSERT_CAMPAIGN_ID_HERE"));

      // Create selector.
      ExperimentSelector selector = new ExperimentSelector();
      selector.campaignIds = new long[] {campaignId};
      selector.includeStats = true;

      try {
        // Get all experiments.
        ExperimentPage page = experimentService.get(selector);

        // Display results.
        if (page != null && page.entries != null) {
          foreach (Experiment experiment in page.entries) {
            ExperimentSummaryStats stats = experiment.experimentSummaryStats;
            Console.WriteLine("Experiment with name = \"{0}\", id = \"{1}\" and control id = " +
                "\"{2}\" was found and it includes {3} ad group(s) and {4} criteria.\n",
                experiment.name, experiment.id, experiment.controlId, stats.adGroupsCount,
                stats.adGroupCriteriaCount);
          }
        } else {
          Console.WriteLine("No experiments were found.\n");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to get experiment(s). Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
