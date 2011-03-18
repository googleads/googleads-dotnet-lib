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
  /// This example promotes an experiment, which permanently applies all the
  /// experiment changes made to its related ad groups, criteria and ads. To get
  /// experiments, run GetAllExperiments.cs.
  ///
  /// Tags: ExperimentService.mutate
  /// </summary>
  class PromoteExperiment : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This example promotes an experiment, which permanently applies all the experiment" +
            " changes made to its related ad groups, criteria and ads. To get experiments, run " +
            "GetAllExperiments.cs.";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new PromoteExperiment();
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

      long experimentId = long.Parse(_T("INSERT_EXPERIMENT_ID_HERE"));

      // Set experiment's status to PROMOTED.
      Experiment experiment = new Experiment();
      experiment.id = experimentId;
      experiment.status = ExperimentStatus.PROMOTED;

      // Create operation.
      ExperimentOperation operation = new ExperimentOperation();
      operation.@operator = Operator.SET;
      operation.operand = experiment;

      try {
        // Update experiment.
        ExperimentReturnValue retVal = experimentService.mutate(
            new ExperimentOperation[] {operation});

        // Display results.
        if (retVal != null && retVal.value != null) {
          foreach (Experiment tempExperiment in retVal.value) {
            Console.WriteLine("Experiment with name = \"{0}\" and id = \"{1}\" was promoted.\n",
                tempExperiment.name, tempExperiment.id);
          }
        } else {
          Console.WriteLine("No experiments were promoted.\n");
        }
      } catch (Exception ex) {
        Console.WriteLine("Failed to promote experiment(s). Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
