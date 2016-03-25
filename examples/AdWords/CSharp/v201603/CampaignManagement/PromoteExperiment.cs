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
  /// This example promotes an experiment, which permanently applies all the
  /// experiment changes made to its related ad groups, criteria and ads. To
  /// create an experiment, run AddExperiment.vb.
  /// </summary>
  public class PromoteExperiment : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      PromoteExperiment codeExample = new PromoteExperiment();
      Console.WriteLine(codeExample.Description);
      try {
        long experimentId = long.Parse("INSERT_EXPERIMENT_ID_HERE");
        codeExample.Run(new AdWordsUser(), experimentId);
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
        return "This example promotes an experiment, which permanently applies all the experiment" +
            " changes made to its related ad groups, criteria and ads. To create an experiment, " +
            "run AddExperiment.cs.";
      }
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="experimentId">Id of the experiment to be promoted.</param>
    public void Run(AdWordsUser user, long experimentId) {
      // Get the ExperimentService.
      ExperimentService experimentService =
          (ExperimentService) user.GetService(AdWordsService.v201603.ExperimentService);

      // Set experiment's status to PROMOTED.
      Experiment experiment = new Experiment();
      experiment.id = experimentId;
      experiment.status = ExperimentStatus.PROMOTED;

      // Create the operation.
      ExperimentOperation operation = new ExperimentOperation();
      operation.@operator = Operator.SET;
      operation.operand = experiment;

      try {
        // Update the experiment.
        ExperimentReturnValue retVal = experimentService.mutate(
            new ExperimentOperation[] {operation});

        // Display the results.
        if (retVal != null && retVal.value != null && retVal.value.Length > 0) {
          Experiment promotedExperiment = retVal.value[0];
          Console.WriteLine("Experiment with name = \"{0}\" and id = \"{1}\" was promoted.\n",
              promotedExperiment.name, promotedExperiment.id);
        } else {
          Console.WriteLine("No experiments were promoted.");
        }
      } catch (Exception e) {
        throw new System.ApplicationException("Failed to promote experiment.", e);
      }
    }
  }
}
